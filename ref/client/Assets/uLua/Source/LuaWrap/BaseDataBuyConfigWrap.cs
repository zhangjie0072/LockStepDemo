using System;
using System.Collections.Generic;
using LuaInterface;

public class BaseDataBuyConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadBuyGoldData", ReadBuyGoldData),
			new LuaMethod("ReadBuyHPData", ReadBuyHPData),
			new LuaMethod("GetBuyGoldDataByTimes", GetBuyGoldDataByTimes),
			new LuaMethod("GetBuyHpDataByTimes", GetBuyHpDataByTimes),
			new LuaMethod("GetBuyGoldMaxTimes", GetBuyGoldMaxTimes),
			new LuaMethod("GetBuyHpMaxTimes", GetBuyHpMaxTimes),
			new LuaMethod("New", _CreateBaseDataBuyConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("BuyGoldDatas", get_BuyGoldDatas, set_BuyGoldDatas),
			new LuaField("BuyHpDatas", get_BuyHpDatas, set_BuyHpDatas),
		};

		LuaScriptMgr.RegisterLib(L, "BaseDataBuyConfig", typeof(BaseDataBuyConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBaseDataBuyConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BaseDataBuyConfig obj = new BaseDataBuyConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BaseDataBuyConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BaseDataBuyConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BuyGoldDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyGoldDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyGoldDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BuyGoldDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BuyHpDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyHpDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyHpDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.BuyHpDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BuyGoldDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyGoldDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyGoldDatas on a nil value");
			}
		}

		obj.BuyGoldDatas = (List<BuyData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<BuyData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_BuyHpDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name BuyHpDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index BuyHpDatas on a nil value");
			}
		}

		obj.BuyHpDatas = (List<BuyData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<BuyData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadBuyGoldData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		obj.ReadBuyGoldData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadBuyHPData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		obj.ReadBuyHPData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuyGoldDataByTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BuyData o = obj.GetBuyGoldDataByTimes(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuyHpDataByTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		BuyData o = obj.GetBuyHpDataByTimes(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuyGoldMaxTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		uint o = obj.GetBuyGoldMaxTimes();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuyHpMaxTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataBuyConfig obj = (BaseDataBuyConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataBuyConfig");
		uint o = obj.GetBuyHpMaxTimes();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

