using UnityEngine;
using System.Collections;

public static class GlobalConst
{
    public static bool IS_DEVELOP = true; //开发者模式（true：不开启热更新，false：开启热更新;备注：从版本检查场景启动时，会自动设置为flase）
    public static bool STREAM_ASSETS = false; //资源路径（true：使用persistentDataPath下的打包文件，false：使用Resouces下的xml文件）
    public static string GAME_VERSION = "0.8.41";
    public static string RES_VERSION = "0.8.41";

    public const bool IS_NETWORKING = true; //是否联网
    public const bool IS_DEBUG = false; //是否调试
    public const bool IS_GETFROMASSETBUNDLE = false; //assetbundle 加载
    public const bool IS_OPENORRELEASE = false; //打印debug信息
    public static bool IS_GUIDE = false; //是否需要游戏指引
    public const bool IS_USEBINFILE = false; //是否使用二进制配置文件
    public static bool IS_DEBUG_START_GUIDE = false;
    public static bool IS_ENABLE_TALKING_DATA = false;
    public static uint CHALLENGE_OPEN = 0; //挑战赛开启时间
    public static uint CHALLENGE_CLOSE = 0; //挑战赛结束时间
    public static uint QUALIFYING_TIMES = 0; //排位赛挑战次数
    public static string GShOOTSEQUENCE = ""; //投篮赛顺序
    public static uint GPRACRICECD = 0; //练习赛cd
    public static uint MAX_QUALITY_NUM = 0; //最大品质
    public static string SHOOTCARD_DIAMOND = "";//投篮赛砖石消耗

    public const int VERSION_NUMBER = 1;

    public const string GLOBAL_UPLOADPHOTOIP = "http://182.92.64.167/uploadFile/ReceiveFile.php";
    public const string GLOBAL_DOWNPHOTOIP = "http://182.92.64.167/uploadFile/upload/{0}/{1}.png";
    public const string GLOBAL_OUTNETIP = "182.92.64.167";
    public const string GLOBAL_ASSETSIP = "http://192.168.1.35/";
    public const string ASSETSLISTNAME = "AssetsList.xml";
    public const string DIR_ASSETS = "Assets/";
    public const string DIR_ASSETBUNDLE = "Assetbundle/";
    public const string DIR_ART = "Art_cd/";

    public const string DIR_ANDROID = "Android/";
    public const string DIR_IPHONE = "Iphone/";
    public const string DIR_EDITOR = "Editor/";

    public const string TIMEPATTERN = "{0}:{1:D2}:{2:D2}";


    //------------XML配置文件------
    public const string DIR_XML = "Config/";
    public const string DIR_XML_FUNC = DIR_XML + "Func/";
    /**比赛位置相关配置*/
    public const string DIR_XML_MATCHPOINTS = DIR_XML + "MatchPoints/";
    //比赛位置具体配置
    public const string DIR_XML_MATCHPOINTS_3PTCENTER           = DIR_XML_MATCHPOINTS + "ThreePTCenter";
    public const string DIR_XML_MATCHPOINTS_BEGINPOS            = DIR_XML_MATCHPOINTS + "BeginPos";
    public const string DIR_XML_MATCHPOINTS_BLOCKSTORM_POS      = DIR_XML_MATCHPOINTS + "BlockStorm_Pos";
    public const string DIR_XML_MATCHPOINTS_FREETHROWCENTER     = DIR_XML_MATCHPOINTS + "FreeThrowCenter";
    public const string DIR_XML_MATCHPOINTS_GRABPOINT_POS       = DIR_XML_MATCHPOINTS + "GrabPoint_Pos";
    public const string DIR_XML_MATCHPOINTS_GRABZONE_POS        = DIR_XML_MATCHPOINTS + "GrabZone_Pos";
    public const string DIR_XML_MATCHPOINTS_MASSBALL_POS        = DIR_XML_MATCHPOINTS + "MassBall_Pos";
    public const string DIR_XML_MATCHPOINTS_NPC_GUANZONG01      = DIR_XML_MATCHPOINTS + "NPC_Guanzong01";
    public const string DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS     = DIR_XML_MATCHPOINTS + "PractiseMove_Pos";
    public const string DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS    = DIR_XML_MATCHPOINTS + "ReboundStorm_Pos";
    public const string DIR_XML_MATCHPOINTS_TIPOFFPOS           = DIR_XML_MATCHPOINTS + "TipOffPos";
    public const string DIR_XML_MATCHPOINTS_TWODEFENDER_POS     = DIR_XML_MATCHPOINTS + "TwoDefender_Pos";
    //public const string DIR_XML_MATCHPOINTS_WAYPOINTS           = DIR_XML_MATCHPOINTS + "WayPoints";
    

