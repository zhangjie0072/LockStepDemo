using System;
using UnityEngine;
using LuaInterface;

public class GlobalConstWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateGlobalConst),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("IS_NETWORKING", get_IS_NETWORKING, null),
			new LuaField("IS_DEBUG", get_IS_DEBUG, null),
			new LuaField("IS_GETFROMASSETBUNDLE", get_IS_GETFROMASSETBUNDLE, null),
			new LuaField("IS_OPENORRELEASE", get_IS_OPENORRELEASE, null),
			new LuaField("IS_USEBINFILE", get_IS_USEBINFILE, null),
			new LuaField("VERSION_NUMBER", get_VERSION_NUMBER, null),
			new LuaField("GLOBAL_UPLOADPHOTOIP", get_GLOBAL_UPLOADPHOTOIP, null),
			new LuaField("GLOBAL_DOWNPHOTOIP", get_GLOBAL_DOWNPHOTOIP, null),
			new LuaField("GLOBAL_OUTNETIP", get_GLOBAL_OUTNETIP, null),
			new LuaField("GLOBAL_ASSETSIP", get_GLOBAL_ASSETSIP, null),
			new LuaField("ASSETSLISTNAME", get_ASSETSLISTNAME, null),
			new LuaField("DIR_ASSETS", get_DIR_ASSETS, null),
			new LuaField("DIR_ASSETBUNDLE", get_DIR_ASSETBUNDLE, null),
			new LuaField("DIR_ART", get_DIR_ART, null),
			new LuaField("DIR_ANDROID", get_DIR_ANDROID, null),
			new LuaField("DIR_IPHONE", get_DIR_IPHONE, null),
			new LuaField("DIR_EDITOR", get_DIR_EDITOR, null),
			new LuaField("TIMEPATTERN", get_TIMEPATTERN, null),
			new LuaField("DIR_XML", get_DIR_XML, null),
			new LuaField("DIR_XML_FUNC", get_DIR_XML_FUNC, null),
			new LuaField("DIR_XML_MATCHPOINTS", get_DIR_XML_MATCHPOINTS, null),
			new LuaField("DIR_XML_MATCHPOINTS_3PTCENTER", get_DIR_XML_MATCHPOINTS_3PTCENTER, null),
			new LuaField("DIR_XML_MATCHPOINTS_BEGINPOS", get_DIR_XML_MATCHPOINTS_BEGINPOS, null),
			new LuaField("DIR_XML_MATCHPOINTS_BLOCKSTORM_POS", get_DIR_XML_MATCHPOINTS_BLOCKSTORM_POS, null),
			new LuaField("DIR_XML_MATCHPOINTS_FREETHROWCENTER", get_DIR_XML_MATCHPOINTS_FREETHROWCENTER, null),
			new LuaField("DIR_XML_MATCHPOINTS_GRABPOINT_POS", get_DIR_XML_MATCHPOINTS_GRABPOINT_POS, null),
			new LuaField("DIR_XML_MATCHPOINTS_GRABZONE_POS", get_DIR_XML_MATCHPOINTS_GRABZONE_POS, null),
			new LuaField("DIR_XML_MATCHPOINTS_MASSBALL_POS", get_DIR_XML_MATCHPOINTS_MASSBALL_POS, null),
			new LuaField("DIR_XML_MATCHPOINTS_NPC_GUANZONG01", get_DIR_XML_MATCHPOINTS_NPC_GUANZONG01, null),
			new LuaField("DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS", get_DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS, null),
			new LuaField("DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS", get_DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS, null),
			new LuaField("DIR_XML_MATCHPOINTS_TIPOFFPOS", get_DIR_XML_MATCHPOINTS_TIPOFFPOS, null),
			new LuaField("DIR_XML_MATCHPOINTS_TWODEFENDER_POS", get_DIR_XML_MATCHPOINTS_TWODEFENDER_POS, null),
			new LuaField("DIR_XML_SERVERIPANDENDPOINT", get_DIR_XML_SERVERIPANDENDPOINT, null),
			new LuaField("DIR_XML_DEBUG", get_DIR_XML_DEBUG, null),
			new LuaField("DIR_XML_CAMERA", get_DIR_XML_CAMERA, null),
			new LuaField("DIR_XML_PLAYGROUND", get_DIR_XML_PLAYGROUND, null),
			new LuaField("DIR_XML_MATCH_COMMON", get_DIR_XML_MATCH_COMMON, null),
			new LuaField("DIR_XML_MATCH_SINGLE", get_DIR_XML_MATCH_SINGLE, null),
			new LuaField("DIR_XML_MATCH_PVP", get_DIR_XML_MATCH_PVP, null),
			new LuaField("DIR_XML_MATCH_READY", get_DIR_XML_MATCH_READY, null),
			new LuaField("DIR_XML_MATCH_FREE_PRACTICE", get_DIR_XML_MATCH_FREE_PRACTICE, null),
			new LuaField("DIR_XML_MATCH_MULTIPLY", get_DIR_XML_MATCH_MULTIPLY, null),
			new LuaField("DIR_XML_MASSBALLREFRESHTIME", get_DIR_XML_MASSBALLREFRESHTIME, null),
			new LuaField("DIR_XML_MATCH_GUIDE", get_DIR_XML_MATCH_GUIDE, null),
			new LuaField("DIR_XML_CONSTSTRING", get_DIR_XML_CONSTSTRING, null),
			new LuaField("DIR_XML_COMMON", get_DIR_XML_COMMON, null),
			new LuaField("DIR_XML_ATTRNAME", get_DIR_XML_ATTRNAME, null),
			new LuaField("DIR_XML_ROBOTATTR", get_DIR_XML_ROBOTATTR, null),
			new LuaField("DIR_XML_ROLEBASE", get_DIR_XML_ROLEBASE, null),
			new LuaField("DIR_XML_TEAMLEVEL", get_DIR_XML_TEAMLEVEL, null),
			new LuaField("DIR_XML_ROLELEVEL", get_DIR_XML_ROLELEVEL, null),
			new LuaField("DIR_XML_NPC", get_DIR_XML_NPC, null),
			new LuaField("DIR_XML_TOURNPC", get_DIR_XML_TOURNPC, null),
			new LuaField("DIR_XML_TOURNPCMODIFY", get_DIR_XML_TOURNPCMODIFY, null),
			new LuaField("DIR_XML_REBOUNDRANGE", get_DIR_XML_REBOUNDRANGE, null),
			new LuaField("DIR_XML_POSITION", get_DIR_XML_POSITION, null),
			new LuaField("DIR_XML_HEIGHT_MAX", get_DIR_XML_HEIGHT_MAX, null),
			new LuaField("DIR_XML_HEIGHT_MIN", get_DIR_XML_HEIGHT_MIN, null),
			new LuaField("DIR_XML_CHAPTER", get_DIR_XML_CHAPTER, null),
			new LuaField("DIR_XML_SECTION", get_DIR_XML_SECTION, null),
			new LuaField("DIR_XML_CareerAWARDLIB", get_DIR_XML_CareerAWARDLIB, null),
			new LuaField("DIR_XML_AWARDPACK", get_DIR_XML_AWARDPACK, null),
			new LuaField("DIR_XML_PLOT", get_DIR_XML_PLOT, null),
			new LuaField("DIR_XML_FREQUENCYCONSUME", get_DIR_XML_FREQUENCYCONSUME, null),
			new LuaField("DIR_XML_STARCONDITION", get_DIR_XML_STARCONDITION, null),
			new LuaField("DIR_XML_ROLEQUALITY", get_DIR_XML_ROLEQUALITY, null),
			new LuaField("DIR_XML_ARTICLE_STRENGTH", get_DIR_XML_ARTICLE_STRENGTH, null),
			new LuaField("DIR_XML_SECTION_RESET_TIMES", get_DIR_XML_SECTION_RESET_TIMES, null),
			new LuaField("DIR_XML_ROLE_GIFT", get_DIR_XML_ROLE_GIFT, null),
			new LuaField("DIR_XML_QIALIFYING_RANKAWARDS", get_DIR_XML_QIALIFYING_RANKAWARDS, null),
			new LuaField("DIR_XML_QUALIFYING_DAYWARDS", get_DIR_XML_QUALIFYING_DAYWARDS, null),
			new LuaField("DIR_XML_QUALIFYING_ROBOT", get_DIR_XML_QUALIFYING_ROBOT, null),
			new LuaField("DIR_XML_QUALIFYING_CONSUME", get_DIR_XML_QUALIFYING_CONSUME, null),
			new LuaField("DIR_XML_QUALIFYING_NEW", get_DIR_XML_QUALIFYING_NEW, null),
			new LuaField("DIR_XML_QUALIFYING_NEW_SEASON", get_DIR_XML_QUALIFYING_NEW_SEASON, null),
			new LuaField("DIR_XML_QUALIFYING_NEWER", get_DIR_XML_QUALIFYING_NEWER, null),
			new LuaField("DIR_XML_QUALIFYING_NEWER_SEASON", get_DIR_XML_QUALIFYING_NEWER_SEASON, null),
			new LuaField("DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS", get_DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS, null),
			new LuaField("DIR_XML_GOODSATTR", get_DIR_XML_GOODSATTR, null),
			new LuaField("DIR_XML_GOODSUSE", get_DIR_XML_GOODSUSE, null),
			new LuaField("DIR_XML_GOODSCOMPOSITE", get_DIR_XML_GOODSCOMPOSITE, null),
			new LuaField("DIR_XML_BADGE_ATTRIBUTE", get_DIR_XML_BADGE_ATTRIBUTE, null),
			new LuaField("DIR_XML_GOODS_COMPOSE_NEW", get_DIR_XML_GOODS_COMPOSE_NEW, null),
			new LuaField("DIR_XML_SKILL_ATTR", get_DIR_XML_SKILL_ATTR, null),
			new LuaField("DIR_XML_SKILL_ACTION", get_DIR_XML_SKILL_ACTION, null),
			new LuaField("DIR_XML_SKILL_UPGRADE", get_DIR_XML_SKILL_UPGRADE, null),
			new LuaField("DIR_XML_SKILL_SLOT", get_DIR_XML_SKILL_SLOT, null),
			new LuaField("DIR_XML_SKILL_WEIGHT", get_DIR_XML_SKILL_WEIGHT, null),
			new LuaField("DIR_XML_SKILL_EFFECTS", get_DIR_XML_SKILL_EFFECTS, null),
			new LuaField("DIR_XML_SKILL_LEVEL", get_DIR_XML_SKILL_LEVEL, null),
			new LuaField("DIR_XML_WINNINGSTREAKAWARD", get_DIR_XML_WINNINGSTREAKAWARD, null),
			new LuaField("DIR_XML_PVP_POINT", get_DIR_XML_PVP_POINT, null),
			new LuaField("DIR_XML_PVP_POINT_CHARGE_COST", get_DIR_XML_PVP_POINT_CHARGE_COST, null),
			new LuaField("DIR_XML_PH_REGAIN", get_DIR_XML_PH_REGAIN, null),
			new LuaField("DIR_XML_PRACTISE", get_DIR_XML_PRACTISE, null),
			new LuaField("DIR_XML_PRACTISEPVE", get_DIR_XML_PRACTISEPVE, null),
			new LuaField("DIR_XML_GAME_MODE", get_DIR_XML_GAME_MODE, null),
			new LuaField("DIR_XML_COMBO_BONUS", get_DIR_XML_COMBO_BONUS, null),
			new LuaField("DIR_XML_TOUR", get_DIR_XML_TOUR, null),
			new LuaField("DIR_XML_TOUR_RESET_LIMIT", get_DIR_XML_TOUR_RESET_LIMIT, null),
			new LuaField("DIR_XML_TOUR_RESET_COST", get_DIR_XML_TOUR_RESET_COST, null),
			new LuaField("DIR_XML_SHOOT_SOLUTION", get_DIR_XML_SHOOT_SOLUTION, null),
			new LuaField("DIR_XML_ANIMINFO", get_DIR_XML_ANIMINFO, null),
			new LuaField("DIR_XML_STOREGOODS", get_DIR_XML_STOREGOODS, null),
			new LuaField("DIR_XML_STOREREFRESHCONSUME", get_DIR_XML_STOREREFRESHCONSUME, null),
			new LuaField("DIR_XML_BUYGOLD", get_DIR_XML_BUYGOLD, null),
			new LuaField("DIR_XML_BUYHP", get_DIR_XML_BUYHP, null),
			new LuaField("DIR_XML_VIPPRIVILEGE", get_DIR_XML_VIPPRIVILEGE, null),
			new LuaField("DIR_XML_VIPPRIVILEGE_STATE", get_DIR_XML_VIPPRIVILEGE_STATE, null),
			new LuaField("DIR_XML_RECHARGE", get_DIR_XML_RECHARGE, null),
			new LuaField("DIR_XML_TASK_MAIN", get_DIR_XML_TASK_MAIN, null),
			new LuaField("DIR_XML_TASK_DAILY", get_DIR_XML_TASK_DAILY, null),
			new LuaField("DIR_XML_TASK_LEVEL", get_DIR_XML_TASK_LEVEL, null),
			new LuaField("DIR_XML_TASK_CONDITION", get_DIR_XML_TASK_CONDITION, null),
			new LuaField("DIR_XML_TASK_PRECONDITION", get_DIR_XML_TASK_PRECONDITION, null),
			new LuaField("DIR_XML_TASK_TASKSIGN", get_DIR_XML_TASK_TASKSIGN, null),
			new LuaField("DIR_XML_TASK_LINK", get_DIR_XML_TASK_LINK, null),
			new LuaField("DIR_XML_DAY_SIGN", get_DIR_XML_DAY_SIGN, null),
			new LuaField("DIR_XML_MONTH_SIGN", get_DIR_XML_MONTH_SIGN, null),
			new LuaField("DIR_XML_CAPTAIN_TRAINING", get_DIR_XML_CAPTAIN_TRAINING, null),
			new LuaField("DIR_XML_CAPTAIN_TRAINING_LEVEL", get_DIR_XML_CAPTAIN_TRAINING_LEVEL, null),
			new LuaField("DIR_XML_TATTOO", get_DIR_XML_TATTOO, null),
			new LuaField("DIR_XML_EQUIPMENT", get_DIR_XML_EQUIPMENT, null),
			new LuaField("DIR_XML_GUIDE_MODULE", get_DIR_XML_GUIDE_MODULE, null),
			new LuaField("DIR_XML_GUIDE_STEP", get_DIR_XML_GUIDE_STEP, null),
			new LuaField("DIR_XML_PRACTICE_STEP", get_DIR_XML_PRACTICE_STEP, null),
			new LuaField("DIR_XML_FUNCTION_CONDITION", get_DIR_XML_FUNCTION_CONDITION, null),
			new LuaField("DIR_XML_BODY_INFO_LIST", get_DIR_XML_BODY_INFO_LIST, null),
			new LuaField("DIR_XML_ROLE_SHAPE", get_DIR_XML_ROLE_SHAPE, null),
			new LuaField("DIR_XML_FASHION", get_DIR_XML_FASHION, null),
			new LuaField("DIR_XML_BONE_MAPPING", get_DIR_XML_BONE_MAPPING, null),
			new LuaField("DIR_XML_FASHIONATTR", get_DIR_XML_FASHIONATTR, null),
			new LuaField("DIR_XML_FASHIONDATA", get_DIR_XML_FASHIONDATA, null),
			new LuaField("DIR_XML_BADGESLOT", get_DIR_XML_BADGESLOT, null),
			new LuaField("DIR_XML_STAR_ATTR", get_DIR_XML_STAR_ATTR, null),
			new LuaField("DIR_XML_QUALITY_ATTR", get_DIR_XML_QUALITY_ATTR, null),
			new LuaField("DIR_XML_QUALITY_ATTR_COR", get_DIR_XML_QUALITY_ATTR_COR, null),
			new LuaField("DIR_XML_FASHION_SHOP", get_DIR_XML_FASHION_SHOP, null),
			new LuaField("DIR_XML_PUSH", get_DIR_XML_PUSH, null),
			new LuaField("DIR_XML_SCENE", get_DIR_XML_SCENE, null),
			new LuaField("DIR_XML_MATCH_ACHIEVEMENT", get_DIR_XML_MATCH_ACHIEVEMENT, null),
			new LuaField("DIR_XML_POSITION_ACTION", get_DIR_XML_POSITION_ACTION, null),
			new LuaField("DIR_XML_ROLE_ACTION", get_DIR_XML_ROLE_ACTION, null),
			new LuaField("DIR_XML_STEAL_ACTION", get_DIR_XML_STEAL_ACTION, null),
			new LuaField("DIR_XML_STEAL_STATE_RATIO", get_DIR_XML_STEAL_STATE_RATIO, null),
			new LuaField("DIR_XML_CURVE_RATE", get_DIR_XML_CURVE_RATE, null),
			new LuaField("DIR_XML_DUNK_RATE", get_DIR_XML_DUNK_RATE, null),
			new LuaField("DIR_XML_AI", get_DIR_XML_AI, null),
			new LuaField("DIR_XML_AI_NAME", get_DIR_XML_AI_NAME, null),
			new LuaField("DIR_XML_PRESENTHP", get_DIR_XML_PRESENTHP, null),
			new LuaField("DIR_XML_ATTR_REDUCE", get_DIR_XML_ATTR_REDUCE, null),
			new LuaField("DIR_XML_LOTTERY", get_DIR_XML_LOTTERY, null),
			new LuaField("DIR_XML_RANK", get_DIR_XML_RANK, null),
			new LuaField("DIR_XML_BULL_FIGHT", get_DIR_XML_BULL_FIGHT, null),
			new LuaField("DIR_XML_SHOOT_MATCH", get_DIR_XML_SHOOT_MATCH, null),
			new LuaField("DIR_XML_BULLFIGHT_CONSUME", get_DIR_XML_BULLFIGHT_CONSUME, null),
			new LuaField("DIR_XML_SHOOT_GAME", get_DIR_XML_SHOOT_GAME, null),
			new LuaField("DIR_XML_SHOOT_GAME_CONSUME", get_DIR_XML_SHOOT_GAME_CONSUME, null),
			new LuaField("DIR_XML_HEDGING", get_DIR_XML_HEDGING, null),
			new LuaField("DIR_XML_ANNOUNCEMENT", get_DIR_XML_ANNOUNCEMENT, null),
			new LuaField("DIR_XML_NEWCOMERSIGN", get_DIR_XML_NEWCOMERSIGN, null),
			new LuaField("DIR_XML_EXPECTED_SCORE_DIFF", get_DIR_XML_EXPECTED_SCORE_DIFF, null),
			new LuaField("DIR_XML_ATTR_ENHANCE", get_DIR_XML_ATTR_ENHANCE, null),
			new LuaField("DIR_XML_POTENTIAL_EFFECT", get_DIR_XML_POTENTIAL_EFFECT, null),
			new LuaField("DIR_XML_MAP", get_DIR_XML_MAP, null),
			new LuaField("DIR_XML_ACTIVITY", get_DIR_XML_ACTIVITY, null),
			new LuaField("DIR_XML_TRAL", get_DIR_XML_TRAL, null),
			new LuaField("DIR_XML_TALENT", get_DIR_XML_TALENT, null),
			new LuaField("DIR_XML_LADDER_LEVEL", get_DIR_XML_LADDER_LEVEL, null),
			new LuaField("DIR_XML_LADDER_SEASON", get_DIR_XML_LADDER_SEASON, null),
			new LuaField("DIR_XML_LADDER_REWARD", get_DIR_XML_LADDER_REWARD, null),
			new LuaField("DIR_XML_MATCH_SOUND", get_DIR_XML_MATCH_SOUND, null),
			new LuaField("DIR_XML_MATCH_MSG", get_DIR_XML_MATCH_MSG, null),
			new LuaField("DIR_EFFECT", get_DIR_EFFECT, null),
			new LuaField("MUS_BGMLOGIN", get_MUS_BGMLOGIN, null),
			new LuaField("MUS_BGMHALL", get_MUS_BGMHALL, null),
			new LuaField("MUS_BGMGAME", get_MUS_BGMGAME, null),
			new LuaField("CONFIG_SWITCH_COLUMN", get_CONFIG_SWITCH_COLUMN, null),
			new LuaField("CONFIG_SWITCH", get_CONFIG_SWITCH, null),
			new LuaField("SCENE_STARTUP", get_SCENE_STARTUP, null),
			new LuaField("SCENE_HALL", get_SCENE_HALL, null),
			new LuaField("SCENE_MATCH", get_SCENE_MATCH, null),
			new LuaField("SCENE_GAME", get_SCENE_GAME, null),
			new LuaField("IS_DEVELOP", get_IS_DEVELOP, set_IS_DEVELOP),
			new LuaField("STREAM_ASSETS", get_STREAM_ASSETS, set_STREAM_ASSETS),
			new LuaField("GAME_VERSION", get_GAME_VERSION, set_GAME_VERSION),
			new LuaField("RES_VERSION", get_RES_VERSION, set_RES_VERSION),
			new LuaField("IS_GUIDE", get_IS_GUIDE, set_IS_GUIDE),
			new LuaField("IS_DEBUG_START_GUIDE", get_IS_DEBUG_START_GUIDE, set_IS_DEBUG_START_GUIDE),
			new LuaField("IS_ENABLE_TALKING_DATA", get_IS_ENABLE_TALKING_DATA, set_IS_ENABLE_TALKING_DATA),
			new LuaField("CHALLENGE_OPEN", get_CHALLENGE_OPEN, set_CHALLENGE_OPEN),
			new LuaField("CHALLENGE_CLOSE", get_CHALLENGE_CLOSE, set_CHALLENGE_CLOSE),
			new LuaField("QUALIFYING_TIMES", get_QUALIFYING_TIMES, set_QUALIFYING_TIMES),
			new LuaField("GShOOTSEQUENCE", get_GShOOTSEQUENCE, set_GShOOTSEQUENCE),
			new LuaField("GPRACRICECD", get_GPRACRICECD, set_GPRACRICECD),
			new LuaField("MAX_QUALITY_NUM", get_MAX_QUALITY_NUM, set_MAX_QUALITY_NUM),
			new LuaField("SHOOTCARD_DIAMOND", get_SHOOTCARD_DIAMOND, set_SHOOTCARD_DIAMOND),
			new LuaField("DIAMOND_ID", get_DIAMOND_ID, set_DIAMOND_ID),
			new LuaField("GOLD_ID", get_GOLD_ID, set_GOLD_ID),
			new LuaField("HONOR_ID", get_HONOR_ID, set_HONOR_ID),
			new LuaField("HP_ID", get_HP_ID, set_HP_ID),
			new LuaField("TEAM_EXP_ID", get_TEAM_EXP_ID, set_TEAM_EXP_ID),
			new LuaField("ROLE_EXP_ID", get_ROLE_EXP_ID, set_ROLE_EXP_ID),
			new LuaField("PRESTIGE_ID", get_PRESTIGE_ID, set_PRESTIGE_ID),
			new LuaField("HONOR2_ID", get_HONOR2_ID, set_HONOR2_ID),
			new LuaField("PRESTIGE2_ID", get_PRESTIGE2_ID, set_PRESTIGE2_ID),
			new LuaField("REPUTATION_ID", get_REPUTATION_ID, set_REPUTATION_ID),
			new LuaField("IS_FASHION_OPEN", get_IS_FASHION_OPEN, set_IS_FASHION_OPEN),
			new LuaField("SWEEP_CARD_ID", get_SWEEP_CARD_ID, set_SWEEP_CARD_ID),
			new LuaField("QUALITY_COLOR", get_QUALITY_COLOR, set_QUALITY_COLOR),
			new LuaField("MATCH_TIP_COLOR_RED", get_MATCH_TIP_COLOR_RED, set_MATCH_TIP_COLOR_RED),
			new LuaField("CONTROLLER_RAD", get_CONTROLLER_RAD, set_CONTROLLER_RAD),
			new LuaField("PT_2", get_PT_2, set_PT_2),
			new LuaField("PT_3", get_PT_3, set_PT_3),
			new LuaField("OPEN_SHOOT_CYCLE_RADIUS", get_OPEN_SHOOT_CYCLE_RADIUS, set_OPEN_SHOOT_CYCLE_RADIUS),
			new LuaField("OPEN_SHOOT_FAN_RADIUS", get_OPEN_SHOOT_FAN_RADIUS, set_OPEN_SHOOT_FAN_RADIUS),
			new LuaField("OPEN_SHOOT_FAN_ANGLE", get_OPEN_SHOOT_FAN_ANGLE, set_OPEN_SHOOT_FAN_ANGLE),
			new LuaField("PVP_VALID_LATENCY", get_PVP_VALID_LATENCY, set_PVP_VALID_LATENCY),
			new LuaField("ALL_HEDGING_ID", get_ALL_HEDGING_ID, set_ALL_HEDGING_ID),
			new LuaField("MATCHED_KEY_BLOCK_RATE_ADJUST", get_MATCHED_KEY_BLOCK_RATE_ADJUST, set_MATCHED_KEY_BLOCK_RATE_ADJUST),
			new LuaField("BACK_TO_BACK_FAN_ANGLE", get_BACK_TO_BACK_FAN_ANGLE, set_BACK_TO_BACK_FAN_ANGLE),
			new LuaField("BACK_TO_BACK_FAN_RADIUS", get_BACK_TO_BACK_FAN_RADIUS, set_BACK_TO_BACK_FAN_RADIUS),
			new LuaField("CHARACTER_SKIN_WIDTH", get_CHARACTER_SKIN_WIDTH, set_CHARACTER_SKIN_WIDTH),
		};

		LuaScriptMgr.RegisterLib(L, "GlobalConst", typeof(GlobalConst), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGlobalConst(IntPtr L)
	{
		LuaDLL.luaL_error(L, "GlobalConst class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(GlobalConst);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_NETWORKING(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_NETWORKING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_DEBUG(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_DEBUG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_GETFROMASSETBUNDLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_GETFROMASSETBUNDLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_OPENORRELEASE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_OPENORRELEASE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_USEBINFILE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_USEBINFILE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VERSION_NUMBER(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.VERSION_NUMBER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GLOBAL_UPLOADPHOTOIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GLOBAL_UPLOADPHOTOIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GLOBAL_DOWNPHOTOIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GLOBAL_DOWNPHOTOIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GLOBAL_OUTNETIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GLOBAL_OUTNETIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GLOBAL_ASSETSIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GLOBAL_ASSETSIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ASSETSLISTNAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.ASSETSLISTNAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_ASSETS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_ASSETS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_ASSETBUNDLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_ASSETBUNDLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_ART(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_ART);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_ANDROID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_ANDROID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_IPHONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_IPHONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_EDITOR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_EDITOR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TIMEPATTERN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.TIMEPATTERN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FUNC(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FUNC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_3PTCENTER(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_3PTCENTER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_BEGINPOS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_BEGINPOS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_BLOCKSTORM_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_BLOCKSTORM_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_FREETHROWCENTER(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_FREETHROWCENTER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_GRABPOINT_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_GRABPOINT_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_GRABZONE_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_GRABZONE_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_MASSBALL_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_MASSBALL_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_NPC_GUANZONG01(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_NPC_GUANZONG01);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_PRACTISEMOVEPOS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_REBOUNDSTORM_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_TIPOFFPOS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_TIPOFFPOS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCHPOINTS_TWODEFENDER_POS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCHPOINTS_TWODEFENDER_POS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SERVERIPANDENDPOINT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SERVERIPANDENDPOINT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_DEBUG(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_DEBUG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CAMERA(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CAMERA);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PLAYGROUND(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PLAYGROUND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_COMMON(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_COMMON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_SINGLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_SINGLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_PVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_PVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_READY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_READY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_FREE_PRACTICE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_FREE_PRACTICE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_MULTIPLY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_MULTIPLY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MASSBALLREFRESHTIME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MASSBALLREFRESHTIME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_GUIDE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_GUIDE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CONSTSTRING(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CONSTSTRING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_COMMON(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_COMMON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ATTRNAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ATTRNAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROBOTATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROBOTATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLEBASE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLEBASE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TEAMLEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TEAMLEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLELEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLELEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_NPC(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_NPC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TOURNPC(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TOURNPC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TOURNPCMODIFY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TOURNPCMODIFY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_REBOUNDRANGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_REBOUNDRANGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_POSITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_POSITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_HEIGHT_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_HEIGHT_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_HEIGHT_MIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_HEIGHT_MIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CHAPTER(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CHAPTER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SECTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SECTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CareerAWARDLIB(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CareerAWARDLIB);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_AWARDPACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_AWARDPACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PLOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PLOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FREQUENCYCONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FREQUENCYCONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STARCONDITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STARCONDITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLEQUALITY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLEQUALITY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ARTICLE_STRENGTH(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ARTICLE_STRENGTH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SECTION_RESET_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SECTION_RESET_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLE_GIFT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLE_GIFT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QIALIFYING_RANKAWARDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QIALIFYING_RANKAWARDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_DAYWARDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_DAYWARDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_ROBOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_ROBOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_CONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_CONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_NEW(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_NEW);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_NEW_SEASON(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_NEW_SEASON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_NEWER(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_NEWER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_NEWER_SEASON(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_NEWER_SEASON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GOODSATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GOODSATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GOODSUSE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GOODSUSE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GOODSCOMPOSITE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GOODSCOMPOSITE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BADGE_ATTRIBUTE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BADGE_ATTRIBUTE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GOODS_COMPOSE_NEW(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GOODS_COMPOSE_NEW);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_ATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_ATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_ACTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_ACTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_UPGRADE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_UPGRADE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_SLOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_SLOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_WEIGHT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_WEIGHT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_EFFECTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_EFFECTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SKILL_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SKILL_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_WINNINGSTREAKAWARD(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_WINNINGSTREAKAWARD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PVP_POINT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PVP_POINT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PVP_POINT_CHARGE_COST(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PVP_POINT_CHARGE_COST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PH_REGAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PH_REGAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PRACTISE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PRACTISE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PRACTISEPVE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PRACTISEPVE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GAME_MODE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GAME_MODE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_COMBO_BONUS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_COMBO_BONUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TOUR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TOUR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TOUR_RESET_LIMIT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TOUR_RESET_LIMIT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TOUR_RESET_COST(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TOUR_RESET_COST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SHOOT_SOLUTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SHOOT_SOLUTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ANIMINFO(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ANIMINFO);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STOREGOODS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STOREGOODS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STOREREFRESHCONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STOREREFRESHCONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BUYGOLD(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BUYGOLD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BUYHP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BUYHP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_VIPPRIVILEGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_VIPPRIVILEGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_VIPPRIVILEGE_STATE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_VIPPRIVILEGE_STATE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_RECHARGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_RECHARGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_MAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_MAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_DAILY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_DAILY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_CONDITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_CONDITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_PRECONDITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_PRECONDITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_TASKSIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_TASKSIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TASK_LINK(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TASK_LINK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_DAY_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_DAY_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MONTH_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MONTH_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CAPTAIN_TRAINING(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CAPTAIN_TRAINING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CAPTAIN_TRAINING_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CAPTAIN_TRAINING_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TATTOO(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TATTOO);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_EQUIPMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_EQUIPMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GUIDE_MODULE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GUIDE_MODULE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_GUIDE_STEP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_GUIDE_STEP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PRACTICE_STEP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PRACTICE_STEP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FUNCTION_CONDITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FUNCTION_CONDITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BODY_INFO_LIST(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BODY_INFO_LIST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLE_SHAPE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLE_SHAPE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FASHION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FASHION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BONE_MAPPING(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BONE_MAPPING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FASHIONATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FASHIONATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FASHIONDATA(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FASHIONDATA);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BADGESLOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BADGESLOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STAR_ATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STAR_ATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALITY_ATTR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALITY_ATTR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_QUALITY_ATTR_COR(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_QUALITY_ATTR_COR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_FASHION_SHOP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_FASHION_SHOP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PUSH(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PUSH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SCENE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SCENE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_ACHIEVEMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_ACHIEVEMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_POSITION_ACTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_POSITION_ACTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ROLE_ACTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ROLE_ACTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STEAL_ACTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STEAL_ACTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_STEAL_STATE_RATIO(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_STEAL_STATE_RATIO);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_CURVE_RATE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_CURVE_RATE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_DUNK_RATE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_DUNK_RATE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_AI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_AI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_AI_NAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_AI_NAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_PRESENTHP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_PRESENTHP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ATTR_REDUCE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ATTR_REDUCE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_LOTTERY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_LOTTERY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_RANK(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_RANK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BULL_FIGHT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BULL_FIGHT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SHOOT_MATCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SHOOT_MATCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_BULLFIGHT_CONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_BULLFIGHT_CONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SHOOT_GAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SHOOT_GAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_SHOOT_GAME_CONSUME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_SHOOT_GAME_CONSUME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_HEDGING(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_HEDGING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ANNOUNCEMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ANNOUNCEMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_NEWCOMERSIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_NEWCOMERSIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_EXPECTED_SCORE_DIFF(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_EXPECTED_SCORE_DIFF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ATTR_ENHANCE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ATTR_ENHANCE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_POTENTIAL_EFFECT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_POTENTIAL_EFFECT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MAP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MAP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_ACTIVITY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_ACTIVITY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TRAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TRAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_TALENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_TALENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_LADDER_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_LADDER_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_LADDER_SEASON(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_LADDER_SEASON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_LADDER_REWARD(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_LADDER_REWARD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_SOUND(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_SOUND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_XML_MATCH_MSG(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_XML_MATCH_MSG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIR_EFFECT(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIR_EFFECT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MUS_BGMLOGIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.MUS_BGMLOGIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MUS_BGMHALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.MUS_BGMHALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MUS_BGMGAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.MUS_BGMGAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CONFIG_SWITCH_COLUMN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.CONFIG_SWITCH_COLUMN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CONFIG_SWITCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.CONFIG_SWITCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SCENE_STARTUP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SCENE_STARTUP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SCENE_HALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SCENE_HALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SCENE_MATCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SCENE_MATCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SCENE_GAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SCENE_GAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_DEVELOP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_DEVELOP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_STREAM_ASSETS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.STREAM_ASSETS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GAME_VERSION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GAME_VERSION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RES_VERSION(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.RES_VERSION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_GUIDE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_GUIDE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_DEBUG_START_GUIDE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_DEBUG_START_GUIDE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_ENABLE_TALKING_DATA(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_ENABLE_TALKING_DATA);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CHALLENGE_OPEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.CHALLENGE_OPEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CHALLENGE_CLOSE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.CHALLENGE_CLOSE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QUALIFYING_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.QUALIFYING_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GShOOTSEQUENCE(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GShOOTSEQUENCE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GPRACRICECD(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GPRACRICECD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MAX_QUALITY_NUM(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.MAX_QUALITY_NUM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SHOOTCARD_DIAMOND(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SHOOTCARD_DIAMOND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DIAMOND_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.DIAMOND_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GOLD_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.GOLD_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HONOR_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.HONOR_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HP_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.HP_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TEAM_EXP_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.TEAM_EXP_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ROLE_EXP_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.ROLE_EXP_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PRESTIGE_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.PRESTIGE_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HONOR2_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.HONOR2_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PRESTIGE2_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.PRESTIGE2_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_REPUTATION_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.REPUTATION_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IS_FASHION_OPEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.IS_FASHION_OPEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SWEEP_CARD_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.SWEEP_CARD_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QUALITY_COLOR(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, GlobalConst.QUALITY_COLOR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MATCH_TIP_COLOR_RED(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.MATCH_TIP_COLOR_RED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CONTROLLER_RAD(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.CONTROLLER_RAD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PT_2(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.PT_2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PT_3(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.PT_3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OPEN_SHOOT_CYCLE_RADIUS(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.OPEN_SHOOT_CYCLE_RADIUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OPEN_SHOOT_FAN_RADIUS(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.OPEN_SHOOT_FAN_RADIUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OPEN_SHOOT_FAN_ANGLE(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.OPEN_SHOOT_FAN_ANGLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PVP_VALID_LATENCY(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.PVP_VALID_LATENCY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ALL_HEDGING_ID(IntPtr L)
	{
		LuaScriptMgr.Push(L, GlobalConst.ALL_HEDGING_ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MATCHED_KEY_BLOCK_RATE_ADJUST(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.MATCHED_KEY_BLOCK_RATE_ADJUST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BACK_TO_BACK_FAN_ANGLE(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.BACK_TO_BACK_FAN_ANGLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BACK_TO_BACK_FAN_RADIUS(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.BACK_TO_BACK_FAN_RADIUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CHARACTER_SKIN_WIDTH(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, GlobalConst.CHARACTER_SKIN_WIDTH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IS_DEVELOP(IntPtr L)
	{
		GlobalConst.IS_DEVELOP = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_STREAM_ASSETS(IntPtr L)
	{
		GlobalConst.STREAM_ASSETS = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GAME_VERSION(IntPtr L)
	{
		GlobalConst.GAME_VERSION = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RES_VERSION(IntPtr L)
	{
		GlobalConst.RES_VERSION = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IS_GUIDE(IntPtr L)
	{
		GlobalConst.IS_GUIDE = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IS_DEBUG_START_GUIDE(IntPtr L)
	{
		GlobalConst.IS_DEBUG_START_GUIDE = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IS_ENABLE_TALKING_DATA(IntPtr L)
	{
		GlobalConst.IS_ENABLE_TALKING_DATA = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CHALLENGE_OPEN(IntPtr L)
	{
		GlobalConst.CHALLENGE_OPEN = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CHALLENGE_CLOSE(IntPtr L)
	{
		GlobalConst.CHALLENGE_CLOSE = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QUALIFYING_TIMES(IntPtr L)
	{
		GlobalConst.QUALIFYING_TIMES = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GShOOTSEQUENCE(IntPtr L)
	{
		GlobalConst.GShOOTSEQUENCE = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GPRACRICECD(IntPtr L)
	{
		GlobalConst.GPRACRICECD = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MAX_QUALITY_NUM(IntPtr L)
	{
		GlobalConst.MAX_QUALITY_NUM = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SHOOTCARD_DIAMOND(IntPtr L)
	{
		GlobalConst.SHOOTCARD_DIAMOND = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DIAMOND_ID(IntPtr L)
	{
		GlobalConst.DIAMOND_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GOLD_ID(IntPtr L)
	{
		GlobalConst.GOLD_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HONOR_ID(IntPtr L)
	{
		GlobalConst.HONOR_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HP_ID(IntPtr L)
	{
		GlobalConst.HP_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TEAM_EXP_ID(IntPtr L)
	{
		GlobalConst.TEAM_EXP_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ROLE_EXP_ID(IntPtr L)
	{
		GlobalConst.ROLE_EXP_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PRESTIGE_ID(IntPtr L)
	{
		GlobalConst.PRESTIGE_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HONOR2_ID(IntPtr L)
	{
		GlobalConst.HONOR2_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PRESTIGE2_ID(IntPtr L)
	{
		GlobalConst.PRESTIGE2_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_REPUTATION_ID(IntPtr L)
	{
		GlobalConst.REPUTATION_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IS_FASHION_OPEN(IntPtr L)
	{
		GlobalConst.IS_FASHION_OPEN = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SWEEP_CARD_ID(IntPtr L)
	{
		GlobalConst.SWEEP_CARD_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QUALITY_COLOR(IntPtr L)
	{
		GlobalConst.QUALITY_COLOR = LuaScriptMgr.GetArrayObject<Color>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MATCH_TIP_COLOR_RED(IntPtr L)
	{
		GlobalConst.MATCH_TIP_COLOR_RED = (Color32)LuaScriptMgr.GetNetObject(L, 3, typeof(Color32));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CONTROLLER_RAD(IntPtr L)
	{
		GlobalConst.CONTROLLER_RAD = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PT_2(IntPtr L)
	{
		GlobalConst.PT_2 = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PT_3(IntPtr L)
	{
		GlobalConst.PT_3 = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OPEN_SHOOT_CYCLE_RADIUS(IntPtr L)
	{
		GlobalConst.OPEN_SHOOT_CYCLE_RADIUS = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OPEN_SHOOT_FAN_RADIUS(IntPtr L)
	{
		GlobalConst.OPEN_SHOOT_FAN_RADIUS = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OPEN_SHOOT_FAN_ANGLE(IntPtr L)
	{
		GlobalConst.OPEN_SHOOT_FAN_ANGLE = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PVP_VALID_LATENCY(IntPtr L)
	{
		GlobalConst.PVP_VALID_LATENCY = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ALL_HEDGING_ID(IntPtr L)
	{
		GlobalConst.ALL_HEDGING_ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MATCHED_KEY_BLOCK_RATE_ADJUST(IntPtr L)
	{
		GlobalConst.MATCHED_KEY_BLOCK_RATE_ADJUST = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BACK_TO_BACK_FAN_ANGLE(IntPtr L)
	{
		GlobalConst.BACK_TO_BACK_FAN_ANGLE = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BACK_TO_BACK_FAN_RADIUS(IntPtr L)
	{
		GlobalConst.BACK_TO_BACK_FAN_RADIUS = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CHARACTER_SKIN_WIDTH(IntPtr L)
	{
		GlobalConst.CHARACTER_SKIN_WIDTH = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}
}

