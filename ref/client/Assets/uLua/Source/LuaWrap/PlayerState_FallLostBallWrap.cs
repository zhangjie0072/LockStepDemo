using System;
using LuaInterface;

public class PlayerState_FallLostBallWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("New", _CreatePlayerState_FallLostBall),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_step", get_m_step, set_m_step),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_FallLostBall", typeof(PlayerState_FallLostBall), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_FallLostBall(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_FallLostBall obj = new PlayerState_FallLostBall(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_FallLostBall.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_FallLostBall);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_step(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_FallLostBall obj = (PlayerState_FallLostBall)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_step");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_step on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_step);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_step(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_FallLostBall obj = (PlayerState_FallLostBall)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_step");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_step on a nil value");
			}
		}

		obj.m_step = (PlayerState_FallLostBall.Step)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerState_FallLostBall.Step));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_FallLostBall obj = (PlayerState_FallLostBall)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_FallLostBall");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_FallLostBall obj = (PlayerState_FallLostBall)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_FallLostBall");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}
}

