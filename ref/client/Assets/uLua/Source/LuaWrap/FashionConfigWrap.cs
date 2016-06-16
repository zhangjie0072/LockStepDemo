using System;
using System.Collections.Generic;
using LuaInterface;

public class FashionConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("MappingPart", MappingPart),
			new LuaMethod("GetFashionAttr", GetFashionAttr),
			new LuaMethod("GetFashionData", GetFashionData),
			new LuaMethod("GetRandomFashionAttr", GetRandomFashionAttr),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("New", _CreateFashionConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("configs", get_configs, set_configs),
			new LuaField("mapHideParts", get_mapHideParts, set_mapHideParts),
			new LuaField("fashionAttrs", get_fashionAttrs, set_fashionAttrs),
			new LuaField("fashionDatas", get_fashionDatas, set_fashionDatas),
		};

		LuaScriptMgr.RegisterLib(L, "FashionConfig", typeof(FashionConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFashionConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FashionConfig obj = new FashionConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(FashionConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.configs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mapHideParts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapHideParts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapHideParts on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mapHideParts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fashionAttrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashionAttrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashionAttrs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fashionAttrs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fashionDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashionDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashionDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fashionDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_configs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name configs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index configs on a nil value");
			}
		}

		obj.configs = (Dictionary<uint,FashionItem>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,FashionItem>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mapHideParts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mapHideParts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mapHideParts on a nil value");
			}
		}

		obj.mapHideParts = (Dictionary<uint,HidePart>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,HidePart>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fashionAttrs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashionAttrs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashionAttrs on a nil value");
			}
		}

		obj.fashionAttrs = (Dictionary<uint,FashionAttr>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,FashionAttr>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fashionDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionConfig obj = (FashionConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashionDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashionDatas on a nil value");
			}
		}

		obj.fashionDatas = (Dictionary<uint,FashionData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,FashionData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionItem o = obj.GetConfig(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MappingPart(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		HidePart o = obj.MappingPart(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionAttr o = obj.GetFashionAttr(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFashionData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		FashionData o = obj.GetFashionData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRandomFashionAttr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		FashionAttr o = obj.GetRandomFashionAttr();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		FashionConfig obj = (FashionConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "FashionConfig");
		obj.ReadConfig();
		return 0;
	}
}

