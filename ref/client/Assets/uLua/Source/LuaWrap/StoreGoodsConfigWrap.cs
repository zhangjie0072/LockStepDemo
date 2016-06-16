using System;
using System.Collections.Generic;
using LuaInterface;

public class StoreGoodsConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadStoreRefreshConsumeData", ReadStoreRefreshConsumeData),
			new LuaMethod("ReadStoreData", ReadStoreData),
			new LuaMethod("GetConsume", GetConsume),
			new LuaMethod("GetStoreGoodsData", GetStoreGoodsData),
			new LuaMethod("GetStoreGoodsDataList", GetStoreGoodsDataList),
			new LuaMethod("New", _CreateStoreGoodsConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "StoreGoodsConfig", typeof(StoreGoodsConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStoreGoodsConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			StoreGoodsConfig obj = new StoreGoodsConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: StoreGoodsConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(StoreGoodsConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadStoreRefreshConsumeData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		obj.ReadStoreRefreshConsumeData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadStoreData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		obj.ReadStoreData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		fogs.proto.config.StoreRefreshConsumeConfig o = obj.GetConsume(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStoreGoodsData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		StoreGoodsData o = obj.GetStoreGoodsData(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStoreGoodsDataList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		StoreGoodsConfig obj = (StoreGoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StoreGoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<StoreGoodsData> o = obj.GetStoreGoodsDataList(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

