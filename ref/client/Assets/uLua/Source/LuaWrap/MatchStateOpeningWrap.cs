using System;
using UnityEngine;
using LuaInterface;

public class MatchStateOpeningWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnEnter", OnEnter),
			new LuaMethod("ViewUpdate", ViewUpdate),
			new LuaMethod("OnExit", OnExit),
			new LuaMethod("OnPress", OnPress),
			new LuaMethod("OnClick", OnClick),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnDragEnd", OnDragEnd),
			new LuaMethod("New", _CreateMatchStateOpening),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "MatchStateOpening", typeof(MatchStateOpening), regs, fields, typeof(MatchState));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMatchStateOpening(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MatchStateMachine arg0 = (MatchStateMachine)LuaScriptMgr.GetNetObject(L, 1, typeof(MatchStateMachine));
			MatchStateOpening obj = new MatchStateOpening(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MatchStateOpening.New");
		}

		return 0;
	}

	static Type classType = typeof(MatchStateOpening);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		MatchState arg0 = (MatchState)LuaScriptMgr.GetNetObject(L, 2, typeof(MatchState));
		obj.OnEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.ViewUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		obj.OnExit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 3);
		bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
		bool arg3;
		obj.OnPress(arg0,arg1,arg2,out arg3);
		LuaScriptMgr.Push(L, arg3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 3);
		bool arg2;
		obj.OnClick(arg0,arg1,out arg2);
		LuaScriptMgr.Push(L, arg2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 3);
		Vector2 arg2 = LuaScriptMgr.GetVector2(L, 4);
		bool arg3;
		obj.OnDrag(arg0,arg1,arg2,out arg3);
		LuaScriptMgr.Push(L, arg3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDragEnd(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MatchStateOpening obj = (MatchStateOpening)LuaScriptMgr.GetNetObjectSelf(L, 1, "MatchStateOpening");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnDragEnd(arg0);
		return 0;
	}
}

