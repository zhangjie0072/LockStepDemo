using System;
using System.Collections.Generic;
using LuaInterface;

public class BullFightLevelWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateBullFightLevel),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("hard", get_hard, set_hard),
			new LuaField("unlock_level", get_unlock_level, set_unlock_level),
			new LuaField("win_hp_cost", get_win_hp_cost, set_win_hp_cost),
			new LuaField("lose_hp_cost", get_lose_hp_cost, set_lose_hp_cost),
			new LuaField("reward_id", get_reward_id, set_reward_id),
			new LuaField("npc", get_npc, set_npc),
		};

		LuaScriptMgr.RegisterLib(L, "BullFightLevel", typeof(BullFightLevel), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBullFightLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BullFightLevel obj = new BullFightLevel();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BullFightLevel.New");
		}

		return 0;
	}

	static Type classType = typeof(BullFightLevel);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hard on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlock_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unlock_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_win_hp_cost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name win_hp_cost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index win_hp_cost on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.win_hp_cost);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lose_hp_cost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lose_hp_cost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lose_hp_cost on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lose_hp_cost);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reward_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reward_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reward_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.reward_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.npc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hard on a nil value");
			}
		}

		obj.hard = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlock_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_level on a nil value");
			}
		}

		obj.unlock_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_win_hp_cost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name win_hp_cost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index win_hp_cost on a nil value");
			}
		}

		obj.win_hp_cost = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lose_hp_cost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lose_hp_cost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lose_hp_cost on a nil value");
			}
		}

		obj.lose_hp_cost = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reward_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reward_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reward_id on a nil value");
			}
		}

		obj.reward_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_npc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BullFightLevel obj = (BullFightLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc on a nil value");
			}
		}

		obj.npc = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

