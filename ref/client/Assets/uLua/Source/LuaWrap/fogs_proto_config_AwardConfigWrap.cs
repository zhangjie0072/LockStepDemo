using System;
using LuaInterface;

public class fogs_proto_config_AwardConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_AwardConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("award_id", get_award_id, set_award_id),
			new LuaField("award_value", get_award_value, set_award_value),
			new LuaField("award_max_value", get_award_max_value, set_award_max_value),
			new LuaField("award_prob", get_award_prob, set_award_prob),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.AwardConfig", typeof(fogs.proto.config.AwardConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_AwardConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.AwardConfig obj = new fogs.proto.config.AwardConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.AwardConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.AwardConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_max_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_max_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_max_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_max_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_prob(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_prob");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_prob on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_prob);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		obj.award_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_value on a nil value");
			}
		}

		obj.award_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_max_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_max_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_max_value on a nil value");
			}
		}

		obj.award_max_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_prob(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.AwardConfig obj = (fogs.proto.config.AwardConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_prob");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_prob on a nil value");
			}
		}

		obj.award_prob = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

