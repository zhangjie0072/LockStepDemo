using System;
using System.Collections.Generic;
using LuaInterface;

public class AwardPackDataConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetAwardPackByID", GetAwardPackByID),
			new LuaMethod("GetAwardPackDatasByID", GetAwardPackDatasByID),
			new LuaMethod("New", _CreateAwardPackDataConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("awardPackConfig", get_awardPackConfig, set_awardPackConfig),
		};

		LuaScriptMgr.RegisterLib(L, "AwardPackDataConfig", typeof(AwardPackDataConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAwardPackDataConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AwardPackDataConfig obj = new AwardPackDataConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AwardPackDataConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(AwardPackDataConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardPackConfig(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, AwardPackDataConfig.awardPackConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardPackConfig(IntPtr L)
	{
		AwardPackDataConfig.awardPackConfig = (Dictionary<uint,fogs.proto.config.AwardPackConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.AwardPackConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AwardPackDataConfig obj = (AwardPackDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AwardPackDataConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AwardPackDataConfig obj = (AwardPackDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AwardPackDataConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAwardPackByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AwardPackDataConfig obj = (AwardPackDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AwardPackDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.AwardPackConfig o = obj.GetAwardPackByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAwardPackDatasByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AwardPackDataConfig obj = (AwardPackDataConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AwardPackDataConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<fogs.proto.config.AwardConfig> o = obj.GetAwardPackDatasByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

