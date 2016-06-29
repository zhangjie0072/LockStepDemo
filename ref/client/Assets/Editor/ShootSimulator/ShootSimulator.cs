using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(UBasketball))]
public class ShootSimulator : Editor
{
	static public ShootSimulator instance;
	public int m_iCurSector{get; private set;}

	private UBasketball m_ball;
	private UBasket		m_basket;

	public bool		m_simulating = false;
    public bool mirrorClicked = false; 
	public Vector2  m_vAngleAdjustment;
	public float	m_fSpeed;
	public float	m_fBounceBackboard;
	public Vector3	m_vBounceRimAdjustment;

	public ShootSolution m_editingShootSolution;

	private List<Vector3> m_shootSolutionKeys = new List<Vector3>();
	//for simulating
	public ShootSolution m_simulatingShootSolution{get; private set;}

    //animation use

    public ShootSolution.AnimationType type = ShootSolution.AnimationType.ballCircle;
    public float m_playTime = 0f;
    public float m_playSpeed = 0f;
    public float m_reductionIndex = 0f;
    public bool m_isForce = false;
	void OnEnable()
	{
		m_ball = (UBasketball)target;
		GameObject goBasket = GameObject.FindGameObjectWithTag("basketStand");
		if( goBasket == null )
			return;
		m_basket = goBasket.GetComponent<UBasket>();
		if( m_basket == null )
			m_basket = goBasket.AddComponent<UBasket>();
		m_basket.Build(new IM.Vector3(IM.Number.zero, IM.Number.zero, new IM.Number(12,8)));

        m_ball.m_ballRadius = new IM.Number(0, 125);

		m_simulatingShootSolution = new ShootSolution(0);
		GameSystem.Instance.shootSolutionManager = new ShootSolutionManager();
		

		instance = this;
	}

	void OnDisable () { instance = null; }

	void OnSceneGUI() 
	{
		//draw current shoot solution
		int iKeyCount = m_shootSolutionKeys.Count;
		for( int idx = 0; idx != iKeyCount; idx++ )
		{
			if( idx + 1 >= iKeyCount )
				break;
			
			Handles.color = Color.yellow;
			Handles.DrawLine(m_shootSolutionKeys[idx], m_shootSolutionKeys[idx+1]);
		}
	}

