using System;
using LuaInterface;

public class fogs_proto_msg_ChatChannelTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("CCT_WORLD", GetCCT_WORLD),
		new LuaMethod("CCT_SYSTEM", GetCCT_SYSTEM),
		new LuaMethod("CCT_LEAGUE", GetCCT_LEAGUE),
		new LuaMethod("CCT_TEAM", GetCCT_TEAM),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.ChatChannelType", typeof(fogs.proto.msg.ChatChannelType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCCT_WORLD(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ChatChannelType.CCT_WORLD);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCCT_SYSTEM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ChatChannelType.CCT_SYSTEM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCCT_LEAGUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ChatChannelType.CCT_LEAGUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCCT_TEAM(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.ChatChannelType.CCT_TEAM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.ChatChannelType o = (fogs.proto.msg.ChatChannelType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

