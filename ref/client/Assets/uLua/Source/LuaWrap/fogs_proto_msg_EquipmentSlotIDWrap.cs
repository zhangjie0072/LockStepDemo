using System;
using LuaInterface;

public class fogs_proto_msg_EquipmentSlotIDWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("ESID_HEAD", GetESID_HEAD),
		new LuaMethod("ESID_CHEST", GetESID_CHEST),
		new LuaMethod("ESID_BRACER", GetESID_BRACER),
		new LuaMethod("ESID_PANTS", GetESID_PANTS),
		new LuaMethod("ESID_SHOE", GetESID_SHOE),
		new LuaMethod("ESID_ALL", GetESID_ALL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.EquipmentSlotID", typeof(fogs.proto.msg.EquipmentSlotID), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_HEAD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_HEAD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_CHEST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_CHEST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_BRACER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_BRACER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_PANTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_PANTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_SHOE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_SHOE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetESID_ALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentSlotID.ESID_ALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.EquipmentSlotID o = (fogs.proto.msg.EquipmentSlotID)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

