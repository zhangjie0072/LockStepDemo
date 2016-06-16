using System;
using System.Collections.Generic;
using LuaInterface;

public class GameMatch_ConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateGameMatch_Config),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("type", get_type, set_type),
			new LuaField("leagueType", get_leagueType, set_leagueType),
			new LuaField("level", get_level, set_level),
			new LuaField("needPlayPlot", get_needPlayPlot, set_needPlayPlot),
			new LuaField("session_id", get_session_id, set_session_id),
			new LuaField("sceneId", get_sceneId, set_sceneId),
			new LuaField("gameModeID", get_gameModeID, set_gameModeID),
			new LuaField("extra_info", get_extra_info, set_extra_info),
			new LuaField("ip", get_ip, set_ip),
			new LuaField("port", get_port, set_port),
			new LuaField("NPCs", get_NPCs, set_NPCs),
			new LuaField("MainRole", get_MainRole, set_MainRole),
			new LuaField("RemotePlayers", get_RemotePlayers, set_RemotePlayers),
			new LuaField("MatchTime", get_MatchTime, set_MatchTime),
			new LuaField("Scene", get_Scene, set_Scene),
			new LuaField("OppoColorMulti", get_OppoColorMulti, set_OppoColorMulti),
		};

		LuaScriptMgr.RegisterLib(L, "GameMatch.Config", typeof(GameMatch.Config), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameMatch_Config(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameMatch.Config obj = new GameMatch.Config();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameMatch.Config.New");
		}

		return 0;
	}

	static Type classType = typeof(GameMatch.Config);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_leagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_needPlayPlot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_session_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_sceneId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sceneId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sceneId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sceneId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_extra_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extra_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extra_info on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.extra_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int get_NPCs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.NPCs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MainRole(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MainRole");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MainRole on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.MainRole);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RemotePlayers(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RemotePlayers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RemotePlayers on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RemotePlayers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MatchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchTime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.MatchTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Scene on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Scene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OppoColorMulti(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OppoColorMulti");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OppoColorMulti on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.OppoColorMulti);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_leagueType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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

		obj.level = (GameMatch.Level)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Level));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needPlayPlot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int set_session_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int set_sceneId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sceneId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sceneId on a nil value");
			}
		}

		obj.sceneId = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int set_extra_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extra_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extra_info on a nil value");
			}
		}

		obj.extra_info = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ip on a nil value");
			}
		}

		obj.ip = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

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
	static int set_NPCs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NPCs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NPCs on a nil value");
			}
		}

		obj.NPCs = (List<GameMatch.Config.TeamMember>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<GameMatch.Config.TeamMember>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MainRole(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MainRole");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MainRole on a nil value");
			}
		}

		obj.MainRole = (GameMatch.Config.TeamMember)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Config.TeamMember));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RemotePlayers(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RemotePlayers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RemotePlayers on a nil value");
			}
		}

		obj.RemotePlayers = (List<GameMatch.Config.TeamMember>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<GameMatch.Config.TeamMember>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_MatchTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MatchTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MatchTime on a nil value");
			}
		}

		obj.MatchTime = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Scene on a nil value");
			}
		}

		obj.Scene = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OppoColorMulti(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMatch.Config obj = (GameMatch.Config)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OppoColorMulti");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OppoColorMulti on a nil value");
			}
		}

		obj.OppoColorMulti = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}
}

