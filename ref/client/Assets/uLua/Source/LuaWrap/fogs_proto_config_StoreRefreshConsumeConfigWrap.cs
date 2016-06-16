using System;
using LuaInterface;

public class fogs_proto_config_StoreRefreshConsumeConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_StoreRefreshConsumeConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("store_id", get_store_id, set_store_id),
			new LuaField("times", get_times, set_times),
			new LuaField("consume_type", get_consume_type, set_consume_type),
			new LuaField("consume", get_consume, set_consume),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.StoreRefreshConsumeConfig", typeof(fogs.proto.config.StoreRefreshConsumeConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_StoreRefreshConsumeConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.StoreRefreshConsumeConfig obj = new fogs.proto.config.StoreRefreshConsumeConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.StoreRefreshConsumeConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.StoreRefreshConsumeConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_id on a nil value");
			}
		}

		obj.store_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		obj.times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_type on a nil value");
			}
		}

		obj.consume_type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.StoreRefreshConsumeConfig obj = (fogs.proto.config.StoreRefreshConsumeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume on a nil value");
			}
		}

		obj.consume = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

