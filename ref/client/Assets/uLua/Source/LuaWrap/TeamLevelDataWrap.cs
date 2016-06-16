using System;
using System.Collections.Generic;
using LuaInterface;

public class TeamLevelDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateTeamLevelData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("award_id", get_award_id, set_award_id),
			new LuaField("flag", get_flag, set_flag),
			new LuaField("exp", get_exp, set_exp),
			new LuaField("max_hp", get_max_hp, set_max_hp),
			new LuaField("add_hp", get_add_hp, set_add_hp),
			new LuaField("max_role_quality", get_max_role_quality, set_max_role_quality),
			new LuaField("max_tattoo", get_max_tattoo, set_max_tattoo),
			new LuaField("max_train", get_max_train, set_max_train),
			new LuaField("max_passive_skill", get_max_passive_skill, set_max_passive_skill),
			new LuaField("unlock_icon", get_unlock_icon, set_unlock_icon),
			new LuaField("unlock_describe", get_unlock_describe, set_unlock_describe),
			new LuaField("link", get_link, set_link),
			new LuaField("subId", get_subId, set_subId),
		};

		LuaScriptMgr.RegisterLib(L, "TeamLevelData", typeof(TeamLevelData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTeamLevelData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TeamLevelData obj = new TeamLevelData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TeamLevelData.New");
		}

		return 0;
	}

	static Type classType = typeof(TeamLevelData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.flag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.exp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_hp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_hp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_add_hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_hp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.add_hp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_role_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_role_quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_role_quality on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_role_quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_tattoo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_tattoo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_tattoo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_tattoo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_train(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_train");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_train on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_train);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_passive_skill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_passive_skill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_passive_skill on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_passive_skill);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlock_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_icon on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.unlock_icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlock_describe(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_describe");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_describe on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.unlock_describe);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_link(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name link");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index link on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.link);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_subId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name subId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index subId on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.subId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		obj.award_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flag on a nil value");
			}
		}

		obj.flag = (short)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp on a nil value");
			}
		}

		obj.exp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_hp on a nil value");
			}
		}

		obj.max_hp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_add_hp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name add_hp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index add_hp on a nil value");
			}
		}

		obj.add_hp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_role_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_role_quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_role_quality on a nil value");
			}
		}

		obj.max_role_quality = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_tattoo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_tattoo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_tattoo on a nil value");
			}
		}

		obj.max_tattoo = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_train(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_train");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_train on a nil value");
			}
		}

		obj.max_train = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_passive_skill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_passive_skill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_passive_skill on a nil value");
			}
		}

		obj.max_passive_skill = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlock_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_icon on a nil value");
			}
		}

		obj.unlock_icon = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlock_describe(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_describe");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_describe on a nil value");
			}
		}

		obj.unlock_describe = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_link(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name link");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index link on a nil value");
			}
		}

		obj.link = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_subId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TeamLevelData obj = (TeamLevelData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name subId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index subId on a nil value");
			}
		}

		obj.subId = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

