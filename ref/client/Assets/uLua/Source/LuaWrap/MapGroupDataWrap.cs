using System;
using System.Collections.Generic;
using LuaInterface;

public class MapGroupDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateMapGroupData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("groupName", get_groupName, set_groupName),
			new LuaField("groupIDs", get_groupIDs, set_groupIDs),
			new LuaField("describe", get_describe, set_describe),
			new LuaField("attrID", get_attrID, set_attrID),
			new LuaField("attrNum", get_attrNum, set_attrNum),
		};

		LuaScriptMgr.RegisterLib(L, "MapGroupData", typeof(MapGroupData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMapGroupData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MapGroupData obj = new MapGroupData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MapGroupData.New");
		}

		return 0;
	}

	static Type classType = typeof(MapGroupData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

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
	static int get_groupName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name groupName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index groupName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.groupName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_groupIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name groupIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index groupIDs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.groupIDs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_describe(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name describe");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index describe on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.describe);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.attrID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attrNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.attrNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

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
	static int set_groupName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name groupName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index groupName on a nil value");
			}
		}

		obj.groupName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_groupIDs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name groupIDs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index groupIDs on a nil value");
			}
		}

		obj.groupIDs = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_describe(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name describe");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index describe on a nil value");
			}
		}

		obj.describe = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrID on a nil value");
			}
		}

		obj.attrID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attrNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapGroupData obj = (MapGroupData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attrNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attrNum on a nil value");
			}
		}

		obj.attrNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

