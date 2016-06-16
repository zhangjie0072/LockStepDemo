using System;
using System.Collections.Generic;
using LuaInterface;

public class BullFightConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetFightLevel", GetFightLevel),
			new LuaMethod("Read", Read),
			new LuaMethod("ReadConsume", ReadConsume),
			new LuaMethod("GetBullFightConsume", GetBullFightConsume),
			new LuaMethod("New", _CreateBullFightConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("levels", get_levels, set_levels),
			new LuaField("bullFightConsumes", get_bullFightConsumes, set_bullFightConsumes),
		};

		LuaScriptMgr.RegisterLib(L, "BullFightConfig", typeof(BullFightConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBullFightConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BullFightConfig obj = new BullFightConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BullFightConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BullFightConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_levels(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, BullFightConfig.levels);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bullFightConsumes(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, BullFightConfig.bullFightConsumes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_levels(IntPtr L)
	{
		BullFightConfig.levels = (Dictionary<uint,BullFightLevel>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,BullFightLevel>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bullFightConsumes(IntPtr L)
	{
		BullFightConfig.bullFightConsumes = (List<BullFightConsume>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<BullFightConsume>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFightLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BullFightLevel o = obj.GetFightLevel(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Read(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		obj.Read();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		obj.ReadConsume();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBullFightConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BullFightConfig obj = (BullFightConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BullFightConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BullFightConsume o = obj.GetBullFightConsume(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

