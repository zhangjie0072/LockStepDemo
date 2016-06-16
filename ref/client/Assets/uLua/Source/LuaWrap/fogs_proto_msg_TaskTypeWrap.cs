using System;
using LuaInterface;

public class fogs_proto_msg_TaskTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("FESTIVAL", GetFESTIVAL),
		new LuaMethod("MAIN", GetMAIN),
		new LuaMethod("DAILY", GetDAILY),
		new LuaMethod("SIGN", GetSIGN),
		new LuaMethod("LEVEL", GetLEVEL),
		new LuaMethod("NEW_COMER", GetNEW_COMER),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TaskType", typeof(fogs.proto.msg.TaskType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFESTIVAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.FESTIVAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.MAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDAILY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.DAILY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSIGN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.SIGN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNEW_COMER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TaskType.NEW_COMER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.TaskType o = (fogs.proto.msg.TaskType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

