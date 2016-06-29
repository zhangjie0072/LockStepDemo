using System;
using LuaInterface;

public class PlayerState_KnockedWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_Knocked),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_bKnockedRecover", get_m_bKnockedRecover, set_m_bKnockedRecover),
			new LuaField("m_bToHoldBall", get_m_bToHoldBall, set_m_bToHoldBall),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Knocked", typeof(PlayerState_Knocked), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Knocked(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Knocked obj = new PlayerState_Knocked(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Knocked.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Knocked);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bKnockedRecover(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Knocked obj = (PlayerState_Knocked)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bKnockedRecover");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bKnockedRecover on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bKnockedRecover);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bToHoldBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Knocked obj = (PlayerState_Knocked)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToHoldBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToHoldBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bToHoldBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bKnockedRecover(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Knocked obj = (PlayerState_Knocked)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bKnockedRecover");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bKnockedRecover on a nil value");
			}
		}

		obj.m_bKnockedRecover = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bToHoldBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Knocked obj = (PlayerState_Knocked)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToHoldBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToHoldBall on a nil value");
			}
		}

		obj.m_bToHoldBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Knocked obj = (PlayerState_Knocked)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Knocked");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Knocked obj = (PlayerState_Knocked)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Knocked");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Knocked obj = (PlayerState_Knocked)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Knocked");
		obj.OnExit();
		return 0;
	}
}

