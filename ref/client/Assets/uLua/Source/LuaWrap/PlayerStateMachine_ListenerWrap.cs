using System;
using LuaInterface;

public class PlayerStateMachine_ListenerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnBeginDunk", OnBeginDunk),
			new LuaMethod("OnDunkLeaveGround", OnDunkLeaveGround),
			new LuaMethod("OnDunk", OnDunk),
			new LuaMethod("OnDunkDone", OnDunkDone),
			new LuaMethod("New", _CreatePlayerStateMachine_Listener),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "PlayerStateMachine.Listener", typeof(PlayerStateMachine.Listener), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerStateMachine_Listener(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PlayerStateMachine.Listener obj = new PlayerStateMachine.Listener();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerStateMachine.Listener.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerStateMachine.Listener);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerStateMachine.Listener obj = (PlayerStateMachine.Listener)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine.Listener");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		obj.OnBeginDunk(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunkLeaveGround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlayerStateMachine.Listener obj = (PlayerStateMachine.Listener)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine.Listener");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		obj.OnDunkLeaveGround(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerStateMachine.Listener obj = (PlayerStateMachine.Listener)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine.Listener");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		obj.OnDunk(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunkDone(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerStateMachine.Listener obj = (PlayerStateMachine.Listener)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerStateMachine.Listener");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		obj.OnDunkDone(arg0);
		return 0;
	}
}

