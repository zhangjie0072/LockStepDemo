using System;
using LuaInterface;

public class fogs_proto_config_QualifyingNewerLeagueAwardsConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_QualifyingNewerLeagueAwardsConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("win_times", get_win_times, set_win_times),
			new LuaField("extra_score", get_extra_score, set_extra_score),
			new LuaField("rewards", get_rewards, set_rewards),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.QualifyingNewerLeagueAwardsConfig", typeof(fogs.proto.config.QualifyingNewerLeagueAwardsConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_QualifyingNewerLeagueAwardsConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = new fogs.proto.config.QualifyingNewerLeagueAwardsConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.QualifyingNewerLeagueAwardsConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.QualifyingNewerLeagueAwardsConfig);

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
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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
	static int get_extra_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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
	static int get_rewards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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

		LuaScriptMgr.Push(L, obj.rewards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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
	static int set_extra_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rewards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerLeagueAwardsConfig obj = (fogs.proto.config.QualifyingNewerLeagueAwardsConfig)o;

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

		obj.rewards = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

