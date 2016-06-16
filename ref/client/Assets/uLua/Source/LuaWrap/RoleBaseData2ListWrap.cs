﻿using System;
using System.Collections.Generic;
using LuaInterface;

public class RoleBaseData2ListWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Add", Add),
			new LuaMethod("AddRange", AddRange),
			new LuaMethod("AsReadOnly", AsReadOnly),
			new LuaMethod("BinarySearch", BinarySearch),
			new LuaMethod("Clear", Clear),
			new LuaMethod("Contains", Contains),
			new LuaMethod("CopyTo", CopyTo),
			new LuaMethod("Exists", Exists),
			new LuaMethod("Find", Find),
			new LuaMethod("FindAll", FindAll),
			new LuaMethod("FindIndex", FindIndex),
			new LuaMethod("FindLast", FindLast),
			new LuaMethod("FindLastIndex", FindLastIndex),
			new LuaMethod("ForEach", ForEach),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetRange", GetRange),
			new LuaMethod("IndexOf", IndexOf),
			new LuaMethod("Insert", Insert),
			new LuaMethod("InsertRange", InsertRange),
			new LuaMethod("LastIndexOf", LastIndexOf),
			new LuaMethod("Remove", Remove),
			new LuaMethod("RemoveAll", RemoveAll),
			new LuaMethod("RemoveAt", RemoveAt),
			new LuaMethod("RemoveRange", RemoveRange),
			new LuaMethod("Reverse", Reverse),
			new LuaMethod("Sort", Sort),
			new LuaMethod("ToArray", ToArray),
			new LuaMethod("TrimExcess", TrimExcess),
			new LuaMethod("TrueForAll", TrueForAll),
			new LuaMethod("get_Item", get_Item),
			new LuaMethod("set_Item", set_Item),
			new LuaMethod("New", _CreateRoleBaseData2List),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Capacity", get_Capacity, set_Capacity),
			new LuaField("Count", get_Count, null),
		};

		LuaScriptMgr.RegisterLib(L, "RoleBaseData2List", typeof(List<RoleBaseData2>), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleBaseData2List(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			List<RoleBaseData2> obj = new List<RoleBaseData2>();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			List<RoleBaseData2> obj = new List<RoleBaseData2>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IEnumerable<RoleBaseData2>)))
		{
			IEnumerable<RoleBaseData2> arg0 = (IEnumerable<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 1, typeof(IEnumerable<RoleBaseData2>));
			List<RoleBaseData2> obj = new List<RoleBaseData2>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.New");
		}

		return 0;
	}

	static Type classType = typeof(List<RoleBaseData2>);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Capacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Capacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Capacity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Capacity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)o;

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
	static int set_Capacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Capacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Capacity on a nil value");
			}
		}

		obj.Capacity = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
		obj.Add(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		IEnumerable<RoleBaseData2> arg0 = (IEnumerable<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(IEnumerable<RoleBaseData2>));
		obj.AddRange(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AsReadOnly(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		System.Collections.ObjectModel.ReadOnlyCollection<RoleBaseData2> o = obj.AsReadOnly();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BinarySearch(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int o = obj.BinarySearch(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			IComparer<RoleBaseData2> arg1 = (IComparer<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 3, typeof(IComparer<RoleBaseData2>));
			int o = obj.BinarySearch(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			RoleBaseData2 arg2 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 4, typeof(RoleBaseData2));
			IComparer<RoleBaseData2> arg3 = (IComparer<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 5, typeof(IComparer<RoleBaseData2>));
			int o = obj.BinarySearch(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.BinarySearch");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		obj.Clear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Contains(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
		bool o = obj.Contains(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyTo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2[] objs0 = LuaScriptMgr.GetArrayObject<RoleBaseData2>(L, 2);
			obj.CopyTo(objs0);
			return 0;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2[] objs0 = LuaScriptMgr.GetArrayObject<RoleBaseData2>(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			obj.CopyTo(objs0,arg1);
			return 0;
		}
		else if (count == 5)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			RoleBaseData2[] objs1 = LuaScriptMgr.GetArrayObject<RoleBaseData2>(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 5);
			obj.CopyTo(arg0,objs1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.CopyTo");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Exists(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		bool o = obj.Exists(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Find(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		RoleBaseData2 o = obj.Find(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		List<RoleBaseData2> o = obj.FindAll(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindIndex(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			Predicate<RoleBaseData2> arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindIndex(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			Predicate<RoleBaseData2> arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 3, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindIndex(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			Predicate<RoleBaseData2> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 4, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
				arg2 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindIndex(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.FindIndex");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindLast(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		RoleBaseData2 o = obj.FindLast(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindLastIndex(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			Predicate<RoleBaseData2> arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindLastIndex(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			Predicate<RoleBaseData2> arg1 = null;
			LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

			if (funcType3 != LuaTypes.LUA_TFUNCTION)
			{
				 arg1 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 3, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 3);
				arg1 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindLastIndex(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			Predicate<RoleBaseData2> arg2 = null;
			LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

			if (funcType4 != LuaTypes.LUA_TFUNCTION)
			{
				 arg2 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 4, typeof(Predicate<RoleBaseData2>));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
				arg2 = (param0) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					func.PCall(top, 1);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (bool)objs[0];
				};
			}

			int o = obj.FindLastIndex(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.FindLastIndex");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForEach(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Action<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Action<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}

		obj.ForEach(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		List<RoleBaseData2>.Enumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		List<RoleBaseData2> o = obj.GetRange(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IndexOf(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int o = obj.IndexOf(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int o = obj.IndexOf(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int o = obj.IndexOf(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.IndexOf");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Insert(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		RoleBaseData2 arg1 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 3, typeof(RoleBaseData2));
		obj.Insert(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InsertRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		IEnumerable<RoleBaseData2> arg1 = (IEnumerable<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 3, typeof(IEnumerable<RoleBaseData2>));
		obj.InsertRange(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LastIndexOf(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int o = obj.LastIndexOf(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int o = obj.LastIndexOf(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int o = obj.LastIndexOf(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.LastIndexOf");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		RoleBaseData2 arg0 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 2, typeof(RoleBaseData2));
		bool o = obj.Remove(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		int o = obj.RemoveAll(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.RemoveAt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.RemoveRange(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reverse(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			obj.Reverse();
			return 0;
		}
		else if (count == 3)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			obj.Reverse(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.Reverse");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sort(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			obj.Sort();
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(List<RoleBaseData2>), typeof(Comparison<RoleBaseData2>)))
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			Comparison<RoleBaseData2> arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (Comparison<RoleBaseData2>)LuaScriptMgr.GetLuaObject(L, 2);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = (param0, param1) =>
				{
					int top = func.BeginPCall();
					LuaScriptMgr.PushObject(L, param0);
					LuaScriptMgr.PushObject(L, param1);
					func.PCall(top, 2);
					object[] objs = func.PopValues(top);
					func.EndPCall(top);
					return (int)objs[0];
				};
			}

			obj.Sort(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(List<RoleBaseData2>), typeof(IComparer<RoleBaseData2>)))
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			IComparer<RoleBaseData2> arg0 = (IComparer<RoleBaseData2>)LuaScriptMgr.GetLuaObject(L, 2);
			obj.Sort(arg0);
			return 0;
		}
		else if (count == 4)
		{
			List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			IComparer<RoleBaseData2> arg2 = (IComparer<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 4, typeof(IComparer<RoleBaseData2>));
			obj.Sort(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<RoleBaseData2>.Sort");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToArray(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		RoleBaseData2[] o = obj.ToArray();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrimExcess(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		obj.TrimExcess();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrueForAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		Predicate<RoleBaseData2> arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Predicate<RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 2, typeof(Predicate<RoleBaseData2>));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.PushObject(L, param0);
				func.PCall(top, 1);
				object[] objs = func.PopValues(top);
				func.EndPCall(top);
				return (bool)objs[0];
			};
		}

		bool o = obj.TrueForAll(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		RoleBaseData2 o = obj[arg0];
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<RoleBaseData2> obj = (List<RoleBaseData2>)LuaScriptMgr.GetNetObjectSelf(L, 1, "List<RoleBaseData2>");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		RoleBaseData2 arg1 = (RoleBaseData2)LuaScriptMgr.GetNetObject(L, 3, typeof(RoleBaseData2));
		obj[arg0] = arg1;
		return 0;
	}
}

