using System;
using LuaInterface;

public class fogs_proto_config_RankConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_RankConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("rank_type", get_rank_type, set_rank_type),
			new LuaField("rank_sub_type", get_rank_sub_type, set_rank_sub_type),
			new LuaField("type", get_type, set_type),
			new LuaField("sub_type", get_sub_type, set_sub_type),
			new LuaField("default_display", get_default_display, set_default_display),
			new LuaField("points_name", get_points_name, set_points_name),
			new LuaField("limit_condition", get_limit_condition, set_limit_condition),
			new LuaField("max_item", get_max_item, set_max_item),
			new LuaField("max_enable_item", get_max_enable_item, set_max_enable_item),
			new LuaField("refresh_time", get_refresh_time, set_refresh_time),
			new LuaField("click_refresh", get_click_refresh, set_click_refresh),
			new LuaField("display_vip", get_display_vip, set_display_vip),
			new LuaField("display_win_times", get_display_win_times, set_display_win_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.RankConfig", typeof(fogs.proto.config.RankConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_RankConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.RankConfig obj = new fogs.proto.config.RankConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.RankConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.RankConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rank_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rank_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rank_sub_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_sub_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_sub_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rank_sub_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sub_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sub_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_default_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name default_display");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index default_display on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.default_display);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_points_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name points_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index points_name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.points_name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_limit_condition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limit_condition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limit_condition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.limit_condition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_item(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_item");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_item on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_item);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_enable_item(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_enable_item");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_enable_item on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_enable_item);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_refresh_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name refresh_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index refresh_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.refresh_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_click_refresh(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name click_refresh");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index click_refresh on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.click_refresh);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_display_vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_vip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.display_vip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_display_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_win_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_win_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.display_win_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rank_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_type on a nil value");
			}
		}

		obj.rank_type = (fogs.proto.msg.RankType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RankType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rank_sub_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_sub_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_sub_type on a nil value");
			}
		}

		obj.rank_sub_type = (fogs.proto.msg.RankSubType)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RankSubType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sub_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_type on a nil value");
			}
		}

		obj.sub_type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_default_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name default_display");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index default_display on a nil value");
			}
		}

		obj.default_display = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_points_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name points_name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index points_name on a nil value");
			}
		}

		obj.points_name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_limit_condition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limit_condition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limit_condition on a nil value");
			}
		}

		obj.limit_condition = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_item(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_item");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_item on a nil value");
			}
		}

		obj.max_item = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max_enable_item(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_enable_item");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_enable_item on a nil value");
			}
		}

		obj.max_enable_item = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_refresh_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name refresh_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index refresh_time on a nil value");
			}
		}

		obj.refresh_time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_click_refresh(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name click_refresh");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index click_refresh on a nil value");
			}
		}

		obj.click_refresh = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_display_vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_vip on a nil value");
			}
		}

		obj.display_vip = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_display_win_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.RankConfig obj = (fogs.proto.config.RankConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_win_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_win_times on a nil value");
			}
		}

		obj.display_win_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

