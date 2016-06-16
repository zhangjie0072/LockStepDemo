using System;
using LuaInterface;

public class fogs_proto_msg_vipOperTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("BUY_HP", GetBUY_HP),
		new LuaMethod("BUY_GOLD", GetBUY_GOLD),
		new LuaMethod("BUY_GIFT", GetBUY_GIFT),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.vipOperType", typeof(fogs.proto.msg.vipOperType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBUY_HP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.vipOperType.BUY_HP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBUY_GOLD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.vipOperType.BUY_GOLD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBUY_GIFT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.vipOperType.BUY_GIFT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.vipOperType o = (fogs.proto.msg.vipOperType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

