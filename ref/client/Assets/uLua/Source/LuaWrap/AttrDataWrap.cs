using System;
using System.Collections.Generic;
using LuaInterface;

public class AttrDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateAttrData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("level", get_level, set_level),
			new LuaField("quality", get_quality, set_quality),
			new LuaField("bias", get_bias, set_bias),
			new LuaField("talent", get_talent, set_talent),
			new LuaField("fightingCapacity", get_fightingCapacity, set_fightingCapacity),
			new LuaField("skills", get_skills, set_skills),
			new LuaField("attrs", get_attrs, set_attrs),
		};

		LuaScriptMgr.RegisterLib(L, "AttrData", typeof(AttrData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAttrData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AttrData obj = new AttrData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AttrData.New");
		}

		return 0;
	}

	static Type classType = typeof(AttrData);

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
		AttrData obj = (AttrData)o;

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
	static int get_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bias(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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
	static int get_talent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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
	static int get_fightingCapacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fightingCapacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fightingCapacity on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.fightingCapacity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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
	static int get_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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
	static int set_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		obj.quality = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bias(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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

		obj.bias = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_talent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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

		obj.talent = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fightingCapacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fightingCapacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fightingCapacity on a nil value");
			}
		}

		obj.fightingCapacity = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skills(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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

		obj.skills = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrData obj = (AttrData)o;

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

		obj.attrs = (Dictionary<string,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,uint>));
		return 0;
	}
}

