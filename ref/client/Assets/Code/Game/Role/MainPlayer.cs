using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;
using fogs.proto.msg;
using fogs.proto.config;
using LuaInterface;
using System.Runtime.InteropServices;

public class MainPlayer : Singleton<MainPlayer>
{
    public bool _isInitialized = false;

    public MainPlayer()
    {
        Initialize();
    }

    public void Initialize()
    {
        System.Timers.Timer t = new System.Timers.Timer(1000);
        t.Elapsed += new System.Timers.ElapsedEventHandler(UpdateEverySecond);
        t.Enabled = true;
        t.AutoReset = true;
    }

    public void Uninitialize()
    {
        FriendData.Instance.Init();

        AllGoodsList.Clear();
        BadgeGoodsList.Clear();
        this.OnAllGoodsListChanged();

        FightRoleList.Clear();
        RoleGoodsList.Clear();
        SkillGoodsList.Clear();
        FavoriteGoodsList.Clear();
        ConsumeGoodsList.Clear();
        //TattooGoodsList.Clear();
        EquipmentGoodsList.Clear();
        //EquipmentPieceList.Clear();
        FashionGoodsList.Clear();
        TrainingList.Clear();
        MaterialList.Clear();
        ChapterList.Clear();
        PlayerList.Clear();
        TrainingInfoList.Clear();
        CaptainInfos.Clear();
        PractiseInfo.Clear();
        GuideInfo.Clear();
		SquadInfo.Clear();
		EquipInfo.Clear();
		ExerciseInfos.Clear();
		ShootGameModeInfo.Clear();
		VipGifts.Clear();
		VipExpGoodsBuyInfo.Clear();
		TrainingInfoList.Clear();
		StoreRefreshTimes.Clear();

		_isInitialized = false;
    }

    public delegate void DataChangedDelegate(DataInfo.DataType type);
    public DataChangedDelegate onDataChanged;
    private Dictionary<string, DataChangedDelegate> cachedOnDataChanged = new Dictionary<string, DataChangedDelegate>();
    public void AddDataChangedDelegate(LuaFunction func, string uiName)
    {
        DataChangedDelegate handler = (type) =>
        {
            func.Call(type);
        };
        cachedOnDataChanged[uiName] = handler;
        onDataChanged += handler;
    }
    public void RemoveDataChangedDelegate(string uiName)
    {
        if (cachedOnDataChanged.ContainsKey(uiName))
        {
            DataChangedDelegate handler = cachedOnDataChanged[uiName];
            if (handler != null)
                onDataChanged -= handler;
            cachedOnDataChanged.Remove(uiName);
        }
        else
        {
            Logger.LogWarning("Remove delegate faile with name " + uiName);
        }
    }

    //------------------------------------------------------------------
    //------------------------基础信息
    //账号ID
    private uint _accountID;

    public uint AccountID
    {
        get { return _accountID; }
        set
        {
            _accountID = value;
        }
    }
    //名称
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.NAME);
        }
    }
    //等级
    public uint prev_level { get; private set; }
    private uint _level;
    public uint Level
    {
        get { return _level; }
        set
        {
            prev_level = _level;
            _level = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.LEVEL);
#if IOS_SDK || ANDROID_SDK
           if (TdAccount != null && GlobalConst.IS_ENABLE_TALKING_DATA )
            {
                TdAccount.SetLevel((int)_level);
            }
#endif
            
        }
    }
    //经验
    private uint _exp;
    public uint Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.EXP);
        }
    }
    //金币
    private uint _gold;
    public uint Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.GOLD);
        }
    }
    //钻石（赠送）
    private uint _diamondFree;
    public uint DiamondFree
    {
        get { return _diamondFree; }
        set
        {
            _diamondFree = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.DIAMOND_FREE);
        }
    }
    //钻石（充值）
    private uint _diamondBuy;
    public uint DiamondBuy
    {
        get { return _diamondBuy; }
        set
        {
            _diamondBuy = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.DIAMOND_BUY);
        }
    }
    //荣誉
    private uint _honor;
    public uint Honor
    {
        get { return _honor; }
        set
        {
            _honor = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.HONOR);
        }
    }
    //荣誉2
    private uint _honor2;
    public uint Honor2
    {
        get { return _honor2; }
        set
        {
            _honor2 = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.HONOR2);
        }
    }
    //威望
    private uint _prestige;
    public uint Prestige
    {
        get { return _prestige; }
        set
        {
            _prestige = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.PRESTIGE);
        }
    }
    //威望2
    private uint _prestige2;
    public uint Prestige2
    {
        get { return _prestige2; }
        set
        {
            _prestige2 = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.PRESTIGE2);
        }
    }
    //声望
    private uint _reputation;
    public uint Reputation 
    {
        get { return _reputation; }
        set
        {
            _reputation = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.REPUTATION);
        }
    }
    //体力
    private uint _prev_hp;
    public uint prev_hp
    {
        get { return _prev_hp; }
        private set { _prev_hp = value; }
    }
    private uint _hp;
    public uint Hp
    {
        get { return _hp; }
        set
        {
            prev_hp = _hp;
            _hp = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.HP);

#if ANDROID_SDK
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

                int hhp = (int)Hp;
                //Logger.Log("MainPlayer hhp=" + hhp);
                PushConfig pushConfig = GameSystem.Instance.pushConfig;
                string date = string.Format("{0}", hhp);
                //Logger.Log("MainPlayer date=" + date);
                string maxHp = GameSystem.Instance.TeamLevelConfigData.GetMaxHP(MainPlayer.Instance.Level).ToString();
                object[] datas = new object[] { pushConfig._hpId, 4, date, maxHp,0, pushConfig._hpStr };
                // object[] datas = new object[] { id, type, item.date, item.time, online, item.content };
                //Logger.Log("MainPlayer Call RecvPush1=");
                if( pushConfig._hpStr != null && pushConfig._hpStr.CompareTo("") != 0 )
                {
                    jo.Call("RecvPush", datas);
                }
                //Logger.Log("MainPlayer Call RecvPush2=");
            }
#endif

#if IOS_SDK
            PushConfig pushConfig = GameSystem.Instance.pushConfig;
            string date = string.Format("{0}", Hp);
            string maxHp = GameSystem.Instance.TeamLevelConfigData.GetMaxHP(MainPlayer.Instance.Level).ToString();
            if (pushConfig._hpStr != null && pushConfig._hpStr.CompareTo("") != 0)
            {
                GameSystem.Instance.pushConfig.RecvPushWrapper(pushConfig._hpId, 4, date, maxHp, 0, pushConfig._hpStr);
            }
