using System;
using LuaInterface;

public class UIBasicSprite_FlipWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Nothing", GetNothing),
		new LuaMethod("Horizontally", GetHorizontally),
		new LuaMethod("Vertically", GetVertically),
		new LuaMethod("Both", GetBoth),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIBasicSprite.Flip", typeof(UIBasicSprite.Flip), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNothing(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIBasicSprite.Flip.Nothing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHorizontally(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIBasicSprite.Flip.Horizontally);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVertically(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIBasicSprite.Flip.Vertically);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBoth(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIBasicSprite.Flip.Both);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UIBasicSprite.Flip o = (UIBasicSprite.Flip)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

