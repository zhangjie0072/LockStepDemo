using System;
using System.Collections.Generic;
using LuaInterface;

public class AttrNameConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetAttrName", GetAttrName),
			new LuaMethod("GetAttrNameById", GetAttrNameById),
			new LuaMethod("IsFactor", IsFactor),
			new LuaMethod("GetAttrSymbol", GetAttrSymbol),
			new LuaMethod("IsRecommend", IsRecommend),
			new LuaMethod("GetAttrData", GetAttrData),
			new LuaMethod("GetTypeBySymbol", GetTypeBySymbol),
			new LuaMethod("isHide", isHide),
			new LuaMethod("New", _CreateAttrNameConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isReadFinish", get_isReadFinish, set_isReadFinish),
			new LuaField("AttrNameDatas", get_AttrNameDatas, set_AttrNameDatas),
			new LuaField("AttrName", get_AttrName, set_AttrName),
		};

		LuaScriptMgr.RegisterLib(L, "AttrNameConfig", typeof(AttrNameConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAttrNameConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AttrNameConfig obj = new AttrNameConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AttrNameConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(AttrNameConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isReadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReadFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isReadFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AttrNameDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrNameDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrNameDatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AttrNameDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AttrName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrName on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.AttrName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isReadFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReadFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReadFinish on a nil value");
			}
		}

		obj.isReadFinish = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AttrNameDatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrNameDatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrNameDatas on a nil value");
			}
		}

		obj.AttrNameDatas = (List<AttrNameData>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<AttrNameData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AttrName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AttrNameConfig obj = (AttrNameConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AttrName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AttrName on a nil value");
			}
		}

		obj.AttrName = (Dictionary<string,string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrName(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AttrNameConfig), typeof(uint)))
		{
			AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
			uint arg0 = (uint)LuaDLL.lua_tonumber(L, 2);
			string o = obj.GetAttrName(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(AttrNameConfig), typeof(string)))
		{
			AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
			string arg0 = LuaScriptMgr.GetString(L, 2);
			string o = obj.GetAttrName(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AttrNameConfig.GetAttrName");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrNameById(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetAttrNameById(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsFactor(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.IsFactor(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrSymbol(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetAttrSymbol(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsRecommend(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.IsRecommend(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		AttrNameData o = obj.GetAttrData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTypeBySymbol(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		AttributeType o = obj.GetTypeBySymbol(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isHide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AttrNameConfig obj = (AttrNameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AttrNameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.isHide(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

