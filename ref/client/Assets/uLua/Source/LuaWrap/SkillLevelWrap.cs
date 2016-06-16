using System;
using System.Collections.Generic;
using LuaInterface;

public class SkillLevelWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateSkillLevel),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("level", get_level, set_level),
			new LuaField("consumables", get_consumables, set_consumables),
			new LuaField("additional_attrs", get_additional_attrs, set_additional_attrs),
			new LuaField("stama", get_stama, set_stama),
			new LuaField("weight", get_weight, set_weight),
			new LuaField("parameters", get_parameters, set_parameters),
		};

		LuaScriptMgr.RegisterLib(L, "SkillLevel", typeof(SkillLevel), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SkillLevel obj = new SkillLevel();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SkillLevel.New");
		}

		return 0;
	}

	static Type classType = typeof(SkillLevel);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

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
	static int get_consumables(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumables");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumables on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.consumables);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_additional_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additional_attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additional_attrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.additional_attrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stama(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stama");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stama on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.stama);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index weight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.weight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parameters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameters on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.parameters);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

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
	static int set_consumables(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumables");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumables on a nil value");
			}
		}

		obj.consumables = (List<SkillConsumable>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<SkillConsumable>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_additional_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additional_attrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additional_attrs on a nil value");
			}
		}

		obj.additional_attrs = (Dictionary<string,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stama(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stama");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stama on a nil value");
			}
		}

		obj.stama = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index weight on a nil value");
			}
		}

		obj.weight = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_parameters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillLevel obj = (SkillLevel)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameters on a nil value");
			}
		}

		obj.parameters = (Dictionary<uint,SkillSpec>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,SkillSpec>));
		return 0;
	}
}

