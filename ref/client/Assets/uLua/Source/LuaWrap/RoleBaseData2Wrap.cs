using System;
using System.Collections.Generic;
using LuaInterface;

public class RoleBaseData2Wrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateRoleBaseData2),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("display", get_display, set_display),
			new LuaField("bias", get_bias, set_bias),
			new LuaField("position", get_position, set_position),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("icon_bust", get_icon_bust, set_icon_bust),
			new LuaField("icon_bg", get_icon_bg, set_icon_bg),
			new LuaField("shape", get_shape, set_shape),
			new LuaField("name", get_name, set_name),
			new LuaField("intro", get_intro, set_intro),
			new LuaField("specialityInfo", get_specialityInfo, set_specialityInfo),
			new LuaField("gender", get_gender, set_gender),
			new LuaField("init_star", get_init_star, set_init_star),
			new LuaField("talent", get_talent, set_talent),
			new LuaField("talent_value", get_talent_value, set_talent_value),
			new LuaField("training_slots", get_training_slots, set_training_slots),
			new LuaField("training_skill_all", get_training_skill_all, set_training_skill_all),
			new LuaField("training_skill_show", get_training_skill_show, set_training_skill_show),
			new LuaField("special_skills", get_special_skills, set_special_skills),
			new LuaField("special_skill", get_special_skill, set_special_skill),
			new LuaField("special_attr", get_special_attr, set_special_attr),
			new LuaField("recruit_consume", get_recruit_consume, set_recruit_consume),
			new LuaField("recruit_consume_string", get_recruit_consume_string, set_recruit_consume_string),
			new LuaField("recruit_output_id", get_recruit_output_id, set_recruit_output_id),
			new LuaField("recruit_output_value", get_recruit_output_value, set_recruit_output_value),
			new LuaField("access_way", get_access_way, set_access_way),
			new LuaField("goodAt", get_goodAt, set_goodAt),
			new LuaField("init_anim", get_init_anim, set_init_anim),
			new LuaField("display_effect", get_display_effect, set_display_effect),
			new LuaField("other_anims", get_other_anims, set_other_anims),
			new LuaField("player_sound", get_player_sound, set_player_sound),
			new LuaField("attrs", get_attrs, set_attrs),
			new LuaField("attrs_symbol", get_attrs_symbol, set_attrs_symbol),
			new LuaField("match_msg_ids", get_match_msg_ids, set_match_msg_ids),
		};

		LuaScriptMgr.RegisterLib(L, "RoleBaseData2", typeof(RoleBaseData2), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleBaseData2(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RoleBaseData2 obj = new RoleBaseData2();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RoleBaseData2.New");
		}

		return 0;
	}

	static Type classType = typeof(RoleBaseData2);

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
		RoleBaseData2 obj = (RoleBaseData2)o;

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
	static int get_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.display);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bias(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bias on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bias);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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

		LuaScriptMgr.Push(L, obj.position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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
	static int get_icon_bust(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_bust");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_bust on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon_bust);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon_bg(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_bg");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_bg on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon_bg);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shape(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shape");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shape on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shape);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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
	static int get_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intro on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.intro);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_specialityInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name specialityInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index specialityInfo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.specialityInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gender on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_init_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name init_star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index init_star on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.init_star);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_talent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.talent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_talent_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talent_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talent_value on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.talent_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_training_slots(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_slots");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_slots on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.training_slots);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_training_skill_all(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_skill_all");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_skill_all on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.training_skill_all);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_training_skill_show(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_skill_show");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_skill_show on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.training_skill_show);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_special_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_skills on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.special_skills);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_special_skill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_skill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_skill on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.special_skill);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_special_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_attr on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.special_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recruit_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_consume on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.recruit_consume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recruit_consume_string(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_consume_string");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_consume_string on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.recruit_consume_string);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recruit_output_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_output_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_output_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.recruit_output_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recruit_output_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_output_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_output_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.recruit_output_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_access_way(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.access_way);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_goodAt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goodAt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goodAt on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.goodAt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_init_anim(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name init_anim");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index init_anim on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.init_anim);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_display_effect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_effect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_effect on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.display_effect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_other_anims(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name other_anims");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index other_anims on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.other_anims);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_sound on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.player_sound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrs_symbol(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs_symbol");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs_symbol on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.attrs_symbol);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_match_msg_ids(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name match_msg_ids");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index match_msg_ids on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.match_msg_ids);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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

		obj.id = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display on a nil value");
			}
		}

		obj.display = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bias(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bias");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bias on a nil value");
			}
		}

		obj.bias = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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

		obj.position = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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
	static int set_icon_bust(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_bust");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_bust on a nil value");
			}
		}

		obj.icon_bust = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon_bg(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon_bg");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon_bg on a nil value");
			}
		}

		obj.icon_bg = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shape(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shape");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shape on a nil value");
			}
		}

		obj.shape = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

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
	static int set_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intro on a nil value");
			}
		}

		obj.intro = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_specialityInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name specialityInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index specialityInfo on a nil value");
			}
		}

		obj.specialityInfo = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gender on a nil value");
			}
		}

		obj.gender = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_init_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name init_star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index init_star on a nil value");
			}
		}

		obj.init_star = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_talent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talent on a nil value");
			}
		}

		obj.talent = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_talent_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name talent_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index talent_value on a nil value");
			}
		}

		obj.talent_value = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_training_slots(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_slots");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_slots on a nil value");
			}
		}

		obj.training_slots = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_training_skill_all(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_skill_all");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_skill_all on a nil value");
			}
		}

		obj.training_skill_all = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_training_skill_show(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name training_skill_show");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index training_skill_show on a nil value");
			}
		}

		obj.training_skill_show = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_special_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_skills on a nil value");
			}
		}

		obj.special_skills = (List<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<int>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_special_skill(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_skill");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_skill on a nil value");
			}
		}

		obj.special_skill = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_special_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_attr on a nil value");
			}
		}

		obj.special_attr = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recruit_consume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_consume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_consume on a nil value");
			}
		}

		obj.recruit_consume = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recruit_consume_string(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_consume_string");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_consume_string on a nil value");
			}
		}

		obj.recruit_consume_string = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recruit_output_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_output_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_output_id on a nil value");
			}
		}

		obj.recruit_output_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recruit_output_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recruit_output_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recruit_output_value on a nil value");
			}
		}

		obj.recruit_output_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_access_way(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way on a nil value");
			}
		}

		obj.access_way = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_goodAt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goodAt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goodAt on a nil value");
			}
		}

		obj.goodAt = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_init_anim(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name init_anim");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index init_anim on a nil value");
			}
		}

		obj.init_anim = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_display_effect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name display_effect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index display_effect on a nil value");
			}
		}

		obj.display_effect = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_other_anims(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name other_anims");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index other_anims on a nil value");
			}
		}

		obj.other_anims = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_sound on a nil value");
			}
		}

		obj.player_sound = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs on a nil value");
			}
		}

		obj.attrs = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrs_symbol(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrs_symbol");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrs_symbol on a nil value");
			}
		}

		obj.attrs_symbol = (Dictionary<string,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_match_msg_ids(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleBaseData2 obj = (RoleBaseData2)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name match_msg_ids");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index match_msg_ids on a nil value");
			}
		}

		obj.match_msg_ids = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

