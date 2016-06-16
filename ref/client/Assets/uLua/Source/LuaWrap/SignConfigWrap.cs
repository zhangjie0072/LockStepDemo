using System;
using System.Collections.Generic;
using LuaInterface;

public class SignConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetCurrentMonth", GetCurrentMonth),
			new LuaMethod("GetDaySignData", GetDaySignData),
			new LuaMethod("GetSignDays", GetSignDays),
			new LuaMethod("GetMonthSignData", GetMonthSignData),
			new LuaMethod("New", _CreateSignConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("msigndataLists", get_msigndataLists, set_msigndataLists),
			new LuaField("allDaySignData", get_allDaySignData, set_allDaySignData),
		};

		LuaScriptMgr.RegisterLib(L, "SignConfig", typeof(SignConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSignConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SignConfig obj = new SignConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SignConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(SignConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_msigndataLists(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SignConfig obj = (SignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name msigndataLists");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index msigndataLists on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.msigndataLists);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allDaySignData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SignConfig obj = (SignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name allDaySignData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index allDaySignData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.allDaySignData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_msigndataLists(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SignConfig obj = (SignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name msigndataLists");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index msigndataLists on a nil value");
			}
		}

		obj.msigndataLists = (Dictionary<uint,MonthSignData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,MonthSignData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_allDaySignData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SignConfig obj = (SignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name allDaySignData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index allDaySignData on a nil value");
			}
		}

		obj.allDaySignData = (Dictionary<uint,Dictionary<uint,DaySignData>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Dictionary<uint,DaySignData>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SignConfig obj = (SignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SignConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCurrentMonth(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SignConfig obj = (SignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SignConfig");
		uint o = obj.GetCurrentMonth();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDaySignData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SignConfig obj = (SignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		DaySignData o = obj.GetDaySignData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSignDays(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SignConfig obj = (SignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SignConfig");
		uint o = obj.GetSignDays();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMonthSignData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SignConfig obj = (SignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "SignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		MonthSignData o = obj.GetMonthSignData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

