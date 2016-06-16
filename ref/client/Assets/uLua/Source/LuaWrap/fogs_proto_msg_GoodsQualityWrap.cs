using System;
using LuaInterface;

public class fogs_proto_msg_GoodsQualityWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("GQ_NONE", GetGQ_NONE),
		new LuaMethod("GQ_WHITE", GetGQ_WHITE),
		new LuaMethod("GQ_GREEN", GetGQ_GREEN),
		new LuaMethod("GQ_BLUE", GetGQ_BLUE),
		new LuaMethod("GQ_PURPEL", GetGQ_PURPEL),
		new LuaMethod("GQ_GOLDEN", GetGQ_GOLDEN),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GoodsQuality", typeof(fogs.proto.msg.GoodsQuality), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_NONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_WHITE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_WHITE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_GREEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_GREEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_BLUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_BLUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_PURPEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_PURPEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGQ_GOLDEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GoodsQuality.GQ_GOLDEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.GoodsQuality o = (fogs.proto.msg.GoodsQuality)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

