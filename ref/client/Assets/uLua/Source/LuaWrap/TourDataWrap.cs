using System;
using System.Collections.Generic;
using LuaInterface;

public class TourDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateTourData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("level", get_level, set_level),
			new LuaField("ID", get_ID, set_ID),
			new LuaField("baseAttrLower", get_baseAttrLower, set_baseAttrLower),
			new LuaField("baseAttrUpper", get_baseAttrUpper, set_baseAttrUpper),
			new LuaField("hedgingAttrLower", get_hedgingAttrLower, set_hedgingAttrLower),
			new LuaField("hedgingAttrUpper", get_hedgingAttrUpper, set_hedgingAttrUpper),
			new LuaField("challengeConsume", get_challengeConsume, set_challengeConsume),
			new LuaField("winAwards", get_winAwards, set_winAwards),
			new LuaField("failAwards", get_failAwards, set_failAwards),
			new LuaField("gameModeID", get_gameModeID, set_gameModeID),
			new LuaField("quality", get_quality, set_quality),
			new LuaField("star", get_star, set_star),
		};

		LuaScriptMgr.RegisterLib(L, "TourData", typeof(TourData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTourData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			TourData obj = new TourData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: TourData.New");
		}

		return 0;
	}

	static Type classType = typeof(TourData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_baseAttrLower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseAttrLower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseAttrLower on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.baseAttrLower);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_baseAttrUpper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseAttrUpper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseAttrUpper on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.baseAttrUpper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hedgingAttrLower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedgingAttrLower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedgingAttrLower on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hedgingAttrLower);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hedgingAttrUpper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedgingAttrUpper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedgingAttrUpper on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hedgingAttrUpper);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_challengeConsume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challengeConsume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challengeConsume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.challengeConsume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_winAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winAwards on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.winAwards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_failAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name failAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index failAwards on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.failAwards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameModeID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameModeID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gameModeID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.star);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		obj.level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		obj.ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_baseAttrLower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseAttrLower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseAttrLower on a nil value");
			}
		}

		obj.baseAttrLower = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_baseAttrUpper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name baseAttrUpper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index baseAttrUpper on a nil value");
			}
		}

		obj.baseAttrUpper = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hedgingAttrLower(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedgingAttrLower");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedgingAttrLower on a nil value");
			}
		}

		obj.hedgingAttrLower = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hedgingAttrUpper(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hedgingAttrUpper");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hedgingAttrUpper on a nil value");
			}
		}

		obj.hedgingAttrUpper = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_challengeConsume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challengeConsume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challengeConsume on a nil value");
			}
		}

		obj.challengeConsume = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_winAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name winAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index winAwards on a nil value");
			}
		}

		obj.winAwards = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_failAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name failAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index failAwards on a nil value");
			}
		}

		obj.failAwards = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameModeID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameModeID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameModeID on a nil value");
			}
		}

		obj.gameModeID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		obj.quality = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_star(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TourData obj = (TourData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star on a nil value");
			}
		}

		obj.star = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}
}

