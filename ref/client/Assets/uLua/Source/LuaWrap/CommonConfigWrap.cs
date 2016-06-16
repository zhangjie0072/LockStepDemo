using System;
using System.Collections.Generic;
using LuaInterface;

public class CommonConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetString", GetString),
			new LuaMethod("GetUInt", GetUInt),
			new LuaMethod("GetFloat", GetFloat),
			new LuaMethod("GetNumber", GetNumber),
			new LuaMethod("New", _CreateCommonConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
		};

		LuaScriptMgr.RegisterLib(L, "CommonConfig", typeof(CommonConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCommonConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			CommonConfig obj = new CommonConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CommonConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(CommonConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configs(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, CommonConfig.configs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configs(IntPtr L)
	{
		CommonConfig.configs = (Dictionary<string,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string o = obj.GetString(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUInt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint o = obj.GetUInt(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		float o = obj.GetFloat(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNumber(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CommonConfig obj = (CommonConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "CommonConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		IM.Number o = obj.GetNumber(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}
}

