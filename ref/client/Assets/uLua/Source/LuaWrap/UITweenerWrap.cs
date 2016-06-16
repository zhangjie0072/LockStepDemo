using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using Object = UnityEngine.Object;

public class UITweenerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetOnFinished", SetOnFinished),
			new LuaMethod("AddOnFinished", AddOnFinished),
			new LuaMethod("RemoveOnFinished", RemoveOnFinished),
			new LuaMethod("Sample", Sample),
			new LuaMethod("PlayForward", PlayForward),
			new LuaMethod("PlayReverse", PlayReverse),
			new LuaMethod("Play", Play),
			new LuaMethod("ResetToBeginning", ResetToBeginning),
			new LuaMethod("Toggle", Toggle),
			new LuaMethod("SetStartToCurrentValue", SetStartToCurrentValue),
			new LuaMethod("SetEndToCurrentValue", SetEndToCurrentValue),
			new LuaMethod("New", _CreateUITweener),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("method", get_method, set_method),
			new LuaField("style", get_style, set_style),
			new LuaField("animationCurve", get_animationCurve, set_animationCurve),
			new LuaField("ignoreTimeScale", get_ignoreTimeScale, set_ignoreTimeScale),
			new LuaField("delay", get_delay, set_delay),
			new LuaField("duration", get_duration, set_duration),
			new LuaField("steeperCurves", get_steeperCurves, set_steeperCurves),
			new LuaField("tweenGroup", get_tweenGroup, set_tweenGroup),
			new LuaField("onFinished", get_onFinished, set_onFinished),
			new LuaField("eventReceiver", get_eventReceiver, set_eventReceiver),
			new LuaField("callWhenFinished", get_callWhenFinished, set_callWhenFinished),
			new LuaField("amountPerDelta", get_amountPerDelta, null),
			new LuaField("tweenFactor", get_tweenFactor, set_tweenFactor),
			new LuaField("direction", get_direction, null),
		};

		LuaScriptMgr.RegisterLib(L, "UITweener", typeof(UITweener), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUITweener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UITweener class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UITweener);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		LuaScriptMgr.Push(L, UITweener.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_method(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name method");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index method on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.method);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_style(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name style");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index style on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.style);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animationCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationCurve on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.animationCurve);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ignoreTimeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreTimeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreTimeScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ignoreTimeScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_delay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name delay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index delay on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.delay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_duration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name duration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index duration on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.duration);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_steeperCurves(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name steeperCurves");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index steeperCurves on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.steeperCurves);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweenGroup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweenGroup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweenGroup on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tweenGroup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFinished(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFinished");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFinished on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onFinished);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventReceiver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventReceiver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventReceiver on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.eventReceiver);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_callWhenFinished(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name callWhenFinished");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index callWhenFinished on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.callWhenFinished);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_amountPerDelta(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name amountPerDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index amountPerDelta on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.amountPerDelta);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweenFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweenFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweenFactor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tweenFactor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_direction(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name direction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index direction on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.direction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		UITweener.current = (UITweener)LuaScriptMgr.GetUnityObject(L, 3, typeof(UITweener));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_method(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name method");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index method on a nil value");
			}
		}

		obj.method = (UITweener.Method)LuaScriptMgr.GetNetObject(L, 3, typeof(UITweener.Method));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_style(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name style");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index style on a nil value");
			}
		}

		obj.style = (UITweener.Style)LuaScriptMgr.GetNetObject(L, 3, typeof(UITweener.Style));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animationCurve(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationCurve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationCurve on a nil value");
			}
		}

		obj.animationCurve = (AnimationCurve)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationCurve));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ignoreTimeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreTimeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreTimeScale on a nil value");
			}
		}

		obj.ignoreTimeScale = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_delay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name delay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index delay on a nil value");
			}
		}

		obj.delay = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_duration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name duration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index duration on a nil value");
			}
		}

		obj.duration = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_steeperCurves(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name steeperCurves");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index steeperCurves on a nil value");
			}
		}

		obj.steeperCurves = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tweenGroup(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweenGroup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweenGroup on a nil value");
			}
		}

		obj.tweenGroup = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFinished(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFinished");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFinished on a nil value");
			}
		}

		obj.onFinished = (List<EventDelegate>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<EventDelegate>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventReceiver(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventReceiver");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventReceiver on a nil value");
			}
		}

		obj.eventReceiver = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_callWhenFinished(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name callWhenFinished");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index callWhenFinished on a nil value");
			}
		}

		obj.callWhenFinished = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tweenFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITweener obj = (UITweener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweenFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweenFactor on a nil value");
			}
		}

		obj.tweenFactor = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetOnFinished(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate)))
		{
			UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
			EventDelegate arg0 = (EventDelegate)LuaScriptMgr.GetLuaObject(L, 2);
			obj.SetOnFinished(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate.Callback)))
		{
			UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
			EventDelegate.Callback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (EventDelegate.Callback)LuaScriptMgr.GetLuaObject(L, 2);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = () =>
				{
					func.Call();
				};
			}

			obj.SetOnFinished(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UITweener.SetOnFinished");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddOnFinished(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate)))
		{
			UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
			EventDelegate arg0 = (EventDelegate)LuaScriptMgr.GetLuaObject(L, 2);
			obj.AddOnFinished(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(UITweener), typeof(EventDelegate.Callback)))
		{
			UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
			EventDelegate.Callback arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (EventDelegate.Callback)LuaScriptMgr.GetLuaObject(L, 2);
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
				arg0 = () =>
				{
					func.Call();
				};
			}

			obj.AddOnFinished(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UITweener.AddOnFinished");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveOnFinished(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		EventDelegate arg0 = (EventDelegate)LuaScriptMgr.GetNetObject(L, 2, typeof(EventDelegate));
		obj.RemoveOnFinished(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sample(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.Sample(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayForward(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.PlayForward();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayReverse(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.PlayReverse();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.Play(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetToBeginning(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.ResetToBeginning();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Toggle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.Toggle();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStartToCurrentValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.SetStartToCurrentValue();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetEndToCurrentValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITweener obj = (UITweener)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UITweener");
		obj.SetEndToCurrentValue();
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

