using System;
using LuaInterface;

public class SkillTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("PASSIVE", GetPASSIVE),
		new LuaMethod("ACTIVE", GetACTIVE),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "SkillType", typeof(SkillType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPASSIVE(IntPtr L)
	{
		LuaScriptMgr.Push(L, SkillType.PASSIVE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetACTIVE(IntPtr L)
	{
		LuaScriptMgr.Push(L, SkillType.ACTIVE);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		SkillType o = (SkillType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

