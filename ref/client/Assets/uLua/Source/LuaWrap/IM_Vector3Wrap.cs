using System;
using UnityEngine;
using LuaInterface;

public class IM_Vector3Wrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Normalize", Normalize),
			new LuaMethod("Dot", Dot),
			new LuaMethod("Cross", Cross),
			new LuaMethod("CrossAndNormalize", CrossAndNormalize),
			new LuaMethod("FromToAngle", FromToAngle),
			new LuaMethod("Angle", Angle),
			new LuaMethod("AngleRad", AngleRad),
			new LuaMethod("Distance", Distance),
			new LuaMethod("Lerp", Lerp),
			new LuaMethod("RotateTowards", RotateTowards),
			new LuaMethod("ToString", ToString),
			new LuaMethod("Parse", Parse),
			new LuaMethod("Equals", Equals),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("New", _CreateIM_Vector3),
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
			new LuaField("x", get_x, set_x),
			new LuaField("y", get_y, set_y),
			new LuaField("z", get_z, set_z),
			new LuaField("zero", get_zero, set_zero),
			new LuaField("one", get_one, set_one),
			new LuaField("up", get_up, set_up),
			new LuaField("down", get_down, set_down),
			new LuaField("left", get_left, set_left),
			new LuaField("right", get_right, set_right),
			new LuaField("forward", get_forward, set_forward),
			new LuaField("back", get_back, set_back),
			new LuaField("gravity", get_gravity, set_gravity),
			new LuaField("magnitude", get_magnitude, null),
			new LuaField("sqrMagnitude", get_sqrMagnitude, null),
			new LuaField("normalized", get_normalized, null),
			new LuaField("xz", get_xz, null),
			new LuaField("xy", get_xy, null),
		};

		LuaScriptMgr.RegisterLib(L, "IM.Vector3", typeof(IM.Vector3), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateIM_Vector3(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
			IM.Vector3 obj = new IM.Vector3(arg0);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 2)
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			IM.Vector3 obj = new IM.Vector3(arg0,arg1);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 3)
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Number));
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Number));
			IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
			IM.Vector3 obj = new IM.Vector3(arg0,arg1,arg2);
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else if (count == 0)
		{
			IM.Vector3 obj = new IM.Vector3();
			LuaScriptMgr.PushValue(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Vector3.New");
		}

		return 0;
	}

	static Type classType = typeof(IM.Vector3);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_x(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name x");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index x on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.x);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_y(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name y");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index y on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.y);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_z(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name z");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index z on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.z);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_zero(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.zero);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_one(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.one);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_up(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.up);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_down(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.down);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_left(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_right(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.right);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forward(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.forward);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_back(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.back);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gravity(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, IM.Vector3.gravity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_magnitude(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name magnitude");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index magnitude on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.magnitude);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sqrMagnitude(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sqrMagnitude");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sqrMagnitude on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.Push(L, obj.sqrMagnitude);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normalized(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalized");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalized on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.normalized);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_xz(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name xz");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index xz on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.xz);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_xy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name xy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index xy on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		LuaScriptMgr.PushValue(L, obj.xy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_x(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name x");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index x on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		obj.x = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		LuaScriptMgr.SetValueObject(L, 1, obj);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_y(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name y");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index y on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		obj.y = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		LuaScriptMgr.SetValueObject(L, 1, obj);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_z(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);

		if (o == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name z");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index z on a nil value");
			}
		}

		IM.Vector3 obj = (IM.Vector3)o;
		obj.z = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		LuaScriptMgr.SetValueObject(L, 1, obj);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_zero(IntPtr L)
	{
		IM.Vector3.zero = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_one(IntPtr L)
	{
		IM.Vector3.one = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_up(IntPtr L)
	{
		IM.Vector3.up = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_down(IntPtr L)
	{
		IM.Vector3.down = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_left(IntPtr L)
	{
		IM.Vector3.left = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_right(IntPtr L)
	{
		IM.Vector3.right = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_forward(IntPtr L)
	{
		IM.Vector3.forward = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_back(IntPtr L)
	{
		IM.Vector3.back = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gravity(IntPtr L)
	{
		IM.Vector3.gravity = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Vector3));
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
			LuaScriptMgr.Push(L, "Table: IM.Vector3");
		}

		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Normalize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Vector3 obj = (IM.Vector3)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Vector3");
		obj.Normalize();
		LuaScriptMgr.SetValueObject(L, 1, obj);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Dot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number o = IM.Vector3.Dot(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Cross(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Vector3 o = IM.Vector3.Cross(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossAndNormalize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Vector3 o = IM.Vector3.CrossAndNormalize(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FromToAngle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number o = IM.Vector3.FromToAngle(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Angle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number o = IM.Vector3.Angle(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AngleRad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number o = IM.Vector3.AngleRad(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Distance(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number o = IM.Vector3.Distance(arg0,arg1);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lerp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		IM.Vector3 o = IM.Vector3.Lerp(arg0,arg1,arg2);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RotateTowards(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Number arg2 = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		IM.Number arg3 = (IM.Number)LuaScriptMgr.GetNetObject(L, 4, typeof(IM.Number));
		IM.Vector3 o = IM.Vector3.RotateTowards(arg0,arg1,arg2,arg3);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Vector3 obj = (IM.Vector3)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Vector3");
		string o = obj.ToString();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Parse(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		IM.Vector3 o = IM.Vector3.Parse(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Vector3)))
		{
			IM.Vector3 obj = (IM.Vector3)LuaScriptMgr.GetVarObject(L, 1);
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.Equals(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(object)))
		{
			IM.Vector3 obj = (IM.Vector3)LuaScriptMgr.GetVarObject(L, 1);
			object arg0 = LuaScriptMgr.GetVarObject(L, 2);
			bool o = obj.Equals(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Vector3.Equals");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Vector3 obj = (IM.Vector3)LuaScriptMgr.GetNetObjectSelf(L, 1, "IM.Vector3");
		int o = obj.GetHashCode();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Add(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Vector3 o = arg0 + arg1;
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Sub(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		IM.Vector3 o = arg0 - arg1;
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Neg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 1, typeof(IM.Vector3));
		IM.Vector3 o = -arg0;
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Mul(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Number), typeof(IM.Vector3)))
		{
			IM.Number arg0 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Number)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Vector3)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = arg0 * arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Vector3.op_Multiply");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Div(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Number)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Number arg1 = (IM.Number)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = arg0 / arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IM.Vector3), typeof(IM.Vector3)))
		{
			IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 1);
			IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetLuaObject(L, 2);
			IM.Vector3 o = arg0 / arg1;
			LuaScriptMgr.PushValue(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: IM.Vector3.op_Division");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetVarObject(L, 1);
		IM.Vector3 arg1 = (IM.Vector3)LuaScriptMgr.GetVarObject(L, 2);
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