    //Ip and Port
    public const string DIR_XML_SERVERIPANDENDPOINT = DIR_XML_FUNC + "ServerIpAndEndpoint";
    //Debug
    public const string DIR_XML_DEBUG = DIR_XML_FUNC + "Debug";
    //Match
    public const string DIR_XML_CAMERA = DIR_XML_FUNC + "camera";
    public const string DIR_XML_PLAYGROUND = DIR_XML_FUNC + "playground";
    public const string DIR_XML_MATCH_COMMON = DIR_XML_FUNC + "match_common";
    public const string DIR_XML_MATCH_SINGLE = DIR_XML_FUNC + "match_single";
	public const string DIR_XML_MATCH_PVP = DIR_XML_FUNC + "match_pvp";
    public const string DIR_XML_MATCH_READY = DIR_XML_FUNC + "match_ready";
    public const string DIR_XML_MATCH_FREE_PRACTICE = DIR_XML_FUNC + "match_freePractice";
    public const string DIR_XML_MATCH_MULTIPLY = DIR_XML_FUNC + "match_multiply";
    public const string DIR_XML_MASSBALLREFRESHTIME = DIR_XML_FUNC + "MassBallRefresh";
    public const string DIR_XML_MATCH_GUIDE = DIR_XML_FUNC + "match_guide";
    //常量字符串
    public const string DIR_XML_CONSTSTRING = DIR_XML_FUNC + "ConstString";
    //Common
    public const string DIR_XML_COMMON = DIR_XML_FUNC + "Common";
    //属性
    public const string DIR_XML_ATTRNAME = DIR_XML_FUNC + "AttrName";
    //public const string DIR_XML_CAPTAINATTR = DIR_XML_FUNC + "CaptainAttr";
    //public const string DIR_XML_ROLEATTR = DIR_XML_FUNC + "RoleAttr";
    public const string DIR_XML_ROBOTATTR = DIR_XML_FUNC + "RobotAttr";
    //
    public const string DIR_XML_ROLEBASE = DIR_XML_FUNC + "RoleBase";
    //等级
    public const string DIR_XML_TEAMLEVEL = DIR_XML_FUNC + "TeamLevel";
    public const string DIR_XML_ROLELEVEL = DIR_XML_FUNC + "RoleLevel";
    //NPC
    public const string DIR_XML_NPC = DIR_XML_FUNC + "NPC";
    public const string DIR_XML_TOURNPC = DIR_XML_FUNC + "TourNPCproperty";
    public const string DIR_XML_TOURNPCMODIFY = DIR_XML_FUNC + "NPCmodify";
    //
    public const string DIR_XML_REBOUNDRANGE = DIR_XML_FUNC + "rebound";
    public const string DIR_XML_POSITION = DIR_XML_FUNC + "position";
    public const string DIR_XML_HEIGHT_MAX = DIR_XML_FUNC + "height_max";
    public const string DIR_XML_HEIGHT_MIN = DIR_XML_FUNC + "height_min";
    //生涯模式
    public const string DIR_XML_CHAPTER = DIR_XML_FUNC + "Chapter";
    public const string DIR_XML_SECTION = DIR_XML_FUNC + "Section";
    public const string DIR_XML_CareerAWARDLIB = DIR_XML_FUNC + "CareerAwardLib";
    public const string DIR_XML_AWARDPACK = DIR_XML_FUNC + "AwardPack";
    public const string DIR_XML_PLOT = DIR_XML_FUNC + "Plot";
    public const string DIR_XML_FREQUENCYCONSUME = DIR_XML_FUNC + "FrequencyConsume";
    public const string DIR_XML_STARCONDITION = DIR_XML_FUNC + "StarCondition";
    public const string DIR_XML_ROLEQUALITY = DIR_XML_FUNC + "RoleQuality";
    public const string DIR_XML_ARTICLE_STRENGTH = DIR_XML_FUNC + "ArticleStrength";
    public const string DIR_XML_SECTION_RESET_TIMES = DIR_XML_FUNC + "SectionResetTimes";
    public const string DIR_XML_ROLE_GIFT = DIR_XML_FUNC + "RoleGift"; 
    //排位赛
    public const string DIR_XML_QIALIFYING_RANKAWARDS = DIR_XML_FUNC + "QualifyingRankingAwards";
    public const string DIR_XML_QUALIFYING_DAYWARDS = DIR_XML_FUNC + "QualifyingDayAwards";
    public const string DIR_XML_QUALIFYING_ROBOT = DIR_XML_FUNC + "QualifyingRobot";
	public const string DIR_XML_QUALIFYING_CONSUME = DIR_XML_FUNC + "QualifyingBuyTimes";
	//排位赛（新）
	public const string DIR_XML_QUALIFYING_NEW = DIR_XML_FUNC + "TourNew";
	public const string DIR_XML_QUALIFYING_NEW_SEASON = DIR_XML_FUNC + "TourNewSeason";
	//排位赛（新版）
	public const string DIR_XML_QUALIFYING_NEWER = DIR_XML_FUNC + "QualifyingNewer";
	public const string DIR_XML_QUALIFYING_NEWER_SEASON = DIR_XML_FUNC + "QualifyingNewerSeason";
	public const string DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS = DIR_XML_FUNC + "QualifyingNewerLeagueAwards";
    //物品
    public const string DIR_XML_GOODSATTR = DIR_XML_FUNC + "GoodsAttr";
    public const string DIR_XML_GOODSUSE = DIR_XML_FUNC + "GoodsUse";
    public const string DIR_XML_GOODSCOMPOSITE = DIR_XML_FUNC + "GoodsComposite";
    //属性配置
    public const string DIR_XML_BADGE_ATTRIBUTE = DIR_XML_FUNC + "BadgeAttr";
    //物品合成基础配置
    public const string DIR_XML_GOODS_COMPOSE_NEW = DIR_XML_FUNC + "GoodsCompose";
    //技能
    public const string DIR_XML_SKILL_ATTR = DIR_XML_FUNC + "SkillAttr";
    public const string DIR_XML_SKILL_ACTION = DIR_XML_FUNC + "SkillAction";
    public const string DIR_XML_SKILL_UPGRADE = DIR_XML_FUNC + "SkillUp";
    public const string DIR_XML_SKILL_SLOT = DIR_XML_FUNC + "SkillSlot";
    public const string DIR_XML_SKILL_WEIGHT = DIR_XML_FUNC + "SkillWeight";
	public const string DIR_XML_SKILL_EFFECTS = DIR_XML_FUNC + "SkillEffects";
	public const string DIR_XML_SKILL_LEVEL = DIR_XML_FUNC + "SkillLevel";
	
