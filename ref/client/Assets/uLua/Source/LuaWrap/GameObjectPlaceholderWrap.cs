using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class GameObjectPlaceholderWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Replace", Replace),
			new LuaMethod("New", _CreateGameObjectPlaceholder),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("prefab", get_prefab, set_prefab),
			new LuaField("spawnedObj", get_spawnedObj, null),
		};

		LuaScriptMgr.RegisterLib(L, "GameObjectPlaceholder", typeof(GameObjectPlaceholder), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameObjectPlaceholder(IntPtr L)
	{
		LuaDLL.luaL_error(L, "GameObjectPlaceholder class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(GameObjectPlaceholder);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prefab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameObjectPlaceholder obj = (GameObjectPlaceholder)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prefab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prefab on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.prefab);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spawnedObj(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameObjectPlaceholder obj = (GameObjectPlaceholder)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spawnedObj");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spawnedObj on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.spawnedObj);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_prefab(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameObjectPlaceholder obj = (GameObjectPlaceholder)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name prefab");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index prefab on a nil value");
			}
		}

		obj.prefab = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Replace(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject)))
		{
			GameObject arg0 = (GameObject)LuaScriptMgr.GetLuaObject(L, 1);
			GameObject o = GameObjectPlaceholder.Replace(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform)))
		{
			Transform arg0 = (Transform)LuaScriptMgr.GetLuaObject(L, 1);
			GameObject o = GameObjectPlaceholder.Replace(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObjectPlaceholder.Replace");
		}

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

