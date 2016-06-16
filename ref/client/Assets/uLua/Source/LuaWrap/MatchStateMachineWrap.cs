using System;
using System.Collections;
using LuaInterface;

public class MatchStateMachineWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetState", GetState),
			new LuaMethod("SetState", SetState),
			new LuaMethod("ReplaceState", ReplaceState),
			new LuaMethod("Update", Update),
			new LuaMethod("New", _CreateMatchStateMachine),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_curState", get_m_curState, null),
			new LuaField("m_owner", get_m_owner, null),
			new LuaField("m_matchStateListeners", get_m_matchStateListeners, null),
		};

		LuaScriptMgr.RegisterLib(L, "MatchStateMachine", typeof(MatchStateMachine), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStateMachine(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			GameMatch arg0 = (GameMatch)LuaScriptMgr.GetNetObject(L, 1, typeof(GameMatch));
			MatchStateMachine obj = new MatchStateMachine(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateMachine.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStateMachine);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_curState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateMachine obj = (MatchStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curState on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_curState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_owner(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateMachine obj = (MatchStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_owner");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_owner on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_owner);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_matchStateListeners(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MatchStateMachine obj = (MatchStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_matchStateListeners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_matchStateListeners on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_matchStateListeners);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
		IEnumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
		MatchState.State arg0 = (MatchState.State)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState.State));
		MatchState o = obj.GetState(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetState(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
			MatchState.State arg0 = (MatchState.State)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState.State));
			bool o = obj.SetState(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
			MatchState.State arg0 = (MatchState.State)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState.State));
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			bool o = obj.SetState(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateMachine.SetState");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReplaceState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		MatchState o = obj.ReplaceState(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MatchStateMachine), typeof(float)))
		{
			MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
			float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
			obj.Update(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MatchStateMachine), typeof(IM.Number)))
		{
			MatchStateMachine obj = (MatchStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateMachine");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			obj.Update(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateMachine.Update");
		}

		return 0;
	}
}