#endif
        }
    }
    //发送队伍战力标记
    private bool isSendFightPower = false;
    //球员战力记录
    private Dictionary<uint, IM.Number> RoleFightPower = new Dictionary<uint, IM.Number>();
    //阵容战力记录
    private IM.Number SquadFightPower;

    //账号是否有效（0：无效，1：有效）
    //private uint _valid;

    //上次下线时间（os.time()）
    //private uint _logout_time;

    //队伍图标
    private string _icon;
    public string Icon
    {
        get { return _icon; }
        set
        {
            _icon = value;
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.ICON);
        }
    }
    //队长ID
    private uint _captainID;
	public Player Captain{ set{} get{ return GetRole(_captainID);} }
    public uint CaptainID
    {
        get { return _captainID; }
        set
        {
            _captainID = value;
            //CreateCaptainObj();
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.CAPTAIN);
        }
    }
    //VIP经验值
    private uint _vipExp;
    public uint VipExp
    {
        get { return _vipExp; }
        set
        {
            _vipExp = value;
			uint i = 0;
			while(true)
			{
				VipData vipData = GameSystem.Instance.VipPrivilegeConfig.GetVipData(i);
				if( vipData != null )
				{
					uint consume = vipData.consume;
					if( consume <= value )
					{
						Vip = i;
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
				i++;
			}

			
            if (_isInitialized && onDataChanged != null)
                onDataChanged(DataInfo.DataType.VIP);
        }
    }
    //活跃度
    public ActivityInfo activityInfo;
    // VIP Recharge.
    public List<uint> VipRechargeList = new List<uint>();
    public Dictionary<uint, uint> VipExpGoodsBuyInfo = new Dictionary<uint, uint>();

    //-----ExtroInfo -- begin
    //队长列表
    public List<RoleInfo> CaptainInfos = new List<RoleInfo>();
    
	//物品列表
    public Dictionary<ulong, Goods> AllGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> RoleGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> SkillGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> FavoriteGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> ConsumeGoodsList = new Dictionary<ulong, Goods>();
    //public Dictionary<ulong, Goods> TattooGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> EquipmentGoodsList = new Dictionary<ulong, Goods>();
    //public Dictionary<ulong, Goods> EquipmentPieceList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> FashionGoodsList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> TrainingList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> MaterialList = new Dictionary<ulong, Goods>();
    public Dictionary<ulong, Goods> BadgeGoodsList = new Dictionary<ulong, Goods>();
    //出战信息
    public Dictionary<fogs.proto.msg.GameMode, List<fogs.proto.msg.FightRole>> FightRoleList = new Dictionary<fogs.proto.msg.GameMode, List<fogs.proto.msg.FightRole>>();
    //生涯信息
    public Dictionary<uint, Chapter> ChapterList = new Dictionary<uint, Chapter>();
    //商店信息
    // -- 每次打开商店是从服务器及时获取信息
    //上一次体力回复时间
    public DateTime HpRestoreTime;
    //体力购买次数
    public uint HpBuyTimes;
    //金币购买次数
    public uint GoldBuyTimes;
    //任务信息
    public TaskInfo taskInfo;
    //签到信息
    public SignInfo signInfo;
    //操作练习数据
    private Dictionary<uint, bool> PractiseInfo = new Dictionary<uint, bool>();
    //等级奖励
    public Dictionary<uint, uint> playerLevelAwardInfo = new Dictionary<uint, uint>();
    //中午免费体力领取状态
    public uint MoonHp;
    //晚上免费体力领取状态
    public uint EvenHp;
    //第三次免费体力领取状态
    public uint ThirdHp;
    //抽奖信息
    public LotteryInfo LotteryInfo;
    //排位赛信息
    public QualifyingInfo QualifyingInfo;
    //斗牛比赛
    public BullFight BullFight;
    public int BullFightHard
    {
        get { return PlayerPrefs.GetInt("BullFightHard", 1); }
        set { PlayerPrefs.SetInt("BullFightHard", value); }
    }

    //投篮赛
    public List<fogs.proto.msg.GameModeInfo> ShootGameModeInfo = new List<fogs.proto.msg.GameModeInfo>();
    public ShootInfo ShootInfo;
    public int MassBallHard
    {
        get
        {
            int getValue = PlayerPrefs.GetInt("MassBallHard", 1);
            return getValue;
        }
        set { PlayerPrefs.SetInt("MassBallHard", value); }
    }

    public int GrabZoneHard
    {
        get { return PlayerPrefs.GetInt("GrabZoneHard", 1); }
        set { PlayerPrefs.SetInt("GrabZOneHard", value); }
    }

    public int GrabPointHard
    {
        get { return PlayerPrefs.GetInt("GrabPointHard", 1); }
        set { PlayerPrefs.SetInt("GrabPointHard", value); }
    }

    // 排位赛(新版)
    public QualifyingNewerInfo QualifyingNewerInfo;
    public uint QualifyingNewerScore;
    public uint QualifyingNewerTime; 




    //阵容
    public List<fogs.proto.msg.FightRole> SquadInfo = new List<fogs.proto.msg.FightRole>();
    //装备
    public List<fogs.proto.msg.EquipInfo> EquipInfo = new List<fogs.proto.msg.EquipInfo>();
    //训练信息
    public Dictionary<uint, List<fogs.proto.msg.ExerciseInfo>> ExerciseInfos = new Dictionary<uint, List<fogs.proto.msg.ExerciseInfo>>();

    //-----ExtroInfo -- end


    //参赛信息
    public RaceInfo RaceInfo;
    //常规竞技赛当前比赛积分
    public uint CurScore;
    //常规竞技赛上一轮比赛积分
    public uint LastScore;
    //常规竞技赛连胜次数
    public uint WinningStreak;
    //排行积分变化的时间

    //排位赛排名信息
    public uint QualifyingRanking;
    //巡回赛npc
	public TourInfo tourInfo;
    //当前巡回赛关卡
    public uint CurTourID;
    //巡回赛最高记录
    public uint MaxTourID;
    //巡回赛重置次数
    public uint TourResetTimes;
    //巡回赛失败次数
    public uint TourFailTimes;
    //游戏指引信息
    private Dictionary<uint, bool> GuideInfo = new Dictionary<uint, bool>();
    //VIP信息
    public List<uint> VipGifts = new List<uint>();
    //创建步骤
    public uint CreateStep;
    //创建时间
    public uint CreateTime;
    //新用户签到
    public NewComerInfo NewComerSign;
    //1v1plusInfo
    public PvpPlusInfo PvpPlusInfo;
    //-----other data -- begin
    //邮件信息
    public List<MailInfo> MailList;

    //球员对象列表
    public List<Player> PlayerList = new List<Player>();

    //训练信息
    public List<TrainingInfo> TrainingInfoList = new List<TrainingInfo>();

    //商店刷新次数信息
    public List<uint> StoreRefreshTimes = new List<uint>();
    
    //练习赛cd
    public uint practice_cd;
    //新手试炼
//    public List<NewComerTrial> newComerTrialInfo;
    public uint trialFlag;
    public uint trialTotalScore;

    // 天梯.
    public PvpLadderInfo pvpLadderInfo;
    public uint pvpLadderScore;
    public bool inPvpJoining = false;

    public uint Vip = 0;

    public uint PvpRunTimes
    {
        get { return RaceInfo != null ? RaceInfo.run_times : 0; }
        set { if (RaceInfo != null) RaceInfo.run_times = value; }
    }
    public uint PvpPointBuyTimes
    {
        get { return RaceInfo != null ? RaceInfo.reset_times : 0; }
        set { if (RaceInfo != null) RaceInfo.reset_times = value; }
    }

    public bool IsLastShootGame;

    public List<uint> BullFightNpc = new List<uint>();

	public PvpRegularInfo pvp_regular;
	public QualifyingNewInfo qualifying_new;
    

    //public delegate void FashionChangeDelege(Goods good);
    //public FashionChangeDelege FashionChange;

    public fogs.proto.msg.GameMode curShootGameMode;

    // 球员碎片
    public int LinkRoleId = 0;  // 默认为0.
    public int LinkExerciseId = 0;
    public bool LinkExerciseLeft = true;
    public int LinkTab = 0;
    public string LinkUiName;
    public bool LinkEnable = false;

	//新的一天来临(12点)
	public System.Action onMidNightCome;

    //公告
    public Action onAnnounce;
    public List<string> AnnouncementList = new List<string>();
    //世界聊天
    public Action onNewChatMessage;
    public List<int> ChatWordsNum = new List<int>();
    public List<ChatBroadcast> WorldChatList = new List<ChatBroadcast>();
    //图鉴
    public List<uint> MapIDInfo = new List<uint>();
    public List<uint> NewMapIDList = new List<uint>();
    public bool InitMap = true;
    //小红点刷新监听
    public Action onCheckUpdate;

    //-----other data -- end

    //-----SDK -- begin
#if IOS_SDK || ANDROID_SDK
    TDGAAccount TdAccount;
#endif
    public bool CanGoCenter = true;
    public bool CanSwitchAccount = true;
    public bool CanLogout = true;
    public bool SDKLogin = false;

    bool _tourist = false;
    public bool Tourist
    {
        set
        {
            _tourist = value;
        }
    }

    uint _buyItemId = 0;
    public uint BuyItemId
    {
        set
        {
            _buyItemId = value;
        }
    }

    uint _buyItemNum = 0;
    public uint BuyItemNum
    {
        set
        {
            _buyItemNum = value;
        }
    }

    uint _buyItemCost = 0;
    public uint BuyItemCost
    {
        set
        {
            _buyItemCost = value;
        }
    }

    string _zqServerId;
    public string ZQServerId
    {
        get
        {
            return _zqServerId;
        }
        set
        {
            _zqServerId = value;
        }
    }

    //-----SDK -- End




    //----------------------------------------------------
    //---------- 设置MainPlayer对象数据
    public void SetBaseInfo(PlayerInfo info)
    {
        Uninitialize();

        if (AccountID != info.acc_id)
        {
            Logger.Log("Error -- SetBaseInfo error occurs: " + AccountID + " -- " + info.acc_id);
            AccountID = info.acc_id;
        }

        Name = info.name;
        Level = info.level;
        Exp = info.exp;
        Gold = info.gold;
        DiamondFree = info.diamond_free;
        DiamondBuy = info.diamond_buy;
        Honor = info.honor;
        Honor2 = info.honor2;
        Prestige = info.prestige;
        Prestige2 = info.prestige2;
        Reputation = info.reputation;
        Hp = info.hp;
        MoonHp = info.extra_info.noon_hp;
        EvenHp = info.extra_info.even_hp;
        ThirdHp = info.extra_info.three_times_hp;
        taskInfo = info.extra_info.tasks;
        signInfo = info.extra_info.tasks.sign_info;
        CreateTime = info.create_time;
        PvpPlusInfo = info.pvp_plus;
        activityInfo = info.activity_info;
//        newComerTrialInfo = info.new_comer_trial_info.info;
        trialFlag = info.new_comer_trial_info.awards_flag;
        trialTotalScore = info.new_comer_trial_info.total_score;
		pvp_regular = info.pvp_regular;
		qualifying_new = info.qualifying_new;


        if (info.badge_info != null)
        {
            badgeSystemInfo.InitBadgeBookData(info.badge_info.badge_book);
        }

		//球员
		if (info.extra_info.roles != null)
		{
			foreach (RoleInfo roleInfo in info.extra_info.roles)
			{
				Player role = new Player(roleInfo, new Team(Team.Side.eNone));
				PlayerList.Add(role);
			}
		}

        CaptainID = info.captain;
        NewComerSign = info.new_comer_info;

        for (int storeid = 0; storeid < info.extra_info.store_info.Count; storeid++)
        {
            StoreRefreshTimes.Add(info.extra_info.store_info[storeid].refresh_request);
        }

        foreach (StoreGoodsInfo storeGoods in info.extra_info.store_info[(int)fogs.proto.msg.StoreType.ST_EXP -1].goods_list)
        {
            if (GameSystem.Instance.StoreGoodsConfigData.GetStoreGoodsData((uint)fogs.proto.msg.StoreType.ST_EXP, storeGoods.id) != null)
            {
                if (!VipExpGoodsBuyInfo.ContainsKey(storeGoods.id))
                {
                    VipExpGoodsBuyInfo.Add(storeGoods.id, storeGoods.sell_out);
                }
                else
                {
                    Logger.LogError("SetBaseInfo VipExpGoodsBuyInfo already has key =" + storeGoods.id);
                }
            }
        }
        HpBuyTimes = info.extra_info.hp_buy_times;
        GoldBuyTimes = info.extra_info.gold_buy_times;
        CurScore = info.cur_regular_points;
        LastScore = info.last_regular_points;
        WinningStreak = info.regular_winning_streak;
        CaptainInfos = info.extra_info.captains;
  

        BullFight = info.extra_info.bull_fight;
        ShootInfo = info.extra_info.shoot;

        CurTourID = info.cur_tour_id;
        MaxTourID = info.max_tour_id;
        TourFailTimes = info.tour_fail_times;
        TourResetTimes = info.tour_reset_times;
        QualifyingRanking = info.qualifying_ranking;

        for (int i = 0; i < info.extra_info.fight_roles.Count; ++i)
        {
            FightRoleInfo fightInfo = info.extra_info.fight_roles[i];
            FightRoleList[fightInfo.game_mode] = fightInfo.fighters;
        }

        //物品列表
        for (int i = 0; i < info.extra_info.goods.Count; ++i)
        {
            for (int j = 0; j < info.extra_info.goods[i].item.Count; ++j)
            {
                Goods goods = new Goods();
                goods.Init(info.extra_info.goods[i].item[j]);
				if (goods.GetCategory() == GoodsCategory.GC_FAVORITE
				    || goods.GetCategory() == GoodsCategory.GC_CONSUME
				    || goods.GetCategory() == GoodsCategory.GC_EQUIPMENT
				    || goods.GetCategory() == GoodsCategory.GC_BADGE)
                {
                    AllGoodsList.Add(goods.GetUUID(), goods);
                    this.OnAllGoodsListChanged();
                }

                if (goods.GetCategory() == GoodsCategory.GC_ROLE)
                    RoleGoodsList.Add(goods.GetUUID(), goods);
                else if (goods.GetCategory() == GoodsCategory.GC_SKILL)
                    SkillGoodsList.Add(goods.GetUUID(), goods);
                else if (goods.GetCategory() == GoodsCategory.GC_FAVORITE)
                    FavoriteGoodsList.Add(goods.GetUUID(), goods);
                else if (goods.GetCategory() == GoodsCategory.GC_CONSUME)
                    ConsumeGoodsList.Add(goods.GetUUID(), goods);
                //else if (goods.GetCategory() == GoodsCategory.GC_TATTOO)
                //    TattooGoodsList.Add(goods.GetUUID(), goods);
                else if (goods.GetCategory() == GoodsCategory.GC_EQUIPMENT)
                    EquipmentGoodsList.Add(goods.GetUUID(), goods);
                //else if (goods.GetCategory() == GoodsCategory.GC_EQUIPMENTPIECE)
                //    EquipmentPieceList.Add(goods.GetUUID(), goods);
                else if (goods.GetCategory() == GoodsCategory.GC_FASHION)
                {
                    FashionGoodsList.Add(goods.GetUUID(), goods);


                    string debugInfo = string.Format("base:fid={0},eqed={1},lft={2},uuid={3}",
                                                     goods.GetID(),
                                                     goods.IsEquip(),
                                                     goods.getTimeLeft(),
                                                     goods.GetUUID()
                    );

                    Logger.Log(debugInfo);
                }
                else if (goods.GetCategory() == GoodsCategory.GC_EXERCISE)
                {
                    TrainingList.Add(goods.GetUUID(), goods);
                    Logger.Log("get training good=" + goods.GetID());
                }
                else if (goods.GetCategory() == GoodsCategory.GC_MATERIAL)
                {
                    MaterialList.Add(goods.GetUUID(), goods);
                    Logger.Log("get material good=" + goods.GetID());
                }
                //涂鸦goods数据
                else if (goods.GetCategory() == GoodsCategory.GC_BADGE)
                {
                    BadgeGoodsList.Add(goods.GetUUID(), goods);
                }
            }
        }

        //生涯
        if (info.extra_info.chapters != null)
        {
            foreach (ChapterProto data in info.extra_info.chapters)
            {
                if (ChapterList.ContainsKey(data.id) == false)
                {
                    Chapter chapter = new Chapter();
                    chapter.Init(data, AccountID);
                    ChapterList[data.id] = chapter;
                }
            }
        }

        RaceInfo = info.race_info;

        //排位赛
        if (info.extra_info.qualifying != null)
        {
            QualifyingInfo = info.extra_info.qualifying;
        }
        if (info.extra_info.practice != null)
        {
            foreach (PracticeInfo practice in info.extra_info.practice)
            {
                if (PractiseInfo.ContainsKey(practice.id))
                    PractiseInfo[practice.id] = (practice.finished != 0);
                else
                    PractiseInfo.Add(practice.id, practice.finished != 0);
            }
        }

        // 等级奖励
        if (info.extra_info.level_award_state != null)
        {
            foreach (var item in info.extra_info.level_award_state)
            {
                if (playerLevelAwardInfo.ContainsKey(item.id))
                    playerLevelAwardInfo[item.id] = item.value;
                else
                    playerLevelAwardInfo.Add(item.id, item.value);
            }
        }

        // 排位赛(新版)
        if ( info.qualifying_newer != null )
        {
            QualifyingNewerInfo = info.qualifying_newer;
        }

        Logger.Log("1927 QualifyingNewerInfo.ranking" + QualifyingNewerInfo.ranking);

        QualifyingNewerScore = info.qualifying_newer_score;
        QualifyingNewerTime = info.qualifying_newer_time;

        if (info.guide_info != null)
        {
            foreach (KeyValueData data in info.guide_info.info)
            {
                GuideInfo.Add(data.id, data.value != 0);
            }
        }

        LotteryInfo = info.extra_info.lottery;

        //阵容
        foreach (FightRole squad in info.extra_info.squad)
        {
            SquadInfo.Add(squad);
        }
        //巡回赛npc
		tourInfo = info.extra_info.tour;
        //装备： 此调用必须在设置完Goods之后操作.
        foreach(EquipInfo equip in info.extra_info.equipments)
        {
            foreach( fogs.proto.msg.EquipmentSlot slot in equip.slot_info )
            {
                if (slot.equipment_id == 0 && slot.equipment_uuid != 0)
                {
                    Goods goods = GetGoods(GoodsCategory.GC_EQUIPMENT, slot.equipment_uuid);
                    slot.equipment_id = goods.GetID();
                    slot.equipment_level = goods.GetLevel();
                }
            }
            EquipInfo.Add(equip);
        }
        foreach (RoleInfo role in info.extra_info.roles)
        {
            List<ExerciseInfo> exinfo = new List<fogs.proto.msg.ExerciseInfo>();
            foreach (ExerciseInfo exercise in role.exercise)
            {
                exinfo.Add(exercise);
            }
            ExerciseInfos[role.id] = exinfo;
        }

        // vip
        VipGifts.Clear();
        foreach (giftInfo item in info.vip.gift)
        {
            VipGifts.Add(item.level);
        }
        VipExp = info.vip.exp;
        VipRechargeList = info.vip.recharge_rec;
        practice_cd = info.extra_info.practice_cd;
        //战力
        if (info.fight_power == 0)
        {
            SendFightPowerChange();
        }

        pvpLadderInfo = info.pvp_ladder;
        pvpLadderScore = info.pvp_ladder_score;
        _isInitialized = true;
    }



    //----------------------------------------------------
    //---------- 系统更新操作
    //
    public void UpdateEverySecond(object source, System.Timers.ElapsedEventArgs e)
    {
        foreach (KeyValuePair<ulong, Goods> kv in FashionGoodsList)
        {
            Goods good = kv.Value;

            if (good.isUsed() && good.getTimeLeft() > 0)
            {
                good.decreaseTimeLeft();
            }
        }
    }

    //新的一天到来
    public void OnNewDayCome()
    {
        //更新生涯模式关卡挑战次数
        foreach (Chapter chapter in ChapterList.Values)
        {
            foreach (Section section in chapter.sections.Values)
            {
                section.challenge_times = 0;
            }
        }
        /*
        if (GameSystem.Instance.mClient.mUIManager.CareerCtrl != null)
            GameSystem.Instance.mClient.mUIManager.CareerCtrl.RefreshRemainTimes();
        */

        //荣誉争霸赛次数更新
        PvpRunTimes = 0;
        PvpPointBuyTimes = 0;
        //街头巡回赛次数更新
        TourResetTimes = 0;
        //巡回赛失败次数
        TourFailTimes = 0;
        //商店刷新次数信息
        for (int i = 0; i < StoreRefreshTimes.Count; ++i)
        {
            StoreRefreshTimes[i] = 0;
        }
        //金币购买次数更新
        GoldBuyTimes = 0;
        //体力购买次数更新
        HpBuyTimes = 0;
        //中午免费体力领取状态
        MoonHp = 0;
        //晚上免费体力领取状态
        EvenHp = 0;
        //第三次免费体力领取状态
        ThirdHp = 0;
        //抽奖信息
        //LotteryInfo.free_times1 = GameSystem.Instance.CommonConfig.GetUInt("gNormalLotteryFreeTimes");
        //LotteryInfo.free_times2 = GameSystem.Instance.CommonConfig.GetUInt("gSpecialLotteryFreeTimes");
        //排位赛信息
        QualifyingInfo.run_times = 0;
        QualifyingInfo.buy_times = 0;
        //斗牛比赛
        BullFight.times = 0;
        BullFight.bullfight_buy_times = 0;
        //投篮赛
        ShootInfo.mass_ball_times = 0;
        ShootInfo.grab_point_times = 0;
        ShootInfo.grab_zone_times = 0;
        //练习赛完成状态重置
		List<uint> keys = new List<uint>();
        foreach (KeyValuePair<uint,bool> info in PractiseInfo)
        {
			keys.Add(info.Key);
        }
		foreach (uint key in keys)
		{
            PractiseInfo[key] = false;
		}
        //活跃度清零
        activityInfo.activity = 0;
        for (int i = 0; i < activityInfo.gift.Count; ++i)
        {
            activityInfo.gift[i] = 1;
        }

        FriendData.Instance.Init();
    }
    


    //----------------------------------------------------
    //---------- 队长、队员、球员系统相关操作
    //创建队长角色
    public void CreateCaptainObj()
    {
		//Captain = GetRole(_captainID);
		//if( Captain == null )
		//	Logger.LogError("xxx");
    }

    public bool HasRole(uint roleId)
    {
		return PlayerList.Find( (Player player)=>{ return player.m_id == roleId; }) != null;
    }

    public Player GetRole(uint roleID)
    {
        for (int i = 0; i < PlayerList.Count; ++i)
        {
            if (PlayerList[i].m_roleInfo.id == roleID)
                return PlayerList[i];
        }
        return null;
    }

    public RoleInfo GetRole2(uint roleId)
    {
		Player target = PlayerList.Find( (Player player)=>{return player.m_id == roleId;} );
		if( target == null )
			return null;
		return target.m_roleInfo;
    }

    public List<uint> GetRoleIDList()
    {
        List<uint> roleIDList = new List<uint>();
        for (int i = 0; i < PlayerList.Count; ++i)
        {
            roleIDList.Add(PlayerList[i].m_roleInfo.id);
        }
        return roleIDList;
    }

    public void SetRoleInfo(RoleInfo info)
    {
        for (int i = 0; i < PlayerList.Count; ++i)
        {
            if (PlayerList[i].m_roleInfo.id == info.id)
            {
				PlayerList[i].m_roleInfo = info;
                break;
            }
        }
    }

	public List<fogs.proto.msg.FightRole> GetFightRoleList(fogs.proto.msg.GameMode mode)
	{
		if (FightRoleList.ContainsKey(mode))
		{
			return FightRoleList[mode];
		}
		return null;
	}

    public void SetRoleQuality(uint roleId, uint newQulaity)
    {
        int[] array = { 2, 4, 7, 11 };
        //List<int> skills = GameSystem.Instance.qualityAttrConfig.GetSkills(roleId);

        for (int i = 0; i < PlayerList.Count; ++i)
        {
            if (PlayerList[i].m_roleInfo.id == roleId)
            {
                PlayerList[i].m_roleInfo.quality = newQulaity;
   
                // m_roleInfo is read only.
				/*
                for (int j = 0; j < 4; j++)
                {
                    if (array[j] == newQulaity )
                    {
                        int skillId = skills[j];
                        // skill slot is unsort.
                        for (int s = 0; s < PlayerList[i].m_roleInfo.skill_slot_info.Count; s++)
                        {
                            if (PlayerList[i].m_roleInfo.skill_slot_info[s].id == (uint)skillId)
                            {
                                PlayerList[i].m_roleInfo.skill_slot_info[s].is_unlock = 1;
                                break;
                            }
                        } 
                    }             
                }
                */
                break;
            }
        }
    }

    public void SetRoleLvAndExp(uint roleId, uint level, uint exp)
    {
		Player player = GetRole(roleId);
		if( player == null )
		{
			Logger.LogError("can not find player: " + roleId);
			return;
		}
		player.m_roleInfo.level = level;
		player.m_roleInfo.exp = exp;
    }

    //切换队长处理
    public void SwitchCaptain(uint newCaptainID)
    {
        RoleInfo captain = GetCaptainInfo(CaptainID);
        //卸载已装备的技能
        for (int i = 0; i < captain.skill_slot_info.Count; ++i)
        {
            if (captain.skill_slot_info[i].id >= 2000 && captain.skill_slot_info[i].skill_uuid != 0)
            {
                Goods goods = GetGoods(GoodsCategory.GC_SKILL, captain.skill_slot_info[i].skill_uuid);
                if (goods != null)
                {
                    goods.Unequip();
                }
                captain.skill_slot_info[i].skill_uuid = 0;
            }
        }
        //卸载已装备的纹身
        //for (int i = 0; i < captain.tattoo_slot_info.Count; ++i)
        //{
        //	if (captain.tattoo_slot_info[i].tattoo_uuid != 0)
        //	{
        //		Goods goods = GetGoods(GoodsCategory.GC_TATTOO, captain.tattoo_slot_info[i].tattoo_uuid);
        //		if (goods != null)
        //		{
        //			goods.Unequip();
        //		}
        //		captain.tattoo_slot_info[i].tattoo_uuid = 0;
        //	}
        //}

        // get rid of the fashion.
        foreach (KeyValuePair<ulong, Goods> kv in FashionGoodsList)
        {
            Goods goods = kv.Value;
            if (goods.IsEquip())
            {
                goods.Unequip();
            }
        }
        CaptainID = newCaptainID;
    }

    //获取队长信息
    public RoleInfo GetCaptainInfo(uint captainID)
    {
        foreach (Player player in PlayerList)
        {
            if (player.m_roleInfo.id == captainID)
                return player.m_roleInfo;
        }
        return null;
    }

    /*
    //获取队长偏好
    public CaptainBias GetCaptainBias(uint captainID)
    {
        for (int i = 0; i < CaptainInfos.Count; ++i)
        {
            if (CaptainInfos[i].id == captainID)
            {
                return (CaptainBias)CaptainInfos[i].bias;
            }
        }
        return CaptainBias.CB_NONE;
        foreach (Player player in PlayerList)
        {
            if (player.m_roleInfo.id == captainID)
            {
                return (CaptainBias)player.m_roleInfo.bias;
            }
        }
        return CaptainBias.CB_NONE;
    }
     * */

    public void AddInviteRoleInList(RoleInfo info)
    {
		Player player = PlayerList.Find((Player inPlayer) =>{ return inPlayer.m_roleInfo.id == info.id; });
		if ( player == null )
        {
			player = new Player(info, new Team(Team.Side.eNone));
			PlayerList.Add(player);
            RoleInfo role = player.m_roleInfo;
            List<ExerciseInfo> exinfo = new List<ExerciseInfo>();
            foreach (ExerciseInfo exercise in role.exercise)
            {
                exinfo.Add(exercise);
            }
            ExerciseInfos[role.id] = exinfo;

            SendGoodsLog("1", info.id.ToString(), GameSystem.Instance.RoleBaseConfigData2.GetConfigData(info.id).name, 1, "add Role");
        }

		/*
        if (RoleInfos.Find((RoleInfo roleInfo) => { return roleInfo.id == info.id; }) == null )
        {
            RoleInfos.Add(info);
            SendGoodsLog("1", info.id.ToString(), GameSystem.Instance.RoleBaseConfigData2.GetConfigData(info.id).name, 1, "add Role");
        }
        */
    }



    //----------------------------------------------------
    //---------- 物品系统相关操作
    //
    //sort specified goods list
    public Dictionary<ulong, Goods> SortGoodsDesc(Dictionary<ulong, Goods> goodsList)
    {
        List<KeyValuePair<ulong, Goods>> list = new List<KeyValuePair<ulong, Goods>>(goodsList);
        list.Sort(delegate(KeyValuePair<ulong, Goods> item1, KeyValuePair<ulong, Goods> item2)
        {
            Goods itemV1 = item1.Value;
            Goods itemV2 = item2.Value;
            if (itemV1.GetQuality() == itemV2.GetQuality())
            {
                if (itemV1.GetLevel() == itemV2.GetLevel())
                {
                    return itemV1.GetID().CompareTo(itemV2.GetID());
                }
                else
                {
                    return itemV2.GetLevel().CompareTo(itemV1.GetLevel());
                }
            }
            else
            {
                return itemV2.GetQuality().CompareTo(itemV1.GetQuality());
            }
        });
        goodsList.Clear();
        foreach (KeyValuePair<ulong, Goods> pair in list)
        {
            goodsList.Add(pair.Key, pair.Value);
        }
        return goodsList;
    }

    public Dictionary<ulong, Goods> SortGoodsDescExpPriority(Dictionary<ulong, Goods> goodsList)
    {
        List<KeyValuePair<ulong, Goods>> list = new List<KeyValuePair<ulong, Goods>>(goodsList);
        list.Sort(delegate(KeyValuePair<ulong, Goods> item1, KeyValuePair<ulong, Goods> item2)
        {
            Goods itemV1 = item1.Value;
            Goods itemV2 = item2.Value;
            if (itemV1.IsInjectExp() && !itemV2.IsInjectExp())
            {
                return -1;
            }
            if (!itemV1.IsInjectExp() && itemV2.IsInjectExp())
            {
                return 1;
            }

            if (itemV1.GetQuality() == itemV2.GetQuality())
            {
                if (itemV1.GetLevel() == itemV2.GetLevel())
                {
                    return itemV1.GetID().CompareTo(itemV2.GetID());
                }
                else
                {
                    return itemV2.GetLevel().CompareTo(itemV1.GetLevel());
                }
            }
            else
            {
                return itemV2.GetQuality().CompareTo(itemV1.GetQuality());
            }
        });
        goodsList.Clear();
        foreach (KeyValuePair<ulong, Goods> pair in list)
        {
            goodsList.Add(pair.Key, pair.Value);
        }
        return goodsList;
    }

    public Dictionary<ulong, Goods> SortGoodsAsc(Dictionary<ulong, Goods> goodsList)
    {
        List<KeyValuePair<ulong, Goods>> list = new List<KeyValuePair<ulong, Goods>>(goodsList);
        list.Sort(delegate(KeyValuePair<ulong, Goods> item1, KeyValuePair<ulong, Goods> item2)
        {
            Goods itemV1 = item1.Value;
            Goods itemV2 = item2.Value;
            if (itemV1.GetQuality() == itemV2.GetQuality())
            {
                if (itemV1.GetLevel() == itemV2.GetLevel())
                {
                    return itemV1.GetID().CompareTo(itemV2.GetID());
                }
                else
                {
                    return itemV1.GetLevel().CompareTo(itemV2.GetLevel());
                }
            }
            else
            {
                return itemV1.GetQuality().CompareTo(itemV2.GetQuality());
            }
        });
        goodsList.Clear();
        foreach (KeyValuePair<ulong, Goods> pair in list)
        {
            goodsList.Add(pair.Key, pair.Value);
        }
        return goodsList;
    }

    public Goods GetGoods(GoodsCategory category, ulong uuid)
    {
        Dictionary<ulong, Goods> goodsItemList = null;
        switch (category)
        {
            case GoodsCategory.GC_ROLE:
                goodsItemList = RoleGoodsList;
                break;
            case GoodsCategory.GC_SKILL:
                goodsItemList = SkillGoodsList;
                break;
            case GoodsCategory.GC_FAVORITE:
                goodsItemList = FavoriteGoodsList;
                break;
            case GoodsCategory.GC_CONSUME:
                goodsItemList = ConsumeGoodsList;
                break;
            //case GoodsCategory.GC_TATTOO:
            //    goodsItemList = TattooGoodsList;
            //    break;
            case GoodsCategory.GC_EQUIPMENT:
                goodsItemList = EquipmentGoodsList;
                break;
            //case GoodsCategory.GC_EQUIPMENTPIECE:
            //    goodsItemList = EquipmentPieceList;
            //    break;
            case GoodsCategory.GC_FASHION:
                goodsItemList = FashionGoodsList;
                break;
            case GoodsCategory.GC_EXERCISE:
                goodsItemList = TrainingList;
                break;
            case GoodsCategory.GC_MATERIAL:
                goodsItemList = MaterialList;
                break;
            case GoodsCategory.GC_BADGE:
                goodsItemList = BadgeGoodsList;
                break;
            case GoodsCategory.GC_TOTAL:
                if (RoleGoodsList.ContainsKey(uuid))
                    return RoleGoodsList[uuid];
                else if (SkillGoodsList.ContainsKey(uuid))
                    return SkillGoodsList[uuid];
                else if (FavoriteGoodsList.ContainsKey(uuid))
                    return FavoriteGoodsList[uuid];
                else if (ConsumeGoodsList.ContainsKey(uuid))
                    return ConsumeGoodsList[uuid];
                //else if (TattooGoodsList.ContainsKey(uuid))
                //    return TattooGoodsList[uuid];
                else if (EquipmentGoodsList.ContainsKey(uuid))
                    return EquipmentGoodsList[uuid];
                //else if (EquipmentPieceList.ContainsKey(uuid))
                //    return EquipmentPieceList[uuid];
                else if (FashionGoodsList.ContainsKey(uuid))
                {
                    return FashionGoodsList[uuid];
                }
                else if (TrainingList.ContainsKey(uuid))
                {
                    return TrainingList[uuid];
                }
                else if (MaterialList.ContainsKey(uuid))
                {
                    return MaterialList[uuid];
                }
                else if (BadgeGoodsList.ContainsKey(uuid))
                {
                    return BadgeGoodsList[uuid];
                }
                break;
        }
        if (goodsItemList != null && goodsItemList.ContainsKey(uuid))
        {
            return goodsItemList[uuid];
        }
        return null;
    }

    public List<Goods> GetGoodsList(GoodsCategory category, uint goodsID)
    {
        List<Dictionary<ulong, Goods>> goodsListList = new List<Dictionary<ulong, Goods>>();
        switch (category)
        {
            case GoodsCategory.GC_ROLE:
                goodsListList.Add(RoleGoodsList);
                break;
            case GoodsCategory.GC_SKILL:
                goodsListList.Add(SkillGoodsList);
                break;
            case GoodsCategory.GC_FAVORITE:
                goodsListList.Add(FavoriteGoodsList);
                break;
            case GoodsCategory.GC_CONSUME:
                goodsListList.Add(ConsumeGoodsList);
                break;
            //case GoodsCategory.GC_TATTOO:
            //    goodsListList.Add(TattooGoodsList);
            //    break;
            case GoodsCategory.GC_EQUIPMENT:
                goodsListList.Add(EquipmentGoodsList);
                break;
            //case GoodsCategory.GC_EQUIPMENTPIECE:
            //    goodsListList.Add(EquipmentPieceList);
            //    break;
            case GoodsCategory.GC_FASHION:
                goodsListList.Add(FashionGoodsList);
                break;
            case GoodsCategory.GC_EXERCISE:
                goodsListList.Add(TrainingList);
                break;
            case GoodsCategory.GC_MATERIAL:
                goodsListList.Add(MaterialList);
                break;
            case GoodsCategory.GC_BADGE:
                goodsListList.Add(BadgeGoodsList);
                break;
            case GoodsCategory.GC_TOTAL:
                goodsListList.Add(RoleGoodsList);
                goodsListList.Add(SkillGoodsList);
                goodsListList.Add(FavoriteGoodsList);
                goodsListList.Add(ConsumeGoodsList);
                //goodsListList.Add(TattooGoodsList);
                goodsListList.Add(EquipmentGoodsList);
                //goodsListList.Add(EquipmentPieceList);
                goodsListList.Add(FashionGoodsList);
                goodsListList.Add(TrainingList);
                goodsListList.Add(MaterialList);
                goodsListList.Add(BadgeGoodsList);
                break;
        }
        List<Goods> goodsList = new List<Goods>();
        foreach (Dictionary<ulong, Goods> list in goodsListList)
        {
            foreach (KeyValuePair<ulong, Goods> goods in list)
            {
                if (goods.Value.GetID() == goodsID)
                    goodsList.Add(goods.Value);
            }
        }
        return goodsList;
    }

    public uint GetGoodsCount(uint goodsID)
    {
        if (goodsID == GlobalConst.GOLD_ID)
            return MainPlayer.Instance.Gold;
        if (goodsID == GlobalConst.DIAMOND_ID)
            return (MainPlayer.Instance.DiamondFree + MainPlayer.Instance.DiamondBuy);
        if (goodsID == GlobalConst.HONOR_ID)
            return MainPlayer.Instance.Honor;
        if (goodsID == GlobalConst.PRESTIGE_ID)
            return MainPlayer.Instance.Prestige;
        if (goodsID == GlobalConst.HONOR2_ID)
            return MainPlayer.Instance.Honor2;
        if (goodsID == GlobalConst.PRESTIGE2_ID)
            return MainPlayer.Instance.Prestige2;
        if (goodsID == GlobalConst.REPUTATION_ID)
            return MainPlayer.Instance.Reputation;

        uint count = 0;
        foreach (KeyValuePair<ulong, Goods> goods_pair in RoleGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in SkillGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in FavoriteGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in ConsumeGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        //foreach (KeyValuePair<ulong, Goods> goods_pair in TattooGoodsList)
        //{
        //    if (goods_pair.Value.GetID() == goodsID)
        //        count += goods_pair.Value.GetNum();
        //}
        foreach (KeyValuePair<ulong, Goods> goods_pair in EquipmentGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        //foreach (KeyValuePair<ulong, Goods> goods_pair in EquipmentPieceList)
        //{
        //    if (goods_pair.Value.GetID() == goodsID)
        //        count += goods_pair.Value.GetNum();
        //}
        foreach (KeyValuePair<ulong, Goods> goods_pair in FashionGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in TrainingList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in MaterialList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        foreach (KeyValuePair<ulong, Goods> goods_pair in BadgeGoodsList)
        {
            if (goods_pair.Value.GetID() == goodsID)
                count += goods_pair.Value.GetNum();
        }
        

        return count;
    }

    public void AddGoods(Goods goods)
    {
        ulong uuid = goods.GetUUID();
        uint goodsID = goods.GetID();
        Dictionary<ulong, Goods> goodsItemList = null;
        switch (goods.GetCategory())
        {
            case GoodsCategory.GC_ROLE:
                goodsItemList = RoleGoodsList;
                break;
            case GoodsCategory.GC_SKILL:
                goodsItemList = SkillGoodsList;
                break;
            case GoodsCategory.GC_FAVORITE:
                goodsItemList = FavoriteGoodsList;
                break;
            case GoodsCategory.GC_CONSUME:
                goodsItemList = ConsumeGoodsList;
                break;
            //case GoodsCategory.GC_TATTOO:
            //    goodsItemList = TattooGoodsList;
            //    break;
            case GoodsCategory.GC_EQUIPMENT:
                goodsItemList = EquipmentGoodsList;
                break;
            //case GoodsCategory.GC_EQUIPMENTPIECE:
            //    goodsItemList = EquipmentPieceList;
            //    break;
            case GoodsCategory.GC_FASHION:
                goodsItemList = FashionGoodsList;
                break;
            case GoodsCategory.GC_EXERCISE:
                goodsItemList = TrainingList;
                break;
            case GoodsCategory.GC_MATERIAL:
                goodsItemList = MaterialList;
                break;
            case GoodsCategory.GC_BADGE:
                goodsItemList = BadgeGoodsList;
                break;
			default:
				Logger.Log("Error goods category, ID: " + goods.GetID());
				break;
        }
        uint stackNum = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(goodsID).stack_num;
        if (stackNum > 0)
        {
            foreach (Goods oldGoods in goodsItemList.Values)
            {
                if (oldGoods.GetUUID() == uuid)
                {
                    uint totalNum = oldGoods.GetNum() + goods.GetNum();
                    if (totalNum > stackNum)
                    {
                        oldGoods.SetNum(stackNum);
                    }
                    else
                    {
                        oldGoods.SetNum(totalNum);
                    }
                    return;
                }
            }
        }
        if (goodsItemList.ContainsKey(uuid))
        {
            return;
        }
        else
        {
            goodsItemList[uuid] = goods;
			if (goods.GetCategory() == GoodsCategory.GC_FAVORITE
			    || goods.GetCategory() == GoodsCategory.GC_CONSUME
			    || goods.GetCategory() == GoodsCategory.GC_EQUIPMENT
			    || goods.GetCategory() == GoodsCategory.GC_BADGE)
            {
                AllGoodsList[uuid] = goods;
                this.OnAllGoodsListChanged();
            }
            //if (goods.GetCategory() == GoodsCategory.GC_FASHION && FashionChange != null)
            //{
            //    FashionChange(goods);
            //}
        }
    }

    public void DelGoods(GoodsCategory category, ulong uuid)
    {
        Dictionary<ulong, Goods> goodsItemList = null;
        switch (category)
        {
            case GoodsCategory.GC_ROLE:
                goodsItemList = RoleGoodsList;
                break;
            case GoodsCategory.GC_SKILL:
                goodsItemList = SkillGoodsList;
                break;
            case GoodsCategory.GC_FAVORITE:
                goodsItemList = FavoriteGoodsList;
                break;
            case GoodsCategory.GC_CONSUME:
                goodsItemList = ConsumeGoodsList;
                break;
            //case GoodsCategory.GC_TATTOO:
            //    goodsItemList = TattooGoodsList;
            //    break;
            case GoodsCategory.GC_EQUIPMENT:
                goodsItemList = EquipmentGoodsList;
                break;
            //case GoodsCategory.GC_EQUIPMENTPIECE:
            //    goodsItemList = EquipmentPieceList;
            //    break;
            case GoodsCategory.GC_EXERCISE:
                goodsItemList = TrainingList;
                break;
            case GoodsCategory.GC_MATERIAL:
                goodsItemList = MaterialList;
                break;
            case GoodsCategory.GC_BADGE:
                goodsItemList = BadgeGoodsList;
                break;
            case GoodsCategory.GC_TOTAL:
                if (RoleGoodsList.ContainsKey(uuid))
                    RoleGoodsList.Remove(uuid);
                else if (SkillGoodsList.ContainsKey(uuid))
                    SkillGoodsList.Remove(uuid);
                else if (FavoriteGoodsList.ContainsKey(uuid))
                    FavoriteGoodsList.Remove(uuid);
                else if (ConsumeGoodsList.ContainsKey(uuid))
                    ConsumeGoodsList.Remove(uuid);
                //else if (TattooGoodsList.ContainsKey(uuid))
                //    TattooGoodsList.Remove(uuid);
                else if (EquipmentGoodsList.ContainsKey(uuid))
                    EquipmentGoodsList.Remove(uuid);
                //else if (EquipmentPieceList.ContainsKey(uuid))
                //    EquipmentPieceList.Remove(uuid);
                else if (FashionGoodsList.ContainsKey(uuid))
                {
                    FashionGoodsList.Remove(uuid);
                }
                else if (TrainingList.ContainsKey(uuid))
                {
                    TrainingList.Remove(uuid);
                }
                else if (MaterialList.ContainsKey(uuid))
                {
                    MaterialList.Remove(uuid);
                }
                else if (BadgeGoodsList.ContainsKey(uuid))
                {
                    BadgeGoodsList.Remove(uuid);
                }
                break;
        }
        if (goodsItemList.ContainsKey(uuid))
        {
            goodsItemList.Remove(uuid);
            if (AllGoodsList.ContainsKey(uuid))
            {
                AllGoodsList.Remove(uuid);
                this.OnAllGoodsListChanged();
            }
        }
    }

    /*
    public Goods GetEquipTattooGoods(uint captainID, TattooType tattooType)
    {
        RoleInfo captainInfo = GetCaptainInfo(captainID);
        if (captainInfo != null)
        {
            for (int i = 0; i < captainInfo.tattoo_slot_info.Count; ++i)
            {
                ulong uuid = captainInfo.tattoo_slot_info[i].tattoo_uuid;
                if (uuid != 0)
                {
                    Goods goods = GetGoods(GoodsCategory.GC_TATTOO, uuid);
                    if (goods != null && goods.GetSubCategory() == tattooType)
                    {
                        return goods;
                    }
                }
            }
        }
        return null;
    }
     * */

    public Goods GetFashionByID(uint fashionID)
    {
        foreach (KeyValuePair<ulong, Goods> kv in FashionGoodsList)
        {
            Goods good = kv.Value;
            if (good.GetID() == fashionID)
            {
                return good;
            }
        }

        return null;
    }

    public Goods GetTrainingByID(uint trainingID)
    {
        foreach (KeyValuePair<ulong, Goods> kv in TrainingList)
        {
            Goods good = kv.Value;
            if (good.GetID() == trainingID)
            {
                return good;
            }
        }

        return null;
    }

    public Goods GetMaterialByID(uint materialID)
    {
        foreach (KeyValuePair<ulong, Goods> kv in MaterialList)
        {
            Goods good = kv.Value;
            if (good.GetID() == materialID)
            {
                return good;
            }
        }
        return null;
    }

    public bool CheckSkillInRole(uint roleID, ulong skillUUID) 
    {
        RoleInfo roleInfo = GetRole2(roleID);
        foreach (SkillSlotProto skillItem in roleInfo.skill_slot_info)
        {
            if (skillItem.skill_uuid != 0 && skillItem.skill_uuid == skillUUID)
                return true;
        }
        return false;
    }

    public Goods GetBadgesGoodByID(uint badgeId)
    {
        foreach (KeyValuePair<ulong, Goods> kv in BadgeGoodsList)
        {
            Goods good = kv.Value;
            if (good.GetID() == badgeId)
            {
                return good;
            }
        }
        return null;
    }

    //----------------------------------------------------
    //---------- 生涯模式系统相关操作
    public bool CheckChapter(uint chapterID)
    {
        return ChapterList.ContainsKey(chapterID);
    }

    public bool CheckChapterComplete(uint chapterID)
    {
        Chapter chapter = GetChapter(chapterID);
        if (chapter != null)
        {
            return chapter.is_complete;
        }
        return false;
    }

    public Chapter GetChapter(uint chapterID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID];
        }
        return null;
    }

    public void ChangeChapterData(ChapterProto chapterProto)
    {
        Chapter chapter = GetChapter(chapterProto.id);
        if (chapter != null)
        {
            chapter.ChangeData(chapterProto);
        }
    }

    public void ChangeChaptersData(List<ChapterProto> chapters)
    {
        for (int i = 0; i < chapters.Count; ++i)
        {
            uint chapterID = chapters[i].id;
            Chapter chapter = GetChapter(chapterID);
            if (chapter != null)
            {
                chapter.ChangeData(chapters[i]);
            }
            else
            {
                MainPlayer.Instance.AddChapter(chapterID);
            }
        }
    }

    public void AddChaptersData(ChapterProto chapter)
    {
        Chapter chapterObj = GetChapter(chapter.id);
        if (chapterObj != null)
        {
            chapterObj.ChangeData(chapter);
        }
        else
        {
            MainPlayer.Instance.AddChapter(chapter.id);
        }
    }

    public bool CheckSection(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID].sections.ContainsKey(sectionID);
        }
        return false;
    }

    public Section GetSection(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID)
            && ChapterList[chapterID].sections.ContainsKey(sectionID))
        {
            return ChapterList[chapterID].sections[sectionID];
        }
        return null;
    }

    public bool CheckSectionComplete(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID].CheckSectionComplete(sectionID);
        }
        return false;
    }

    public bool CheckAllSectionComplete(uint chapterID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID].CheckAllSectionComplete();
        }
        return false;
    }

    public bool AddChapter(uint chapterID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return false;
        }
        Chapter chapter = new Chapter();
        chapter.id = chapterID;
        ChapterList[chapterID] = chapter;

        chapter.AddFirstSection();
        return true;
    }

    public bool AddSection(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID) == false)
        {
            return false;
        }
        return ChapterList[chapterID].AddSection(sectionID);
    }

    //奖励球员 返回当前关卡奖励球员列表
    public uint CheckGetRole(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID].sections[sectionID].get_role;
        }
        return 0;
    }

    //奖励球员 修改是否已经领取状态
    public void SetGetRole(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
             ChapterList[chapterID].sections[sectionID].get_role = 1;
        }
    }

    //关卡激活动画 返回当前关卡是否播放激活动画
    public uint CheckSectionActivate(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            return ChapterList[chapterID].sections[sectionID].is_activate;
        }
        return 0;
    }

    //关卡激活动画 修改动画播放状态
    public void SetSectionActivate(uint chapterID, uint sectionID)
    {
        if (ChapterList.ContainsKey(chapterID))
        {
            ChapterList[chapterID].sections[sectionID].is_activate = 1;
        }
    }


    //----------------------------------------------------
    //---------- 邮件系统相关操作
    //初始化邮件信息
    public void InitMailInfo(List<MailInfo> mails)
    {
        MailList = mails;
    }

    //添加新邮件
    public void AddNewMail(MailInfo newMail)
    {
        MailList.Add(newMail);
    }

    //通过邮件ID获取邮件对象
    public MailInfo GetMailByID(uint mailID)
    {
        for (int i = 0; i < MailList.Count; ++i)
        {
            if (MailList[i].id == mailID)
                return MailList[i];
        }
        return null;
    }

    //通过UUID获取邮件对象
    public MailInfo GetMailByUUID(ulong uuid)
    {
        for (int i = 0; i < MailList.Count; ++i)
        {
            if (MailList[i].uuid == uuid)
                return MailList[i];
        }
        return null;
    }

    //通过邮件ID获取邮件UUID
    public ulong GetMailUUIDByID(uint mailID)
    {
        for (int i = 0; i < MailList.Count; ++i)
        {
            if (MailList[i].id == mailID)
                return MailList[i].uuid;
        }
        return 0;
    }

    //读取邮件
    public void ReadMail(ulong uuid)
    {
        for (int i = 0; i < MailList.Count; ++i)
        {
            if (MailList[i].uuid == uuid)
            {
                if (MailList[i].attachment != null)
                    MailList[i].state = (uint)MailState.READ_NOT_GET;
                else
                    MailList[i].state = (uint)MailState.READ_WITHOUT;
                return;
            }
        }
    }

    //领取附件
    public void GetMailAttachment(ulong uuid)
    {
        for (int i = 0; i < MailList.Count; ++i)
        {
            if (MailList[i].uuid == uuid)
            {
                if (MailList[i].get_delete == 1)
                {
                    MailList.Remove(MailList[i]);
                    return;
                }
                if (MailList[i].state == (uint)MailState.READ_NOT_GET)
                {
                    MailList[i].state = (uint)MailState.READ_GET;
                }
                return;
            }
        }
    }


    //----------------------------------------------------
    //---------- 任务系统相关操作
    //判断任务是否已完成
    public bool IsTaskCompleted(uint taskID)
    {
        foreach (TaskData data in taskInfo.task_list)
        {
            if (data.id == taskID && (data.state == 2 || data.state == 3))
                return true;
        }
        return false;
    }

    //设置任务完成状态
    public void SetTaskFinished(uint taskID)
    {
        if (taskInfo == null || taskInfo.task_list == null || taskInfo.task_list.Count <= 0)
            return;

        foreach (TaskData data in taskInfo.task_list)
        {
            if (data.id == taskID)
            {
                data.state = 2;
                break;
            }
        }
    }



    //----------------------------------------------------
    //---------- 球员训练项系统相关操作
    //
    public void SetExerciseInfo(uint roleId, uint exerciseId, ExerciseInfo info)
    {
		RoleInfo roleinfo = GetRole2(roleId);
		if( roleinfo == null )
			return;

		for (int j = 0; j < roleinfo.exercise.Count; j++)
		{
			if (roleinfo.exercise[j].id == exerciseId)
			{
				roleinfo.exercise[j] = info;
				break;
			}
		}
    }

    public ExerciseInfo GetExerciseInfo(uint roleId, uint exerciseId)
    {
        RoleInfo rInfo = GetRole2(roleId);
        if (rInfo == null)
        {
            Logger.LogError("GetExerciseInfo error for not exist roleId=" + roleId);
            return null;
        }

        foreach (ExerciseInfo eInfo in rInfo.exercise)
        {
            if (eInfo.id == exerciseId)
            {
                return eInfo;
            }
        }
        return null;

    }

    public uint GetExerciseLevel(uint roleId, uint exerciseId)
    {
        RoleInfo rInfo = GetRole2(roleId);
        if (rInfo == null)
        {
            return 0;
        }

        foreach (ExerciseInfo eInfo in rInfo.exercise)
        {
            if (eInfo.id == exerciseId)
            {

                return (eInfo.quality - 1) * 5 + eInfo.star;
            }
        }
        return 0;
    }

    public List<ExerciseInfo> GetExerciseInfoList(uint roleId) 
    {
        List<ExerciseInfo> exerciseInfoList = null;
        ExerciseInfos.TryGetValue(roleId, out exerciseInfoList);
        return exerciseInfoList;
    }

    public bool IsPractiseCompleted(uint id)
    {
        bool completed = false;
        PractiseInfo.TryGetValue(id, out completed);
        return completed;
    }

    public void SetPractiseCompleted(uint id, bool completed)
    {
        PractiseInfo[id] = completed;
    }

    public bool IsGuideCompleted(uint id)
    {
        return GuideInfo.ContainsKey(id) && GuideInfo[id];
    }

    public uint GetRemainTimes()
    {
        uint count = 0;
        foreach (KeyValuePair<uint, bool> info in PractiseInfo)
        {
			PractiseData config = GameSystem.Instance.PractiseConfig.GetConfig(info.Key);
			if (config != null && config.is_activity == 1 && !info.Value)
                ++count; 
        }
        return count;
    }

    //----------------------------------------------------
    //---------- 引导系统相关操作
    public uint GetUncompletedGuide()
    {
        foreach (KeyValuePair<uint, bool> pair in GuideInfo)
        {
            if (pair.Value == false)
                return pair.Key;
        }
        return 0;
    }

    public uint GetInterruptedGuide()
    {
        foreach (KeyValuePair<uint, bool> pair in GuideInfo)
        {
            if (!pair.Value)
                return pair.Key;
        }
        return 0;
    }

    public void SetGuideCompleted(uint id)
    {
        GuideInfo[id] = true;
    }



    //----------------------------------------------------
    //---------- 属性相关操作
    /*
    //获取队长战斗属性（等级+被动技能+训练+纹身）
    public Dictionary<uint, uint> GetCaptainAttr(uint captainID, uint bias = (uint)CaptainBias.CB_NONE)
    {
        Dictionary<uint, uint> attr = new Dictionary<uint, uint>();
        //等级属性
        if (bias == (uint)CaptainBias.CB_NONE)
        {
            bias = (uint)GetCaptainBias(captainID);
        }
        Dictionary<uint, uint> levelAddn = GetCaptainLevelAttr(captainID, bias);
        foreach (KeyValuePair<uint, uint> item in levelAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        //被动技能属性
        Dictionary<uint, uint> passiveSkillAddn = GetRolePassiveSkillAttr(captainID);
        foreach (KeyValuePair<uint, uint> item in passiveSkillAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        //训练属性
        Dictionary<uint, uint> trainingAddn = GetCaptainTrainingAttr(captainID);
        foreach (KeyValuePair<uint, uint> item in trainingAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        //纹身属性
        Dictionary<uint, uint> tattooAddn = GetRoleTattooAttr(captainID);
        foreach (KeyValuePair<uint, uint> item in tattooAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        return attr;
    }
     * */

    /*
    //获取队员战斗属性（品阶+被动技能+纹身）
    public Dictionary<uint, uint> GetMemberAttr(uint memberID)
    {
        Dictionary<uint, uint> attr = new Dictionary<uint, uint>();
        //等级属性
        Dictionary<uint, uint> qualityAddn = GetMemberQualityAttr(memberID);
        foreach (KeyValuePair<uint, uint> item in qualityAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        //被动技能属性
        Dictionary<uint, uint> passiveSkillAddn = GetRolePassiveSkillAttr(memberID);
        foreach (KeyValuePair<uint, uint> item in passiveSkillAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        //纹身属性
        Dictionary<uint, uint> tattooAddn = GetRoleTattooAttr(memberID);
        foreach (KeyValuePair<uint, uint> item in tattooAddn)
        {
            if (attr.ContainsKey(item.Key))
            {
                attr[item.Key] += item.Value;
            }
            else
            {
                attr[item.Key] = item.Value;
            }
        }
        return attr;
    }
     * */

    /*
    //获取队长等级战斗属性
    public Dictionary<uint, uint> GetCaptainLevelAttr(uint captainID, uint bias)
    {
        Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
        AttrData levelAttr = GameSystem.Instance.AttrDataConfigData.GetCaptainAttrData(captainID, bias);
        if (levelAttr != null)
        {
            foreach (KeyValuePair<string, uint> attr in levelAttr.attrs)
            {
                uint attrID = GameSystem.Instance.AttrNameConfigData.GetAttrData(attr.Key).id;
                if (addn_attr.ContainsKey(attrID))
                {
                    addn_attr[attrID] += attr.Value;
                }
                else
                {
                    addn_attr[attrID] = attr.Value;
                }
            }
        }
        return addn_attr;
    }
     * */

    /*
    //获取队长训练战斗属性加成
    public Dictionary<uint, uint> GetCaptainTrainingAttr(uint captainID)
    {
        Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
        RoleInfo captainInfo = GetCaptainInfo(captainID);
        if (captainInfo != null)
        {
            for (int i = 0; i < captainInfo.training.Count; ++i)
            {
                TrainingLevelConfigData config = GameSystem.Instance.TrainingConfig.GetTrainingLevelConfig(captainInfo.training[i].id, captainInfo.training[i].level);
                foreach (KeyValuePair<uint, uint> child in config.addn_attr)
                {
                    if (addn_attr.ContainsKey(child.Key))
                    {
                        addn_attr[child.Key] += child.Value;
                    }
                    else
                    {
                        addn_attr[child.Key] = child.Value;
                    }
                }
            }
        }
        return addn_attr;
    }
     * */

    //获取球员被动技能战斗属性加成
    public Dictionary<uint, uint> GetRolePassiveSkillAttr(uint roleID)
    {
        Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();

        Player role = GetRole(roleID);
        if (role == null)
            return addn_attr;
        RoleInfo roleInfo = role.m_roleInfo;
        if (roleInfo == null)
            return addn_attr;

        for (int i = 0; i < roleInfo.skill_slot_info.Count; ++i)
        {
            ulong skillUUID = roleInfo.skill_slot_info[i].skill_uuid;
            if (skillUUID != 0 && (roleInfo.skill_slot_info[i].id / 1000 == 1))
            {
                Goods skillGoods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_SKILL, skillUUID);
                if (skillGoods != null)
                {
                    SkillAttr skillAttr = GameSystem.Instance.SkillConfig.GetSkill(skillGoods.GetID());
                    if (skillAttr != null)
                    {
                        foreach (KeyValuePair<string, uint> attr in skillAttr.levels[skillGoods.GetLevel()].additional_attrs)
                        {
                            uint attrID = GameSystem.Instance.AttrNameConfigData.GetAttrData(attr.Key).id;
                            if (addn_attr.ContainsKey(attrID))
                            {
                                addn_attr[attrID] += attr.Value;
                            }
                            else
                            {
                                addn_attr[attrID] = attr.Value;
                            }
                        }
                    }
                }
            }
        }
        return addn_attr;
    }

    /*
    //获取球员纹身战斗属性加成
    public Dictionary<uint, uint> GetRoleTattooAttr(uint roleID)
    {
        Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();

        Player role = GetRole(roleID);
        if (role == null)
            return addn_attr;
        RoleInfo roleInfo = role.m_roleInfo;
        if (roleInfo == null)
            return addn_attr;

        for (int i = 0; i < roleInfo.tattoo_slot_info.Count; ++i)
        {
            ulong tattooUUID = roleInfo.tattoo_slot_info[i].tattoo_uuid;
            if (tattooUUID != 0)
            {
                Goods tattooGoods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TATTOO, tattooUUID);
                if (tattooGoods != null)
                {
                    TattooLvConfigData tattooConfig = GameSystem.Instance.TattooConfig.GetTattooConfig(tattooGoods.GetID(), tattooGoods.GetLevel());
                    if (tattooConfig != null)
                    {
                        foreach (KeyValuePair<uint, uint> attr in tattooConfig.addn_attr)
                        {
                            if (addn_attr.ContainsKey(attr.Key))
                            {
                                addn_attr[attr.Key] += attr.Value;
                            }
                            else
                            {
                                addn_attr[attr.Key] = attr.Value;
                            }
                        }
                    }
                }
            }
        }
        return addn_attr;
    }
     * */

    //获取队员品阶战斗属性
    public Dictionary<uint, uint> GetMemberQualityAttr(uint memberID)
    {
        Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();

        Player role = GetRole(memberID);
        if (role == null)
            return addn_attr;
        RoleInfo member = role.m_roleInfo;
        if (member == null)
            return addn_attr;

        //Dictionary<string, uint> stringAttr = GameSystem.Instance.RoleQualityConfigData.GetRoleQualityProperty(memberID, member.quality);
        //foreach (KeyValuePair<string, uint> attr in stringAttr)
        //{
        //    uint attrID = GameSystem.Instance.AttrNameConfigData.GetAttrData(attr.Key).id;
        //    if (addn_attr.ContainsKey(attrID))
        //    {
        //        addn_attr[attrID] += attr.Value;
        //    }
        //    else
        //    {
        //        addn_attr[attrID] = attr.Value;
        //    }
        //}
        return addn_attr;
    }

    public AttrData GetRoleAttrs(RoleInfo roleInfo, List<EquipInfo> equipInfo = null, List<FightRole> squadInfo=null, List<uint> mapsInfo = null, List<List<uint>> rivalAttr = null,BadgeBook book = null)
    {
        //if (equipInfo == null)
        //    equipInfo = EquipInfo;
        if (squadInfo == null)
            squadInfo = SquadInfo;
        //if (mapsInfo == null)
        //    mapsInfo = MapIDInfo;
        Dictionary<string, uint> ret = new Dictionary<string, uint>();
        //属性等级叠加
        Dictionary<uint, uint> hedgeLevelAttrs = new Dictionary<uint, uint>();
        uint allHedgingAddn = 0;
        uint allHedgingMulti = 0;
        //GetEquipmentSuitAttr(roleInfo.id, GlobalConst.ALL_HEDGING_ID, out allHedgingAddn, out allHedgingMulti, equipInfo, squadInfo); //全部对冲属性系数
        foreach (AttrNameData item in GameSystem.Instance.AttrNameConfigData.AttrNameDatas)
        {
            IM.Number multiAttrValue = IM.Number.zero;
            uint v = GetAttrValue(roleInfo, item.id, out multiAttrValue, equipInfo, squadInfo);
            v = (uint)(v * (1.0f + (float)allHedgingMulti / 1000));
            if (item.type != AttributeType.HEDGINGLEVEL)
                ret.Add(item.symbol, v);
            else
                hedgeLevelAttrs.Add(item.id, v);
        }

        AttrData attrData = new AttrData();
        attrData.attrs = ret;

        //if (mapsInfo != null && mapsInfo.Count > 0)
        //    MapsPromoteAttr(attrData, mapsInfo);
        //SkillPromoteAttr(roleInfo, hedgeLevelAttrs);
        FashionPromoteAttr(roleInfo, hedgeLevelAttrs, rivalAttr);
        GetBadgeBookAttrByBookId(hedgeLevelAttrs,book);
        Logger.Log("Hedging level, calc begin. Role ID: " + roleInfo.id);
        CalcPromoteAttr(attrData, hedgeLevelAttrs);

        IM.Number preFightCapacity = IM.Number.zero;
        preFightCapacity = attrData.fightingCapacity;
		CalcFightingCapacity(roleInfo.id, roleInfo.star, attrData);
		//foreach (KeyValuePair<string, uint> pair in attrData.attrs)
		//	Logger.Log(pair.Key + " : " + pair.Value);
        if (RoleFightPower.ContainsKey(roleInfo.id))
        {
            if (RoleFightPower[roleInfo.id] != attrData.fightingCapacity)
            {
                RoleFightPower[roleInfo.id] = attrData.fightingCapacity;
                if (isSendFightPower == false && squadInfo.Find(x => x.role_id == roleInfo.id) != null)
                {
                    SendFightPowerChange();
                }
            }
        }
        else
        {
            RoleFightPower.Add(roleInfo.id, attrData.fightingCapacity);
            if (isSendFightPower == false && squadInfo.Find(x => x.role_id == roleInfo.id) != null)
            {
                SendFightPowerChange();
            }
        }

        return attrData;
    }

    public AttrData GetRoleAttrsByID(uint roleID)
    {
        RoleInfo roleInfo = GetRole2(roleID);
        if (roleInfo == null)
            return null;

		BadgeBook book = null;
		if (roleInfo != null && roleInfo.badge_book_id != 0)
		{
			book = MainPlayer.Instance.badgeSystemInfo.GetBadgeBookByBookId (roleInfo.badge_book_id);
		}
		return GetRoleAttrs(roleInfo, null, null, MapIDInfo, null, book);
    }

    // According to RoleInfo, attrID, get value
    public uint GetAttrValue(RoleInfo roleInfo, uint attrID, out IM.Number multiAttrValue, List<EquipInfo> equipInfo, List<FightRole> squadInfo = null)
    {
        bool isFactor = GameSystem.Instance.AttrNameConfigData.IsFactor(attrID);
        int value = (int)GameSystem.Instance.RoleBaseConfigData2.GetAttrValue(roleInfo.id, attrID);
        multiAttrValue = IM.Number.zero;
        if (isFactor)
        {
            IM.Number talent = GameSystem.Instance.RoleBaseConfigData2.GetTalent(roleInfo.id);
            talent = value * (1 + talent);
            value = talent.ceilToInt;
            /*
            float levelFactor = GameSystem.Instance.RoleLevelConfigData.GetFactor(roleInfo.level);
            float qualityFactor = GameSystem.Instance.qualityAttrCorConfig.GetFactor(roleInfo.id, roleInfo.quality);
            value = value * levelFactor * qualityFactor;

            //装备属性
            value += GetEquipmentAttr(roleInfo.id, attrID, equipInfo, squadInfo);
            float talent = GameSystem.Instance.RoleBaseConfigData2.GetTalent(roleInfo.id);

            //套装属性
            uint addnAttr = 0;
            uint multiAttr = 0;
            GetEquipmentSuitAttr(roleInfo.id, attrID, out addnAttr, out multiAttr, equipInfo, squadInfo);
            uint allHedgingAddn = 0;
            uint allHedgingMulti = 0;
            GetEquipmentSuitAttr(roleInfo.id, GlobalConst.ALL_HEDGING_ID, out allHedgingAddn, out allHedgingMulti, equipInfo, squadInfo); //全部对冲属性系数 
            multiAttrValue = (value + addnAttr) * (float)(allHedgingMulti + multiAttr) / 1000;
            value = (value + addnAttr) * (talent + (float)multiAttr / 1000);
             */
        }
        /*
        Dictionary<uint, uint>  attrs = GameSystem.Instance.starAttrConfig.GetAttr(roleInfo.id, roleInfo.star);
        if (attrs != null && attrs.ContainsKey(attrID))
        {
            value += attrs[attrID];
        }*/
        return (uint)(value);
    }

    public uint GetEquipmentAttr(uint roleID, uint attrID, List<EquipInfo> equipInfo, List<FightRole> squadInfo = null)
    {
        uint value = 0;
        foreach (FightRole fighter in squadInfo)
        {
            if (fighter.role_id == roleID)
            {
                foreach (fogs.proto.msg.EquipInfo equip in equipInfo)
                {
                    if (equip.pos == fighter.status)
                    {
                        Dictionary<uint, List<SuitAddnAttrData>> suitAttrInfo = new Dictionary<uint, List<SuitAddnAttrData>>();
                        for (int i = 0; i < equip.slot_info.Count; ++i)
                        {
                            uint goodsID = equip.slot_info[i].equipment_id;
                            uint goodsLevel = equip.slot_info[i].equipment_level;
                            //Goods equipGoods = GetGoods(GoodsCategory.GC_EQUIPMENT, equip.slot_info[i].equipment_uuid);
                            //if (equipGoods != null)
                            {
                                EquipmentBaseDataConfig equipConfig = GameSystem.Instance.EquipmentConfigData.GetBaseConfig(goodsID, goodsLevel);
                                if (equipConfig != null)
                                {
                                    uint equipValue = 0;
                                    equipConfig.addn_attr.TryGetValue(attrID, out equipValue);
                                    value += equipValue;
                                }
                            }
                        }
                        break;
                    }
                }
                break;
            }
        }
        return value;
    }

    public uint GetEquipmentSuitAttr(uint roleID, uint attrID, out uint addnAttr, out uint multiAttr, List<EquipInfo> equipInfo, List<FightRole> squadInfo = null)
    {
        addnAttr = 0;
        multiAttr = 0;
        Dictionary<uint, List<SuitAddnAttrData>> suitAttrInfo = new Dictionary<uint, List<SuitAddnAttrData>>();
        foreach (FightRole fighter in squadInfo)
        {
            if (fighter.role_id == roleID)
            {
                foreach (fogs.proto.msg.EquipInfo equip in equipInfo)
                {
                    if (equip.pos == fighter.status)
                    {
                        for (int i = 0; i < equip.slot_info.Count; ++i)
                        {
                            uint goodsID = equip.slot_info[i].equipment_id;
                            //套装
                            SuitAddnAttrData suitAttrData = GameSystem.Instance.GoodsConfigData.GetSuitAttrConfig(goodsID);
                            if (suitAttrData != null)
                            {
                                uint suitID = suitAttrData.suitID;
                                if (suitAttrInfo.ContainsKey(suitID))
                                {
                                    suitAttrInfo[suitID].Add(suitAttrData);
                                }
                                else
                                {
                                    List<SuitAddnAttrData> suitList = new List<SuitAddnAttrData>();
                                    suitList.Add(suitAttrData);
                                    suitAttrInfo.Add(suitID, suitList);
                                }
                            }
                        }
                        break;
                    }
                }
                break;
            }
        }

        //套装属性
        foreach (List<SuitAddnAttrData> dataList in suitAttrInfo.Values)
        {
            for (int i = 0; i < dataList.Count; ++i)
            {
                uint uKey = (uint)i;
                //加属性
                if (dataList[i].addn_attr.ContainsKey(uKey) && dataList[i].addn_attr[uKey].ContainsKey(attrID))
                {
                    addnAttr = addnAttr + dataList[i].addn_attr[uKey][attrID];
                }
                //乘属性
                if (dataList[i].multi_attr.ContainsKey(uKey) && dataList[i].multi_attr[uKey].ContainsKey(attrID))
                {
                    multiAttr = multiAttr + dataList[i].multi_attr[uKey][attrID];
                }
            }
        }
        return addnAttr;
    }

    public void GetBadgeBookAttrByBookId(Dictionary<uint, uint> hedgeLevelAttrs,BadgeBook book)
    {
       if(book!=null)
       {
           List<BadgeSlot> slots = book.slot_list;
           int count = slots.Count;
           for (int i = 0; i < count; i++)
           {
               BadgeSlot slot = slots[i];
               if (slot.badge_id > 0)
               {
                   BadgeAttrBaseConfig badgeAttconfig = GameSystem.Instance.BadgeAttrConfigData.GetBaseConfig(slot.badge_id);
                   if (badgeAttconfig!=null)
                   {
                       Dictionary<uint, uint> addAttr = badgeAttconfig.addAttr;
                       foreach (KeyValuePair<uint, uint> attrItem in addAttr)
                       {
                           uint playerAttrID = attrItem.Key;
                           if (hedgeLevelAttrs.ContainsKey(playerAttrID))
                               hedgeLevelAttrs[playerAttrID] += attrItem.Value;
                           else
                               hedgeLevelAttrs.Add(playerAttrID, attrItem.Value);
                       }
                   }
               }
           }
       }
    }

	public void CalcFightingCapacity(uint id, uint star, AttrData attrData)
	{
		PositionType position;
		if (id < 10000)
			position = (PositionType)(GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id).position);
		else
		{
			position = (PositionType)(GameSystem.Instance.NPCConfigData.GetConfigData(id).position);
			if (star == 0)	// Calculate star for NPC
			{
				IM.Number attrSumNPC = IM.Number.zero;
				foreach (AttrNameData attr in GameSystem.Instance.AttrNameConfigData.AttrNameDatas)
				{
					if (attr.type == AttributeType.BASIC && attrData.attrs.ContainsKey(attr.symbol))
					{
						attrSumNPC += attrData.attrs[attr.symbol];
					}
				}
				IM.Number factor = GameSystem.Instance.CommonConfig.GetNumber("g" + position + "StarFactor");
                //star = (uint)(( 4 + Math.Pow(Math.Abs(16 + 44 * (factor - attrSumNPC) ),0.5) )/ 22 );
                star = (uint)(4 + IM.Math.Sqrt(IM.Math.Abs(16 + 44 * (factor - attrSumNPC))) / 22).ceilToInt;
			}
		}

		IM.Number attrSum = IM.Number.zero;
		foreach (AttrNameData attr in GameSystem.Instance.AttrNameConfigData.AttrNameDatas)
		{
            if (attr.type == AttributeType.HEDGING && attrData.attrs.ContainsKey(attr.symbol))
			{
				attrSum += attrData.attrs[attr.symbol] * attr.fc_weight;
			}
		}
		IM.Number starFactor = GameSystem.Instance.CommonConfig.GetNumber("gStarFCFactor");
        IM.Number positionFactor = GameSystem.Instance.CommonConfig.GetNumber("g" + position + "FCFactor");

		attrData.fightingCapacity = attrSum * (1 + star * starFactor) * positionFactor;
		//Logger.Log("Fighting capacity of role " + id + " is " + attrData.fightingCapacity);
	}

    public void SendFightPowerChange()
    {
        IM.Number squadCapacity = IM.Number.zero;
        isSendFightPower = true;
        for (int i = 0 ; i < SquadInfo.Count; ++i) 
        {
            IM.Number temp = IM.Number.zero;
            if (SquadInfo[i].role_id == 0)
                return;
            if (RoleFightPower.ContainsKey(SquadInfo[i].role_id))
            {
                temp = RoleFightPower[SquadInfo[i].role_id]; 
            }
            else
            {
				BadgeBook book = null;
                RoleInfo role = GetRole2(SquadInfo[i].role_id);
				if (role != null && role.badge_book_id != 0)
				{
					book = MainPlayer.Instance.badgeSystemInfo.GetBadgeBookByBookId (role.badge_book_id);
				}
				temp = GetRoleAttrs(role, null, null, MapIDInfo, null, book).fightingCapacity;
            }

            squadCapacity = squadCapacity + temp;
        }
        if (SquadFightPower != squadCapacity)
        {
            SquadFightPower = squadCapacity;
            FightPowerChange data = new FightPowerChange();
            data.fight_power = (uint)squadCapacity;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FightPowerChange>(0, data, MsgID.FightPowerChangeID);
            isSendFightPower = false;
        } 
    }

    public IM.Number CalcBaseFighting(uint id)
    {
        RoleBaseData2 roleData2 = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id);
        if(roleData2== null)
        {
            Logger.LogError("cannot find baseFighting for id=" + id);
            return IM.Number.zero;
        }
        AttrData attrData = new AttrData();
        foreach(KeyValuePair<string,uint> kv in roleData2.attrs_symbol)
        {
            IM.Number value = new IM.Number((int)kv.Value);

            AttrNameData nameData = GameSystem.Instance.AttrNameConfigData.GetAttrData(kv.Key);
            if(nameData.is_factor == 1 )
            {
                value = value * roleData2.talent_value;
            }

            attrData.attrs.Add(kv.Key, Convert.ToUInt32((float)value));
        }
        CalcFightingCapacity(id, (uint)roleData2.init_star, attrData);
        return attrData.fightingCapacity;
    }

    public void MapsPromoteAttr(AttrData data, List<uint> mapsInfo) 
    {
        Dictionary<string, uint> attrs = new Dictionary<string, uint>();
        foreach (uint mapsID in mapsInfo)
        {
            MapGroupData mapData = GameSystem.Instance.MapConfig.GetMapGroupDataByID(mapsID);
            if (mapData != null)
            {
                string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(mapData.attrID);
                if (symbol != String.Empty && data.attrs.ContainsKey(symbol))
                {
                    if (attrs.ContainsKey(symbol))
                        attrs[symbol] += mapData.attrNum;
                    else
                        attrs[symbol] = mapData.attrNum;
                }
            }
        }
        foreach (KeyValuePair<string, uint> attr in attrs)
        {
            //uint a = data.attrs[attr.Key];
            //double b = data.attrs[attr.Key] * (1 + attr.Value * 0.01);
            //uint c = (uint)Math.Floor(data.attrs[attr.Key] * (1 + attr.Value * 0.01));
            data.attrs[attr.Key] = (uint)Math.Floor(data.attrs[attr.Key] * (1 + attr.Value * 0.01));
        }
    }

    public void FashionPromoteAttr(RoleInfo roleInfo, Dictionary<uint, uint> hedgeLevelAttrs, List<List<uint>> rivalAttr = null)
    {
        if (roleInfo == null)
        {
            Debug.LogError("roleInfo is null!");
            return;
        }

        FashionConfig fashionConfig = GameSystem.Instance.FashionConfig;
        
        if (rivalAttr == null)
        {
            List<FashionSlotProto> fashionList = roleInfo.fashion_slot_info;
            foreach (FashionSlotProto fashion in fashionList)
            {
                Goods fashionGoods = GetGoods(GoodsCategory.GC_FASHION, fashion.fashion_uuid);
                if (fashionGoods != null)
                {
                    List<uint> attrList = fashionGoods.GetFashionAttrIDList();
                    if (attrList != null && attrList.Count > 0)
                    {
                        foreach (uint attrid in attrList)
                        {
                            if (attrid != 0)
                            {
                                FashionAttr fashionAttr = fashionConfig.GetFashionAttr(attrid);
                                uint playerAttrID = fashionAttr.player_attr_id;
                                if (hedgeLevelAttrs.ContainsKey(playerAttrID))
                                    hedgeLevelAttrs[playerAttrID] += fashionAttr.player_attr_num;
                                else
                                    hedgeLevelAttrs.Add(playerAttrID, fashionAttr.player_attr_num);
                            }
                        }
                    }
                }
            }
        }
        else 
        {
            if (rivalAttr.Count > 0) 
            {
                foreach (List<uint> fashionSlot in rivalAttr)
                {
                    foreach (uint attrid in fashionSlot)
                    {
                        if (attrid != 0)
                        {
                            FashionAttr fashionAttr = fashionConfig.GetFashionAttr(attrid);
                            uint playerAttrID = fashionAttr.player_attr_id;
                            if (hedgeLevelAttrs.ContainsKey(playerAttrID))
                                hedgeLevelAttrs[playerAttrID] += fashionAttr.player_attr_num;
                            else
                                hedgeLevelAttrs.Add(playerAttrID, fashionAttr.player_attr_num);
                        }
                    }
                }
            }
        }
    }

    public void SkillPromoteAttr(RoleInfo roleInfo, Dictionary<uint, uint> hedgeLevelAttrs)
    {
        if (roleInfo.skill_slot_info == null || roleInfo.skill_slot_info.Count <= 0)
        {
            Debug.Log("roleInfo have no skill !");
            return;
        }

        foreach (SkillSlotProto skillSlot in roleInfo.skill_slot_info)
        {
            if (skillSlot.skill_uuid == 0)
                continue;

            uint skillID = skillSlot.skill_id;
            uint skillLevel = skillSlot.skill_level;
            SkillAttr attr = GameSystem.Instance.SkillConfig.GetSkill(skillID);
            SkillLevel currentSkill = attr.GetSkillLevel(skillLevel);
            foreach (KeyValuePair<string, uint> data in currentSkill.additional_attrs)
            {
                AttrNameData addData = GameSystem.Instance.AttrNameConfigData.GetAttrData(data.Key);
                if (hedgeLevelAttrs.ContainsKey(addData.id))
                    hedgeLevelAttrs[addData.id] += data.Value;
                else
                    hedgeLevelAttrs.Add(addData.id, data.Value);
            }
        }
        //foreach (KeyValuePair<uint, uint> attr in hedgeAttrs)
        //{
        //    uint symbolID = attr.Key - 100;
        //    uint hedgeID = attr.Key;
        //    uint hedgeValue = attr.Value;
        //    float factor = GameSystem.Instance.HedgingConfig.GetHedgeLevelFactor(hedgeID);
        //    string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(symbolID);
        //    attrData.attrs[symbol] = (uint)Math.Floor(attrData.attrs[symbol] * (1 + hedgeValue * factor));
        //}
    }

    public void CalcPromoteAttr(AttrData attrData, Dictionary<uint, uint> hedgeLevelAttrs) 
    {
        Dictionary<uint, uint> attrs = new Dictionary<uint, uint>();
        foreach (KeyValuePair<uint, uint> hedgeLevel in hedgeLevelAttrs)
        {
            HedgingConfig.hedgeLevelData data = GameSystem.Instance.HedgingConfig.GetHedgeLevelFactor(hedgeLevel.Key);
            if (data != null)
            {
                attrs.Add(hedgeLevel.Key, hedgeLevel.Value);
            }
            else 
            {
                string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(hedgeLevel.Key);
                if (attrData.attrs.ContainsKey(symbol))
                {
                    attrData.attrs[symbol] = attrData.attrs[symbol] + hedgeLevel.Value;
                }
            }
        }
        foreach (KeyValuePair<uint, uint> attr in attrs)
        {
            HedgingConfig.hedgeLevelData data = GameSystem.Instance.HedgingConfig.GetHedgeLevelFactor(attr.Key);
            if (data != null)
            {
                float factor = data.factor;
                uint attrID = data.oppositeID;
                string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol(attrID);
                if (attrData.attrs.ContainsKey(symbol))
                {
                    if (attr.Value != 0)
                        Logger.Log(string.Format("Hedging level, before: {0} -> {1}", symbol, attrData.attrs[symbol]));
                    attrData.attrs[symbol] = (uint)Math.Round(attrData.attrs[symbol] * (1 + attr.Value * factor));
                    if (attr.Value != 0)
                        Logger.Log(string.Format("Hedging level, after: {0} -> {1}", symbol, attrData.attrs[symbol]));
                }
            }
        }
    }
    //----------------------------------------------------
    //---------- 斗牛赛相关操作
    public void SetBullFightTimes(uint fightTimes)
    {
        BullFight.times = fightTimes;
    }

    public void SetBullFightCompleteByClient()
    {
        int level = BullFightHard;
        BullFight.complete[level-1] = 1;
    }


    //----------------------------------------------------
    //---------- 投篮赛相关操作

    public void AddShootGameModeInfo(fogs.proto.msg.GameModeInfo info)
    {
        ShootGameModeInfo.Add(info);
    }
	
	public void ClearShootGameModeInfo()
	{
		ShootGameModeInfo.Clear();
	}

    public void SetShootedTimes(uint shootedTimes)
    {
        foreach (var info in ShootGameModeInfo)
        {
            info.times = shootedTimes;
        }
    }

    public void SetShootCompleteByClient()
    {
        int level = 0;
        if (curShootGameMode == fogs.proto.msg.GameMode.GM_MassBall)
        {
            level = MassBallHard;
            MainPlayer.Instance.ShootInfo.mass_ball_complete[level - 1] = 1;
        }
        else if (curShootGameMode == fogs.proto.msg.GameMode.GM_GrabZone)
        {
            level = GrabZoneHard;
            MainPlayer.Instance.ShootInfo.grab_zone_complete[level - 1] = 1;
        }
        else if (curShootGameMode == fogs.proto.msg.GameMode.GM_GrabPoint)
        {
            level = GrabPointHard;
            MainPlayer.Instance.ShootInfo.grab_point_complete[level - 1] = 1;
        }
    }
       

    //----------------------------------------------------
    //---------- 阵容相关操作
    public uint GetFightRole(FightStatus pos)
    {
        for (int i = 0; i < SquadInfo.Count; ++i)
        {
            if (SquadInfo[i].status == pos)
                return SquadInfo[i].role_id;
        }
        return 0;
    }

    public void SetFightRole(List<FightRole> info)
    {
        for (int i = 0; i < SquadInfo.Count; ++i)
        {
            for (int j = 0; i < SquadInfo.Count; ++j)
            {
                if (SquadInfo[j].status == info[i].status)
                {
                    SquadInfo[j].role_id = info[i].role_id;
                    //更新阵容角色的训练消息
                    List<ExerciseInfo> exinfo = new List<fogs.proto.msg.ExerciseInfo>();
                    RoleInfo role = MainPlayer.Instance.GetRole2(SquadInfo[j].role_id);
                    foreach (ExerciseInfo exercise in role.exercise)
                    {
                        exinfo.Add(exercise);
                    }
                    ExerciseInfos[role.id] = exinfo;
                    break;
                }
            }
        }
    }

    public bool IsInSquad(uint roleId )
    {
        foreach(var item in SquadInfo )
        {
            if( item.role_id == roleId )
            {
                return true;
            }
        }
        return false;
    }
    


    //----------------------------------------------------
    //---------- 装备相关操作
    public List<EquipmentSlot> GetEquipInfoByPos(FightStatus pos)
    {
        for (int i = 0; i < EquipInfo.Count; ++i)
        {
            if (EquipInfo[i].pos == pos)
                return EquipInfo[i].slot_info;
        }
        return null;
    }

    public ulong GetEquipInfoBySlotID(FightStatus pos, EquipmentSlotID slotID)
    {
        for (int i = 0; i < EquipInfo.Count; ++i)
        {
            if (EquipInfo[i].pos == pos)
            {
                for (int j = 0; j < EquipInfo[i].slot_info.Count; ++j)
                {
                    if (EquipInfo[i].slot_info[j].id == slotID)
                        return EquipInfo[i].slot_info[j].equipment_uuid;
                }
            }
        }
        return 0;
    }

    public void SetEquipInfoByPos(FightStatus pos, List<EquipmentSlot> slotInfo)
    {
        for (int equip = 0; equip < EquipInfo.Count; ++equip)
        {
            if (EquipInfo[equip].pos == pos)
            {
                List<EquipmentSlot> equipedSlotInfo = EquipInfo[equip].slot_info;
                for (int i = 0; i < slotInfo.Count; ++i)
                {
                    for (int j = 0; j < equipedSlotInfo.Count; ++j)
                    {
                        if (equipedSlotInfo[j].id == slotInfo[i].id)
                        {
                            equipedSlotInfo[j].equipment_uuid = slotInfo[i].equipment_uuid;
                            break;
                        }
                    }
                }
                break;
            }
        }
    }

    public void SetEquipInfoBySlotID(FightStatus pos, EquipmentSlotID slotID, ulong uuid)
    {
        for (int equip = 0; equip < EquipInfo.Count; ++equip)
        {
            if (EquipInfo[equip].pos == pos)
            {
                List<EquipmentSlot> equipedSlotInfo = EquipInfo[equip].slot_info;
                for (int i = 0; i < equipedSlotInfo.Count; ++i)
                {
                    if (equipedSlotInfo[i].id == slotID)
                    {
                        equipedSlotInfo[i].equipment_uuid = uuid;
                        break;
                    }
                }
                break;
            }
        }
    }

	public void OnNewDayComeMidNight(NewDayCome resp)
	{
        //签到信息
        if (signInfo != null)
            signInfo.signed = 0;
        LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Sign");

        DateTime s = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        long iTime = long.Parse(GameSystem.mTime + "0000000");
        TimeSpan toNow = new TimeSpan(iTime);
        s = s.Add(toNow);
        uint currentDay = uint.Parse(s.ToString("dd"));
        Logger.Log("currentDay = " + currentDay);
        if (currentDay == 1 && signInfo != null)
        {
            signInfo.sign_list.Clear();
            signInfo.append_sign_times = 0;
            GameObject rootObj = UIManager.Instance.m_uiRootBasePanel;
            Transform signTrans = rootObj.transform.FindChild("UISign(Clone)");
            if (signTrans && signTrans.transform.gameObject.activeSelf) 
            {
                LuaComponent signLua = signTrans.transform.GetComponent<LuaComponent>();
                LuaFunction function = signLua.table["OnClose"] as LuaFunction;
                function.Call(new object[] { signLua.table });
            }
        }

        if (resp.info.sign_list != null && resp.info.sign_list.Count > 0) 
        {
			if (MainPlayer.Instance.NewComerSign == null)
			{
				MainPlayer.Instance.NewComerSign = resp.info;
			}
			else
			{
				MainPlayer.Instance.NewComerSign.sign_list.Clear();
				foreach (uint signState in resp.info.sign_list)
				{
					//Logger.Log("signState :" + signState);
					MainPlayer.Instance.NewComerSign.sign_list.Add(signState);
				}
			}
            MainPlayer.Instance.NewComerSign.open_flag = resp.info.open_flag;
        }

		if (onMidNightCome != null)
			onMidNightCome();
	}

	public void AddOnMidNightCome(System.Action handler)
	{
		onMidNightCome += handler;
	}

	public void RemoveOnMidNightCome(System.Action handler)
	{
		onMidNightCome -= handler;
	}


    //----------------------------------------------------
    //---------- SDK