    //荣誉争霸赛
    public const string DIR_XML_WINNINGSTREAKAWARD = DIR_XML_FUNC + "WinningStreakAward";
    public const string DIR_XML_PVP_POINT = DIR_XML_FUNC + "PVPPoint";
    public const string DIR_XML_PVP_POINT_CHARGE_COST = DIR_XML_FUNC + "BuyRaceTimes";
    //
    public const string DIR_XML_PH_REGAIN = DIR_XML_FUNC + "PhRegain";
    //练习
    public const string DIR_XML_PRACTISE = DIR_XML_FUNC + "Practice";
    public const string DIR_XML_PRACTISEPVE = DIR_XML_FUNC + "PracticePve";
    //玩法
    public const string DIR_XML_GAME_MODE = DIR_XML_FUNC + "GameMode";
	public const string DIR_XML_COMBO_BONUS = DIR_XML_FUNC + "ComboBonus";
    //巡回赛
    public const string DIR_XML_TOUR = DIR_XML_FUNC + "Tour";
    public const string DIR_XML_TOUR_RESET_LIMIT = DIR_XML_FUNC + "TourResetLimit";
    public const string DIR_XML_TOUR_RESET_COST = DIR_XML_FUNC + "TourResetCost";
    //
    public const string DIR_XML_SHOOT_SOLUTION = DIR_XML + "ShootSolution/";
    public const string DIR_XML_ANIMINFO = DIR_XML + "Player/AnimInfo";
    //商店
    public const string DIR_XML_STOREGOODS = DIR_XML_FUNC + "StoreGoods";
    public const string DIR_XML_STOREREFRESHCONSUME = DIR_XML_FUNC + "StoreRefreshConsume";
    //购买金币体力
    public const string DIR_XML_BUYGOLD = DIR_XML_FUNC + "BuyGold";
    public const string DIR_XML_BUYHP = DIR_XML_FUNC + "BuyHp";
    //vip功能
    public const string DIR_XML_VIPPRIVILEGE = DIR_XML_FUNC + "VipPrivilege";
	public const string DIR_XML_VIPPRIVILEGE_STATE = DIR_XML_FUNC + "VipPrivilegestate";
	public const string DIR_XML_RECHARGE = DIR_XML_FUNC + "Recharge";
    //任务配置
    public const string DIR_XML_TASK_MAIN = DIR_XML_FUNC + "TaskMain";
	public const string DIR_XML_TASK_DAILY = DIR_XML_FUNC + "TaskDaily";
	public const string DIR_XML_TASK_LEVEL = DIR_XML_FUNC + "TaskLevel";
    public const string DIR_XML_TASK_CONDITION = DIR_XML_FUNC + "TaskCondition";
    public const string DIR_XML_TASK_PRECONDITION = DIR_XML_FUNC + "TaskPrecondition";
    public const string DIR_XML_TASK_TASKSIGN = DIR_XML_FUNC + "TaskSign";
    public const string DIR_XML_TASK_LINK = DIR_XML_FUNC + "TaskLink";
    //签到
    public const string DIR_XML_DAY_SIGN = DIR_XML_FUNC + "DaySign";
    public const string DIR_XML_MONTH_SIGN = DIR_XML_FUNC + "MonthSign";
    //队长训练
    public const string DIR_XML_CAPTAIN_TRAINING = DIR_XML_FUNC + "Training";
    public const string DIR_XML_CAPTAIN_TRAINING_LEVEL = DIR_XML_FUNC + "TrainingLevel";
    //纹身
    public const string DIR_XML_TATTOO = DIR_XML_FUNC + "Tattoo";
    //装备
    public const string DIR_XML_EQUIPMENT = DIR_XML_FUNC + "Equipment";
	//指引
	public const string DIR_XML_GUIDE_MODULE = DIR_XML_FUNC + "GuideModule";
	public const string DIR_XML_GUIDE_STEP = DIR_XML_FUNC + "GuideStep";
	//操作练习流程
	public const string DIR_XML_PRACTICE_STEP = DIR_XML_FUNC + "PracticeStep";
	//功能开启条件
	public const string DIR_XML_FUNCTION_CONDITION = DIR_XML_FUNC + "FunctionCondition";
	//角色描述
	public const string DIR_XML_BODY_INFO_LIST = DIR_XML_FUNC + "BodyInfoList";
	public const string DIR_XML_ROLE_SHAPE = DIR_XML_FUNC + "RoleShape";
	public const string DIR_XML_FASHION = DIR_XML_FUNC + "Fashion";
	public const string DIR_XML_BONE_MAPPING = DIR_XML_FUNC + "BoneMapping";
    public const string DIR_XML_FASHIONATTR = DIR_XML_FUNC + "FashionAttr";
    public const string DIR_XML_FASHIONDATA = DIR_XML_FUNC + "FashionData";
    //徽章槽位配置文件地址
    public const string DIR_XML_BADGESLOT = DIR_XML_FUNC + "BadgeSlot";
    // Star attr
    public const string DIR_XML_STAR_ATTR = DIR_XML_FUNC + "StarAttr";    
    // Quality attr
    public const string DIR_XML_QUALITY_ATTR = DIR_XML_FUNC + "QualityAttr";
    public const string DIR_XML_QUALITY_ATTR_COR = DIR_XML_FUNC + "QualityAttrCor";
    // 时装商店.
    public const string DIR_XML_FASHION_SHOP = DIR_XML_FUNC + "FashionShop";
	// push
    public const string DIR_XML_PUSH = DIR_XML_FUNC + "Push";
	//场景
	public const string DIR_XML_SCENE = DIR_XML_FUNC + "Scene";
	//比赛内成就
	public const string DIR_XML_MATCH_ACHIEVEMENT = DIR_XML_FUNC + "MatchAchievement";
	//特色动作
	public const string DIR_XML_POSITION_ACTION = DIR_XML_FUNC + "PositionAction";
	public const string DIR_XML_ROLE_ACTION = DIR_XML_FUNC + "RoleAction";
	//抢断动作
	public const string DIR_XML_STEAL_ACTION = DIR_XML_FUNC + "StealAction";
	public const string DIR_XML_STEAL_STATE_RATIO = DIR_XML_FUNC + "StealStateRatio";
	//曲线机率
	public const string DIR_XML_CURVE_RATE = DIR_XML_FUNC + "CurveRate";
	//扣篮发动机率
	public const string DIR_XML_DUNK_RATE = DIR_XML_FUNC + "DunkRate";
	//AI
	public const string DIR_XML_AI = DIR_XML_FUNC + "AI";
	public const string DIR_XML_AI_NAME = DIR_XML_FUNC + "AIName";
    //赠送体力
    public const string DIR_XML_PRESENTHP = DIR_XML_FUNC + "PresentHp";
	//PVE数值削减
	public const string DIR_XML_ATTR_REDUCE = DIR_XML_FUNC + "AttrReduce";
	//抽奖
	public const string DIR_XML_LOTTERY = DIR_XML_FUNC + "Lottery";
	//排行榜
	public const string DIR_XML_RANK = DIR_XML_FUNC + "Rank";
	// 斗牛
	public const string DIR_XML_BULL_FIGHT = DIR_XML_FUNC + "BullFightLevel";
	public const string DIR_XML_SHOOT_MATCH = DIR_XML_FUNC + "ShootMatch";
    public const string DIR_XML_BULLFIGHT_CONSUME = DIR_XML_FUNC + "BullFightBuyTimes";
    //投篮
    public const string DIR_XML_SHOOT_GAME = DIR_XML_FUNC + "ShootGame";
    public const string DIR_XML_SHOOT_GAME_CONSUME = DIR_XML_FUNC + "ShootBuyTimes";
	//对冲
	public const string DIR_XML_HEDGING = DIR_XML_FUNC + "Hedging";
    //公告
    public const string DIR_XML_ANNOUNCEMENT = DIR_XML_FUNC + "Announcement";
	//新人七连送
    public const string DIR_XML_NEWCOMERSIGN = DIR_XML_FUNC + "NewComerSign";
	//战斗力相关
	public const string DIR_XML_EXPECTED_SCORE_DIFF = DIR_XML_FUNC + "ExpectedScoreDiff";
	public const string DIR_XML_ATTR_ENHANCE = DIR_XML_FUNC + "AttrEnhance";

