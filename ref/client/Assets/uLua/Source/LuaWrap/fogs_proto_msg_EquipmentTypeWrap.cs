using System;
using LuaInterface;

public class fogs_proto_msg_EquipmentTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("ET_HEAD", GetET_HEAD),
		new LuaMethod("ET_CHEST", GetET_CHEST),
		new LuaMethod("ET_BRACER", GetET_BRACER),
		new LuaMethod("ET_PANTS", GetET_PANTS),
		new LuaMethod("ET_SHOE", GetET_SHOE),
		new LuaMethod("ET_EQUIPMENTPIECE", GetET_EQUIPMENTPIECE),
		new LuaMethod("ET_ALL", GetET_ALL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.EquipmentType", typeof(fogs.proto.msg.EquipmentType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_HEAD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_HEAD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_CHEST(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_CHEST);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_BRACER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_BRACER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_PANTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_PANTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_SHOE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_SHOE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_EQUIPMENTPIECE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_EQUIPMENTPIECE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetET_ALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.EquipmentType.ET_ALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.EquipmentType o = (fogs.proto.msg.EquipmentType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

