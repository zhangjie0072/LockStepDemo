using System;
using System.Collections.Generic;
using LuaInterface;

public class SkillAttrWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetSkillLevel", GetSkillLevel),
			new LuaMethod("SameAction", SameAction),
			new LuaMethod("New", _CreateSkillAttr),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("intro", get_intro, set_intro),
			new LuaField("cast", get_cast, set_cast),
			new LuaField("type", get_type, set_type),
			new LuaField("subtype", get_subtype, set_subtype),
			new LuaField("action_type", get_action_type, set_action_type),
			new LuaField("positions", get_positions, set_positions),
			new LuaField("roles", get_roles, set_roles),
			new LuaField("area", get_area, set_area),
			new LuaField("condition", get_condition, set_condition),
			new LuaField("attrange", get_attrange, set_attrange),
			new LuaField("levels", get_levels, set_levels),
			new LuaField("actions", get_actions, set_actions),
			new LuaField("equip_conditions", get_equip_conditions, set_equip_conditions),
			new LuaField("side_effects", get_side_effects, set_side_effects),
		};

		LuaScriptMgr.RegisterLib(L, "SkillAttr", typeof(SkillAttr), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillAttr(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SkillAttr obj = new SkillAttr();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SkillAttr.New");
		}

		return 0;
	}

	static Type classType = typeof(SkillAttr);

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
		SkillAttr obj = (SkillAttr)o;

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
		SkillAttr obj = (SkillAttr)o;

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
		SkillAttr obj = (SkillAttr)o;

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
	static int get_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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
	static int get_cast(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cast on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cast);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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
	static int get_subtype(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name subtype");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index subtype on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.subtype);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_action_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name action_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index action_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.action_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_positions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name positions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index positions on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.positions);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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
	static int get_area(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name area");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index area on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.area);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_condition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.condition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrange(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrange");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrange on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.attrange);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_levels(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name levels");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index levels on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.levels);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_actions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name actions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index actions on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.actions);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equip_conditions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equip_conditions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equip_conditions on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.equip_conditions);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_side_effects(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name side_effects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index side_effects on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.side_effects);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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
		SkillAttr obj = (SkillAttr)o;

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
		SkillAttr obj = (SkillAttr)o;

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
	static int set_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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
	static int set_cast(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cast on a nil value");
			}
		}

		obj.cast = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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

		obj.type = (SkillType)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_subtype(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name subtype");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index subtype on a nil value");
			}
		}

		obj.subtype = (SkillSubType)LuaScriptMgr.GetNetObject(L, 3, typeof(SkillSubType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_action_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name action_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index action_type on a nil value");
			}
		}

		obj.action_type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_positions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name positions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index positions on a nil value");
			}
		}

		obj.positions = (List<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<int>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

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

		obj.roles = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_area(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name area");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index area on a nil value");
			}
		}

		obj.area = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_condition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name condition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index condition on a nil value");
			}
		}

		obj.condition = (List<int>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<int>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrange(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrange");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrange on a nil value");
			}
		}

		obj.attrange = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_levels(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name levels");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index levels on a nil value");
			}
		}

		obj.levels = (Dictionary<uint,SkillLevel>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillLevel>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_actions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name actions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index actions on a nil value");
			}
		}

		obj.actions = (List<SkillAction>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<SkillAction>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_equip_conditions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equip_conditions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equip_conditions on a nil value");
			}
		}

		obj.equip_conditions = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_side_effects(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillAttr obj = (SkillAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name side_effects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index side_effects on a nil value");
			}
		}

		obj.side_effects = (Dictionary<int,SkillSideEffect>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<int,SkillSideEffect>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkillAttr obj = (SkillAttr)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillAttr");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		SkillLevel o = obj.GetSkillLevel(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SameAction(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkillAttr obj = (SkillAttr)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillAttr");
		SkillAttr arg0 = (SkillAttr)LuaScriptMgr.GetNetObject(L, 2, typeof(SkillAttr));
		bool o = obj.SameAction(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

