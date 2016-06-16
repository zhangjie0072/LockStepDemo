using System;
using LuaInterface;

public class StoreGoodsDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateStoreGoodsData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("store_good_grid", get_store_good_grid, set_store_good_grid),
			new LuaField("store_good_id", get_store_good_id, set_store_good_id),
			new LuaField("store_good_num", get_store_good_num, set_store_good_num),
			new LuaField("store_good_consume_type", get_store_good_consume_type, set_store_good_consume_type),
			new LuaField("store_good_price", get_store_good_price, set_store_good_price),
			new LuaField("store_goods_weight", get_store_goods_weight, set_store_goods_weight),
			new LuaField("apply_min_level", get_apply_min_level, set_apply_min_level),
			new LuaField("apply_max_level", get_apply_max_level, set_apply_max_level),
			new LuaField("is_sell", get_is_sell, set_is_sell),
		};

		LuaScriptMgr.RegisterLib(L, "StoreGoodsData", typeof(StoreGoodsData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStoreGoodsData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			StoreGoodsData obj = new StoreGoodsData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: StoreGoodsData.New");
		}

		return 0;
	}

	static Type classType = typeof(StoreGoodsData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_good_grid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_grid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_grid on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_good_grid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_good_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_good_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_good_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_good_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_good_consume_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_consume_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_consume_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_good_consume_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_good_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_price on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_good_price);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_store_goods_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_goods_weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_goods_weight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.store_goods_weight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_apply_min_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_min_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_min_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.apply_min_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_apply_max_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_max_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_max_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.apply_max_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_sell(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_sell");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_sell on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_sell);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_good_grid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_grid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_grid on a nil value");
			}
		}

		obj.store_good_grid = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_good_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_id on a nil value");
			}
		}

		obj.store_good_id = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_good_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_num on a nil value");
			}
		}

		obj.store_good_num = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_good_consume_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_consume_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_consume_type on a nil value");
			}
		}

		obj.store_good_consume_type = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_good_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_good_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_good_price on a nil value");
			}
		}

		obj.store_good_price = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_store_goods_weight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name store_goods_weight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index store_goods_weight on a nil value");
			}
		}

		obj.store_goods_weight = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_apply_min_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_min_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_min_level on a nil value");
			}
		}

		obj.apply_min_level = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_apply_max_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apply_max_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apply_max_level on a nil value");
			}
		}

		obj.apply_max_level = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_sell(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StoreGoodsData obj = (StoreGoodsData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_sell");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_sell on a nil value");
			}
		}

		obj.is_sell = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

