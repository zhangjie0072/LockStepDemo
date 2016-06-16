using System;
using LuaInterface;

public class PlayerState_StealWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PreEnter", PreEnter),
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnSteal", OnSteal),
			new LuaMethod("InStealPosition", InStealPosition),
			new LuaMethod("InAutoPosition", InAutoPosition),
			new LuaMethod("GetStealPosition", GetStealPosition),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_Steal),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("forcedByAI", get_forcedByAI, set_forcedByAI),
			new LuaField("m_bGetBall", get_m_bGetBall, set_m_bGetBall),
			new LuaField("stealTarget", get_stealTarget, set_stealTarget),
			new LuaField("onSteal", get_onSteal, set_onSteal),
			new LuaField("m_success", get_m_success, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Steal", typeof(PlayerState_Steal), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Steal(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Steal obj = new PlayerState_Steal(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Steal.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Steal);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forcedByAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forcedByAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forcedByAI on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.forcedByAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bGetBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bGetBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bGetBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bGetBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stealTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stealTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stealTarget on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.stealTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onSteal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSteal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSteal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onSteal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_success(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_success");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_success on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_success);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_forcedByAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forcedByAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forcedByAI on a nil value");
			}
		}

		obj.forcedByAI = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bGetBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bGetBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bGetBall on a nil value");
			}
		}

		obj.m_bGetBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stealTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stealTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stealTarget on a nil value");
			}
		}

		obj.stealTarget = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onSteal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSteal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSteal on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onSteal = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onSteal = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PreEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Steal");
		bool o = obj.PreEnter();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Steal obj = (PlayerState_Steal)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Steal");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSteal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Steal");
		obj.OnSteal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InStealPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		UBasketball arg1 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		bool o = PlayerState_Steal.InStealPosition(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InAutoPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		UBasketball arg1 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		bool o = PlayerState_Steal.InAutoPosition(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStealPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		IM.Vector3 o = PlayerState_Steal.GetStealPosition(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Steal obj = (PlayerState_Steal)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Steal");
		obj.OnExit();
		return 0;
	}
}

