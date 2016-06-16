using System;
using System.Collections.Generic;
using LuaInterface;

public class GoodsComposeBaseNewConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateGoodsComposeBaseNewConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("needIDs", get_needIDs, set_needIDs),
			new LuaField("packNum", get_packNum, set_packNum),
			new LuaField("costIDs", get_costIDs, set_costIDs),
		};

		LuaScriptMgr.RegisterLib(L, "GoodsComposeBaseNewConfig", typeof(GoodsComposeBaseNewConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGoodsComposeBaseNewConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GoodsComposeBaseNewConfig obj = new GoodsComposeBaseNewConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GoodsComposeBaseNewConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(GoodsComposeBaseNewConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_needIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needIDs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.needIDs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_packNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name packNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index packNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.packNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_costIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costIDs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.costIDs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name needIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index needIDs on a nil value");
			}
		}

		obj.needIDs = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_packNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name packNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index packNum on a nil value");
			}
		}

		obj.packNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_costIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsComposeBaseNewConfig obj = (GoodsComposeBaseNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costIDs on a nil value");
			}
		}

		obj.costIDs = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}
}

