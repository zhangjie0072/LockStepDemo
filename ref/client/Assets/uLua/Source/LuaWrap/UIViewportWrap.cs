using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIViewportWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateUIViewport),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sourceCamera", get_sourceCamera, set_sourceCamera),
			new LuaField("topLeft", get_topLeft, set_topLeft),
			new LuaField("bottomRight", get_bottomRight, set_bottomRight),
			new LuaField("fullSize", get_fullSize, set_fullSize),
		};

		LuaScriptMgr.RegisterLib(L, "UIViewport", typeof(UIViewport), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIViewport(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIViewport class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIViewport);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sourceCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sourceCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sourceCamera on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sourceCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_topLeft(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name topLeft");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index topLeft on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.topLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bottomRight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bottomRight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bottomRight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bottomRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fullSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.fullSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sourceCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sourceCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sourceCamera on a nil value");
			}
		}

		obj.sourceCamera = (Camera)LuaScriptMgr.GetUnityObject(L, 3, typeof(Camera));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_topLeft(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name topLeft");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index topLeft on a nil value");
			}
		}

		obj.topLeft = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bottomRight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bottomRight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bottomRight on a nil value");
			}
		}

		obj.bottomRight = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fullSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIViewport obj = (UIViewport)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fullSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fullSize on a nil value");
			}
		}

		obj.fullSize = (float)LuaScriptMgr.GetNumber(L, 3);
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

