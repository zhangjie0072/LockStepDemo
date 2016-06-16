using System;
using LuaInterface;

public class fogs_proto_msg_GenderTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("GT_COMMON", GetGT_COMMON),
		new LuaMethod("GT_MALE", GetGT_MALE),
		new LuaMethod("GT_FEMALE", GetGT_FEMALE),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GenderType", typeof(fogs.proto.msg.GenderType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGT_COMMON(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GenderType.GT_COMMON);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGT_MALE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GenderType.GT_MALE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGT_FEMALE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GenderType.GT_FEMALE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.GenderType o = (fogs.proto.msg.GenderType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

