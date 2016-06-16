using System;
using LuaInterface;

public class MonthSignDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateMonthSignData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sign_times", get_sign_times, set_sign_times),
			new LuaField("sign_award1", get_sign_award1, set_sign_award1),
			new LuaField("award_count1", get_award_count1, set_award_count1),
			new LuaField("sign_award2", get_sign_award2, set_sign_award2),
			new LuaField("award_count2", get_award_count2, set_award_count2),
			new LuaField("sign_award3", get_sign_award3, set_sign_award3),
			new LuaField("award_count3", get_award_count3, set_award_count3),
		};

		LuaScriptMgr.RegisterLib(L, "MonthSignData", typeof(MonthSignData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMonthSignData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MonthSignData obj = new MonthSignData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MonthSignData.New");
		}

		return 0;
	}

	static Type classType = typeof(MonthSignData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_award1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award1 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_award1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_count1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count1 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_count1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_award2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_award2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_count2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_count2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_award3(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award3");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award3 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_award3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_count3(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count3");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count3 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_count3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_times on a nil value");
			}
		}

		obj.sign_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_award1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award1 on a nil value");
			}
		}

		obj.sign_award1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_count1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count1 on a nil value");
			}
		}

		obj.award_count1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_award2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award2 on a nil value");
			}
		}

		obj.sign_award2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_count2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count2 on a nil value");
			}
		}

		obj.award_count2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_award3(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award3");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award3 on a nil value");
			}
		}

		obj.sign_award3 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_count3(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MonthSignData obj = (MonthSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count3");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count3 on a nil value");
			}
		}

		obj.award_count3 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

