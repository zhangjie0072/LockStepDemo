using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIDragDropItemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetLimit", SetLimit),
			new LuaMethod("New", _CreateUIDragDropItem),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("originalItem", get_originalItem, set_originalItem),
			new LuaField("restriction", get_restriction, set_restriction),
			new LuaField("cloneOnDrag", get_cloneOnDrag, set_cloneOnDrag),
			new LuaField("pressAndHoldDelay", get_pressAndHoldDelay, set_pressAndHoldDelay),
		};

		LuaScriptMgr.RegisterLib(L, "UIDragDropItem", typeof(UIDragDropItem), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIDragDropItem(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIDragDropItem class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIDragDropItem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_originalItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name originalItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index originalItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.originalItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_restriction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name restriction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index restriction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.restriction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cloneOnDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cloneOnDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cloneOnDrag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cloneOnDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pressAndHoldDelay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressAndHoldDelay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressAndHoldDelay on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pressAndHoldDelay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_originalItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name originalItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index originalItem on a nil value");
			}
		}

		obj.originalItem = (UIDragDropItem)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIDragDropItem));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_restriction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name restriction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index restriction on a nil value");
			}
		}

		obj.restriction = (UIDragDropItem.Restriction)LuaScriptMgr.GetNetObject(L, 3, typeof(UIDragDropItem.Restriction));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cloneOnDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cloneOnDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cloneOnDrag on a nil value");
			}
		}

		obj.cloneOnDrag = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pressAndHoldDelay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIDragDropItem obj = (UIDragDropItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressAndHoldDelay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressAndHoldDelay on a nil value");
			}
		}

		obj.pressAndHoldDelay = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLimit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		UIDragDropItem obj = (UIDragDropItem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIDragDropItem");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		obj.SetLimit(arg0,arg1,arg2,arg3);
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

