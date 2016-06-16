using System;
using System.Collections.Generic;
using LuaInterface;

public class AttrNameDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateAttrNameData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("type", get_type, set_type),
			new LuaField("symbol", get_symbol, set_symbol),
			new LuaField("name", get_name, set_name),
			new LuaField("display", get_display, set_display),
			new LuaField("recommend", get_recommend, set_recommend),
			new LuaField("is_factor", get_is_factor, set_is_factor),
			new LuaField("fc_weight", get_fc_weight, set_fc_weight),
		};

		LuaScriptMgr.RegisterLib(L, "AttrNameData", typeof(AttrNameData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAttrNameData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AttrNameData obj = new AttrNameData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AttrNameData.New");
		}

		return 0;
	}

	static Type classType = typeof(AttrNameData);

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
		AttrNameData obj = (AttrNameData)o;

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
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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
	static int get_symbol(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name symbol");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index symbol on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.symbol);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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
	static int get_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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
	static int get_recommend(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recommend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recommend on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.recommend);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_factor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_factor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_factor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_factor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fc_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fc_weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fc_weight on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.fc_weight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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

		obj.type = (AttributeType)LuaScriptMgr.GetNetObject(L, 3, typeof(AttributeType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_symbol(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name symbol");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index symbol on a nil value");
			}
		}

		obj.symbol = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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
	static int set_display(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

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

		obj.display = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recommend(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recommend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recommend on a nil value");
			}
		}

		obj.recommend = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_factor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_factor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_factor on a nil value");
			}
		}

		obj.is_factor = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fc_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameData obj = (AttrNameData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fc_weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fc_weight on a nil value");
			}
		}

		obj.fc_weight = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}
}

