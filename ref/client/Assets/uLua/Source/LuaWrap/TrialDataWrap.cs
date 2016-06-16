using System;
using System.Collections.Generic;
using LuaInterface;

public class TrialDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateTrialData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("index", get_index, set_index),
			new LuaField("id", get_id, set_id),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("activity", get_activity, set_activity),
			new LuaField("title", get_title, set_title),
			new LuaField("score", get_score, set_score),
			new LuaField("showProgress", get_showProgress, set_showProgress),
			new LuaField("award_id", get_award_id, set_award_id),
			new LuaField("link", get_link, set_link),
		};

		LuaScriptMgr.RegisterLib(L, "TrialData", typeof(TrialData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTrialData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TrialData obj = new TrialData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TrialData.New");
		}

		return 0;
	}

	static Type classType = typeof(TrialData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_index(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name index");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index index on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.index);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.activity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.title);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.score);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_showProgress(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showProgress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showProgress on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.showProgress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.award_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_link(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name link");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index link on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.link);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_index(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name index");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index index on a nil value");
			}
		}

		obj.index = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		obj.icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activity on a nil value");
			}
		}

		obj.activity = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		obj.title = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_score(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name score");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index score on a nil value");
			}
		}

		obj.score = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_showProgress(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name showProgress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index showProgress on a nil value");
			}
		}

		obj.showProgress = (short)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_award_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name award_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index award_id on a nil value");
			}
		}

		obj.award_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_link(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TrialData obj = (TrialData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name link");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index link on a nil value");
			}
		}

		obj.link = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