#if IOS_SDK
    [DllImport("__Internal")]
    private static extern void setLoginInfo(bool isLogin,string roleId, string roleName, string serverId, string serverName,string playerLevel,string vipLv);
#endif
    public void AddCreateNewRoleLog(bool isLogin )
    {
#if IOS_SDK || ANDROID_SDK
        string roleId = MainPlayer.Instance.AccountID.ToString();
        string roleName = MainPlayer.Instance.Name;
        string serverId = LoginIDManager.GetPlatServerID().ToString();
        string serverName = LoginIDManager.GetPlatServerName();
        string playerLevel = MainPlayer.Instance.Level.ToString();
       
	
		Logger.Log ("----roleId=" + roleId);
		Logger.Log ("----roleName=" + roleName );
		Logger.Log ("----serverId=" + serverId );
		Logger.Log ("servername=" + serverName );
#endif

#if IOS_SDK
        setLoginInfo(isLogin, roleId, roleName, serverId, serverName, playerLevel,Vip.ToString());
#endif

#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
       AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

       int type = 1;
       if( isLogin )
       {
           type=2;
       }
       object[] datas = new object[] { type, serverName, serverId, roleName, roleId, playerLevel, Vip.ToString() };
       jo.Call("SetLoginInfo", datas);
#endif

#if IOS_SDK || ANDROID_SDK
        if (GlobalConst.IS_ENABLE_TALKING_DATA)
        {
            TalkingDataGA.OnStart("DB6554D2E3C655F105731945CAE7DECC", MainPlayer.Instance.ZQServerId);
            TdAccount = TDGAAccount.SetAccount(MainPlayer.Instance.AccountID.ToString());
            if( isLogin )
            {
                TdAccount.SetAccountName(MainPlayer.Instance.Name);
            }
            TdAccount.SetGameServer(LoginIDManager.GetPlatServerName());
            TdAccount.SetLevel((int)_level);
            if (_tourist)
            {
                TdAccount.SetAccountType(AccountType.ANONYMOUS);
            }
            else
            {
                TdAccount.SetAccountType(AccountType.REGISTERED);
            }
        }
#endif
    }

