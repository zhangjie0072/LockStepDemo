using System;
using LuaInterface;

public class UIWidget_PivotWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("TopLeft", GetTopLeft),
		new LuaMethod("Top", GetTop),
		new LuaMethod("TopRight", GetTopRight),
		new LuaMethod("Left", GetLeft),
		new LuaMethod("Center", GetCenter),
		new LuaMethod("Right", GetRight),
		new LuaMethod("BottomLeft", GetBottomLeft),
		new LuaMethod("Bottom", GetBottom),
		new LuaMethod("BottomRight", GetBottomRight),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIWidget.Pivot", typeof(UIWidget.Pivot), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTopLeft(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.TopLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTop(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.Top);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTopRight(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.TopRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLeft(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.Left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCenter(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.Center);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRight(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.Right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBottomLeft(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.BottomLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBottom(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.Bottom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBottomRight(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIWidget.Pivot.BottomRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UIWidget.Pivot o = (UIWidget.Pivot)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

