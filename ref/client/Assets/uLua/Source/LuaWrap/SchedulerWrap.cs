using System;
using LuaInterface;

public class SchedulerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Update", Update),
			new LuaMethod("AddFrame", AddFrame),
			new LuaMethod("RemoveFrame", RemoveFrame),
			new LuaMethod("AddTimer", AddTimer),
			new LuaMethod("RemoveTimer", RemoveTimer),
			new LuaMethod("AddUpdator", AddUpdator),
			new LuaMethod("RemoveUpdator", RemoveUpdator),
			new LuaMethod("ClearUpdator", ClearUpdator),
			new LuaMethod("New", _CreateScheduler),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Instance", get_Instance, null),
		};

		LuaScriptMgr.RegisterLib(L, "Scheduler", typeof(Scheduler), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateScheduler(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Scheduler class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(Scheduler);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Scheduler.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddFrame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		Action arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action)LuaScriptMgr.GetNetObject(L, 4, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = () =>
			{
				func.Call();
			};
		}

		obj.AddFrame(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveFrame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RemoveFrame(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		Action arg2 = null;
		LuaTypes funcType4 = LuaDLL.lua_type(L, 4);

		if (funcType4 != LuaTypes.LUA_TFUNCTION)
		{
			 arg2 = (Action)LuaScriptMgr.GetNetObject(L, 4, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 4);
			arg2 = () =>
			{
				func.Call();
			};
		}

		obj.AddTimer(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RemoveTimer(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddUpdator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.AddUpdator(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveUpdator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		Action arg0 = null;
		LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (Action)LuaScriptMgr.GetNetObject(L, 2, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RemoveUpdator(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearUpdator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Scheduler obj = (Scheduler)LuaScriptMgr.GetNetObjectSelf(L, 1, "Scheduler");
		obj.ClearUpdator();
		return 0;
	}
}

