using System;
using LuaInterface;

public class fogs_proto_config_QualifyingNewerConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_QualifyingNewerConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("score", get_score, set_score),
			new LuaField("title", get_title, set_title),
			new LuaField("star", get_star, set_star),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("upgrade_score", get_upgrade_score, set_upgrade_score),
			new LuaField("section", get_section, set_section),
			new LuaField("mail_id", get_mail_id, set_mail_id),
			new LuaField("award_id", get_award_id, set_award_id),
			new LuaField("award_icon", get_award_icon, set_award_icon),
			new LuaField("awardpack_id", get_awardpack_id, set_awardpack_id),
			new LuaField("icon_small", get_icon_small, set_icon_small),
			new LuaField("combo", get_combo, set_combo),
			new LuaField("icon_board", get_icon_board, set_icon_board),
			new LuaField("sub_section", get_sub_section, set_sub_section),
			new LuaField("team_ai", get_team_ai, set_team_ai),
			new LuaField("enemy_ai", get_enemy_ai, set_enemy_ai),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.QualifyingNewerConfig", typeof(fogs.proto.config.QualifyingNewerConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_QualifyingNewerConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.QualifyingNewerConfig obj = new fogs.proto.config.QualifyingNewerConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.QualifyingNewerConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.QualifyingNewerConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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
	static int get_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.title);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.star);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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
	static int get_upgrade_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name upgrade_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index upgrade_score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.upgrade_score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_section(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name section");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index section on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.section);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mail_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mail_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mail_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mail_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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
	static int get_award_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardpack_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardpack_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardpack_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awardpack_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon_small(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_small");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_small on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon_small);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_combo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name combo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index combo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.combo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon_board(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_board");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_board on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon_board);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sub_section(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_section");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_section on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sub_section);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_team_ai(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name team_ai");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index team_ai on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.team_ai);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enemy_ai(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enemy_ai");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enemy_ai on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.enemy_ai);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		obj.title = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		obj.star = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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
	static int set_upgrade_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name upgrade_score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index upgrade_score on a nil value");
			}
		}

		obj.upgrade_score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_section(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name section");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index section on a nil value");
			}
		}

		obj.section = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mail_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mail_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mail_id on a nil value");
			}
		}

		obj.mail_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

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
	static int set_award_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_icon on a nil value");
			}
		}

		obj.award_icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardpack_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardpack_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardpack_id on a nil value");
			}
		}

		obj.awardpack_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon_small(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_small");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_small on a nil value");
			}
		}

		obj.icon_small = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_combo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name combo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index combo on a nil value");
			}
		}

		obj.combo = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon_board(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_board");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_board on a nil value");
			}
		}

		obj.icon_board = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sub_section(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_section");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_section on a nil value");
			}
		}

		obj.sub_section = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_team_ai(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name team_ai");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index team_ai on a nil value");
			}
		}

		obj.team_ai = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enemy_ai(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.QualifyingNewerConfig obj = (fogs.proto.config.QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enemy_ai");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enemy_ai on a nil value");
			}
		}

		obj.enemy_ai = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

