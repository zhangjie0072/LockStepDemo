using System;
using System.Collections.Generic;
using LuaInterface;

public class MapConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetMapGroupDataByID", GetMapGroupDataByID),
			new LuaMethod("SortMapList", SortMapList),
			new LuaMethod("New", _CreateMapConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mapConfig", get_mapConfig, set_mapConfig),
			new LuaField("mapConfigList", get_mapConfigList, set_mapConfigList),
		};

		LuaScriptMgr.RegisterLib(L, "MapConfig", typeof(MapConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMapConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			MapConfig obj = new MapConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MapConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(MapConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapConfig obj = (MapConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapConfig on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mapConfig);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapConfigList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapConfig obj = (MapConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapConfigList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapConfigList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mapConfigList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapConfig(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapConfig obj = (MapConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapConfig");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapConfig on a nil value");
			}
		}

		obj.mapConfig = (Dictionary<uint,MapGroupData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,MapGroupData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapConfigList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MapConfig obj = (MapConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapConfigList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapConfigList on a nil value");
			}
		}

		obj.mapConfigList = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MapConfig obj = (MapConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "MapConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MapConfig obj = (MapConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "MapConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMapGroupDataByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MapConfig obj = (MapConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "MapConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		MapGroupData o = obj.GetMapGroupDataByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortMapList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MapConfig obj = (MapConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "MapConfig");
		List<uint> arg0 = (List<uint>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<uint>));
		obj.SortMapList(arg0);
		return 0;
	}
}

