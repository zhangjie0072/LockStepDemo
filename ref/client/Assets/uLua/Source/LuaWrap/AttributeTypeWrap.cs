using System;
using LuaInterface;

public class AttributeTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("NONE", GetNONE),
		new LuaMethod("BASIC", GetBASIC),
		new LuaMethod("HEDGING", GetHEDGING),
		new LuaMethod("HEDGINGLEVEL", GetHEDGINGLEVEL),
		new LuaMethod("OTHER", GetOTHER),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "AttributeType", typeof(AttributeType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNONE(IntPtr L)
	{
		LuaScriptMgr.Push(L, AttributeType.NONE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBASIC(IntPtr L)
	{
		LuaScriptMgr.Push(L, AttributeType.BASIC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHEDGING(IntPtr L)
	{
		LuaScriptMgr.Push(L, AttributeType.HEDGING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHEDGINGLEVEL(IntPtr L)
	{
		LuaScriptMgr.Push(L, AttributeType.HEDGINGLEVEL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOTHER(IntPtr L)
	{
		LuaScriptMgr.Push(L, AttributeType.OTHER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		AttributeType o = (AttributeType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

