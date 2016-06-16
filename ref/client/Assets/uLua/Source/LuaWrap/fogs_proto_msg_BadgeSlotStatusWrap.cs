using System;
using LuaInterface;

public class fogs_proto_msg_BadgeSlotStatusWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("LOCKED", GetLOCKED),
		new LuaMethod("LOCKED_CANPRE_OPEN", GetLOCKED_CANPRE_OPEN),
		new LuaMethod("LOCKED_WILL_OPEN", GetLOCKED_WILL_OPEN),
		new LuaMethod("UNLOCK", GetUNLOCK),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.BadgeSlotStatus", typeof(fogs.proto.msg.BadgeSlotStatus), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOCKED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeSlotStatus.LOCKED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOCKED_CANPRE_OPEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeSlotStatus.LOCKED_CANPRE_OPEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLOCKED_WILL_OPEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeSlotStatus.LOCKED_WILL_OPEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetUNLOCK(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeSlotStatus.UNLOCK);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.BadgeSlotStatus o = (fogs.proto.msg.BadgeSlotStatus)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

