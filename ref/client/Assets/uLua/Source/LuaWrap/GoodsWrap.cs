using System;
using System.Collections.Generic;
using LuaInterface;

public class GoodsWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("GetUUID", GetUUID),
			new LuaMethod("getTimeLeft", getTimeLeft),
			new LuaMethod("decreaseTimeLeft", decreaseTimeLeft),
			new LuaMethod("isUsed", isUsed),
			new LuaMethod("GetID", GetID),
			new LuaMethod("GetName", GetName),
			new LuaMethod("GetSubName", GetSubName),
			new LuaMethod("GetIcon", GetIcon),
			new LuaMethod("GetCategory", GetCategory),
			new LuaMethod("GetSubCategory", GetSubCategory),
			new LuaMethod("GetBadgeCategory", GetBadgeCategory),
			new LuaMethod("GetFashionSubCategory", GetFashionSubCategory),
			new LuaMethod("GetNum", GetNum),
			new LuaMethod("SetNum", SetNum),
			new LuaMethod("GetLevel", GetLevel),
			new LuaMethod("SetLevel", SetLevel),
			new LuaMethod("GetQuality", GetQuality),
			new LuaMethod("SetQuality", SetQuality),
			new LuaMethod("IsEquip", IsEquip),
			new LuaMethod("Equip", Equip),
			new LuaMethod("Unequip", Unequip),
			new LuaMethod("GetExp", GetExp),
			new LuaMethod("SetExp", SetExp),
			new LuaMethod("CanUser", CanUser),
			new LuaMethod("CanSell", CanSell),
			new LuaMethod("GetSuitID", GetSuitID),
			new LuaMethod("IsSuit", IsSuit),
			new LuaMethod("IsInjectExp", IsInjectExp),
			new LuaMethod("GetFashionAtlas", GetFashionAtlas),
			new LuaMethod("GetFashionAttrIDList", GetFashionAttrIDList),
			new LuaMethod("New", _CreateGoods),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "Goods", typeof(Goods), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGoods(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Goods obj = new Goods();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Goods.New");
		}

		return 0;
	}

	static Type classType = typeof(Goods);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.GoodsProto arg0 = (fogs.proto.msg.GoodsProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GoodsProto));
		obj.Init(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUUID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		ulong o = obj.GetUUID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getTimeLeft(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.getTimeLeft();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int decreaseTimeLeft(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		obj.decreaseTimeLeft();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isUsed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.isUsed();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.GetID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		string o = obj.GetName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSubName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		string o = obj.GetSubName();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIcon(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		string o = obj.GetIcon();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCategory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.GoodsCategory o = obj.GetCategory();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSubCategory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.EquipmentType o = obj.GetSubCategory();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeCategory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.BadgeCG o = obj.GetBadgeCategory();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionSubCategory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		List<uint> o = obj.GetFashionSubCategory();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.GetNum();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetNum(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.GetLevel();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetLevel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.GoodsQuality o = obj.GetQuality();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetQuality(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		fogs.proto.msg.GoodsQuality arg0 = (fogs.proto.msg.GoodsQuality)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GoodsQuality));
		obj.SetQuality(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsEquip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.IsEquip();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		obj.Equip();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Unequip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		obj.Unequip();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.GetExp();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.SetExp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanUser(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.CanUser();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanSell(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.CanSell();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSuitID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint o = obj.GetSuitID();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsSuit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.IsSuit();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInjectExp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		bool o = obj.IsInjectExp();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionAtlas(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetFashionAtlas(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionAttrIDList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Goods obj = (Goods)LuaScriptMgr.GetNetObjectSelf(L, 1, "Goods");
		List<uint> o = obj.GetFashionAttrIDList();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

