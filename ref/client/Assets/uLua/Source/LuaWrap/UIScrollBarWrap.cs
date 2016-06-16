using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIScrollBarWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ForceUpdate", ForceUpdate),
			new LuaMethod("New", _CreateUIScrollBar),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("barSize", get_barSize, set_barSize),
		};

		LuaScriptMgr.RegisterLib(L, "UIScrollBar", typeof(UIScrollBar), regs, fields, typeof(UISlider));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIScrollBar(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIScrollBar class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIScrollBar);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_barSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIScrollBar obj = (UIScrollBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name barSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index barSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.barSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_barSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIScrollBar obj = (UIScrollBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name barSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index barSize on a nil value");
			}
		}

		obj.barSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIScrollBar obj = (UIScrollBar)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIScrollBar");
		obj.ForceUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

