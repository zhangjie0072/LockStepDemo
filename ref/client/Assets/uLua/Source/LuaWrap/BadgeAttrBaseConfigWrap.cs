using System;
using System.Collections.Generic;
using LuaInterface;

public class BadgeAttrBaseConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateBadgeAttrBaseConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("addAttr", get_addAttr, set_addAttr),
			new LuaField("specAttr", get_specAttr, set_specAttr),
			new LuaField("level", get_level, set_level),
		};

		LuaScriptMgr.RegisterLib(L, "BadgeAttrBaseConfig", typeof(BadgeAttrBaseConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBadgeAttrBaseConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BadgeAttrBaseConfig obj = new BadgeAttrBaseConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BadgeAttrBaseConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BadgeAttrBaseConfig);

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
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

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
	static int get_addAttr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addAttr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addAttr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.addAttr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_specAttr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name specAttr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index specAttr on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.specAttr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

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
	static int set_addAttr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addAttr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addAttr on a nil value");
			}
		}

		obj.addAttr = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_specAttr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name specAttr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index specAttr on a nil value");
			}
		}

		obj.specAttr = (KeyValuePair<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(KeyValuePair<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrBaseConfig obj = (BadgeAttrBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		obj.level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

