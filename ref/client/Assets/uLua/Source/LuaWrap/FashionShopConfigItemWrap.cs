using System;
using System.Collections.Generic;
using LuaInterface;

public class FashionShopConfigItemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("getGender", getGender),
			new LuaMethod("getFashionType", getFashionType),
			new LuaMethod("getAtlas", getAtlas),
			new LuaMethod("isForver", isForver),
			new LuaMethod("isMine", isMine),
			new LuaMethod("getGood", getGood),
			new LuaMethod("isInDate", isInDate),
			new LuaMethod("isUsed", isUsed),
			new LuaMethod("isDressOn", isDressOn),
			new LuaMethod("getActualTimeDur", getActualTimeDur),
			new LuaMethod("getActualDayDur", getActualDayDur),
			new LuaMethod("getActuallyCost", getActuallyCost),
			new LuaMethod("getSpriteName", getSpriteName),
			new LuaMethod("getTimeLeftStr", getTimeLeftStr),
			new LuaMethod("getName", getName),
			new LuaMethod("New", _CreateFashionShopConfigItem),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("_fashionID", get__fashionID, set__fashionID),
			new LuaField("_timeDur", get__timeDur, set__timeDur),
			new LuaField("_costType", get__costType, set__costType),
			new LuaField("_costNum", get__costNum, set__costNum),
			new LuaField("_isDiscount", get__isDiscount, set__isDiscount),
			new LuaField("_discountCost", get__discountCost, set__discountCost),
			new LuaField("_isNew", get__isNew, set__isNew),
			new LuaField("_sortValue", get__sortValue, set__sortValue),
			new LuaField("_vip", get__vip, set__vip),
			new LuaField("_renewTimeDur", get__renewTimeDur, set__renewTimeDur),
			new LuaField("_renewCostType", get__renewCostType, set__renewCostType),
			new LuaField("_renewCostNum", get__renewCostNum, set__renewCostNum),
		};

		LuaScriptMgr.RegisterLib(L, "FashionShopConfigItem", typeof(FashionShopConfigItem), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFashionShopConfigItem(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FashionShopConfigItem obj = new FashionShopConfigItem();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionShopConfigItem.New");
		}

		return 0;
	}

	static Type classType = typeof(FashionShopConfigItem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__fashionID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _fashionID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _fashionID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._fashionID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__timeDur(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _timeDur");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _timeDur on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._timeDur);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__costType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _costType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _costType on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._costType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__costNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _costNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _costNum on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._costNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__isDiscount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isDiscount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isDiscount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._isDiscount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__discountCost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _discountCost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _discountCost on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._discountCost);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__isNew(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isNew");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isNew on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._isNew);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__sortValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _sortValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _sortValue on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._sortValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _vip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._vip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__renewTimeDur(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewTimeDur");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewTimeDur on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._renewTimeDur);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__renewCostType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewCostType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewCostType on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._renewCostType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__renewCostNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewCostNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewCostNum on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._renewCostNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__fashionID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _fashionID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _fashionID on a nil value");
			}
		}

		obj._fashionID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__timeDur(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _timeDur");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _timeDur on a nil value");
			}
		}

		obj._timeDur = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__costType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _costType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _costType on a nil value");
			}
		}

		obj._costType = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__costNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _costNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _costNum on a nil value");
			}
		}

		obj._costNum = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__isDiscount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isDiscount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isDiscount on a nil value");
			}
		}

		obj._isDiscount = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__discountCost(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _discountCost");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _discountCost on a nil value");
			}
		}

		obj._discountCost = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__isNew(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _isNew");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _isNew on a nil value");
			}
		}

		obj._isNew = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__sortValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _sortValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _sortValue on a nil value");
			}
		}

		obj._sortValue = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__vip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _vip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _vip on a nil value");
			}
		}

		obj._vip = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__renewTimeDur(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewTimeDur");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewTimeDur on a nil value");
			}
		}

		obj._renewTimeDur = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__renewCostType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewCostType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewCostType on a nil value");
			}
		}

		obj._renewCostType = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__renewCostNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _renewCostNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _renewCostNum on a nil value");
			}
		}

		obj._renewCostNum = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getGender(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
			uint o = obj.getGender();
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(uint)))
		{
			uint arg0 = (uint)LuaDLL.lua_tonumber(L, 1);
			uint o = FashionShopConfigItem.getGender(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionShopConfigItem.getGender");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getFashionType(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
			List<uint> o = obj.getFashionType();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(uint)))
		{
			uint arg0 = (uint)LuaDLL.lua_tonumber(L, 1);
			List<uint> o = FashionShopConfigItem.getFashionType(arg0);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionShopConfigItem.getFashionType");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getAtlas(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		string o = obj.getAtlas();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isForver(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		bool o = obj.isForver();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isMine(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		bool o = obj.isMine();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getGood(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		Goods o = obj.getGood();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isInDate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		bool o = obj.isInDate();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isUsed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		bool o = obj.isUsed();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isDressOn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		bool o = obj.isDressOn();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getActualTimeDur(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		List<uint> o = obj.getActualTimeDur();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getActualDayDur(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		List<uint> o = obj.getActualDayDur();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getActuallyCost(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		uint arg1;
		uint o = obj.getActuallyCost(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getSpriteName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		string o = obj.getSpriteName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getTimeLeftStr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		string o = obj.getTimeLeftStr();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionShopConfigItem obj = (FashionShopConfigItem)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionShopConfigItem");
		string o = obj.getName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

