using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UBasketWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Build", Build),
			new LuaMethod("SetEffect", SetEffect),
			new LuaMethod("OnGoal", OnGoal),
			new LuaMethod("OnNoGoal", OnNoGoal),
			new LuaMethod("OnBackboardCollision", OnBackboardCollision),
			new LuaMethod("OnRimCollision", OnRimCollision),
			new LuaMethod("OnDunk", OnDunk),
			new LuaMethod("New", _CreateUBasket),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onGoal", get_onGoal, set_onGoal),
			new LuaField("onNoGoal", get_onNoGoal, set_onNoGoal),
			new LuaField("onRimCollision", get_onRimCollision, set_onRimCollision),
			new LuaField("onDunk", get_onDunk, set_onDunk),
			new LuaField("m_backboard", get_m_backboard, null),
			new LuaField("m_rim", get_m_rim, null),
			new LuaField("m_vShootTarget", get_m_vShootTarget, null),
		};

		LuaScriptMgr.RegisterLib(L, "UBasket", typeof(UBasket), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUBasket(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UBasket class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UBasket);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGoal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onGoal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onNoGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNoGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNoGoal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onNoGoal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onRimCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRimCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRimCollision on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onRimCollision);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDunk(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDunk");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDunk on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDunk);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_backboard(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_backboard");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_backboard on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_backboard);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_rim(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_rim");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_rim on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_rim);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_vShootTarget(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_vShootTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_vShootTarget on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.m_vShootTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onGoal on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onGoal = (UBasket.BasketEventDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasket.BasketEventDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onGoal = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onNoGoal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onNoGoal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onNoGoal on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onNoGoal = (UBasket.BasketEventDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasket.BasketEventDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onNoGoal = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onRimCollision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onRimCollision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onRimCollision on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onRimCollision = (UBasket.BasketEventDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasket.BasketEventDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onRimCollision = (param0, param1) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDunk(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UBasket obj = (UBasket)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDunk");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDunk on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDunk = (UBasket.BasketEventDunkDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UBasket.BasketEventDunkDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDunk = (param0, param1, param2) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				LuaScriptMgr.Push(L, param1);
				LuaScriptMgr.Push(L, param2);
				func.PCall(top, 3);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Build(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		IM.Vector3 arg0 = (IM.Vector3)LuaScriptMgr.GetNetObject(L, 2, typeof(IM.Vector3));
		obj.Build(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEffect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		BasketState arg0 = (BasketState)LuaScriptMgr.GetNetObject(L, 2, typeof(BasketState));
		Object arg1 = (Object)LuaScriptMgr.GetUnityObject(L, 3, typeof(Object));
		obj.SetEffect(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnGoal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.OnGoal(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnNoGoal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.OnNoGoal(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBackboardCollision(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.OnBackboardCollision(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRimCollision(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		obj.OnRimCollision(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDunk(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UBasket obj = (UBasket)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UBasket");
		UBasketball arg0 = (UBasketball)LuaScriptMgr.GetUnityObject(L, 2, typeof(UBasketball));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.OnDunk(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

