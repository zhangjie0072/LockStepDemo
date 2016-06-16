using System;
using LuaInterface;

public class fogs_proto_msg_ErrorIDWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("SUCCESS", GetSUCCESS),
		new LuaMethod("FAILED", GetFAILED),
		new LuaMethod("DB_UNCONNECTION", GetDB_UNCONNECTION),
		new LuaMethod("DB_QUERY_FAILED", GetDB_QUERY_FAILED),
		new LuaMethod("DB_QUERY_EMPTY", GetDB_QUERY_EMPTY),
		new LuaMethod("DB_UNKONOW_OPERATION_TYPE", GetDB_UNKONOW_OPERATION_TYPE),
		new LuaMethod("LOGIN_GAME_VERSION_ERROR", GetLOGIN_GAME_VERSION_ERROR),
		new LuaMethod("LOGIN_RESOURCE_VERSION_ERROR", GetLOGIN_RESOURCE_VERSION_ERROR),
		new LuaMethod("LOGIN_VERIFY_FAILED", GetLOGIN_VERIFY_FAILED),
		new LuaMethod("LOGIN_SERVER_CLOSED", GetLOGIN_SERVER_CLOSED),
		new LuaMethod("LOGIN_SERVER_BUSY", GetLOGIN_SERVER_BUSY),
		new LuaMethod("LOGIN_SERVER_ERROR", GetLOGIN_SERVER_ERROR),
		new LuaMethod("INVALID_SESSION", GetINVALID_SESSION),
		new LuaMethod("LOGIN_ACCOUNT_FAILED", GetLOGIN_ACCOUNT_FAILED),
		new LuaMethod("LOGIN_SERVER_REPEAT", GetLOGIN_SERVER_REPEAT),
		new LuaMethod("LOGIN_EMPTY_CDKEY", GetLOGIN_EMPTY_CDKEY),
		new LuaMethod("LOGIN_WRONG_PASSWORD", GetLOGIN_WRONG_PASSWORD),
		new LuaMethod("LOGIN_ALREADY_ACCOUNT", GetLOGIN_ALREADY_ACCOUNT),
		new LuaMethod("LOGIN_ANOTHER_PLAYER", GetLOGIN_ANOTHER_PLAYER),
		new LuaMethod("LOGIN_LOADING", GetLOGIN_LOADING),
		new LuaMethod("SDK_VERIFY_FAILED", GetSDK_VERIFY_FAILED),
		new LuaMethod("INVAILD_CDKEY", GetINVAILD_CDKEY),
		new LuaMethod("ACCOUNT_ALREADY_LOGIN", GetACCOUNT_ALREADY_LOGIN),
		new LuaMethod("ACCOUNT_FROZEN", GetACCOUNT_FROZEN),
		new LuaMethod("ACCOUNT_MANAGED", GetACCOUNT_MANAGED),
		new LuaMethod("ACCOUNT_SILENT", GetACCOUNT_SILENT),
		new LuaMethod("FRESHMAN", GetFRESHMAN),
		new LuaMethod("KICK_OFFLINE", GetKICK_OFFLINE),
		new LuaMethod("NOT_OWNED_CAPTAIN", GetNOT_OWNED_CAPTAIN),
		new LuaMethod("ALREADY_OWNED_CAPTAIN", GetALREADY_OWNED_CAPTAIN),
		new LuaMethod("ALREADY_IN_PLAY", GetALREADY_IN_PLAY),
		new LuaMethod("CREATE_SESSION_FAILED", GetCREATE_SESSION_FAILED),
		new LuaMethod("NOT_FIND_PLAYER", GetNOT_FIND_PLAYER),
		new LuaMethod("INVALID_NAME", GetINVALID_NAME),
		new LuaMethod("REPEATED_NAME", GetREPEATED_NAME),
		new LuaMethod("INVALID_NUM", GetINVALID_NUM),
		new LuaMethod("REACH_MAX", GetREACH_MAX),
		new LuaMethod("NOT_ENOUGH_MONEY", GetNOT_ENOUGH_MONEY),
		new LuaMethod("NOT_ENOUGH_DIAMOND", GetNOT_ENOUGH_DIAMOND),
		new LuaMethod("NOT_ENOUGH_STUFF", GetNOT_ENOUGH_STUFF),
		new LuaMethod("NOT_ENOUGH_LEVEL", GetNOT_ENOUGH_LEVEL),
		new LuaMethod("NOT_ENOUGH_HP", GetNOT_ENOUGH_HP),
		new LuaMethod("NOT_ENOUGH_CHALLENGE_TIMES", GetNOT_ENOUGH_CHALLENGE_TIMES),
		new LuaMethod("MATCH_LOSE", GetMATCH_LOSE),
		new LuaMethod("NOT_ENOUGH_HONOR", GetNOT_ENOUGH_HONOR),
		new LuaMethod("NOT_ENOUGH_SWEEP_CARD", GetNOT_ENOUGH_SWEEP_CARD),
		new LuaMethod("NOT_ENOUGH_PRESTIGE", GetNOT_ENOUGH_PRESTIGE),
		new LuaMethod("NOT_ENOUGH_PRESTIGE2", GetNOT_ENOUGH_PRESTIGE2),
		new LuaMethod("NOT_ENOUGH_HONOR2", GetNOT_ENOUGH_HONOR2),
		new LuaMethod("NOT_ENOUGH_REPUTATION", GetNOT_ENOUGH_REPUTATION),
		new LuaMethod("NOT_ENOUGH_HP2", GetNOT_ENOUGH_HP2),
		new LuaMethod("NOT_ENOUGH_CHALLENGE_TIMES2", GetNOT_ENOUGH_CHALLENGE_TIMES2),
		new LuaMethod("NOT_ENOUGH_VIP_LEVEL", GetNOT_ENOUGH_VIP_LEVEL),
		new LuaMethod("REPEAT_GET_AWARD", GetREPEAT_GET_AWARD),
		new LuaMethod("VIP_AWARD_ALREADY_GET", GetVIP_AWARD_ALREADY_GET),
		new LuaMethod("NOT_ENOUGH_SIGN", GetNOT_ENOUGH_SIGN),
		new LuaMethod("REPEATED_SIGN", GetREPEATED_SIGN),
		new LuaMethod("NOT_MISS_SIGN", GetNOT_MISS_SIGN),
		new LuaMethod("SIGN_BEFORE_APPEND_SIGN", GetSIGN_BEFORE_APPEND_SIGN),
		new LuaMethod("CAREER_SECTION_LOCK", GetCAREER_SECTION_LOCK),
		new LuaMethod("CAREER_CHAPTER_LOCK", GetCAREER_CHAPTER_LOCK),
		new LuaMethod("CAREER_SECTION_FINISHED", GetCAREER_SECTION_FINISHED),
		new LuaMethod("CAREER_CHAPTER_FINISHED", GetCAREER_CHAPTER_FINISHED),
		new LuaMethod("CAREER_NOT_ALL_SECTION_FINISHED", GetCAREER_NOT_ALL_SECTION_FINISHED),
		new LuaMethod("ADD_SECTION_AWARD_FAILED", GetADD_SECTION_AWARD_FAILED),
		new LuaMethod("ADD_CHAPTER_AWARD_FAILED", GetADD_CHAPTER_AWARD_FAILED),
		new LuaMethod("CAREER_SECTION_NOT_COMPLETE", GetCAREER_SECTION_NOT_COMPLETE),
		new LuaMethod("CAREER_STAR_NUM_LACK", GetCAREER_STAR_NUM_LACK),
		new LuaMethod("CAREER_STAR_AWARD_ALREADY_GET", GetCAREER_STAR_AWARD_ALREADY_GET),
		new LuaMethod("ROLE_ALREADY_GET", GetROLE_ALREADY_GET),
		new LuaMethod("SWEEP_NEED_FULL_STAR", GetSWEEP_NEED_FULL_STAR),
		new LuaMethod("NOT_OWNED_ROLE", GetNOT_OWNED_ROLE),
		new LuaMethod("ALREADY_OWNED_ROLE", GetALREADY_OWNED_ROLE),
		new LuaMethod("NOT_ENOUGH_PIECES", GetNOT_ENOUGH_PIECES),
		new LuaMethod("LEVEL_LIMIT_QUALITY", GetLEVEL_LIMIT_QUALITY),
		new LuaMethod("NOT_OWNED_EXERCISE", GetNOT_OWNED_EXERCISE),
		new LuaMethod("EXERCISE_REACH_MAX", GetEXERCISE_REACH_MAX),
		new LuaMethod("EXERCISE_QUALITY_BIG_THAN_ROLE_QUALITY", GetEXERCISE_QUALITY_BIG_THAN_ROLE_QUALITY),
		new LuaMethod("TALENT_LIMIT_STAR", GetTALENT_LIMIT_STAR),
		new LuaMethod("NOT_ENOUGH_EXERCISE_QUALITY", GetNOT_ENOUGH_EXERCISE_QUALITY),
		new LuaMethod("LEVEL_LIMIT_ROLE_LEVEL", GetLEVEL_LIMIT_ROLE_LEVEL),
		new LuaMethod("CANNOT_COMPOSITE_GOODS", GetCANNOT_COMPOSITE_GOODS),
		new LuaMethod("NOT_ENOUGH_COMPOSITE_STUFF", GetNOT_ENOUGH_COMPOSITE_STUFF),
		new LuaMethod("CANNOT_SELL_GOODS", GetCANNOT_SELL_GOODS),
		new LuaMethod("CANNOT_USE_GOODS", GetCANNOT_USE_GOODS),
		new LuaMethod("CANNOT_DECOMPOSE_GOODS", GetCANNOT_DECOMPOSE_GOODS),
		new LuaMethod("REACHED_MAX_STACK_NUM", GetREACHED_MAX_STACK_NUM),
		new LuaMethod("STR_ENHANCE_LESS_GOLD", GetSTR_ENHANCE_LESS_GOLD),
		new LuaMethod("SKILL_SLOT_LOCK", GetSKILL_SLOT_LOCK),
		new LuaMethod("SKILL_SLOT_UNLOCK", GetSKILL_SLOT_UNLOCK),
		new LuaMethod("SKILL_SLOT_FILLED", GetSKILL_SLOT_FILLED),
		new LuaMethod("SKILL_SLOT_UNFILLED", GetSKILL_SLOT_UNFILLED),
		new LuaMethod("SKILL_EQUIPED", GetSKILL_EQUIPED),
		new LuaMethod("SKILL_UNEQUIPED", GetSKILL_UNEQUIPED),
		new LuaMethod("NOT_ENOUGH_SKILL_UP_STUFF", GetNOT_ENOUGH_SKILL_UP_STUFF),
		new LuaMethod("NOT_ENOUGH_ATTR_VALUE", GetNOT_ENOUGH_ATTR_VALUE),
		new LuaMethod("GOODS_EQUIPED", GetGOODS_EQUIPED),
		new LuaMethod("GOODS_UNEQUIPED", GetGOODS_UNEQUIPED),
		new LuaMethod("GOODS_SLOT_UNFILLED", GetGOODS_SLOT_UNFILLED),
		new LuaMethod("LEVEL_LIMIT_GOODS_LEVEL", GetLEVEL_LIMIT_GOODS_LEVEL),
		new LuaMethod("EQUIPMENT_POSITION_NOT_MATCH", GetEQUIPMENT_POSITION_NOT_MATCH),
		new LuaMethod("NOT_HAVE_EQUIPMENT", GetNOT_HAVE_EQUIPMENT),
		new LuaMethod("EQUIPMENT_NOT_ENOUGH_MONTY", GetEQUIPMENT_NOT_ENOUGH_MONTY),
		new LuaMethod("EQUIPMENT_NOT_EXIST", GetEQUIPMENT_NOT_EXIST),
		new LuaMethod("GOODS_REACH_MAX", GetGOODS_REACH_MAX),
		new LuaMethod("STORE_GOODS_SELL_OUT", GetSTORE_GOODS_SELL_OUT),
		new LuaMethod("BUY_TIME_USE_UP", GetBUY_TIME_USE_UP),
		new LuaMethod("REMAIN_CHALLENGE_TIMES", GetREMAIN_CHALLENGE_TIMES),
		new LuaMethod("ALREADY_MATCHED", GetALREADY_MATCHED),
		new LuaMethod("NOT_ENOUGH_ROLES", GetNOT_ENOUGH_ROLES),
		new LuaMethod("ALREADY_PVP", GetALREADY_PVP),
		new LuaMethod("WAIT_ALL_PLAYER_READY", GetWAIT_ALL_PLAYER_READY),
		new LuaMethod("REACH_FRIENDS_MAX", GetREACH_FRIENDS_MAX),
		new LuaMethod("REACH_BLACK_LIST_MAX", GetREACH_BLACK_LIST_MAX),
		new LuaMethod("REACH_FRIEND_PRESENT_MAX", GetREACH_FRIEND_PRESENT_MAX),
		new LuaMethod("REACH_FRIEND_GET_GIFT_MAX", GetREACH_FRIEND_GET_GIFT_MAX),
		new LuaMethod("ALREADY_PRESEND_FRIEND", GetALREADY_PRESEND_FRIEND),
		new LuaMethod("ALREADY_GET_GIFT_FRIEND", GetALREADY_GET_GIFT_FRIEND),
		new LuaMethod("TARGET_NOT_FRIEND", GetTARGET_NOT_FRIEND),
		new LuaMethod("TARGET_NOT_BLACKLIST", GetTARGET_NOT_BLACKLIST),
		new LuaMethod("TARGET_IN_BLACKLIST", GetTARGET_IN_BLACKLIST),
		new LuaMethod("TARGET_IN_FRIENDS", GetTARGET_IN_FRIENDS),
		new LuaMethod("TARGET_IS_NOT_EXIST", GetTARGET_IS_NOT_EXIST),
		new LuaMethod("NOT_FRIEND_CAN_PRESENT", GetNOT_FRIEND_CAN_PRESENT),
		new LuaMethod("FRIEND_OFFLINE", GetFRIEND_OFFLINE),
		new LuaMethod("FRIEND_IN_MATCH", GetFRIEND_IN_MATCH),
		new LuaMethod("INVITE_OUT_DUE", GetINVITE_OUT_DUE),
		new LuaMethod("MASTER_EXIT", GetMASTER_EXIT),
		new LuaMethod("PARTTEN_OFFLINE", GetPARTTEN_OFFLINE),
		new LuaMethod("FRIEND_IN_ROOM", GetFRIEND_IN_ROOM),
		new LuaMethod("ROOM_FULL", GetROOM_FULL),
		new LuaMethod("ALREADY_START_MATCHING", GetALREADY_START_MATCHING),
		new LuaMethod("ALREADY_INVITE_FRIEND", GetALREADY_INVITE_FRIEND),
		new LuaMethod("INVITE_REACH_MAX", GetINVITE_REACH_MAX),
		new LuaMethod("IN_COOLING", GetIN_COOLING),
		new LuaMethod("LEVEL_LIMIT_TRAINING", GetLEVEL_LIMIT_TRAINING),
		new LuaMethod("REACH_MAX_COUNT_TRAINING", GetREACH_MAX_COUNT_TRAINING),
		new LuaMethod("STR_EXERCIE_LESS_GOLD", GetSTR_EXERCIE_LESS_GOLD),
		new LuaMethod("GET_MAIL_ATTACHMENT_FAILED", GetGET_MAIL_ATTACHMENT_FAILED),
		new LuaMethod("GET_HP_ERROR_TIME", GetGET_HP_ERROR_TIME),
		new LuaMethod("REPEATED_GET_HP", GetREPEATED_GET_HP),
		new LuaMethod("REACH_MAX_RESET_TIMES", GetREACH_MAX_RESET_TIMES),
		new LuaMethod("NEED_GET_THROUGH_FIRST", GetNEED_GET_THROUGH_FIRST),
		new LuaMethod("OTHER_AWARDS_CAN_GET", GetOTHER_AWARDS_CAN_GET),
		new LuaMethod("FUNCTION_CLOSED", GetFUNCTION_CLOSED),
		new LuaMethod("AWARDS_GETED", GetAWARDS_GETED),
		new LuaMethod("DO_NOT_GET_AWARDS", GetDO_NOT_GET_AWARDS),
		new LuaMethod("TASK_CONDITION_UNFINISHED", GetTASK_CONDITION_UNFINISHED),
		new LuaMethod("GUIDE_CONDITION_UNFINISHED", GetGUIDE_CONDITION_UNFINISHED),
		new LuaMethod("GOODS_OUT_OF_DATE", GetGOODS_OUT_OF_DATE),
		new LuaMethod("FASHION_NOT_MATCH_SHAPE", GetFASHION_NOT_MATCH_SHAPE),
		new LuaMethod("GENDER_NOT_MATCH", GetGENDER_NOT_MATCH),
		new LuaMethod("NOT_OWN_THIS_FASHION", GetNOT_OWN_THIS_FASHION),
		new LuaMethod("NOT_REACH_CONDITION", GetNOT_REACH_CONDITION),
		new LuaMethod("NOT_IN_MATCH_QUEUE", GetNOT_IN_MATCH_QUEUE),
		new LuaMethod("GAME_TIME_OUT", GetGAME_TIME_OUT),
		new LuaMethod("QUALIFYING_COOL", GetQUALIFYING_COOL),
		new LuaMethod("RIVAL_BATTLEING", GetRIVAL_BATTLEING),
		new LuaMethod("RANKING_CHANGE", GetRANKING_CHANGE),
		new LuaMethod("BE_CHALLENGEED_IN", GetBE_CHALLENGEED_IN),
		new LuaMethod("GAME_NOT_OPEN", GetGAME_NOT_OPEN),
		new LuaMethod("ACTIVITY_CLOSE", GetACTIVITY_CLOSE),
		new LuaMethod("NOT_ENOUGH_OPEN_CARD_TIMES", GetNOT_ENOUGH_OPEN_CARD_TIMES),
		new LuaMethod("PRACTICE_COOL", GetPRACTICE_COOL),
		new LuaMethod("WORLD_CHANNEL_COOL", GetWORLD_CHANNEL_COOL),
		new LuaMethod("NOT_ENOUGH_LOTTERY_TIMES", GetNOT_ENOUGH_LOTTERY_TIMES),
		new LuaMethod("ADD_LOTTERY_AWARD_FAILED", GetADD_LOTTERY_AWARD_FAILED),
		new LuaMethod("BADGE_BOOK_NUM_REACH_MAX", GetBADGE_BOOK_NUM_REACH_MAX),
		new LuaMethod("UNLOCK_BADGE_SLOT_IN_ORDER", GetUNLOCK_BADGE_SLOT_IN_ORDER),
		new LuaMethod("NO_UNLOCK_BADGE_SLOT", GetNO_UNLOCK_BADGE_SLOT),
		new LuaMethod("NO_ENOUGH_BADGE", GetNO_ENOUGH_BADGE),
		new LuaMethod("CAN_NOT_UNLOCK_IN_ADVANCE", GetCAN_NOT_UNLOCK_IN_ADVANCE),
		new LuaMethod("NO_CAN_DECOMPOSE_BADGE", GetNO_CAN_DECOMPOSE_BADGE),
		new LuaMethod("INVALID_OPERATION", GetINVALID_OPERATION),
		new LuaMethod("ERROR_CONFIGURATION", GetERROR_CONFIGURATION),
		new LuaMethod("ERROR_CHEAT", GetERROR_CHEAT),
		new LuaMethod("NO_IN_CHAT_ROOM", GetNO_IN_CHAT_ROOM),
		new LuaMethod("UNKNOW_ERROR", GetUNKNOW_ERROR),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.ErrorID", typeof(fogs.proto.msg.ErrorID), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSUCCESS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SUCCESS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDB_UNCONNECTION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.DB_UNCONNECTION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDB_QUERY_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.DB_QUERY_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDB_QUERY_EMPTY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.DB_QUERY_EMPTY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDB_UNKONOW_OPERATION_TYPE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.DB_UNKONOW_OPERATION_TYPE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_GAME_VERSION_ERROR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_GAME_VERSION_ERROR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_RESOURCE_VERSION_ERROR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_RESOURCE_VERSION_ERROR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_VERIFY_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_VERIFY_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_SERVER_CLOSED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_SERVER_CLOSED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_SERVER_BUSY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_SERVER_BUSY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_SERVER_ERROR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_SERVER_ERROR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVALID_SESSION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVALID_SESSION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_ACCOUNT_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_ACCOUNT_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_SERVER_REPEAT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_SERVER_REPEAT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_EMPTY_CDKEY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_EMPTY_CDKEY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_WRONG_PASSWORD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_WRONG_PASSWORD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_ALREADY_ACCOUNT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_ALREADY_ACCOUNT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_ANOTHER_PLAYER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_ANOTHER_PLAYER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOGIN_LOADING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LOGIN_LOADING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSDK_VERIFY_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SDK_VERIFY_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVAILD_CDKEY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVAILD_CDKEY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACCOUNT_ALREADY_LOGIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ACCOUNT_ALREADY_LOGIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACCOUNT_FROZEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ACCOUNT_FROZEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACCOUNT_MANAGED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ACCOUNT_MANAGED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACCOUNT_SILENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ACCOUNT_SILENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFRESHMAN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FRESHMAN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetKICK_OFFLINE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.KICK_OFFLINE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_OWNED_CAPTAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_OWNED_CAPTAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_OWNED_CAPTAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_OWNED_CAPTAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_IN_PLAY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_IN_PLAY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCREATE_SESSION_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CREATE_SESSION_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_FIND_PLAYER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_FIND_PLAYER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVALID_NAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVALID_NAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREPEATED_NAME(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REPEATED_NAME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVALID_NUM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVALID_NUM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_MONEY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_MONEY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_DIAMOND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_DIAMOND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_STUFF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_STUFF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_HP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_HP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_CHALLENGE_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_CHALLENGE_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMATCH_LOSE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.MATCH_LOSE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_HONOR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_HONOR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_SWEEP_CARD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_SWEEP_CARD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_PRESTIGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_PRESTIGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_PRESTIGE2(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_PRESTIGE2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_HONOR2(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_HONOR2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_REPUTATION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_REPUTATION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_HP2(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_HP2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_CHALLENGE_TIMES2(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_CHALLENGE_TIMES2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_VIP_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_VIP_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREPEAT_GET_AWARD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REPEAT_GET_AWARD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVIP_AWARD_ALREADY_GET(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.VIP_AWARD_ALREADY_GET);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREPEATED_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REPEATED_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_MISS_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_MISS_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSIGN_BEFORE_APPEND_SIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SIGN_BEFORE_APPEND_SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_SECTION_LOCK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_SECTION_LOCK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_CHAPTER_LOCK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_CHAPTER_LOCK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_SECTION_FINISHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_SECTION_FINISHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_CHAPTER_FINISHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_CHAPTER_FINISHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_NOT_ALL_SECTION_FINISHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_NOT_ALL_SECTION_FINISHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetADD_SECTION_AWARD_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ADD_SECTION_AWARD_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetADD_CHAPTER_AWARD_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ADD_CHAPTER_AWARD_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_SECTION_NOT_COMPLETE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_SECTION_NOT_COMPLETE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_STAR_NUM_LACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_STAR_NUM_LACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAREER_STAR_AWARD_ALREADY_GET(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAREER_STAR_AWARD_ALREADY_GET);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetROLE_ALREADY_GET(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ROLE_ALREADY_GET);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSWEEP_NEED_FULL_STAR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SWEEP_NEED_FULL_STAR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_OWNED_ROLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_OWNED_ROLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_OWNED_ROLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_OWNED_ROLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_PIECES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_PIECES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLEVEL_LIMIT_QUALITY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LEVEL_LIMIT_QUALITY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_OWNED_EXERCISE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_OWNED_EXERCISE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEXERCISE_REACH_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.EXERCISE_REACH_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEXERCISE_QUALITY_BIG_THAN_ROLE_QUALITY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.EXERCISE_QUALITY_BIG_THAN_ROLE_QUALITY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTALENT_LIMIT_STAR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TALENT_LIMIT_STAR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_EXERCISE_QUALITY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_EXERCISE_QUALITY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLEVEL_LIMIT_ROLE_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LEVEL_LIMIT_ROLE_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCANNOT_COMPOSITE_GOODS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CANNOT_COMPOSITE_GOODS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_COMPOSITE_STUFF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_COMPOSITE_STUFF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCANNOT_SELL_GOODS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CANNOT_SELL_GOODS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCANNOT_USE_GOODS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CANNOT_USE_GOODS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCANNOT_DECOMPOSE_GOODS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CANNOT_DECOMPOSE_GOODS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACHED_MAX_STACK_NUM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACHED_MAX_STACK_NUM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSTR_ENHANCE_LESS_GOLD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.STR_ENHANCE_LESS_GOLD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_SLOT_LOCK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_SLOT_LOCK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_SLOT_UNLOCK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_SLOT_UNLOCK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_SLOT_FILLED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_SLOT_FILLED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_SLOT_UNFILLED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_SLOT_UNFILLED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_EQUIPED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_EQUIPED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSKILL_UNEQUIPED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.SKILL_UNEQUIPED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_SKILL_UP_STUFF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_SKILL_UP_STUFF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_ATTR_VALUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_ATTR_VALUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGOODS_EQUIPED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GOODS_EQUIPED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGOODS_UNEQUIPED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GOODS_UNEQUIPED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGOODS_SLOT_UNFILLED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GOODS_SLOT_UNFILLED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLEVEL_LIMIT_GOODS_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LEVEL_LIMIT_GOODS_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEQUIPMENT_POSITION_NOT_MATCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.EQUIPMENT_POSITION_NOT_MATCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_HAVE_EQUIPMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_HAVE_EQUIPMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEQUIPMENT_NOT_ENOUGH_MONTY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.EQUIPMENT_NOT_ENOUGH_MONTY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEQUIPMENT_NOT_EXIST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.EQUIPMENT_NOT_EXIST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGOODS_REACH_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GOODS_REACH_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSTORE_GOODS_SELL_OUT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.STORE_GOODS_SELL_OUT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBUY_TIME_USE_UP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.BUY_TIME_USE_UP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREMAIN_CHALLENGE_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REMAIN_CHALLENGE_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_MATCHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_MATCHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_ROLES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_ROLES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_PVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_PVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWAIT_ALL_PLAYER_READY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.WAIT_ALL_PLAYER_READY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_FRIENDS_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_FRIENDS_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_BLACK_LIST_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_BLACK_LIST_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_FRIEND_PRESENT_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_FRIEND_PRESENT_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_FRIEND_GET_GIFT_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_FRIEND_GET_GIFT_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_PRESEND_FRIEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_PRESEND_FRIEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_GET_GIFT_FRIEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_GET_GIFT_FRIEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTARGET_NOT_FRIEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TARGET_NOT_FRIEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTARGET_NOT_BLACKLIST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TARGET_NOT_BLACKLIST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTARGET_IN_BLACKLIST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TARGET_IN_BLACKLIST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTARGET_IN_FRIENDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TARGET_IN_FRIENDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTARGET_IS_NOT_EXIST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TARGET_IS_NOT_EXIST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_FRIEND_CAN_PRESENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_FRIEND_CAN_PRESENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFRIEND_OFFLINE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FRIEND_OFFLINE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFRIEND_IN_MATCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FRIEND_IN_MATCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVITE_OUT_DUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVITE_OUT_DUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMASTER_EXIT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.MASTER_EXIT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPARTTEN_OFFLINE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.PARTTEN_OFFLINE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFRIEND_IN_ROOM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FRIEND_IN_ROOM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetROOM_FULL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ROOM_FULL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_START_MATCHING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_START_MATCHING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetALREADY_INVITE_FRIEND(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ALREADY_INVITE_FRIEND);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVITE_REACH_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVITE_REACH_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIN_COOLING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.IN_COOLING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLEVEL_LIMIT_TRAINING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.LEVEL_LIMIT_TRAINING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_MAX_COUNT_TRAINING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_MAX_COUNT_TRAINING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSTR_EXERCIE_LESS_GOLD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.STR_EXERCIE_LESS_GOLD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGET_MAIL_ATTACHMENT_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GET_MAIL_ATTACHMENT_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGET_HP_ERROR_TIME(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GET_HP_ERROR_TIME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREPEATED_GET_HP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REPEATED_GET_HP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetREACH_MAX_RESET_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.REACH_MAX_RESET_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNEED_GET_THROUGH_FIRST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NEED_GET_THROUGH_FIRST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOTHER_AWARDS_CAN_GET(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.OTHER_AWARDS_CAN_GET);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFUNCTION_CLOSED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FUNCTION_CLOSED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAWARDS_GETED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.AWARDS_GETED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDO_NOT_GET_AWARDS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.DO_NOT_GET_AWARDS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTASK_CONDITION_UNFINISHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.TASK_CONDITION_UNFINISHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGUIDE_CONDITION_UNFINISHED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GUIDE_CONDITION_UNFINISHED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGOODS_OUT_OF_DATE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GOODS_OUT_OF_DATE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFASHION_NOT_MATCH_SHAPE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.FASHION_NOT_MATCH_SHAPE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGENDER_NOT_MATCH(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GENDER_NOT_MATCH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_OWN_THIS_FASHION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_OWN_THIS_FASHION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_REACH_CONDITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_REACH_CONDITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_IN_MATCH_QUEUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_IN_MATCH_QUEUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGAME_TIME_OUT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GAME_TIME_OUT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQUALIFYING_COOL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.QUALIFYING_COOL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRIVAL_BATTLEING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.RIVAL_BATTLEING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRANKING_CHANGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.RANKING_CHANGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBE_CHALLENGEED_IN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.BE_CHALLENGEED_IN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGAME_NOT_OPEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.GAME_NOT_OPEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACTIVITY_CLOSE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ACTIVITY_CLOSE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_OPEN_CARD_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_OPEN_CARD_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPRACTICE_COOL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.PRACTICE_COOL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWORLD_CHANNEL_COOL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.WORLD_CHANNEL_COOL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNOT_ENOUGH_LOTTERY_TIMES(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NOT_ENOUGH_LOTTERY_TIMES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetADD_LOTTERY_AWARD_FAILED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ADD_LOTTERY_AWARD_FAILED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBADGE_BOOK_NUM_REACH_MAX(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.BADGE_BOOK_NUM_REACH_MAX);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUNLOCK_BADGE_SLOT_IN_ORDER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.UNLOCK_BADGE_SLOT_IN_ORDER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNO_UNLOCK_BADGE_SLOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NO_UNLOCK_BADGE_SLOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNO_ENOUGH_BADGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NO_ENOUGH_BADGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCAN_NOT_UNLOCK_IN_ADVANCE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.CAN_NOT_UNLOCK_IN_ADVANCE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNO_CAN_DECOMPOSE_BADGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NO_CAN_DECOMPOSE_BADGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetINVALID_OPERATION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.INVALID_OPERATION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetERROR_CONFIGURATION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ERROR_CONFIGURATION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetERROR_CHEAT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.ERROR_CHEAT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNO_IN_CHAT_ROOM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.NO_IN_CHAT_ROOM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUNKNOW_ERROR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ErrorID.UNKNOW_ERROR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.ErrorID o = (fogs.proto.msg.ErrorID)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

