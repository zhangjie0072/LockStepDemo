﻿using System;
using System.Collections.Generic;
using LuaInterface;

public class ChapterConfigKeyValuePairWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ToString", ToString),
			new LuaMethod("New", _CreateChapterConfigKeyValuePair),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__tostring", Lua_ToString),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Key", get_Key, null),
			new LuaField("Value", get_Value, null),
		};

		LuaScriptMgr.RegisterLib(L, "ChapterConfigKeyValuePair", typeof(KeyValuePair<uint,fogs.proto.config.ChapterConfig>), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateChapterConfigKeyValuePair(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 1);
			fogs.proto.config.ChapterConfig arg1 = (fogs.proto.config.ChapterConfig)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.config.ChapterConfig));
			KeyValuePair<uint,fogs.proto.config.ChapterConfig> obj = new KeyValuePair<uint,fogs.proto.config.ChapterConfig>(arg0,arg1);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 0)
		{
			KeyValuePair<uint,fogs.proto.config.ChapterConfig> obj = new KeyValuePair<uint,fogs.proto.config.ChapterConfig>();
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: KeyValuePair<uint,fogs.proto.config.ChapterConfig>.New");
		}

		return 0;
	}

	static Type classType = typeof(KeyValuePair<uint,fogs.proto.config.ChapterConfig>);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Key(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Key");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Key on a nil value");
			}
		}

		KeyValuePair<uint,fogs.proto.config.ChapterConfig> obj = (KeyValuePair<uint,fogs.proto.config.ChapterConfig>)o;
		LuaScriptMgr.Push(L, obj.Key);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Value on a nil value");
			}
		}

		KeyValuePair<uint,fogs.proto.config.ChapterConfig> obj = (KeyValuePair<uint,fogs.proto.config.ChapterConfig>)o;
		LuaScriptMgr.PushObject(L, obj.Value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_ToString(IntPtr L)
	{
		object obj = LuaScriptMgr.GetLuaObject(L, 1);

		if (obj != null)
		{
			LuaScriptMgr.Push(L, obj.ToString());
		}
		else
		{
			LuaScriptMgr.Push(L, "Table: ChapterConfigKeyValuePair");
		}

		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		KeyValuePair<uint,fogs.proto.config.ChapterConfig> obj = (KeyValuePair<uint,fogs.proto.config.ChapterConfig>)LuaScriptMgr.GetNetObjectSelf(L, 1, "KeyValuePair<uint,fogs.proto.config.ChapterConfig>");
		string o = obj.ToString();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

