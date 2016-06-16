using System;
using System.Collections.Generic;
using LuaInterface;

public class StarAttrConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetStarAttr", GetStarAttr),
			new LuaMethod("GetAttr", GetAttr),
			new LuaMethod("New", _CreateStarAttrConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("starAttrData", get_starAttrData, set_starAttrData),
		};

		LuaScriptMgr.RegisterLib(L, "StarAttrConfig", typeof(StarAttrConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStarAttrConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			StarAttrConfig obj = new StarAttrConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: StarAttrConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(StarAttrConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_starAttrData(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, StarAttrConfig.starAttrData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_starAttrData(IntPtr L)
	{
		StarAttrConfig.starAttrData = (List<StarAttr>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<StarAttr>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StarAttrConfig obj = (StarAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StarAttrConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		StarAttrConfig obj = (StarAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StarAttrConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStarAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		StarAttrConfig obj = (StarAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StarAttrConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		StarAttr o = obj.GetStarAttr(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		StarAttrConfig obj = (StarAttrConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "StarAttrConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		Dictionary<uint,uint> o = obj.GetAttr(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

