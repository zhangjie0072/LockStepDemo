using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class GameMatchWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AddCount24Listener", AddCount24Listener),
			new LuaMethod("RemoveAllCount24Listener", RemoveAllCount24Listener),
			new LuaMethod("LeagueTypeToString", LeagueTypeToString),
			new LuaMethod("LeagueTypeToSpriteName", LeagueTypeToSpriteName),
			new LuaMethod("HandleBroadcast", HandleBroadcast),
			new LuaMethod("CreateUI", CreateUI),
			new LuaMethod("GetMatchType", GetMatchType),
			new LuaMethod("OnDestroy", OnDestroy),
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("Build", Build),
			new LuaMethod("OnSceneComplete", OnSceneComplete),
			new LuaMethod("HandleGameBegin", HandleGameBegin),
			new LuaMethod("OnGameBegin", OnGameBegin),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("ViewUpdate", ViewUpdate),
			new LuaMethod("GameLateUpdate", GameLateUpdate),
			new LuaMethod("ViewLateUpdate", ViewLateUpdate),
			new LuaMethod("ShowMatchTip", ShowMatchTip),
			new LuaMethod("ShowMatchTipScoreDiff", ShowMatchTipScoreDiff),
			new LuaMethod("ShowMatchPromptTip", ShowMatchPromptTip),
			new LuaMethod("ShowPlayerTip", ShowPlayerTip),
			new LuaMethod("ShowBasketTip", ShowBasketTip),
			new LuaMethod("ShowTips", ShowTips),
			new LuaMethod("TimmingOnStarting", TimmingOnStarting),
			new LuaMethod("DoGoalState", DoGoalState),
			new LuaMethod("EnableGoalState", EnableGoalState),
			new LuaMethod("EnableNPCGoalSound", EnableNPCGoalSound),
			new LuaMethod("EnableCounter24", EnableCounter24),
			new LuaMethod("EnableSwitchRole", EnableSwitchRole),
			new LuaMethod("EnableCheckBall", EnableCheckBall),
			new LuaMethod("EnableEnhanceAttr", EnableEnhanceAttr),
			new LuaMethod("EnableTakeOver", EnableTakeOver),
			new LuaMethod("EnablePlayerTips", EnablePlayerTips),
			new LuaMethod("EnableMatchTips", EnableMatchTips),
			new LuaMethod("EnableMatchAchievement", EnableMatchAchievement),
			new LuaMethod("EnableSwitchDefenseTarget", EnableSwitchDefenseTarget),
			new LuaMethod("ResetPlayerPos", ResetPlayerPos),
			new LuaMethod("ConstrainMovementOnBegin", ConstrainMovementOnBegin),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("IsFinalTime", IsFinalTime),
			new LuaMethod("AdjustShootRate", AdjustShootRate),
			new LuaMethod("AdjustBlockRate", AdjustBlockRate),
			new LuaMethod("AdjustCrossRate", AdjustCrossRate),
			new LuaMethod("GetShootSolution", GetShootSolution),
			new LuaMethod("GetScore", GetScore),
			new LuaMethod("IsDraw", IsDraw),
			new LuaMethod("GetWinTeam", GetWinTeam),
			new LuaMethod("GetLoseTeam", GetLoseTeam),
			new LuaMethod("GenerateIn3PTPosition", GenerateIn3PTPosition),
			new LuaMethod("ShowCanShoot", ShowCanShoot),
			new LuaMethod("HideCanShoot", HideCanShoot),
			new LuaMethod("ShowOpportunity", ShowOpportunity),
			new LuaMethod("HideOpportunity", HideOpportunity),
			new LuaMethod("ShowAnimTip", ShowAnimTip),
			new LuaMethod("HideAnimTip", HideAnimTip),
			new LuaMethod("ShowCombo", ShowCombo),
			new LuaMethod("HideCombo", HideCombo),
			new LuaMethod("ShowComboBonus", ShowComboBonus),
			new LuaMethod("HideComboBonus", HideComboBonus),
			new LuaMethod("HideAllTip", HideAllTip),
			new LuaMethod("CreateTeamMember", CreateTeamMember),
			new LuaMethod("AssumeDefenseTarget", AssumeDefenseTarget),
			new LuaMethod("GetAttrReduceScale", GetAttrReduceScale),
			new LuaMethod("InitBallHolder", InitBallHolder),
			new LuaMethod("EnhanceAttr", EnhanceAttr),
			new LuaMethod("ProcessTurn", ProcessTurn),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_homeTeam", get_m_homeTeam, set_m_homeTeam),
			new LuaField("m_awayTeam", get_m_awayTeam, set_m_awayTeam),
			new LuaField("m_bOverTime", get_m_bOverTime, set_m_bOverTime),
			new LuaField("gameMatchTime", get_gameMatchTime, set_gameMatchTime),
			new LuaField("m_gameMatchCountStop", get_m_gameMatchCountStop, set_m_gameMatchCountStop),
			new LuaField("m_gameMathCountEnable", get_m_gameMathCountEnable, set_m_gameMathCountEnable),
			new LuaField("MAX_COUNT24_TIME", get_MAX_COUNT24_TIME, set_MAX_COUNT24_TIME),
			new LuaField("m_count24TimeStop", get_m_count24TimeStop, set_m_count24TimeStop),
			new LuaField("reboundHelper", get_reboundHelper, set_reboundHelper),
			new LuaField("m_uiMatch", get_m_uiMatch, set_m_uiMatch),
			new LuaField("m_uiInGamePanel", get_m_uiInGamePanel, set_m_uiInGamePanel),
			new LuaField("m_uiController", get_m_uiController, set_m_uiController),
			new LuaField("m_bgSoundPlayer", get_m_bgSoundPlayer, set_m_bgSoundPlayer),
			new LuaField("m_bTimeUp", get_m_bTimeUp, set_m_bTimeUp),
			new LuaField("m_b24TimeUp", get_m_b24TimeUp, set_m_b24TimeUp),
			new LuaField("m_config", get_m_config, set_m_config),
			new LuaField("attrReduceItems", get_attrReduceItems, set_attrReduceItems),
			new LuaField("neverWin", get_neverWin, set_neverWin),
			new LuaField("m_auxiliaries", get_m_auxiliaries, set_m_auxiliaries),
			new LuaField("m_preloadCache", get_m_preloadCache, set_m_preloadCache),
			new LuaField("m_needTipOff", get_m_needTipOff, set_m_needTipOff),
			new LuaField("m_bLoadingComplete", get_m_bLoadingComplete, null),
			new LuaField("leagueType", get_leagueType, null),
			new LuaField("m_gameMathCountTimer", get_m_gameMathCountTimer, null),
			new LuaField("m_count24Time", get_m_count24Time, set_m_count24Time),
			new LuaField("m_offenseTeam", get_m_offenseTeam, set_m_offenseTeam),
			new LuaField("m_defenseTeam", get_m_defenseTeam, set_m_defenseTeam),
			new LuaField("m_strongTeam", get_m_strongTeam, null),
			new LuaField("m_weakTeam", get_m_weakTeam, null),
			new LuaField("m_strongTeamScore", get_m_strongTeamScore, null),
			new LuaField("m_weakTeamScore", get_m_weakTeamScore, null),
			new LuaField("mCurScene", get_mCurScene, null),
			new LuaField("mainRole", get_mainRole, set_mainRole),
			new LuaField("m_ruler", get_m_ruler, null),
			new LuaField("m_cam", get_m_cam, null),
			new LuaField("m_camFollowPath", get_m_camFollowPath, null),
			new LuaField("m_origCameraForward", get_m_origCameraForward, null),
			new LuaField("m_awayScore", get_m_awayScore, set_m_awayScore),
			new LuaField("m_homeScore", get_m_homeScore, set_m_homeScore),
			new LuaField("m_stateMachine", get_m_stateMachine, null),
			new LuaField("gameMode", get_gameMode, null),
			new LuaField("level", get_level, null),
			new LuaField("m_context", get_m_context, null),
			new LuaField("turnManager", get_turnManager, null),
			new LuaField("curGameUpdateID", get_curGameUpdateID, null),
		};

		LuaScriptMgr.RegisterLib(L, "GameMatch", typeof(GameMatch), regs, fields, typeof(object));
	}

	static Type classType = typeof(GameMatch);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_homeTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_homeTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_homeTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_homeTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_awayTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_awayTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_awayTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_awayTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bOverTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bOverTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bOverTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bOverTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameMatchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameMatchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameMatchTime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.gameMatchTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gameMatchCountStop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMatchCountStop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMatchCountStop on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_gameMatchCountStop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gameMathCountEnable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMathCountEnable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMathCountEnable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_gameMathCountEnable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MAX_COUNT24_TIME(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MAX_COUNT24_TIME");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MAX_COUNT24_TIME on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.MAX_COUNT24_TIME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_count24TimeStop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_count24TimeStop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_count24TimeStop on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_count24TimeStop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reboundHelper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reboundHelper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reboundHelper on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.reboundHelper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiMatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiMatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiMatch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiMatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiInGamePanel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiInGamePanel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiInGamePanel on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiInGamePanel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiController(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiController");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiController on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiController);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bgSoundPlayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bgSoundPlayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bgSoundPlayer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_bgSoundPlayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bTimeUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bTimeUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bTimeUp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bTimeUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_b24TimeUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_b24TimeUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_b24TimeUp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_b24TimeUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_config(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_config");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_config on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_config);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrReduceItems(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrReduceItems");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrReduceItems on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attrReduceItems);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_neverWin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name neverWin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index neverWin on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.neverWin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_auxiliaries(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_auxiliaries");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_auxiliaries on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_auxiliaries);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_preloadCache(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_preloadCache");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_preloadCache on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_preloadCache);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_needTipOff(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_needTipOff");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_needTipOff on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_needTipOff);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bLoadingComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bLoadingComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bLoadingComplete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bLoadingComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_leagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name leagueType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index leagueType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.leagueType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gameMathCountTimer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMathCountTimer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMathCountTimer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_gameMathCountTimer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_count24Time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_count24Time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_count24Time on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_count24Time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_offenseTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_offenseTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_offenseTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_offenseTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_defenseTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defenseTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defenseTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_defenseTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_strongTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_strongTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_strongTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_strongTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_weakTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_weakTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_weakTeam on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_weakTeam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_strongTeamScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_strongTeamScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_strongTeamScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_strongTeamScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_weakTeamScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_weakTeamScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_weakTeamScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_weakTeamScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mCurScene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCurScene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCurScene on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mCurScene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mainRole(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainRole");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainRole on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mainRole);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ruler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ruler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ruler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_ruler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_cam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_cam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_cam on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_cam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_camFollowPath(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_camFollowPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_camFollowPath on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_camFollowPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_origCameraForward(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_origCameraForward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_origCameraForward on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_origCameraForward);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_awayScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_awayScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_awayScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_awayScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_homeScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_homeScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_homeScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_homeScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_stateMachine(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_stateMachine");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_stateMachine on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_stateMachine);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameMode on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.gameMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_context(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_context");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_context on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_context);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_turnManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name turnManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index turnManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.turnManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curGameUpdateID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curGameUpdateID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curGameUpdateID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.curGameUpdateID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_homeTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_homeTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_homeTeam on a nil value");
			}
		}

		obj.m_homeTeam = (Team)LuaScriptMgr.GetNetObject(L, 3, typeof(Team));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_awayTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_awayTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_awayTeam on a nil value");
			}
		}

		obj.m_awayTeam = (Team)LuaScriptMgr.GetNetObject(L, 3, typeof(Team));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bOverTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bOverTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bOverTime on a nil value");
			}
		}

		obj.m_bOverTime = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameMatchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameMatchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameMatchTime on a nil value");
			}
		}

		obj.gameMatchTime = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_gameMatchCountStop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMatchCountStop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMatchCountStop on a nil value");
			}
		}

		obj.m_gameMatchCountStop = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_gameMathCountEnable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMathCountEnable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMathCountEnable on a nil value");
			}
		}

		obj.m_gameMathCountEnable = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MAX_COUNT24_TIME(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MAX_COUNT24_TIME");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MAX_COUNT24_TIME on a nil value");
			}
		}

		obj.MAX_COUNT24_TIME = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_count24TimeStop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_count24TimeStop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_count24TimeStop on a nil value");
			}
		}

		obj.m_count24TimeStop = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reboundHelper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reboundHelper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reboundHelper on a nil value");
			}
		}

		obj.reboundHelper = (ReboundHelper)LuaScriptMgr.GetNetObject(L, 3, typeof(ReboundHelper));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiMatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiMatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiMatch on a nil value");
			}
		}

		obj.m_uiMatch = (UIMatch)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIMatch));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiInGamePanel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiInGamePanel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiInGamePanel on a nil value");
			}
		}

		obj.m_uiInGamePanel = (UIPanel)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIPanel));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiController(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiController");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiController on a nil value");
			}
		}

		obj.m_uiController = (UController)LuaScriptMgr.GetUnityObject(L, 3, typeof(UController));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bgSoundPlayer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bgSoundPlayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bgSoundPlayer on a nil value");
			}
		}

		obj.m_bgSoundPlayer = (BackgroundSoundPlayer)LuaScriptMgr.GetNetObject(L, 3, typeof(BackgroundSoundPlayer));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bTimeUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bTimeUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bTimeUp on a nil value");
			}
		}

		obj.m_bTimeUp = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_b24TimeUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_b24TimeUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_b24TimeUp on a nil value");
			}
		}

		obj.m_b24TimeUp = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_config(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_config");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_config on a nil value");
			}
		}

		obj.m_config = (GameMatch.Config)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Config));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrReduceItems(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrReduceItems");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrReduceItems on a nil value");
			}
		}

		obj.attrReduceItems = (Dictionary<string,AttrReduceConfig.AttrReduceItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,AttrReduceConfig.AttrReduceItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_neverWin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name neverWin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index neverWin on a nil value");
			}
		}

		obj.neverWin = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_auxiliaries(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_auxiliaries");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_auxiliaries on a nil value");
			}
		}

		obj.m_auxiliaries = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_preloadCache(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_preloadCache");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_preloadCache on a nil value");
			}
		}

		obj.m_preloadCache = (Dictionary<GameObject,GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<GameObject,GameObject>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_needTipOff(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_needTipOff");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_needTipOff on a nil value");
			}
		}

		obj.m_needTipOff = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_count24Time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_count24Time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_count24Time on a nil value");
			}
		}

		obj.m_count24Time = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_offenseTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_offenseTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_offenseTeam on a nil value");
			}
		}

		obj.m_offenseTeam = (Team)LuaScriptMgr.GetNetObject(L, 3, typeof(Team));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_defenseTeam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defenseTeam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defenseTeam on a nil value");
			}
		}

		obj.m_defenseTeam = (Team)LuaScriptMgr.GetNetObject(L, 3, typeof(Team));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mainRole(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainRole");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainRole on a nil value");
			}
		}

		obj.mainRole = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_awayScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_awayScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_awayScore on a nil value");
			}
		}

		obj.m_awayScore = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_homeScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch obj = (GameMatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_homeScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_homeScore on a nil value");
			}
		}

		obj.m_homeScore = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCount24Listener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		GameMatch.Count24Listener arg0 = (GameMatch.Count24Listener)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch.Count24Listener));
		obj.AddCount24Listener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAllCount24Listener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.RemoveAllCount24Listener();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LeagueTypeToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string o = obj.LeagueTypeToString();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LeagueTypeToSpriteName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string o = obj.LeagueTypeToSpriteName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleBroadcast(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 1, typeof(Pack));
		GameMatch.HandleBroadcast(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.CreateUI();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMatchType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		GameMatch.Type o = obj.GetMatchType();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDestroy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.OnDestroy();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		GameMatch.Config o = obj.GetConfig();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Build(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.Build();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSceneComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.OnSceneComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HandleGameBegin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.HandleGameBegin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGameBegin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		fogs.proto.msg.GameBeginResp arg0 = (fogs.proto.msg.GameBeginResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GameBeginResp));
		obj.OnGameBegin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.FixedUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.ViewUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameLateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameLateUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewLateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.ViewLateUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowMatchTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.ShowMatchTip(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowMatchTipScoreDiff(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.ShowMatchTipScoreDiff(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowMatchPromptTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		obj.ShowMatchPromptTip(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowPlayerTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.ShowPlayerTip(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowBasketTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.ShowBasketTip(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowTips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		Color arg2 = LuaScriptMgr.GetColor(L, 4);
		obj.ShowTips(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TimmingOnStarting(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.TimmingOnStarting();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DoGoalState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.DoGoalState();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableGoalState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableGoalState();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableNPCGoalSound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableNPCGoalSound();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableCounter24(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableCounter24();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableSwitchRole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableSwitchRole();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableCheckBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableCheckBall();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableEnhanceAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableEnhanceAttr();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableTakeOver(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableTakeOver();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnablePlayerTips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnablePlayerTips();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableMatchTips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableMatchTips();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableMatchAchievement(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableMatchAchievement();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableSwitchDefenseTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.EnableSwitchDefenseTarget();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetPlayerPos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.ResetPlayerPos();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConstrainMovementOnBegin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.ConstrainMovementOnBegin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsFinalTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		bool o = obj.IsFinalTime(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdjustShootRate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.PrecNumber arg1 = (IM.PrecNumber)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.PrecNumber));
		IM.PrecNumber o = obj.AdjustShootRate(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdjustBlockRate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Number o = obj.AdjustBlockRate(arg0,arg1,arg2);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AdjustCrossRate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Number o = obj.AdjustCrossRate(arg0,arg1,arg2);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShootSolution(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		UBasket arg0 = (UBasket)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasket));
		fogs.proto.msg.Area arg1 = (fogs.proto.msg.Area)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.Area));
		Player arg2 = (Player)LuaScriptMgr.GetNetObject(L, 4, typeof(Player));
		IM.PrecNumber arg3 = (IM.PrecNumber)LuaScriptMgr.GetNetObject(L, 5, typeof(IM.PrecNumber));
		ShootSolution.Type arg4 = (ShootSolution.Type)LuaScriptMgr.GetNetObject(L, 6, typeof(ShootSolution.Type));
		ShootSolution o = obj.GetShootSolution(arg0,arg1,arg2,arg3,arg4);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetScore(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetScore(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDraw(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		bool o = obj.IsDraw();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWinTeam(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Team o = obj.GetWinTeam();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLoseTeam(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Team o = obj.GetLoseTeam();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GenerateIn3PTPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		IM.Vector3 o = obj.GenerateIn3PTPosition();
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowCanShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.ShowCanShoot();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideCanShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.HideCanShoot();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowOpportunity(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.ShowOpportunity();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideOpportunity(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.HideOpportunity();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowAnimTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.ShowAnimTip(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideAnimTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.HideAnimTip(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowCombo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.ShowCombo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideCombo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.HideCombo();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowComboBonus(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.ShowComboBonus(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideComboBonus(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.HideComboBonus();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideAllTip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.HideAllTip();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateTeamMember(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		obj.CreateTeamMember(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AssumeDefenseTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.AssumeDefenseTarget();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrReduceScale(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		IM.Number o = obj.GetAttrReduceScale(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitBallHolder(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.InitBallHolder();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnhanceAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		obj.EnhanceAttr();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ProcessTurn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch obj = (GameMatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch");
		fogs.proto.msg.FrameInfo arg0 = (fogs.proto.msg.FrameInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FrameInfo));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		obj.ProcessTurn(arg0,arg1);
		return 0;
	}
}

