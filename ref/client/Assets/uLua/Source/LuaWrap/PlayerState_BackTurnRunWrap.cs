using System;
using LuaInterface;

public class PlayerState_BackTurnRunWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_BackTurnRun),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isTurnLeft", get_isTurnLeft, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_BackTurnRun", typeof(PlayerState_BackTurnRun), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_BackTurnRun(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_BackTurnRun obj = new PlayerState_BackTurnRun(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_BackTurnRun.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_BackTurnRun);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isTurnLeft(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_BackTurnRun obj = (PlayerState_BackTurnRun)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isTurnLeft");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isTurnLeft on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isTurnLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_BackTurnRun obj = (PlayerState_BackTurnRun)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_BackTurnRun");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_BackTurnRun obj = (PlayerState_BackTurnRun)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_BackTurnRun");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_BackTurnRun obj = (PlayerState_BackTurnRun)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_BackTurnRun");
		obj.OnExit();
		return 0;
	}
}

