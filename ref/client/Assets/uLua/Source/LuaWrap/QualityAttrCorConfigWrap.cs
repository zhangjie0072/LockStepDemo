using System;
using System.Collections.Generic;
using LuaInterface;

public class QualityAttrCorConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("GetConsume", GetConsume),
			new LuaMethod("GetFactor", GetFactor),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateQualityAttrCorConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("attrData", get_attrData, set_attrData),
		};

		LuaScriptMgr.RegisterLib(L, "QualityAttrCorConfig", typeof(QualityAttrCorConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateQualityAttrCorConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualityAttrCorConfig obj = new QualityAttrCorConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualityAttrCorConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(QualityAttrCorConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrData(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, QualityAttrCorConfig.attrData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrData(IntPtr L)
	{
		QualityAttrCorConfig.attrData = (List<QualityAttrCor>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<QualityAttrCor>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		QualityAttrCorConfig obj = (QualityAttrCorConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualityAttrCorConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		QualityAttrCorConfig obj = (QualityAttrCorConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualityAttrCorConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		Dictionary<uint,uint> o = obj.GetConsume(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFactor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		QualityAttrCorConfig obj = (QualityAttrCorConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualityAttrCorConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		float o = obj.GetFactor(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		QualityAttrCorConfig obj = (QualityAttrCorConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualityAttrCorConfig");
		obj.ReadConfig();
		return 0;
	}
}

