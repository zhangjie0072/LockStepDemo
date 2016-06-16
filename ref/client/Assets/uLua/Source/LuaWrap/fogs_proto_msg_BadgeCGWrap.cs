using System;
using LuaInterface;

public class fogs_proto_msg_BadgeCGWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("CG_ALL", GetCG_ALL),
		new LuaMethod("CG_RED", GetCG_RED),
		new LuaMethod("CG_BLUE", GetCG_BLUE),
		new LuaMethod("CG_GREEN", GetCG_GREEN),
		new LuaMethod("CG_GLODEN", GetCG_GLODEN),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.BadgeCG", typeof(fogs.proto.msg.BadgeCG), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCG_ALL(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeCG.CG_ALL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCG_RED(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeCG.CG_RED);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCG_BLUE(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeCG.CG_BLUE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCG_GREEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeCG.CG_GREEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCG_GLODEN(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.BadgeCG.CG_GLODEN);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.BadgeCG o = (fogs.proto.msg.BadgeCG)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

