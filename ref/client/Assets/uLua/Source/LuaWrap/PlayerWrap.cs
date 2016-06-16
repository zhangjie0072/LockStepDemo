using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class PlayerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("RegisterListener", RegisterListener),
			new LuaMethod("RemoveListener", RemoveListener),
			new LuaMethod("RemoveAllListeners", RemoveAllListeners),
			new LuaMethod("BuildFavorSectors", BuildFavorSectors),
			new LuaMethod("FaceTo", FaceTo),
			new LuaMethod("Show", Show),
			new LuaMethod("Hide", Hide),
			new LuaMethod("ShowIndicator", ShowIndicator),
			new LuaMethod("HideIndicator", HideIndicator),
			new LuaMethod("UpdateIndicator", UpdateIndicator),
			new LuaMethod("ShowBallOwnerIndicator", ShowBallOwnerIndicator),
			new LuaMethod("LateUpdate", LateUpdate),
			new LuaMethod("Update", Update),
			new LuaMethod("GrabBall", GrabBall),
			new LuaMethod("DropBall", DropBall),
			new LuaMethod("Move", Move),
			new LuaMethod("MoveTowards", MoveTowards),
			new LuaMethod("IsDefended", IsDefended),
			new LuaMethod("GetDefender", GetDefender),
			new LuaMethod("GetNearestDefender", GetNearestDefender),
			new LuaMethod("CanDunk", CanDunk),
			new LuaMethod("CanLayup", CanLayup),
			new LuaMethod("GetSkillAttribute", GetSkillAttribute),
			new LuaMethod("GetSkillSpecialAttribute", GetSkillSpecialAttribute),
			new LuaMethod("CanRebound", CanRebound),
			new LuaMethod("InitAnimStateMachine", InitAnimStateMachine),
			new LuaMethod("CreateAnimation", CreateAnimation),
			new LuaMethod("PlayAnimation", PlayAnimation),
			new LuaMethod("BuildGameData", BuildGameData),
			new LuaMethod("OnLostBall", OnLostBall),
			new LuaMethod("Build", Build),
			new LuaMethod("Release", Release),
			new LuaMethod("OnMatchStateChange", OnMatchStateChange),
			new LuaMethod("GetQuality", GetQuality),
			new LuaMethod("SetQuality", SetQuality),
			new LuaMethod("EnhanceAttr", EnhanceAttr),
			new LuaMethod("TransformNodePosition", TransformNodePosition),
			new LuaMethod("GetNodePosition", GetNodePosition),
			new LuaMethod("New", _CreatePlayer),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mStatistics", get_mStatistics, set_mStatistics),
			new LuaField("mMovements", get_mMovements, set_mMovements),
			new LuaField("m_gender", get_m_gender, set_m_gender),
			new LuaField("m_teamName", get_m_teamName, set_m_teamName),
			new LuaField("m_position", get_m_position, set_m_position),
			new LuaField("m_roleInfo", get_m_roleInfo, set_m_roleInfo),
			new LuaField("m_moveType", get_m_moveType, set_m_moveType),
			new LuaField("m_enableMovement", get_m_enableMovement, set_m_enableMovement),
			new LuaField("m_lastReceiveShootTime", get_m_lastReceiveShootTime, set_m_lastReceiveShootTime),
			new LuaField("m_config", get_m_config, set_m_config),
			new LuaField("m_defTargetSwitched", get_m_defTargetSwitched, set_m_defTargetSwitched),
			new LuaField("m_defenseTarget", get_m_defenseTarget, set_m_defenseTarget),
			new LuaField("m_attrData", get_m_attrData, set_m_attrData),
			new LuaField("m_bForceShoot", get_m_bForceShoot, set_m_bForceShoot),
			new LuaField("m_bToDirectShoot", get_m_bToDirectShoot, set_m_bToDirectShoot),
			new LuaField("m_bToCatch", get_m_bToCatch, set_m_bToCatch),
			new LuaField("m_passTarget", get_m_passTarget, set_m_passTarget),
			new LuaField("m_bMovedWithBall", get_m_bMovedWithBall, set_m_bMovedWithBall),
			new LuaField("m_speedPassBall", get_m_speedPassBall, set_m_speedPassBall),
			new LuaField("m_dunkDistance", get_m_dunkDistance, set_m_dunkDistance),
			new LuaField("m_LayupDistance", get_m_LayupDistance, set_m_LayupDistance),
			new LuaField("m_startPos", get_m_startPos, set_m_startPos),
			new LuaField("m_eHandWithBall", get_m_eHandWithBall, set_m_eHandWithBall),
			new LuaField("m_bSimulator", get_m_bSimulator, set_m_bSimulator),
			new LuaField("m_bOnGround", get_m_bOnGround, set_m_bOnGround),
			new LuaField("m_catchHelper", get_m_catchHelper, set_m_catchHelper),
			new LuaField("m_alwaysForbiddenPickup", get_m_alwaysForbiddenPickup, set_m_alwaysForbiddenPickup),
			new LuaField("m_enablePickupDetector", get_m_enablePickupDetector, set_m_enablePickupDetector),
			new LuaField("rootLocalRotation", get_rootLocalRotation, set_rootLocalRotation),
			new LuaField("rootLocalPos", get_rootLocalPos, set_rootLocalPos),
			new LuaField("rootPos", get_rootPos, set_rootPos),
			new LuaField("ballSocketLocalPos", get_ballSocketLocalPos, set_ballSocketLocalPos),
			new LuaField("ballSocketPos", get_ballSocketPos, set_ballSocketPos),
			new LuaField("rHandLocalPos", get_rHandLocalPos, set_rHandLocalPos),
			new LuaField("rHandPos", get_rHandPos, set_rHandPos),
			new LuaField("pelvisLocalPos", get_pelvisLocalPos, set_pelvisLocalPos),
			new LuaField("pelvisPos", get_pelvisPos, set_pelvisPos),
			new LuaField("m_aiMgr", get_m_aiMgr, set_m_aiMgr),
			new LuaField("m_aiAssist", get_m_aiAssist, set_m_aiAssist),
			new LuaField("positionFavorSectors", get_positionFavorSectors, set_positionFavorSectors),
			new LuaField("positionBounceSectors", get_positionBounceSectors, set_positionBounceSectors),
			new LuaField("m_inputDispatcher", get_m_inputDispatcher, set_m_inputDispatcher),
			new LuaField("m_curInputDir", get_m_curInputDir, set_m_curInputDir),
			new LuaField("m_AOD", get_m_AOD, set_m_AOD),
			new LuaField("m_InfoVisualizer", get_m_InfoVisualizer, set_m_InfoVisualizer),
			new LuaField("m_animAttributes", get_m_animAttributes, set_m_animAttributes),
			new LuaField("model", get_model, set_model),
			new LuaField("moveCtrl", get_moveCtrl, set_moveCtrl),
			new LuaField("m_takenSectorRanges", get_m_takenSectorRanges, set_m_takenSectorRanges),
			new LuaField("animMgr", get_animMgr, set_animMgr),
			new LuaField("m_blockable", get_m_blockable, set_m_blockable),
			new LuaField("shootStrength", get_shootStrength, set_shootStrength),
			new LuaField("m_lostBallContext", get_m_lostBallContext, set_m_lostBallContext),
			new LuaField("m_smcManager", get_m_smcManager, set_m_smcManager),
			new LuaField("m_toTakeOver", get_m_toTakeOver, set_m_toTakeOver),
			new LuaField("m_takingOver", get_m_takingOver, set_m_takingOver),
			new LuaField("m_applyLogicPostion", get_m_applyLogicPostion, set_m_applyLogicPostion),
			new LuaField("m_team", get_m_team, set_m_team),
			new LuaField("m_id", get_m_id, null),
			new LuaField("m_name", get_m_name, set_m_name),
			new LuaField("m_shapeID", get_m_shapeID, null),
			new LuaField("m_matchPosition", get_m_matchPosition, set_m_matchPosition),
			new LuaField("m_roomPosId", get_m_roomPosId, null),
			new LuaField("m_enableAction", get_m_enableAction, set_m_enableAction),
			new LuaField("m_dir", get_m_dir, set_m_dir),
			new LuaField("m_bIsNPC", get_m_bIsNPC, null),
			new LuaField("moveDirection", get_moveDirection, set_moveDirection),
			new LuaField("position", get_position, set_position),
			new LuaField("forward", get_forward, set_forward),
			new LuaField("up", get_up, null),
			new LuaField("right", get_right, null),
			new LuaField("rotation", get_rotation, set_rotation),
			new LuaField("scale", get_scale, set_scale),
			new LuaField("m_finalAttrs", get_m_finalAttrs, null),
			new LuaField("m_fightingCapacity", get_m_fightingCapacity, null),
			new LuaField("m_fReboundDist", get_m_fReboundDist, null),
			new LuaField("m_toSkillInstance", get_m_toSkillInstance, set_m_toSkillInstance),
			new LuaField("m_stamina", get_m_stamina, null),
			new LuaField("m_bWithBall", get_m_bWithBall, null),
			new LuaField("m_ball", get_m_ball, null),
			new LuaField("m_bNative", get_m_bNative, set_m_bNative),
			new LuaField("m_StateMachine", get_m_StateMachine, null),
			new LuaField("eventHandler", get_eventHandler, null),
			new LuaField("m_collider", get_m_collider, null),
			new LuaField("m_pickupDetector", get_m_pickupDetector, null),
			new LuaField("m_moveCollider", get_m_moveCollider, null),
			new LuaField("m_favorSectors", get_m_favorSectors, null),
			new LuaField("m_bounceSectors", get_m_bounceSectors, null),
			new LuaField("m_bIsAI", get_m_bIsAI, null),
			new LuaField("gameObject", get_gameObject, null),
			new LuaField("transform", get_transform, null),
			new LuaField("m_skillSystem", get_m_skillSystem, null),
			new LuaField("m_moveHelper", get_m_moveHelper, null),
			new LuaField("mSparkEffect", get_mSparkEffect, null),
		};

		LuaScriptMgr.RegisterLib(L, "Player", typeof(Player), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			fogs.proto.msg.RoleInfo arg0 = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 1, typeof(fogs.proto.msg.RoleInfo));
			Team arg1 = (Team)LuaScriptMgr.GetNetObject(L, 2, typeof(Team));
			Player obj = new Player(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Player.New");
		}

		return 0;
	}

	static Type classType = typeof(Player);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mStatistics(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mStatistics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mStatistics on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mStatistics);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mMovements(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mMovements");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mMovements on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.mMovements);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gender on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_gender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_teamName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_teamName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_teamName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_teamName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_position on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_roleInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_roleInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_roleInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_roleInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_moveType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_moveType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_moveType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_moveType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_enableMovement(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enableMovement");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enableMovement on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_enableMovement);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_lastReceiveShootTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lastReceiveShootTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lastReceiveShootTime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_lastReceiveShootTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_config(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_config");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_config on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_config);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_defTargetSwitched(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defTargetSwitched");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defTargetSwitched on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_defTargetSwitched);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_defenseTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defenseTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defenseTarget on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_defenseTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_attrData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_attrData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_attrData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_attrData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bForceShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bForceShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bForceShoot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bForceShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bToDirectShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToDirectShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToDirectShoot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bToDirectShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bToCatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToCatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToCatch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bToCatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_passTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_passTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_passTarget on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_passTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bMovedWithBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bMovedWithBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bMovedWithBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bMovedWithBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_speedPassBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_speedPassBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_speedPassBall on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_speedPassBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dunkDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dunkDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dunkDistance on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_dunkDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_LayupDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_LayupDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_LayupDistance on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_LayupDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_startPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_startPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_startPos on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_startPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_eHandWithBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_eHandWithBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_eHandWithBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_eHandWithBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bSimulator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bSimulator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bSimulator on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bSimulator);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bOnGround(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bOnGround");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bOnGround on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bOnGround);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_catchHelper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_catchHelper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_catchHelper on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_catchHelper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_alwaysForbiddenPickup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_alwaysForbiddenPickup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_alwaysForbiddenPickup on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_alwaysForbiddenPickup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_enablePickupDetector(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enablePickupDetector");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enablePickupDetector on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_enablePickupDetector);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootLocalRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootLocalRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootLocalRotation on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rootLocalRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootLocalPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rootLocalPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rootPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ballSocketLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ballSocketLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ballSocketLocalPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.ballSocketLocalPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ballSocketPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ballSocketPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ballSocketPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.ballSocketPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rHandLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rHandLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rHandLocalPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rHandLocalPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rHandPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rHandPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rHandPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rHandPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pelvisLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pelvisLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pelvisLocalPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.pelvisLocalPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pelvisPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pelvisPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pelvisPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.pelvisPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_aiMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_aiMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_aiMgr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_aiMgr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_aiAssist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_aiAssist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_aiAssist on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_aiAssist);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_positionFavorSectors(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Player.positionFavorSectors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_positionBounceSectors(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Player.positionBounceSectors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_inputDispatcher(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_inputDispatcher");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_inputDispatcher on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_inputDispatcher);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_curInputDir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curInputDir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curInputDir on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_curInputDir);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_AOD(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_AOD");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_AOD on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_AOD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_InfoVisualizer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_InfoVisualizer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_InfoVisualizer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_InfoVisualizer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_animAttributes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_animAttributes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_animAttributes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_animAttributes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_model(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name model");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index model on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.model);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_moveCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moveCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moveCtrl on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.moveCtrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_takenSectorRanges(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_takenSectorRanges");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_takenSectorRanges on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_takenSectorRanges);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animMgr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.animMgr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_blockable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_blockable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_blockable on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_blockable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shootStrength(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootStrength");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootStrength on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.shootStrength);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_lostBallContext(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lostBallContext");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lostBallContext on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_lostBallContext);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_smcManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_smcManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_smcManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_smcManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_toTakeOver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toTakeOver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toTakeOver on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_toTakeOver);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_takingOver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_takingOver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_takingOver on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_takingOver);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_applyLogicPostion(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_applyLogicPostion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_applyLogicPostion on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_applyLogicPostion);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_team(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_team");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_team on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_team);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_shapeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_shapeID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_shapeID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_shapeID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_matchPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_matchPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_matchPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_matchPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_roomPosId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_roomPosId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_roomPosId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_roomPosId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_enableAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enableAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enableAction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_enableAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dir on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_dir);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bIsNPC(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bIsNPC");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bIsNPC on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bIsNPC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_moveDirection(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moveDirection");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moveDirection on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.moveDirection);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forward(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.forward);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_up(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name up");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index up on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.up);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_right(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index right on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scale on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.scale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_finalAttrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_finalAttrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_finalAttrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_finalAttrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_fightingCapacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fightingCapacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fightingCapacity on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_fightingCapacity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_fReboundDist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fReboundDist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fReboundDist on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_fReboundDist);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_toSkillInstance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toSkillInstance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toSkillInstance on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_toSkillInstance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_stamina(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_stamina");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_stamina on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_stamina);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bWithBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bWithBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bWithBall on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bWithBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ball(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ball");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ball on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_ball);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bNative(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bNative");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bNative on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bNative);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_StateMachine(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_StateMachine");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_StateMachine on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_StateMachine);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventHandler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.eventHandler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_collider(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_collider");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_collider on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_collider);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_pickupDetector(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_pickupDetector");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_pickupDetector on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_pickupDetector);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_moveCollider(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_moveCollider");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_moveCollider on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_moveCollider);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_favorSectors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_favorSectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_favorSectors on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_favorSectors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bounceSectors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bounceSectors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bounceSectors on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_bounceSectors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bIsAI(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bIsAI");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bIsAI on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bIsAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameObject(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameObject on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gameObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transform(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transform on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.transform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_skillSystem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_skillSystem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_skillSystem on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_skillSystem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_moveHelper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_moveHelper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_moveHelper on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_moveHelper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mSparkEffect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mSparkEffect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mSparkEffect on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mSparkEffect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mStatistics(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mStatistics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mStatistics on a nil value");
			}
		}

		obj.mStatistics = (PlayerStatistics)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerStatistics));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mMovements(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mMovements");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mMovements on a nil value");
			}
		}

		obj.mMovements = LuaScriptMgr.GetArrayObject<PlayerMovement>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gender on a nil value");
			}
		}

		obj.m_gender = (fogs.proto.msg.GenderType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GenderType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_teamName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_teamName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_teamName on a nil value");
			}
		}

		obj.m_teamName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_position on a nil value");
			}
		}

		obj.m_position = (fogs.proto.msg.PositionType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PositionType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_roleInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_roleInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_roleInfo on a nil value");
			}
		}

		obj.m_roleInfo = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RoleInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_moveType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_moveType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_moveType on a nil value");
			}
		}

		obj.m_moveType = (fogs.proto.msg.MoveType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.MoveType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_enableMovement(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enableMovement");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enableMovement on a nil value");
			}
		}

		obj.m_enableMovement = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_lastReceiveShootTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lastReceiveShootTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lastReceiveShootTime on a nil value");
			}
		}

		obj.m_lastReceiveShootTime = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_config(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_config");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_config on a nil value");
			}
		}

		obj.m_config = (GameMatch.Config.TeamMember)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Config.TeamMember));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_defTargetSwitched(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defTargetSwitched");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defTargetSwitched on a nil value");
			}
		}

		obj.m_defTargetSwitched = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_defenseTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_defenseTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_defenseTarget on a nil value");
			}
		}

		obj.m_defenseTarget = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_attrData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_attrData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_attrData on a nil value");
			}
		}

		obj.m_attrData = (AttrData)LuaScriptMgr.GetNetObject(L, 3, typeof(AttrData));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bForceShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bForceShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bForceShoot on a nil value");
			}
		}

		obj.m_bForceShoot = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bToDirectShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToDirectShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToDirectShoot on a nil value");
			}
		}

		obj.m_bToDirectShoot = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bToCatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bToCatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bToCatch on a nil value");
			}
		}

		obj.m_bToCatch = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_passTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_passTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_passTarget on a nil value");
			}
		}

		obj.m_passTarget = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bMovedWithBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bMovedWithBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bMovedWithBall on a nil value");
			}
		}

		obj.m_bMovedWithBall = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_speedPassBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_speedPassBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_speedPassBall on a nil value");
			}
		}

		obj.m_speedPassBall = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_dunkDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dunkDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dunkDistance on a nil value");
			}
		}

		obj.m_dunkDistance = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_LayupDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_LayupDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_LayupDistance on a nil value");
			}
		}

		obj.m_LayupDistance = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_startPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_startPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_startPos on a nil value");
			}
		}

		obj.m_startPos = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.FightStatus));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_eHandWithBall(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_eHandWithBall");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_eHandWithBall on a nil value");
			}
		}

		obj.m_eHandWithBall = (Player.HandWithBall)LuaScriptMgr.GetNetObject(L, 3, typeof(Player.HandWithBall));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bSimulator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bSimulator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bSimulator on a nil value");
			}
		}

		obj.m_bSimulator = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bOnGround(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bOnGround");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bOnGround on a nil value");
			}
		}

		obj.m_bOnGround = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_catchHelper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_catchHelper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_catchHelper on a nil value");
			}
		}

		obj.m_catchHelper = (CatchHelper)LuaScriptMgr.GetNetObject(L, 3, typeof(CatchHelper));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_alwaysForbiddenPickup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_alwaysForbiddenPickup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_alwaysForbiddenPickup on a nil value");
			}
		}

		obj.m_alwaysForbiddenPickup = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_enablePickupDetector(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enablePickupDetector");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enablePickupDetector on a nil value");
			}
		}

		obj.m_enablePickupDetector = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootLocalRotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootLocalRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootLocalRotation on a nil value");
			}
		}

		obj.rootLocalRotation = (IM.Quaternion)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Quaternion));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootLocalPos on a nil value");
			}
		}

		obj.rootLocalPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootPos on a nil value");
			}
		}

		obj.rootPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ballSocketLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ballSocketLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ballSocketLocalPos on a nil value");
			}
		}

		obj.ballSocketLocalPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ballSocketPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ballSocketPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ballSocketPos on a nil value");
			}
		}

		obj.ballSocketPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rHandLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rHandLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rHandLocalPos on a nil value");
			}
		}

		obj.rHandLocalPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rHandPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rHandPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rHandPos on a nil value");
			}
		}

		obj.rHandPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pelvisLocalPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pelvisLocalPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pelvisLocalPos on a nil value");
			}
		}

		obj.pelvisLocalPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pelvisPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pelvisPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pelvisPos on a nil value");
			}
		}

		obj.pelvisPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_aiMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_aiMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_aiMgr on a nil value");
			}
		}

		obj.m_aiMgr = (AISystem)LuaScriptMgr.GetNetObject(L, 3, typeof(AISystem));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_aiAssist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_aiAssist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_aiAssist on a nil value");
			}
		}

		obj.m_aiAssist = (AISystem_Assist)LuaScriptMgr.GetNetObject(L, 3, typeof(AISystem_Assist));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_positionFavorSectors(IntPtr L)
	{
		Player.positionFavorSectors = (Dictionary<fogs.proto.msg.PositionType,RoadPathManager.SectorArea>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<fogs.proto.msg.PositionType,RoadPathManager.SectorArea>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_positionBounceSectors(IntPtr L)
	{
		Player.positionBounceSectors = (Dictionary<fogs.proto.msg.PositionType,RoadPathManager.SectorArea>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<fogs.proto.msg.PositionType,RoadPathManager.SectorArea>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_inputDispatcher(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_inputDispatcher");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_inputDispatcher on a nil value");
			}
		}

		obj.m_inputDispatcher = (InputDispatcher)LuaScriptMgr.GetNetObject(L, 3, typeof(InputDispatcher));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_curInputDir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_curInputDir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_curInputDir on a nil value");
			}
		}

		obj.m_curInputDir = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_AOD(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_AOD");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_AOD on a nil value");
			}
		}

		obj.m_AOD = (AOD)LuaScriptMgr.GetNetObject(L, 3, typeof(AOD));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_InfoVisualizer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_InfoVisualizer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_InfoVisualizer on a nil value");
			}
		}

		obj.m_InfoVisualizer = (PlayerInfoVisualizer)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerInfoVisualizer));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_animAttributes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_animAttributes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_animAttributes on a nil value");
			}
		}

		obj.m_animAttributes = (PlayerAnimAttribute)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerAnimAttribute));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_model(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name model");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index model on a nil value");
			}
		}

		obj.model = (PlayerModel)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayerModel));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_moveCtrl(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moveCtrl");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moveCtrl on a nil value");
			}
		}

		obj.moveCtrl = (MoveController)LuaScriptMgr.GetNetObject(L, 3, typeof(MoveController));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_takenSectorRanges(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_takenSectorRanges");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_takenSectorRanges on a nil value");
			}
		}

		obj.m_takenSectorRanges = (List<Vector2>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Vector2>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animMgr on a nil value");
			}
		}

		obj.animMgr = (AnimationManager)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationManager));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_blockable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_blockable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_blockable on a nil value");
			}
		}

		obj.m_blockable = (Blockable)LuaScriptMgr.GetNetObject(L, 3, typeof(Blockable));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shootStrength(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shootStrength");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shootStrength on a nil value");
			}
		}

		obj.shootStrength = (ShootStrength)LuaScriptMgr.GetNetObject(L, 3, typeof(ShootStrength));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_lostBallContext(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_lostBallContext");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_lostBallContext on a nil value");
			}
		}

		obj.m_lostBallContext = (LostBallContext)LuaScriptMgr.GetNetObject(L, 3, typeof(LostBallContext));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_smcManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_smcManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_smcManager on a nil value");
			}
		}

		obj.m_smcManager = (SimulateCommandManager)LuaScriptMgr.GetNetObject(L, 3, typeof(SimulateCommandManager));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_toTakeOver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toTakeOver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toTakeOver on a nil value");
			}
		}

		obj.m_toTakeOver = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_takingOver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_takingOver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_takingOver on a nil value");
			}
		}

		obj.m_takingOver = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_applyLogicPostion(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_applyLogicPostion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_applyLogicPostion on a nil value");
			}
		}

		obj.m_applyLogicPostion = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_team(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_team");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_team on a nil value");
			}
		}

		obj.m_team = (Team)LuaScriptMgr.GetNetObject(L, 3, typeof(Team));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_name on a nil value");
			}
		}

		obj.m_name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_matchPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_matchPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_matchPosition on a nil value");
			}
		}

		obj.m_matchPosition = (fogs.proto.msg.PositionType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.PositionType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_enableAction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_enableAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_enableAction on a nil value");
			}
		}

		obj.m_enableAction = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_dir(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dir");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dir on a nil value");
			}
		}

		obj.m_dir = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_moveDirection(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moveDirection");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moveDirection on a nil value");
			}
		}

		obj.moveDirection = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		obj.position = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_forward(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}

		obj.forward = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rotation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}

		obj.rotation = (IM.Quaternion)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Quaternion));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scale on a nil value");
			}
		}

		obj.scale = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_toSkillInstance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_toSkillInstance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_toSkillInstance on a nil value");
			}
		}

		obj.m_toSkillInstance = (SkillInstance)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillInstance));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bNative(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Player obj = (Player)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bNative");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bNative on a nil value");
			}
		}

		obj.m_bNative = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		Player.PlayerBuildListener arg0 = (Player.PlayerBuildListener)LuaScriptMgr.GetNetObject(L, 2, typeof(Player.PlayerBuildListener));
		obj.RegisterListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		Player.PlayerBuildListener arg0 = (Player.PlayerBuildListener)LuaScriptMgr.GetNetObject(L, 2, typeof(Player.PlayerBuildListener));
		obj.RemoveListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAllListeners(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.RemoveAllListeners();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BuildFavorSectors(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Player.BuildFavorSectors();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FaceTo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		obj.FaceTo(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Show(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Show(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Hide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.Hide();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowIndicator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.ShowIndicator(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HideIndicator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.HideIndicator();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateIndicator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		Color arg0 = LuaScriptMgr.GetColor(L, 2);
		obj.UpdateIndicator(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowBallOwnerIndicator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.ShowBallOwnerIndicator(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LateUpdate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			obj.LateUpdate();
			return 0;
		}
		else if (count == 2)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			obj.LateUpdate(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Player.LateUpdate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			obj.Update();
			return 0;
		}
		else if (count == 2)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			obj.Update(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Player.Update");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GrabBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.GrabBall(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DropBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.DropBall(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Move(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		obj.Move(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MoveTowards(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Vector3 arg3 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 5, typeof(IM.Vector3));
		obj.MoveTowards(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDefended(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			bool o = obj.IsDefended();
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			bool o = obj.IsDefended(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
			bool o = obj.IsDefended(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Player.IsDefended");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefender(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			Player o = obj.GetDefender();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 2)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			Player o = obj.GetDefender(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
			Player o = obj.GetDefender(arg0,arg1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Player.GetDefender");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNearestDefender(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		Player o = obj.GetNearestDefender();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		bool o = obj.CanDunk();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanLayup(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		bool o = obj.CanLayup();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillAttribute(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		SkillInstance arg0 = (SkillInstance)LuaScriptMgr.GetNetObject(L, 2, typeof(SkillInstance));
		Dictionary<string,uint> o = obj.GetSkillAttribute(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillSpecialAttribute(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		SkillSpecParam arg0 = (SkillSpecParam)LuaScriptMgr.GetNetObject(L, 2, typeof(SkillSpecParam));
		SkillInstance arg1 = (SkillInstance)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillInstance));
		SkillSpec o = obj.GetSkillSpecialAttribute(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanRebound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		bool o = obj.CanRebound(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitAnimStateMachine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.InitAnimStateMachine();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateAnimation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		bool o = obj.CreateAnimation(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayAnimation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.PlayAnimation(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BuildGameData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.BuildGameData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLostBall(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.OnLostBall();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Build(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.Build();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		obj.Release();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMatchStateChange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		MatchState arg1 = (MatchState)LuaScriptMgr.GetNetObject(L, 3, typeof(MatchState));
		obj.OnMatchStateChange(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		uint o = obj.GetQuality();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetQuality(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnhanceAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.EnhanceAttr(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TransformNodePosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		SampleNode arg0 = (SampleNode)LuaScriptMgr.GetNetObject(L, 2, typeof(SampleNode));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		IM.Vector3 o = obj.TransformNodePosition(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNodePosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		Player obj = (Player)LuaScriptMgr.GetNetObjectSelf(L, 1, "Player");
		SampleNode arg0 = (SampleNode)LuaScriptMgr.GetNetObject(L, 2, typeof(SampleNode));
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Vector3 arg3;
		bool o = obj.GetNodePosition(arg0,arg1,arg2,out arg3);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.PushValue(L, arg3);
		return 2;
	}
}

