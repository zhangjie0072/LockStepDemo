using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AsyncItemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetItemsCounter", SetItemsCounter),
			new LuaMethod("New", _CreateAsyncItem),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("OnCreateItem", get_OnCreateItem, set_OnCreateItem),
		};

		LuaScriptMgr.RegisterLib(L, "AsyncItem", typeof(AsyncItem), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAsyncItem(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AsyncItem class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(AsyncItem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnCreateItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AsyncItem obj = (AsyncItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnCreateItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnCreateItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.OnCreateItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnCreateItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AsyncItem obj = (AsyncItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name OnCreateItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index OnCreateItem on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.OnCreateItem = (Func<int,Transform,GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Func<int,Transform,GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.OnCreateItem = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (GameObject)objs[0];
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetItemsCounter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AsyncItem obj = (AsyncItem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AsyncItem");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.SetItemsCounter(arg0);
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

