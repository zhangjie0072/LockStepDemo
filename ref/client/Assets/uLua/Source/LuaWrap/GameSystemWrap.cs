using System;
using LuaInterface;

public class GameSystemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Start", Start),
			new LuaMethod("PreLoadConfig", PreLoadConfig),
			new LuaMethod("LoadConfig", LoadConfig),
			new LuaMethod("ParseCommonConfig", ParseCommonConfig),
			new LuaMethod("ParseConfig", ParseConfig),
			new LuaMethod("ParsePreConfig", ParsePreConfig),
			new LuaMethod("ParseHallConfig", ParseHallConfig),
			new LuaMethod("ParseMatchConfig", ParseMatchConfig),
			new LuaMethod("LateUpdate", LateUpdate),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("Update", Update),
			new LuaMethod("ReceiveHeartbeatMsg", ReceiveHeartbeatMsg),
			new LuaMethod("SendHeartbeatMsg", SendHeartbeatMsg),
			new LuaMethod("DebugDraw", DebugDraw),
			new LuaMethod("Exit", Exit),
			new LuaMethod("New", _CreateGameSystem),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("MaxIntervalTime", get_MaxIntervalTime, null),
			new LuaField("MaxIntervalCount", get_MaxIntervalCount, null),
			new LuaField("LoseFocusTime", get_LoseFocusTime, null),
			new LuaField("mEngineFramework", get_mEngineFramework, set_mEngineFramework),
			new LuaField("AnnouncementConfigData", get_AnnouncementConfigData, set_AnnouncementConfigData),
			new LuaField("BadgeAttrConfigData", get_BadgeAttrConfigData, set_BadgeAttrConfigData),
			new LuaField("ConstStringConfigData", get_ConstStringConfigData, set_ConstStringConfigData),
			new LuaField("CommonConfig", get_CommonConfig, set_CommonConfig),
			new LuaField("ServerDataConfigData", get_ServerDataConfigData, set_ServerDataConfigData),
			new LuaField("AttrNameConfigData", get_AttrNameConfigData, set_AttrNameConfigData),
			new LuaField("RoleBaseConfigData2", get_RoleBaseConfigData2, set_RoleBaseConfigData2),
			new LuaField("AttrDataConfigData", get_AttrDataConfigData, set_AttrDataConfigData),
			new LuaField("ReboundAttrConfigData", get_ReboundAttrConfigData, set_ReboundAttrConfigData),
			new LuaField("TeamLevelConfigData", get_TeamLevelConfigData, set_TeamLevelConfigData),
			new LuaField("RoleLevelConfigData", get_RoleLevelConfigData, set_RoleLevelConfigData),
			new LuaField("NPCConfigData", get_NPCConfigData, set_NPCConfigData),
			new LuaField("CareerConfigData", get_CareerConfigData, set_CareerConfigData),
			new LuaField("SkillConfig", get_SkillConfig, set_SkillConfig),
			new LuaField("GoodsConfigData", get_GoodsConfigData, set_GoodsConfigData),
			new LuaField("StoreGoodsConfigData", get_StoreGoodsConfigData, set_StoreGoodsConfigData),
			new LuaField("BaseDataBuyConfigData", get_BaseDataBuyConfigData, set_BaseDataBuyConfigData),
			new LuaField("PVPPointConfig", get_PVPPointConfig, set_PVPPointConfig),
			new LuaField("WinningStreakAwardConfig", get_WinningStreakAwardConfig, set_WinningStreakAwardConfig),
			new LuaField("TaskConfigData", get_TaskConfigData, set_TaskConfigData),
			new LuaField("AwardPackConfigData", get_AwardPackConfigData, set_AwardPackConfigData),
			new LuaField("ArticleStrengthConfig", get_ArticleStrengthConfig, set_ArticleStrengthConfig),
			new LuaField("PhRegainConfig", get_PhRegainConfig, set_PhRegainConfig),
			new LuaField("PractiseConfig", get_PractiseConfig, set_PractiseConfig),
			new LuaField("PracticePveConfig", get_PracticePveConfig, set_PracticePveConfig),
			new LuaField("PractiseStepConfig", get_PractiseStepConfig, set_PractiseStepConfig),
			new LuaField("GameModeConfig", get_GameModeConfig, set_GameModeConfig),
			new LuaField("TrainingConfig", get_TrainingConfig, set_TrainingConfig),
			new LuaField("TattooConfig", get_TattooConfig, set_TattooConfig),
			new LuaField("EquipmentConfigData", get_EquipmentConfigData, set_EquipmentConfigData),
			new LuaField("TourConfig", get_TourConfig, set_TourConfig),
			new LuaField("GuideConfig", get_GuideConfig, set_GuideConfig),
			new LuaField("FunctionConditionConfig", get_FunctionConditionConfig, set_FunctionConditionConfig),
			new LuaField("MatchAchievementConfig", get_MatchAchievementConfig, set_MatchAchievementConfig),
			new LuaField("BadgeSlotsConfig", get_BadgeSlotsConfig, set_BadgeSlotsConfig),
			new LuaField("GoodsComposeNewConfigData", get_GoodsComposeNewConfigData, set_GoodsComposeNewConfigData),
			new LuaField("SceneConfig", get_SceneConfig, set_SceneConfig),
			new LuaField("BodyInfoListConfig", get_BodyInfoListConfig, set_BodyInfoListConfig),
			new LuaField("RoleShapeConfig", get_RoleShapeConfig, set_RoleShapeConfig),
			new LuaField("FashionConfig", get_FashionConfig, set_FashionConfig),
			new LuaField("FashionShopConfig", get_FashionShopConfig, set_FashionShopConfig),
			new LuaField("SpecialActionConfig", get_SpecialActionConfig, set_SpecialActionConfig),
			new LuaField("StealConfig", get_StealConfig, set_StealConfig),
			new LuaField("CurveRateConfig", get_CurveRateConfig, set_CurveRateConfig),
			new LuaField("DunkRateConfig", get_DunkRateConfig, set_DunkRateConfig),
			new LuaField("AIConfig", get_AIConfig, set_AIConfig),
			new LuaField("AttrReduceConfig", get_AttrReduceConfig, set_AttrReduceConfig),
			new LuaField("VipPrivilegeConfig", get_VipPrivilegeConfig, set_VipPrivilegeConfig),
			new LuaField("pushConfig", get_pushConfig, set_pushConfig),
			new LuaField("qualifyingConfig", get_qualifyingConfig, set_qualifyingConfig),
			new LuaField("qualifyingNewConfig", get_qualifyingNewConfig, set_qualifyingNewConfig),
			new LuaField("qualifyingNewerConfig", get_qualifyingNewerConfig, set_qualifyingNewerConfig),
			new LuaField("presentHpConfigData", get_presentHpConfigData, set_presentHpConfigData),
			new LuaField("LotteryConfig", get_LotteryConfig, set_LotteryConfig),
			new LuaField("starAttrConfig", get_starAttrConfig, set_starAttrConfig),
			new LuaField("qualityAttrCorConfig", get_qualityAttrCorConfig, set_qualityAttrCorConfig),
			new LuaField("skillUpConfig", get_skillUpConfig, set_skillUpConfig),
			new LuaField("RankConfig", get_RankConfig, set_RankConfig),
			new LuaField("signConfig", get_signConfig, set_signConfig),
			new LuaField("bullFightConfig", get_bullFightConfig, set_bullFightConfig),
			new LuaField("HedgingConfig", get_HedgingConfig, set_HedgingConfig),
			new LuaField("roleGiftConfig", get_roleGiftConfig, set_roleGiftConfig),
			new LuaField("shootGameConfig", get_shootGameConfig, set_shootGameConfig),
			new LuaField("NewComerSignConfig", get_NewComerSignConfig, set_NewComerSignConfig),
			new LuaField("FightingCapacityConfig", get_FightingCapacityConfig, set_FightingCapacityConfig),
			new LuaField("PotientialEffectConfig", get_PotientialEffectConfig, set_PotientialEffectConfig),
			new LuaField("DebugConfig", get_DebugConfig, set_DebugConfig),
			new LuaField("MapConfig", get_MapConfig, set_MapConfig),
			new LuaField("activityConfig", get_activityConfig, set_activityConfig),
			new LuaField("trialConfig", get_trialConfig, set_trialConfig),
			new LuaField("gameMatchConfig", get_gameMatchConfig, set_gameMatchConfig),
			new LuaField("shootSolutionManager", get_shootSolutionManager, set_shootSolutionManager),
			new LuaField("talentConfig", get_talentConfig, set_talentConfig),
			new LuaField("ladderConfig", get_ladderConfig, set_ladderConfig),
			new LuaField("matchSoundConfig", get_matchSoundConfig, set_matchSoundConfig),
			new LuaField("matchMsgConfig", get_matchMsgConfig, set_matchMsgConfig),
			new LuaField("MatchPointsConfig", get_MatchPointsConfig, set_MatchPointsConfig),
			new LuaField("loadConfigCnt", get_loadConfigCnt, set_loadConfigCnt),
			new LuaField("readConfigCnt", get_readConfigCnt, set_readConfigCnt),
			new LuaField("canLoadConfig", get_canLoadConfig, set_canLoadConfig),
			new LuaField("loadConfigFinish", get_loadConfigFinish, set_loadConfigFinish),
			new LuaField("showLoading", get_showLoading, set_showLoading),
			new LuaField("isNewPlayer", get_isNewPlayer, set_isNewPlayer),
			new LuaField("configPreLoadFinish", get_configPreLoadFinish, set_configPreLoadFinish),
			new LuaField("configCommonLoadFinish", get_configCommonLoadFinish, set_configCommonLoadFinish),
			new LuaField("_Time", get__Time, set__Time),
			new LuaField("serverIP", get_serverIP, set_serverIP),
			new LuaField("port", get_port, set_port),
			new LuaField("appPaused", get_appPaused, set_appPaused),
			new LuaField("Instance", get_Instance, null),
			new LuaField("mVDebug", get_mVDebug, null),
			new LuaField("mClient", get_mClient, null),
			new LuaField("mNetworkManager", get_mNetworkManager, null),
			new LuaField("mTime", get_mTime, set_mTime),
		};

		LuaScriptMgr.RegisterLib(L, "GameSystem", typeof(GameSystem), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameSystem(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameSystem obj = new GameSystem();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameSystem.New");
		}

		return 0;
	}

	static Type classType = typeof(GameSystem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MaxIntervalTime(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameSystem.MaxIntervalTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MaxIntervalCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameSystem.MaxIntervalCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LoseFocusTime(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameSystem.LoseFocusTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mEngineFramework(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mEngineFramework");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mEngineFramework on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mEngineFramework);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AnnouncementConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AnnouncementConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AnnouncementConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AnnouncementConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BadgeAttrConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeAttrConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeAttrConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BadgeAttrConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ConstStringConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ConstStringConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ConstStringConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ConstStringConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CommonConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CommonConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CommonConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.CommonConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerDataConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ServerDataConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ServerDataConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ServerDataConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AttrNameConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrNameConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrNameConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AttrNameConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RoleBaseConfigData2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleBaseConfigData2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleBaseConfigData2 on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RoleBaseConfigData2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AttrDataConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrDataConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrDataConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AttrDataConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ReboundAttrConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ReboundAttrConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ReboundAttrConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ReboundAttrConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TeamLevelConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TeamLevelConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TeamLevelConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TeamLevelConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RoleLevelConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleLevelConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleLevelConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RoleLevelConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NPCConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NPCConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CareerConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CareerConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CareerConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.CareerConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SkillConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SkillConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SkillConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.SkillConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GoodsConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoodsConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoodsConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.GoodsConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StoreGoodsConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StoreGoodsConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StoreGoodsConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.StoreGoodsConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BaseDataBuyConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BaseDataBuyConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BaseDataBuyConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BaseDataBuyConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PVPPointConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PVPPointConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PVPPointConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PVPPointConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WinningStreakAwardConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WinningStreakAwardConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WinningStreakAwardConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.WinningStreakAwardConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TaskConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TaskConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TaskConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TaskConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AwardPackConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AwardPackConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AwardPackConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AwardPackConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ArticleStrengthConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ArticleStrengthConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ArticleStrengthConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ArticleStrengthConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PhRegainConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PhRegainConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PhRegainConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PhRegainConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PractiseConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PractiseConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PractiseConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PractiseConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PracticePveConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PracticePveConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PracticePveConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PracticePveConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PractiseStepConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PractiseStepConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PractiseStepConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PractiseStepConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GameModeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GameModeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GameModeConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.GameModeConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TrainingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TrainingConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TattooConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TattooConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TattooConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TattooConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EquipmentConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipmentConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipmentConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.EquipmentConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TourConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TourConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GuideConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GuideConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GuideConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.GuideConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FunctionConditionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FunctionConditionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FunctionConditionConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FunctionConditionConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MatchAchievementConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchAchievementConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchAchievementConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MatchAchievementConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BadgeSlotsConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeSlotsConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeSlotsConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BadgeSlotsConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GoodsComposeNewConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoodsComposeNewConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoodsComposeNewConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.GoodsComposeNewConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SceneConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SceneConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SceneConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.SceneConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BodyInfoListConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BodyInfoListConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BodyInfoListConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BodyInfoListConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RoleShapeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleShapeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleShapeConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RoleShapeConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FashionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FashionConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FashionShopConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionShopConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionShopConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FashionShopConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SpecialActionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SpecialActionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SpecialActionConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.SpecialActionConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StealConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StealConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StealConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.StealConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CurveRateConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurveRateConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurveRateConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.CurveRateConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DunkRateConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DunkRateConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DunkRateConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.DunkRateConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AIConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AIConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AIConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AIConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AttrReduceConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrReduceConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrReduceConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AttrReduceConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_VipPrivilegeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipPrivilegeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipPrivilegeConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.VipPrivilegeConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pushConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pushConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pushConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pushConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifyingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifyingConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifyingNewConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifyingNewConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifyingNewerConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewerConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewerConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifyingNewerConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_presentHpConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name presentHpConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index presentHpConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.presentHpConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LotteryConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LotteryConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LotteryConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LotteryConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_starAttrConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name starAttrConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index starAttrConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.starAttrConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualityAttrCorConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualityAttrCorConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualityAttrCorConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualityAttrCorConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skillUpConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillUpConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillUpConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skillUpConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RankConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RankConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RankConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RankConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_signConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.signConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bullFightConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullFightConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullFightConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.bullFightConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HedgingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HedgingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HedgingConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.HedgingConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roleGiftConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roleGiftConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roleGiftConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.roleGiftConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shootGameConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootGameConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootGameConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.shootGameConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NewComerSignConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewComerSignConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewComerSignConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NewComerSignConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_FightingCapacityConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FightingCapacityConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FightingCapacityConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.FightingCapacityConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PotientialEffectConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PotientialEffectConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PotientialEffectConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.PotientialEffectConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DebugConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DebugConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DebugConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.DebugConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MapConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MapConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MapConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MapConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activityConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activityConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activityConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.activityConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trialConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.trialConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameMatchConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameMatchConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameMatchConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.gameMatchConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shootSolutionManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootSolutionManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootSolutionManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.shootSolutionManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_talentConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talentConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talentConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.talentConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ladderConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ladderConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ladderConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.ladderConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matchSoundConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchSoundConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchSoundConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.matchSoundConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matchMsgConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchMsgConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchMsgConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.matchMsgConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MatchPointsConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchPointsConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchPointsConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MatchPointsConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadConfigCnt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadConfigCnt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadConfigCnt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.loadConfigCnt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_readConfigCnt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name readConfigCnt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index readConfigCnt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.readConfigCnt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canLoadConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canLoadConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canLoadConfig on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.canLoadConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loadConfigFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadConfigFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadConfigFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.loadConfigFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showLoading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showLoading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showLoading on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showLoading);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isNewPlayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isNewPlayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isNewPlayer on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isNewPlayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configPreLoadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configPreLoadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configPreLoadFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.configPreLoadFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configCommonLoadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configCommonLoadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configCommonLoadFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.configCommonLoadFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__Time(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameSystem._Time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_serverIP(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name serverIP");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index serverIP on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.serverIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name port");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index port on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.port);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_appPaused(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name appPaused");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index appPaused on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.appPaused);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, GameSystem.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mVDebug(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mVDebug");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mVDebug on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mVDebug);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mClient(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mClient");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mClient on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mClient);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mNetworkManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mNetworkManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mNetworkManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mNetworkManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mTime(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameSystem.mTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mEngineFramework(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mEngineFramework");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mEngineFramework on a nil value");
			}
		}

		obj.mEngineFramework = (EngineFramework)LuaScriptMgr.GetUnityObject(L, 3, typeof(EngineFramework));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AnnouncementConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AnnouncementConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AnnouncementConfigData on a nil value");
			}
		}

		obj.AnnouncementConfigData = (AnnouncementConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AnnouncementConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BadgeAttrConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeAttrConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeAttrConfigData on a nil value");
			}
		}

		obj.BadgeAttrConfigData = (BadgeAttrConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(BadgeAttrConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ConstStringConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ConstStringConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ConstStringConfigData on a nil value");
			}
		}

		obj.ConstStringConfigData = (ConstStringConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ConstStringConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CommonConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CommonConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CommonConfig on a nil value");
			}
		}

		obj.CommonConfig = (CommonConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(CommonConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ServerDataConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ServerDataConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ServerDataConfigData on a nil value");
			}
		}

		obj.ServerDataConfigData = (ServerDataConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ServerDataConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AttrNameConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrNameConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrNameConfigData on a nil value");
			}
		}

		obj.AttrNameConfigData = (AttrNameConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AttrNameConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RoleBaseConfigData2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleBaseConfigData2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleBaseConfigData2 on a nil value");
			}
		}

		obj.RoleBaseConfigData2 = (BaseDataConfig2)LuaScriptMgr.GetNetObject(L, 3, typeof(BaseDataConfig2));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AttrDataConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrDataConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrDataConfigData on a nil value");
			}
		}

		obj.AttrDataConfigData = (AttrDataConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AttrDataConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ReboundAttrConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ReboundAttrConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ReboundAttrConfigData on a nil value");
			}
		}

		obj.ReboundAttrConfigData = (ReboundAttrConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ReboundAttrConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TeamLevelConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TeamLevelConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TeamLevelConfigData on a nil value");
			}
		}

		obj.TeamLevelConfigData = (TeamLevelConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TeamLevelConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RoleLevelConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleLevelConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleLevelConfigData on a nil value");
			}
		}

		obj.RoleLevelConfigData = (RoleLevelConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(RoleLevelConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NPCConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCConfigData on a nil value");
			}
		}

		obj.NPCConfigData = (NPCDataConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(NPCDataConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CareerConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CareerConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CareerConfigData on a nil value");
			}
		}

		obj.CareerConfigData = (CareerConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(CareerConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SkillConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SkillConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SkillConfig on a nil value");
			}
		}

		obj.SkillConfig = (SkillConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GoodsConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoodsConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoodsConfigData on a nil value");
			}
		}

		obj.GoodsConfigData = (GoodsConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(GoodsConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_StoreGoodsConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StoreGoodsConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StoreGoodsConfigData on a nil value");
			}
		}

		obj.StoreGoodsConfigData = (StoreGoodsConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(StoreGoodsConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BaseDataBuyConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BaseDataBuyConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BaseDataBuyConfigData on a nil value");
			}
		}

		obj.BaseDataBuyConfigData = (BaseDataBuyConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(BaseDataBuyConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PVPPointConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PVPPointConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PVPPointConfig on a nil value");
			}
		}

		obj.PVPPointConfig = (PVPPointConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PVPPointConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_WinningStreakAwardConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name WinningStreakAwardConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index WinningStreakAwardConfig on a nil value");
			}
		}

		obj.WinningStreakAwardConfig = (WinningStreakAwardConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(WinningStreakAwardConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TaskConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TaskConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TaskConfigData on a nil value");
			}
		}

		obj.TaskConfigData = (TaskDataConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TaskDataConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AwardPackConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AwardPackConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AwardPackConfigData on a nil value");
			}
		}

		obj.AwardPackConfigData = (AwardPackDataConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AwardPackDataConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ArticleStrengthConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ArticleStrengthConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ArticleStrengthConfig on a nil value");
			}
		}

		obj.ArticleStrengthConfig = (ArticleStrengthConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ArticleStrengthConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PhRegainConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PhRegainConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PhRegainConfig on a nil value");
			}
		}

		obj.PhRegainConfig = (PhRegainConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PhRegainConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PractiseConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PractiseConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PractiseConfig on a nil value");
			}
		}

		obj.PractiseConfig = (PractiseConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PractiseConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PracticePveConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PracticePveConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PracticePveConfig on a nil value");
			}
		}

		obj.PracticePveConfig = (PracticePveConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PracticePveConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PractiseStepConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PractiseStepConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PractiseStepConfig on a nil value");
			}
		}

		obj.PractiseStepConfig = (PractiseStepConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PractiseStepConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GameModeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GameModeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GameModeConfig on a nil value");
			}
		}

		obj.GameModeConfig = (GameModeConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(GameModeConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TrainingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TrainingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TrainingConfig on a nil value");
			}
		}

		obj.TrainingConfig = (TrainingConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TrainingConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TattooConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TattooConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TattooConfig on a nil value");
			}
		}

		obj.TattooConfig = (TattooConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TattooConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EquipmentConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name EquipmentConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index EquipmentConfigData on a nil value");
			}
		}

		obj.EquipmentConfigData = (EquipmentConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(EquipmentConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TourConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TourConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TourConfig on a nil value");
			}
		}

		obj.TourConfig = (TourConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TourConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GuideConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GuideConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GuideConfig on a nil value");
			}
		}

		obj.GuideConfig = (GuideConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(GuideConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FunctionConditionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FunctionConditionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FunctionConditionConfig on a nil value");
			}
		}

		obj.FunctionConditionConfig = (FunctionConditionConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(FunctionConditionConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MatchAchievementConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchAchievementConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchAchievementConfig on a nil value");
			}
		}

		obj.MatchAchievementConfig = (MatchAchievementConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchAchievementConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BadgeSlotsConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BadgeSlotsConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BadgeSlotsConfig on a nil value");
			}
		}

		obj.BadgeSlotsConfig = (BadgeSlotConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(BadgeSlotConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GoodsComposeNewConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name GoodsComposeNewConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index GoodsComposeNewConfigData on a nil value");
			}
		}

		obj.GoodsComposeNewConfigData = (GoodsComposeNewConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(GoodsComposeNewConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SceneConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SceneConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SceneConfig on a nil value");
			}
		}

		obj.SceneConfig = (SceneConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(SceneConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BodyInfoListConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BodyInfoListConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BodyInfoListConfig on a nil value");
			}
		}

		obj.BodyInfoListConfig = (BodyInfoListConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(BodyInfoListConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RoleShapeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleShapeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleShapeConfig on a nil value");
			}
		}

		obj.RoleShapeConfig = (RoleShapeConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(RoleShapeConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FashionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionConfig on a nil value");
			}
		}

		obj.FashionConfig = (FashionConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(FashionConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FashionShopConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FashionShopConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FashionShopConfig on a nil value");
			}
		}

		obj.FashionShopConfig = (FashionShopConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(FashionShopConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SpecialActionConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name SpecialActionConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index SpecialActionConfig on a nil value");
			}
		}

		obj.SpecialActionConfig = (SpecialActionConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(SpecialActionConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_StealConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name StealConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index StealConfig on a nil value");
			}
		}

		obj.StealConfig = (StealConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(StealConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CurveRateConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CurveRateConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CurveRateConfig on a nil value");
			}
		}

		obj.CurveRateConfig = (CurveRateConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(CurveRateConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DunkRateConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DunkRateConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DunkRateConfig on a nil value");
			}
		}

		obj.DunkRateConfig = (DunkRateConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(DunkRateConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AIConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AIConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AIConfig on a nil value");
			}
		}

		obj.AIConfig = (AIConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AIConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AttrReduceConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrReduceConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrReduceConfig on a nil value");
			}
		}

		obj.AttrReduceConfig = (AttrReduceConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(AttrReduceConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_VipPrivilegeConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name VipPrivilegeConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index VipPrivilegeConfig on a nil value");
			}
		}

		obj.VipPrivilegeConfig = (VipPrivilegeConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(VipPrivilegeConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pushConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pushConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pushConfig on a nil value");
			}
		}

		obj.pushConfig = (PushConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PushConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifyingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingConfig on a nil value");
			}
		}

		obj.qualifyingConfig = (QualifyingConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(QualifyingConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifyingNewConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewConfig on a nil value");
			}
		}

		obj.qualifyingNewConfig = (QualifyingNewConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(QualifyingNewConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifyingNewerConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewerConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewerConfig on a nil value");
			}
		}

		obj.qualifyingNewerConfig = (QualifyingNewerConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(QualifyingNewerConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_presentHpConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name presentHpConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index presentHpConfigData on a nil value");
			}
		}

		obj.presentHpConfigData = (PresentHpConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PresentHpConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LotteryConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LotteryConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LotteryConfig on a nil value");
			}
		}

		obj.LotteryConfig = (LotteryConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(LotteryConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_starAttrConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name starAttrConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index starAttrConfig on a nil value");
			}
		}

		obj.starAttrConfig = (StarAttrConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(StarAttrConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualityAttrCorConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualityAttrCorConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualityAttrCorConfig on a nil value");
			}
		}

		obj.qualityAttrCorConfig = (QualityAttrCorConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(QualityAttrCorConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skillUpConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillUpConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillUpConfig on a nil value");
			}
		}

		obj.skillUpConfig = (SkillUpConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillUpConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RankConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RankConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RankConfig on a nil value");
			}
		}

		obj.RankConfig = (RankConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(RankConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_signConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signConfig on a nil value");
			}
		}

		obj.signConfig = (SignConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(SignConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bullFightConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullFightConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullFightConfig on a nil value");
			}
		}

		obj.bullFightConfig = (BullFightConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(BullFightConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HedgingConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HedgingConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HedgingConfig on a nil value");
			}
		}

		obj.HedgingConfig = (HedgingConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(HedgingConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roleGiftConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roleGiftConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roleGiftConfig on a nil value");
			}
		}

		obj.roleGiftConfig = (RoleGiftConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(RoleGiftConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shootGameConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootGameConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootGameConfig on a nil value");
			}
		}

		obj.shootGameConfig = (ShootGameConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ShootGameConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NewComerSignConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NewComerSignConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NewComerSignConfig on a nil value");
			}
		}

		obj.NewComerSignConfig = (NewComerSignConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(NewComerSignConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_FightingCapacityConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name FightingCapacityConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index FightingCapacityConfig on a nil value");
			}
		}

		obj.FightingCapacityConfig = (FightingCapacityConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(FightingCapacityConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PotientialEffectConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PotientialEffectConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PotientialEffectConfig on a nil value");
			}
		}

		obj.PotientialEffectConfig = (PotientialEffectConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(PotientialEffectConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DebugConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DebugConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DebugConfig on a nil value");
			}
		}

		obj.DebugConfig = (DebugConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(DebugConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MapConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MapConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MapConfig on a nil value");
			}
		}

		obj.MapConfig = (MapConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(MapConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_activityConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activityConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activityConfig on a nil value");
			}
		}

		obj.activityConfig = (ActivityConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(ActivityConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_trialConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trialConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trialConfig on a nil value");
			}
		}

		obj.trialConfig = (TrialConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TrialConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameMatchConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameMatchConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameMatchConfig on a nil value");
			}
		}

		obj.gameMatchConfig = (GameMatchConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatchConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shootSolutionManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootSolutionManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootSolutionManager on a nil value");
			}
		}

		obj.shootSolutionManager = (ShootSolutionManager)LuaScriptMgr.GetNetObject(L, 3, typeof(ShootSolutionManager));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_talentConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talentConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talentConfig on a nil value");
			}
		}

		obj.talentConfig = (TalentConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(TalentConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ladderConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ladderConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ladderConfig on a nil value");
			}
		}

		obj.ladderConfig = (LadderConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(LadderConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchSoundConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchSoundConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchSoundConfig on a nil value");
			}
		}

		obj.matchSoundConfig = (MatchSoundConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchSoundConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchMsgConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchMsgConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchMsgConfig on a nil value");
			}
		}

		obj.matchMsgConfig = (MatchMsgConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchMsgConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MatchPointsConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchPointsConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchPointsConfig on a nil value");
			}
		}

		obj.MatchPointsConfig = (MatchPointsConfig)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchPointsConfig));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_loadConfigCnt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadConfigCnt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadConfigCnt on a nil value");
			}
		}

		obj.loadConfigCnt = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_readConfigCnt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name readConfigCnt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index readConfigCnt on a nil value");
			}
		}

		obj.readConfigCnt = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_canLoadConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canLoadConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canLoadConfig on a nil value");
			}
		}

		obj.canLoadConfig = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_loadConfigFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadConfigFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadConfigFinish on a nil value");
			}
		}

		obj.loadConfigFinish = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showLoading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showLoading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showLoading on a nil value");
			}
		}

		obj.showLoading = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isNewPlayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isNewPlayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isNewPlayer on a nil value");
			}
		}

		obj.isNewPlayer = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configPreLoadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configPreLoadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configPreLoadFinish on a nil value");
			}
		}

		obj.configPreLoadFinish = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configCommonLoadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configCommonLoadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configCommonLoadFinish on a nil value");
			}
		}

		obj.configCommonLoadFinish = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__Time(IntPtr L)
	{
		GameSystem._Time = (double)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_serverIP(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name serverIP");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index serverIP on a nil value");
			}
		}

		obj.serverIP = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name port");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index port on a nil value");
			}
		}

		obj.port = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_appPaused(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameSystem obj = (GameSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name appPaused");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index appPaused on a nil value");
			}
		}

		obj.appPaused = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mTime(IntPtr L)
	{
		GameSystem.mTime = (long)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Start(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.Start();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PreLoadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.PreLoadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.LoadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseCommonConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ParseCommonConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ParseConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParsePreConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ParsePreConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseHallConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ParseHallConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ParseMatchConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ParseMatchConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.LateUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.FixedUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReceiveHeartbeatMsg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.ReceiveHeartbeatMsg();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendHeartbeatMsg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.SendHeartbeatMsg();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DebugDraw(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.DebugDraw();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Exit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameSystem obj = (GameSystem)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameSystem");
		obj.Exit();
		return 0;
	}
}

