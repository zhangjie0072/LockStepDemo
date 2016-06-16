using System;
using System.Collections.Generic;
using LuaInterface;

public class LadderRewardWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateLadderReward),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("win_times", get_win_times, set_win_times),
			new LuaField("rewards", get_rewards, set_rewards),
			new LuaField("extra_score", get_extra_score, set_extra_score),
		};

		LuaScriptMgr.RegisterLib(L, "LadderReward", typeof(LadderReward), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLadderReward(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LadderReward obj = new LadderReward();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LadderReward.New");
		}

		return 0;
	}

	static Type classType = typeof(LadderReward);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

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
	static int get_rewards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rewards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rewards on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.rewards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_extra_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extra_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extra_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.extra_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

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
	static int set_rewards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rewards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rewards on a nil value");
			}
		}

		obj.rewards = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_extra_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderReward obj = (LadderReward)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extra_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extra_score on a nil value");
			}
		}

		obj.extra_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

