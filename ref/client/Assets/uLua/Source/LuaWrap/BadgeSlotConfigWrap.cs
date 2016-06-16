using System;
using System.Collections.Generic;
using LuaInterface;

public class BadgeSlotConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetAllConfigs", GetAllConfigs),
			new LuaMethod("New", _CreateBadgeSlotConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
		};

		LuaScriptMgr.RegisterLib(L, "BadgeSlotConfig", typeof(BadgeSlotConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBadgeSlotConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BadgeSlotConfig obj = new BadgeSlotConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BadgeSlotConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BadgeSlotConfig);

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
		BadgeSlotConfig obj = (BadgeSlotConfig)o;

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
		BadgeSlotConfig obj = (BadgeSlotConfig)o;

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

		obj.configs = (Dictionary<uint,BadgeSlotBaseConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,BadgeSlotBaseConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSlotConfig obj = (BadgeSlotConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSlotConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BadgeSlotBaseConfig o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSlotConfig obj = (BadgeSlotConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSlotConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllConfigs(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSlotConfig obj = (BadgeSlotConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSlotConfig");
		Dictionary<uint,BadgeSlotBaseConfig> o = obj.GetAllConfigs();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

