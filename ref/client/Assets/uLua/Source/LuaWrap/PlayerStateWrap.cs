using System;
using System.Collections.Generic;
using LuaInterface;

public class PlayerStateWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PreEnter", PreEnter),
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("OnFaceToBasket", OnFaceToBasket),
			new LuaMethod("Update", Update),
			new LuaMethod("OnLeaveGround", OnLeaveGround),
			new LuaMethod("OnGround", OnGround),
			new LuaMethod("LateUpdate", LateUpdate),
			new LuaMethod("IsCommandValid", IsCommandValid),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_rotateTo", get_m_rotateTo, set_m_rotateTo),
			new LuaField("m_rotateFreeDir", get_m_rotateFreeDir, set_m_rotateFreeDir),
			new LuaField("m_relAngle", get_m_relAngle, set_m_relAngle),
			new LuaField("m_rotateType", get_m_rotateType, set_m_rotateType),
			new LuaField("m_lstActionId", get_m_lstActionId, set_m_lstActionId),
			new LuaField("m_speed", get_m_speed, set_m_speed),
			new LuaField("m_accelerate", get_m_accelerate, set_m_accelerate),
			new LuaField("m_turningSpeed", get_m_turningSpeed, set_m_turningSpeed),
			new LuaField("m_onActionDone", get_m_onActionDone, set_m_onActionDone),
			new LuaField("m_curExecSkill", get_m_curExecSkill, set_m_curExecSkill),
			new LuaField("m_animType", get_m_animType, set_m_animType),
			new LuaField("m_eState", get_m_eState, null),
			new LuaField("m_player", get_m_player, null),
			new LuaField("curAction", get_curAction, null),
			new LuaField("time", get_time, null),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState", typeof(PlayerState), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState obj = new PlayerState(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_rotateTo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateTo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateTo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_rotateTo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_rotateFreeDir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateFreeDir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateFreeDir on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_rotateFreeDir);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_relAngle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_relAngle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_relAngle on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_relAngle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_rotateType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_rotateType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_lstActionId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lstActionId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lstActionId on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_lstActionId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_speed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_speed on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_speed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_accelerate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_accelerate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_accelerate on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_accelerate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_turningSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_turningSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_turningSpeed on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_turningSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_onActionDone(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_onActionDone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_onActionDone on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_onActionDone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_curExecSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curExecSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curExecSkill on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_curExecSkill);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_animType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_animType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_animType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_animType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_eState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_eState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_eState on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_eState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_player(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_player");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_player on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_player);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curAction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.curAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_rotateTo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateTo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateTo on a nil value");
			}
		}

		obj.m_rotateTo = (RotateTo)LuaScriptMgr.GetNetObject(L, 3, typeof(RotateTo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_rotateFreeDir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateFreeDir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateFreeDir on a nil value");
			}
		}

		obj.m_rotateFreeDir = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_relAngle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_relAngle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_relAngle on a nil value");
			}
		}

		obj.m_relAngle = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_rotateType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rotateType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rotateType on a nil value");
			}
		}

		obj.m_rotateType = (RotateType)LuaScriptMgr.GetNetObject(L, 3, typeof(RotateType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_lstActionId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lstActionId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lstActionId on a nil value");
			}
		}

		obj.m_lstActionId = (List<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<int>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_speed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_speed on a nil value");
			}
		}

		obj.m_speed = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_accelerate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_accelerate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_accelerate on a nil value");
			}
		}

		obj.m_accelerate = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_turningSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_turningSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_turningSpeed on a nil value");
			}
		}

		obj.m_turningSpeed = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_onActionDone(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_onActionDone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_onActionDone on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.m_onActionDone = (PlayerState.OnActionDone)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerState.OnActionDone));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.m_onActionDone = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_curExecSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curExecSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curExecSkill on a nil value");
			}
		}

		obj.m_curExecSkill = (SkillInstance)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillInstance));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_animType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState obj = (PlayerState)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_animType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_animType on a nil value");
			}
		}

		obj.m_animType = (fogs.proto.msg.AnimType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.AnimType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PreEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		bool o = obj.PreEnter();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFaceToBasket(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		obj.OnFaceToBasket();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLeaveGround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		obj.OnLeaveGround();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		obj.OnGround();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.LateUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCommandValid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		Command arg0 = (Command)LuaScriptMgr.GetNetObject(L, 2, typeof(Command));
		bool o = obj.IsCommandValid(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState obj = (PlayerState)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState");
		obj.OnExit();
		return 0;
	}
}

