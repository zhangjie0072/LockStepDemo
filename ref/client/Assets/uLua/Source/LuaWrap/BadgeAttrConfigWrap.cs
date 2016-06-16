using System;
using System.Collections.Generic;
using LuaInterface;

public class BadgeAttrConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetBaseConfig", GetBaseConfig),
			new LuaMethod("New", _CreateBadgeAttrConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("allConfig", get_allConfig, set_allConfig),
		};

		LuaScriptMgr.RegisterLib(L, "BadgeAttrConfig", typeof(BadgeAttrConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBadgeAttrConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BadgeAttrConfig obj = new BadgeAttrConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BadgeAttrConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BadgeAttrConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrConfig obj = (BadgeAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name allConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index allConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.allConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_allConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeAttrConfig obj = (BadgeAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name allConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index allConfig on a nil value");
			}
		}

		obj.allConfig = (Dictionary<uint,BadgeAttrBaseConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,BadgeAttrBaseConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeAttrConfig obj = (BadgeAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeAttrConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBaseConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeAttrConfig obj = (BadgeAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeAttrConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BadgeAttrBaseConfig o = obj.GetBaseConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

