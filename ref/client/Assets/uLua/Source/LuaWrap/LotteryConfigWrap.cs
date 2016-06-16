using System;
using LuaInterface;

public class LotteryConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetLottery", GetLottery),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateLotteryConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "LotteryConfig", typeof(LotteryConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLotteryConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LotteryConfig obj = new LotteryConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LotteryConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(LotteryConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLottery(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		LotteryConfig obj = (LotteryConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LotteryConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		fogs.proto.config.LotteryConfig o = obj.GetLottery(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LotteryConfig obj = (LotteryConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "LotteryConfig");
		obj.ReadConfig();
		return 0;
	}
}

