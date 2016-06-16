using System;
using LuaInterface;

public class RankSubTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("RST_POSITION_C", GetRST_POSITION_C),
		new LuaMethod("RST_POSITION_PF", GetRST_POSITION_PF),
		new LuaMethod("RST_POSITION_SF", GetRST_POSITION_SF),
		new LuaMethod("RST_POSITION_PG", GetRST_POSITION_PG),
		new LuaMethod("RST_POSITION_SG", GetRST_POSITION_SG),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.RankSubType", typeof(fogs.proto.msg.RankSubType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRST_POSITION_C(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankSubType.RST_POSITION_C);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRST_POSITION_PF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankSubType.RST_POSITION_PF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRST_POSITION_SF(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankSubType.RST_POSITION_SF);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRST_POSITION_PG(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankSubType.RST_POSITION_PG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRST_POSITION_SG(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankSubType.RST_POSITION_SG);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.RankSubType o = (fogs.proto.msg.RankSubType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

