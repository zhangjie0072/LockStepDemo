using System;
using LuaInterface;

public class PlayerStateMachineWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReplaceState", ReplaceState),
			new LuaMethod("GetState", GetState),
			new LuaMethod("Update", Update),
			new LuaMethod("SetState", SetState),
			new LuaMethod("New", _CreatePlayerStateMachine),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onStateChanged", get_onStateChanged, set_onStateChanged),
			new LuaField("attackRandom", get_attackRandom, set_attackRandom),
			new LuaField("m_listeners", get_m_listeners, null),
			new LuaField("m_curState", get_m_curState, null),
			new LuaField("assistAI", get_assistAI, null),
			new LuaField("m_owner", get_m_owner, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerStateMachine", typeof(PlayerStateMachine), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerStateMachine(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			PlayerStateMachine obj = new PlayerStateMachine(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerStateMachine.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerStateMachine);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onStateChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onStateChanged on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onStateChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attackRandom(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attackRandom");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attackRandom on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attackRandom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_listeners(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_listeners");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_listeners on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_listeners);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_curState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

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
	static int get_assistAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assistAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assistAI on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.assistAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_owner(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

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
	static int set_onStateChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onStateChanged on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onStateChanged = (Action<PlayerState,PlayerState>)LuaScriptMgr.GetNetObject(L, 3, typeof(Action<PlayerState,PlayerState>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onStateChanged = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				LuaScriptMgr.PushObject(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attackRandom(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerStateMachine obj = (PlayerStateMachine)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attackRandom");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attackRandom on a nil value");
			}
		}

		obj.attackRandom = (PseudoRandom)LuaScriptMgr.GetNetObject(L, 3, typeof(PseudoRandom));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReplaceState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		PlayerState o = obj.ReplaceState(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
		PlayerState.State arg0 = (PlayerState.State)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState.State));
		PlayerState o = obj.GetState(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetState(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(PlayerStateMachine), typeof(PlayerState.State)))
		{
			PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
			PlayerState.State arg0 = (PlayerState.State)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.SetState(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(PlayerStateMachine), typeof(PlayerState)))
		{
			PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
			PlayerState arg0 = (PlayerState)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.SetState(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(PlayerStateMachine), typeof(PlayerState), typeof(bool)))
		{
			PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
			PlayerState arg0 = (PlayerState)LuaScriptMgr.GetLuaObject(L, 2);
			bool arg1 = LuaDLL.lua_toboolean(L, 3);
			bool o = obj.SetState(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(PlayerStateMachine), typeof(PlayerState.State), typeof(bool)))
		{
			PlayerStateMachine obj = (PlayerStateMachine)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine");
			PlayerState.State arg0 = (PlayerState.State)LuaScriptMgr.GetLuaObject(L, 2);
			bool arg1 = LuaDLL.lua_toboolean(L, 3);
			bool o = obj.SetState(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerStateMachine.SetState");
		}

		return 0;
	}
}

