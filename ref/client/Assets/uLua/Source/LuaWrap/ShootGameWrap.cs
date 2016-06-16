using System;
using LuaInterface;

public class ShootGameWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateShootGame),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("times", get_times, set_times),
			new LuaField("level_low", get_level_low, set_level_low),
			new LuaField("level_high", get_level_high, set_level_high),
			new LuaField("npc_level", get_npc_level, set_npc_level),
			new LuaField("basic", get_basic, set_basic),
			new LuaField("hedging", get_hedging, set_hedging),
			new LuaField("game_mode_id", get_game_mode_id, set_game_mode_id),
			new LuaField("score_level", get_score_level, set_score_level),
			new LuaField("rewards_num", get_rewards_num, set_rewards_num),
			new LuaField("reward_id", get_reward_id, set_reward_id),
		};

		LuaScriptMgr.RegisterLib(L, "ShootGame", typeof(ShootGame), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateShootGame(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ShootGame obj = new ShootGame();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ShootGame.New");
		}

		return 0;
	}

	static Type classType = typeof(ShootGame);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_low(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_low");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_low on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_low);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_high(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_high");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_high on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_high);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npc_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.npc_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_basic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name basic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index basic on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.basic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hedging(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedging on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hedging);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_game_mode_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name game_mode_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index game_mode_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.game_mode_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.score_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rewards_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rewards_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rewards_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rewards_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reward_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

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
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		obj.times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_low(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_low");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_low on a nil value");
			}
		}

		obj.level_low = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_high(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_high");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_high on a nil value");
			}
		}

		obj.level_high = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_npc_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc_level on a nil value");
			}
		}

		obj.npc_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_basic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name basic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index basic on a nil value");
			}
		}

		obj.basic = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hedging(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedging on a nil value");
			}
		}

		obj.hedging = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_game_mode_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name game_mode_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index game_mode_id on a nil value");
			}
		}

		obj.game_mode_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score_level on a nil value");
			}
		}

		obj.score_level = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rewards_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rewards_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rewards_num on a nil value");
			}
		}

		obj.rewards_num = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reward_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ShootGame obj = (ShootGame)o;

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
}

