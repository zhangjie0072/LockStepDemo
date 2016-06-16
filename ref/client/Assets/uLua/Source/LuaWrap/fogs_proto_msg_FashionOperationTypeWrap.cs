using System;
using LuaInterface;

public class fogs_proto_msg_FashionOperationTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("FOT_EQUIP", GetFOT_EQUIP),
		new LuaMethod("FOT_UNEQUIP", GetFOT_UNEQUIP),
		new LuaMethod("FOT_RENEW", GetFOT_RENEW),
		new LuaMethod("FOT_DELETE", GetFOT_DELETE),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FashionOperationType", typeof(fogs.proto.msg.FashionOperationType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_EQUIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FashionOperationType.FOT_EQUIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_UNEQUIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FashionOperationType.FOT_UNEQUIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_RENEW(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FashionOperationType.FOT_RENEW);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFOT_DELETE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FashionOperationType.FOT_DELETE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.FashionOperationType o = (fogs.proto.msg.FashionOperationType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

