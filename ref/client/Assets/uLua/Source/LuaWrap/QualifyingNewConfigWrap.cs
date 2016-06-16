using System;
using System.Collections.Generic;
using LuaInterface;

public class QualifyingNewConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetGrade", GetGrade),
			new LuaMethod("GetPrevGrade", GetPrevGrade),
			new LuaMethod("GetNextGrade", GetNextGrade),
			new LuaMethod("GetMaxStarNum", GetMaxStarNum),
			new LuaMethod("GetSeason", GetSeason),
			new LuaMethod("getGradeConfigByGrade", getGradeConfigByGrade),
			new LuaMethod("New", _CreateQualifyingNewConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("grades", get_grades, set_grades),
			new LuaField("seasons", get_seasons, set_seasons),
			new LuaField("maxStarNum", get_maxStarNum, set_maxStarNum),
		};

		LuaScriptMgr.RegisterLib(L, "QualifyingNewConfig", typeof(QualifyingNewConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateQualifyingNewConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualifyingNewConfig obj = new QualifyingNewConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualifyingNewConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(QualifyingNewConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grades(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grades");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grades on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.grades);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_seasons(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name seasons");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index seasons on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.seasons);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxStarNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxStarNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxStarNum on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.maxStarNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grades(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grades");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grades on a nil value");
			}
		}

		obj.grades = (List<fogs.proto.config.QualifyingNewConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.config.QualifyingNewConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_seasons(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name seasons");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index seasons on a nil value");
			}
		}

		obj.seasons = (Dictionary<uint,fogs.proto.config.QualifyingNewSeasonConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.QualifyingNewSeasonConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxStarNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxStarNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxStarNum on a nil value");
			}
		}

		obj.maxStarNum = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewConfig o = obj.GetGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPrevGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewConfig o = obj.GetPrevGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewConfig o = obj.GetNextGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxStarNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxStarNum(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSeason(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewSeasonConfig o = obj.GetSeason(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int getGradeConfigByGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewConfig obj = (QualifyingNewConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewConfig");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewConfig o = obj.getGradeConfigByGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

