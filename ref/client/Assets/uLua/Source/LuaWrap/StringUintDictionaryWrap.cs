﻿using System;
using System.Collections.Generic;
using LuaInterface;

public class StringUintDictionaryWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("get_Item", get_Item),
			new LuaMethod("set_Item", set_Item),
			new LuaMethod("Add", Add),
			new LuaMethod("Clear", Clear),
			new LuaMethod("ContainsKey", ContainsKey),
			new LuaMethod("ContainsValue", ContainsValue),
			new LuaMethod("GetObjectData", GetObjectData),
			new LuaMethod("OnDeserialization", OnDeserialization),
			new LuaMethod("Remove", Remove),
			new LuaMethod("TryGetValue", TryGetValue),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("New", _CreateStringUintDictionary),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Count", get_Count, null),
			new LuaField("Comparer", get_Comparer, null),
			new LuaField("Keys", get_Keys, null),
			new LuaField("Values", get_Values, null),
		};

		LuaScriptMgr.RegisterLib(L, "StringUintDictionary", typeof(Dictionary<string,uint>), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStringUintDictionary(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Dictionary<string,uint> obj = new Dictionary<string,uint>();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			Dictionary<string,uint> obj = new Dictionary<string,uint>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IDictionary<string,uint>)))
		{
			IDictionary<string,uint> arg0 = (IDictionary<string,uint>)LuaScriptMgr.GetNetObject(L, 1, typeof(IDictionary<string,uint>));
			Dictionary<string,uint> obj = new Dictionary<string,uint>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IEqualityComparer<string>)))
		{
			IEqualityComparer<string> arg0 = (IEqualityComparer<string>)LuaScriptMgr.GetNetObject(L, 1, typeof(IEqualityComparer<string>));
			Dictionary<string,uint> obj = new Dictionary<string,uint>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IEqualityComparer<string>)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			IEqualityComparer<string> arg1 = (IEqualityComparer<string>)LuaScriptMgr.GetNetObject(L, 2, typeof(IEqualityComparer<string>));
			Dictionary<string,uint> obj = new Dictionary<string,uint>(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IDictionary<string,uint>), typeof(IEqualityComparer<string>)))
		{
			IDictionary<string,uint> arg0 = (IDictionary<string,uint>)LuaScriptMgr.GetNetObject(L, 1, typeof(IDictionary<string,uint>));
			IEqualityComparer<string> arg1 = (IEqualityComparer<string>)LuaScriptMgr.GetNetObject(L, 2, typeof(IEqualityComparer<string>));
			Dictionary<string,uint> obj = new Dictionary<string,uint>(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Dictionary<string,uint>.New");
		}

		return 0;
	}

	static Type classType = typeof(Dictionary<string,uint>);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Count on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Count);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Comparer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Comparer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Comparer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Comparer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keys(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Keys");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Keys on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Keys);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Values(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Values");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Values on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Values);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint o = obj[arg0];
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj[arg0] = arg1;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.Add(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		obj.Clear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ContainsKey(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.ContainsValue(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjectData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		System.Runtime.Serialization.SerializationInfo arg0 = (System.Runtime.Serialization.SerializationInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(System.Runtime.Serialization.SerializationInfo));
		System.Runtime.Serialization.StreamingContext arg1 = (System.Runtime.Serialization.StreamingContext)LuaScriptMgr.GetNetObject(L, 3, typeof(System.Runtime.Serialization.StreamingContext));
		obj.GetObjectData(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeserialization(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		obj.OnDeserialization(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.Remove(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryGetValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1;
		bool o = obj.TryGetValue(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Dictionary<string,uint> obj = (Dictionary<string,uint>)LuaScriptMgr.GetNetObjectSelf(L, 1, "Dictionary<string,uint>");
		Dictionary<string,uint>.Enumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

