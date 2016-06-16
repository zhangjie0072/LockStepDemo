using System;
using LuaInterface;

public class PlayerState_PickupWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_Pickup),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_bSuccess", get_m_bSuccess, set_m_bSuccess),
			new LuaField("m_ballToPickup", get_m_ballToPickup, set_m_ballToPickup),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Pickup", typeof(PlayerState_Pickup), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Pickup(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Pickup obj = new PlayerState_Pickup(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Pickup.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Pickup);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Pickup obj = (PlayerState_Pickup)o;

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
	static int get_m_ballToPickup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Pickup obj = (PlayerState_Pickup)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballToPickup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballToPickup on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_ballToPickup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Pickup obj = (PlayerState_Pickup)o;

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
	static int set_m_ballToPickup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_Pickup obj = (PlayerState_Pickup)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballToPickup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballToPickup on a nil value");
			}
		}

		obj.m_ballToPickup = (UBasketball)LuaScriptMgr.GetUnityObject(L, 3, typeof(UBasketball));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Pickup obj = (PlayerState_Pickup)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Pickup");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Pickup obj = (PlayerState_Pickup)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Pickup");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Pickup obj = (PlayerState_Pickup)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Pickup");
		obj.OnExit();
		return 0;
	}
}

