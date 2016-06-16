using System;
using System.Collections.Generic;
using LuaInterface;

public class SkillUpConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetSkillAttr", GetSkillAttr),
			new LuaMethod("GetSkillConsume", GetSkillConsume),
			new LuaMethod("GetSkillAttrSymbol", GetSkillAttrSymbol),
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateSkillUpConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("skillUpList", get_skillUpList, set_skillUpList),
		};

		LuaScriptMgr.RegisterLib(L, "SkillUpConfig", typeof(SkillUpConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillUpConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SkillUpConfig obj = new SkillUpConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SkillUpConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(SkillUpConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skillUpList(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, SkillUpConfig.skillUpList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skillUpList(IntPtr L)
	{
		SkillUpConfig.skillUpList = (List<SkillUp>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<SkillUp>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SkillUpConfig obj = (SkillUpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillUpConfig");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		Dictionary<uint,uint> o = obj.GetSkillAttr(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SkillUpConfig obj = (SkillUpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillUpConfig");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		Dictionary<uint,uint> o = obj.GetSkillConsume(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSkillAttrSymbol(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SkillUpConfig obj = (SkillUpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillUpConfig");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		Dictionary<string,uint> o = obj.GetSkillAttrSymbol(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SkillUpConfig obj = (SkillUpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillUpConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SkillUpConfig obj = (SkillUpConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SkillUpConfig");
		obj.ReadConfig();
		return 0;
	}
}

