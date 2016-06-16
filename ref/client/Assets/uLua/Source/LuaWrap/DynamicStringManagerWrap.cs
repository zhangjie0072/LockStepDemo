using System;
using LuaInterface;

public class DynamicStringManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("New", _CreateDynamicStringManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Instance", get_Instance, null),
			new LuaField("NoticePopupString", get_NoticePopupString, null),
			new LuaField("ContactUsString", get_ContactUsString, null),
			new LuaField("LoginServerClosedString", get_LoginServerClosedString, null),
		};

		LuaScriptMgr.RegisterLib(L, "DynamicStringManager", typeof(DynamicStringManager), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDynamicStringManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "DynamicStringManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(DynamicStringManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, DynamicStringManager.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NoticePopupString(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DynamicStringManager obj = (DynamicStringManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NoticePopupString");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NoticePopupString on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.NoticePopupString);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ContactUsString(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DynamicStringManager obj = (DynamicStringManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ContactUsString");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ContactUsString on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ContactUsString);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LoginServerClosedString(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DynamicStringManager obj = (DynamicStringManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LoginServerClosedString");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LoginServerClosedString on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LoginServerClosedString);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		DynamicStringManager obj = (DynamicStringManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "DynamicStringManager");
		obj.Init();
		return 0;
	}
}

