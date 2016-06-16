using System;
using System.Collections.Generic;
using LuaInterface;

public class QualifyingNewerConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetGrade", GetGrade),
			new LuaMethod("GetPrevGrade", GetPrevGrade),
			new LuaMethod("GetNextGrade", GetNextGrade),
			new LuaMethod("GetNextSubSection", GetNextSubSection),
			new LuaMethod("GetMaxStarNum", GetMaxStarNum),
			new LuaMethod("GetSeason", GetSeason),
			new LuaMethod("GetFirstGrade", GetFirstGrade),
			new LuaMethod("GetFirstSubGrade", GetFirstSubGrade),
			new LuaMethod("New", _CreateQualifyingNewerConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("grades", get_grades, set_grades),
			new LuaField("seasons", get_seasons, set_seasons),
			new LuaField("maxStarNum", get_maxStarNum, set_maxStarNum),
			new LuaField("MaxScore", get_MaxScore, null),
		};

		LuaScriptMgr.RegisterLib(L, "QualifyingNewerConfig", typeof(QualifyingNewerConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateQualifyingNewerConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualifyingNewerConfig obj = new QualifyingNewerConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualifyingNewerConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(QualifyingNewerConfig);

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
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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
	static int get_MaxScore(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name MaxScore");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index MaxScore on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.MaxScore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grades(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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

		obj.grades = (List<fogs.proto.config.QualifyingNewerConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.config.QualifyingNewerConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_seasons(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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

		obj.seasons = (Dictionary<uint,fogs.proto.config.QualifyingNewerSeasonConfig>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,fogs.proto.config.QualifyingNewerSeasonConfig>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxStarNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)o;

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
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPrevGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetPrevGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetNextGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextSubSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetNextSubSection(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMaxStarNum(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetMaxStarNum(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSeason(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerSeasonConfig o = obj.GetSeason(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFirstGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetFirstGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFirstSubGrade(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		QualifyingNewerConfig obj = (QualifyingNewerConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "QualifyingNewerConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.config.QualifyingNewerConfig o = obj.GetFirstSubGrade(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

