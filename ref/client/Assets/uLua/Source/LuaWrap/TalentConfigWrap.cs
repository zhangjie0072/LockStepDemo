using System;
using LuaInterface;

public class TalentConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfigData", GetConfigData),
			new LuaMethod("New", _CreateTalentConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "TalentConfig", typeof(TalentConfig), regs, fields, typeof(ConfigBase));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTalentConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TalentConfig obj = new TalentConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TalentConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(TalentConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TalentConfig obj = (TalentConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "TalentConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		TalentItem o = obj.GetConfigData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

