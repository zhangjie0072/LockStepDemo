using System;
using System.Collections.Generic;
using LuaInterface;

public class FuncConditionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateFuncCondition),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("name", get_name, set_name),
			new LuaField("conditionTypes", get_conditionTypes, set_conditionTypes),
			new LuaField("conditionParams", get_conditionParams, set_conditionParams),
			new LuaField("lockTip", get_lockTip, set_lockTip),
			new LuaField("unlockTip", get_unlockTip, set_unlockTip),
		};

		LuaScriptMgr.RegisterLib(L, "FuncCondition", typeof(FuncCondition), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFuncCondition(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FuncCondition obj = new FuncCondition();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FuncCondition.New");
		}

		return 0;
	}

	static Type classType = typeof(FuncCondition);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_conditionTypes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionTypes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionTypes on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.conditionTypes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_conditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionParams on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.conditionParams);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lockTip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lockTip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lockTip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lockTip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlockTip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockTip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockTip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unlockTip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		obj.name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_conditionTypes(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionTypes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionTypes on a nil value");
			}
		}

		obj.conditionTypes = (List<ConditionType>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ConditionType>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_conditionParams(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name conditionParams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index conditionParams on a nil value");
			}
		}

		obj.conditionParams = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lockTip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lockTip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lockTip on a nil value");
			}
		}

		obj.lockTip = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlockTip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FuncCondition obj = (FuncCondition)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockTip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockTip on a nil value");
			}
		}

		obj.unlockTip = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

