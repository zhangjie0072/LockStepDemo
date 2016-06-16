using System;
using LuaInterface;

public class PlayerState_BlockWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InBlockArea", InBlockArea),
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("LateUpdate", LateUpdate),
			new LuaMethod("OnBlock", OnBlock),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_Block),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("idBlockGrabBall", get_idBlockGrabBall, null),
			new LuaField("idBlockPassBall", get_idBlockPassBall, null),
			new LuaField("m_success", get_m_success, set_m_success),
			new LuaField("m_failedShootSolution", get_m_failedShootSolution, set_m_failedShootSolution),
			new LuaField("m_loseBallContext", get_m_loseBallContext, set_m_loseBallContext),
			new LuaField("m_blockedMoveVel", get_m_blockedMoveVel, set_m_blockedMoveVel),
			new LuaField("m_failReason", get_m_failReason, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Block", typeof(PlayerState_Block), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Block(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Block obj = new PlayerState_Block(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Block.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Block);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_idBlockGrabBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState_Block.idBlockGrabBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_idBlockPassBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState_Block.idBlockPassBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_success(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

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
	static int get_m_failedShootSolution(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_failedShootSolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_failedShootSolution on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_failedShootSolution);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_loseBallContext(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loseBallContext");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loseBallContext on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_loseBallContext);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_blockedMoveVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_blockedMoveVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_blockedMoveVel on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_blockedMoveVel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_failReason(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_failReason");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_failReason on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_failReason);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_success(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

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

		obj.m_success = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_failedShootSolution(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_failedShootSolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_failedShootSolution on a nil value");
			}
		}

		obj.m_failedShootSolution = (ShootSolution)LuaScriptMgr.GetNetObject(L, 3, typeof(ShootSolution));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_loseBallContext(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loseBallContext");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loseBallContext on a nil value");
			}
		}

		obj.m_loseBallContext = (LostBallContext)LuaScriptMgr.GetNetObject(L, 3, typeof(LostBallContext));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_blockedMoveVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Block obj = (PlayerState_Block)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_blockedMoveVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_blockedMoveVel on a nil value");
			}
		}

		obj.m_blockedMoveVel = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InBlockArea(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.Vector3 arg2 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		bool o = PlayerState_Block.InBlockArea(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Block obj = (PlayerState_Block)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Block");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Block obj = (PlayerState_Block)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Block");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.LateUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Block obj = (PlayerState_Block)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Block");
		obj.OnBlock();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Block obj = (PlayerState_Block)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Block");
		obj.OnExit();
		return 0;
	}
}

