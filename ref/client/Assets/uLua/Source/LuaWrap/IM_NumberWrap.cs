using System;
using LuaInterface;

public class IM_NumberWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Raw", Raw),
			new LuaMethod("CompareTo", CompareTo),
			new LuaMethod("Equals", Equals),
			new LuaMethod("Approximately", Approximately),
			new LuaMethod("ToString", ToString),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("TryParse", TryParse),
			new LuaMethod("Parse", Parse),
			new LuaMethod("New", _CreateIM_Number),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__tostring", Lua_ToString),
			new LuaMethod("__add", Lua_Add),
			new LuaMethod("__sub", Lua_Sub),
			new LuaMethod("__mul", Lua_Mul),
			new LuaMethod("__div", Lua_Div),
			new LuaMethod("__eq", Lua_Eq),
			new LuaMethod("__unm", Lua_Neg),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("max", get_max, set_max),
			new LuaField("min", get_min, set_min),
			new LuaField("zero", get_zero, set_zero),
			new LuaField("one", get_one, set_one),
			new LuaField("two", get_two, set_two),
			new LuaField("half", get_half, set_half),
			new LuaField("deviation", get_deviation, set_deviation),
			new LuaField("raw", get_raw, null),
			new LuaField("round", get_round, null),
			new LuaField("floor", get_floor, null),
			new LuaField("ceil", get_ceil, null),
			new LuaField("roundToInt", get_roundToInt, null),
			new LuaField("floorToInt", get_floorToInt, null),
			new LuaField("ceilToInt", get_ceilToInt, null),
		};

		LuaScriptMgr.RegisterLib(L, "IM.Number", typeof(IM.Number), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateIM_Number(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			IM.Number obj = new IM.Number(arg0);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 2)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			IM.Number obj = new IM.Number(arg0,arg1);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 0)
		{
			IM.Number obj = new IM.Number();
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.New");
		}

		return 0;
	}

	static Type classType = typeof(IM.Number);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.max);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_min(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.min);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_zero(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.zero);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_one(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.one);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_two(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.two);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_half(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.half);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deviation(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Number.deviation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_raw(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name raw");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index raw on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.Push(L, obj.raw);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_round(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name round");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index round on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.PushValue(L, obj.round);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_floor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name floor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index floor on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.PushValue(L, obj.floor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ceil(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ceil");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ceil on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.PushValue(L, obj.ceil);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roundToInt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roundToInt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roundToInt on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.Push(L, obj.roundToInt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_floorToInt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name floorToInt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index floorToInt on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.Push(L, obj.floorToInt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ceilToInt(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ceilToInt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ceilToInt on a nil value");
			}
		}

		IM.Number obj = (IM.Number)o;
		LuaScriptMgr.Push(L, obj.ceilToInt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_max(IntPtr L)
	{
		IM.Number.max = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_min(IntPtr L)
	{
		IM.Number.min = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_zero(IntPtr L)
	{
		IM.Number.zero = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_one(IntPtr L)
	{
		IM.Number.one = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_two(IntPtr L)
	{
		IM.Number.two = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_half(IntPtr L)
	{
		IM.Number.half = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_deviation(IntPtr L)
	{
		IM.Number.deviation = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
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
			LuaScriptMgr.Push(L, "Table: IM.Number");
		}

		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Raw(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		long arg0 = (long)LuaScriptMgr.GetNumber(L, 1);
		IM.Number o = IM.Number.Raw(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareTo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number obj = (IM.Number)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Number");
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			int o = obj.CompareTo(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number obj = (IM.Number)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Number");
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			int o = obj.CompareTo(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.CompareTo");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number obj = (IM.Number)LuaScriptMgr.GetVarObject(L, 1);
			int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
			bool o = obj.Equals(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number obj = (IM.Number)LuaScriptMgr.GetVarObject(L, 1);
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.Equals(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(object)))
		{
			IM.Number obj = (IM.Number)LuaScriptMgr.GetVarObject(L, 1);
			object arg0 = LuaScriptMgr.GetVarObject(L, 2);
			bool o = obj.Equals(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.Equals");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Approximately(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			bool o = IM.Number.Approximately(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
			bool o = IM.Number.Approximately(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.Approximately");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Number obj = (IM.Number)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Number");
		string o = obj.ToString();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Number obj = (IM.Number)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Number");
		int o = obj.GetHashCode();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryParse(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		IM.Number arg1;
		bool o = IM.Number.TryParse(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.PushValue(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Parse(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		IM.Number o = IM.Number.Parse(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Add(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IM.Number)))
		{
			int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 + arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			IM.Number o = arg0 + arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 + arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.op_Addition");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Sub(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IM.Number)))
		{
			int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 - arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			IM.Number o = arg0 - arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 - arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.op_Subtraction");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Neg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
		IM.Number o = -arg0;
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Mul(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IM.Number)))
		{
			int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			IM.Number o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.op_Multiply");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Div(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IM.Number)))
		{
			int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 / arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			IM.Number o = arg0 / arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Number o = arg0 / arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.op_Division");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(int)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetVarObject(L, 1);
			int arg1 = (int)LuaDLL.lua_tonumber(L, 2);
			bool o = arg0 == arg1;
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Number)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetVarObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetVarObject(L, 2);
			bool o = arg0 == arg1;
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Number.op_Equality");
		}

		return 0;
	}
}

