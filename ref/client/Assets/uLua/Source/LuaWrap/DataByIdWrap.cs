using System;
using System.Collections.Generic;
using LuaInterface;

public class DataByIdWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateDataById),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("rank_min", get_rank_min, set_rank_min),
			new LuaField("rank_max", get_rank_max, set_rank_max),
			new LuaField("databyid", get_databyid, set_databyid),
		};

		LuaScriptMgr.RegisterLib(L, "DataById", typeof(DataById), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDataById(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			DataById obj = new DataById();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: DataById.New");
		}

		return 0;
	}

	static Type classType = typeof(DataById);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rank_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_min on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rank_min);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rank_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_max on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rank_max);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_databyid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name databyid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index databyid on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.databyid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rank_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_min on a nil value");
			}
		}

		obj.rank_min = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rank_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rank_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rank_max on a nil value");
			}
		}

		obj.rank_max = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_databyid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		DataById obj = (DataById)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name databyid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index databyid on a nil value");
			}
		}

		obj.databyid = (List<QualifyingAwardsData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<QualifyingAwardsData>));
		return 0;
	}
}