	public override void OnInspectorGUI()
	{	
		if( m_basket == null || m_ball == null )
			return;

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Sector: ");
        m_iCurSector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(m_basket.m_rim.center, m_ball.position);
		EditorGUILayout.IntField(m_iCurSector);
		EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        type = (ShootSolution.AnimationType)EditorGUILayout.EnumPopup("Animation type:", type);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Animation play time: ");
        m_playTime = EditorGUILayout.FloatField(m_playTime);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Animation play speed: ");
        m_playSpeed = EditorGUILayout.FloatField(m_playSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Animation Redunction index: ");
        m_reductionIndex = EditorGUILayout.FloatField(m_reductionIndex); 
        EditorGUILayout.EndHorizontal();
		/*
		Vector3 dirBall2BasketH = (mBasket.m_rim.center - mBall.transform.position).normalized;
		dirBall2BasketH.y = 0.0f;
		
		Vector3 dirInitVel = mCurSelectedSolution.m_vInitVel.normalized;
		Vector3 dirInitVelH = dirInitVel;
		dirInitVelH.y = 0.0f;
		float fAlpha = Vector3.Angle(dirInitVelH, dirInitVel);
		float fBeta =  Vector3.Cross(dirInitVelH, dirBall2BasketH).y > 0.0f ? Vector3.Angle(dirInitVelH, dirBall2BasketH) : -Vector3.Angle(dirInitVelH, dirBall2BasketH);
		float fVel_ini = mCurSelectedSolution.m_vInitVel.magnitude;
		*/

		EditorGUILayout.BeginHorizontal();
        //m_simulating = GUILayout.Toggle( m_simulating, "Simulate", GUILayout.ExpandWidth(true) );
        if (GUILayout.Button("Simulate", GUILayout.Height(20f)))
            Dosimulating();
		bool mirrorClicked = GUILayout.Button("Mirror", GUILayout.ExpandWidth(true) );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Shoot angle alpha: ");
		m_vAngleAdjustment.y = EditorGUILayout.Slider(m_vAngleAdjustment.y, 0.0f, 90.0f);
		EditorGUILayout.EndHorizontal();
		
		//path name:
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Shoot angle beta: ");
		m_vAngleAdjustment.x = EditorGUILayout.Slider(m_vAngleAdjustment.x, -180f, 180f);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Shoot vel: ");
		m_fSpeed = EditorGUILayout.Slider(m_fSpeed, 0.0f, 20.0f);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		m_vBounceRimAdjustment = EditorGUILayout.Vector3Field("Bounce rim dir adjustment:", m_vBounceRimAdjustment);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Bounce backboard: ");
		m_fBounceBackboard = EditorGUILayout.Slider(m_fBounceBackboard, 0.0f, 2.0f);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button("Load solution set", GUILayout.ExpandWidth(true)) )
		{
            string path = GlobalConst.DIR_XML_SHOOT_SOLUTION + "shootsolutionset";
			GameSystem.Instance.shootSolutionManager.LoadShootSolutionSet(path, true);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button("Solutions", GUILayout.ExpandWidth(true)) )
		{
			EditorWindow.GetWindow<ShootSolutionEditor>(false, "ShootSolutionEditor", true).Show();
		}
		EditorGUILayout.EndHorizontal();

        //if(m_simulating)
        //{
        //    if (ShootSolutionEditor.instance != null && ShootSolutionEditor.instance.mCurSelectedSolution != null)
        //    {
        //        m_editingShootSolution = ShootSolutionEditor.instance.mCurSelectedSolution;
        //    }
        //    else
        //        m_editingShootSolution = m_simulatingShootSolution;

        //    if (mirrorClicked)
        //    {
        //        m_ball.transform.position = new Vector3(-m_ball.transform.position.x, m_ball.transform.position.y, m_ball.transform.position.z);
        //        m_vAngleAdjustment.x *= -1;
        //        m_editingShootSolution = m_editingShootSolution.Clone();
        //        ShootSolutionEditor.instance.mCurSelectedSolution = m_editingShootSolution;
        //    }
        //    m_iCurSector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(m_basket.m_rim.center, m_ball.transform.position);
        //    ShootSimulation.Instance.Build(m_basket, m_ball);

        //    m_editingShootSolution.m_vBounceRimAdjustment = m_vBounceRimAdjustment;
        //    m_editingShootSolution.m_fBounceBackboard = m_fBounceBackboard;

        //    m_editingShootSolution.m_animationType = type;
        //    m_editingShootSolution.m_playTime = m_playTime;
        //    m_editingShootSolution.m_playSpeed = m_playSpeed;

        //    if (!ShootSimulation.Instance.DoSimulate(m_iCurSector, m_vAngleAdjustment, m_fSpeed, type, ref m_editingShootSolution))
        //        return;
			
        //    m_shootSolutionKeys.Clear();
        //    if( m_editingShootSolution == null )
        //        return;
			
        //    float fTime = 0.0f;
        //    float fStep = 0.01f;
        //    Vector3 curPos;
			
        //    while( m_editingShootSolution.GetPosition(fTime, out curPos) )
        //    {
        //        m_shootSolutionKeys.Add(curPos);
        //        fTime += fStep;
        //    }
			
        //    m_ball.m_shootSolution = m_editingShootSolution;
        //    SceneView.RepaintAll();
        //}
	}
    void Dosimulating()
    {
        if (ShootSolutionEditor.instance != null && ShootSolutionEditor.instance.mCurSelectedSolution != null)
			{
				m_editingShootSolution = ShootSolutionEditor.instance.mCurSelectedSolution;
			}
			else
				m_editingShootSolution = m_simulatingShootSolution;  
            

			if (mirrorClicked)
			{
				m_ball.transform.position = new Vector3(-m_ball.transform.position.x, m_ball.transform.position.y, m_ball.transform.position.z);
				m_vAngleAdjustment.x *= -1;
				m_editingShootSolution = m_editingShootSolution.Clone();
				ShootSolutionEditor.instance.mCurSelectedSolution = m_editingShootSolution;
			}
            m_iCurSector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(m_basket.m_rim.center, m_ball.position);
			ShootSimulation.Instance.Build(m_basket, m_ball);

			m_editingShootSolution.m_vBounceRimAdjustment = IM.Editor.Tools.Convert(m_vBounceRimAdjustment);
			m_editingShootSolution.m_fBounceBackboard = IM.Editor.Tools.Convert(m_fBounceBackboard);

            m_editingShootSolution.m_animationType = type;
            m_editingShootSolution.m_playTime = IM.Editor.Tools.Convert(m_playTime);
            m_editingShootSolution.m_playSpeed = IM.Editor.Tools.Convert(m_playSpeed);
            m_editingShootSolution.m_reductionIndex = IM.Editor.Tools.Convert(m_reductionIndex);

            if (!ShootSimulation.Instance.DoSimulate(m_iCurSector, IM.Editor.Tools.Convert(m_vAngleAdjustment), IM.Editor.Tools.Convert(m_fSpeed), type, IM.Editor.Tools.Convert(m_reductionIndex),ref m_editingShootSolution))
				return;
			
			m_shootSolutionKeys.Clear();
			if( m_editingShootSolution == null )
				return;
			
			float fTime = 0.0f;
			float fStep = 0.01f;
			IM.Vector3 curPos;
			
			while( m_editingShootSolution.GetPosition(IM.Editor.Tools.Convert(fTime), out curPos) )
			{
				m_shootSolutionKeys.Add((Vector3)curPos);
				fTime += fStep;
			}
			
			m_ball.m_shootSolution = m_editingShootSolution;
            SceneView.RepaintAll();
            ShootSolutionEditor.instance._DrawCurves();
    }
}