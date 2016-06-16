using System;
using System.Collections.Generic;
using LuaInterface;

public class SkillConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetSkill", GetSkill),
			new LuaMethod("GetSlot", GetSlot),
			new LuaMethod("New", _CreateSkillConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("skills", get_skills, set_skills),
			new LuaField("slots", get_slots, set_slots),
			new LuaField("basic_skills", get_basic_skills, set_basic_skills),
			new LuaField("actions", get_actions, set_actions),
			new LuaField("skillEffectItems", get_skillEffectItems, set_skillEffectItems),
		};

		LuaScriptMgr.RegisterLib(L, "SkillConfig", typeof(SkillConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SkillConfig obj = new SkillConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SkillConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(SkillConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skills on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skills);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_slots(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slots");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slots on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.slots);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_basic_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name basic_skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index basic_skills on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.basic_skills);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_actions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

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
	static int get_skillEffectItems(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillEffectItems");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillEffectItems on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skillEffectItems);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skills on a nil value");
			}
		}

		obj.skills = (Dictionary<uint,SkillAttr>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillAttr>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_slots(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slots");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slots on a nil value");
			}
		}

		obj.slots = (Dictionary<uint,SkillSlot>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillSlot>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_basic_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name basic_skills");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index basic_skills on a nil value");
			}
		}

		obj.basic_skills = (List<SkillAttr>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<SkillAttr>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_actions(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

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

		obj.actions = (Dictionary<uint,SkillAction>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillAction>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skillEffectItems(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConfig obj = (SkillConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillEffectItems");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillEffectItems on a nil value");
			}
		}

		obj.skillEffectItems = (Dictionary<uint,SkillEffect>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillEffect>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SkillConfig obj = (SkillConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkill(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkillConfig obj = (SkillConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		SkillAttr o = obj.GetSkill(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSlot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkillConfig obj = (SkillConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		SkillSlot o = obj.GetSlot(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

