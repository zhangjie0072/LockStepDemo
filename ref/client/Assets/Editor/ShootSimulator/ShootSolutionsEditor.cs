using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShootSolutionEditor : EditorWindow
{
	static public ShootSolutionEditor instance;
	public ShootSolution mCurSelectedSolution;
	
	UBasketball	mBall;
	UBasket		mBasket;
	Vector2 	mSuccessScroll = Vector2.zero;
	Vector2 	mFailScroll = Vector2.zero;

	int			mCurSelectSector = -1;
	int			mDistanceCount = 0;
	int			mAngleCount = 0;
	float		mDistanceStep = 1.5f;
	
	List<int>	mDelNamesSuccess = new List<int>();
	List<int>	mDelNamesFail = new List<int>();

	private List<Vector3> m_shootCurveKeys = new List<Vector3>();

	void OnEnable () 
	{ 
		instance = this;

		if( Selection.activeGameObject == null )
			return;
		mBall = Selection.activeGameObject.GetComponent<UBasketball>();
		if( mBall == null )
			EditorUtility.DisplayDialog("ShootSolutionEditor", "请选择篮球", "ok");

		GameObject goBasket = GameObject.FindGameObjectWithTag("basketStand");
		if( goBasket == null )
			EditorUtility.DisplayDialog("ShootSolutionEditor", "请先创建篮板", "ok");
		mBasket = goBasket.GetComponent<UBasket>();

		mDistanceCount = GameSystem.Instance.shootSolutionManager.m_DistanceList.Count;
		mAngleCount = GameSystem.Instance.shootSolutionManager.m_AngleList.Count;
	}
	void OnDisable () { instance = null; }

	void OnDestroy()
	{
		SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
	}

	void OnSceneGUI(SceneView sceneView) 
	{
		if( mBasket == null )
			return;
		Vector3 ballTo = mBall.transform.position;
		ballTo.y = 0.0f;
		Handles.color = Color.red;
		Handles.DrawLine( mBall.transform.position, ballTo );

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		if( ShootSimulator.instance != null )
			Handles.Label( ballTo, ShootSimulator.instance.m_iCurSector.ToString(), style );

		_DrawSectors( (Vector3)mBasket.m_rim.center );
		_DrawCurves();
	}

	void OnGUI ()
	{
		if(SceneView.onSceneGUIDelegate != this.OnSceneGUI)
			SceneView.onSceneGUIDelegate += this.OnSceneGUI;

		if( mBall == null )
			return;

		//NGUIEditorTools.SetLabelWidth(100f);
		GUILayout.Space(3f);
		
		NGUIEditorTools.DrawHeader("Input");
		NGUIEditorTools.BeginContents();
		GUILayout.BeginHorizontal();
		{
			string resPath = "Assets/Resources/";
			if(GUILayout.Button("Save", GUILayout.ExpandWidth(true)))
			{
				//save solution manager
				string path = resPath + GlobalConst.DIR_XML_SHOOT_SOLUTION + "shootsolutionset";
				GameSystem.Instance.shootSolutionManager.SaveShootSolutionSet(path);
				List<ShootSolutionManager.ShootSolutionSector> sectors = GameSystem.Instance.shootSolutionManager.m_ShootSolutionSectors;
				foreach( ShootSolutionManager.ShootSolutionSector sector in sectors )
					GameSystem.Instance.shootSolutionManager.SaveShootSolutionSector(sector.index, resPath + GlobalConst.DIR_XML_SHOOT_SOLUTION + "shoot");
				EditorUtility.DisplayDialog("ShootSolutionEditor", "Save complete.", "ok");
			}
			if(GUILayout.Button("Load", GUILayout.ExpandWidth(true)))
			{
				//load solution manager
                string path = GlobalConst.DIR_XML_SHOOT_SOLUTION + "shootsolutionset";
				if( !GameSystem.Instance.shootSolutionManager.LoadShootSolutionSet(path, true) )
					EditorUtility.DisplayDialog("Solution", "load shoot solution set failed.", "ok");
				else
				{
					mDistanceCount = GameSystem.Instance.shootSolutionManager.m_DistanceList.Count;
					mAngleCount = GameSystem.Instance.shootSolutionManager.m_AngleList.Count;
					if( GameSystem.Instance.shootSolutionManager.m_AngleList.Count > 1 )
						mDistanceStep = GameSystem.Instance.shootSolutionManager.m_DistanceList[0].ToUnity2();
				}
				ShootSimulator.instance.Repaint();
				SceneView.RepaintAll();
			}
		}
		GUILayout.EndHorizontal();
		NGUIEditorTools.EndContents();

		NGUIEditorTools.DrawHeader("Sectors");
		NGUIEditorTools.BeginContents();
		{
			GUILayout.BeginHorizontal();
			mAngleCount = Mathf.Clamp(EditorGUILayout.IntField("Angle: ", mAngleCount, GUILayout.ExpandWidth(true)), 1, 8);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			mDistanceCount = Mathf.Clamp(EditorGUILayout.IntField("Distance: ", mDistanceCount, GUILayout.ExpandWidth(true)), 1, 8);
			mDistanceStep = Mathf.Clamp( EditorGUILayout.FloatField("Distance step: ", mDistanceStep, GUILayout.ExpandWidth(true)), 1.0f, 2.0f);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label(string.Format("Sector Count: {0}", (mAngleCount + 1)*(mDistanceCount + 1)));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if( GUILayout.Button("Generate", GUILayout.Height(20f)) )
			{
				GameSystem.Instance.shootSolutionManager.Reset();
				for( int idx = 1; idx <= mDistanceCount; idx++ )
					GameSystem.Instance.shootSolutionManager.m_DistanceList.Add(IM.Number.ToIMNumber(mDistanceStep) * idx);

				float angleStep = 180.0f / ( mAngleCount + 1 );
				for( int idx = 1; idx <= mAngleCount; idx++ )
					GameSystem.Instance.shootSolutionManager.m_AngleList.Add(IM.Number.ToIMNumber(angleStep) * idx);

				int iNumSector = (mDistanceCount + 1) * (mAngleCount + 1);
				for( int idx = 0; idx < iNumSector; idx++)
				{
					ShootSolutionManager.ShootSolutionSector sector = new ShootSolutionManager.ShootSolutionSector(idx);
					GameSystem.Instance.shootSolutionManager.m_ShootSolutionSectors.Add(sector);
				}

				ShootSimulator.instance.Repaint();
				SceneView.RepaintAll();
			}
			GUILayout.EndHorizontal();
		}
		NGUIEditorTools.EndContents();

		int iSectorCount = GameSystem.Instance.shootSolutionManager.m_ShootSolutionSectors.Count;
		if( iSectorCount == 0 )
			return;

		bool delete = false;

		if( Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Escape )
		{
			mCurSelectedSolution = null;
			if( ShootSimulator.instance )
				ShootSimulator.instance.Repaint();
		}

		NGUIEditorTools.DrawHeader("Solutions", true);
		NGUIEditorTools.BeginContents();
		{
			GUILayout.BeginHorizontal();
			string[] strSectors = new string[iSectorCount];
			for( int idx = 0; idx != iSectorCount; idx++ )
				strSectors[idx] = idx.ToString();
			mCurSelectSector = EditorGUILayout.Popup("CurrentSector", mCurSelectSector, strSectors);
			GUILayout.EndHorizontal();

			if( mCurSelectSector >= iSectorCount || mCurSelectSector < 0)
			{
				NGUIEditorTools.EndContents();
				return;
			}

			if( ShootSimulator.instance != null && ShootSimulator.instance.m_simulating )
			{
				mCurSelectSector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(mBasket.m_rim.center, mBall.position);
				Repaint();
			}

			ShootSolutionManager.ShootSolutionSector curSector = GameSystem.Instance.shootSolutionManager.m_ShootSolutionSectors[mCurSelectSector];
			GUILayout.BeginHorizontal();
			{
				if(GUILayout.Button("Add current solution.", GUILayout.Width(240.0f)))
				{
					if (ShootSimulator.instance != null && ShootSimulator.instance.m_simulating)
					{
						ShootSolution solution = mBall.m_shootSolution.Clone();
						int actualIndex = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(mBasket.m_rim.center, mBall.position);
						ShootSolutionManager.ShootSolutionSector actualSector = GameSystem.Instance.shootSolutionManager.m_ShootSolutionSectors[actualIndex];
						if (actualSector != curSector)
							Debug.LogError("Actual index is not current index. Contact to programmer.");
						curSector.AddSolution(solution);
						mCurSelectedSolution = solution;
					}
					else
					{
						EditorUtility.DisplayDialog("ShootSolutionEditor", "Make a solution by simulate on UBasketball component at the first.", "ok");
						GUILayout.EndHorizontal();
					}
				}
			}
			GUILayout.EndHorizontal();

			NGUIEditorTools.DrawHeader("Success", true);
			GUILayout.BeginHorizontal();
			GUILayout.Space(3f);
			GUILayout.BeginVertical();
			mSuccessScroll = GUILayout.BeginScrollView(mSuccessScroll, GUILayout.Height(250f));

			_ListSolutions(ref curSector.success, true);

			GUILayout.EndScrollView();
			GUILayout.Space(3f);
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();

			NGUIEditorTools.DrawHeader("Fail", true);
			GUILayout.BeginHorizontal();
			GUILayout.Space(3f);
			GUILayout.BeginVertical();
			mFailScroll = GUILayout.BeginScrollView(mFailScroll, GUILayout.Height(250f));
			
			_ListSolutions(ref curSector.fail, false);
			
			GUILayout.EndScrollView();
			GUILayout.Space(3f);
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();


			Repaint();
		}
		NGUIEditorTools.EndContents();
	}

	void _ListSolutions(ref List<ShootSolution> solutions, bool success)
	{
		ShootSolution toDelSolution = null;
		foreach( ShootSolution solution in solutions )
		{
			GUILayout.Space(-1f);
			GUI.backgroundColor = solution == mCurSelectedSolution ? Color.white : new Color(0.8f, 0.8f, 0.8f);
			GUILayout.BeginHorizontal("AS TextArea", GUILayout.MaxHeight(20f));
			GUI.backgroundColor = Color.white;
			
			int idx = solutions.IndexOf(solution);
			GUILayout.Label( idx.ToString(), GUILayout.Width(24f));
			
			if (GUILayout.Button("solution_" + idx.ToString(), "OL TextField", GUILayout.MaxHeight(20f)))
				mCurSelectedSolution = solution;

			if (success)
				solution.m_bCleanShot = GUILayout.Toggle(solution.m_bCleanShot, "Clean shot");

			solution.m_type = (ShootSolution.Type)EditorGUILayout.EnumPopup(solution.m_type);

			List<int> delNames = success ? mDelNamesSuccess : mDelNamesFail;
			
			if (delNames.Contains(idx))
			{
				GUI.backgroundColor = Color.red;
				if (GUILayout.Button("Delete", GUILayout.Width(60f)))
					toDelSolution = solution;
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("X", GUILayout.Width(22f)))
					delNames.Remove(idx);
				GUI.backgroundColor = Color.white;
			}
			else
			{
				if (GUILayout.Button("X", GUILayout.Width(22f))) 
					delNames.Add(idx);
			}
			GUILayout.EndHorizontal();
		}

		if( toDelSolution != null )
		{
			if( toDelSolution == mCurSelectedSolution )
				mCurSelectedSolution = null;
			solutions.Remove(toDelSolution);
		}

		if( ShootSimulator.instance == null )
			return;

		if( mCurSelectedSolution != null && mBall.m_shootSolution != mCurSelectedSolution )
		{
			mBall.m_shootSolution = mCurSelectedSolution;
			mBall.position = mCurSelectedSolution.m_vInitPos;
			Vector3 dirBall2BasketH = ((Vector3)mBasket.m_rim.center - mBall.transform.position).normalized;
			dirBall2BasketH.y = 0.0f;

			Vector3 dirInitVel = mCurSelectedSolution.m_vInitVel.normalized.ToUnity2();
			Vector3 dirInitVelH = dirInitVel;
			dirInitVelH.y = 0.0f;
			ShootSimulator.instance.m_vAngleAdjustment.y = Vector3.Angle(dirInitVelH, dirInitVel);
			ShootSimulator.instance.m_vAngleAdjustment.x = Vector3.Cross(dirInitVelH, dirBall2BasketH).y > 0.0f ? -Vector3.Angle(dirInitVelH, dirBall2BasketH) : Vector3.Angle(dirInitVelH, dirBall2BasketH);
			ShootSimulator.instance.m_fSpeed = mCurSelectedSolution.m_vInitVel.magnitude.ToUnity2();
			ShootSimulator.instance.m_fBounceBackboard = mCurSelectedSolution.m_fBounceBackboard.ToUnity2();
			ShootSimulator.instance.m_vBounceRimAdjustment = mCurSelectedSolution.m_vBounceRimAdjustment.ToUnity2();

			ShootSimulator.instance.Repaint();
			SceneView.RepaintAll();
		}
	}

	void _DrawCurves()
	{
		//draw shoot solution
		ShootSolution solution = mBall.m_shootSolution;
		if( solution == null )
			return;
		if( solution.m_fTime > new IM.Number(1000))
			return;
		_BuildShootCurves();
		int iKeyCount = m_shootCurveKeys.Count;
		for( int idx = 0; idx != iKeyCount; idx++ )
		{
			if( idx + 1 >= iKeyCount )
				break;
			if( solution.m_bSuccess )
				Handles.color = Color.red;
			else
			{
				if( ShootSimulator.instance != null && solution == ShootSimulator.instance.m_simulatingShootSolution )
					Handles.color = Color.cyan;
				else
					Handles.color = Color.green;
			}
			Handles.DrawLine(m_shootCurveKeys[idx], m_shootCurveKeys[idx+1]);
		}

		if( !solution.m_bSuccess )
		{
			int iCurveCnt = solution.m_ShootCurveList.Count;
			if( iCurveCnt == 0 )
				return;
			ShootSolution.SShootCurve curve = solution.m_ShootCurveList[iCurveCnt - 1];
			Vector3 vPosHighest = curve.GetHighestPosition().ToUnity2();

			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.red;
			Handles.Label(vPosHighest, vPosHighest.y.ToString(), style);

			const float dotLineLength = 0.2f;
			List<Vector3> poly = new List<Vector3>();
			while(vPosHighest.y > 0.0f)
			{
				poly.Add(vPosHighest);
				vPosHighest.y -= dotLineLength;
				poly.Add(vPosHighest);
				vPosHighest.y -= dotLineLength;
			}

			Handles.color = Color.red;
			Handles.DrawPolyLine(poly.ToArray());
		}
	}
	
	void _DrawSectors(Vector3 center)
	{
		center.y = 0;
		
		Handles.color = Color.black;
		//draw distance range
		List<IM.Number> angleList = GameSystem.Instance.shootSolutionManager.m_AngleList;
		List<IM.Number> distanceList = GameSystem.Instance.shootSolutionManager.m_DistanceList;
		
		if( angleList.Count == 0 || distanceList.Count == 0 )
			return;
		
		float radius = 9.0f;
		int iCount = GameSystem.Instance.shootSolutionManager.m_AngleList.Count;
		
		Handles.DrawLine(center, center + Vector3.right * radius);
		for( int idx = 0; idx != iCount; idx++ )
		{
			float angle = GameSystem.Instance.shootSolutionManager.m_AngleList[idx].ToUnity2();
			Handles.DrawLine(center, center + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right * radius);
		}
		Handles.DrawLine(center, center - Vector3.right * radius);
		
		iCount = GameSystem.Instance.shootSolutionManager.m_DistanceList.Count;
		for( int idx = 0; idx != iCount; idx++ )
		{
			float distance = GameSystem.Instance.shootSolutionManager.m_DistanceList[idx].ToUnity2();
			Handles.DrawWireArc(center, Vector3.up, Vector3.right, 180.0f, distance);
		}
	}

	void _BuildShootCurves()
	{
		m_shootCurveKeys.Clear();
		
		if( mBall.m_shootSolution != null )
			mBall.m_shootSolution.m_vStartPos = mBall.position;
		
		IM.Number fTime = IM.Number.zero;
		IM.Number fStep = new IM.Number(0, 01);
		IM.Vector3 curPos;

		while( mBall.m_shootSolution.GetPosition(fTime, out curPos) )
		{
			m_shootCurveKeys.Add(curPos.ToUnity2());
			fTime += fStep;
		}
	}
}