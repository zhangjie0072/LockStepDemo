using System;
using System.Collections.Generic;
using LuaInterface;

public class QualifyingConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadConsume", ReadConsume),
			new LuaMethod("GetAwardsData", GetAwardsData),
			new LuaMethod("GetRobotPlayer", GetRobotPlayer),
			new LuaMethod("GetRobotPlayerAttr", GetRobotPlayerAttr),
			new LuaMethod("RobotTeamAI", RobotTeamAI),
			new LuaMethod("GetQualifyingConsume", GetQualifyingConsume),
			new LuaMethod("New", _CreateQualifyingConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("DayAwardsData", get_DayAwardsData, set_DayAwardsData),
			new LuaField("RobotPlayerData", get_RobotPlayerData, set_RobotPlayerData),
			new LuaField("qualifyingConsumes", get_qualifyingConsumes, set_qualifyingConsumes),
		};

		LuaScriptMgr.RegisterLib(L, "QualifyingConfig", typeof(QualifyingConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateQualifyingConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualifyingConfig obj = new QualifyingConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualifyingConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(QualifyingConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DayAwardsData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DayAwardsData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DayAwardsData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.DayAwardsData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RobotPlayerData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RobotPlayerData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RobotPlayerData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RobotPlayerData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_qualifyingConsumes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingConsumes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingConsumes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.qualifyingConsumes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DayAwardsData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DayAwardsData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DayAwardsData on a nil value");
			}
		}

		obj.DayAwardsData = (List<DataById>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<DataById>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RobotPlayerData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RobotPlayerData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RobotPlayerData on a nil value");
			}
		}

		obj.RobotPlayerData = (List<RobotTeam>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<RobotTeam>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_qualifyingConsumes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingConfig obj = (QualifyingConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name qualifyingConsumes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index qualifyingConsumes on a nil value");
			}
		}

		obj.qualifyingConsumes = (List<QualifyingConsume>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<QualifyingConsume>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		obj.ReadConsume();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAwardsData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		DataById o = obj.GetAwardsData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRobotPlayer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<RobotPlayer> o = obj.GetRobotPlayer(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRobotPlayerAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		RobotTeam o = obj.GetRobotPlayerAttr(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RobotTeamAI(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.RobotTeamAI(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualifyingConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingConfig obj = (QualifyingConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		QualifyingConsume o = obj.GetQualifyingConsume(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

