using System;
using LuaInterface;

public class fogs_proto_msg_TattooTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("TT_NECK", GetTT_NECK),
		new LuaMethod("TT_CHEST", GetTT_CHEST),
		new LuaMethod("TT_ARM", GetTT_ARM),
		new LuaMethod("TT_BACK", GetTT_BACK),
		new LuaMethod("TT_PIECE", GetTT_PIECE),
		new LuaMethod("TT_ALL", GetTT_ALL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TattooType", typeof(fogs.proto.msg.TattooType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_NECK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_NECK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_CHEST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_CHEST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_ARM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_ARM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_BACK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_BACK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_PIECE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_PIECE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_ALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TattooType.TT_ALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.TattooType o = (fogs.proto.msg.TattooType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