#if IOS_SDK
    [DllImport("__Internal")]
    private static extern void sendGoodsLog(int roleLv, int vipLv, string updateType, string itemId, string itemName, int itemCount, string custorm);

	[DllImport("__Internal")]
	private static extern void sendPlayerLog(int roleLv, int vipLv, string propKey, string propValue, string rangeAbility );

	[DllImport("__Internal")]
	private static extern void openPlayerPlat();

	[DllImport("__Internal")]
	private static extern void userFeedback();
#endif

    public void SendGoodsLog(string updateType, string itemId, string itemName, int itemCount, string custom)
    {
        //Logger.Log("##SendGoodsLog CreateStep=" + CreateStep);
        // Make sure player has registered alreayd.
#if IOS_SDK
         sendGoodsLog((int)Level,(int)Vip,updateType,itemId, itemName, itemCount, custom);
#endif

#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        object[] datas = new object[] { (int)Level, (int)Vip, updateType, itemId, itemName, itemCount, custom };
        jo.Call("SendGoodsLog", datas);
#endif
    }

    public void SendPlayerLog( string propKey, string propValue, string rangeAbility )
    {
        //Logger.Log("##SendPlayerLog CreateStep=" + CreateStep);
#if IOS_SDK
		sendPlayerLog((int)Level, (int)Vip, propKey, propValue, rangeAbility);
#endif

#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        object[] datas = new object[] { (int)Level, (int)Vip, propKey, propValue, rangeAbility };
        jo.Call("SendPlayerLog",datas);
#endif
    }

	public void SendGiftExchangeCode(String giftCode, String url, String extendParams)
	{
		Logger.Log("##SendGiftExchangeCode giftCode=" + giftCode);
#if IOS_SDK
		//SendGiftExchangeCode(giftCode, url, extendParams);
#endif
		
#if ANDROID_SDK
		Logger.Log("##SendGiftExchangeCode 12 giftCode=" + giftCode);
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		
		object[] datas = new object[] { giftCode, url, extendParams};
		jo.Call("SendGiftExchangeCode",datas);
		Logger.Log("##SendGiftExchangeCode finish giftCode=" + giftCode);
#endif
	}

	public void OpenPlayerPlat()
	{
        Logger.Log("OpenPlayerPlat called");
#if IOS_SDK
		openPlayerPlat();
#endif

#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("GoCenter");
#endif
    }

    public bool EnterServiceQuestion()
    {
#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("EnterServiceQuestion");
        return true;
#endif

#if IOS_SDK
        userFeedback();
        return true;
#endif


        return false;
    }



    // Only For Store.
    public void ConfirmBuy()
    {
#if IOS_SDK || ANDROID_SDK
        if( GlobalConst.IS_ENABLE_TALKING_DATA )
        {
            GoodsAttrConfig goodAttrConfig = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(_buyItemId);

            Logger.Log("ConfirmBuy id=" + _buyItemId);
            Logger.Log("ConfirmBuy _buyItemNum=" + _buyItemNum);
            Logger.Log("ConfirmBuy _buyItemCost=" + _buyItemCost);
            Logger.Log("ConfirmBuy _buyItemCost/_buyItemNum=" + (_buyItemCost / _buyItemNum));
            Logger.Log("ConfirmBuy goodAttrConfig.name=" + goodAttrConfig.name);
            TDGAItem.OnPurchase(goodAttrConfig.name, (int)_buyItemNum, _buyItemCost/_buyItemNum);
        }
#endif
    }
    public void ConfirmCommonBuy(string item, int itemNumber, double priceInVirtualCurrency )
    {

#if IOS_SDK || ANDROID_SDK
        if( GlobalConst.IS_ENABLE_TALKING_DATA )
        {
            TDGAItem.OnPurchase(item, itemNumber, priceInVirtualCurrency);
        }
#endif
    }

    public void Pay(uint id)
    {
#if IOS_SDK
         
#endif

#if ANDROID_SDK
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        
        Recharge recharge;
        if ( GameSystem.Instance.VipPrivilegeConfig.recharges.TryGetValue(id, out recharge) )
        {
            object[] datas = new object[] { id.ToString(),
                recharge.recharge.ToString(),
                "1",
                recharge.name,
                recharge.diamond.ToString(), 
                recharge.des,
                GameSystem.Instance.CommonConfig.GetString("gPayCallbackUrl"),
                "", 
                Level.ToString(), 
                Vip.ToString() };
            jo.Call("Pay", datas);
        }
   
#endif
	}
    //----------------------------------------------------
    //---------- VIP
    
    // Be calling by the Lua.
    public void SetVipExpGoodsBuyTimes(uint goodsId, uint BuyTims)
    {
        if (VipExpGoodsBuyInfo.ContainsKey(goodsId))
        {
            VipExpGoodsBuyInfo[goodsId] = BuyTims;
            return;
        }
        Logger.LogError("SetVipExpGoodsBuyTimes failed to set goodsId=" + goodsId);
    }

    //新手试炼数据
    public uint IsTrialState( int day, int index)
    {
        //if (newComerTrialInfo[day - 1].record.Count >= index)
        //{
//            if (newComerTrialInfo[day - 1].record.Find(x => x.id == index) != null )
//            return newComerTrialInfo[day - 1].record.Find(x => x.id == index).value;
        //}
        return 0;
    }

    public uint IsTrialNotReceive()
    {
//        for(int i = 0; i < newComerTrialInfo.Count; ++i)
//        {
//            for(int j = 0; j < newComerTrialInfo[i].record.Count; ++j)
//            {
//                if (newComerTrialInfo[i].record[j].value == 2)
//                    return 1;
//            }
//        }
        return 0;
    }

    public List<uint> GetTrialRedList()
    {
//        List<uint> redList = new List<uint>();
//        bool flag = false;
//        for (int i = 0; i < newComerTrialInfo.Count; ++i)
//        {
//            for (int j = 0; j < newComerTrialInfo[i].record.Count; ++j)
//            {
//                if (newComerTrialInfo[i].record[j].value == 2)
//                {
//                    flag = true;
//                    continue;
//                }
//            }
//            if (flag == true)
//            {
//                redList.Add(1);
//                flag = false;
//            }
//
//            else
//                redList.Add(0);
//            
//        }
//        return redList;
		return null;
    }
	/// <summary>
	/// 巡回赛奖励列表状态只有在战斗结束和登录游戏才会更新到玩家信息，在领取奖励后需要使用此接口来更新玩家最新的奖励状态
	/// </summary>
	/// <param name="gl">新的巡回赛奖励列表.</param>
	public void ResetQualifyingNewGrades(List<uint> gl)
	{
		if(gl!=null)
		{
			qualifying_new.grade_awards.Clear();
			qualifying_new.grade_awards.AddRange(gl);
		}
	}
    #region libing 2016-3-10
    /// <summary>
    /// 物品表变更
    /// </summary>
    private void OnAllGoodsListChanged()
    {
        IsNewGiftTabDataChanged = true;
        IsNewGoodsTabDataChanged = true;
        IsNewBadgeTabDataChanged = true;
    }

    /* ***********************************************
     * 切页修改道具、礼包、徽章
     * 道具切页显示：主类型3和5的所有道具，主类型为4子类型为3、4、5、6、7的道具
     * 礼包切页显示：主类型为4，子类型为2的道具。
     * ***********************************************/
    private bool IsNewGoodsTabDataChanged = false;
    private Dictionary<ulong, Goods> NewGoodsTabDataList = new Dictionary<ulong, Goods>();
    /// <summary>
    /// 道具切页显示：主类型3和5的所有道具，主类型为4子类型为3、4、5、6、7的道具
    /// </summary>
    /// <param name="goods"></param>
    /// <returns></returns>
    public Dictionary<ulong, Goods> NewGoodsTabData
    {
        get
        {
            if (!IsNewGoodsTabDataChanged)
                return NewGoodsTabDataList;

            IsNewGoodsTabDataChanged = false;
            NewGoodsTabDataList.Clear();
            foreach (var goods in AllGoodsList.Values)
            {
                bool result = false;
                if (goods.GetCategory() == GoodsCategory.GC_FAVORITE)
                    result = true;

                if (goods.GetCategory() == GoodsCategory.GC_EQUIPMENT)
                    result = true;

                if (goods.GetCategory() == GoodsCategory.GC_CONSUME)
                {
                    if (goods.GetSubCategory() == EquipmentType.ET_BRACER ||
                        goods.GetSubCategory() == EquipmentType.ET_PANTS ||
                        goods.GetSubCategory() == EquipmentType.ET_SHOE ||
                        goods.GetSubCategory() == EquipmentType.ET_EQUIPMENTPIECE ||
                        goods.GetSubCategory() == EquipmentType.ET_ALL ||
					    (int)goods.GetSubCategory() == (int)GoodsSubCategory.GSC_BADGE_PAINT)
                    {
                        result = true;
                    }
                }

                if (result)
                    NewGoodsTabDataList.Add(goods.GetUUID(), goods);
            }
            return NewGoodsTabDataList;
        }
    }

    private bool IsNewGiftTabDataChanged = false;
    private Dictionary<ulong, Goods> NewGiftTabDataList = new Dictionary<ulong, Goods>();
    /// <summary>
    /// 礼包切页显示：主类型为4，子类型为2的道具。
    /// </summary>
    /// <param name="goods"></param>
    /// <returns></returns>
    public Dictionary<ulong, Goods> NewGiftTabData
    {
        get
        {
            if (!IsNewGiftTabDataChanged)
                return NewGiftTabDataList;

            IsNewGiftTabDataChanged = false;
            NewGiftTabDataList.Clear();
            foreach (var goods in AllGoodsList.Values)
            {
                if (goods.GetCategory() == GoodsCategory.GC_CONSUME &&
                    goods.GetSubCategory() == EquipmentType.ET_CHEST)
                {
                    NewGiftTabDataList.Add(goods.GetUUID(), goods);
                }
            }

            return NewGiftTabDataList;
        }
    }

    private bool IsNewBadgeTabDataChanged = false;
    private Dictionary<ulong, Goods> NewBadgeTabDataList = new Dictionary<ulong, Goods>();
    /// <summary>
    /// 徽章切页显示：规则未定
    /// </summary>
    /// <param name="goods"></param>
    /// <returns></returns>
    public Dictionary<ulong, Goods> NewBadgeTabData
    {
        get
        {
            if (!IsNewBadgeTabDataChanged)
                return NewBadgeTabDataList;

            IsNewBadgeTabDataChanged = false;
            NewBadgeTabDataList.Clear();
            foreach (var goods in AllGoodsList.Values)
            {
                if (goods.GetCategory() == GoodsCategory.GC_BADGE)
                {
                    NewBadgeTabDataList.Add(goods.GetUUID(), goods);
                }
            }
            return NewBadgeTabDataList;
        }
    }
    //----------------------------------------------------
    //---------- 涂鸦系统数据
    #endregion
    public BadgeSystemInfo badgeSystemInfo = new BadgeSystemInfo();
    //just for test
    
}
