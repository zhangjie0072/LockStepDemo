using System;
using LuaInterface;

public class fogs_proto_config_QualifyingNewerSeasonConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_QualifyingNewerSeasonConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("season", get_season, set_season),
			new LuaField("start_time", get_start_time, set_start_time),
			new LuaField("end_time", get_end_time, set_end_time),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.QualifyingNewerSeasonConfig", typeof(fogs.proto.config.QualifyingNewerSeasonConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_QualifyingNewerSeasonConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.QualifyingNewerSeasonConfig obj = new fogs.proto.config.QualifyingNewerSeasonConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.QualifyingNewerSeasonConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.QualifyingNewerSeasonConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_season(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name season");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index season on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.season);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_start_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name start_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index start_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.start_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_end_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name end_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index end_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.end_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_season(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name season");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index season on a nil value");
			}
		}

		obj.season = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_start_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name start_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index start_time on a nil value");
			}
		}

		obj.start_time = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_end_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerSeasonConfig obj = (fogs.proto.config.QualifyingNewerSeasonConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name end_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index end_time on a nil value");
			}
		}

		obj.end_time = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