	public const string DIR_XML_POTENTIAL_EFFECT = DIR_XML_FUNC + "PotentialEffect";
    //图鉴
    public const string DIR_XML_MAP = DIR_XML_FUNC + "Map";
    //活跃度
    public const string DIR_XML_ACTIVITY = DIR_XML_FUNC + "Activity";
    //新手试炼
    public const string DIR_XML_TRAL = DIR_XML_FUNC + "Trial";

    // 天赋配置表
    public const string DIR_XML_TALENT = DIR_XML_FUNC + "Talent";

    // 天梯
    public const string DIR_XML_LADDER_LEVEL = DIR_XML_FUNC + "LadderLevel";
    public const string DIR_XML_LADDER_SEASON = DIR_XML_FUNC + "LadderSeason";
    public const string DIR_XML_LADDER_REWARD = DIR_XML_FUNC + "LadderReward";

	//比赛音效配置
	public const string DIR_XML_MATCH_SOUND = DIR_XML_FUNC + "MatchSound";

	public const string DIR_XML_MATCH_MSG = DIR_XML_FUNC + "MatchMsg";
	
    //------------Perfab------
    //
    //public const string DIR_UI_CAMERAOBJ = "Prefabs_Model/ModelCamera";
    //
    public const string DIR_EFFECT = "Prefabs_Effects/";


