using System;
using LuaInterface;

public class DaySignDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateDaySignData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sign_award", get_sign_award, set_sign_award),
			new LuaField("award_count", get_award_count, set_award_count),
			new LuaField("vip_level", get_vip_level, set_vip_level),
			new LuaField("vip_award", get_vip_award, set_vip_award),
		};

		LuaScriptMgr.RegisterLib(L, "DaySignData", typeof(DaySignData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDaySignData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			DaySignData obj = new DaySignData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DaySignData.New");
		}

		return 0;
	}

	static Type classType = typeof(DaySignData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_count);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vip_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.vip_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vip_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.vip_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award on a nil value");
			}
		}

		obj.sign_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_count on a nil value");
			}
		}

		obj.award_count = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vip_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_level on a nil value");
			}
		}

		obj.vip_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vip_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DaySignData obj = (DaySignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vip_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vip_award on a nil value");
			}
		}

		obj.vip_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

