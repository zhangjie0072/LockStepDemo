using System;
using System.Collections.Generic;
using LuaInterface;

public class TrialConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadTrialData", ReadTrialData),
			new LuaMethod("GetTrialListByDay", GetTrialListByDay),
			new LuaMethod("GetTrialDataByIndex", GetTrialDataByIndex),
			new LuaMethod("GetTotalScore", GetTotalScore),
			new LuaMethod("New", _CreateTrialConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("trialConfig", get_trialConfig, set_trialConfig),
		};

		LuaScriptMgr.RegisterLib(L, "TrialConfig", typeof(TrialConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTrialConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TrialConfig obj = new TrialConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TrialConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(TrialConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trialConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, TrialConfig.trialConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_trialConfig(IntPtr L)
	{
		TrialConfig.trialConfig = (Dictionary<uint,List<TrialData>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<TrialData>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadTrialData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		obj.ReadTrialData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTrialListByDay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<TrialData> o = obj.GetTrialListByDay(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTrialDataByIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		TrialData o = obj.GetTrialDataByIndex(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTotalScore(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TrialConfig obj = (TrialConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TrialConfig");
		uint o = obj.GetTotalScore();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

