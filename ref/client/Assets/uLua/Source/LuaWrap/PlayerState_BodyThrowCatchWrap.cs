using System;
using LuaInterface;

public class PlayerState_BodyThrowCatchWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("GetMaxDistance", GetMaxDistance),
			new LuaMethod("New", _CreatePlayerState_BodyThrowCatch),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_dirThrow", get_m_dirThrow, set_m_dirThrow),
			new LuaField("m_bSuccess", get_m_bSuccess, set_m_bSuccess),
			new LuaField("m_bCollideBall", get_m_bCollideBall, set_m_bCollideBall),
			new LuaField("m_vInitPos", get_m_vInitPos, set_m_vInitPos),
			new LuaField("m_vInitVelocity", get_m_vInitVelocity, set_m_vInitVelocity),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_BodyThrowCatch", typeof(PlayerState_BodyThrowCatch), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_BodyThrowCatch(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_BodyThrowCatch obj = new PlayerState_BodyThrowCatch(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_BodyThrowCatch.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_BodyThrowCatch);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dirThrow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dirThrow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dirThrow on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_dirThrow);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bSuccess");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bSuccess on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bSuccess);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bCollideBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bCollideBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bCollideBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bCollideBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_vInitPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_vInitPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_vInitPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_vInitPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_vInitVelocity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_vInitVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_vInitVelocity on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_vInitVelocity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_dirThrow(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dirThrow");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dirThrow on a nil value");
			}
		}

		obj.m_dirThrow = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bSuccess");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bSuccess on a nil value");
			}
		}

		obj.m_bSuccess = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bCollideBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bCollideBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bCollideBall on a nil value");
			}
		}

		obj.m_bCollideBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_vInitPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_vInitPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_vInitPos on a nil value");
			}
		}

		obj.m_vInitPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_vInitVelocity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_vInitVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_vInitVelocity on a nil value");
			}
		}

		obj.m_vInitVelocity = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_BodyThrowCatch obj = (PlayerState_BodyThrowCatch)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_BodyThrowCatch");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxDistance(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		IM.Number o = PlayerState_BodyThrowCatch.GetMaxDistance(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}
}

