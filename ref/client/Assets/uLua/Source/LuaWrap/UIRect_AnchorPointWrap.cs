using System;
using UnityEngine;
using LuaInterface;

public class UIRect_AnchorPointWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Set", Set),
			new LuaMethod("SetToNearest", SetToNearest),
			new LuaMethod("SetHorizontal", SetHorizontal),
			new LuaMethod("SetVertical", SetVertical),
			new LuaMethod("GetSides", GetSides),
			new LuaMethod("New", _CreateUIRect_AnchorPoint),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("target", get_target, set_target),
			new LuaField("relative", get_relative, set_relative),
			new LuaField("absolute", get_absolute, set_absolute),
			new LuaField("rect", get_rect, set_rect),
			new LuaField("targetCam", get_targetCam, set_targetCam),
		};

		LuaScriptMgr.RegisterLib(L, "UIRect.AnchorPoint", typeof(UIRect.AnchorPoint), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIRect_AnchorPoint(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UIRect.AnchorPoint obj = new UIRect.AnchorPoint();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1)
		{
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 1);
			UIRect.AnchorPoint obj = new UIRect.AnchorPoint(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UIRect.AnchorPoint.New");
		}

		return 0;
	}

	static Type classType = typeof(UIRect.AnchorPoint);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.target);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_relative(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name relative");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index relative on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.relative);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_absolute(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name absolute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index absolute on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.absolute);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.rect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetCam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetCam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetCam on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.targetCam);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		obj.target = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_relative(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name relative");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index relative on a nil value");
			}
		}

		obj.relative = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_absolute(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name absolute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index absolute on a nil value");
			}
		}

		obj.absolute = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}

		obj.rect = (UIRect)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIRect));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetCam(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetCam");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetCam on a nil value");
			}
		}

		obj.targetCam = (Camera)LuaScriptMgr.GetUnityObject(L, 3, typeof(Camera));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Set(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.Set(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetToNearest(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			obj.SetToNearest(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 7)
		{
			UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 6);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 7);
			obj.SetToNearest(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UIRect.AnchorPoint.SetToNearest");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetHorizontal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.SetHorizontal(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetVertical(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.SetVertical(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSides(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIRect.AnchorPoint obj = (UIRect.AnchorPoint)LuaScriptMgr.GetNetObjectSelf(L, 1, "UIRect.AnchorPoint");
		Transform arg0 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		Vector3[] o = obj.GetSides(arg0);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}
}

