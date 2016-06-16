using System;
using LuaInterface;

public class PvpRegularInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreatePvpRegularInfo),
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
			new LuaField("mvp_times", get_mvp_times, set_mvp_times),
			new LuaField("score_king", get_score_king, set_score_king),
			new LuaField("rebound_king", get_rebound_king, set_rebound_king),
			new LuaField("block_king", get_block_king, set_block_king),
			new LuaField("assist_king", get_assist_king, set_assist_king),
			new LuaField("steal_king", get_steal_king, set_steal_king),
		};

		LuaScriptMgr.RegisterLib(L, "PvpRegularInfo", typeof(fogs.proto.msg.PvpRegularInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePvpRegularInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.PvpRegularInfo obj = new fogs.proto.msg.PvpRegularInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.PvpRegularInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.PvpRegularInfo);

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
	static int get_mvp_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mvp_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mvp_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mvp_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score_king on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.score_king);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rebound_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rebound_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rebound_king on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rebound_king);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_block_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name block_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index block_king on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.block_king);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assist_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assist_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assist_king on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.assist_king);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_steal_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name steal_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index steal_king on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.steal_king);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

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

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mvp_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mvp_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mvp_times on a nil value");
			}
		}

		obj.mvp_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score_king on a nil value");
			}
		}

		obj.score_king = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rebound_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rebound_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rebound_king on a nil value");
			}
		}

		obj.rebound_king = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_block_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name block_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index block_king on a nil value");
			}
		}

		obj.block_king = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assist_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assist_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assist_king on a nil value");
			}
		}

		obj.assist_king = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_steal_king(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PvpRegularInfo obj = (fogs.proto.msg.PvpRegularInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name steal_king");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index steal_king on a nil value");
			}
		}

		obj.steal_king = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

