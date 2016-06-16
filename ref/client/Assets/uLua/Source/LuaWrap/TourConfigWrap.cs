using System;
using System.Collections.Generic;
using LuaInterface;

public class TourConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetTourData", GetTourData),
			new LuaMethod("GetResetCost", GetResetCost),
			new LuaMethod("GetResetTimes", GetResetTimes),
			new LuaMethod("New", _CreateTourConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "TourConfig", typeof(TourConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTourConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TourConfig obj = new TourConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TourConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(TourConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TourConfig obj = (TourConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TourConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTourData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		TourConfig obj = (TourConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TourConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		TourData o = obj.GetTourData(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResetCost(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TourConfig obj = (TourConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TourConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		KeyValuePair<uint,uint> o = obj.GetResetCost(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResetTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TourConfig obj = (TourConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TourConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetResetTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

