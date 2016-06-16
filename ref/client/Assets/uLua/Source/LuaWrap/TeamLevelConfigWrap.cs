using System;
using System.Collections.Generic;
using LuaInterface;

public class TeamLevelConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetMaxExp", GetMaxExp),
			new LuaMethod("GetMaxHP", GetMaxHP),
			new LuaMethod("GetMaxRoleQuality", GetMaxRoleQuality),
			new LuaMethod("GetMaxTattoo", GetMaxTattoo),
			new LuaMethod("GetMaxTrain", GetMaxTrain),
			new LuaMethod("GetMaxPassiveSkill", GetMaxPassiveSkill),
			new LuaMethod("GetQualityLimitLevel", GetQualityLimitLevel),
			new LuaMethod("GetAddHp", GetAddHp),
			new LuaMethod("GetUnLockdata", GetUnLockdata),
			new LuaMethod("GetTeamLevelDataWithReward", GetTeamLevelDataWithReward),
			new LuaMethod("New", _CreateTeamLevelConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("TeamLevelDatas", get_TeamLevelDatas, set_TeamLevelDatas),
		};

		LuaScriptMgr.RegisterLib(L, "TeamLevelConfig", typeof(TeamLevelConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTeamLevelConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TeamLevelConfig obj = new TeamLevelConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TeamLevelConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(TeamLevelConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TeamLevelDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelConfig obj = (TeamLevelConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TeamLevelDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TeamLevelDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.TeamLevelDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TeamLevelDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelConfig obj = (TeamLevelConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TeamLevelDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TeamLevelDatas on a nil value");
			}
		}

		obj.TeamLevelDatas = (Dictionary<uint,TeamLevelData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,TeamLevelData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxExp(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxHP(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxHP(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxRoleQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxRoleQuality(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxTattoo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxTattoo(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxTrain(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxTrain(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxPassiveSkill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxPassiveSkill(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualityLimitLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetQualityLimitLevel(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAddHp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetAddHp(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUnLockdata(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		TeamLevelData o = obj.GetUnLockdata(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTeamLevelDataWithReward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TeamLevelConfig obj = (TeamLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TeamLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		TeamLevelData o = obj.GetTeamLevelDataWithReward(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

