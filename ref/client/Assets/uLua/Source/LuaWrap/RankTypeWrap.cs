using System;
using LuaInterface;

public class RankTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("RT_LEVEL", GetRT_LEVEL),
		new LuaMethod("RT_WEALTH", GetRT_WEALTH),
		new LuaMethod("RT_CUR_REGULAR_POINTS", GetRT_CUR_REGULAR_POINTS),
		new LuaMethod("RT_LAST_REGULAR_POINTS", GetRT_LAST_REGULAR_POINTS),
		new LuaMethod("RT_QUALIFYING", GetRT_QUALIFYING),
		new LuaMethod("RT_PVP", GetRT_PVP),
		new LuaMethod("RT_QUALIFYING_NEW", GetRT_QUALIFYING_NEW),
		new LuaMethod("RT_LADDER", GetRT_LADDER),
		new LuaMethod("RT_POSITION", GetRT_POSITION),
		new LuaMethod("RT_ACHIEVEMENT", GetRT_ACHIEVEMENT),
		new LuaMethod("RT_TOTAL", GetRT_TOTAL),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.RankType", typeof(fogs.proto.msg.RankType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_LEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_LEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_WEALTH(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_WEALTH);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_CUR_REGULAR_POINTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_CUR_REGULAR_POINTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_LAST_REGULAR_POINTS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_LAST_REGULAR_POINTS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_QUALIFYING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_QUALIFYING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_PVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_PVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_QUALIFYING_NEW(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_QUALIFYING_NEW);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_LADDER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_LADDER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_POSITION(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_POSITION);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_ACHIEVEMENT(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_ACHIEVEMENT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRT_TOTAL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.RankType.RT_TOTAL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.RankType o = (fogs.proto.msg.RankType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

