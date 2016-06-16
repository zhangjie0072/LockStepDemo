using System;
using LuaInterface;

public class UIForm_ShowHideDirectionWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("none", Getnone),
		new LuaMethod("left", Getleft),
		new LuaMethod("right", Getright),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIForm.ShowHideDirection", typeof(UIForm.ShowHideDirection), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getnone(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIForm.ShowHideDirection.none);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getleft(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIForm.ShowHideDirection.left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getright(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIForm.ShowHideDirection.right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UIForm.ShowHideDirection o = (UIForm.ShowHideDirection)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

