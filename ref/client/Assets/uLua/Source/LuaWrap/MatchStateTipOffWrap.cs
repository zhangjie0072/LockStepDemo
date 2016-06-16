using System;
using LuaInterface;

public class MatchStateTipOffWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnEvent", OnEvent),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("New", _CreateMatchStateTipOff),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onCounterDone", get_onCounterDone, set_onCounterDone),
		};

		LuaScriptMgr.RegisterLib(L, "MatchStateTipOff", typeof(MatchStateTipOff), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStateTipOff(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStateTipOff obj = new MatchStateTipOff(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateTipOff.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStateTipOff);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCounterDone(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateTipOff obj = (MatchStateTipOff)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCounterDone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCounterDone on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onCounterDone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onCounterDone(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateTipOff obj = (MatchStateTipOff)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCounterDone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCounterDone on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onCounterDone = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onCounterDone = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateTipOff obj = (MatchStateTipOff)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateTipOff");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MatchStateTipOff obj = (MatchStateTipOff)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateTipOff");
		PlayerActionEventHandler.AnimEvent arg0 = (PlayerActionEventHandler.AnimEvent)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerActionEventHandler.AnimEvent));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		object arg2 = LuaScriptMgr.GetVarObject(L, 4);
		obj.OnEvent(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateTipOff obj = (MatchStateTipOff)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateTipOff");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateTipOff obj = (MatchStateTipOff)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateTipOff");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateTipOff obj = (MatchStateTipOff)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateTipOff");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

