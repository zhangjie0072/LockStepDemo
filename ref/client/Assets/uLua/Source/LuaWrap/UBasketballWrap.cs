using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UBasketballWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetEffect", SetEffect),
			new LuaMethod("RestoreMaterial", RestoreMaterial),
			new LuaMethod("SetSparkMaterial", SetSparkMaterial),
			new LuaMethod("SetInitPos", SetInitPos),
			new LuaMethod("GetPositionInAir", GetPositionInAir),
			new LuaMethod("OnShoot", OnShoot),
			new LuaMethod("OnBeginDunk", OnBeginDunk),
			new LuaMethod("OnDunk", OnDunk),
			new LuaMethod("OnBlock", OnBlock),
			new LuaMethod("OnPass", OnPass),
			new LuaMethod("OnCatch", OnCatch),
			new LuaMethod("OnGrab", OnGrab),
			new LuaMethod("Reset", Reset),
			new LuaMethod("CompleteLastCurve", CompleteLastCurve),
			new LuaMethod("ViewUpdate", ViewUpdate),
			new LuaMethod("GameUpdate", GameUpdate),
			new LuaMethod("ViewLateUpdate", ViewLateUpdate),
			new LuaMethod("New", _CreateUBasketball),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_id", get_m_id, set_m_id),
			new LuaField("m_shootArea", get_m_shootArea, set_m_shootArea),
			new LuaField("m_ballState", get_m_ballState, set_m_ballState),
			new LuaField("m_passer", get_m_passer, set_m_passer),
			new LuaField("m_catcher", get_m_catcher, set_m_catcher),
			new LuaField("m_interceptor", get_m_interceptor, set_m_interceptor),
			new LuaField("m_interceptedPos", get_m_interceptedPos, set_m_interceptedPos),
			new LuaField("m_pickable", get_m_pickable, set_m_pickable),
			new LuaField("m_special", get_m_special, set_m_special),
			new LuaField("m_bGoal", get_m_bGoal, set_m_bGoal),
			new LuaField("m_collidedWithRim", get_m_collidedWithRim, set_m_collidedWithRim),
			new LuaField("m_castedSkill", get_m_castedSkill, set_m_castedSkill),
			new LuaField("m_ballRadius", get_m_ballRadius, set_m_ballRadius),
			new LuaField("onHitGround", get_onHitGround, set_onHitGround),
			new LuaField("onBackboardCollision", get_onBackboardCollision, set_onBackboardCollision),
			new LuaField("onRimCollision", get_onRimCollision, set_onRimCollision),
			new LuaField("onRebound", get_onRebound, set_onRebound),
			new LuaField("onShoot", get_onShoot, set_onShoot),
			new LuaField("onShootGoal", get_onShootGoal, set_onShootGoal),
			new LuaField("onBlock", get_onBlock, set_onBlock),
			new LuaField("onLost", get_onLost, set_onLost),
			new LuaField("onIntercepted", get_onIntercepted, set_onIntercepted),
			new LuaField("onGrab", get_onGrab, set_onGrab),
			new LuaField("onCatch", get_onCatch, set_onCatch),
			new LuaField("onDunk", get_onDunk, set_onDunk),
			new LuaField("m_bBlockSuccess", get_m_bBlockSuccess, set_m_bBlockSuccess),
			new LuaField("m_pt", get_m_pt, set_m_pt),
			new LuaField("m_fTime", get_m_fTime, set_m_fTime),
			new LuaField("m_picker", get_m_picker, set_m_picker),
			new LuaField("initVel", get_initVel, set_initVel),
			new LuaField("curVel", get_curVel, set_curVel),
			new LuaField("m_loseBallSimulator", get_m_loseBallSimulator, set_m_loseBallSimulator),
			new LuaField("m_fAngleSpeed", get_m_fAngleSpeed, set_m_fAngleSpeed),
			new LuaField("m_actor", get_m_actor, null),
			new LuaField("m_owner", get_m_owner, null),
			new LuaField("m_reboundPlacement", get_m_reboundPlacement, null),
			new LuaField("m_bReachMaxHeight", get_m_bReachMaxHeight, null),
			new LuaField("m_bounceCnt", get_m_bounceCnt, null),
			new LuaField("m_shootSolution", get_m_shootSolution, set_m_shootSolution),
			new LuaField("m_isLayup", get_m_isLayup, null),
			new LuaField("m_isDunk", get_m_isDunk, null),
			new LuaField("_position", get__position, set__position),
			new LuaField("position", get_position, set_position),
			new LuaField("initPos", get_initPos, set_initPos),
		};

		LuaScriptMgr.RegisterLib(L, "UBasketball", typeof(UBasketball), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUBasketball(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UBasketball class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UBasketball);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

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
	static int get_m_shootArea(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_shootArea");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_shootArea on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_shootArea);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ballState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballState on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_ballState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_passer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_passer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_passer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_passer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_catcher(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_catcher");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_catcher on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_catcher);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_interceptor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_interceptor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_interceptor on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_interceptor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_interceptedPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_interceptedPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_interceptedPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_interceptedPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_pickable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_pickable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_pickable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_pickable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_special(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_special");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_special on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_special);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bGoal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bGoal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_collidedWithRim(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_collidedWithRim");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_collidedWithRim on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_collidedWithRim);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_castedSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_castedSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_castedSkill on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_castedSkill);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ballRadius(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballRadius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballRadius on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_ballRadius);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onHitGround(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onHitGround");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onHitGround on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onHitGround);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBackboardCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBackboardCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBackboardCollision on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onBackboardCollision);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRimCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRimCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRimCollision on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onRimCollision);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRebound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRebound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRebound on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onRebound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onShoot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onShootGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onShootGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onShootGoal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onShootGoal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBlock(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBlock");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBlock on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onBlock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onLost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLost on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onLost);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onIntercepted(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onIntercepted");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onIntercepted on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onIntercepted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onGrab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGrab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGrab on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onGrab);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCatch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onCatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDunk(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDunk");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDunk on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDunk);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bBlockSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bBlockSuccess");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bBlockSuccess on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bBlockSuccess);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_pt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_pt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_pt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_pt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_fTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fTime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_fTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_picker(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_picker");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_picker on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_picker);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_initVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name initVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index initVel on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.initVel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curVel on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.curVel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_loseBallSimulator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loseBallSimulator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loseBallSimulator on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_loseBallSimulator);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_fAngleSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fAngleSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fAngleSpeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_fAngleSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_actor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_actor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_actor on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_actor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_owner(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_owner");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_owner on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_owner);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_reboundPlacement(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_reboundPlacement");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_reboundPlacement on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_reboundPlacement);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bReachMaxHeight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bReachMaxHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bReachMaxHeight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bReachMaxHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_bounceCnt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bounceCnt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bounceCnt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_bounceCnt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_shootSolution(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_shootSolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_shootSolution on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_shootSolution);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_isLayup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_isLayup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_isLayup on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_isLayup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_isDunk(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_isDunk");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_isDunk on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_isDunk);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _position on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj._position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

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
	static int get_initPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name initPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index initPos on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.initPos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

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

		obj.m_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_shootArea(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_shootArea");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_shootArea on a nil value");
			}
		}

		obj.m_shootArea = (fogs.proto.msg.Area)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.Area));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_ballState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballState on a nil value");
			}
		}

		obj.m_ballState = (BallState)LuaScriptMgr.GetNetObject(L, 3, typeof(BallState));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_passer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_passer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_passer on a nil value");
			}
		}

		obj.m_passer = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_catcher(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_catcher");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_catcher on a nil value");
			}
		}

		obj.m_catcher = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_interceptor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_interceptor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_interceptor on a nil value");
			}
		}

		obj.m_interceptor = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_interceptedPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_interceptedPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_interceptedPos on a nil value");
			}
		}

		obj.m_interceptedPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_pickable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_pickable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_pickable on a nil value");
			}
		}

		obj.m_pickable = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_special(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_special");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_special on a nil value");
			}
		}

		obj.m_special = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bGoal on a nil value");
			}
		}

		obj.m_bGoal = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_collidedWithRim(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_collidedWithRim");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_collidedWithRim on a nil value");
			}
		}

		obj.m_collidedWithRim = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_castedSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_castedSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_castedSkill on a nil value");
			}
		}

		obj.m_castedSkill = (SkillInstance)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillInstance));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_ballRadius(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ballRadius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ballRadius on a nil value");
			}
		}

		obj.m_ballRadius = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onHitGround(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onHitGround");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onHitGround on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onHitGround = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onHitGround = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onBackboardCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBackboardCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBackboardCollision on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onBackboardCollision = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onBackboardCollision = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRimCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRimCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRimCollision on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onRimCollision = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onRimCollision = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRebound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRebound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRebound on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onRebound = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onRebound = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onShoot on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onShoot = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onShoot = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onShootGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onShootGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onShootGoal on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onShootGoal = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onShootGoal = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onBlock(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBlock");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBlock on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onBlock = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onBlock = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onLost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLost on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onLost = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onLost = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onIntercepted(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onIntercepted");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onIntercepted on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onIntercepted = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onIntercepted = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onGrab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGrab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGrab on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onGrab = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onGrab = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onCatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCatch on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onCatch = (UBasketball.BallDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.BallDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onCatch = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDunk(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDunk");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDunk on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDunk = (UBasketball.OnDunkDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasketball.OnDunkDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDunk = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_bBlockSuccess(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_bBlockSuccess");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_bBlockSuccess on a nil value");
			}
		}

		obj.m_bBlockSuccess = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_pt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_pt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_pt on a nil value");
			}
		}

		obj.m_pt = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_fTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fTime on a nil value");
			}
		}

		obj.m_fTime = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_picker(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_picker");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_picker on a nil value");
			}
		}

		obj.m_picker = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_initVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name initVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index initVel on a nil value");
			}
		}

		obj.initVel = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_curVel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curVel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curVel on a nil value");
			}
		}

		obj.curVel = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_loseBallSimulator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loseBallSimulator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loseBallSimulator on a nil value");
			}
		}

		obj.m_loseBallSimulator = (LoseBallSimulator)LuaScriptMgr.GetNetObject(L, 3, typeof(LoseBallSimulator));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_fAngleSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fAngleSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fAngleSpeed on a nil value");
			}
		}

		obj.m_fAngleSpeed = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_shootSolution(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_shootSolution");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_shootSolution on a nil value");
			}
		}

		obj.m_shootSolution = (ShootSolution)LuaScriptMgr.GetNetObject(L, 3, typeof(ShootSolution));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _position on a nil value");
			}
		}

		obj._position = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

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
	static int set_initPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasketball obj = (UBasketball)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name initPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index initPos on a nil value");
			}
		}

		obj.initPos = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEffect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		BallState arg0 = (BallState)LuaScriptMgr.GetNetObject(L, 2, typeof(BallState));
		GameObject arg1 = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		obj.SetEffect(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RestoreMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.RestoreMaterial();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSparkMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		Material arg0 = (Material)LuaScriptMgr.GetUnityObject(L, 2, typeof(Material));
		obj.SetSparkMaterial(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInitPos(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		obj.SetInitPos(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPositionInAir(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		IM.Vector3 arg1;
		bool o = obj.GetPositionInAir(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.PushValue(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		fogs.proto.msg.Area arg1 = (fogs.proto.msg.Area)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.Area));
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		obj.OnShoot(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		obj.OnBeginDunk(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		IM.Vector3 arg2 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Vector3));
		Player arg3 = (Player)LuaScriptMgr.GetNetObject(L, 5, typeof(Player));
		obj.OnDunk(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.OnBlock();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPass(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		Player arg1 = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		IM.Vector3 arg2 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Vector3));
		Player arg3 = (Player)LuaScriptMgr.GetNetObject(L, 5, typeof(Player));
		obj.OnPass(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.OnCatch();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGrab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 2, typeof(Player));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.OnGrab(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.Reset();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompleteLastCurve(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		ShootSolution.SShootCurve o = obj.CompleteLastCurve();
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.ViewUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GameUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.GameUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewLateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UBasketball obj = (UBasketball)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasketball");
		obj.ViewLateUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

