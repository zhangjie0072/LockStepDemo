using System;
using LuaInterface;

public class PlayerState_PrepareToShootWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPrepareToShoot", OnPrepareToShoot),
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("Update", Update),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("New", _CreatePlayerState_PrepareToShoot),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mCanShoot", get_mCanShoot, set_mCanShoot),
			new LuaField("mCachedShootSkill", get_mCachedShootSkill, set_mCachedShootSkill),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerState_PrepareToShoot", typeof(PlayerState_PrepareToShoot), regs, fields, typeof(PlayerState_Skill));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerState_PrepareToShoot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			PlayerStateMachine arg0 = (PlayerStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(PlayerStateMachine));
			GameMatch arg1 = (GameMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch));
			PlayerState_PrepareToShoot obj = new PlayerState_PrepareToShoot(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerState_PrepareToShoot.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerState_PrepareToShoot);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mCanShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCanShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCanShoot on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mCanShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mCachedShootSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCachedShootSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCachedShootSkill on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mCachedShootSkill);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mCanShoot(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCanShoot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCanShoot on a nil value");
			}
		}

		obj.mCanShoot = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mCachedShootSkill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCachedShootSkill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCachedShootSkill on a nil value");
			}
		}

		obj.mCachedShootSkill = (SkillInstance)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillInstance));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPrepareToShoot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_PrepareToShoot");
		obj.OnPrepareToShoot();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_PrepareToShoot");
		PlayerState arg0 = (PlayerState)LuaScriptMgr.GetNetObject(L, 2, typeof(PlayerState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_PrepareToShoot");
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerState_PrepareToShoot obj = (PlayerState_PrepareToShoot)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerState_PrepareToShoot");
		obj.OnExit();
		return 0;
	}
}

