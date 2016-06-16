using System;
using LuaInterface;

public class fogs_proto_msg_PvpPlusInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_PvpPlusInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("score", get_score, set_score),
			new LuaField("race_times", get_race_times, set_race_times),
			new LuaField("winning_streak", get_winning_streak, set_winning_streak),
			new LuaField("win_times", get_win_times, set_win_times),
			new LuaField("max_winning_streak", get_max_winning_streak, set_max_winning_streak),
			new LuaField("kill_goal_times", get_kill_goal_times, set_kill_goal_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.PvpPlusInfo", typeof(fogs.proto.msg.PvpPlusInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_PvpPlusInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.PvpPlusInfo obj = new fogs.proto.msg.PvpPlusInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.PvpPlusInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.PvpPlusInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_race_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name race_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index race_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.race_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winning_streak on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.winning_streak);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name win_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index win_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.win_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_winning_streak on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_winning_streak);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_kill_goal_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name kill_goal_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index kill_goal_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.kill_goal_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		obj.score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_race_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name race_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index race_times on a nil value");
			}
		}

		obj.race_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winning_streak on a nil value");
			}
		}

		obj.winning_streak = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name win_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index win_times on a nil value");
			}
		}

		obj.win_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_winning_streak on a nil value");
			}
		}

		obj.max_winning_streak = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_kill_goal_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpPlusInfo obj = (fogs.proto.msg.PvpPlusInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name kill_goal_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index kill_goal_times on a nil value");
			}
		}

		obj.kill_goal_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

