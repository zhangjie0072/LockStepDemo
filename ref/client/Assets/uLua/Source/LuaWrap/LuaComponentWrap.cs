using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class LuaComponentWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateLuaComponent),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("scriptFile", get_scriptFile, set_scriptFile),
			new LuaField("parameters", get_parameters, set_parameters),
			new LuaField("table", get_table, set_table),
		};

		LuaScriptMgr.RegisterLib(L, "LuaComponent", typeof(LuaComponent), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaComponent(IntPtr L)
	{
		LuaDLL.luaL_error(L, "LuaComponent class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(LuaComponent);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scriptFile(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scriptFile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scriptFile on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scriptFile);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parameters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameters on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.parameters);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_table(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name table");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index table on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.table);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scriptFile(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scriptFile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scriptFile on a nil value");
			}
		}

		obj.scriptFile = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_parameters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameters on a nil value");
			}
		}

		obj.parameters = LuaScriptMgr.GetArrayObject<LuaComponent.Param>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_table(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaComponent obj = (LuaComponent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name table");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index table on a nil value");
			}
		}

		obj.table = LuaScriptMgr.GetLuaTable(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

