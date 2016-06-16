--encoding=utf-8

SUCCESS = 0
FAILED = 1

ErrorCode =
{
	DB_UNCONNECTION						= 49, --DB未连接
	DB_QUERY_FAILED						= 50, --查询执行失败
	DB_QUERY_EMPTY						= 51, --查询结果为空
	DB_UNKONOW_OPERATION_TYPE			= 52, --

	--Login error code  100~199
	LOGIN_GAME_VERSION_ERROR			= 100, --游戏版本不匹配
	LOGIN_RESOURCE_VERSION_ERROR		= 101, --资源版本不匹配
	LOGIN_VERIFY_FAILED					= 102, --账号平台验证失败
	LOGIN_SERVER_CLOSED					= 103, --服务器未开启
	LOGIN_SERVER_BUSY					= 104, --服务器繁忙
	LOGIN_SERVER_ERROR					= 105, --服务器异常
	INVALID_SESSION						= 106, --非法Session
	LOGIN_ACCOUNT_FAILED				= 107, --
	LOGIN_SERVER_REPEAT					= 108, --重复登录
	LOGIN_EMPTY_CDKEY					= 109, --EMPTY CDKEY
	LOGIN_WRONG_PASSWORD				= 110, --密码错误
	LOGIN_ALREADY_ACCOUNT				= 111, --账号正在验证中
	LOGIN_ANOTHER_PLAYER				= 112, --账号在其他地方登录
	LOGIN_LOADING						= 113, --正在登陆

	FRESHMAN							= 120, --新玩家

	NOT_OWNED_CAPTAIN					= 130, --尚未拥有该队长
	ALREADY_OWNED_CAPTAIN				= 131, --已经拥有该队长
	ALREADY_IN_PLAY						= 132, --出战中

	CREATE_SESSION_FAILED				= 150, --创建Session失败

	NOT_FIND_PLAYER						= 200, --未找到指定玩家
	INVALID_NAME						= 201, --名字非法
	REPEATED_NAME						= 202, --重复命名

	INVALID_NUM							= 210, --数量非法


	REACH_MAX							= 300, --已达上限
	NOT_ENOUGH_MONEY					= 301, --金钱不足
	NOT_ENOUGH_DIAMOND					= 302, --钻石不足
	NOT_ENOUGH_STUFF 					= 303, --所需材料不足
	NOT_ENOUGH_LEVEL					= 304, --等级不足
	
	NOT_ENOUGH_HP						= 305, --体力不足
	NOT_ENOUGH_CHALLENGE_TIMES			= 306, --挑战次数不足
	MATCH_LOSE							= 307, --比赛结果为负
	NOT_ENOUGH_HONOR					= 308, --荣誉不足
	NOT_ENOUGH_SWEEP_CARD				= 309, --扫荡卡不足
	NOT_ENOUGH_PRESTIGE					= 310, --威望不足

	CAREER_SECTION_LOCK					= 350, --关卡未解锁
	CAREER_CHAPTER_LOCK					= 351, --章节未解锁
	CAREER_SECTION_FINISHED				= 352, --关卡已完成
	CAREER_CHAPTER_FINISHED				= 353, --章节已完成
	CAREER_NOT_ALL_SECTION_FINISHED 	= 354, --所有关卡尚未完成
	ADD_SECTION_AWARD_FAILED			= 355, --添加关卡奖励失败
	ADD_CHAPTER_AWARD_FAILED			= 356, --添加章节奖励失败
	CAREER_SECTION_NOT_COMPLETE			= 357, --关卡尚未通关
	CAREER_STAR_NUM_LACK				= 358, --星等不足
	CAREER_STAR_AWARD_ALREADY_GET		= 359, --奖励已领取


	NOT_OWNED_ROLE						= 400, --尚未拥有球员
	ALREADY_OWNED_ROLE					= 401, --已经拥有球员
	NOT_ENOUGH_PIECES					= 402, --碎片数量不足
	LEVEL_LIMIT_QUALITY					= 403, --队长等级限制品质提升

	CANNOT_COMPOSITE_GOODS				= 450, --物品不能合成
	NOT_ENOUGH_COMPOSITE_STUFF			= 451, --合成所需材料不足
	CANNOT_SELL_GOODS					= 452, --物品不能出售
	CANNOT_USE_GOODS					= 453, --物品不能使用
	CANNOT_DECOMPOSE_GOODS				= 454, --物品不能分解
	REACHED_MAX_STACK_NUM				= 455, --已达最大可叠加数量

	SKILL_SLOT_LOCK						= 470, --技能槽位未解锁
	SKILL_SLOT_UNLOCK					= 471, --技能槽位已解锁
	SKILL_SLOT_FILLED					= 472, --槽位已放置技能
	SKILL_SLOT_UNFILLED					= 473, --槽位未放置技能
	SKILL_EQUIPED						= 474, --技能已经装备
	SKILL_UNEQUIPED						= 475, --技能未装备
	NOT_ENOUGH_SKILL_UP_STUFF			= 476, --升级所需材料不足
	NOT_ENOUGH_ATTR_VALUE				= 477, --属性不足

	GOODS_EQUIPED						= 480, --物品已装备
	GOODS_UNEQUIPED						= 481, --物品未装备
	GOODS_SLOT_UNFILLED					= 482, --槽位未装备物品
	LEVEL_LIMIT_GOODS_LEVEL  			= 483, --队伍等级限制物品等级
	EQUIPMENT_POSITION_NOT_MATCH 		= 484, --装备位置和槽位不匹配
	NOT_HAVE_EQUIPMENT					= 485, --连装备都没有，怎么强化呢？
	--EQUIPMENT_REACH_MAX_LEVEL			= 486, --装备都强化满了！真给力！
	EQUIPMENT_NOT_ENOUGH_MONTY			= 487, --先去赚点钱吧，你都快破产了！
	EQUIPMENT_NOT_EXIST					= 488, --你刚刚装备了一件不存在的装备（空气） ！


	STORE_GOODS_SELL_OUT				= 490, --商品已卖光
	BUY_TIME_USE_UP						= 491, --购买次数已用完

	ALREADY_MATCHED						= 500, --已在匹配中
	NOT_ENOUGH_ROLES					= 501, --参赛球员不足

	IN_TRAINING							= 550, --正在训练中
	LEVEL_LIMIT_TRAINING				= 551, --队长等级限制训练提升

	GET_MAIL_ATTACHMENT_FAILED			= 600, --领取附件失败

	REACH_MAX_RESET_TIMES				= 650, --已达最大重置次数
	NEED_GET_THROUGH_FIRST				= 651, --尚未通关
	NOT_ENOUGH_TIMES 					= 652, --次数不足

	TASK_CONDITION_UNFINISHED			= 680, --指定任务未达成	
	GUIDE_CONDITION_UNFINISHED			= 681, --指定指引未达成

	GOODS_OUT_OF_DATE					= 700, --时装已过期
	FASHION_NOT_MATCH_SHAPE				= 701, --时装与体型不符
	GENDER_NOT_MATCH					= 702, --性别不符

	ALREADY_GAMING						= 750, --已经在游戏中，重复操作
	CREATE_ROOM_FAILED					= 751, --创建房间失败
	JOIN_ROOM_FAILED					= 752, --加入房间失败
	ENTER_ROOM_FAILED					= 753, --进入房间失败

	INVALID_OPERATION					= 900, --非法操作

	ERROR_CONFIGURATION					= 920, --配置错误

	UNKNOW_ERROR						= 99999, --未知错误
}