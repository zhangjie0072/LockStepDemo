using System;
using LuaInterface;

public class fogs_proto_msg_PositionTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("PT_NONE", GetPT_NONE),
		new LuaMethod("PT_PF", GetPT_PF),
		new LuaMethod("PT_SF", GetPT_SF),
		new LuaMethod("PT_C", GetPT_C),
		new LuaMethod("PT_PG", GetPT_PG),
		new LuaMethod("PT_SG", GetPT_SG),
		new LuaMethod("PT_TOTAL", GetPT_TOTAL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.PositionType", typeof(fogs.proto.msg.PositionType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_NONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_PF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_PF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_SF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_SF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_C(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_C);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_PG(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_PG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_SG(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_SG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPT_TOTAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.PositionType.PT_TOTAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.PositionType o = (fogs.proto.msg.PositionType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

