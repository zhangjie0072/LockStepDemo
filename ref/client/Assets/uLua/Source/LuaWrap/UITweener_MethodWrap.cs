using System;
using LuaInterface;

public class UITweener_MethodWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Linear", GetLinear),
		new LuaMethod("EaseIn", GetEaseIn),
		new LuaMethod("EaseOut", GetEaseOut),
		new LuaMethod("EaseInOut", GetEaseInOut),
		new LuaMethod("BounceIn", GetBounceIn),
		new LuaMethod("BounceOut", GetBounceOut),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UITweener.Method", typeof(UITweener.Method), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLinear(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.Linear);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEaseIn(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.EaseIn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEaseOut(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.EaseOut);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEaseInOut(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.EaseInOut);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBounceIn(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.BounceIn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBounceOut(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.Method.BounceOut);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UITweener.Method o = (UITweener.Method)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

