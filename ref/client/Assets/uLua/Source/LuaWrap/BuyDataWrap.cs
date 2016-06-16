using System;
using LuaInterface;

public class BuyDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateBuyData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("times", get_times, set_times),
			new LuaField("diamond_need", get_diamond_need, set_diamond_need),
			new LuaField("value", get_value, set_value),
			new LuaField("level_min", get_level_min, set_level_min),
			new LuaField("level_max", get_level_max, set_level_max),
		};

		LuaScriptMgr.RegisterLib(L, "BuyData", typeof(BuyData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBuyData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BuyData obj = new BuyData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BuyData.New");
		}

		return 0;
	}

	static Type classType = typeof(BuyData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_diamond_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond_need on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.diamond_need);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_min on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_min);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_max on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_max);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		obj.times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_diamond_need(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond_need");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond_need on a nil value");
			}
		}

		obj.diamond_need = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index value on a nil value");
			}
		}

		obj.value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_min on a nil value");
			}
		}

		obj.level_min = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BuyData obj = (BuyData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_max on a nil value");
			}
		}

		obj.level_max = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

