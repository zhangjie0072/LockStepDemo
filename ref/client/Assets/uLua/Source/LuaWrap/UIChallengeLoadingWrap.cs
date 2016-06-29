using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIChallengeLoadingWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnSceneLoaded", OnSceneLoaded),
			new LuaMethod("LoadFromMatch", LoadFromMatch),
			new LuaMethod("Refresh", Refresh),
			new LuaMethod("LoadScene", LoadScene),
			new LuaMethod("LoadCharacter", LoadCharacter),
			new LuaMethod("LoadUI", LoadUI),
			new LuaMethod("New", _CreateUIChallengeLoading),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("wait_seconds", get_wait_seconds, set_wait_seconds),
			new LuaField("wait_loading_seconds", get_wait_loading_seconds, set_wait_loading_seconds),
			new LuaField("single", get_single, set_single),
			new LuaField("scene_name", get_scene_name, set_scene_name),
			new LuaField("session_id", get_session_id, set_session_id),
			new LuaField("leagueType", get_leagueType, set_leagueType),
			new LuaField("matchType", get_matchType, set_matchType),
			new LuaField("matchTime", get_matchTime, set_matchTime),
			new LuaField("gameModeID", get_gameModeID, set_gameModeID),
			new LuaField("needPlayPlot", get_needPlayPlot, set_needPlayPlot),
			new LuaField("rival_list", get_rival_list, set_rival_list),
			new LuaField("rival_player_list", get_rival_player_list, set_rival_player_list),
			new LuaField("rival_name_list", get_rival_name_list, set_rival_name_list),
			new LuaField("rival_score_list", get_rival_score_list, set_rival_score_list),
			new LuaField("my_role_list", get_my_role_list, set_my_role_list),
			new LuaField("my_role_player_list", get_my_role_player_list, set_my_role_player_list),
			new LuaField("my_role_name_list", get_my_role_name_list, set_my_role_name_list),
			new LuaField("my_role_score_list", get_my_role_score_list, set_my_role_score_list),
			new LuaField("myName", get_myName, set_myName),
			new LuaField("rivalName", get_rivalName, set_rivalName),
			new LuaField("onComplete", get_onComplete, set_onComplete),
			new LuaField("m_curLoadingStep", get_m_curLoadingStep, set_m_curLoadingStep),
			new LuaField("pvp", get_pvp, set_pvp),
			new LuaField("disConnected", get_disConnected, set_disConnected),
			new LuaField("pvpPlusEndResp", get_pvpPlusEndResp, set_pvpPlusEndResp),
			new LuaField("pvpExEndResp", get_pvpExEndResp, set_pvpExEndResp),
			new LuaField("qualifyingNewerResp", get_qualifyingNewerResp, set_qualifyingNewerResp),
			new LuaField("pvpRegularEndResp", get_pvpRegularEndResp, set_pvpRegularEndResp),
			new LuaField("pvpQualifyingEndResp", get_pvpQualifyingEndResp, set_pvpQualifyingEndResp),
		};

		LuaScriptMgr.RegisterLib(L, "UIChallengeLoading", typeof(UIChallengeLoading), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIChallengeLoading(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIChallengeLoading class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIChallengeLoading);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wait_seconds(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wait_seconds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wait_seconds on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.wait_seconds);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wait_loading_seconds(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wait_loading_seconds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wait_loading_seconds on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.wait_loading_seconds);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_single(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name single");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index single on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.single);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scene_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene_name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scene_name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_session_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name session_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index session_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.session_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_leagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

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
	static int get_matchType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.matchType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_matchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.matchTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameModeID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameModeID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gameModeID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_needPlayPlot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needPlayPlot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needPlayPlot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.needPlayPlot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rival_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.rival_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rival_player_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_player_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_player_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.rival_player_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rival_name_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_name_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_name_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.rival_name_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rival_score_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_score_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_score_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.rival_score_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_my_role_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.my_role_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_my_role_player_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_player_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_player_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.my_role_player_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_my_role_name_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_name_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_name_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.my_role_name_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_my_role_score_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_score_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_score_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.my_role_score_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_myName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name myName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index myName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.myName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rivalName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rivalName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rivalName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rivalName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onComplete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_curLoadingStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curLoadingStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curLoadingStep on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_curLoadingStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pvp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_disConnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disConnected on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.disConnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpPlusEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpPlusEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpPlusEndResp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvpPlusEndResp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpExEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpExEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpExEndResp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvpExEndResp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifyingNewerResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewerResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewerResp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifyingNewerResp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpRegularEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpRegularEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpRegularEndResp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvpRegularEndResp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvpQualifyingEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpQualifyingEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpQualifyingEndResp on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.pvpQualifyingEndResp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_wait_seconds(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wait_seconds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wait_seconds on a nil value");
			}
		}

		obj.wait_seconds = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_wait_loading_seconds(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wait_loading_seconds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wait_loading_seconds on a nil value");
			}
		}

		obj.wait_loading_seconds = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_single(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name single");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index single on a nil value");
			}
		}

		obj.single = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scene_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene_name on a nil value");
			}
		}

		obj.scene_name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_session_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name session_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index session_id on a nil value");
			}
		}

		obj.session_id = (ulong)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_leagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

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

		obj.leagueType = (GameMatch.LeagueType)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.LeagueType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchType on a nil value");
			}
		}

		obj.matchType = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchTime on a nil value");
			}
		}

		obj.matchTime = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameModeID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameModeID on a nil value");
			}
		}

		obj.gameModeID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needPlayPlot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needPlayPlot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needPlayPlot on a nil value");
			}
		}

		obj.needPlayPlot = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rival_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_list on a nil value");
			}
		}

		obj.rival_list = (List<fogs.proto.msg.RoleInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.RoleInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rival_player_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_player_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_player_list on a nil value");
			}
		}

		obj.rival_player_list = (List<Player>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Player>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rival_name_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_name_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_name_list on a nil value");
			}
		}

		obj.rival_name_list = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rival_score_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rival_score_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rival_score_list on a nil value");
			}
		}

		obj.rival_score_list = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_my_role_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_list on a nil value");
			}
		}

		obj.my_role_list = (List<fogs.proto.msg.RoleInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.RoleInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_my_role_player_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_player_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_player_list on a nil value");
			}
		}

		obj.my_role_player_list = (List<Player>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Player>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_my_role_name_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_name_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_name_list on a nil value");
			}
		}

		obj.my_role_name_list = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_my_role_score_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name my_role_score_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index my_role_score_list on a nil value");
			}
		}

		obj.my_role_score_list = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_myName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name myName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index myName on a nil value");
			}
		}

		obj.myName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rivalName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rivalName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rivalName on a nil value");
			}
		}

		obj.rivalName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onComplete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onComplete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onComplete on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onComplete = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onComplete = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_curLoadingStep(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curLoadingStep");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curLoadingStep on a nil value");
			}
		}

		obj.m_curLoadingStep = (LoadingStep)LuaScriptMgr.GetNetObject(L, 3, typeof(LoadingStep));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp on a nil value");
			}
		}

		obj.pvp = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_disConnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disConnected on a nil value");
			}
		}

		obj.disConnected = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpPlusEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpPlusEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpPlusEndResp on a nil value");
			}
		}

		obj.pvpPlusEndResp = (fogs.proto.msg.PVPEndChallengePlusResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PVPEndChallengePlusResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpExEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpExEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpExEndResp on a nil value");
			}
		}

		obj.pvpExEndResp = (fogs.proto.msg.PVPEndChallengeExResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PVPEndChallengeExResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifyingNewerResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingNewerResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingNewerResp on a nil value");
			}
		}

		obj.qualifyingNewerResp = (fogs.proto.msg.PVPEndQualifyingNewerResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PVPEndQualifyingNewerResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpRegularEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpRegularEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpRegularEndResp on a nil value");
			}
		}

		obj.pvpRegularEndResp = (fogs.proto.msg.PVPEndRegularResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PVPEndRegularResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvpQualifyingEndResp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvpQualifyingEndResp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvpQualifyingEndResp on a nil value");
			}
		}

		obj.pvpQualifyingEndResp = (fogs.proto.msg.PVPEndQualifyingResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PVPEndQualifyingResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSceneLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		obj.OnSceneLoaded();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadFromMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch));
		obj.LoadFromMatch(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Refresh(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadScene(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		obj.LoadScene();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadCharacter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		PlayerManager arg0 = (PlayerManager)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerManager));
		GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch));
		obj.LoadCharacter(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadUI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIChallengeLoading obj = (UIChallengeLoading)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIChallengeLoading");
		GameMatch arg0 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
		obj.LoadUI(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

