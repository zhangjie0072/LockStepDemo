using System;
using LuaInterface;

public class PlayerState_ReboundWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnGround", OnGround),
			new LuaMethod("OnRebound", OnRebound),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("GetEventTime", GetEventTime),
			new LuaMethod("GetHeightRange", GetHeightRange),
			new LuaMethod("GetDefaultHeightRange", GetDefaultHeightRange),
			new LuaMethod("GetMaxDist", GetMaxDist),
			new LuaMethod("GetDefaultMaxDist", GetDefaultMaxDist),
			new LuaMethod("New", _CreatePlayerState_Rebound),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_success", get_m_success, set_m_success),
			new LuaField("tooLate", get_tooLate, set_tooLate),
			new LuaField("m_heightScale", get_m_heightScale, set_m_heightScale),
			new LuaField("rootMotionScale", get_rootMotionScale, set_rootMotionScale),
			new LuaField("m_toReboundBall", get_m_toReboundBall, set_m_toReboundBall),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Rebound", typeof(PlayerState_Rebound), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Rebound(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Rebound obj = new PlayerState_Rebound(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Rebound.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Rebound);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_success(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

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
	static int get_tooLate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tooLate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tooLate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tooLate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_heightScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_heightScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_heightScale on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_heightScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootMotionScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootMotionScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootMotionScale on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rootMotionScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_toReboundBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toReboundBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toReboundBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_toReboundBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_success(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

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
	static int set_tooLate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tooLate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tooLate on a nil value");
			}
		}

		obj.tooLate = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_heightScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_heightScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_heightScale on a nil value");
			}
		}

		obj.m_heightScale = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootMotionScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootMotionScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootMotionScale on a nil value");
			}
		}

		obj.rootMotionScale = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_toReboundBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toReboundBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toReboundBall on a nil value");
			}
		}

		obj.m_toReboundBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Rebound obj = (PlayerState_Rebound)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Rebound");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Rebound obj = (PlayerState_Rebound)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Rebound");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Rebound");
		obj.OnGround();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRebound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Rebound");
		obj.OnRebound();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Rebound obj = (PlayerState_Rebound)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Rebound");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEventTime(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			IM.Number o = PlayerState_Rebound.GetEventTime(arg0);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			string arg1 = LuaScriptMgr.GetLuaString(L, 2);
			IM.Number o = PlayerState_Rebound.GetEventTime(arg0,arg1);
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Rebound.GetEventTime");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHeightRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		IM.Number arg1;
		IM.Number arg2;
		SkillInstance arg3 = (SkillInstance)LuaScriptMgr.GetNetObject(L, 4, typeof(SkillInstance));
		PlayerState_Rebound.GetHeightRange(arg0,out arg1,out arg2,arg3);
		LuaScriptMgr.PushValue(L, arg1);
		LuaScriptMgr.PushValue(L, arg2);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefaultHeightRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		IM.Number arg1;
		IM.Number arg2;
		PlayerState_Rebound.GetDefaultHeightRange(arg0,out arg1,out arg2);
		LuaScriptMgr.PushValue(L, arg1);
		LuaScriptMgr.PushValue(L, arg2);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxDist(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		SkillInstance arg1 = (SkillInstance)LuaScriptMgr.GetNetObject(L, 2, typeof(SkillInstance));
		IM.Number o = PlayerState_Rebound.GetMaxDist(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefaultMaxDist(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
		IM.Number o = PlayerState_Rebound.GetDefaultMaxDist(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}
}

