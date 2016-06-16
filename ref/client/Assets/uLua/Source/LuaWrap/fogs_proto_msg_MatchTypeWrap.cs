using System;
using LuaInterface;

public class fogs_proto_msg_MatchTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("MT_NONE", GetMT_NONE),
		new LuaMethod("MT_PRACTICE", GetMT_PRACTICE),
		new LuaMethod("MT_CAREER", GetMT_CAREER),
		new LuaMethod("MT_TOUR", GetMT_TOUR),
		new LuaMethod("MT_REGULAR", GetMT_REGULAR),
		new LuaMethod("MT_QUALIFYING", GetMT_QUALIFYING),
		new LuaMethod("MT_1V1", GetMT_1V1),
		new LuaMethod("MT_1V1_PLUS", GetMT_1V1_PLUS),
		new LuaMethod("MT_3V3", GetMT_3V3),
		new LuaMethod("MT_PVP_1V1", GetMT_PVP_1V1),
		new LuaMethod("MT_PVP_1V1_PLUS", GetMT_PVP_1V1_PLUS),
		new LuaMethod("MT_PVP_3V3", GetMT_PVP_3V3),
		new LuaMethod("MT_REGULAR_RACE", GetMT_REGULAR_RACE),
		new LuaMethod("MT_QUALIFYING_NEW", GetMT_QUALIFYING_NEW),
		new LuaMethod("MT_QUALIFYING_NEWER", GetMT_QUALIFYING_NEWER),
		new LuaMethod("MT_BULLFIGHT", GetMT_BULLFIGHT),
		new LuaMethod("MT_SHOOT", GetMT_SHOOT),
		new LuaMethod("MT_PRACTICE_1V1", GetMT_PRACTICE_1V1),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.MatchType", typeof(fogs.proto.msg.MatchType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_NONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_PRACTICE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_PRACTICE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_CAREER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_CAREER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_TOUR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_TOUR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_REGULAR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_REGULAR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_QUALIFYING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_QUALIFYING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_1V1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_1V1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_1V1_PLUS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_1V1_PLUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_3V3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_3V3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_PVP_1V1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_PVP_1V1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_PVP_1V1_PLUS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_PVP_1V1_PLUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_PVP_3V3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_PVP_3V3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_REGULAR_RACE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_REGULAR_RACE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_QUALIFYING_NEW(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_QUALIFYING_NEW);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_QUALIFYING_NEWER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_QUALIFYING_NEWER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_BULLFIGHT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_BULLFIGHT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_SHOOT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_SHOOT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMT_PRACTICE_1V1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.MatchType.MT_PRACTICE_1V1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.MatchType o = (fogs.proto.msg.MatchType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

