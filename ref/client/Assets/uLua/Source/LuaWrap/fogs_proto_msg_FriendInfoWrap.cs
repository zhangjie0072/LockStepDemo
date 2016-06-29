using System;
using LuaInterface;

public class fogs_proto_msg_FriendInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_FriendInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("acc_id", get_acc_id, set_acc_id),
			new LuaField("name", get_name, set_name),
			new LuaField("plat_id", get_plat_id, set_plat_id),
			new LuaField("level", get_level, set_level),
			new LuaField("vip_level", get_vip_level, set_vip_level),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("present_flag", get_present_flag, set_present_flag),
			new LuaField("get_flag", get_get_flag, set_get_flag),
			new LuaField("online", get_online, set_online),
			new LuaField("logout_time", get_logout_time, set_logout_time),
			new LuaField("shinwakan", get_shinwakan, set_shinwakan),
			new LuaField("pvp_ladder_score", get_pvp_ladder_score, set_pvp_ladder_score),
			new LuaField("first_win_time", get_first_win_time, set_first_win_time),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FriendInfo", typeof(fogs.proto.msg.FriendInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_FriendInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.FriendInfo obj = new fogs.proto.msg.FriendInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.FriendInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.FriendInfo);

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
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
	static int get_plat_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plat_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plat_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.plat_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
	static int get_vip_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.vip_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_present_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name present_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index present_flag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.present_flag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_get_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_flag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.get_flag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_online(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name online");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index online on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.online);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_logout_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logout_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logout_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.logout_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shinwakan(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shinwakan");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shinwakan on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shinwakan);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pvp_ladder_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp_ladder_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp_ladder_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pvp_ladder_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_first_win_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_win_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_win_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.first_win_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_acc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
	static int set_plat_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plat_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plat_id on a nil value");
			}
		}

		obj.plat_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

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
	static int set_vip_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_level on a nil value");
			}
		}

		obj.vip_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		obj.icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_present_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name present_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index present_flag on a nil value");
			}
		}

		obj.present_flag = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_get_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_flag on a nil value");
			}
		}

		obj.get_flag = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_online(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name online");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index online on a nil value");
			}
		}

		obj.online = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_logout_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logout_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logout_time on a nil value");
			}
		}

		obj.logout_time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shinwakan(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shinwakan");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shinwakan on a nil value");
			}
		}

		obj.shinwakan = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pvp_ladder_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pvp_ladder_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pvp_ladder_score on a nil value");
			}
		}

		obj.pvp_ladder_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_first_win_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FriendInfo obj = (fogs.proto.msg.FriendInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_win_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_win_time on a nil value");
			}
		}

		obj.first_win_time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

