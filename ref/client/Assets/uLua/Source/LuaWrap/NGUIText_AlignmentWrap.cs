using System;
using LuaInterface;

public class NGUIText_AlignmentWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Automatic", GetAutomatic),
		new LuaMethod("Left", GetLeft),
		new LuaMethod("Center", GetCenter),
		new LuaMethod("Right", GetRight),
		new LuaMethod("Justified", GetJustified),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "NGUIText.Alignment", typeof(NGUIText.Alignment), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAutomatic(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.Alignment.Automatic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLeft(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.Alignment.Left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCenter(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.Alignment.Center);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRight(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.Alignment.Right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetJustified(IntPtr L)
	{
		LuaScriptMgr.Push(L, NGUIText.Alignment.Justified);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		NGUIText.Alignment o = (NGUIText.Alignment)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

