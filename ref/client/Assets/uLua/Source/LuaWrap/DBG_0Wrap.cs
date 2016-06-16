using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class DBG_0Wrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("set_y", set_y),
			new LuaMethod("New", _CreateDBG_0),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "DBG_0", typeof(DBG_0), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDBG_0(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DBG_0 class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(DBG_0);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_y(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		DBG_0 obj = (DBG_0)LuaScriptMgr.GetUnityObjectSelf(L, 1, "DBG_0");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.set_y(arg0);
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

