using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameSystem
{

    static GameSystem _instance;
    public static GameSystem Instance { get { if (_instance == null) { _instance = new GameSystem(); } return _instance; } private set { } }

    public VDebugStatus mVDebug { get; private set; }
    public Client mClient { get; private set; }
    public NetworkManager mNetworkManager { get; private set; }

    //public bool mClientOnly = true;
    public EngineFramework mEngineFramework;

    private bool mRevLogoutOnce = false;

    public AnnouncementConfig AnnouncementConfigData;
    public BadgeAttrConfig BadgeAttrConfigData;
    public ConstStringConfig ConstStringConfigData;
    public CommonConfig CommonConfig;
    public ServerDataConfig ServerDataConfigData;
    public AttrNameConfig AttrNameConfigData;
    public BaseDataConfig2 RoleBaseConfigData2;
    public AttrDataConfig AttrDataConfigData;
	public ReboundAttrConfig ReboundAttrConfigData;
    public TeamLevelConfig TeamLevelConfigData;
    public RoleLevelConfig RoleLevelConfigData;
    public NPCDataConfig NPCConfigData;
    public CareerConfig CareerConfigData;
    public SkillConfig SkillConfig;
    public GoodsConfig GoodsConfigData;
    public StoreGoodsConfig StoreGoodsConfigData;
    public BaseDataBuyConfig BaseDataBuyConfigData;//购买金币体力
    public PVPPointConfig PVPPointConfig;
    public WinningStreakAwardConfig WinningStreakAwardConfig;
    public TaskDataConfig TaskConfigData;
    public AwardPackDataConfig AwardPackConfigData;//奖励库配置
    public ArticleStrengthConfig ArticleStrengthConfig;
    public PhRegainConfig PhRegainConfig;
    public PractiseConfig PractiseConfig;
    public PracticePveConfig PracticePveConfig;
    public PractiseStepConfig PractiseStepConfig;
	public GameModeConfig GameModeConfig;
    public TrainingConfig TrainingConfig;
    public TattooConfig TattooConfig;
    public EquipmentConfig EquipmentConfigData;
	public TourConfig TourConfig;
	public GuideConfig GuideConfig;
	public FunctionConditionConfig FunctionConditionConfig;
	public MatchAchievementConfig MatchAchievementConfig;
    public BadgeSlotConfig BadgeSlotsConfig;
    public GoodsComposeNewConfig GoodsComposeNewConfigData;

	public SceneConfig	SceneConfig;
	public BodyInfoListConfig BodyInfoListConfig;
	public RoleShapeConfig RoleShapeConfig;
	public FashionConfig FashionConfig;
    public FashionShopConfig FashionShopConfig;
	public SpecialActionConfig SpecialActionConfig;
	public StealConfig StealConfig;
	public CurveRateConfig CurveRateConfig;
	public DunkRateConfig DunkRateConfig;
	public AIConfig AIConfig;
	public AttrReduceConfig AttrReduceConfig;
    public VipPrivilegeConfig VipPrivilegeConfig;
	public PushConfig pushConfig;
    public QualifyingConfig qualifyingConfig;
	public QualifyingNewConfig qualifyingNewConfig;
	public QualifyingNewerConfig qualifyingNewerConfig;
    public PresentHpConfig presentHpConfigData;
	public LotteryConfig LotteryConfig;
    public StarAttrConfig starAttrConfig;
    //public QualityAttrConfig qualityAttrConfig;
    public QualityAttrCorConfig qualityAttrCorConfig;
    public SkillUpConfig skillUpConfig;
	public RankConfig RankConfig;
    public SignConfig signConfig;
    public BullFightConfig bullFightConfig;
	public HedgingConfig HedgingConfig;
    public RoleGiftConfig roleGiftConfig;
    public ShootGameConfig shootGameConfig;
    public NewComerSignConfig NewComerSignConfig;
	public FightingCapacityConfig FightingCapacityConfig;
	public PotientialEffectConfig PotientialEffectConfig;
	public DebugConfig DebugConfig;
    public MapConfig MapConfig;
    public ActivityConfig activityConfig;
    public TrialConfig trialConfig;
    public GameMatchConfig gameMatchConfig;
    public ShootSolutionManager shootSolutionManager;
    public TalentConfig talentConfig;
    public LadderConfig ladderConfig;
	public MatchSoundConfig matchSoundConfig;
	public MatchMsgConfig matchMsgConfig;
    public MatchPointsConfig MatchPointsConfig;
	
    public int loadConfigCnt = 0;
    public int readConfigCnt = 0;
    public bool canLoadConfig = false;
    public bool loadConfigFinish = false;
    public bool showLoading = false;
    public bool isNewPlayer = false;
    public bool configPreLoadFinish = false;
    public bool configCommonLoadFinish = false;

    //服务器时间(每3秒和服务器同步一次)
	public static double _Time;
	public static long mTime
	{
		get { return (long)_Time; }
		set { _Time = (double)value; }
	}
    //=================心跳机制================//
    public const float MaxIntervalTime = 5.0F;
    public const int MaxIntervalCount = 12;
    private bool _isOpenHeartbeatMsg = false;
    private float _currentIntervalTime = 0.0F;
    private int _currentIntervalCount = 0;
    public const float LoseFocusTime = 15f;

    public string serverIP;
    public int port;

	public bool appPaused;

    private object LockObject = new object();

    public GameSystem()
    {

    }

    public void Start()
    {
        Application.targetFrameRate = 30;
        Application.runInBackground = true;
        try
        {
            DateTime time = System.DateTime.Now;
            mClient = new Client();
            Logger.Log("【Time】new Client=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

            time = System.DateTime.Now;
            mVDebug = new VDebugStatus();
            Logger.Log("【Time】new VDebugStatus=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

            time = System.DateTime.Now;
            mNetworkManager = new NetworkManager();
            Logger.Log("【Time】new NetworkManager=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

            time = System.DateTime.Now;
            LuaMgr.Instance.Init();
            Logger.Log("【Time】LuaMgr.Instance.Init=>" + (System.DateTime.Now - time).TotalSeconds.ToString());
        }
        catch (UnityException exp)
        {
            Logger.LogError(exp.Message);
            if (mClient != null)
                mClient.Exit();
        }

        int nAudio = PlayerPrefs.GetInt("Audio", 1);
        AudioListener.volume = (float)nAudio;

        canLoadConfig = true;
    }

    public void PreLoadConfig()
    {
        if(ServerDataConfigData == null)
            ServerDataConfigData = new ServerDataConfig();
    }

    public void LoadConfig()
    {
        AnnouncementConfigData = new AnnouncementConfig();
        BadgeAttrConfigData = new BadgeAttrConfig();
        ConstStringConfigData = new ConstStringConfig();
        CommonConfig = new CommonConfig();

        AttrNameConfigData = new AttrNameConfig();
        RoleBaseConfigData2 = new BaseDataConfig2();
        AttrDataConfigData = new AttrDataConfig();
        TeamLevelConfigData = new TeamLevelConfig();
        RoleLevelConfigData = new RoleLevelConfig();
        NPCConfigData = new NPCDataConfig();
        SkillConfig = new SkillConfig();
        GoodsConfigData = new GoodsConfig();
        StoreGoodsConfigData = new StoreGoodsConfig();
        BaseDataBuyConfigData = new BaseDataBuyConfig();
        TaskConfigData = new TaskDataConfig();
        AwardPackConfigData = new AwardPackDataConfig();
        PractiseConfig = new PractiseConfig();
        PracticePveConfig = new PracticePveConfig();
        PractiseStepConfig = new PractiseStepConfig();
        GameModeConfig = new GameModeConfig();
        TrainingConfig = new TrainingConfig();
        TattooConfig = new TattooConfig();
        EquipmentConfigData = new EquipmentConfig();
        TourConfig = new TourConfig();
        GuideConfig = new GuideConfig();
        FunctionConditionConfig = new FunctionConditionConfig();
        RoleShapeConfig = new RoleShapeConfig();
        FashionConfig = new FashionConfig();
        FashionShopConfig = new FashionShopConfig();
        VipPrivilegeConfig = new VipPrivilegeConfig();
        pushConfig = new PushConfig();
        presentHpConfigData = new PresentHpConfig();
        LotteryConfig = new LotteryConfig();
        starAttrConfig = new StarAttrConfig();
        qualityAttrCorConfig = new QualityAttrCorConfig();
        skillUpConfig = new SkillUpConfig();
        RankConfig = new RankConfig();
        signConfig = new SignConfig();
        NewComerSignConfig = new NewComerSignConfig();
        FightingCapacityConfig = new FightingCapacityConfig();
        BodyInfoListConfig = new BodyInfoListConfig();
        BadgeSlotsConfig = new BadgeSlotConfig();
        GoodsComposeNewConfigData = new GoodsComposeNewConfig();

        SceneConfig = new SceneConfig();

        ReboundAttrConfigData = new ReboundAttrConfig();
        CareerConfigData = new CareerConfig();
        PotientialEffectConfig = new PotientialEffectConfig();
        PVPPointConfig = new PVPPointConfig();
        WinningStreakAwardConfig = new WinningStreakAwardConfig();
        ArticleStrengthConfig = new ArticleStrengthConfig();
        PhRegainConfig = new PhRegainConfig();
        MatchAchievementConfig = new MatchAchievementConfig();
        SpecialActionConfig = new SpecialActionConfig();
        StealConfig = new StealConfig();
        CurveRateConfig = new CurveRateConfig();
        DunkRateConfig = new DunkRateConfig();
        AIConfig = new AIConfig();
        AttrReduceConfig = new AttrReduceConfig();
        qualifyingConfig = new QualifyingConfig();
		qualifyingNewConfig = new QualifyingNewConfig();
		qualifyingNewerConfig = new QualifyingNewerConfig();
        bullFightConfig = new BullFightConfig();
        HedgingConfig = new HedgingConfig();
        roleGiftConfig = new RoleGiftConfig();
		DebugConfig	= new DebugConfig();
        shootGameConfig = new ShootGameConfig();
        MapConfig = new MapConfig();
        activityConfig = new ActivityConfig();
        trialConfig = new TrialConfig();
        gameMatchConfig = new GameMatchConfig();
        shootSolutionManager = new ShootSolutionManager();
        talentConfig = new TalentConfig();
        ladderConfig = new LadderConfig();
		matchSoundConfig = new MatchSoundConfig();
		matchMsgConfig = new MatchMsgConfig();
        MatchPointsConfig = new MatchPointsConfig();
        AnimationSampleManager.Instance.LoadXml();
    }

    public void ParseCommonConfig()
    {
        try
        {
            while (true)
            {
                AnnouncementConfigData.ReadConfig();
                BadgeAttrConfigData.ReadConfig();
                ConstStringConfigData.ReadConfig();
                CommonConfig.ReadConfig();
                TrainingConfig.ReadConfig();
                TattooConfig.ReadConfig();
                EquipmentConfigData.ReadConfig();
                starAttrConfig.ReadConfig();
                qualityAttrCorConfig.ReadConfig();
                FightingCapacityConfig.ReadConfig();
                HedgingConfig.ReadConfig();
                roleGiftConfig.ReadConfig();
                shootGameConfig.ReadConfig();
                TourConfig.ReadConfig();
                GuideConfig.ReadConfig();
                DebugConfig.ReadConfig();
                talentConfig.ReadConfig();
                ladderConfig.ReadConfig();


                lock (LockObject)
                {
                    //Logger.Log("ParseCommonConfig --- readConfigCnt : " + readConfigCnt);
                    if (loadConfigCnt > 30 && loadConfigCnt == readConfigCnt)
                    {
                        return;
                    }
                }
                Thread.Sleep(15);
            }
        }
        catch (System.Exception ex)
        {
            //Logger.LogError("Config parse error: " + ex.Message + "" + LogType.Exception + ex.StackTrace);
        }
    }

    public void ParseConfig()
    {
		try
		{
			while (true)
            {
                ParsePreConfig();
                //Logger.Log("ParseConfig --- showLoading : " + showLoading);
                if (showLoading)
                {
                    if (!isNewPlayer)
                    {
                        ParseHallConfig();
                        ParseMatchConfig();
                    }
                    else
                    {
                        ParseMatchConfig();
                        ParseHallConfig();
                    }

                    lock (LockObject)
                    {
                        //Logger.Log("ParseConfig --- readConfigCnt : " + readConfigCnt);
                        if (loadConfigCnt > 30 && loadConfigCnt == readConfigCnt)
                        {
                            loadConfigFinish = true;
                            return;
                        }
                    }
                }
				Thread.Sleep(15);
			}
		}
		catch(System.Exception ex)
		{
            //Logger.LogError("Config parse error: " + ex.Message + "" + LogType.Exception + ex.StackTrace);
		}
    }

    public void ParsePreConfig()
    {
        //Logger.Log("this log is needed!");
        RoleShapeConfig.ReadConfig();
        BodyInfoListConfig.ReadConfig();
        AttrDataConfigData.ReadConfig();
        TeamLevelConfigData.ReadConfig();
        RoleLevelConfigData.ReadConfig();
        NPCConfigData.ReadConfig();
        PotientialEffectConfig.ReadConfig();

        GoodsConfigData.ReadConfig();

        AttrNameConfigData.ReadConfig();
        AttrReduceConfig.ReadConfig();
        RoleBaseConfigData2.ReadConfig();
        SkillConfig.ReadConfig();
        skillUpConfig.ReadConfig();
  
    }

    public void ParseHallConfig()
    {
        FashionConfig.ReadConfig();
        FashionShopConfig.ReadConfig();
        CareerConfigData.ReadConfig();
        StoreGoodsConfigData.ReadConfig();
        BaseDataBuyConfigData.ReadConfig();
        TaskConfigData.ReadConfig();
        AwardPackConfigData.ReadConfig();
        FunctionConditionConfig.ReadConfig();
        VipPrivilegeConfig.ReadConfig();
        pushConfig.ReadConfig();
        LotteryConfig.ReadConfig();
        RankConfig.ReadConfig();
        signConfig.ReadConfig();
        NewComerSignConfig.ReadConfig();
        activityConfig.ReadConfig();
        trialConfig.ReadConfig();
        BadgeSlotsConfig.ReadConfig();
        GoodsComposeNewConfigData.ReadConfig();
    }

    public void ParseMatchConfig()
    {
        PractiseConfig.ReadConfig();
        PracticePveConfig.ReadConfig();
        PractiseStepConfig.ReadConfig();
        GameModeConfig.ReadConfig();
        SceneConfig.ReadConfig();
        MatchAchievementConfig.ReadConfig();
        ArticleStrengthConfig.ReadConfig();
        PVPPointConfig.ReadConfig();
        ReboundAttrConfigData.ReadConfig();
        WinningStreakAwardConfig.ReadConfig();
        PhRegainConfig.ReadConfig();
        SpecialActionConfig.ParseConfig();
        StealConfig.ReadConfig();
        CurveRateConfig.ReadConfig();
        DunkRateConfig.ReadConfig();
        AIConfig.ReadConfig();
        qualifyingConfig.ReadConfig();
		qualifyingNewConfig.ReadConfig();
		qualifyingNewerConfig.ReadConfig();
        presentHpConfigData.ReadConfig();
        bullFightConfig.ReadConfig();
        MapConfig.ReadConfig();
        gameMatchConfig.ReadConfig();
        shootSolutionManager.ReadConfig();
		matchSoundConfig.ReadConfig();
		matchMsgConfig.ReadConfig();
        MatchPointsConfig.ReadConfig();
        AnimationSampleManager.Instance.ReadConfig();
    }

	public void LateUpdate()
	{
		mClient.LateUpdate();
	}

	public void FixedUpdate()
	{
		_Time = _Time + Time.fixedDeltaTime;
		LuaMgr.Instance.FixedUpdate();

		if (mNetworkManager != null)
		    mNetworkManager.FixedUpdate(Time.fixedDeltaTime);

        mClient.FixedUpdate();
	}
	
	public void Update()
    {
        if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Escape)))
        {
#if ANDROID_SDK
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("PressBack");
#else
            Application.Quit();
#endif


        }
        //// Home键
        //if (Application.platform == RuntimePlatform.Android && (Input.GetKeyDown(KeyCode.Home)))
        //{
        //    Application.Quit();
        //}

        /* 下面的处理顺序非常重要，关系到PVE的操作响应延迟，慎重调整
        *  输入 -> 发送输入消息 -> 虚拟服务器处理输入消息 -> 虚拟服务器更新（发送同步帧） -> 再处理消息 -> 客户端逻辑更新
        *  保证虚拟连接消息的发送和接收在一帧内完成，不会这帧发送下一帧才收到。
        *  响应延迟应当被限制在一个同步帧周期内
        */

        //读取输入----------------
        //刷新按键状态（非移动平台）
        if (!mClient.mUIManager.IsUIActived())
            mClient.mInputManager.Update();
        else
            mClient.mInputManager.Reset();

        GameMatch match = mClient.mCurMatch;
        if (match != null)
        {
            //刷新按键状态（移动平台）
            if (match.m_uiController != null)
                match.m_uiController.UpdateBtnState();
            //转换按键状态为方向操作和命令，并发送服务器
            InputReader.Instance.Update(match);
        }
        //-----------------------

		LuaScriptMgr.Instance.Update();
        mVDebug.Update();

        //处理消息（包括虚拟服务器消息处理）
		if (mNetworkManager != null)
			mNetworkManager.Update(Time.deltaTime);

        //更新虚拟服务器（如果发送了消息，再更新一次虚拟连接，保证一帧之内客户端收到回复）
        VirtualGameServer.Instance.Update(Time.deltaTime);

        //客户端更新（驱动比赛逻辑层）
        mClient.Update();

        if (_isOpenHeartbeatMsg)
        {
            if (Time.time - _currentIntervalTime >= MaxIntervalTime)
            {
                _currentIntervalTime = Time.time;
                _currentIntervalCount++;
            }

            if (_currentIntervalCount >= MaxIntervalCount)
            {
                //NetWorkModule.Instance.ShowOffNetUI();
                _currentIntervalCount = 0;
                _isOpenHeartbeatMsg = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape) && !mRevLogoutOnce)
        {
            mRevLogoutOnce = true;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
#if ANDROID_SDK

#else
			Application.Quit();
#endif
#endif
        }
    }

    public void ReceiveHeartbeatMsg()
    {
        _currentIntervalTime = Time.time;
        _currentIntervalCount = 0;
    }

    public void SendHeartbeatMsg()
    {
        //_currentIntervalTime = Time.time;
        _isOpenHeartbeatMsg = true;
    }

    public void DebugDraw()
    {
        if (mClient == null || mClient.mCurMatch == null)
            return;

        if (mClient.mCurMatch.mCurScene != null)
            mClient.mCurMatch.mCurScene.OnDrawGizmos();
    }

    public void Exit()
    {
        if (mClient != null)
            mClient.Exit();
        if (mNetworkManager != null)
            mNetworkManager.Exit();
        if (mEngineFramework != null)
            GameObject.DestroyObject(mEngineFramework);
#if IOS_SDK || ANDROID_SDK
        if( GlobalConst.IS_ENABLE_TALKING_DATA )
        {
            TalkingDataGA.OnEnd();
        }
#endif

        _instance = null;
    }
}
