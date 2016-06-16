using System;
using LuaInterface;

public class fogs_proto_msg_EquipmentOperationTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("EOT_EQUIP", GetEOT_EQUIP),
		new LuaMethod("EOT_UNEQUIP", GetEOT_UNEQUIP),
		new LuaMethod("EOT_EXCHANGE", GetEOT_EXCHANGE),
		new LuaMethod("EOT_UPGRADE_SINGLE", GetEOT_UPGRADE_SINGLE),
		new LuaMethod("EOT_UPGRADE_AUTO", GetEOT_UPGRADE_AUTO),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.EquipmentOperationType", typeof(fogs.proto.msg.EquipmentOperationType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEOT_EQUIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentOperationType.EOT_EQUIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEOT_UNEQUIP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentOperationType.EOT_UNEQUIP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEOT_EXCHANGE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentOperationType.EOT_EXCHANGE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEOT_UPGRADE_SINGLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentOperationType.EOT_UPGRADE_SINGLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEOT_UPGRADE_AUTO(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentOperationType.EOT_UPGRADE_AUTO);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.EquipmentOperationType o = (fogs.proto.msg.EquipmentOperationType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

