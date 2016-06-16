using System;
using LuaInterface;

public class fogs_proto_msg_TeamTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("TT_HOME", GetTT_HOME),
		new LuaMethod("TT_AWAY", GetTT_AWAY),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TeamType", typeof(fogs.proto.msg.TeamType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_HOME(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TeamType.TT_HOME);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTT_AWAY(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.TeamType.TT_AWAY);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.TeamType o = (fogs.proto.msg.TeamType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

