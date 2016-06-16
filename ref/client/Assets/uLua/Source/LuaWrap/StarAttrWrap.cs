using System;
using System.Collections.Generic;
using LuaInterface;

public class StarAttrWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetAttrValue", GetAttrValue),
			new LuaMethod("New", _CreateStarAttr),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("star", get_star, set_star),
			new LuaField("consume", get_consume, set_consume),
			new LuaField("attrs", get_attrs, set_attrs),
		};

		LuaScriptMgr.RegisterLib(L, "StarAttr", typeof(StarAttr), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStarAttr(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			StarAttr obj = new StarAttr();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: StarAttr.New");
		}

		return 0;
	}

	static Type classType = typeof(StarAttr);

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
		StarAttr obj = (StarAttr)o;

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
	static int get_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.star);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

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

		LuaScriptMgr.PushObject(L, obj.consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

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

		obj.id = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		obj.star = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

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

		obj.consume = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StarAttr obj = (StarAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		obj.attrs = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		StarAttr obj = (StarAttr)LuaScriptMgr.GetNetObjectSelf(L, 1, "StarAttr");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetAttrValue(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

