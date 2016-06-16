using System;
using LuaInterface;

public class fogs_proto_msg_PlayerDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_PlayerData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("acc_id", get_acc_id, set_acc_id),
			new LuaField("name", get_name, set_name),
			new LuaField("level", get_level, set_level),
			new LuaField("roles", get_roles, null),
			new LuaField("is_room_master", get_is_room_master, set_is_room_master),
			new LuaField("is_room_ready", get_is_room_ready, set_is_room_ready),
			new LuaField("is_home_field", get_is_home_field, set_is_home_field),
			new LuaField("equipments", get_equipments, null),
			new LuaField("squad", get_squad, null),
			new LuaField("role_map", get_role_map, null),
			new LuaField("winning_streak", get_winning_streak, set_winning_streak),
			new LuaField("fashion_items", get_fashion_items, null),
			new LuaField("badge_info", get_badge_info, set_badge_info),
			new LuaField("score", get_score, set_score),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.PlayerData", typeof(fogs.proto.msg.PlayerData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_PlayerData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.PlayerData obj = new fogs.proto.msg.PlayerData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.PlayerData.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.PlayerData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_acc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name acc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index acc_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.acc_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roles on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.roles);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_room_master(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_master");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_master on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_room_master);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_room_ready(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_ready");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_ready on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_room_ready);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_home_field(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_home_field");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_home_field on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_home_field);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equipments(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipments");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipments on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.equipments);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_squad(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name squad");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index squad on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.squad);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role_map(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_map");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_map on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.role_map);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winning_streak on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.winning_streak);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fashion_items(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashion_items");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashion_items on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fashion_items);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_badge_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badge_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badge_info on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.badge_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_acc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name acc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index acc_id on a nil value");
			}
		}

		obj.acc_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		obj.name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		obj.level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_room_master(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_master");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_master on a nil value");
			}
		}

		obj.is_room_master = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_room_ready(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_ready");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_ready on a nil value");
			}
		}

		obj.is_room_ready = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_home_field(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_home_field");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_home_field on a nil value");
			}
		}

		obj.is_home_field = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_winning_streak(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winning_streak");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winning_streak on a nil value");
			}
		}

		obj.winning_streak = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_badge_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badge_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badge_info on a nil value");
			}
		}

		obj.badge_info = (fogs.proto.msg.BadgeInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.BadgeInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.PlayerData obj = (fogs.proto.msg.PlayerData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		obj.score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

