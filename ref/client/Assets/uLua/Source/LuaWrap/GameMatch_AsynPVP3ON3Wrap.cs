using System;
using LuaInterface;

public class GameMatch_AsynPVP3ON3Wrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetNotifyGameStart", SetNotifyGameStart),
			new LuaMethod("GetRivalScore", GetRivalScore),
			new LuaMethod("GetRivalName", GetRivalName),
			new LuaMethod("OnGameBegin", OnGameBegin),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("EnableTakeOver", EnableTakeOver),
			new LuaMethod("EnableMatchTips", EnableMatchTips),
			new LuaMethod("EnablePlayerTips", EnablePlayerTips),
			new LuaMethod("EnableMatchAchievement", EnableMatchAchievement),
			new LuaMethod("EnableEnhanceAttr", EnableEnhanceAttr),
			new LuaMethod("EnableSwitchDefenseTarget", EnableSwitchDefenseTarget),
			new LuaMethod("New", _CreateGameMatch_AsynPVP3ON3),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "GameMatch_AsynPVP3ON3", typeof(GameMatch_AsynPVP3ON3), regs, fields, typeof(GameMatch_MultiPlayer));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameMatch_AsynPVP3ON3(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			GameMatch.Config arg0 = (GameMatch.Config)LuaScriptMgr.GetNetObject(L, 1, typeof(GameMatch.Config));
			GameMatch_AsynPVP3ON3 obj = new GameMatch_AsynPVP3ON3(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameMatch_AsynPVP3ON3.New");
		}

		return 0;
	}

	static Type classType = typeof(GameMatch_AsynPVP3ON3);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNotifyGameStart(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		LuaStringBuffer arg0 = LuaScriptMgr.GetStringBuffer(L, 2);
		obj.SetNotifyGameStart(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRivalScore(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		uint o = obj.GetRivalScore();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRivalName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		string o = obj.GetRivalName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGameBegin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		fogs.proto.msg.GameBeginResp arg0 = (fogs.proto.msg.GameBeginResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GameBeginResp));
		obj.OnGameBegin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableTakeOver(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnableTakeOver();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableMatchTips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnableMatchTips();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnablePlayerTips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnablePlayerTips();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableMatchAchievement(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnableMatchAchievement();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableEnhanceAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnableEnhanceAttr();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableSwitchDefenseTarget(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_AsynPVP3ON3 obj = (GameMatch_AsynPVP3ON3)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_AsynPVP3ON3");
		bool o = obj.EnableSwitchDefenseTarget();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

