using System;
using LuaInterface;

public class fogs_proto_msg_StoreTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("ST_BLACK", GetST_BLACK),
		new LuaMethod("ST_SKILL", GetST_SKILL),
		new LuaMethod("ST_FASHION", GetST_FASHION),
		new LuaMethod("ST_HONOR", GetST_HONOR),
		new LuaMethod("ST_TOUR", GetST_TOUR),
		new LuaMethod("ST_REPUTATION", GetST_REPUTATION),
		new LuaMethod("ST_EXP", GetST_EXP),
		new LuaMethod("ST_OTHER", GetST_OTHER),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.StoreType", typeof(fogs.proto.msg.StoreType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_BLACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_BLACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_SKILL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_SKILL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_FASHION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_FASHION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_HONOR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_HONOR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_TOUR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_TOUR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_REPUTATION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_REPUTATION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_EXP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_EXP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetST_OTHER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.StoreType.ST_OTHER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.StoreType o = (fogs.proto.msg.StoreType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

