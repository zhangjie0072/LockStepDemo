using System;
using System.Collections.Generic;
using LuaInterface;

public class RoleLevelConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetFactor", GetFactor),
			new LuaMethod("GetMaxExp", GetMaxExp),
			new LuaMethod("New", _CreateRoleLevelConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("RoleLevelDatas", get_RoleLevelDatas, set_RoleLevelDatas),
		};

		LuaScriptMgr.RegisterLib(L, "RoleLevelConfig", typeof(RoleLevelConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleLevelConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RoleLevelConfig obj = new RoleLevelConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RoleLevelConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(RoleLevelConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RoleLevelDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleLevelConfig obj = (RoleLevelConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleLevelDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleLevelDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.RoleLevelDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RoleLevelDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleLevelConfig obj = (RoleLevelConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name RoleLevelDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index RoleLevelDatas on a nil value");
			}
		}

		obj.RoleLevelDatas = (Dictionary<uint,RoleLevel>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,RoleLevel>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RoleLevelConfig obj = (RoleLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleLevelConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RoleLevelConfig obj = (RoleLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleLevelConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFactor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RoleLevelConfig obj = (RoleLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		float o = obj.GetFactor(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RoleLevelConfig obj = (RoleLevelConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleLevelConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxExp(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

