using System;
using LuaInterface;

public class GameMatch_MultiPlayerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SwitchMainrole", SwitchMainrole),
			new LuaMethod("InitBallHolder", InitBallHolder),
			new LuaMethod("ResetPlayerPos", ResetPlayerPos),
			new LuaMethod("OnSwitch", OnSwitch),
			new LuaMethod("Update", Update),
			new LuaMethod("OnMatchStateChange", OnMatchStateChange),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "GameMatch_MultiPlayer", typeof(GameMatch_MultiPlayer), regs, fields, typeof(GameMatch));
	}

	static Type classType = typeof(GameMatch_MultiPlayer);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SwitchMainrole(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		obj.SwitchMainrole(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitBallHolder(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		obj.InitBallHolder();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetPlayerPos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		obj.ResetPlayerPos();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSwitch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		obj.OnSwitch();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMatchStateChange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		GameMatch_MultiPlayer obj = (GameMatch_MultiPlayer)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMatch_MultiPlayer");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		MatchState arg1 = (MatchState)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchState));
		obj.OnMatchStateChange(arg0,arg1);
		return 0;
	}
}

