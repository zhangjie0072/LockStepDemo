using System;
using LuaInterface;

public class fogs_proto_config_SectionConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_SectionConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("coord_x", get_coord_x, set_coord_x),
			new LuaField("coord_y", get_coord_y, set_coord_y),
			new LuaField("next_section_id", get_next_section_id, set_next_section_id),
			new LuaField("costing", get_costing, set_costing),
			new LuaField("sweep_card", get_sweep_card, set_sweep_card),
			new LuaField("daily_times", get_daily_times, set_daily_times),
			new LuaField("buy_consume", get_buy_consume, set_buy_consume),
			new LuaField("member_need", get_member_need, set_member_need),
			new LuaField("space_need", get_space_need, set_space_need),
			new LuaField("condition_id", get_condition_id, set_condition_id),
			new LuaField("condition_value", get_condition_value, set_condition_value),
			new LuaField("award_id", get_award_id, set_award_id),
			new LuaField("sweep_award_id", get_sweep_award_id, set_sweep_award_id),
			new LuaField("one_star_id", get_one_star_id, set_one_star_id),
			new LuaField("one_star_value", get_one_star_value, set_one_star_value),
			new LuaField("two_star_id", get_two_star_id, set_two_star_id),
			new LuaField("two_star_value", get_two_star_value, set_two_star_value),
			new LuaField("three_star_id", get_three_star_id, set_three_star_id),
			new LuaField("three_star_value", get_three_star_value, set_three_star_value),
			new LuaField("plot_begin_id", get_plot_begin_id, set_plot_begin_id),
			new LuaField("plot_end_id", get_plot_end_id, set_plot_end_id),
			new LuaField("plot_intro", get_plot_intro, set_plot_intro),
			new LuaField("scene", get_scene, set_scene),
			new LuaField("music", get_music, set_music),
			new LuaField("time", get_time, set_time),
			new LuaField("team_side", get_team_side, set_team_side),
			new LuaField("home_score", get_home_score, set_home_score),
			new LuaField("guest_score", get_guest_score, set_guest_score),
			new LuaField("npc_id", get_npc_id, null),
			new LuaField("assistant_id", get_assistant_id, set_assistant_id),
			new LuaField("assistant_level", get_assistant_level, set_assistant_level),
			new LuaField("game_mode_id", get_game_mode_id, set_game_mode_id),
			new LuaField("loading", get_loading, set_loading),
			new LuaField("type", get_type, set_type),
			new LuaField("role_gift", get_role_gift, set_role_gift),
			new LuaField("awards_id", get_awards_id, set_awards_id),
			new LuaField("icon_level", get_icon_level, set_icon_level),
			new LuaField("frame", get_frame, set_frame),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.SectionConfig", typeof(fogs.proto.config.SectionConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_SectionConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.SectionConfig obj = new fogs.proto.config.SectionConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.SectionConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.SectionConfig);

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
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_coord_x(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name coord_x");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index coord_x on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.coord_x);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_coord_y(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name coord_y");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index coord_y on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.coord_y);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_next_section_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name next_section_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index next_section_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.next_section_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_costing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costing on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.costing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sweep_card(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sweep_card");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sweep_card on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sweep_card);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_daily_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name daily_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index daily_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.daily_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_buy_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_consume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.buy_consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_member_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name member_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index member_need on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.member_need);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_space_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name space_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index space_need on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.space_need);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_condition_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.condition_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_condition_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.condition_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_sweep_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sweep_award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sweep_award_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sweep_award_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_one_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name one_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index one_star_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.one_star_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_one_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name one_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index one_star_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.one_star_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_two_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name two_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index two_star_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.two_star_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_two_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name two_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index two_star_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.two_star_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_three_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name three_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index three_star_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.three_star_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_three_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name three_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index three_star_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.three_star_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_plot_begin_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_begin_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_begin_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.plot_begin_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_plot_end_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_end_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_end_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.plot_end_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_plot_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_intro on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.plot_intro);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_music(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name music");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index music on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.music);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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

		LuaScriptMgr.Push(L, obj.time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_team_side(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name team_side");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index team_side on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.team_side);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_home_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name home_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index home_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.home_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_guest_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name guest_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index guest_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.guest_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc_id on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.npc_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assistant_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assistant_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assistant_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.assistant_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_assistant_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assistant_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assistant_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.assistant_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_game_mode_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_loading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loading on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.loading);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int get_role_gift(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_gift");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_gift on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.role_gift);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awards_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awards_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_frame(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frame");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frame on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.frame);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_coord_x(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name coord_x");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index coord_x on a nil value");
			}
		}

		obj.coord_x = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_coord_y(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name coord_y");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index coord_y on a nil value");
			}
		}

		obj.coord_y = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_next_section_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name next_section_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index next_section_id on a nil value");
			}
		}

		obj.next_section_id = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_costing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name costing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index costing on a nil value");
			}
		}

		obj.costing = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sweep_card(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sweep_card");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sweep_card on a nil value");
			}
		}

		obj.sweep_card = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_daily_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name daily_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index daily_times on a nil value");
			}
		}

		obj.daily_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_buy_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_consume on a nil value");
			}
		}

		obj.buy_consume = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_member_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name member_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index member_need on a nil value");
			}
		}

		obj.member_need = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_space_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name space_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index space_need on a nil value");
			}
		}

		obj.space_need = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_condition_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition_id on a nil value");
			}
		}

		obj.condition_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_condition_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition_value on a nil value");
			}
		}

		obj.condition_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_sweep_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sweep_award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sweep_award_id on a nil value");
			}
		}

		obj.sweep_award_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_one_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name one_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index one_star_id on a nil value");
			}
		}

		obj.one_star_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_one_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name one_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index one_star_value on a nil value");
			}
		}

		obj.one_star_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_two_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name two_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index two_star_id on a nil value");
			}
		}

		obj.two_star_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_two_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name two_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index two_star_value on a nil value");
			}
		}

		obj.two_star_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_three_star_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name three_star_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index three_star_id on a nil value");
			}
		}

		obj.three_star_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_three_star_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name three_star_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index three_star_value on a nil value");
			}
		}

		obj.three_star_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_plot_begin_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_begin_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_begin_id on a nil value");
			}
		}

		obj.plot_begin_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_plot_end_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_end_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_end_id on a nil value");
			}
		}

		obj.plot_end_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_plot_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plot_intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plot_intro on a nil value");
			}
		}

		obj.plot_intro = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene on a nil value");
			}
		}

		obj.scene = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_music(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name music");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index music on a nil value");
			}
		}

		obj.music = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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

		obj.time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_team_side(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name team_side");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index team_side on a nil value");
			}
		}

		obj.team_side = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_home_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name home_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index home_score on a nil value");
			}
		}

		obj.home_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_guest_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name guest_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index guest_score on a nil value");
			}
		}

		obj.guest_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assistant_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assistant_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assistant_id on a nil value");
			}
		}

		obj.assistant_id = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_assistant_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assistant_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assistant_level on a nil value");
			}
		}

		obj.assistant_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_game_mode_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_loading(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loading");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loading on a nil value");
			}
		}

		obj.loading = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

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
	static int set_role_gift(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_gift");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_gift on a nil value");
			}
		}

		obj.role_gift = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awards_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards_id on a nil value");
			}
		}

		obj.awards_id = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_level on a nil value");
			}
		}

		obj.icon_level = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_frame(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.SectionConfig obj = (fogs.proto.config.SectionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frame");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frame on a nil value");
			}
		}

		obj.frame = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

