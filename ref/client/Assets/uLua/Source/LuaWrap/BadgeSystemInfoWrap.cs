using System;
using System.Collections.Generic;
using LuaInterface;

public class BadgeSystemInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InitBadgeBookData", InitBadgeBookData),
			new LuaMethod("AddBadgeBooks", AddBadgeBooks),
			new LuaMethod("CreateBadgeBook", CreateBadgeBook),
			new LuaMethod("CreateBadgeSlot", CreateBadgeSlot),
			new LuaMethod("RemoveBadgeBook", RemoveBadgeBook),
			new LuaMethod("GetBadgeUseCountInAllSlots", GetBadgeUseCountInAllSlots),
			new LuaMethod("GetBadgeleftNumExceptUsed", GetBadgeleftNumExceptUsed),
			new LuaMethod("GetBadgeLeftNumExceptAllUsed", GetBadgeLeftNumExceptAllUsed),
			new LuaMethod("GetBadgeUseCountInAllBooks", GetBadgeUseCountInAllBooks),
			new LuaMethod("GetBadgeSlotByBookIdAndSlotId", GetBadgeSlotByBookIdAndSlotId),
			new LuaMethod("GetBadgeBookByBookId", GetBadgeBookByBookId),
			new LuaMethod("GetAllBadgeBooks", GetAllBadgeBooks),
			new LuaMethod("GetAllOwnBadgeBooksNum", GetAllOwnBadgeBooksNum),
			new LuaMethod("SavePlayerUseBadgeBookId", SavePlayerUseBadgeBookId),
			new LuaMethod("GetPlayerUserBadgeBookdId", GetPlayerUserBadgeBookdId),
			new LuaMethod("GetBookProvideTotalBadgeLevelByBookId", GetBookProvideTotalBadgeLevelByBookId),
			new LuaMethod("New", _CreateBadgeSystemInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "BadgeSystemInfo", typeof(BadgeSystemInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBadgeSystemInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BadgeSystemInfo obj = new BadgeSystemInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BadgeSystemInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(BadgeSystemInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitBadgeBookData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		List<fogs.proto.msg.BadgeBook> arg0 = (List<fogs.proto.msg.BadgeBook>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<fogs.proto.msg.BadgeBook>));
		obj.InitBadgeBookData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddBadgeBooks(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		fogs.proto.msg.BadgeBook arg0 = (fogs.proto.msg.BadgeBook)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.BadgeBook));
		obj.AddBadgeBooks(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateBadgeBook(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		fogs.proto.msg.BadgeBook o = obj.CreateBadgeBook();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateBadgeSlot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		fogs.proto.msg.BadgeSlot o = obj.CreateBadgeSlot();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveBadgeBook(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.RemoveBadgeBook(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeUseCountInAllSlots(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		int o = obj.GetBadgeUseCountInAllSlots(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeleftNumExceptUsed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		int o = obj.GetBadgeleftNumExceptUsed(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeLeftNumExceptAllUsed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetBadgeLeftNumExceptAllUsed(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeUseCountInAllBooks(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetBadgeUseCountInAllBooks(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeSlotByBookIdAndSlotId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		fogs.proto.msg.BadgeSlot o = obj.GetBadgeSlotByBookIdAndSlotId(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBadgeBookByBookId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.BadgeBook o = obj.GetBadgeBookByBookId(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllBadgeBooks(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		List<fogs.proto.msg.BadgeBook> o = obj.GetAllBadgeBooks();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllOwnBadgeBooksNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint o = obj.GetAllOwnBadgeBooksNum();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SavePlayerUseBadgeBookId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.SavePlayerUseBadgeBookId(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlayerUserBadgeBookdId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint o = obj.GetPlayerUserBadgeBookdId();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBookProvideTotalBadgeLevelByBookId(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BadgeSystemInfo obj = (BadgeSystemInfo)LuaScriptMgr.GetNetObjectSelf(L, 1, "BadgeSystemInfo");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetBookProvideTotalBadgeLevelByBookId(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

