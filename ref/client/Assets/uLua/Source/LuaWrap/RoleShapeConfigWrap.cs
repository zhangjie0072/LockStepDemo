using System;
using System.Collections.Generic;
using LuaInterface;

public class RoleShapeConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateRoleShapeConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
		};

		LuaScriptMgr.RegisterLib(L, "RoleShapeConfig", typeof(RoleShapeConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleShapeConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RoleShapeConfig obj = new RoleShapeConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RoleShapeConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(RoleShapeConfig);

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
		RoleShapeConfig obj = (RoleShapeConfig)o;

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
		RoleShapeConfig obj = (RoleShapeConfig)o;

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

		obj.configs = (Dictionary<uint,RoleShape>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,RoleShape>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RoleShapeConfig obj = (RoleShapeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleShapeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		RoleShape o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RoleShapeConfig obj = (RoleShapeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleShapeConfig");
		obj.ReadConfig();
		return 0;
	}
}