    //------------音效配置------
    //
    public const string MUS_BGMLOGIN = "Audio/UI/Login";
    public const string MUS_BGMHALL = "Audio/UI/Hall";
    public const string MUS_BGMGAME = "Audio/UI/Game";




    //配置注释
    public const string CONFIG_SWITCH_COLUMN = "switch";
    public const string CONFIG_SWITCH = "#";

    //Scene常量
    public const string SCENE_STARTUP = "Startup";
    public const string SCENE_HALL = "Hall";
    public const string SCENE_MATCH = "Match";
    public const string SCENE_GAME = "Game";

    //特殊物品ID
    public static uint DIAMOND_ID = 1; //钻石
    public static uint GOLD_ID = 2; //金币
    public static uint HONOR_ID = 3; //荣誉
    public static uint HP_ID = 4; //体力
    public static uint TEAM_EXP_ID = 5; //球队经验
    public static uint ROLE_EXP_ID = 6; //球员经验
    public static uint PRESTIGE_ID = 7; //威望
    public static uint HONOR2_ID = 8; //荣誉2
    public static uint PRESTIGE2_ID = 9; //威望2
    public static uint REPUTATION_ID = 10; //声望

    public static uint IS_FASHION_OPEN = 0; //是否开放服装商城

    public static uint SWEEP_CARD_ID = 4000; //扫荡卡ID

    //物品品质颜色
    public static Color[] QUALITY_COLOR = new Color[5];
    public static Color32 MATCH_TIP_COLOR_RED = new Color32(255, 75, 12, 255);

	//
	public static float CONTROLLER_RAD = 0.9f;

	public static int	PT_2 = 2;
	public static int	PT_3 = 3;

	public static IM.Number OPEN_SHOOT_CYCLE_RADIUS = IM.Number.one;
	public static IM.Number OPEN_SHOOT_FAN_RADIUS = new IM.Number(2,500);
	public static IM.Number OPEN_SHOOT_FAN_ANGLE = new IM.Number(80);

	public static uint PVP_VALID_LATENCY = 100; //PVP网速限制

    public static uint ALL_HEDGING_ID = 100;

	public static IM.Number MATCHED_KEY_BLOCK_RATE_ADJUST = IM.Number.one;

    public static IM.Number BACK_TO_BACK_FAN_ANGLE = new IM.Number(80);
    public static IM.Number BACK_TO_BACK_FAN_RADIUS = IM.Number.one; 

	public static IM.Number CHARACTER_SKIN_WIDTH = new IM.Number(0,1);
}


