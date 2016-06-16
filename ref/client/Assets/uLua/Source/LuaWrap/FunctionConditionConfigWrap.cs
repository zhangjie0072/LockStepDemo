using System;
using System.Collections.Generic;
using LuaInterface;

public class FunctionConditionConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ValidateFunc", ValidateFunc),
			new LuaMethod("GetFuncConditions", GetFuncConditions),
			new LuaMethod("GetFuncCondition", GetFuncCondition),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateFunctionConditionConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "FunctionConditionConfig", typeof(FunctionConditionConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFunctionConditionConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FunctionConditionConfig obj = new FunctionConditionConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FunctionConditionConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(FunctionConditionConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ValidateFunc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(FunctionConditionConfig), typeof(FuncCondition)))
		{
			FunctionConditionConfig obj = (FunctionConditionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FunctionConditionConfig");
			FuncCondition arg0 = (FuncCondition)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.ValidateFunc(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(FunctionConditionConfig), typeof(string)))
		{
			FunctionConditionConfig obj = (FunctionConditionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FunctionConditionConfig");
			string arg0 = LuaScriptMgr.GetString(L, 2);
			bool o = obj.ValidateFunc(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FunctionConditionConfig.ValidateFunc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFuncConditions(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FunctionConditionConfig obj = (FunctionConditionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FunctionConditionConfig");
		ConditionType arg0 = (ConditionType)LuaScriptMgr.GetNetObject(L, 2, typeof(ConditionType));
		List<FuncCondition> o = obj.GetFuncConditions(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFuncCondition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FunctionConditionConfig obj = (FunctionConditionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FunctionConditionConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		FuncCondition o = obj.GetFuncCondition(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FunctionConditionConfig obj = (FunctionConditionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FunctionConditionConfig");
		obj.ReadConfig();
		return 0;
	}
}

