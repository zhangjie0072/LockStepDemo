using System;
using UnityEngine;
using LuaInterface;

public class UIStoreFashionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Create", Create),
			new LuaMethod("Destroy", Destroy),
			new LuaMethod("Refresh", Refresh),
			new LuaMethod("update", update),
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("leftBtnClick", leftBtnClick),
			new LuaMethod("ActionBack", ActionBack),
			new LuaMethod("OnFashionOperationFailed", OnFashionOperationFailed),
			new LuaMethod("OnFashionOperation", OnFashionOperation),
			new LuaMethod("OnBuyStoreGoodsResp", OnBuyStoreGoodsResp),
			new LuaMethod("OnBuyOne", OnBuyOne),
			new LuaMethod("OnClickItem", OnClickItem),
			new LuaMethod("OnFashionChange", OnFashionChange),
			new LuaMethod("getFItem", getFItem),
			new LuaMethod("DressOnUpdate", DressOnUpdate),
			new LuaMethod("OnDressOn", OnDressOn),
			new LuaMethod("New", _CreateUIStoreFashion),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("tempIntance", get_tempIntance, set_tempIntance),
			new LuaField("_goReturn", get__goReturn, set__goReturn),
			new LuaField("_selectGrid", get__selectGrid, set__selectGrid),
			new LuaField("_FDItemMgr", get__FDItemMgr, set__FDItemMgr),
			new LuaField("_fashionSelected", get__fashionSelected, set__fashionSelected),
			new LuaField("UIName", get_UIName, null),
		};

		LuaScriptMgr.RegisterLib(L, "UIStoreFashion", typeof(UIStoreFashion), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIStoreFashion(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UIStoreFashion obj = new UIStoreFashion();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UIStoreFashion.New");
		}

		return 0;
	}

	static Type classType = typeof(UIStoreFashion);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tempIntance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, UIStoreFashion.tempIntance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__goReturn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _goReturn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _goReturn on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._goReturn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__selectGrid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _selectGrid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _selectGrid on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj._selectGrid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__FDItemMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _FDItemMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _FDItemMgr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._FDItemMgr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__fashionSelected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _fashionSelected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _fashionSelected on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj._fashionSelected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UIName(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIStoreFashion.UIName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tempIntance(IntPtr L)
	{
		UIStoreFashion.tempIntance = (UIStoreFashion)LuaScriptMgr.GetNetObject(L, 3, typeof(UIStoreFashion));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__goReturn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _goReturn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _goReturn on a nil value");
			}
		}

		obj._goReturn = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__selectGrid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _selectGrid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _selectGrid on a nil value");
			}
		}

		obj._selectGrid = (UIGrid)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIGrid));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__FDItemMgr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _FDItemMgr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _FDItemMgr on a nil value");
			}
		}

		obj._FDItemMgr = (FDItemMgr)LuaScriptMgr.GetNetObject(L, 3, typeof(FDItemMgr));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__fashionSelected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIStoreFashion obj = (UIStoreFashion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name _fashionSelected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index _fashionSelected on a nil value");
			}
		}

		obj._fashionSelected = (UIStoreFashion.FashionSelected)LuaScriptMgr.GetNetObject(L, 3, typeof(UIStoreFashion.FashionSelected));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Create(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		UIStoreFashion o = UIStoreFashion.Create();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Destroy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		UIStoreFashion.Destroy();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		obj.Refresh();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		obj.update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.Initialize(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int leftBtnClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.leftBtnClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ActionBack(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		obj.ActionBack();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFashionOperationFailed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		obj.OnFashionOperationFailed();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFashionOperation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		fogs.proto.msg.FashionOperationResp arg0 = (fogs.proto.msg.FashionOperationResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.FashionOperationResp));
		obj.OnFashionOperation(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBuyStoreGoodsResp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		fogs.proto.msg.BuyStoreGoodsResp arg0 = (fogs.proto.msg.BuyStoreGoodsResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.BuyStoreGoodsResp));
		obj.OnBuyStoreGoodsResp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBuyOne(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		FashionShopConfigItem arg0 = (FashionShopConfigItem)LuaScriptMgr.GetNetObject(L, 2, typeof(FashionShopConfigItem));
		obj.OnBuyOne(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnClickItem(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.OnClickItem(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnFashionChange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		Goods arg0 = (Goods)LuaScriptMgr.GetNetObject(L, 2, typeof(Goods));
		obj.OnFashionChange(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getFItem(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionShopConfigItem o = obj.getFItem(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DressOnUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.DressOnUpdate(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDressOn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIStoreFashion obj = (UIStoreFashion)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIStoreFashion");
		FashionShopItem arg0 = (FashionShopItem)LuaScriptMgr.GetUnityObject(L, 2, typeof(FashionShopItem));
		obj.OnDressOn(arg0);
		return 0;
	}
}

