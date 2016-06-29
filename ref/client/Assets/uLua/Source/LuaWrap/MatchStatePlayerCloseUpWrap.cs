using System;
using LuaInterface;

public class MatchStatePlayerCloseUpWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("ViewUpdate", ViewUpdate),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreateMatchStatePlayerCloseUp),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "MatchStatePlayerCloseUp", typeof(MatchStatePlayerCloseUp), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStatePlayerCloseUp(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStatePlayerCloseUp obj = new MatchStatePlayerCloseUp(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStatePlayerCloseUp.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStatePlayerCloseUp);

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
		MatchStatePlayerCloseUp obj = (MatchStatePlayerCloseUp)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlayerCloseUp");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlayerCloseUp obj = (MatchStatePlayerCloseUp)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlayerCloseUp");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.ViewUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlayerCloseUp obj = (MatchStatePlayerCloseUp)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlayerCloseUp");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlayerCloseUp obj = (MatchStatePlayerCloseUp)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlayerCloseUp");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlayerCloseUp obj = (MatchStatePlayerCloseUp)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlayerCloseUp");
		obj.OnExit();
		return 0;
	}
}

