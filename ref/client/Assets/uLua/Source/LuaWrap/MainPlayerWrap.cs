using System;
using System.Collections.Generic;
using LuaInterface;

public class MainPlayerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("Uninitialize", Uninitialize),
			new LuaMethod("AddDataChangedDelegate", AddDataChangedDelegate),
			new LuaMethod("RemoveDataChangedDelegate", RemoveDataChangedDelegate),
			new LuaMethod("SetBaseInfo", SetBaseInfo),
			new LuaMethod("UpdateEverySecond", UpdateEverySecond),
			new LuaMethod("OnNewDayCome", OnNewDayCome),
			new LuaMethod("CreateCaptainObj", CreateCaptainObj),
			new LuaMethod("HasRole", HasRole),
			new LuaMethod("GetRole", GetRole),
			new LuaMethod("GetRole2", GetRole2),
			new LuaMethod("GetRoleIDList", GetRoleIDList),
			new LuaMethod("SetRoleInfo", SetRoleInfo),
			new LuaMethod("GetFightRoleList", GetFightRoleList),
			new LuaMethod("SetRoleQuality", SetRoleQuality),
			new LuaMethod("SetRoleLvAndExp", SetRoleLvAndExp),
			new LuaMethod("SwitchCaptain", SwitchCaptain),
			new LuaMethod("GetCaptainInfo", GetCaptainInfo),
			new LuaMethod("AddInviteRoleInList", AddInviteRoleInList),
			new LuaMethod("SortGoodsDesc", SortGoodsDesc),
			new LuaMethod("SortGoodsDescExpPriority", SortGoodsDescExpPriority),
			new LuaMethod("SortGoodsAsc", SortGoodsAsc),
			new LuaMethod("GetGoods", GetGoods),
			new LuaMethod("GetGoodsList", GetGoodsList),
			new LuaMethod("GetGoodsCount", GetGoodsCount),
			new LuaMethod("AddGoods", AddGoods),
			new LuaMethod("DelGoods", DelGoods),
			new LuaMethod("GetFashionByID", GetFashionByID),
			new LuaMethod("GetTrainingByID", GetTrainingByID),
			new LuaMethod("GetMaterialByID", GetMaterialByID),
			new LuaMethod("CheckSkillInRole", CheckSkillInRole),
			new LuaMethod("GetBadgesGoodByID", GetBadgesGoodByID),
			new LuaMethod("CheckChapter", CheckChapter),
			new LuaMethod("CheckChapterComplete", CheckChapterComplete),
			new LuaMethod("GetChapter", GetChapter),
			new LuaMethod("ChangeChapterData", ChangeChapterData),
			new LuaMethod("ChangeChaptersData", ChangeChaptersData),
			new LuaMethod("AddChaptersData", AddChaptersData),
			new LuaMethod("CheckSection", CheckSection),
			new LuaMethod("GetSection", GetSection),
			new LuaMethod("CheckSectionComplete", CheckSectionComplete),
			new LuaMethod("CheckAllSectionComplete", CheckAllSectionComplete),
			new LuaMethod("AddChapter", AddChapter),
			new LuaMethod("AddSection", AddSection),
			new LuaMethod("CheckGetRole", CheckGetRole),
			new LuaMethod("SetGetRole", SetGetRole),
			new LuaMethod("CheckSectionActivate", CheckSectionActivate),
			new LuaMethod("SetSectionActivate", SetSectionActivate),
			new LuaMethod("InitMailInfo", InitMailInfo),
			new LuaMethod("AddNewMail", AddNewMail),
			new LuaMethod("GetMailByID", GetMailByID),
			new LuaMethod("GetMailByUUID", GetMailByUUID),
			new LuaMethod("GetMailUUIDByID", GetMailUUIDByID),
			new LuaMethod("ReadMail", ReadMail),
			new LuaMethod("GetMailAttachment", GetMailAttachment),
			new LuaMethod("IsTaskCompleted", IsTaskCompleted),
			new LuaMethod("SetTaskFinished", SetTaskFinished),
			new LuaMethod("SetExerciseInfo", SetExerciseInfo),
			new LuaMethod("GetExerciseInfo", GetExerciseInfo),
			new LuaMethod("GetExerciseLevel", GetExerciseLevel),
			new LuaMethod("GetExerciseInfoList", GetExerciseInfoList),
			new LuaMethod("IsPractiseCompleted", IsPractiseCompleted),
			new LuaMethod("SetPractiseCompleted", SetPractiseCompleted),
			new LuaMethod("IsGuideCompleted", IsGuideCompleted),
			new LuaMethod("GetRemainTimes", GetRemainTimes),
			new LuaMethod("GetUncompletedGuide", GetUncompletedGuide),
			new LuaMethod("GetInterruptedGuide", GetInterruptedGuide),
			new LuaMethod("SetGuideCompleted", SetGuideCompleted),
			new LuaMethod("GetRolePassiveSkillAttr", GetRolePassiveSkillAttr),
			new LuaMethod("GetMemberQualityAttr", GetMemberQualityAttr),
			new LuaMethod("GetRoleAttrs", GetRoleAttrs),
			new LuaMethod("GetRoleAttrsByID", GetRoleAttrsByID),
			new LuaMethod("GetAttrValue", GetAttrValue),
			new LuaMethod("GetEquipmentAttr", GetEquipmentAttr),
			new LuaMethod("GetEquipmentSuitAttr", GetEquipmentSuitAttr),
			new LuaMethod("GetBadgeBookAttrByBookId", GetBadgeBookAttrByBookId),
			new LuaMethod("CalcFightingCapacity", CalcFightingCapacity),
			new LuaMethod("SendFightPowerChange", SendFightPowerChange),
			new LuaMethod("CalcBaseFighting", CalcBaseFighting),
			new LuaMethod("MapsPromoteAttr", MapsPromoteAttr),
			new LuaMethod("FashionPromoteAttr", FashionPromoteAttr),
			new LuaMethod("SkillPromoteAttr", SkillPromoteAttr),
			new LuaMethod("CalcPromoteAttr", CalcPromoteAttr),
			new LuaMethod("SetBullFightTimes", SetBullFightTimes),
			new LuaMethod("SetBullFightCompleteByClient", SetBullFightCompleteByClient),
			new LuaMethod("AddShootGameModeInfo", AddShootGameModeInfo),
			new LuaMethod("ClearShootGameModeInfo", ClearShootGameModeInfo),
			new LuaMethod("SetShootedTimes", SetShootedTimes),
			new LuaMethod("SetShootCompleteByClient", SetShootCompleteByClient),
			new LuaMethod("GetFightRole", GetFightRole),
			new LuaMethod("SetFightRole", SetFightRole),
			new LuaMethod("IsInSquad", IsInSquad),
			new LuaMethod("GetEquipInfoByPos", GetEquipInfoByPos),
			new LuaMethod("GetEquipInfoBySlotID", GetEquipInfoBySlotID),
			new LuaMethod("SetEquipInfoByPos", SetEquipInfoByPos),
			new LuaMethod("SetEquipInfoBySlotID", SetEquipInfoBySlotID),
			new LuaMethod("OnNewDayComeMidNight", OnNewDayComeMidNight),
			new LuaMethod("AddOnMidNightCome", AddOnMidNightCome),
			new LuaMethod("RemoveOnMidNightCome", RemoveOnMidNightCome),
			new LuaMethod("AddCreateNewRoleLog", AddCreateNewRoleLog),
			new LuaMethod("SendGoodsLog", SendGoodsLog),
			new LuaMethod("SendPlayerLog", SendPlayerLog),
			new LuaMethod("SendGiftExchangeCode", SendGiftExchangeCode),
			new LuaMethod("OpenPlayerPlat", OpenPlayerPlat),
			new LuaMethod("EnterServiceQuestion", EnterServiceQuestion),
			new LuaMethod("ConfirmBuy", ConfirmBuy),
			new LuaMethod("ConfirmCommonBuy", ConfirmCommonBuy),
			new LuaMethod("Pay", Pay),
			new LuaMethod("SetVipExpGoodsBuyTimes", SetVipExpGoodsBuyTimes),
			new LuaMethod("IsTrialState", IsTrialState),
			new LuaMethod("IsTrialNotReceive", IsTrialNotReceive),
			new LuaMethod("GetTrialRedList", GetTrialRedList),
			new LuaMethod("ResetQualifyingNewGrades", ResetQualifyingNewGrades),
			new LuaMethod("New", _CreateMainPlayer),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("_isInitialized", get__isInitialized, set__isInitialized),
			new LuaField("onDataChanged", get_onDataChanged, set_onDataChanged),
			new LuaField("activityInfo", get_activityInfo, set_activityInfo),
			new LuaField("VipRechargeList", get_VipRechargeList, set_VipRechargeList),
			new LuaField("VipExpGoodsBuyInfo", get_VipExpGoodsBuyInfo, set_VipExpGoodsBuyInfo),
			new LuaField("CaptainInfos", get_CaptainInfos, set_CaptainInfos),
			new LuaField("AllGoodsList", get_AllGoodsList, set_AllGoodsList),
			new LuaField("RoleGoodsList", get_RoleGoodsList, set_RoleGoodsList),
			new LuaField("SkillGoodsList", get_SkillGoodsList, set_SkillGoodsList),
			new LuaField("FavoriteGoodsList", get_FavoriteGoodsList, set_FavoriteGoodsList),
			new LuaField("ConsumeGoodsList", get_ConsumeGoodsList, set_ConsumeGoodsList),
			new LuaField("EquipmentGoodsList", get_EquipmentGoodsList, set_EquipmentGoodsList),
			new LuaField("FashionGoodsList", get_FashionGoodsList, set_FashionGoodsList),
			new LuaField("TrainingList", get_TrainingList, set_TrainingList),
			new LuaField("MaterialList", get_MaterialList, set_MaterialList),
			new LuaField("BadgeGoodsList", get_BadgeGoodsList, set_BadgeGoodsList),
			new LuaField("FightRoleList", get_FightRoleList, set_FightRoleList),
			new LuaField("ChapterList", get_ChapterList, set_ChapterList),
			new LuaField("HpRestoreTime", get_HpRestoreTime, set_HpRestoreTime),
			new LuaField("HpBuyTimes", get_HpBuyTimes, set_HpBuyTimes),
			new LuaField("GoldBuyTimes", get_GoldBuyTimes, set_GoldBuyTimes),
			new LuaField("taskInfo", get_taskInfo, set_taskInfo),
			new LuaField("signInfo", get_signInfo, set_signInfo),
			new LuaField("playerLevelAwardInfo", get_playerLevelAwardInfo, set_playerLevelAwardInfo),
			new LuaField("MoonHp", get_MoonHp, set_MoonHp),
			new LuaField("EvenHp", get_EvenHp, set_EvenHp),
			new LuaField("ThirdHp", get_ThirdHp, set_ThirdHp),
			new LuaField("LotteryInfo", get_LotteryInfo, set_LotteryInfo),
			new LuaField("QualifyingInfo", get_QualifyingInfo, set_QualifyingInfo),
			new LuaField("BullFight", get_BullFight, set_BullFight),
			new LuaField("ShootGameModeInfo", get_ShootGameModeInfo, set_ShootGameModeInfo),
			new LuaField("ShootInfo", get_ShootInfo, set_ShootInfo),
			new LuaField("QualifyingNewerInfo", get_QualifyingNewerInfo, set_QualifyingNewerInfo),
			new LuaField("QualifyingNewerScore", get_QualifyingNewerScore, set_QualifyingNewerScore),
			new LuaField("QualifyingNewerTime", get_QualifyingNewerTime, set_QualifyingNewerTime),
			new LuaField("SquadInfo", get_SquadInfo, set_SquadInfo),
			new LuaField("EquipInfo", get_EquipInfo, set_EquipInfo),
			new LuaField("ExerciseInfos", get_ExerciseInfos, set_ExerciseInfos),
			new LuaField("RaceInfo", get_RaceInfo, set_RaceInfo),
			new LuaField("CurScore", get_CurScore, set_CurScore),
			new LuaField("LastScore", get_LastScore, set_LastScore),
			new LuaField("WinningStreak", get_WinningStreak, set_WinningStreak),
			new LuaField("QualifyingRanking", get_QualifyingRanking, set_QualifyingRanking),
			new LuaField("tourInfo", get_tourInfo, set_tourInfo),
			new LuaField("CurTourID", get_CurTourID, set_CurTourID),
			new LuaField("MaxTourID", get_MaxTourID, set_MaxTourID),
			new LuaField("TourResetTimes", get_TourResetTimes, set_TourResetTimes),
			new LuaField("TourFailTimes", get_TourFailTimes, set_TourFailTimes),
			new LuaField("VipGifts", get_VipGifts, set_VipGifts),
			new LuaField("CreateStep", get_CreateStep, set_CreateStep),
			new LuaField("CreateTime", get_CreateTime, set_CreateTime),
			new LuaField("NewComerSign", get_NewComerSign, set_NewComerSign),
			new LuaField("PvpPlusInfo", get_PvpPlusInfo, set_PvpPlusInfo),
			new LuaField("MailList", get_MailList, set_MailList),
			new LuaField("PlayerList", get_PlayerList, set_PlayerList),
			new LuaField("TrainingInfoList", get_TrainingInfoList, set_TrainingInfoList),
			new LuaField("StoreRefreshTimes", get_StoreRefreshTimes, set_StoreRefreshTimes),
			new LuaField("practice_cd", get_practice_cd, set_practice_cd),
			new LuaField("trialFlag", get_trialFlag, set_trialFlag),
			new LuaField("trialTotalScore", get_trialTotalScore, set_trialTotalScore),
			new LuaField("pvpLadderInfo", get_pvpLadderInfo, set_pvpLadderInfo),
			new LuaField("pvpLadderScore", get_pvpLadderScore, set_pvpLadderScore),
			new LuaField("inPvpJoining", get_inPvpJoining, set_inPvpJoining),
			new LuaField("Vip", get_Vip, set_Vip),
			new LuaField("IsLastShootGame", get_IsLastShootGame, set_IsLastShootGame),
			new LuaField("BullFightNpc", get_BullFightNpc, set_BullFightNpc),
			new LuaField("pvp_regular", get_pvp_regular, set_pvp_regular),
			new LuaField("qualifying_new", get_qualifying_new, set_qualifying_new),
			new LuaField("curShootGameMode", get_curShootGameMode, set_curShootGameMode),
			new LuaField("LinkRoleId", get_LinkRoleId, set_LinkRoleId),
			new LuaField("LinkExerciseId", get_LinkExerciseId, set_LinkExerciseId),
			new LuaField("LinkExerciseLeft", get_LinkExerciseLeft, set_LinkExerciseLeft),
			new LuaField("LinkTab", get_LinkTab, set_LinkTab),
			new LuaField("LinkUiName", get_LinkUiName, set_LinkUiName),
			new LuaField("LinkEnable", get_LinkEnable, set_LinkEnable),
			new LuaField("onMidNightCome", get_onMidNightCome, set_onMidNightCome),
			new LuaField("onAnnounce", get_onAnnounce, set_onAnnounce),
			new LuaField("AnnouncementList", get_AnnouncementList, set_AnnouncementList),
			new LuaField("onNewChatMessage", get_onNewChatMessage, set_onNewChatMessage),
			new LuaField("onFriendChatMessage", get_onFriendChatMessage, set_onFriendChatMessage),
			new LuaField("OnTeamChatMessage", get_OnTeamChatMessage, set_OnTeamChatMessage),
			new LuaField("OnLeagueChatMessage", get_OnLeagueChatMessage, set_OnLeagueChatMessage),
			new LuaField("FriendChatMessage", get_FriendChatMessage, null),
			new LuaField("TeamChatMessage", get_TeamChatMessage, null),
			new LuaField("LeagueChatMessage", get_LeagueChatMessage, null),
			new LuaField("ChatWordsNum", get_ChatWordsNum, set_ChatWordsNum),
			new LuaField("WorldChatList", get_WorldChatList, set_WorldChatList),
			new LuaField("MapIDInfo", get_MapIDInfo, set_MapIDInfo),
			new LuaField("NewMapIDList", get_NewMapIDList, set_NewMapIDList),
			new LuaField("InitMap", get_InitMap, set_InitMap),
			new LuaField("onCheckUpdate", get_onCheckUpdate, set_onCheckUpdate),
			new LuaField("CanGoCenter", get_CanGoCenter, set_CanGoCenter),
			new LuaField("CanSwitchAccount", get_CanSwitchAccount, set_CanSwitchAccount),
			new LuaField("CanLogout", get_CanLogout, set_CanLogout),
			new LuaField("SDKLogin", get_SDKLogin, set_SDKLogin),
			new LuaField("badgeSystemInfo", get_badgeSystemInfo, set_badgeSystemInfo),
			new LuaField("AccountID", get_AccountID, set_AccountID),
			new LuaField("Name", get_Name, set_Name),
			new LuaField("prev_level", get_prev_level, null),
			new LuaField("Level", get_Level, set_Level),
			new LuaField("Exp", get_Exp, set_Exp),
			new LuaField("Gold", get_Gold, set_Gold),
			new LuaField("DiamondFree", get_DiamondFree, set_DiamondFree),
			new LuaField("DiamondBuy", get_DiamondBuy, set_DiamondBuy),
			new LuaField("Honor", get_Honor, set_Honor),
			new LuaField("Honor2", get_Honor2, set_Honor2),
			new LuaField("Prestige", get_Prestige, set_Prestige),
			new LuaField("Prestige2", get_Prestige2, set_Prestige2),
			new LuaField("Reputation", get_Reputation, set_Reputation),
			new LuaField("prev_hp", get_prev_hp, null),
			new LuaField("Hp", get_Hp, set_Hp),
			new LuaField("Icon", get_Icon, set_Icon),
			new LuaField("Captain", get_Captain, set_Captain),
			new LuaField("CaptainID", get_CaptainID, set_CaptainID),
			new LuaField("VipExp", get_VipExp, set_VipExp),
			new LuaField("BullFightHard", get_BullFightHard, set_BullFightHard),
			new LuaField("MassBallHard", get_MassBallHard, set_MassBallHard),
			new LuaField("GrabZoneHard", get_GrabZoneHard, set_GrabZoneHard),
			new LuaField("GrabPointHard", get_GrabPointHard, set_GrabPointHard),
			new LuaField("PvpRunTimes", get_PvpRunTimes, set_PvpRunTimes),
			new LuaField("PvpPointBuyTimes", get_PvpPointBuyTimes, set_PvpPointBuyTimes),
			new LuaField("Tourist", null, set_Tourist),
			new LuaField("BuyItemId", null, set_BuyItemId),
			new LuaField("BuyItemNum", null, set_BuyItemNum),
			new LuaField("BuyItemCost", null, set_BuyItemCost),
			new LuaField("ZQServerId", get_ZQServerId, set_ZQServerId),
			new LuaField("NewGoodsTabData", get_NewGoodsTabData, null),
			new LuaField("NewGiftTabData", get_NewGiftTabData, null),
			new LuaField("NewBadgeTabData", get_NewBadgeTabData, null),
			new LuaField("assist_first_win_times",get_assist_first_win_times,null),
		};

		LuaScriptMgr.RegisterLib(L, "MainPlayer", typeof(MainPlayer), regs, fields, typeof(Singleton<MainPlayer>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMainPlayer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MainPlayer obj = new MainPlayer();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MainPlayer.New");
		}

		return 0;
	}

	static Type classType = typeof(MainPlayer);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__isInitialized(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isInitialized");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isInitialized on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._isInitialized);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDataChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDataChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDataChanged on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDataChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assist_first_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assist_first_win_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assist_first_win_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.assist_first_win_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activityInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activityInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activityInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.activityInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VipRechargeList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipRechargeList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipRechargeList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.VipRechargeList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VipExpGoodsBuyInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipExpGoodsBuyInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipExpGoodsBuyInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.VipExpGoodsBuyInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaptainInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CaptainInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CaptainInfos on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.CaptainInfos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AllGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AllGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AllGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AllGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RoleGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RoleGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SkillGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SkillGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SkillGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.SkillGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FavoriteGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FavoriteGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FavoriteGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FavoriteGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ConsumeGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ConsumeGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ConsumeGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ConsumeGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EquipmentGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipmentGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipmentGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.EquipmentGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FashionGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FashionGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TrainingList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TrainingList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MaterialList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MaterialList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MaterialList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MaterialList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BadgeGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeGoodsList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BadgeGoodsList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FightRoleList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FightRoleList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FightRoleList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FightRoleList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ChapterList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ChapterList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ChapterList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ChapterList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HpRestoreTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HpRestoreTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HpRestoreTime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.HpRestoreTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HpBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HpBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HpBuyTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.HpBuyTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GoldBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoldBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoldBuyTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.GoldBuyTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_taskInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name taskInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index taskInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.taskInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_signInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.signInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playerLevelAwardInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playerLevelAwardInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playerLevelAwardInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.playerLevelAwardInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MoonHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MoonHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MoonHp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.MoonHp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EvenHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EvenHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EvenHp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.EvenHp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ThirdHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ThirdHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ThirdHp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ThirdHp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LotteryInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LotteryInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LotteryInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LotteryInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QualifyingInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.QualifyingInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BullFight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFight on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BullFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ShootGameModeInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ShootGameModeInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ShootGameModeInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ShootGameModeInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ShootInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ShootInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ShootInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ShootInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QualifyingNewerInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.QualifyingNewerInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QualifyingNewerScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.QualifyingNewerScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QualifyingNewerTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.QualifyingNewerTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SquadInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SquadInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SquadInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.SquadInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EquipInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.EquipInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ExerciseInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ExerciseInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ExerciseInfos on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ExerciseInfos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RaceInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RaceInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RaceInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RaceInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CurScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CurScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LastScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LastScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LastScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LastScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WinningStreak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WinningStreak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WinningStreak on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.WinningStreak);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_QualifyingRanking(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingRanking");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingRanking on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.QualifyingRanking);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tourInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tourInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tourInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.tourInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CurTourID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurTourID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurTourID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CurTourID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MaxTourID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MaxTourID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MaxTourID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.MaxTourID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TourResetTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourResetTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourResetTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.TourResetTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TourFailTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourFailTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourFailTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.TourFailTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VipGifts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipGifts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipGifts on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.VipGifts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CreateStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CreateStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CreateStep on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CreateStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CreateTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CreateTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CreateTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CreateTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewComerSign(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewComerSign");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewComerSign on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewComerSign);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PvpPlusInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpPlusInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpPlusInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PvpPlusInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MailList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MailList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MailList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MailList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PlayerList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PlayerList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PlayerList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PlayerList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TrainingInfoList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingInfoList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingInfoList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TrainingInfoList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StoreRefreshTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StoreRefreshTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StoreRefreshTimes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.StoreRefreshTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_practice_cd(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name practice_cd");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index practice_cd on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.practice_cd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trialFlag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialFlag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialFlag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.trialFlag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trialTotalScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialTotalScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialTotalScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.trialTotalScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpLadderInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpLadderInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpLadderInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvpLadderInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpLadderScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpLadderScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpLadderScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pvpLadderScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inPvpJoining(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inPvpJoining");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inPvpJoining on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.inPvpJoining);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Vip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Vip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsLastShootGame(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsLastShootGame");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsLastShootGame on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsLastShootGame);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BullFightNpc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFightNpc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFightNpc on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BullFightNpc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvp_regular(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp_regular");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp_regular on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvp_regular);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifying_new(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifying_new");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifying_new on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifying_new);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curShootGameMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curShootGameMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curShootGameMode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.curShootGameMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkRoleId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkRoleId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkRoleId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkRoleId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkExerciseId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkExerciseId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkExerciseId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkExerciseId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkExerciseLeft(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkExerciseLeft");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkExerciseLeft on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkExerciseLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkTab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkTab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkTab on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkTab);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkUiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkUiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkUiName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkUiName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LinkEnable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkEnable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkEnable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LinkEnable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onMidNightCome(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMidNightCome");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMidNightCome on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onMidNightCome);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAnnounce(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAnnounce");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAnnounce on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onAnnounce);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AnnouncementList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AnnouncementList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AnnouncementList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AnnouncementList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onNewChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onNewChatMessage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFriendChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onFriendChatMessage);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnTeamChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.OnTeamChatMessage);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnLeagueChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.OnLeagueChatMessage);
		return 1;
	}
	static int get_FriendChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FriendChatMessage);
		return 1;
	}
	static int get_TeamChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TeamChatMessage);
		return 1;
	}
	static int get_LeagueChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LeagueChatMessage);
		return 1;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ChatWordsNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ChatWordsNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ChatWordsNum on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ChatWordsNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WorldChatList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WorldChatList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WorldChatList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.WorldChatList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MapIDInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MapIDInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MapIDInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MapIDInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewMapIDList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewMapIDList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewMapIDList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewMapIDList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_InitMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name InitMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index InitMap on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.InitMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCheckUpdate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCheckUpdate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCheckUpdate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onCheckUpdate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CanGoCenter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanGoCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanGoCenter on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CanGoCenter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CanSwitchAccount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanSwitchAccount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanSwitchAccount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CanSwitchAccount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CanLogout(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanLogout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanLogout on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CanLogout);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SDKLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SDKLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SDKLogin on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.SDKLogin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_badgeSystemInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badgeSystemInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badgeSystemInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.badgeSystemInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AccountID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AccountID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AccountID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.AccountID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prev_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prev_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prev_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.prev_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Exp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Exp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Gold(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Gold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Gold on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Gold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DiamondFree(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DiamondFree");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DiamondFree on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.DiamondFree);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DiamondBuy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DiamondBuy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DiamondBuy on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.DiamondBuy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Honor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Honor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Honor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Honor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Honor2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Honor2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Honor2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Honor2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Prestige(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Prestige");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Prestige on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Prestige);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Prestige2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Prestige2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Prestige2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Prestige2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Reputation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Reputation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Reputation on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Reputation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prev_hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prev_hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prev_hp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.prev_hp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Hp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Hp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Captain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Captain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Captain on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Captain);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CaptainID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CaptainID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CaptainID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.CaptainID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VipExp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipExp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipExp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.VipExp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BullFightHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFightHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFightHard on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.BullFightHard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MassBallHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MassBallHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MassBallHard on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.MassBallHard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GrabZoneHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GrabZoneHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GrabZoneHard on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.GrabZoneHard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GrabPointHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GrabPointHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GrabPointHard on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.GrabPointHard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PvpRunTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpRunTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpRunTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.PvpRunTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PvpPointBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpPointBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpPointBuyTimes on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.PvpPointBuyTimes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ZQServerId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ZQServerId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ZQServerId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ZQServerId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewGoodsTabData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewGoodsTabData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewGoodsTabData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewGoodsTabData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewGiftTabData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewGiftTabData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewGiftTabData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewGiftTabData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewBadgeTabData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewBadgeTabData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewBadgeTabData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewBadgeTabData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__isInitialized(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isInitialized");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isInitialized on a nil value");
			}
		}

		obj._isInitialized = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDataChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDataChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDataChanged on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDataChanged = (MainPlayer.DataChangedDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(MainPlayer.DataChangedDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDataChanged = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_activityInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activityInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activityInfo on a nil value");
			}
		}

		obj.activityInfo = (fogs.proto.msg.ActivityInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.ActivityInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VipRechargeList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipRechargeList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipRechargeList on a nil value");
			}
		}

		obj.VipRechargeList = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VipExpGoodsBuyInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipExpGoodsBuyInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipExpGoodsBuyInfo on a nil value");
			}
		}

		obj.VipExpGoodsBuyInfo = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CaptainInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CaptainInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CaptainInfos on a nil value");
			}
		}

		obj.CaptainInfos = (List<fogs.proto.msg.RoleInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.RoleInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AllGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AllGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AllGoodsList on a nil value");
			}
		}

		obj.AllGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RoleGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleGoodsList on a nil value");
			}
		}

		obj.RoleGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SkillGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SkillGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SkillGoodsList on a nil value");
			}
		}

		obj.SkillGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FavoriteGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FavoriteGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FavoriteGoodsList on a nil value");
			}
		}

		obj.FavoriteGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ConsumeGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ConsumeGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ConsumeGoodsList on a nil value");
			}
		}

		obj.ConsumeGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EquipmentGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipmentGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipmentGoodsList on a nil value");
			}
		}

		obj.EquipmentGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FashionGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionGoodsList on a nil value");
			}
		}

		obj.FashionGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TrainingList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingList on a nil value");
			}
		}

		obj.TrainingList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MaterialList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MaterialList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MaterialList on a nil value");
			}
		}

		obj.MaterialList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BadgeGoodsList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeGoodsList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeGoodsList on a nil value");
			}
		}

		obj.BadgeGoodsList = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<ulong,Goods>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FightRoleList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FightRoleList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FightRoleList on a nil value");
			}
		}

		obj.FightRoleList = (Dictionary<fogs.proto.msg.GameMode,List<fogs.proto.msg.FightRole>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<fogs.proto.msg.GameMode,List<fogs.proto.msg.FightRole>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ChapterList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ChapterList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ChapterList on a nil value");
			}
		}

		obj.ChapterList = (Dictionary<uint,Chapter>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Chapter>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HpRestoreTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HpRestoreTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HpRestoreTime on a nil value");
			}
		}

		obj.HpRestoreTime = (DateTime)LuaScriptMgr.GetNetObject(L, 3, typeof(DateTime));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HpBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HpBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HpBuyTimes on a nil value");
			}
		}

		obj.HpBuyTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GoldBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoldBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoldBuyTimes on a nil value");
			}
		}

		obj.GoldBuyTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_taskInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name taskInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index taskInfo on a nil value");
			}
		}

		obj.taskInfo = (fogs.proto.msg.TaskInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.TaskInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_signInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signInfo on a nil value");
			}
		}

		obj.signInfo = (fogs.proto.msg.SignInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.SignInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playerLevelAwardInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playerLevelAwardInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playerLevelAwardInfo on a nil value");
			}
		}

		obj.playerLevelAwardInfo = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MoonHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MoonHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MoonHp on a nil value");
			}
		}

		obj.MoonHp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EvenHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EvenHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EvenHp on a nil value");
			}
		}

		obj.EvenHp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ThirdHp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ThirdHp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ThirdHp on a nil value");
			}
		}

		obj.ThirdHp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LotteryInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LotteryInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LotteryInfo on a nil value");
			}
		}

		obj.LotteryInfo = (fogs.proto.msg.LotteryInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.LotteryInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QualifyingInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingInfo on a nil value");
			}
		}

		obj.QualifyingInfo = (fogs.proto.msg.QualifyingInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.QualifyingInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BullFight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFight on a nil value");
			}
		}

		obj.BullFight = (fogs.proto.msg.BullFight)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.BullFight));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ShootGameModeInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ShootGameModeInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ShootGameModeInfo on a nil value");
			}
		}

		obj.ShootGameModeInfo = (List<fogs.proto.msg.GameModeInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.GameModeInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ShootInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ShootInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ShootInfo on a nil value");
			}
		}

		obj.ShootInfo = (fogs.proto.msg.ShootInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.ShootInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QualifyingNewerInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerInfo on a nil value");
			}
		}

		obj.QualifyingNewerInfo = (fogs.proto.msg.QualifyingNewerInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.QualifyingNewerInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QualifyingNewerScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerScore on a nil value");
			}
		}

		obj.QualifyingNewerScore = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QualifyingNewerTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingNewerTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingNewerTime on a nil value");
			}
		}

		obj.QualifyingNewerTime = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SquadInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SquadInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SquadInfo on a nil value");
			}
		}

		obj.SquadInfo = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FightRole>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EquipInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipInfo on a nil value");
			}
		}

		obj.EquipInfo = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.EquipInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ExerciseInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ExerciseInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ExerciseInfos on a nil value");
			}
		}

		obj.ExerciseInfos = (Dictionary<uint,List<fogs.proto.msg.ExerciseInfo>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<fogs.proto.msg.ExerciseInfo>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RaceInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RaceInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RaceInfo on a nil value");
			}
		}

		obj.RaceInfo = (fogs.proto.msg.RaceInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RaceInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CurScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurScore on a nil value");
			}
		}

		obj.CurScore = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LastScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LastScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LastScore on a nil value");
			}
		}

		obj.LastScore = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_WinningStreak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WinningStreak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WinningStreak on a nil value");
			}
		}

		obj.WinningStreak = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_QualifyingRanking(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name QualifyingRanking");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index QualifyingRanking on a nil value");
			}
		}

		obj.QualifyingRanking = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tourInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tourInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tourInfo on a nil value");
			}
		}

		obj.tourInfo = (fogs.proto.msg.TourInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.TourInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CurTourID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurTourID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurTourID on a nil value");
			}
		}

		obj.CurTourID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MaxTourID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MaxTourID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MaxTourID on a nil value");
			}
		}

		obj.MaxTourID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TourResetTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourResetTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourResetTimes on a nil value");
			}
		}

		obj.TourResetTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TourFailTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourFailTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourFailTimes on a nil value");
			}
		}

		obj.TourFailTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VipGifts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipGifts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipGifts on a nil value");
			}
		}

		obj.VipGifts = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CreateStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CreateStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CreateStep on a nil value");
			}
		}

		obj.CreateStep = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CreateTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CreateTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CreateTime on a nil value");
			}
		}

		obj.CreateTime = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NewComerSign(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewComerSign");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewComerSign on a nil value");
			}
		}

		obj.NewComerSign = (fogs.proto.msg.NewComerInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.NewComerInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PvpPlusInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpPlusInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpPlusInfo on a nil value");
			}
		}

		obj.PvpPlusInfo = (fogs.proto.msg.PvpPlusInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PvpPlusInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MailList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MailList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MailList on a nil value");
			}
		}

		obj.MailList = (List<fogs.proto.msg.MailInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.MailInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PlayerList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PlayerList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PlayerList on a nil value");
			}
		}

		obj.PlayerList = (List<Player>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Player>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TrainingInfoList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingInfoList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingInfoList on a nil value");
			}
		}

		obj.TrainingInfoList = (List<fogs.proto.msg.TrainingInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.TrainingInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_StoreRefreshTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StoreRefreshTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StoreRefreshTimes on a nil value");
			}
		}

		obj.StoreRefreshTimes = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_practice_cd(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name practice_cd");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index practice_cd on a nil value");
			}
		}

		obj.practice_cd = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_trialFlag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialFlag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialFlag on a nil value");
			}
		}

		obj.trialFlag = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_trialTotalScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialTotalScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialTotalScore on a nil value");
			}
		}

		obj.trialTotalScore = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpLadderInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpLadderInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpLadderInfo on a nil value");
			}
		}

		obj.pvpLadderInfo = (fogs.proto.msg.PvpLadderInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PvpLadderInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpLadderScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpLadderScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpLadderScore on a nil value");
			}
		}

		obj.pvpLadderScore = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_inPvpJoining(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inPvpJoining");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inPvpJoining on a nil value");
			}
		}

		obj.inPvpJoining = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Vip on a nil value");
			}
		}

		obj.Vip = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IsLastShootGame(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsLastShootGame");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsLastShootGame on a nil value");
			}
		}

		obj.IsLastShootGame = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BullFightNpc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFightNpc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFightNpc on a nil value");
			}
		}

		obj.BullFightNpc = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvp_regular(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp_regular");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp_regular on a nil value");
			}
		}

		obj.pvp_regular = (fogs.proto.msg.PvpRegularInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PvpRegularInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifying_new(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifying_new");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifying_new on a nil value");
			}
		}

		obj.qualifying_new = (fogs.proto.msg.QualifyingNewInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.QualifyingNewInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_curShootGameMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curShootGameMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curShootGameMode on a nil value");
			}
		}

		obj.curShootGameMode = (fogs.proto.msg.GameMode)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GameMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkRoleId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkRoleId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkRoleId on a nil value");
			}
		}

		obj.LinkRoleId = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkExerciseId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkExerciseId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkExerciseId on a nil value");
			}
		}

		obj.LinkExerciseId = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkExerciseLeft(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkExerciseLeft");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkExerciseLeft on a nil value");
			}
		}

		obj.LinkExerciseLeft = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkTab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkTab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkTab on a nil value");
			}
		}

		obj.LinkTab = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkUiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkUiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkUiName on a nil value");
			}
		}

		obj.LinkUiName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LinkEnable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LinkEnable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LinkEnable on a nil value");
			}
		}

		obj.LinkEnable = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onMidNightCome(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMidNightCome");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMidNightCome on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onMidNightCome = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onMidNightCome = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onAnnounce(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAnnounce");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAnnounce on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onAnnounce = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onAnnounce = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AnnouncementList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AnnouncementList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AnnouncementList on a nil value");
			}
		}

		obj.AnnouncementList = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onNewChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onNewChatMessage = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onNewChatMessage = () =>
			{
				func.Call();
			};
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFriendChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onFriendChatMessage = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onFriendChatMessage = () =>
			{
				func.Call();
			};
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnTeamChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.OnTeamChatMessage = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.OnTeamChatMessage = () =>
			{
				func.Call();
			};
		}
		return 0;
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnLeagueChatMessage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNewChatMessage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNewChatMessage on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.OnLeagueChatMessage = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.OnLeagueChatMessage = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ChatWordsNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ChatWordsNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ChatWordsNum on a nil value");
			}
		}

		obj.ChatWordsNum = (List<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<int>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_WorldChatList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WorldChatList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WorldChatList on a nil value");
			}
		}

		obj.WorldChatList = (List<fogs.proto.msg.ChatBroadcast>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.ChatBroadcast>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MapIDInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MapIDInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MapIDInfo on a nil value");
			}
		}

		obj.MapIDInfo = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NewMapIDList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewMapIDList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewMapIDList on a nil value");
			}
		}

		obj.NewMapIDList = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_InitMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name InitMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index InitMap on a nil value");
			}
		}

		obj.InitMap = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onCheckUpdate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCheckUpdate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCheckUpdate on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onCheckUpdate = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onCheckUpdate = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CanGoCenter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanGoCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanGoCenter on a nil value");
			}
		}

		obj.CanGoCenter = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CanSwitchAccount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanSwitchAccount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanSwitchAccount on a nil value");
			}
		}

		obj.CanSwitchAccount = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CanLogout(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CanLogout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CanLogout on a nil value");
			}
		}

		obj.CanLogout = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SDKLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SDKLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SDKLogin on a nil value");
			}
		}

		obj.SDKLogin = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_badgeSystemInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badgeSystemInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badgeSystemInfo on a nil value");
			}
		}

		obj.badgeSystemInfo = (BadgeSystemInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(BadgeSystemInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AccountID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AccountID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AccountID on a nil value");
			}
		}

		obj.AccountID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Name on a nil value");
			}
		}

		obj.Name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Level on a nil value");
			}
		}

		obj.Level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Exp on a nil value");
			}
		}

		obj.Exp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Gold(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Gold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Gold on a nil value");
			}
		}

		obj.Gold = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DiamondFree(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DiamondFree");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DiamondFree on a nil value");
			}
		}

		obj.DiamondFree = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DiamondBuy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DiamondBuy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DiamondBuy on a nil value");
			}
		}

		obj.DiamondBuy = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Honor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Honor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Honor on a nil value");
			}
		}

		obj.Honor = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Honor2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Honor2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Honor2 on a nil value");
			}
		}

		obj.Honor2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Prestige(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Prestige");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Prestige on a nil value");
			}
		}

		obj.Prestige = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Prestige2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Prestige2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Prestige2 on a nil value");
			}
		}

		obj.Prestige2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Reputation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Reputation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Reputation on a nil value");
			}
		}

		obj.Reputation = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Hp on a nil value");
			}
		}

		obj.Hp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Icon on a nil value");
			}
		}

		obj.Icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Captain(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Captain");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Captain on a nil value");
			}
		}

		obj.Captain = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CaptainID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CaptainID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CaptainID on a nil value");
			}
		}

		obj.CaptainID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VipExp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipExp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipExp on a nil value");
			}
		}

		obj.VipExp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BullFightHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BullFightHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BullFightHard on a nil value");
			}
		}

		obj.BullFightHard = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MassBallHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MassBallHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MassBallHard on a nil value");
			}
		}

		obj.MassBallHard = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GrabZoneHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GrabZoneHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GrabZoneHard on a nil value");
			}
		}

		obj.GrabZoneHard = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GrabPointHard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GrabPointHard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GrabPointHard on a nil value");
			}
		}

		obj.GrabPointHard = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PvpRunTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpRunTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpRunTimes on a nil value");
			}
		}

		obj.PvpRunTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PvpPointBuyTimes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PvpPointBuyTimes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PvpPointBuyTimes on a nil value");
			}
		}

		obj.PvpPointBuyTimes = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Tourist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Tourist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Tourist on a nil value");
			}
		}

		obj.Tourist = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BuyItemId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyItemId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyItemId on a nil value");
			}
		}

		obj.BuyItemId = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BuyItemNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyItemNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyItemNum on a nil value");
			}
		}

		obj.BuyItemNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BuyItemCost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyItemCost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyItemCost on a nil value");
			}
		}

		obj.BuyItemCost = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ZQServerId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MainPlayer obj = (MainPlayer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ZQServerId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ZQServerId on a nil value");
			}
		}

		obj.ZQServerId = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Uninitialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.Uninitialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddDataChangedDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.AddDataChangedDelegate(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveDataChangedDelegate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.RemoveDataChangedDelegate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBaseInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.PlayerInfo arg0 = (fogs.proto.msg.PlayerInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PlayerInfo));
		obj.SetBaseInfo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateEverySecond(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		System.Timers.ElapsedEventArgs arg1 = (System.Timers.ElapsedEventArgs)LuaScriptMgr.GetNetObject(L, 3, typeof(System.Timers.ElapsedEventArgs));
		obj.UpdateEverySecond(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnNewDayCome(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.OnNewDayCome();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateCaptainObj(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.CreateCaptainObj();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.HasRole(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Player o = obj.GetRole(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRole2(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.RoleInfo o = obj.GetRole2(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleIDList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<uint> o = obj.GetRoleIDList();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRoleInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		obj.SetRoleInfo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFightRoleList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.GameMode arg0 = (fogs.proto.msg.GameMode)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GameMode));
		List<fogs.proto.msg.FightRole> o = obj.GetFightRoleList(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRoleQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.SetRoleQuality(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRoleLvAndExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint arg2 = (uint)LuaScriptMgr.GetNumber(L, 4);
		obj.SetRoleLvAndExp(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SwitchCaptain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SwitchCaptain(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCaptainInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.RoleInfo o = obj.GetCaptainInfo(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddInviteRoleInList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		obj.AddInviteRoleInList(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortGoodsDesc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Dictionary<ulong,Goods> arg0 = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 2, typeof(Dictionary<ulong,Goods>));
		Dictionary<ulong,Goods> o = obj.SortGoodsDesc(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortGoodsDescExpPriority(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Dictionary<ulong,Goods> arg0 = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 2, typeof(Dictionary<ulong,Goods>));
		Dictionary<ulong,Goods> o = obj.SortGoodsDescExpPriority(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortGoodsAsc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Dictionary<ulong,Goods> arg0 = (Dictionary<ulong,Goods>)LuaScriptMgr.GetNetObject(L, 2, typeof(Dictionary<ulong,Goods>));
		Dictionary<ulong,Goods> o = obj.SortGoodsAsc(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoods(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.GoodsCategory arg0 = (fogs.proto.msg.GoodsCategory)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GoodsCategory));
		ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
		Goods o = obj.GetGoods(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoodsList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.GoodsCategory arg0 = (fogs.proto.msg.GoodsCategory)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GoodsCategory));
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		List<Goods> o = obj.GetGoodsList(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoodsCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetGoodsCount(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddGoods(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Goods arg0 = (Goods)LuaScriptMgr.GetNetObject(L, 2, typeof(Goods));
		obj.AddGoods(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DelGoods(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.GoodsCategory arg0 = (fogs.proto.msg.GoodsCategory)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GoodsCategory));
		ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
		obj.DelGoods(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Goods o = obj.GetFashionByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTrainingByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Goods o = obj.GetTrainingByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaterialByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Goods o = obj.GetMaterialByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSkillInRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.CheckSkillInRole(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgesGoodByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Goods o = obj.GetBadgesGoodByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckChapter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.CheckChapter(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckChapterComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.CheckChapterComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetChapter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Chapter o = obj.GetChapter(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeChapterData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.ChapterProto arg0 = (fogs.proto.msg.ChapterProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.ChapterProto));
		obj.ChangeChapterData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeChaptersData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<fogs.proto.msg.ChapterProto> arg0 = (List<fogs.proto.msg.ChapterProto>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<fogs.proto.msg.ChapterProto>));
		obj.ChangeChaptersData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChaptersData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.ChapterProto arg0 = (fogs.proto.msg.ChapterProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.ChapterProto));
		obj.AddChaptersData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.CheckSection(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		Section o = obj.GetSection(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.CheckSectionComplete(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckAllSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.CheckAllSectionComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddChapter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.AddChapter(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.AddSection(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckGetRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint o = obj.CheckGetRole(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetGetRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.SetGetRole(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSectionActivate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint o = obj.CheckSectionActivate(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSectionActivate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.SetSectionActivate(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitMailInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<fogs.proto.msg.MailInfo> arg0 = (List<fogs.proto.msg.MailInfo>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<fogs.proto.msg.MailInfo>));
		obj.InitMailInfo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddNewMail(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.MailInfo arg0 = (fogs.proto.msg.MailInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.MailInfo));
		obj.AddNewMail(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMailByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.MailInfo o = obj.GetMailByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMailByUUID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.MailInfo o = obj.GetMailByUUID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMailUUIDByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		ulong o = obj.GetMailUUIDByID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadMail(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		obj.ReadMail(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMailAttachment(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		obj.GetMailAttachment(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsTaskCompleted(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsTaskCompleted(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTaskFinished(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetTaskFinished(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetExerciseInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		fogs.proto.msg.ExerciseInfo arg2 = (fogs.proto.msg.ExerciseInfo)LuaScriptMgr.GetNetObject(L, 4, typeof(fogs.proto.msg.ExerciseInfo));
		obj.SetExerciseInfo(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetExerciseInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		fogs.proto.msg.ExerciseInfo o = obj.GetExerciseInfo(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetExerciseLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint o = obj.GetExerciseLevel(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetExerciseInfoList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<fogs.proto.msg.ExerciseInfo> o = obj.GetExerciseInfoList(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPractiseCompleted(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsPractiseCompleted(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPractiseCompleted(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.SetPractiseCompleted(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsGuideCompleted(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsGuideCompleted(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRemainTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint o = obj.GetRemainTimes();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUncompletedGuide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint o = obj.GetUncompletedGuide();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInterruptedGuide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint o = obj.GetInterruptedGuide();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetGuideCompleted(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetGuideCompleted(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRolePassiveSkillAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Dictionary<uint,uint> o = obj.GetRolePassiveSkillAttr(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMemberQualityAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Dictionary<uint,uint> o = obj.GetMemberQualityAttr(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleAttrs(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		List<fogs.proto.msg.EquipInfo> arg1 = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.EquipInfo>));
		List<fogs.proto.msg.FightRole> arg2 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 4, typeof(List<fogs.proto.msg.FightRole>));
		List<uint> arg3 = (List<uint>)LuaScriptMgr.GetNetObject(L, 5, typeof(List<uint>));
		List<List<uint>> arg4 = (List<List<uint>>)LuaScriptMgr.GetNetObject(L, 6, typeof(List<List<uint>>));
		fogs.proto.msg.BadgeBook arg5 = (fogs.proto.msg.BadgeBook)LuaScriptMgr.GetNetObject(L, 7, typeof(fogs.proto.msg.BadgeBook));
		AttrData o = obj.GetRoleAttrs(arg0,arg1,arg2,arg3,arg4,arg5);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleAttrsByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		AttrData o = obj.GetRoleAttrsByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		IM.Number arg2;
		List<fogs.proto.msg.EquipInfo> arg3 = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 5, typeof(List<fogs.proto.msg.EquipInfo>));
		List<fogs.proto.msg.FightRole> arg4 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 6, typeof(List<fogs.proto.msg.FightRole>));
		uint o = obj.GetAttrValue(arg0,arg1,out arg2,arg3,arg4);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.PushValue(L, arg2);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEquipmentAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		List<fogs.proto.msg.EquipInfo> arg2 = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 4, typeof(List<fogs.proto.msg.EquipInfo>));
		List<fogs.proto.msg.FightRole> arg3 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 5, typeof(List<fogs.proto.msg.FightRole>));
		uint o = obj.GetEquipmentAttr(arg0,arg1,arg2,arg3);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEquipmentSuitAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint arg2;
		uint arg3;
		List<fogs.proto.msg.EquipInfo> arg4 = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 6, typeof(List<fogs.proto.msg.EquipInfo>));
		List<fogs.proto.msg.FightRole> arg5 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 7, typeof(List<fogs.proto.msg.FightRole>));
		uint o = obj.GetEquipmentSuitAttr(arg0,arg1,out arg2,out arg3,arg4,arg5);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg2);
		LuaScriptMgr.Push(L, arg3);
		return 3;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeBookAttrByBookId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Dictionary<uint,uint> arg0 = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 2, typeof(Dictionary<uint,uint>));
		fogs.proto.msg.BadgeBook arg1 = (fogs.proto.msg.BadgeBook)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.BadgeBook));
		obj.GetBadgeBookAttrByBookId(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalcFightingCapacity(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		AttrData arg2 = (AttrData)LuaScriptMgr.GetNetObject(L, 4, typeof(AttrData));
		obj.CalcFightingCapacity(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendFightPowerChange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.SendFightPowerChange();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalcBaseFighting(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		IM.Number o = obj.CalcBaseFighting(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MapsPromoteAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		AttrData arg0 = (AttrData)LuaScriptMgr.GetNetObject(L, 2, typeof(AttrData));
		List<uint> arg1 = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		obj.MapsPromoteAttr(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FashionPromoteAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		Dictionary<uint,uint> arg1 = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		List<List<uint>> arg2 = (List<List<uint>>)LuaScriptMgr.GetNetObject(L, 4, typeof(List<List<uint>>));
		obj.FashionPromoteAttr(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SkillPromoteAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RoleInfo));
		Dictionary<uint,uint> arg1 = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		obj.SkillPromoteAttr(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalcPromoteAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		AttrData arg0 = (AttrData)LuaScriptMgr.GetNetObject(L, 2, typeof(AttrData));
		Dictionary<uint,uint> arg1 = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		obj.CalcPromoteAttr(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBullFightTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetBullFightTimes(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBullFightCompleteByClient(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.SetBullFightCompleteByClient();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddShootGameModeInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.GameModeInfo arg0 = (fogs.proto.msg.GameModeInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GameModeInfo));
		obj.AddShootGameModeInfo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearShootGameModeInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.ClearShootGameModeInfo();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetShootedTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetShootedTimes(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetShootCompleteByClient(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.SetShootCompleteByClient();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFightRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.FightStatus arg0 = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FightStatus));
		uint o = obj.GetFightRole(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFightRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<fogs.proto.msg.FightRole> arg0 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<fogs.proto.msg.FightRole>));
		obj.SetFightRole(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInSquad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsInSquad(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEquipInfoByPos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.FightStatus arg0 = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FightStatus));
		List<fogs.proto.msg.EquipmentSlot> o = obj.GetEquipInfoByPos(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEquipInfoBySlotID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.FightStatus arg0 = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FightStatus));
		fogs.proto.msg.EquipmentSlotID arg1 = (fogs.proto.msg.EquipmentSlotID)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.EquipmentSlotID));
		ulong o = obj.GetEquipInfoBySlotID(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEquipInfoByPos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.FightStatus arg0 = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FightStatus));
		List<fogs.proto.msg.EquipmentSlot> arg1 = (List<fogs.proto.msg.EquipmentSlot>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.EquipmentSlot>));
		obj.SetEquipInfoByPos(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEquipInfoBySlotID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.FightStatus arg0 = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FightStatus));
		fogs.proto.msg.EquipmentSlotID arg1 = (fogs.proto.msg.EquipmentSlotID)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.EquipmentSlotID));
		ulong arg2 = (ulong)LuaScriptMgr.GetNumber(L, 4);
		obj.SetEquipInfoBySlotID(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnNewDayComeMidNight(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		fogs.proto.msg.NewDayCome arg0 = (fogs.proto.msg.NewDayCome)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.NewDayCome));
		obj.OnNewDayComeMidNight(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddOnMidNightCome(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.AddOnMidNightCome(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveOnMidNightCome(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RemoveOnMidNightCome(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCreateNewRoleLog(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.AddCreateNewRoleLog(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendGoodsLog(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		string arg2 = LuaScriptMgr.GetLuaString(L, 4);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 5);
		string arg4 = LuaScriptMgr.GetLuaString(L, 6);
		obj.SendGoodsLog(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendPlayerLog(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		string arg2 = LuaScriptMgr.GetLuaString(L, 4);
		obj.SendPlayerLog(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendGiftExchangeCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		string arg2 = LuaScriptMgr.GetLuaString(L, 4);
		obj.SendGiftExchangeCode(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OpenPlayerPlat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.OpenPlayerPlat();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnterServiceQuestion(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		bool o = obj.EnterServiceQuestion();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConfirmBuy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		obj.ConfirmBuy();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConfirmCommonBuy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		double arg2 = (double)LuaScriptMgr.GetNumber(L, 4);
		obj.ConfirmCommonBuy(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.Pay(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetVipExpGoodsBuyTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.SetVipExpGoodsBuyTimes(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsTrialState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		uint o = obj.IsTrialState(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsTrialNotReceive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		uint o = obj.IsTrialNotReceive();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTrialRedList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<uint> o = obj.GetTrialRedList();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetQualifyingNewGrades(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MainPlayer obj = (MainPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "MainPlayer");
		List<uint> arg0 = (List<uint>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<uint>));
		obj.ResetQualifyingNewGrades(arg0);
		return 0;
	}
}

