using System;
using LuaInterface;

public class MatchStateGoalWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("New", _CreateMatchStateGoal),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "MatchStateGoal", typeof(MatchStateGoal), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStateGoal(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStateGoal obj = new MatchStateGoal(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateGoal.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStateGoal);

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
		MatchStateGoal obj = (MatchStateGoal)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateGoal");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateGoal obj = (MatchStateGoal)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateGoal");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateGoal obj = (MatchStateGoal)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateGoal");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateGoal obj = (MatchStateGoal)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateGoal");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

