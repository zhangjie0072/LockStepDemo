using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIDragDropItemEventWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateUIDragDropItemEvent),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnDragDrop", get_OnDragDrop, set_OnDragDrop),
		};

		LuaScriptMgr.RegisterLib(L, "UIDragDropItemEvent", typeof(UIDragDropItemEvent), regs, fields, typeof(UIDragDropItem));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIDragDropItemEvent(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIDragDropItemEvent class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIDragDropItemEvent);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnDragDrop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItemEvent obj = (UIDragDropItemEvent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnDragDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnDragDrop on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.OnDragDrop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnDragDrop(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItemEvent obj = (UIDragDropItemEvent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnDragDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnDragDrop on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.OnDragDrop = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.OnDragDrop = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
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

