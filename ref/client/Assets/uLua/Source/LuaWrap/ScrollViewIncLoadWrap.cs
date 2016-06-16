using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ScrollViewIncLoadWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Refresh", Refresh),
			new LuaMethod("New", _CreateScrollViewIncLoad),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("maxLineNum", get_maxLineNum, set_maxLineNum),
			new LuaField("preloadLineNum", get_preloadLineNum, set_preloadLineNum),
			new LuaField("onAcquireItem", get_onAcquireItem, set_onAcquireItem),
			new LuaField("onDestroyItem", get_onDestroyItem, set_onDestroyItem),
			new LuaField("holdItem", get_holdItem, set_holdItem),
		};

		LuaScriptMgr.RegisterLib(L, "ScrollViewIncLoad", typeof(ScrollViewIncLoad), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateScrollViewIncLoad(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ScrollViewIncLoad class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(ScrollViewIncLoad);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxLineNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLineNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLineNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxLineNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preloadLineNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preloadLineNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preloadLineNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.preloadLineNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAcquireItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAcquireItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAcquireItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onAcquireItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDestroyItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDestroyItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDestroyItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDestroyItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_holdItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name holdItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index holdItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.holdItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxLineNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLineNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLineNum on a nil value");
			}
		}

		obj.maxLineNum = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_preloadLineNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preloadLineNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preloadLineNum on a nil value");
			}
		}

		obj.preloadLineNum = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onAcquireItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAcquireItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAcquireItem on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onAcquireItem = (Func<int,Transform,GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Func<int,Transform,GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onAcquireItem = (param0, param1) =>
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
	static int set_onDestroyItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDestroyItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDestroyItem on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDestroyItem = (Action<int,GameObject>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<int,GameObject>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDestroyItem = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_holdItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name holdItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index holdItem on a nil value");
			}
		}

		obj.holdItem = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollViewIncLoad obj = (ScrollViewIncLoad)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ScrollViewIncLoad");
		obj.Refresh();
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

