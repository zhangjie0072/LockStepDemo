using System;
using System.Collections.Generic;
using LuaInterface;

public class GoodsConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("ReadGoodsAttrData", ReadGoodsAttrData),
			new LuaMethod("ReadGoodsUseData", ReadGoodsUseData),
			new LuaMethod("ReadGoodsCompositeData", ReadGoodsCompositeData),
			new LuaMethod("GetgoodsAttrConfig", GetgoodsAttrConfig),
			new LuaMethod("CanComposite", CanComposite),
			new LuaMethod("GetComposite", GetComposite),
			new LuaMethod("GetSuitAttrConfig", GetSuitAttrConfig),
			new LuaMethod("GetGoodsUseConfig", GetGoodsUseConfig),
			new LuaMethod("GetGoodsDicByCategory", GetGoodsDicByCategory),
			new LuaMethod("New", _CreateGoodsConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isReadFinish", get_isReadFinish, set_isReadFinish),
		};

		LuaScriptMgr.RegisterLib(L, "GoodsConfig", typeof(GoodsConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGoodsConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GoodsConfig obj = new GoodsConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GoodsConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(GoodsConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isReadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsConfig obj = (GoodsConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReadFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isReadFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isReadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GoodsConfig obj = (GoodsConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReadFinish on a nil value");
			}
		}

		obj.isReadFinish = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadGoodsAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		obj.ReadGoodsAttrData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadGoodsUseData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		obj.ReadGoodsUseData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadGoodsCompositeData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		obj.ReadGoodsCompositeData();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetgoodsAttrConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.GoodsAttrConfig o = obj.GetgoodsAttrConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanComposite(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.CanComposite(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComposite(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.GoodsCompositeConfig o = obj.GetComposite(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSuitAttrConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		SuitAddnAttrData o = obj.GetSuitAttrConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoodsUseConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.GoodsUseConfig o = obj.GetGoodsUseConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGoodsDicByCategory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GoodsConfig obj = (GoodsConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GoodsConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		Dictionary<uint,fogs.proto.config.GoodsAttrConfig> o = obj.GetGoodsDicByCategory(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

