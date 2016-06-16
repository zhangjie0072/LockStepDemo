using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class MotionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateMotion),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("averageDuration", get_averageDuration, null),
			new LuaField("averageAngularSpeed", get_averageAngularSpeed, null),
			new LuaField("averageSpeed", get_averageSpeed, null),
			new LuaField("apparentSpeed", get_apparentSpeed, null),
			new LuaField("isLooping", get_isLooping, null),
			new LuaField("legacy", get_legacy, null),
			new LuaField("isHumanMotion", get_isHumanMotion, null),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.Motion", typeof(Motion), regs, fields, typeof(Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMotion(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Motion obj = new Motion();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Motion.New");
		}

		return 0;
	}

	static Type classType = typeof(Motion);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_averageDuration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name averageDuration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index averageDuration on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.averageDuration);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_averageAngularSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name averageAngularSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index averageAngularSpeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.averageAngularSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_averageSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name averageSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index averageSpeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.averageSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_apparentSpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name apparentSpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index apparentSpeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.apparentSpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isLooping(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isLooping");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isLooping on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isLooping);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_legacy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name legacy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index legacy on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.legacy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isHumanMotion(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Motion obj = (Motion)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isHumanMotion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isHumanMotion on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isHumanMotion);
		return 1;
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

