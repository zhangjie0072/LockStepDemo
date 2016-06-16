using System;
using LuaInterface;

public class EquipmentConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetBaseConfig", GetBaseConfig),
			new LuaMethod("New", _CreateEquipmentConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "EquipmentConfig", typeof(EquipmentConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateEquipmentConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			EquipmentConfig obj = new EquipmentConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: EquipmentConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(EquipmentConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		EquipmentConfig obj = (EquipmentConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "EquipmentConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBaseConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		EquipmentConfig obj = (EquipmentConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "EquipmentConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		EquipmentBaseDataConfig o = obj.GetBaseConfig(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

