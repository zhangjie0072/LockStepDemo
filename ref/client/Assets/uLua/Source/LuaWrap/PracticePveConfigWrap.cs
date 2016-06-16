using System;
using System.Collections.Generic;
using LuaInterface;

public class PracticePveConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreatePracticePveConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
			new LuaField("Count", get_Count, null),
		};

		LuaScriptMgr.RegisterLib(L, "PracticePveConfig", typeof(PracticePveConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePracticePveConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PracticePveConfig obj = new PracticePveConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PracticePveConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(PracticePveConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveConfig obj = (PracticePveConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveConfig obj = (PracticePveConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Count on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Count);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveConfig obj = (PracticePveConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		obj.configs = (Dictionary<uint,PracticePveData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,PracticePveData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PracticePveConfig obj = (PracticePveConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PracticePveConfig");
		Dictionary<uint,PracticePveData>.ValueCollection.Enumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PracticePveConfig obj = (PracticePveConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PracticePveConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		PracticePveData o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PracticePveConfig obj = (PracticePveConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PracticePveConfig");
		obj.ReadConfig();
		return 0;
	}
}

