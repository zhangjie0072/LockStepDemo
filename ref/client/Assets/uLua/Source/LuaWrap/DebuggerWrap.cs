using System;
using UnityEngine;
using LuaInterface;

public class DebuggerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("DrawSphere", DrawSphere),
			new LuaMethod("Reset", Reset),
			new LuaMethod("Log", Log),
			new LuaMethod("LogWarning", LogWarning),
			new LuaMethod("LogError", LogError),
			new LuaMethod("New", _CreateDebugger),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_bEnableAI", get_m_bEnableAI, set_m_bEnableAI),
			new LuaField("m_bEnableDebugMsg", get_m_bEnableDebugMsg, set_m_bEnableDebugMsg),
			new LuaField("m_bEnableDefenderAction", get_m_bEnableDefenderAction, set_m_bEnableDefenderAction),
			new LuaField("m_bEnableTiming", get_m_bEnableTiming, set_m_bEnableTiming),
			new LuaField("m_steamer", get_m_steamer, set_m_steamer),
		};

		LuaScriptMgr.RegisterLib(L, "Debugger", typeof(Debugger), regs, fields, typeof(Singleton<Debugger>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDebugger(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Debugger obj = new Debugger();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Debugger.New");
		}

		return 0;
	}

	static Type classType = typeof(Debugger);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bEnableAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableAI on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bEnableAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bEnableDebugMsg(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableDebugMsg");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableDebugMsg on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bEnableDebugMsg);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bEnableDefenderAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableDefenderAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableDefenderAction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bEnableDefenderAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bEnableTiming(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableTiming");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableTiming on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bEnableTiming);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_steamer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_steamer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_steamer on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_steamer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bEnableAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableAI on a nil value");
			}
		}

		obj.m_bEnableAI = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bEnableDebugMsg(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableDebugMsg");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableDebugMsg on a nil value");
			}
		}

		obj.m_bEnableDebugMsg = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bEnableDefenderAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableDefenderAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableDefenderAction on a nil value");
			}
		}

		obj.m_bEnableDefenderAction = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bEnableTiming(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bEnableTiming");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bEnableTiming on a nil value");
			}
		}

		obj.m_bEnableTiming = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_steamer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Debugger obj = (Debugger)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_steamer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_steamer on a nil value");
			}
		}

		obj.m_steamer = (DebugStreamer)LuaScriptMgr.GetUnityObject(L, 3, typeof(DebugStreamer));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawSphere(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Debugger obj = (Debugger)LuaScriptMgr.GetNetObjectSelf(L, 1, "Debugger");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
		Color arg2 = LuaScriptMgr.GetColor(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		obj.DrawSphere(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Debugger obj = (Debugger)LuaScriptMgr.GetNetObjectSelf(L, 1, "Debugger");
		obj.Reset();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Log(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.Log(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogWarning(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.LogWarning(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogError(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.LogError(arg0,objs1);
		return 0;
	}
}

