using System;
using LuaInterface;

public class LadderSeasonWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateLadderSeason),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("season", get_season, set_season),
			new LuaField("startYear", get_startYear, set_startYear),
			new LuaField("startMonth", get_startMonth, set_startMonth),
			new LuaField("startDay", get_startDay, set_startDay),
			new LuaField("endYear", get_endYear, set_endYear),
			new LuaField("endMonth", get_endMonth, set_endMonth),
			new LuaField("endDay", get_endDay, set_endDay),
		};

		LuaScriptMgr.RegisterLib(L, "LadderSeason", typeof(LadderSeason), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLadderSeason(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LadderSeason obj = new LadderSeason();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LadderSeason.New");
		}

		return 0;
	}

	static Type classType = typeof(LadderSeason);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_season(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name season");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index season on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.season);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startYear(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startYear");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startYear on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.startYear);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startMonth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startMonth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startMonth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.startMonth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startDay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startDay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startDay on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.startDay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_endYear(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endYear");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endYear on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.endYear);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_endMonth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endMonth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endMonth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.endMonth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_endDay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endDay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endDay on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.endDay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_season(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name season");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index season on a nil value");
			}
		}

		obj.season = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startYear(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startYear");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startYear on a nil value");
			}
		}

		obj.startYear = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startMonth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startMonth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startMonth on a nil value");
			}
		}

		obj.startMonth = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startDay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name startDay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index startDay on a nil value");
			}
		}

		obj.startDay = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_endYear(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endYear");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endYear on a nil value");
			}
		}

		obj.endYear = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_endMonth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endMonth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endMonth on a nil value");
			}
		}

		obj.endMonth = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_endDay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LadderSeason obj = (LadderSeason)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name endDay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index endDay on a nil value");
			}
		}

		obj.endDay = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

