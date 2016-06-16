using System;
using System.Collections.Generic;
using LuaInterface;

public class PresentHpConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("IsGetPresentHP", IsGetPresentHP),
			new LuaMethod("GetHP", GetHP),
			new LuaMethod("GetTimeInterval", GetTimeInterval),
			new LuaMethod("New", _CreatePresentHpConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
		};

		LuaScriptMgr.RegisterLib(L, "PresentHpConfig", typeof(PresentHpConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePresentHpConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PresentHpConfig obj = new PresentHpConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PresentHpConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(PresentHpConfig);

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
		PresentHpConfig obj = (PresentHpConfig)o;

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
	static int set_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PresentHpConfig obj = (PresentHpConfig)o;

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

		obj.configs = (Dictionary<string,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PresentHpConfig obj = (PresentHpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PresentHpConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsGetPresentHP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PresentHpConfig obj = (PresentHpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PresentHpConfig");
		int o = obj.IsGetPresentHP();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PresentHpConfig obj = (PresentHpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PresentHpConfig");
		int o = obj.GetHP();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTimeInterval(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PresentHpConfig obj = (PresentHpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "PresentHpConfig");
		string o = obj.GetTimeInterval();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

