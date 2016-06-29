using System;
using LuaInterface;

public class MatchStateWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnEvent", OnEvent),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("ViewUpdate", ViewUpdate),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("New", _CreateMatchState),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_eState", get_m_eState, null),
			new LuaField("m_match", get_m_match, null),
		};

		LuaScriptMgr.RegisterLib(L, "MatchState", typeof(MatchState), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchState(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchState obj = new MatchState(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchState.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchState);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_eState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchState obj = (MatchState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_eState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_eState on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_eState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_match(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchState obj = (MatchState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_match");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_match on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_match);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
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
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.ViewUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchState obj = (MatchState)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchState");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

