using System;
using LuaInterface;

public class PlayerState_StolenWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnLostBall", OnLostBall),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_Stolen),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_bLostBall", get_m_bLostBall, set_m_bLostBall),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Stolen", typeof(PlayerState_Stolen), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Stolen(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Stolen obj = new PlayerState_Stolen(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Stolen.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Stolen);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bLostBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Stolen obj = (PlayerState_Stolen)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bLostBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bLostBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bLostBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bLostBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Stolen obj = (PlayerState_Stolen)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bLostBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bLostBall on a nil value");
			}
		}

		obj.m_bLostBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Stolen obj = (PlayerState_Stolen)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Stolen");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLostBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Stolen obj = (PlayerState_Stolen)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Stolen");
		obj.OnLostBall();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Stolen obj = (PlayerState_Stolen)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Stolen");
		obj.OnExit();
		return 0;
	}
}

