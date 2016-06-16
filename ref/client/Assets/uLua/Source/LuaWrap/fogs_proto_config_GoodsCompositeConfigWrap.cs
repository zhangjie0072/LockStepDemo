using System;
using LuaInterface;

public class fogs_proto_config_GoodsCompositeConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_GoodsCompositeConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("src_id", get_src_id, set_src_id),
			new LuaField("src_num", get_src_num, set_src_num),
			new LuaField("dest_id", get_dest_id, set_dest_id),
			new LuaField("dest_num", get_dest_num, set_dest_num),
			new LuaField("costing", get_costing, set_costing),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.GoodsCompositeConfig", typeof(fogs.proto.config.GoodsCompositeConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_GoodsCompositeConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.GoodsCompositeConfig obj = new fogs.proto.config.GoodsCompositeConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.GoodsCompositeConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.GoodsCompositeConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_src_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name src_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index src_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.src_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_src_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name src_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index src_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.src_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dest_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dest_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dest_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.dest_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dest_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dest_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dest_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.dest_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_costing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costing on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.costing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_src_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name src_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index src_id on a nil value");
			}
		}

		obj.src_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_src_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name src_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index src_num on a nil value");
			}
		}

		obj.src_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dest_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dest_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dest_id on a nil value");
			}
		}

		obj.dest_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dest_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dest_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dest_num on a nil value");
			}
		}

		obj.dest_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_costing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsCompositeConfig obj = (fogs.proto.config.GoodsCompositeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costing on a nil value");
			}
		}

		obj.costing = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

