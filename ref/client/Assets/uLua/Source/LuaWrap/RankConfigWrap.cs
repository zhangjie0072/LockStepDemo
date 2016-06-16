using System;
using LuaInterface;

public class RankConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateRankConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "RankConfig", typeof(RankConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRankConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RankConfig obj = new RankConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RankConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(RankConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			RankConfig obj = (RankConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RankConfig");
			fogs.proto.msg.RankType arg0 = (fogs.proto.msg.RankType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RankType));
			fogs.proto.config.RankConfig o = obj.GetConfig(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3)
		{
			RankConfig obj = (RankConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RankConfig");
			fogs.proto.msg.RankType arg0 = (fogs.proto.msg.RankType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.RankType));
			fogs.proto.msg.RankSubType arg1 = (fogs.proto.msg.RankSubType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RankSubType));
			fogs.proto.config.RankConfig o = obj.GetConfig(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RankConfig.GetConfig");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RankConfig obj = (RankConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "RankConfig");
		obj.ReadConfig();
		return 0;
	}
}

