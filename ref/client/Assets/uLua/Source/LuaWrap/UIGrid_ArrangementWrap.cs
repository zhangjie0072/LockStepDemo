using System;
using LuaInterface;

public class UIGrid_ArrangementWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Horizontal", GetHorizontal),
		new LuaMethod("Vertical", GetVertical),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIGrid.Arrangement", typeof(UIGrid.Arrangement), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHorizontal(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIGrid.Arrangement.Horizontal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVertical(IntPtr L)
	{
		LuaScriptMgr.Push(L, UIGrid.Arrangement.Vertical);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UIGrid.Arrangement o = (UIGrid.Arrangement)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

