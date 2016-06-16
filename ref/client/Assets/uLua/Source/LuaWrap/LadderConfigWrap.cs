using System;
using System.Collections.Generic;
using LuaInterface;

public class LadderConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("Read", Read),
			new LuaMethod("ReadSeason", ReadSeason),
			new LuaMethod("ReadReward", ReadReward),
			new LuaMethod("GetLevelByScore", GetLevelByScore),
			new LuaMethod("GetSeason", GetSeason),
			new LuaMethod("GetReward", GetReward),
			new LuaMethod("New", _CreateLadderConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("levels", get_levels, set_levels),
			new LuaField("seasons", get_seasons, set_seasons),
			new LuaField("rewards", get_rewards, set_rewards),
		};

		LuaScriptMgr.RegisterLib(L, "LadderConfig", typeof(LadderConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLadderConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LadderConfig obj = new LadderConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LadderConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(LadderConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_levels(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, LadderConfig.levels);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_seasons(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, LadderConfig.seasons);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rewards(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, LadderConfig.rewards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_levels(IntPtr L)
	{
		LadderConfig.levels = (Dictionary<uint,LadderLevel>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,LadderLevel>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_seasons(IntPtr L)
	{
		LadderConfig.seasons = (Dictionary<uint,LadderSeason>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,LadderSeason>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rewards(IntPtr L)
	{
		LadderConfig.rewards = (Dictionary<uint,LadderReward>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,LadderReward>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Read(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		obj.Read();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadSeason(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		obj.ReadSeason();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadReward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		obj.ReadReward();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLevelByScore(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		LadderLevel o = obj.GetLevelByScore(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSeason(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		LadderSeason o = obj.GetSeason(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetReward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LadderConfig obj = (LadderConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LadderConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		LadderReward o = obj.GetReward(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

