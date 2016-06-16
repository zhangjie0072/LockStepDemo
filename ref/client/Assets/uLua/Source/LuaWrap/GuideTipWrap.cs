using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class GuideTipWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("AutoHide", AutoHide),
			new LuaMethod("Show", Show),
			new LuaMethod("Hide", Hide),
			new LuaMethod("New", _CreateGuideTip),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("tip", get_tip, set_tip),
			new LuaField("firstButtonVisible", get_firstButtonVisible, set_firstButtonVisible),
			new LuaField("firstButtonText", get_firstButtonText, set_firstButtonText),
			new LuaField("nextButtonVisible", get_nextButtonVisible, set_nextButtonVisible),
			new LuaField("instructorVisible", get_instructorVisible, set_instructorVisible),
			new LuaField("instructor", null, set_instructor),
			new LuaField("instructorPos", null, set_instructorPos),
			new LuaField("onFirstButtonClick", get_onFirstButtonClick, set_onFirstButtonClick),
		};

		LuaScriptMgr.RegisterLib(L, "GuideTip", typeof(GuideTip), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGuideTip(IntPtr L)
	{
		LuaDLL.luaL_error(L, "GuideTip class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(GuideTip);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.tip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_firstButtonVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstButtonVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstButtonVisible on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.firstButtonVisible);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_firstButtonText(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstButtonText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstButtonText on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.firstButtonText);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nextButtonVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nextButtonVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nextButtonVisible on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.nextButtonVisible);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instructorVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instructorVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instructorVisible on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.instructorVisible);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onFirstButtonClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFirstButtonClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFirstButtonClick on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onFirstButtonClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tip on a nil value");
			}
		}

		obj.tip = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_firstButtonVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstButtonVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstButtonVisible on a nil value");
			}
		}

		obj.firstButtonVisible = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_firstButtonText(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name firstButtonText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index firstButtonText on a nil value");
			}
		}

		obj.firstButtonText = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_nextButtonVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nextButtonVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nextButtonVisible on a nil value");
			}
		}

		obj.nextButtonVisible = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instructorVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instructorVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instructorVisible on a nil value");
			}
		}

		obj.instructorVisible = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instructor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instructor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instructor on a nil value");
			}
		}

		obj.instructor = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instructorPos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instructorPos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instructorPos on a nil value");
			}
		}

		obj.instructorPos = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onFirstButtonClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GuideTip obj = (GuideTip)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onFirstButtonClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onFirstButtonClick on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onFirstButtonClick = (UIEventListener.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(UIEventListener.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onFirstButtonClick = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AutoHide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GuideTip obj = (GuideTip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GuideTip");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.AutoHide(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Show(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideTip obj = (GuideTip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GuideTip");
		obj.Show();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Hide(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GuideTip obj = (GuideTip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GuideTip");
		obj.Hide();
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

