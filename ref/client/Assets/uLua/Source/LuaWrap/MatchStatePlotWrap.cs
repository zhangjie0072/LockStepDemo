using System;
using LuaInterface;

public class MatchStatePlotWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("ShowDialog", ShowDialog),
			new LuaMethod("OnNextDialogClick", OnNextDialogClick),
			new LuaMethod("OnEndResult", OnEndResult),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("New", _CreateMatchStatePlot),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "MatchStatePlot", typeof(MatchStatePlot), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStatePlot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchState.State arg1 = (MatchState.State)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState.State));
			MatchStatePlot obj = new MatchStatePlot(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStatePlot.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStatePlot);

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
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowDialog(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.ShowDialog(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnNextDialogClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		obj.OnNextDialogClick();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndResult(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		obj.OnEndResult();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStatePlot obj = (MatchStatePlot)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStatePlot");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

