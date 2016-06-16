using System;
using LuaInterface;

public class fogs_proto_msg_FightStatusWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("FS_NONE", GetFS_NONE),
		new LuaMethod("FS_MAIN", GetFS_MAIN),
		new LuaMethod("FS_ASSIST1", GetFS_ASSIST1),
		new LuaMethod("FS_ASSIST2", GetFS_ASSIST2),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FightStatus", typeof(fogs.proto.msg.FightStatus), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFS_NONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FightStatus.FS_NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFS_MAIN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FightStatus.FS_MAIN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFS_ASSIST1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FightStatus.FS_ASSIST1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFS_ASSIST2(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.FightStatus.FS_ASSIST2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.FightStatus o = (fogs.proto.msg.FightStatus)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

