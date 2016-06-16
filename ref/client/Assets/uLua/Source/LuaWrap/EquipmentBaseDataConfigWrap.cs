using System;
using System.Collections.Generic;
using LuaInterface;

public class EquipmentBaseDataConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetAddnAttr", GetAddnAttr),
			new LuaMethod("New", _CreateEquipmentBaseDataConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("name", get_name, set_name),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("addn_attr", get_addn_attr, set_addn_attr),
			new LuaField("require_level", get_require_level, set_require_level),
			new LuaField("sacrifice_consume", get_sacrifice_consume, set_sacrifice_consume),
			new LuaField("sell_price", get_sell_price, set_sell_price),
		};

		LuaScriptMgr.RegisterLib(L, "EquipmentBaseDataConfig", typeof(EquipmentBaseDataConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateEquipmentBaseDataConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			EquipmentBaseDataConfig obj = new EquipmentBaseDataConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: EquipmentBaseDataConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(EquipmentBaseDataConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addn_attr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.addn_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_require_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name require_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index require_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.require_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sacrifice_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sacrifice_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sacrifice_consume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sacrifice_consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sell_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_price on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sell_price);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		obj.name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		obj.icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addn_attr on a nil value");
			}
		}

		obj.addn_attr = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_require_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name require_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index require_level on a nil value");
			}
		}

		obj.require_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sacrifice_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sacrifice_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sacrifice_consume on a nil value");
			}
		}

		obj.sacrifice_consume = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sell_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_price on a nil value");
			}
		}

		obj.sell_price = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAddnAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		EquipmentBaseDataConfig obj = (EquipmentBaseDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "EquipmentBaseDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetAddnAttr(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

