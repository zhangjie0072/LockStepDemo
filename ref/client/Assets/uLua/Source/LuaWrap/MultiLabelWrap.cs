using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class MultiLabelWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetText", SetText),
			new LuaMethod("SetColor", SetColor),
			new LuaMethod("New", _CreateMultiLabel),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("IsConstString", get_IsConstString, set_IsConstString),
		};

		LuaScriptMgr.RegisterLib(L, "MultiLabel", typeof(MultiLabel), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMultiLabel(IntPtr L)
	{
		LuaDLL.luaL_error(L, "MultiLabel class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(MultiLabel);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsConstString(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MultiLabel obj = (MultiLabel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsConstString");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsConstString on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsConstString);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IsConstString(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MultiLabel obj = (MultiLabel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsConstString");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsConstString on a nil value");
			}
		}

		obj.IsConstString = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetText(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MultiLabel obj = (MultiLabel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MultiLabel");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.SetText(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetColor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MultiLabel obj = (MultiLabel)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MultiLabel");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		obj.SetColor(arg0);
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

