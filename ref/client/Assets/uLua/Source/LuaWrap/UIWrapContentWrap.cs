using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIWrapContentWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SortBasedOnScrollMovement", SortBasedOnScrollMovement),
			new LuaMethod("SortAlphabetically", SortAlphabetically),
			new LuaMethod("WrapContent", WrapContent),
			new LuaMethod("New", _CreateUIWrapContent),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("itemSize", get_itemSize, set_itemSize),
			new LuaField("cullContent", get_cullContent, set_cullContent),
			new LuaField("minIndex", get_minIndex, set_minIndex),
			new LuaField("maxIndex", get_maxIndex, set_maxIndex),
			new LuaField("onInitializeItem", get_onInitializeItem, set_onInitializeItem),
		};

		LuaScriptMgr.RegisterLib(L, "UIWrapContent", typeof(UIWrapContent), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIWrapContent(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIWrapContent class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(UIWrapContent);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_itemSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemSize on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.itemSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cullContent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullContent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullContent on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cullContent);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minIndex on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.minIndex);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxIndex on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxIndex);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onInitializeItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onInitializeItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onInitializeItem on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onInitializeItem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_itemSize(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemSize on a nil value");
			}
		}

		obj.itemSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cullContent(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullContent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullContent on a nil value");
			}
		}

		obj.cullContent = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minIndex on a nil value");
			}
		}

		obj.minIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxIndex(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxIndex on a nil value");
			}
		}

		obj.maxIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onInitializeItem(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIWrapContent obj = (UIWrapContent)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onInitializeItem");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onInitializeItem on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onInitializeItem = (UIWrapContent.OnInitializeItem)LuaScriptMgr.GetNetObject(L, 3, typeof(UIWrapContent.OnInitializeItem));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onInitializeItem = (param0, param1, param2) =>
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
	static int SortBasedOnScrollMovement(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIWrapContent obj = (UIWrapContent)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIWrapContent");
		obj.SortBasedOnScrollMovement();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SortAlphabetically(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIWrapContent obj = (UIWrapContent)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIWrapContent");
		obj.SortAlphabetically();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WrapContent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIWrapContent obj = (UIWrapContent)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UIWrapContent");
		obj.WrapContent();
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

