using System;
using LuaInterface;

public class fogs_proto_msg_TrainingStateWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("TS_IDLE", GetTS_IDLE),
		new LuaMethod("TS_COOLING", GetTS_COOLING),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TrainingState", typeof(fogs.proto.msg.TrainingState), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTS_IDLE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TrainingState.TS_IDLE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTS_COOLING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TrainingState.TS_COOLING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.TrainingState o = (fogs.proto.msg.TrainingState)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

