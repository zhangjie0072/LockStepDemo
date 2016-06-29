using System;
using LuaInterface;

public class MatchStatePlayingWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnMatchTimeUp", OnMatchTimeUp),
			new LuaMethod("OnEvent", OnEvent),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreateMatchStatePlaying),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "MatchStatePlaying", typeof(MatchStatePlaying), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStatePlaying(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStatePlaying obj = new MatchStatePlaying(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStatePlaying.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStatePlaying);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlaying obj = (MatchStatePlaying)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlaying");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMatchTimeUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlaying obj = (MatchStatePlaying)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlaying");
		obj.OnMatchTimeUp();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MatchStatePlaying obj = (MatchStatePlaying)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlaying");
		PlayerActionEventHandler.AnimEvent arg0 = (PlayerActionEventHandler.AnimEvent)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerActionEventHandler.AnimEvent));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		object arg2 = LuaScriptMgr.GetVarObject(L, 4);
		obj.OnEvent(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlaying obj = (MatchStatePlaying)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlaying");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlaying obj = (MatchStatePlaying)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlaying");
		obj.OnExit();
		return 0;
	}
}

