using System;
using System.Collections.Generic;
using LuaInterface;

public class RoleGiftConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetRoleGiftList", GetRoleGiftList),
			new LuaMethod("New", _CreateRoleGiftConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("roleList", get_roleList, set_roleList),
		};

		LuaScriptMgr.RegisterLib(L, "RoleGiftConfig", typeof(RoleGiftConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleGiftConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RoleGiftConfig obj = new RoleGiftConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RoleGiftConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(RoleGiftConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roleList(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, RoleGiftConfig.roleList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roleList(IntPtr L)
	{
		RoleGiftConfig.roleList = (Dictionary<uint,List<uint>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,List<uint>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RoleGiftConfig obj = (RoleGiftConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleGiftConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRoleGiftList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RoleGiftConfig obj = (RoleGiftConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RoleGiftConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<uint> o = obj.GetRoleGiftList(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

