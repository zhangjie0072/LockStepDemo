using System;
using LuaInterface;

public class PlayerState_ResultPoseWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_ResultPose),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("pose", get_pose, set_pose),
			new LuaField("withBall", get_withBall, set_withBall),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_ResultPose", typeof(PlayerState_ResultPose), regs, fields, typeof(PlayerState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_ResultPose(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_ResultPose obj = new PlayerState_ResultPose(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_ResultPose.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_ResultPose);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pose(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pose");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pose on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_withBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name withBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index withBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.withBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pose(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pose");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pose on a nil value");
			}
		}

		obj.pose = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_withBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name withBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index withBall on a nil value");
			}
		}

		obj.withBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_ResultPose");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_ResultPose");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_ResultPose obj = (PlayerState_ResultPose)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_ResultPose");
		obj.OnExit();
		return 0;
	}
}

