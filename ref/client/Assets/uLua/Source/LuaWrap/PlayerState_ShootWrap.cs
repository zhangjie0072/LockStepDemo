using System;
using LuaInterface;

public class PlayerState_ShootWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("BeginShoot", BeginShoot),
			new LuaMethod("OnShoot", OnShoot),
			new LuaMethod("New", _CreatePlayerState_Shoot),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_Shoot", typeof(PlayerState_Shoot), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_Shoot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_Shoot obj = new PlayerState_Shoot(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_Shoot.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_Shoot);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Shoot obj = (PlayerState_Shoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Shoot");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Shoot obj = (PlayerState_Shoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Shoot");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Shoot obj = (PlayerState_Shoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Shoot");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_Shoot obj = (PlayerState_Shoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Shoot");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.BeginShoot(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_Shoot obj = (PlayerState_Shoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_Shoot");
		obj.OnShoot();
		return 0;
	}
}

