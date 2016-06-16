using System;
using System.Collections.Generic;
using LuaInterface;

public class ActivityConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadActivityData", ReadActivityData),
			new LuaMethod("GetActivityData", GetActivityData),
			new LuaMethod("New", _CreateActivityConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("activityConfig", get_activityConfig, set_activityConfig),
		};

		LuaScriptMgr.RegisterLib(L, "ActivityConfig", typeof(ActivityConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateActivityConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ActivityConfig obj = new ActivityConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ActivityConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(ActivityConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activityConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ActivityConfig.activityConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_activityConfig(IntPtr L)
	{
		ActivityConfig.activityConfig = (List<ActivityData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ActivityData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ActivityConfig obj = (ActivityConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ActivityConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ActivityConfig obj = (ActivityConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ActivityConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadActivityData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ActivityConfig obj = (ActivityConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ActivityConfig");
		obj.ReadActivityData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetActivityData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ActivityConfig obj = (ActivityConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ActivityConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		ActivityData o = obj.GetActivityData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

