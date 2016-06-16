using System;
using LuaInterface;

public class PractiseData_TypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Normal", GetNormal),
		new LuaMethod("Guide", GetGuide),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "PractiseData.Type", typeof(PractiseData.Type), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNormal(IntPtr L)
	{
		LuaScriptMgr.Push(L, PractiseData.Type.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGuide(IntPtr L)
	{
		LuaScriptMgr.Push(L, PractiseData.Type.Guide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		PractiseData.Type o = (PractiseData.Type)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

