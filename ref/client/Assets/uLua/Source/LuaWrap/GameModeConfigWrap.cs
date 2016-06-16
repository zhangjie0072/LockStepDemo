using System;
using LuaInterface;

public class GameModeConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetGameMode", GetGameMode),
			new LuaMethod("GetComboBonus", GetComboBonus),
			new LuaMethod("New", _CreateGameModeConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "GameModeConfig", typeof(GameModeConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameModeConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameModeConfig obj = new GameModeConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameModeConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(GameModeConfig);

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
		GameModeConfig obj = (GameModeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameModeConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGameMode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameModeConfig obj = (GameModeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameModeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		GameMode o = obj.GetGameMode(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComboBonus(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameModeConfig obj = (GameModeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameModeConfig");
		GameMatch.Type arg0 = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch.Type));
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		float o = obj.GetComboBonus(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

