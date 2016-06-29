using UnityEngine;
using System;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameScene : PlayerStateMachine.Listener
{
	public PlayGround 	mGround{ get; private set; }
	public UBasket 		mBasket{ get; private set; }

	public List<UBasketball> balls = new List<UBasketball>();
    public List<UBasketball> removeBalls = new List<UBasketball>();
	public UBasketball mBall
	{
		get
		{
			if (balls.Count > 0)
				return balls[0];
			else
				return null;
		}
		private set
		{
			if (balls.Count > 0)
				balls[0] = value;
			else
				balls.Add(value);
		}
	}

	public class Ending
	{
		public UResultPose[] winPoses = new UResultPose[3];
		public UResultPose[] losePoses = new UResultPose[3];

		public Transform winLookAt;
		public Transform loseLookAt;

		public iTweenPath winPath;
		public iTweenPath losePath;
		
		public float time;
	}
	public List<Ending> m_endings { get; private set; }

	public bool loaded = false;
	public delegate void DEBUG_DRAW_DELEGATE();
	public DEBUG_DRAW_DELEGATE	onDebugDraw;

	public Scene mSceneInfo;
	private bool mDebugDraw;

	private uint m_ballIdCounter = 0;

	public interface GameSceneBuildListener
	{
		void OnSceneComplete();
	}
	private List<GameSceneBuildListener> m_listeners = new List<GameSceneBuildListener>();
	public void RegisterListener( GameSceneBuildListener listener )
	{ 
		if( m_listeners.Contains(listener) )
			return;
		m_listeners.Add(listener);
	}
	public void RemoveAllListeners()
	{
		m_listeners.Clear();
	}
	
	public GameScene(Scene sceneInfo)
	{
		this.mSceneInfo = sceneInfo;
		mDebugDraw = true;
		m_endings = new List<Ending>();
	}

	public void OnLevelWasLoaded(int level)
	{
		if( Application.loadedLevelName != mSceneInfo.resourceId )
			return;
	
		Debug.Log("Build scene : " + mSceneInfo.id );
		
		_BuildConcreteContent();
		
		Debug.Log("Scene: " + mSceneInfo.resourceId + " build successfully" );

		loaded = true;
	
		foreach ( GameSceneBuildListener lsn in m_listeners )
			lsn.OnSceneComplete();
	}
	
	protected void _BuildConcreteContent()
	{
		PlayGround ground = new PlayGround( this );
        GameSystem.Instance.gameMatchConfig.LoadPlayGround(ref ground);
		mGround = ground;
		mGround.Build();

		GameObject goBasketStands = GameObject.FindGameObjectWithTag(TagDef.basketStand);
		if( goBasketStands != null )
		{
			mBasket = goBasketStands.GetComponent<UBasket>();
			if( mBasket == null )
				mBasket = goBasketStands.AddComponent<UBasket>();
			mBasket.Build(ground.mBasketPos);
		}

        /*
        if( GameObject.Find("StandardBorder") == null )
        {
            UnityEngine.Object resBorder = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/StandardBorder");
            if( resBorder != null )
                GameObject.Instantiate(resBorder);
        }
        */

        GameObject endings = GameObject.Find("Endings");
		if( endings == null )
		{
            UnityEngine.Object resEndings = ResourceLoadManager.Instance.LoadPrefab("Prefab/Camera/Endings");
			if( resEndings != null )
				endings = GameObject.Instantiate(resEndings) as GameObject;
		}
		if( endings != null )
			m_endings.Add(ParseEnding(endings.transform));

		GameObject goGround = GameObject.FindGameObjectWithTag(TagDef.playground);
		goGround.GetComponent<Renderer>().material.renderQueue = RenderQueue.PlayGround;

		GameObject[] goGroundLines = GameObject.FindGameObjectsWithTag(TagDef.groundline);
		foreach( GameObject goLine in goGroundLines )
			goLine.GetComponent<Renderer>().material.renderQueue = RenderQueue.PlayGroundLine;

		_CreateAudiences();
	}

	Ending ParseEnding(Transform tm)
	{
		Ending ending = new Ending();
		ending.winPoses[0] = _FindResultPose(tm, "WinPos1");
		ending.winPoses[1] = _FindResultPose(tm, "WinPos2");
		ending.winPoses[2] = _FindResultPose(tm, "WinPos3");
		ending.losePoses[0] = _FindResultPose(tm, "LosePos1");
		ending.losePoses[1] = _FindResultPose(tm, "LosePos2");
		ending.losePoses[2] = _FindResultPose(tm, "LosePos3");

		Transform winPath = GameUtils.FindChildRecursive( tm, "WinPath");
		ending.winPath = winPath.GetComponent<iTweenPath>();
		ending.winLookAt = GameUtils.FindChildRecursive( tm, "WinLookAt");

		Transform losePath = GameUtils.FindChildRecursive( tm, "LosePath");
		ending.losePath = losePath.GetComponent<iTweenPath>();
		ending.loseLookAt = GameUtils.FindChildRecursive( tm, "LoseLookAt");
		
		ending.time = tm.GetComponentInChildren<UPathTime>().time;
		return ending;
	}

	void _CreateAudiences()
	{
		GameObject goPoints = GameObject.Find("Points");
		if( goPoints == null )
			return;
		for( int iIdx = 0; iIdx != goPoints.transform.childCount; ++iIdx )
		{
			Transform trChild = goPoints.transform.GetChild(iIdx);
			uint pointId = uint.Parse(trChild.name);
			Audience audience = mSceneInfo.audiences.Find( (Audience aud)=>{return aud.pointId == pointId;} );
			if( audience == null )
				continue;
			GameObject resAud = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/Audience/" + audience.id);
			GameObject goAud = GameObject.Instantiate(resAud) as GameObject;
            for (int i = 0 ; i < goAud.transform.GetChildCount(); ++i)
                goAud.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("points");
            //添加群众脚下的阴影
            GameObject shadow = GameObject.Instantiate(ResourceLoadManager.Instance.LoadPrefab("prefab/effect/shadow")) as GameObject;
            shadow.transform.parent = goAud.transform;
            shadow.transform.localPosition = new Vector3(0.0f, 0.02f, 0.0f);
            goAud.transform.parent = trChild;
			goAud.transform.localPosition = Vector3.zero;
			goAud.transform.localRotation = Quaternion.identity;
		}
	}

	UResultPose _FindResultPose(Transform root, string tag)
	{
		Transform pos = GameUtils.FindChildRecursive( root, tag);
		if( pos == null )
			return null;
		return pos.GetComponent<UResultPose>();
	}

	public UBasketball CreateBall()
	{
		if( mBasket == null )
		{
			Debug.LogError("create basket before creating balls.");
			return null;
		}

        GameObject goBall = GameObject.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/basketBall")) as GameObject;
		UBasketball ball = null;
		if( goBall != null )
		{
			ball = goBall.GetComponent<UBasketball>();
			if( ball == null )
				ball = goBall.AddComponent<UBasketball>();
		}
		ball.onShootGoal += mBasket.OnGoal;
		ball.onRebound += mBasket.OnNoGoal;

		ball.onRimCollision += mBasket.OnRimCollision;
		ball.onDunk += mBasket.OnDunk;
		
        //TODO 如果使用下面注释的这句，在跳球的时候，球的显示位置会不断偏离，然而并没有弄懂是为什么
		//ball.transform.position = -Vector3.up;
        ball.position = -IM.Vector3.up;

		ball.m_loseBallSimulator = new LoseBallSimulator(ball, mGround);

		balls.Add(ball);
		ball.m_id = m_ballIdCounter;
		m_ballIdCounter++;

		return ball;
	}

	public void DestroyBall(UBasketball ball)
	{
		if (ball.m_owner != null)
			ball.m_owner.DropBall(ball);
		//balls.Remove(ball);
        removeBalls.Add(ball);
		UnityEngine.Object.Destroy(ball.gameObject);
	}

	public override void OnDunkLeaveGround (Player player, IM.Number fDunkTime)
	{
		UCamCtrl_MatchNew cam = Camera.main.GetComponent<UCamCtrl_MatchNew>();
		if( cam == null )
			return;
		cam.m_Zoom.SetZoom(mBasket.transform, ZoomType.eMatch);
	}

	public void OnDrawGizmos()
	{
		if( mDebugDraw )
		{
			//shoot solution sector
			//_DrawSectors(GameSystem.Instance.shootSolutionManager.m_AngleList, GameSystem.Instance.shootSolutionManager.m_DistanceList, Color.black);
			//road path

            if (mBasket != null)
            {
                List<float> mAngleList = new List<float>();
                int aNum = RoadPathManager.Instance.m_AngleList.Count;
                for (int iAngleIdx = 0; iAngleIdx < aNum; ++iAngleIdx)
                    mAngleList.Add((float)RoadPathManager.Instance.m_AngleList[iAngleIdx]);

                List<float> mDistList = new List<float>();
                int dNum = RoadPathManager.Instance.m_DistanceList.Count;
                for (int iAngleIdx = 0; iAngleIdx < dNum; ++iAngleIdx)
                    mDistList.Add((float)RoadPathManager.Instance.m_DistanceList[iAngleIdx]);

                GameUtils.DrawSectors((Vector3)mBasket.m_vShootTarget, mAngleList, mDistList, 2, Color.blue);
            }

			if( onDebugDraw != null )
				onDebugDraw();
		}
	}
}
