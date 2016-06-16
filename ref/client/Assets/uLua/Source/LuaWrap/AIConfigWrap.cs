using System;
using System.Collections.Generic;
using LuaInterface;

public class AIConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetRandAIName", GetRandAIName),
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateAIConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
			new LuaField("names", get_names, set_names),
		};

		LuaScriptMgr.RegisterLib(L, "AIConfig", typeof(AIConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAIConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AIConfig obj = new AIConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AIConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(AIConfig);

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
		AIConfig obj = (AIConfig)o;

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
	static int get_names(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIConfig obj = (AIConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name names");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index names on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.names);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIConfig obj = (AIConfig)o;

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

		obj.configs = (Dictionary<uint,AIConfig.AI>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,AIConfig.AI>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_names(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AIConfig obj = (AIConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name names");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index names on a nil value");
			}
		}

		obj.names = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRandAIName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIConfig obj = (AIConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AIConfig");
		string o = obj.GetRandAIName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AIConfig obj = (AIConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AIConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		AIConfig.AI o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AIConfig obj = (AIConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AIConfig");
		obj.ReadConfig();
		return 0;
	}
}

