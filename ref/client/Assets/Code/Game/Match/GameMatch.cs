using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using ProtoBuf;
using fogs.proto.msg;
using fogs.proto.config;
using LuaInterface;


public abstract class GameMatch
	: GameScene.GameSceneBuildListener
{
    public interface Count24Listener
    {
        void OnTimeUp();
    }
    private List<GameMatch.Count24Listener> m_countListers = new List<Count24Listener>();

    public void AddCount24Listener(Count24Listener newLsn)
    { 
        foreach(Count24Listener lsn in m_countListers)
        {
            if (newLsn == lsn)
                return;
        }
        m_countListers.Add(newLsn);
    }

    public void RemoveAllCount24Listener()
    {
        m_countListers.Clear();
    }

    /// <summary>
    /// 增加战斗类型必须对应增加本类的LeagueTypeToSpriteName比赛名称字符数组类型
    /// </summary>
    public enum LeagueType
	{
		eNone			= 0,
		ePractise,
		eReady4AsynPVP,
		eReady4Tour,
		eCareer,
		eTour,
		eAsynPVP,
		eSkillGuide,
		ePVP,
		eQualifying,
        eBullFight,
        eShoot,
		ePracticeLocal,
		eRegular1V1,
		eQualifyingNew,
		eQualifyingNewer,
		eQualifyingNewerAI,
		eLadderAI,
		eMax,
        ePractise1vs1,
    }

	public string LeagueTypeToString()
	{
		string[] leagueType = 
		{
			"",
			"",
			"",
			"",
			"STR_CAREER",
			"STR_STREET_TOUR",
			"",
			"",
            "STR_LADDER_MATCH",
			"STR_LABBER",
			"STR_ONE_ON_ONE",
			"STR_SHOOT",
			"STR_PRACTICE",
            "STR_REGULAR_MATCH",
            "STR_LABBER",
            "STR_LABBER",
			"",
            "STR_PRACTISE1VS1",
        };
		return CommonFunction.GetConstString(leagueType[(int)m_config.leagueType]);

	}
	public string LeagueTypeToSpriteName()
	{
		string[] leagueType = 
		{
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t140",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t150",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t144",
			"com_tencent_t143",
			"com_tencent_t144",
            "com_tencent_t148",
			"com_tencent_t144",
            "com_tencent_t144",
            "com_tencent_t150",
            "com_tencent_t143",
            "com_tencent_t143"
        };
        int aa = (int)m_config.leagueType;
        return CommonFunction.GetConstString(leagueType[(int)m_config.leagueType]);
	}


	//?œQ????
    public enum Type
    {
        eNone = 0,
        ePractise,
        eReady,
        e1On1,
        e3On3,
        eCareer1On1,
        eCareer3On3,
        eAsynPVP3On3,
		e3AIOn3AI,
		ePVP_1PLUS = 10,
		ePVP_3On3,
		eGuide,
        eFreePractice,
        ePracticeVs,

        eReboundStorm = 100,
        eBlockStorm,
        eUltimate21,
        eMassBall,
        eGrabZone,
        eGrabPoint,
		eBullFight,
        ePractice1V1,
        eQualifyingNewerAI,
        eLadderAI,
    }

	public enum MatchRole
	{
		eNone,
		eDefense,
		eOffense
	}

	public enum Level
	{
		None,
		Easy,
		Normal,
		Hard,
	}

	public class Config
    {
        public Type type;
		public LeagueType leagueType;
		public Level level;
        public bool needPlayPlot;
        public ulong session_id;
		public uint	sceneId;
		public uint gameModeID;
        public uint extra_info;

		public string 	ip;
		public int		port;

        public class TeamMember
        {
            public Team.Side team;
            public string id;
            public int pos;
            public string team_name;
			public uint AIID;
			public RoleInfo roleInfo;
            public bool isRobot;
            public List<EquipInfo> equipInfo;
            public List<FightRole> squadInfo;
            public List<uint> mapInfo;
            public List<List<uint>> fashionAttrInfo;
            public uint mode_type_id;
			public bool bIsNPC = false;
            public BadgeBook badgeBook;
        }

        public List<TeamMember> NPCs = new List<TeamMember>();
        public TeamMember MainRole;
        public List<TeamMember> RemotePlayers = new List<TeamMember>();

        public IM.Number MatchTime;
        public string Scene;

        public IM.Number OppoColorMulti = new IM.Number(0, 200);

        public Config()
        {
        }
    }

	public LeagueType leagueType { get { return m_config.leagueType; } }
	
	public Team	m_homeTeam;
	public Team	m_awayTeam;

    /**是否加时*/
	public bool m_bOverTime = false;
    /**比赛总时间*/
    public IM.Number gameMatchTime = IM.Number.zero;
    /**比赛倒计时timer*/
    public GameUtils.Timer m_gameMathCountTimer{ get; protected set; }
    /**比赛倒计时是否停止(暂定)*/
    public bool m_gameMatchCountStop = false;
    /**比赛倒计时间功能开启enable*/
    public bool m_gameMathCountEnable = true;
    /**进攻24秒常量*/
    public IM.Number MAX_COUNT24_TIME = new IM.Number(14);
    private IM.Number _count24TimeSec;
    /**进攻24秒*/
    public IM.Number m_count24Time {
        get { return _count24TimeSec; } 
        set {
            if (value > MAX_COUNT24_TIME)
                _count24TimeSec = MAX_COUNT24_TIME;
            else
                _count24TimeSec = value;
            if (m_uiMatch != null)
                m_uiMatch.mCounter24.UpdateTime((float)_count24TimeSec, m_count24TimeStop,true);
        }
    }
    /**进攻24秒倒计时暂停*/
    public bool m_count24TimeStop = true;

    /**进攻队伍*/
	public Team	m_offenseTeam
	{ 
		get{ 
			if( m_homeTeam.m_role == MatchRole.eNone && m_awayTeam.m_role == MatchRole.eNone )
				return null;
			return m_homeTeam.m_role == MatchRole.eOffense ? m_homeTeam : m_awayTeam;}
		set{}
	}
    /**防守队伍*/
	public Team	m_defenseTeam
	{ 
		get{
			if( m_homeTeam.m_role == MatchRole.eNone && m_awayTeam.m_role == MatchRole.eNone )
				return null;
			return m_homeTeam.m_role == MatchRole.eDefense ? m_homeTeam : m_awayTeam;}
		set{}
	}
    /**强队*/
	public Team m_strongTeam
	{
		get { return m_homeTeam.fightingCapacity >= m_awayTeam.fightingCapacity ? m_homeTeam : m_awayTeam; }
	}
    /**弱队*/
	public Team m_weakTeam
	{
		get { return m_homeTeam.fightingCapacity < m_awayTeam.fightingCapacity ? m_homeTeam : m_awayTeam; }
	}
    /**强队得分*/
	public int m_strongTeamScore
	{
		get { return m_homeTeam == m_strongTeam ? m_homeScore : m_awayScore; }
	}
    /**弱队得分*/
	public int m_weakTeamScore
	{
		get { return m_homeTeam == m_weakTeam ? m_homeScore : m_awayScore; }
	}
    /**当前场景*/
	public GameScene mCurScene{ get; protected set; }

	public Player m_mainRole
	{
		get { return _mainRole; }
		protected set
		{
			bool isPassTarget = false;
			if (_mainRole != null)
			{
				_mainRole.HideIndicator();
				if ( _mainRole.m_StateMachine != null && _mainRole.m_StateMachine.m_curState != null && value != null &&
					_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.ePass &&
					_mainRole.m_passTarget == value &&
					!value.m_bWithBall)
					isPassTarget = true;
			}
			_mainRole = value;
			if (isPassTarget)
			{
				_mainRole.ShowIndicator(Color.yellow, false);
				UBasketball.BallDelegate onCatch = null;
				onCatch = (UBasketball ball) =>
				{
					ball.onCatch -= onCatch;
                    _mainRole.ShowIndicator(Color.yellow, true);
				};
				mCurScene.mBall.onCatch += onCatch;
			}
			else
			{
                _mainRole.ShowIndicator(Color.yellow, true);
			}
		}
	}
	
	public ReboundHelper reboundHelper = new ReboundHelper();

	public GameRuler			m_ruler{ get; protected set; }
	public UCamCtrl_MatchNew	m_cam{ get; protected set; }
	public UCamCtrl_FollowPath 	m_camFollowPath{ get; protected set; }

	public Vector3 m_origCameraForward{ get; protected set; }

	public int m_awayScore
	{
		get { return _awayScore; }
		set { int delta = value - _awayScore; _awayScore = value; OnScoreChanged(false, delta); }
	}
	public int m_homeScore
	{
		get { return _homeScore; }
		set { int delta = value - _homeScore; _homeScore = value; OnScoreChanged(true, delta); }
	}

	public MatchStateMachine	m_stateMachine{ get; protected set; }
	public UIMatch		m_uiMatch;
	public UIPanel 		m_uiInGamePanel;
	public UController 			m_uiController;
	public BackgroundSoundPlayer	m_bgSoundPlayer;
	public bool	m_bTimeUp = false;
	public bool	m_b24TimeUp = false;
	public Config			m_config;
	public GameMode gameMode { get; private set; }
	public Level level { get { return gameMode != null ? gameMode.level : m_config.level; } }
	public Dictionary<string, AttrReduceConfig.AttrReduceItem> attrReduceItems;
	public bool neverWin = false;
	public List<uint>	m_auxiliaries = new List<uint>();
	public Dictionary<GameObject, GameObject> m_preloadCache = new Dictionary<GameObject, GameObject>();
	public bool	m_needTipOff = false;

	public GameMatchContext m_context{get; private set;}
    public TurnManager turnManager { get; private set; }
    public int curGameUpdateID { get; private set; }

	protected UnityEngine.Object m_resTips;
	protected ArrayList	m_goTips = new ArrayList();
	protected Team 	m_mainTeam;
	protected bool	m_bShowGoalUIEffect = true;

	private GameObject	m_preloadCamera;
	private	uint		m_roomId = 0;
	private	UnityEngine.Object	m_shadowEffect;
	private GameObject m_tipPanel;
	private UIAtlas m_atlas;
	private GameObject matchAchievementItemPrefab;
	private GameObject m_basketTipNode;
	private bool m_outscored = false;
	
	private Player _mainRole;
	private int _awayScore = 0;
	private int _homeScore = 0;

	private Dictionary<Player, Dictionary<PlayerStatistics.StatType, uint>> completedAchievement = new Dictionary<Player, Dictionary<PlayerStatistics.StatType, uint>>();
	private Dictionary<PlayerStatistics.StatType, uint> highestAchievement = new Dictionary<PlayerStatistics.StatType, uint>();

	public GameMatch(Config config)
	{
		m_stateMachine = CreateMatchStateMachine();
		 
		m_homeTeam = new Team(Team.Side.eHome);
		m_awayTeam = new Team(Team.Side.eAway);
	
		m_config = config;
		gameMode = GameSystem.Instance.GameModeConfig.GetGameMode(config.gameModeID);
		uint sectionID = 0u;
		if (leagueType == LeagueType.eCareer)
		{
			sectionID = (uint)(double)LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionID"];
		}
		else if (leagueType == LeagueType.eTour)
		{
			sectionID = MainPlayer.Instance.CurTourID;
		}
		else if (leagueType == LeagueType.eAsynPVP)
		{
			sectionID = MainPlayer.Instance.WinningStreak;
		}
		if (MainPlayer.Instance.Captain != null)
			attrReduceItems = GameSystem.Instance.AttrReduceConfig.GetItems(leagueType, sectionID, MainPlayer.Instance.Level, MainPlayer.Instance.Captain.m_position);

		m_bOverTime = false;
		m_needTipOff = false;

		m_context = new GameMatchContext();

		_CreatePlayersData();

        VirtualGameServer.Instance.Reset();

        turnManager = new TurnManager();

        TurnController.Instance.match = this;
	}
	
	static public void HandleBroadcast(Pack pack)
	{
		InstructionBroadcast resp = Serializer.Deserialize<InstructionBroadcast>(new MemoryStream(pack.buffer));
		MatchMsg msg = GameSystem.Instance.matchMsgConfig.matchMsgs.Find( (MatchMsg inMsg)=>{return inMsg.id == resp.id;} );
		if( msg == null )
		{
			Logger.LogError("Unable to find msg.");
			return;
		}
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByRoomId(resp.char_id);
		if( curMatch.m_uiMatch != null )
		{
			if( player.m_team.m_side == Team.Side.eHome )
				curMatch.m_uiMatch.m_msgHomeBlobs.AddNewBlob(msg, true);
			else 
				curMatch.m_uiMatch.m_msgAwayBlobs.AddNewBlob(msg, false);
		}
		
		//play sound
		AudioClip clip = AudioManager.Instance.GetClip("MatchMsg/" + msg.audio_src);
		if (clip != null)
			AudioManager.Instance.PlaySound(clip);
		else
			Logger.LogWarning("Sound file " + msg.audio_src + " not found");
	}		

	protected virtual MatchStateMachine CreateMatchStateMachine()
	{
		return new MatchStateMachine(this);
	}

	public virtual void CreateUI()
	{
        _CreateGUI();
	}

    public Type GetMatchType()
    {
        return m_config.type;
    }

	virtual public void OnDestroy()
	{
        VirtualGameServer.Instance.Stop();
	}

    public Config GetConfig()
    {
        return m_config;
    }
	
    /**游戏场景构建*/
	public void Build()
	{
		Scene sceneInfo = GameSystem.Instance.SceneConfig.GetConfig( m_config.sceneId );
		if( sceneInfo == null )
		{
			Logger.LogError("No scene config: " + m_config.sceneId);
			return;
		}
		
		mCurScene = new GameScene( sceneInfo );
		mCurScene.RegisterListener(this);
		
		m_ruler = new GameRuler(this);
		
        //string path = GlobalConst.DIR_XML_SHOOT_SOLUTION + "shootsolutionset";
		if( !GameSystem.Instance.shootSolutionManager.LoadShootSolutionSet(false) )
			Logger.LogError("Failed to load shoot solution set.");
		
		mCurScene.onDebugDraw += RoadPathManager.Instance.OnDebugDraw;
	}
    /**战斗场景加载完成*/
	virtual public void OnSceneComplete ()
	{
		m_shadowEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/shadow");
		if( m_config == null )
		{
			Logger.LogError("Match config file loading failed.");
			return;
		}

		PlaySoundManager.ClosePlaySound();
		m_bgSoundPlayer = new BackgroundSoundPlayer();
		foreach( string trackSrc in mCurScene.mSceneInfo.bgSound.trackSrc )
		{
			BackgroundSoundPlayer.Track track = new BackgroundSoundPlayer.Track();
			if( trackSrc.Contains("Bg") )
				track.name = trackSrc;
			else
				track.name = "Bg/" + trackSrc;
			track.interval = mCurScene.mSceneInfo.bgSound.interval;
			m_bgSoundPlayer.m_tracks.Add(track);
		}

		if( m_bgSoundPlayer != null )
			m_bgSoundPlayer.Play( BackgroundSoundPlayer.PlaySequence.eNormal );

		IM.Number secDistance = new IM.Number(1,180);
		IM.Number angle3pt = mCurScene.mGround.m3PointMaxAngle * 2;
		RoadPathManager.Instance.BuildSectors(mCurScene.mBasket.m_vShootTarget, angle3pt, 2, 9, secDistance * new IM.Number(8), secDistance);
		Debugger.Instance.Reset();

		mCurScene.mBasket.onGoal += OnGoal;
		mCurScene.mBasket.onRimCollision += OnRimCollision;

		GameObject goMatchCamera = GameObject.Find("MatchCamera");
		if( goMatchCamera == null )
		{
			GameObject resCamera = ResourceLoadManager.Instance.LoadPrefab("Prefab/Camera/MatchCamera");
			goMatchCamera = GameObject.Instantiate(resCamera) as GameObject;
		}
		
		m_cam = goMatchCamera.GetComponent<UCamCtrl_MatchNew>();
		if (m_cam == null)
			m_cam = goMatchCamera.AddComponent<UCamCtrl_MatchNew>();

		m_camFollowPath = goMatchCamera.GetComponent<UCamCtrl_FollowPath>();
		if (m_camFollowPath == null)
			m_camFollowPath = goMatchCamera.AddComponent<UCamCtrl_FollowPath>();

        /*
		m_preloadCamera = new GameObject("Preload Camera");
		Camera cam = m_preloadCamera.AddComponent<Camera>();
		cam.targetTexture = new RenderTexture(32,32,1);
		cam.cullingMask = LayerMask.NameToLayer("preload");
		*/

        m_resTips = ResourceLoadManager.Instance.LoadPrefab("prefab/gui/UIMatchTips");
        List<string> UiNames = new List<string>();
        UiNames.Add("Prefab/GUI/MatchTipAnim");
        UiNames.Add("Prefab/GUI/MatchTipScoreDiff");
        UiNames.Add("Prefab/GUI/PlayerTip");
        UiNames.Add("Prefab/GUI/circle");
        UiNames.Add("Prefab/GUI/GroundDown");
        UiNames.Add("Prefab/GUI/Hit_1");
        UiNames.Add("Prefab/GUI/pre_3pt");
        UiNames.Add("Prefab/GUI/RebPlacement");

       PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		foreach( Player player in pm )
			_CreateTeamMember(player);

        UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
        goLoading.LoadResources(UiNames, pm);

		GameObject inGameInfoPanel = new GameObject("inGameInfoPanel");
		m_uiInGamePanel = inGameInfoPanel.AddComponent<UIPanel>();
		inGameInfoPanel.AddComponent<UIManagedPanel>();
		m_uiInGamePanel.transform.parent = UIManager.Instance.m_uiRootBasePanel.transform;
		m_uiInGamePanel.transform.localPosition = Vector3.zero;
		m_uiInGamePanel.transform.localScale = Vector3.one;
		GameUtils.SetLayerRecursive(inGameInfoPanel.transform, LayerMask.NameToLayer("GUI"));
		goLoading.onComplete += OnLoadingComplete;

        /*string prefabName = "Prefab/GUI/MatchTipAnim";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "Prefab/GUI/MatchTipScoreDiff";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "Prefab/GUI/PlayerTip";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "prefab/indicator/circle";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "prefab/effect/GroundDown";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "prefab/effect/Hit_1";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
        prefabName = "Prefab/indicator/pre_3pt";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);
		prefabName = "Prefab/indicator/RebPlacement";
		ResourceLoadManager.Instance.LoadPrefab(prefabName);*/
    }

	virtual protected void OnLoadingComplete()
	{
	}


	virtual protected void OnRimCollision(UBasket basket, UBasketball ball)
	{
	}

	protected void _CreateGUI()
	{
		//UIControl
        if (m_mainRole != null)
        {
            CreateUIController();
        }

		CreateCustomGUI();

		m_tipPanel = NGUITools.AddChild<UIPanel>(GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel).gameObject;
		m_tipPanel.AddComponent<UIManagedPanel>();
		NGUITools.BringForward(m_tipPanel);

        m_atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/Match/MatchUI");

		matchAchievementItemPrefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/MatchAchievementItem") as GameObject;
	}

	protected virtual void CreateCustomGUI()
	{
        if (m_uiMatch != null) return;
		GameObject uiMatch = GameSystem.Instance.mClient.mUIManager.CreateUI("UIMatch");  
		if (uiMatch != null)
		{
			m_uiMatch = uiMatch.GetComponentInChildren<UIMatch>();
            gameMatchTime = m_config.MatchTime;
            m_uiMatch.SetTimeBoardVisible(m_gameMathCountEnable);
            m_uiMatch.UpdateTime((float)gameMatchTime);
			m_uiMatch.spritePrefix = "gameInterface_figure_black";
			m_uiMatch.digitCount = 2;
			m_uiMatch.leftScoreBoard.gap = 8;
            m_uiMatch.rightScoreBoard.gap = 8;
			m_uiMatch.leftScoreBoard.Initialize();
			m_uiMatch.rightScoreBoard.Initialize();

			m_uiMatch.timerBoard.isChronograph = true;
			m_uiMatch.enableBack = !neverWin;
            m_count24Time = MAX_COUNT24_TIME;
			if (leagueType == LeagueType.eRegular1V1 || leagueType == LeagueType.eQualifyingNew || leagueType == LeagueType.ePVP || leagueType == LeagueType.eQualifyingNewerAI
                || leagueType == LeagueType.eLadderAI)
				m_uiMatch.enableBack = false;

            if (m_config.type == GameMatch.Type.eCareer1On1)//创建比赛类型不对应该是ePractice1V1
            {
                if (GuideSystem.Instance.curModule != null)
                {
                    m_uiMatch.enableBack = false;
                }
            }
            MatchStatePlaying playState = m_stateMachine.GetState(MatchState.State.ePlaying) as MatchStatePlaying;
            if (m_gameMathCountEnable)
            {
                if (m_gameMathCountTimer == null)
                {
                    m_gameMathCountTimer = new GameUtils.Timer(gameMatchTime, playState.OnMatchTimeUp);
                }
                else
                {
                    m_gameMathCountTimer.SetTimer(gameMatchTime);
                }
            }
            AddCount24Listener(playState);
		}
		else
			Logger.LogError("create match UI failed.");
	}

    protected virtual void CreateUIController()
    {
        if (m_uiController != null)
        {
            m_uiController.visible = true;
            return;
        }
        GameObject uiController = GameSystem.Instance.mClient.mUIManager.CreateUI("UIController");
        if (uiController != null)
            GameSystem.Instance.mClient.mInputManager.mJoystick = uiController.GetComponentInChildren<UIJoystick>();
        else
            Logger.Log("Error -- create UI controller failed.");

        m_uiController = uiController.GetComponentInChildren<UController>();
        InputReader.Instance.player = m_mainRole;
        //m_uiController.onAutoDefenseChanged = (state) => AutoDefTakeOver.Enabled = state;
    }

    public abstract void HandleGameBegin(Pack pack);
	
    public virtual void FixedUpdate()
    {
		if( mCurScene == null || mCurScene.mGround == null ) 
			return;

		if (m_uiMatch != null)
            PlayTimeSound((float)gameMatchTime, (float)m_count24Time);
    }

    protected void PlayTimeSound(float matchTime, float attackTime)
    {
        if ((int)matchTime <= 0f && matchTime != float.PositiveInfinity)
        {
            if ((int)matchTime < (int)(matchTime + Time.fixedDeltaTime))
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Last0Sec);
        }
        else if ((int)matchTime <= 10f && matchTime != float.PositiveInfinity)
        {
            if ((int)matchTime < (int)(matchTime + Time.fixedDeltaTime))
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Last3Sec);
        }
        else if ((int)attackTime <= 3f && (int)attackTime > 0)
        {
            if ((int)attackTime < (int)(attackTime + Time.fixedDeltaTime))
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Foul3Sec);
        }
        else if ((int)attackTime <= 0)
        {
            if ((int)attackTime < (int)(attackTime + Time.fixedDeltaTime))
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Foul);
        }
    }

	protected void _RefreshAOD()
	{
		MatchState curState = m_stateMachine.m_curState;
		if( curState != null && curState.m_eState == MatchState.State.ePlaying)
		{
			foreach(Player player in m_mainRole.m_defenseTarget.m_team)
				player.m_AOD.visible = false;
			
			Player target = m_mainRole.m_defenseTarget;
			if( m_mainRole != null && target != null && mCurScene.mBall != null && mCurScene.mBall.m_owner != null)
			{
				if (target != null && target.m_AOD != null)
					target.m_AOD.visible = (m_mainRole.m_team.m_role == GameMatch.MatchRole.eDefense);
			}
		}
	}

    //逻辑帧
	virtual public void Update(IM.Number deltaTime)
    {
        if (mCurScene == null)
            return;
        if (m_ruler != null && m_ruler.timer != null)
        {
            m_ruler.timer.Update(deltaTime);
        }
        //比赛倒计时
        bool needCount = true;
        if (m_bOverTime && m_uiMatch != null && m_gameMathCountEnable)
        {
            m_gameMathCountEnable = false;
            m_uiMatch.SetTimeBoardVisible(false);
            needCount = false;
        }

        if (needCount && m_gameMathCountTimer != null && !m_gameMatchCountStop && m_gameMathCountEnable)
        {
            m_gameMathCountTimer.Update(deltaTime);
            if (m_uiMatch != null)
            {
                m_uiMatch.UpdateTime((float)m_gameMathCountTimer.Remaining());
            }
        }

        //进攻倒计时
        if (!m_count24TimeStop && m_uiMatch != null && EnableCounter24())
        {
            _count24TimeSec -= deltaTime;
            if (m_count24Time > IM.Number.zero)
            {
                m_uiMatch.mCounter24.UpdateTime((float)m_count24Time,false,false);
            }

            if (m_count24Time <= IM.Number.zero)
            {
                m_count24TimeStop = true;
                m_countListers.ForEach(delegate(Count24Listener lsn)
                {
                    lsn.OnTimeUp();
                });
            }
        }

        //比赛状态机
        if (m_stateMachine == null)
            return;
        m_stateMachine.Update(deltaTime);

        //球员
		MatchState curState = m_stateMachine.m_curState;
		if( curState != null 
		   && curState.m_eState != MatchState.State.eOpening
		   && curState.m_eState != MatchState.State.eOverTime
		   )
		{
			PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
			if (pm != null)
			{
				foreach (Player pl in pm)
					pl.Update(deltaTime);
			}
		}

        //球
        foreach (UBasketball ball in mCurScene.balls)
        {
            ball.Update(deltaTime);
        }

        //抢篮板队列
		reboundHelper.Update(deltaTime);
        /*
        //测试功能键
		if( GameSystem.Instance.mClient.mInputManager.m_CmdBtnTestClick )
			_OnTestKeyDown();
        */
    }

    //渲染帧
	virtual public void Update()
    {
        //背景音乐
        if (m_bgSoundPlayer != null)
            m_bgSoundPlayer.Update();

        //篮筐提示信息位置刷新
		UpdateBasketTip();

        //显示比分
		if (m_uiMatch != null)
		{
			m_uiMatch.leftScore	 = m_mainRole.m_team.m_side == Team.Side.eHome ? m_homeScore : m_awayScore;
			m_uiMatch.rightScore = m_mainRole.m_team.m_side == Team.Side.eHome ? m_awayScore : m_homeScore;
		}

        //比赛状态机
        if (m_stateMachine == null)
            return;
        m_stateMachine.Update(Time.deltaTime);

        //玩家
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        if (pm != null)
        {
            foreach (Player pl in pm)
                pl.Update();
        }
    }

    //逻辑层
    virtual public void LateUpdate(IM.Number deltaTime)
    {
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        if (pm != null)
        {
            foreach (Player player in pm)
            {
                player.LateUpdate(deltaTime);

                IM.Vector3 vPos = player.moveCtrl.position;
                mCurScene.mGround.BoundInZone(ref vPos);
                player.moveCtrl.position = vPos;
            }
        }

        ++curGameUpdateID;
    }

    //渲染层
    virtual public void LateUpdate()
    {
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        if (pm != null)
        {
            foreach (Player pl in pm)
                pl.LateUpdate();
        }
    }

	virtual protected void _OnTestKeyDown()
	{
	}

    public void OnPostRender()
    {
    }

	public void ShowMatchTip(string tipSprite, bool hasBG)
	{
		if (!EnableMatchTips())
			return;

		string prefabName = "Prefab/GUI/MatchTipAnim";
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab(prefabName) as GameObject;
		GameObject tip = CommonFunction.InstantiateObject(prefab, m_uiMatch.timerBoard.transform);
		tip.transform.localPosition = new Vector3(0f, -72f, 0f);
		tip.GetComponent<UISprite>().spriteName = tipSprite;
		tip.GetComponent<UISprite>().MakePixelPerfect();
		tip.GetComponent<UITweener>().AddOnFinished(() => { NGUITools.Destroy(tip); });
		NGUITools.SetActive(tip.transform.GetChild(0).gameObject, hasBG);
	}

	public void ShowMatchTipScoreDiff(int scoreDiff)
	{
		if (!EnableMatchTips())
			return;

		bool outscore = scoreDiff > 0;
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/MatchTipScoreDiff") as GameObject;
		GameObject tip = CommonFunction.InstantiateObject(prefab, m_uiMatch.timerBoard.transform);
		tip.transform.localPosition = new Vector3(0f, -72f, 0f);
		GameObject table = tip.transform.GetChild(0).gameObject;
		UISprite sprite = NGUITools.AddSprite(table, m_atlas, outscore ? "gameInterface_tip_lead" : "gameInterface_tip_behind");
		sprite.name = "0";
		sprite.MakePixelPerfect();
		int score = Mathf.Abs(scoreDiff);
		do
		{
			sprite = NGUITools.AddSprite(table, m_atlas, (outscore ? "gameInterface_tip_figure_lu_" : "gameInterface_tip_figure_") + score % 10);
			sprite.name = score.ToString();
			sprite.MakePixelPerfect();
			score = score / 10;
		} while (score != 0);
		sprite = NGUITools.AddSprite(table, m_atlas, outscore ? "gameInterface_tip_point_lu" : "gameInterface_tip_point");
		sprite.name = "z";
		sprite.MakePixelPerfect();
		table.GetComponent<UITable>().Reposition();
		tip.GetComponent<UITweener>().AddOnFinished(() => { NGUITools.Destroy(tip); });
	}
    public void ShowMatchPromptTip(bool isScore, string str = "", int scoreDiff = 0)
    {
        GameObject prompt = m_uiMatch.transform.FindChild("Prompt").gameObject;
        if (isScore)
        {    
            NGUITools.SetActive(prompt, true);
            if (scoreDiff > 0)
                prompt.transform.GetComponent<UILabel>().text = string.Format(CommonFunction.GetConstString("STR_FIELD_PROMPT80"), scoreDiff);
           
            else
                prompt.transform.GetComponent<UILabel>().text = string.Format(CommonFunction.GetConstString("STR_FIELD_PROMPT81"), -scoreDiff);           
        }
        else
        {
            NGUITools.SetActive(prompt, true);
            prompt.transform.GetComponent<UILabel>().text = CommonFunction.GetConstString(str);
        }
        //prompt.GetComponent<UITweener>().SetOnFinished(() => { NGUITools.SetActive(prompt, false); });
        TweenPosition tween = TweenPosition.Begin(prompt, 2, prompt.transform.localPosition);
        tween.SetOnFinished(() => { NGUITools.SetActive(prompt, false); }); 

    }

	public void ShowPlayerTip(Player player, string tipSprite)
	{
		if (!EnablePlayerTips())
			return;

		if (player != m_mainRole)
			return;

		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PlayerTip") as GameObject;
		GameObject tip = CommonFunction.InstantiateObject(prefab, player.m_InfoVisualizer.m_goPlayerInfo.transform);
		tip.GetComponent<UISprite>().spriteName = tipSprite;
		tip.GetComponent<UISprite>().MakePixelPerfect();
		tip.GetComponent<TweenAlpha>().AddOnFinished(() => { NGUITools.Destroy(tip); });
	}

	public void ShowBasketTip(Player player, string tipSprite)
	{
		if (!EnablePlayerTips())
			return;

		if (player != m_mainRole)
			return;

		m_basketTipNode = NGUITools.AddChild(m_tipPanel);
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PlayerTip") as GameObject;
		GameObject tip = CommonFunction.InstantiateObject(prefab, m_basketTipNode.transform);
		tip.GetComponent<UISprite>().spriteName = tipSprite;
		tip.GetComponent<UISprite>().MakePixelPerfect();
		tip.GetComponent<TweenAlpha>().AddOnFinished(() => { NGUITools.Destroy(m_basketTipNode); m_basketTipNode = null; });

		UpdateBasketTip();
	}

	void UpdateBasketTip()
	{
		if (m_basketTipNode == null) return;

		Vector3 viewPos = Camera.main.WorldToViewportPoint(mCurScene.mBasket.transform.position);
		Vector3 worldPos = UIManager.Instance.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewPos);
		m_basketTipNode.transform.position = worldPos;
		Vector3 pos = m_basketTipNode.transform.localPosition;
		pos.x = Mathf.FloorToInt(pos.x);
		pos.y = Mathf.FloorToInt(pos.y);
		pos.z = 2.0f;
		m_basketTipNode.transform.localPosition = pos;
	}

    public void ShowTips(Vector3 worldPos, string tips, Color color)
    {
		return;

		if (!EnablePlayerTips())
			return;

        if (m_resTips == null)
            m_resTips = ResourceLoadManager.Instance.LoadPrefab("Prefab/gui/UIMatchTips");

        if (GameObject.Find(tips))
            return;

        GameObject goTips = GameObject.Instantiate(m_resTips) as GameObject;
        if (goTips == null)
            return;
        goTips.name = tips;

        UICurveAnimator curve = goTips.GetComponentInChildren<UICurveAnimator>();
        if (curve != null)
            curve.mRoot = goTips;

        if (goTips.transform.parent == null)
        {
            goTips.transform.parent = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform;
            goTips.transform.localPosition = Vector3.zero;
            goTips.transform.localScale = Vector3.one;
        }
        UIManager uiMgr = GameSystem.Instance.mClient.mUIManager;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        goTips.transform.position = uiMgr.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewPos);

        Vector3 pos = goTips.transform.localPosition;
        pos.x = Mathf.FloorToInt(pos.x);
        pos.y = Mathf.FloorToInt(pos.y);
        pos.z = 2.0f;
        goTips.transform.localPosition = pos;

        UILabel label = goTips.GetComponentInChildren<UILabel>();
        label.text = tips;
        label.color = color;
    }

    public virtual bool TimmingOnStarting()
    {
        return true;
    }

	public virtual void DoGoalState()
	{
		m_stateMachine.SetState(MatchState.State.eGoal);
	}

    public virtual bool EnableGoalState()
    {
        return m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying;
    }

    public virtual bool EnableNPCGoalSound()
    {
        return true;
    }

	public virtual bool EnableCounter24()
	{
		return true;
	}

	public virtual bool EnableSwitchRole()
	{
		return true;
	}

	public virtual bool EnableCheckBall()
	{
		return true;
	}

	public virtual bool EnableEnhanceAttr()
	{
		return false;
	}

	public virtual bool EnableTakeOver()
	{
		return false;
	}

	public virtual bool EnablePlayerTips()
	{
		return false;
	}

	public virtual bool EnableMatchTips()
	{
		return false;
	}

	public virtual bool EnableMatchAchievement()
	{
		return false;
	}

	public virtual bool EnableSwitchDefenseTarget()
	{
		return false;
	}

    public virtual void ResetPlayerPos()
    {
        BeginPos beginPos = GameSystem.Instance.MatchPointsConfig.BeginPos;
		int idx = 0;
		if( m_homeTeam.m_role == GameMatch.MatchRole.eDefense )
		{
			foreach (Player player in m_homeTeam)
			{
                player.position = beginPos.defenses_transform[idx].position;
                if (player.m_defenseTarget != null)
                    player.m_defenseTarget.position = beginPos.offenses_transform[idx].position;

				idx++;
			}
		}
		else
		{
			foreach (Player player in m_homeTeam)
			{
                player.position = beginPos.offenses_transform[idx].position;
                if (player.m_defenseTarget != null)
                    player.m_defenseTarget.position = beginPos.defenses_transform[idx].position;

				idx++;
			}
		}

        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            if (player.m_defenseTarget == null)
            {
                player.forward = IM.Vector3.forward;
                continue;
            }
            player.FaceTo(player.m_defenseTarget.position);
        }
		//m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand);
    }

    public virtual void ConstrainMovementOnBegin(IM.Number fCurTime)
    {
        m_ruler.ConstrainMovementOnBegin(fCurTime);
    }

    public virtual bool IsCommandValid(Command command)
    {
		if (m_stateMachine.m_curState != null)
			return m_stateMachine.m_curState.IsCommandValid(command);
		else
			return true;
    }

	public virtual bool IsFinalTime(IM.Number seconds)
	{
        return gameMatchTime < seconds || m_count24Time < seconds;
	}

    /**调整投篮命中率*/
	public virtual IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
		if (neverWin && shooter.m_team == m_mainRole.m_team)
		{
			Area area = mCurScene.mGround.GetArea(shooter);
			int score = GetScore(area == Area.eFar ? 3 : 2);
			int myScore = m_mainRole.m_team == m_homeTeam ? m_homeScore : m_awayScore;
			int rivalScore = m_mainRole.m_team == m_homeTeam ? m_awayScore : m_homeScore;
            if (myScore + score > rivalScore)
                return IM.Number.zero;
		}
		return rate;
	}
    
    /**调整盖帽命中率*/
	public virtual IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
		return rate;
	}

    /**调整过人命中率*/
	public virtual IM.Number AdjustCrossRate(Player crosser, Player defender, IM.Number rate)
	{
		return rate;
	}

    /**获取投篮曲线*/
    public virtual ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.BigNumber rate, ShootSolution.Type type)
	{
		string attrName = "";
		switch(area)
		{
			case Area.eFar:
				attrName = "shoot_far";
				break;
			case Area.eMiddle:
				attrName = (type == ShootSolution.Type.Shoot) ? "shoot_middle" : "layup_middle";
				break;
			case Area.eNear:
				attrName = (type == ShootSolution.Type.Shoot) ? "shoot_near" : "layup_near";
				break;
		}
        //TODO
        //float fCleanShotRate = 0.35f + shooter.m_finalAttrs[attrName] * 0.00291f;
        IM.BigNumber fCleanShotRate = new IM.Number(0,350) + shooter.m_finalAttrs[attrName] * new IM.BigNumber(0,002910);

		IM.BigNumber fRam = IM.Random.bigValue;
		if (type != ShootSolution.Type.Dunk)
		{
			string message = "Shoot rate: " + rate + " , Clean shot rate: " + fCleanShotRate + " random value: " + fRam;
            Debugger.Instance.m_steamer.message += " " + message;
		}
		ShootSolution solution = GameSystem.Instance.shootSolutionManager.GetShootSolution(
			basket.m_rim.center , shooter.position, fRam < rate, type, fRam < fCleanShotRate);
		if ((fRam < rate) != solution.m_bSuccess)
			Logger.LogError("Shoot solution: " + solution.m_id + " success flag error.");
		return solution;
	}

	public virtual int GetScore(int score)
	{
		return score == GlobalConst.PT_3 ? 3 : 2;
	}

	public bool IsDraw()
	{
		return m_homeScore == m_awayScore;
	}

    public Team GetWinTeam()
    {
        if (m_homeScore > m_awayScore)
            return m_homeTeam;
        else if (m_awayScore > m_homeScore)
            return m_awayTeam;
        else
        {
            if (m_mainRole.m_team == m_homeTeam)
                return m_awayTeam;
            else
                return m_homeTeam;
        }
    }

    public Team GetLoseTeam()
    {
        if (GetWinTeam() == m_homeTeam)
            return m_awayTeam;
        else
            return m_homeTeam;
    }

    ////TODO:目前调用此接口，先用GenerateIn3PTPosition来替换
    //public IM.Vector3 GenerateInViewPosition()
    //{
    //    return GenerateInViewPosition(IM.Number.zero);
    //}
    ////TODO:目前调用此接口，先用GenerateIn3PTPosition来替换
    ///** only works for PVE*/
    //public IM.Vector3 GenerateInViewPosition(IM.Number maxDistFromBasket)
    //{
    //    //IM.Vector3 position = IM.Vector3.zero;
    //    //bool isVisible = false;
    //    //bool limitDist = false;
    //    //bool outOfDist = false;
    //    //do
    //    //{
    //    //    position = new IM.Vector3(IM.Random.Range(-new IM.Number(8),new IM.Number(8)),IM.Number.zero,IM.Random.Range(IM.Number.zero,new IM.Number(14));
    //    //    IM.Vector3 viewPos = Camera.main.WorldToViewportPoint(position);
    //    //    isVisible = (0.05 <= viewPos.x && viewPos.x <= 0.95 && 0.05 <= viewPos.y && viewPos.y <= 0.95);
    //    //    if (isVisible)
    //    //    {
    //    //        limitDist = !Mathf.Approximately(maxDistFromBasket, 0f);
    //    //        if (limitDist)
    //    //        {
    //    //            outOfDist = GameUtils.HorizonalDistance(position, mCurScene.mBasket.transform.position) > maxDistFromBasket;
    //    //        }
    //    //    }
    //    //}
    //    //while (!isVisible || (limitDist && outOfDist));
    //    //return position;
    //}

    /**only works for PVE*/
    public IM.Vector3 GenerateIn3PTPosition()
    {
        IM.Vector3 position = IM.Vector3.zero;
        bool in3PT = false;
        do
        {
            //position = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), 0, UnityEngine.Random.Range(0.0f, 14.0f));
            position = new IM.Vector3(IM.Random.Range(-new IM.Number(0,800),new IM.Number(0,800)) , IM.Number.zero,IM.Random.Range(IM.Number.zero,new IM.Number(14)));
            in3PT = mCurScene.mGround.In3PointRange(position.xz);
        }
        while (!in3PT);
        return position;
    }

	void OnScoreChanged(bool isHome, int delta)
	{
		if (delta == 0)
			return;
		if( m_stateMachine.m_curState == null || 
			m_stateMachine.m_curState.m_eState == MatchState.State.eBegin)
			return;

		int myScore = m_mainRole.m_team == m_homeTeam ? m_homeScore : m_awayScore;
		int rivalScore = m_mainRole.m_team == m_homeTeam ? m_awayScore : m_homeScore;
		bool isMySide = isHome == (m_mainRole.m_team == m_homeTeam);

		if (myScore <= 3 && rivalScore <= 3)
			return;

		int scoreBehind = rivalScore - myScore;
		if (scoreBehind >= 3)
		{
            //ShowMatchTipScoreDiff(myScore - rivalScore);
            ShowMatchPromptTip(true, "", myScore - rivalScore);
		}
		else if (scoreBehind > 0)
		{
			if (m_uiMatch != null)
			{
                if (gameMatchTime >= new IM.Number(30))
				{
                    //ShowMatchTip("gameInterface_tip_goal_one_more", false);
                    ShowMatchPromptTip(false, "STR_FIELD_PROMPT78");
				}
				else
				{
                    //ShowMatchTip("gameInterface_tip_notgiveup", false);
                    ShowMatchPromptTip(false, "STR_FIELD_PROMPT77");
				}
			}
		}
		else if (scoreBehind == 0)
		{
			if (isMySide)
                //ShowMatchTip("gameInterface_tip_equalizer", false);
                ShowMatchPromptTip(false, "STR_FIELD_PROMPT79");
		}
		else if (scoreBehind > -3)
		{
			if (isMySide && myScore - delta < rivalScore)
			{
				if (m_outscored)
                    //ShowMatchTip("gameInterface_tip_relead", true);
                    ShowMatchPromptTip(false, "STR_FIELD_PROMPT82");
				else
					ShowMatchTip("gameInterface_tip_overtake", true);
			}
			m_outscored = true;
		}
		else
		{
			m_outscored = true;
			if (isMySide)
                //ShowMatchTipScoreDiff(myScore - rivalScore);
                ShowMatchPromptTip(true, "", myScore - rivalScore);
		}
	}

	void OnGoal(UBasket basket, UBasketball ball)
	{
		if( m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			return;

		if (m_ruler.m_assistor != null && ball.m_actor == m_ruler.m_assistee)
		{
			ShowPlayerTip(m_ruler.m_assistor, "gameInterface_tip_assist");
			m_ruler.m_assistor.mStatistics.secondary_attack++;
		}
		
		if (ball.m_pt == GlobalConst.PT_3)
		{
			ShowBasketTip(ball.m_actor, "gameInterface_tip_3point");
            if (m_bOverTime)
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Critical);
            else if (ball.m_actor == m_mainRole || EnableNPCGoalSound())
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.FarShoot);
		}
		else if (ball.m_pt == GlobalConst.PT_2)
		{
				/*
			if (ball.m_isLayup)
			{
				if (ball.m_shootArea == Area.eNear)
					ShowBasketTip(ball.m_actor, "gameInterface_tip_layupSuccess");
				else
					ShowBasketTip(ball.m_actor, "gameInterface_tip_layup");
			}
			else */
			if (ball.m_isDunk)
			{
                if (m_bOverTime)
					PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Critical);
                else if (ball.m_actor == m_mainRole || EnableNPCGoalSound())
					PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Dunk);
			}
			else
			{
                //if (ball.m_shootArea == Area.eNear)
                //    ShowBasketTip(ball.m_actor, "gameInterface_tip_nearlySuccess");
                //else
                //    ShowBasketTip(ball.m_actor, "gameInterface_tip_fg");

                if (m_bOverTime)
					PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Critical);
                else if (ball.m_actor == m_mainRole || EnableNPCGoalSound())
					PlaySoundManager.Instance.PlaySound(MatchSoundEvent.NormalShoot);
			}
		}

		if( m_uiMatch != null && m_bShowGoalUIEffect )
			m_uiMatch.ShowScoreGoal(ball);

		if (EnableGoalState())
		{
			if (ball.m_actor != null)
				ball.m_actor.mStatistics.AddUpShoot();
			DoGoalState();
		}
	}

	void OnPlayerStatUpdated(PlayerStatistics.StatType type, PlayerStatistics playerStatistics)
	{
		if (!EnableMatchAchievement())
			return;

		Dictionary<PlayerStatistics.StatType, uint> completion = null;	
		if (!completedAchievement.TryGetValue(playerStatistics.player, out completion))
		{
			completion = new Dictionary<PlayerStatistics.StatType, uint>();
			completedAchievement.Add(playerStatistics.player, completion);
		}

		uint level = 0;
		completion.TryGetValue(type, out level);

		uint curValue = (uint)playerStatistics.GetStatValue(type);
		uint highestValue;
		highestAchievement.TryGetValue(type, out highestValue);
		if (curValue <= highestValue)
		{
			return;
		}
		else
		{
			highestAchievement[type] = curValue;
		}
		//Logger.Log("***Player: " + playerStatistics.player.m_name + " type: " + type + " value: " + curValue);
		MatchAchievement achievement = GameSystem.Instance.MatchAchievementConfig.GetMatchAchievement(type, curValue);
		if (achievement != null && (achievement.level > level || achievement.level == 3))	//New achievement
		{
			//Logger.Log("Match achievement completed, type: " + type + ", level: " + achievement.level + " value: " + curValue);
			completion[type] = achievement.level;
			MatchAchievementItem[] items = m_tipPanel.GetComponentsInChildren<MatchAchievementItem>();
			foreach (MatchAchievementItem item in items)
				item.MoveUp();
			GameObject obj = CommonFunction.InstantiateObject(matchAchievementItemPrefab, m_tipPanel.transform);
			obj.name = "MatchAchievementItem";
			MatchAchievementItem curItem = obj.GetComponent<MatchAchievementItem>();
			curItem.player = playerStatistics.player;
			curItem.matchAchievement = achievement;
		}
	}

	public void ShowCanShoot()
	{
		UISprite sprite = NGUITools.AddSprite(m_tipPanel, m_atlas, "gameInterface_text_shoot");
		sprite.gameObject.name = "TipCanShoot";
		sprite.MakePixelPerfect();
	}

	public void HideCanShoot()
	{
		Transform transform = m_tipPanel.transform.FindChild("TipCanShoot");
		if (transform != null)
			NGUITools.Destroy(transform.gameObject);
	}

	public void ShowOpportunity()
	{
		UISprite sprite = NGUITools.AddSprite(m_tipPanel, m_atlas, "gameInterface_text_chance");
		sprite.gameObject.name = "TipOpportunity";
		sprite.MakePixelPerfect();
		TweenAlpha tween = sprite.gameObject.AddComponent<TweenAlpha>();
		tween.from = 0.3f;
		tween.to = 1f;
		tween.style = UITweener.Style.PingPong;
		tween.method = UITweener.Method.EaseInOut;
	}

	public void HideOpportunity()
	{
		Transform transform = m_tipPanel.transform.FindChild("TipOpportunity");
		if (transform != null)
			NGUITools.Destroy(transform.gameObject);
	}

	public void ShowAnimTip(string spriteTip)
	{
		UISprite sprite = NGUITools.AddSprite(m_tipPanel, m_atlas, spriteTip);
		sprite.gameObject.name = "Tip" + spriteTip;
		sprite.MakePixelPerfect();
		TweenAlpha tween = sprite.gameObject.AddComponent<TweenAlpha>();
		tween.from = 0.3f;
		tween.to = 1f;
		tween.style = UITweener.Style.PingPong;
		tween.method = UITweener.Method.EaseInOut;
	}

	public void HideAnimTip(string spriteTip)
	{
		Transform transform = m_tipPanel.transform.FindChild("Tip" + spriteTip);
		if (transform != null)
			NGUITools.Destroy(transform.gameObject);
	}

	public void ShowCombo(uint combo)
	{
		HideCombo();

		UIWidget widget = NGUITools.AddWidget<UIWidget>(m_tipPanel);
		widget.gameObject.name = "TipCombo";
		UIAnchor anchor = widget.gameObject.AddComponent<UIAnchor>();
		anchor.side = UIAnchor.Side.TopRight;
		anchor.relativeOffset = new Vector2(0f, -0.25f);
		UISprite text = NGUITools.AddSprite(widget.gameObject, m_atlas, "gameInterface_text_doubleHit");
		text.MakePixelPerfect();
		text.pivot = UIWidget.Pivot.Right;
		text.transform.localPosition = Vector3.zero;

		int x = text.width;
		uint num = combo;
		do
		{
			uint mod = num % 10;
			UISprite numSprite = NGUITools.AddSprite(widget.gameObject, m_atlas, "gameInterface_tip_figure_jade_" + mod);
			numSprite.MakePixelPerfect();
			numSprite.pivot = UIWidget.Pivot.Right;
			numSprite.transform.localPosition = new Vector3(-x, 0f, 0f);
			x += numSprite.width;
			num = num / 10;
		} while (num > 0);
	}
	
	public void HideCombo()
	{
		Transform transform = m_tipPanel.transform.FindChild("TipCombo");
		if (transform != null)
			NGUITools.Destroy(transform.gameObject);
	}

	public void ShowComboBonus(float bonusRatio)
	{
		HideComboBonus();

		UIWidget widget = NGUITools.AddWidget<UIWidget>(m_tipPanel);
		widget.gameObject.name = "TipComboBonus";
		UIAnchor anchor = widget.gameObject.AddComponent<UIAnchor>();
		anchor.side = UIAnchor.Side.TopRight;
		anchor.relativeOffset = new Vector2(0f, -0.07f);

		int x = 32;
		UISprite percent = NGUITools.AddSprite(widget.gameObject, m_atlas, "gameInterface_figure_green_be%");
		percent.MakePixelPerfect();
		percent.pivot = UIWidget.Pivot.Right;
		percent.transform.localPosition = new Vector3(-x, 0f, 0f);

		x += percent.width;
		uint num = (uint)(bonusRatio * 100);
		do
		{
			uint mod = num % 10;
			UISprite numSprite = NGUITools.AddSprite(widget.gameObject, m_atlas, "gameInterface_figure_green_be" + mod);
			numSprite.MakePixelPerfect();
			numSprite.pivot = UIWidget.Pivot.Right;
			numSprite.transform.localPosition = new Vector3(-x, 0f, 0f);
			x += numSprite.width;
			num = num / 10;
		} while (num > 0);

		UISprite text = NGUITools.AddSprite(widget.gameObject, m_atlas, "gameInterface_text_Scoringscension");
		text.MakePixelPerfect();
		text.pivot = UIWidget.Pivot.Right;
		text.transform.localPosition = new Vector3(-x, 0f, 0f);
	}
	
	public void HideComboBonus()
	{
		Transform transform = m_tipPanel.transform.FindChild("TipComboBonus");
		if (transform != null)
			NGUITools.Destroy(transform.gameObject);
	}

	public void HideAllTip()
	{
		if (m_tipPanel != null)
		{
			foreach (Transform child in m_tipPanel.transform)
			{
				NGUITools.Destroy(child.gameObject);
			}
		}
	}

	protected virtual void _CreatePlayersData()
	{
		_GeneratePlayerData(m_config.MainRole, m_config.MainRole.id.Length > 4 );
		foreach( GameMatch.Config.TeamMember member in m_config.NPCs )
			_GeneratePlayerData(member, member.team != m_config.MainRole.team);
	}

	protected Player _GeneratePlayerData(GameMatch.Config.TeamMember member, bool rival=false)
	{
		if (member.roleInfo == null)
		{
			if (rival)
			{
				member.roleInfo = new RoleInfo();
				member.roleInfo.id = uint.Parse(member.id);
                if (leagueType != LeagueType.eAsynPVP && leagueType != LeagueType.ePVP && leagueType != LeagueType.eTour
                    && leagueType != LeagueType.eQualifyingNewer
                    )

                    // Quality not use.
                    member.roleInfo.quality = 1;
			}
			else
			{
				member.roleInfo = MainPlayer.Instance.GetRole2(uint.Parse(member.id));
				if (member.roleInfo == null)
				{
					member.roleInfo = new RoleInfo();
					member.roleInfo.id = uint.Parse(member.id);
					//member.roleInfo.quality = GameSystem.Instance.RoleBaseConfigData2.GetQualityByID(member.roleInfo.id);
                    member.roleInfo.quality = 1;
					//member.roleInfo.star = MainPlayer.Instance.GetRole2(member.roleInfo.id).star;
				}
			}
		}
		AttrData attrData = null;
		string name = string.Empty;
		if (!rival && uint.Parse(member.id) == MainPlayer.Instance.CaptainID
           && leagueType != LeagueType.eLadderAI && leagueType != LeagueType.eQualifyingNewerAI
            )
		{
			//name = MainPlayer.Instance.Name;//???????
			name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			if (attrData == null)
                attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
		}
        else if( leagueType == LeagueType.ePracticeLocal )
        {
            if( rival )
            {
                if( attrData == null )
                {
                    RoleBaseData2 rd2 = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(member.roleInfo.id);
                    attrData = new AttrData();
                    attrData.attrs = rd2.attrs_symbol;
                    attrData.bias = (uint)rd2.bias;
                    attrData.level = 1;
                    attrData.quality = 1;
                    attrData.talent = (uint)rd2.talent;
                    attrData.fightingCapacity = MainPlayer.Instance.CalcBaseFighting(member.roleInfo.id);
                }
            }
            else
            {
                if (attrData == null)
					attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
			}
			name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
        }
        else if (leagueType == LeagueType.eQualifyingNewerAI )
        {
            if (member.id.Length < 5)
            {
                name = MainPlayer.Instance.Name;
                if (attrData == null)
                    attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo, null, member.badgeBook);
            }
            else
            {
                if (attrData == null)
                    attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(uint.Parse(member.id));
                MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
                LuaFunction func = LuaScriptMgr.Instance.GetLuaFunction("QualifyingNewerAI.GetNPCName");

                GameMatch_QualifyingNewerAI match = this as GameMatch_QualifyingNewerAI;
                name = (string)(func.Call(new object[] { match._nameIndex++ })[0]);
            }
        }

        else if (leagueType == LeagueType.ePractise1vs1 || leagueType == LeagueType.eCareer ||
                 leagueType == LeagueType.ePractise ||
                 (leagueType == LeagueType.eRegular1V1 && GetMatchType() == Type.eCareer3On3) ||
                 (leagueType == LeagueType.eQualifyingNew && GetMatchType() == Type.eCareer3On3)
                 )
        {
            if (rival)
            {
                if (attrData == null)
                    attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(uint.Parse(member.id));
                MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);

                name = GameSystem.Instance.NPCConfigData.GetName(uint.Parse(member.id));
            }
            else
            {
                if (attrData == null)
                    attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo, null, member.badgeBook);

                name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
            }
        }
        else if (leagueType == LeagueType.eLadderAI)
        {
            if (member.id.Length < 5)
            {
                name = MainPlayer.Instance.Name;
                if (attrData == null)
                    attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo, null, member.badgeBook);
            }
            else
            {
                if (attrData == null)
                    attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(uint.Parse(member.id));
                MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
                LuaFunction func = LuaScriptMgr.Instance.GetLuaFunction("QualifyingNewerAI.GetNPCName");

                GameMatch_LadderAI match = this as GameMatch_LadderAI;
                name = (string)(func.Call(new object[] { match._nameIndex++ })[0]);
            }
        }
        else if (leagueType == LeagueType.eTour)
        {
            if (rival)
            {
                if (attrData == null)
                    attrData = GameSystem.Instance.NPCConfigData.GetTourNPCAttrData(uint.Parse(member.id), MainPlayer.Instance.CurTourID);
                MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
                name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
                int num = GameSystem.Instance.mClient.mPlayerManager.m_Players.Count;
                if( num >= 3 )
                {
                    TourData td = GameSystem.Instance.TourConfig.GetTourData(MainPlayer.Instance.Level, MainPlayer.Instance.CurTourID);
                    member.roleInfo.star = td.star[num-3];
                    member.roleInfo.quality = td.quality[num - 3];
                    member.roleInfo.level = MainPlayer.Instance.Level;
                }
			}
			else
			{
				if (attrData == null)
					attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
				name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			}
		}
		else if (leagueType == LeagueType.eAsynPVP)
		{
			name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			RobotAttrData robotAttr = GameSystem.Instance.AttrDataConfigData.GetRobotAttr(
				MainPlayer.Instance.Level, MainPlayer.Instance.WinningStreak,
				(PositionType)(GameSystem.Instance.RoleBaseConfigData2.GetConfigData(MainPlayer.Instance.CaptainID).position));
			member.AIID = robotAttr.AIID;
			if (attrData == null)
			{
				attrData = robotAttr.attrData;
			}
		}
		else if (leagueType == LeagueType.ePVP || 
			(leagueType == LeagueType.eRegular1V1 && GetMatchType() != Type.eCareer3On3) ||
			(leagueType == LeagueType.eQualifyingNew && GetMatchType() != Type.eCareer3On3) ||
			(leagueType == LeagueType.eQualifyingNewer && GetMatchType() != Type.eCareer3On3)
            )
		{
			uint id = uint.Parse(member.id);
			if (id < 10000)
				attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, member.equipInfo, member.squadInfo, member.mapInfo, member.fashionAttrInfo,member.badgeBook);
			else
			{
				attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(id);
				MainPlayer.Instance.CalcFightingCapacity(id, 0, attrData);
			}
		}
		else if (leagueType == LeagueType.eQualifying)
		{
			if (member.isRobot)
			{
				attrData = GameSystem.Instance.NPCConfigData.GetQualifyingNPCAttrData(member.roleInfo.id, member.roleInfo.level);
				MainPlayer.Instance.CalcFightingCapacity(member.roleInfo.id, member.roleInfo.star, attrData);
			}
			else
				attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, member.equipInfo, member.squadInfo, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
			name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
		}
		else if (leagueType == LeagueType.eBullFight)
		{
			if (rival)
			{
				if (attrData == null)
				{
					attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(uint.Parse(member.id));
					MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
				}
				
				name = GameSystem.Instance.NPCConfigData.GetName(uint.Parse(member.id));
			}
			else
			{
				if (attrData == null)
				{
					attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
				}
				
				name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			}
		}
		else if( leagueType == LeagueType.eShoot )
		{
			if(rival)
			{
				if(attrData == null)
				{
					attrData = GameSystem.Instance.shootGameConfig.GetShootgNPCAttrData(uint.Parse(member.id), member.mode_type_id, MainPlayer.Instance.Level);
					MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
				}
				name = GameSystem.Instance.NPCConfigData.GetName(uint.Parse(member.id));
			}
			else
			{
				if(attrData == null)
				{
					attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
				}
				name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			}
		}
		else
		{
			if (attrData == null)
			{
				attrData = GameSystem.Instance.NPCConfigData.GetNPCAttrData(uint.Parse(member.id));
				MainPlayer.Instance.CalcFightingCapacity(uint.Parse(member.id), 0, attrData);
				name = GameSystem.Instance.NPCConfigData.GetName(uint.Parse(member.id));
			}
			if(attrData == null)
			{
				attrData = MainPlayer.Instance.GetRoleAttrs(member.roleInfo, null, null, MainPlayer.Instance.MapIDInfo,null,member.badgeBook);
				name = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(uint.Parse(member.id)).name;
			}
		}
		Player player = _GenerateTeamMember(member, name);
		if( attrData != null )
			player.m_attrData = attrData;

		const int iMainRoleIdLength = 4;
		const int iNPCIdLength = 5;
		if( member.id.Length > iNPCIdLength )
			Logger.LogError("Invalid npc id.");
			
		member.bIsNPC = member.id.Length > iMainRoleIdLength;
		if( member.bIsNPC )
		{
			NPCConfig npcConfig = GameSystem.Instance.NPCConfigData.GetConfigData(uint.Parse(member.id));
			if( npcConfig != null )
				member.AIID = npcConfig.aiid;
		}
		return player;
	}
	
	protected void _UpdateCamera(Player mainRole)
	{
		if (mainRole != null)
            m_cam.m_trLook = mainRole.transform;
        else
            m_cam.m_trLook = mCurScene.mBasket.transform;
		m_origCameraForward = new Vector3(0.0f, 0.0f, 1.0f);
        //GameMatchConfig.Instance.LoadCameraConfig(GlobalConst.DIR_XML_CAMERA, m_cam);
    }

    /**创建队员*/
    public void _CreateTeamMember(Player player)
    {
        GameSystem.Instance.gameMatchConfig.LoadPlayerAttributes(player);
        player.mStatistics = new PlayerStatistics(player, this);
		player.mStatistics.onStatUpdated += OnPlayerStatUpdated;
       
		player.model = new PlayerModel(player);

		RoleShape roleShape = GameSystem.Instance.RoleShapeConfig.GetConfig((uint)player.m_shapeID);
		if( roleShape == null )
		{
			Logger.LogError("No role shape data found for player: " + player.m_shapeID);
			return;
		}

        player.model.Init(roleShape, false);
        player.Build();
		
		player.BuildGameData(new IM.Number(1, 100));

		player.m_StateMachine.SetState( PlayerState.State.eNone );

        player.m_StateMachine.m_listeners.Add(mCurScene);
        foreach (MatchState matchState in m_stateMachine)
            player.eventHandler.AddEventListener(matchState);

        player.m_InfoVisualizer = new PlayerInfoVisualizer(player);

        if (m_shadowEffect != null)
        {
            GameObject shadow = GameObject.Instantiate(m_shadowEffect) as GameObject;
            shadow.transform.parent = player.transform;
            shadow.transform.localPosition = new Vector3(0.0f, 0.02f, 0.0f);
        }

        if (m_stateMachine != null)
            m_stateMachine.m_matchStateListeners.Add(player);

		Logger.Log("create a new team member: " + player.m_roomPosId);
    }

	protected virtual void _CreateRoomUser(PlayerData playerData)
	{
		foreach(RoleInfo roleInfo in playerData.roles )
		{
			uint roleId = roleInfo.id;
			Config.TeamMember tm = new Config.TeamMember();
			tm.team = playerData.is_home_field == 0 ? Team.Side.eHome : Team.Side.eAway;

			tm.id = roleId.ToString();
			tm.roleInfo = roleInfo;
			tm.equipInfo = playerData.equipments;
            tm.squadInfo = playerData.squad;
            if (playerData.role_map.Count > 0)
            {
                tm.mapInfo = new List<uint>();
                foreach (KeyValueData data in playerData.role_map)
                {
                    tm.mapInfo.Add(data.id);
                }
            }
            if (playerData.fashion_items != null && playerData.fashion_items.Count > 0) 
            {
                tm.fashionAttrInfo = new List<List<uint>>();
                foreach (GoodsProto goods in playerData.fashion_items)
                {
                    foreach (FashionSlotProto fashionSlot in tm.roleInfo.fashion_slot_info)
                    {
                        if (fashionSlot.fashion_uuid == goods.uuid)
                        {
                            tm.fashionAttrInfo.Add(goods.fashion_attr_id);
                        }
                    }
                }
            }
            //获取每个角色使用的涂鸦墙信息
            if (roleInfo.badge_book_id != 0)
            {
                foreach (BadgeBook book in playerData.badge_info.badge_book)
                {
                    if (roleInfo.badge_book_id == book.id)
                    {
                        tm.badgeBook = book;
                        break;
                    }
                }
            }

			if( m_config.type == Type.ePVP_3On3 )
				tm.team_name = playerData.name;

            Player newPlayer;
            if (playerData.acc_id == MainPlayer.Instance.AccountID)
            {
				newPlayer = _GeneratePlayerData(tm, false);
				_CreateTeamMember(newPlayer);
            }
            else
            { 
				newPlayer = _GeneratePlayerData(tm, true);
				_CreateTeamMember(newPlayer);
            }
			newPlayer.m_roleInfo.acc_id = playerData.acc_id;

			if( playerData.acc_id == MainPlayer.Instance.AccountID && m_mainRole == null )
			{
				m_mainRole = newPlayer;
				m_mainTeam = m_mainRole.m_team;
			}
			if( m_config.type == Type.ePVP_3On3 )
			{
				newPlayer.m_name = tm.team_name;
				if( newPlayer.m_InfoVisualizer != null )
					newPlayer.m_InfoVisualizer.m_uiName.text = tm.team_name;
			}
			else if (m_config.type == Type.eAsynPVP3On3)
			{
				newPlayer.m_aiMgr = new AISystem_Basic(this, newPlayer);
				newPlayer.m_aiMgr.m_enable = (newPlayer != m_mainRole);
				newPlayer.m_InfoVisualizer.CreateStrengthBar();
			}

			if( newPlayer.m_catchHelper == null )
				newPlayer.m_catchHelper = new CatchHelper(newPlayer);
			newPlayer.m_catchHelper.ExtractBallLocomotion();
		}
	}

	virtual protected Player _GenerateTeamMember(GameMatch.Config.TeamMember member, string name)
	{
        Team team = (member.team == Team.Side.eAway ? m_awayTeam : m_homeTeam);
		member.roleInfo.index = (uint)GameSystem.Instance.mClient.mPlayerManager.m_Players.Count + 1;

		Player player = GameSystem.Instance.mClient.mPlayerManager.CreatePlayer(member.roleInfo, team);
		player.m_teamName = member.team_name;
		player.m_name = name;
		player.m_config = member;
		return player;
	}

    virtual public void AssumeDefenseTarget()
    {
		Logger.Log("AssumeDefenseTarget");
        int count = m_homeTeam.GetMemberCount();
        for (int idx = 0; idx != count; idx++)
        {
            if (idx >= m_awayTeam.GetMemberCount())
                break;

            m_awayTeam.GetMember(idx).m_defenseTarget = m_homeTeam.GetMember(idx);
			m_awayTeam.GetMember(idx).m_defTargetSwitched = false;
            m_homeTeam.GetMember(idx).m_defenseTarget = m_awayTeam.GetMember(idx);
			m_homeTeam.GetMember(idx).m_defTargetSwitched = false;
			Logger.Log(m_homeTeam.GetMember(idx).m_name + "<->" + m_awayTeam.GetMember(idx).m_name);

			if( m_awayTeam.GetMember(idx).m_AOD == null )
            	m_awayTeam.GetMember(idx).m_AOD = new AOD(m_awayTeam.GetMember(idx));

			if( m_homeTeam.GetMember(idx).m_AOD == null )
            	m_homeTeam.GetMember(idx).m_AOD = new AOD(m_homeTeam.GetMember(idx));
        }
    }

	public IM.Number GetAttrReduceScale(string attr, Player player)
	{
		if (player.m_roleInfo.id == MainPlayer.Instance.CaptainID)
		{
			AttrReduceConfig.AttrReduceItem item;
			if (attrReduceItems != null && attrReduceItems.TryGetValue(attr, out item))
			{
				if (player.m_finalAttrs[attr] < item.requireValue)
					return item.scaleFactor;
			}
		}
		return IM.Number.one;
	}

	virtual public void InitBallHolder()
	{
		UBasketball ball = mCurScene.mBall;
		if(ball != null && ball.m_owner != null )
			ball.m_owner.DropBall(ball);
		if (ball != null)
			ball.Reset();
		
		if( m_offenseTeam.GetMemberCount() != 0 )
		{
			Player offenserWithBall = m_offenseTeam.GetMember(0);
			offenserWithBall.GrabBall(ball);
			offenserWithBall.m_StateMachine.SetState(PlayerState.State.eHold);
		}
	}

	public void EnhanceAttr()
	{
		if (neverWin)
		{
			Player defenseTarget = m_mainRole.m_defenseTarget;
			if (defenseTarget != null)
			{
				foreach (Player member in defenseTarget.m_team.members)
				{
					member.EnhanceAttr(new IM.Number(1,300));
				}
			}
			return;
		}
		IM.Number fcRatio = m_strongTeam.fightingCapacity / m_weakTeam.fightingCapacity;
		Logger.Log("FC: " + m_strongTeam.fightingCapacity + " " + m_weakTeam.fightingCapacity + " Ratio: " + fcRatio);
		int expectedScoreDiff = GameSystem.Instance.FightingCapacityConfig.GetExpectedScoreDiff(fcRatio);
		if (GetMatchType() == Type.eBullFight)
			expectedScoreDiff = (int)(expectedScoreDiff * 0.5f);
		int realScoreDiff = m_strongTeamScore - m_weakTeamScore;
		int enhanceLevel = expectedScoreDiff - realScoreDiff;
		Logger.Log("Expected score diff: " + expectedScoreDiff + " Real score diff: " + realScoreDiff + " Enhance level: " + enhanceLevel);
		IM.Number factor = GameSystem.Instance.FightingCapacityConfig.GetEnhanceFactor(enhanceLevel);
		foreach (Player member in m_strongTeam.members)
		{
			member.EnhanceAttr(factor);
		}
	}

    public virtual void ProcessTurn(FrameInfo turn, IM.Number deltaTime)
    {
        ClientInput input = turn.info.Find(i => i.acc_id == MainPlayer.Instance.AccountID);
        if (input != null)
        {
            InputDirection dir = (InputDirection)input.dir;
            Command cmd = (Command)input.cmd;
            //Logger.Log(string.Format("ProcessTurn, {0} Dir:{1} Cmd:{2}", turn.frameNum, dir, cmd));
            Player player = InputReader.Instance.player;
            player.m_inputDispatcher.dir = dir;
            player.m_inputDispatcher.cmd = cmd;
        }
    }
}
