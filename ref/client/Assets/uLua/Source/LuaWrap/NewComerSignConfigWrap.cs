using System;
using System.Collections.Generic;
using LuaInterface;

public class NewComerSignConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetDayAwardType", GetDayAwardType),
			new LuaMethod("GetDayAwardNum", GetDayAwardNum),
			new LuaMethod("GetConsumeType", GetConsumeType),
			new LuaMethod("GetConsumeNum", GetConsumeNum),
			new LuaMethod("GetTotalAward", GetTotalAward),
			new LuaMethod("GetDayDesc", GetDayDesc),
			new LuaMethod("New", _CreateNewComerSignConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configData", get_configData, set_configData),
			new LuaField("totalConfigData", get_totalConfigData, set_totalConfigData),
		};

		LuaScriptMgr.RegisterLib(L, "NewComerSignConfig", typeof(NewComerSignConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNewComerSignConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NewComerSignConfig obj = new NewComerSignConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NewComerSignConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(NewComerSignConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignConfig obj = (NewComerSignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_totalConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignConfig obj = (NewComerSignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name totalConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index totalConfigData on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.totalConfigData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignConfig obj = (NewComerSignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configData on a nil value");
			}
		}

		obj.configData = (Dictionary<uint,NewComerSignData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,NewComerSignData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_totalConfigData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignConfig obj = (NewComerSignConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name totalConfigData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index totalConfigData on a nil value");
			}
		}

		obj.totalConfigData = (Dictionary<uint,NewComerTotalData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,NewComerTotalData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDayAwardType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetDayAwardType(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDayAwardNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetDayAwardNum(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConsumeType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetConsumeType(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConsumeNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetConsumeNum(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTotalAward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<string> o = obj.GetTotalAward(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDayDesc(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NewComerSignConfig obj = (NewComerSignConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "NewComerSignConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		string o = obj.GetDayDesc(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

