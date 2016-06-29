using System;
using LuaInterface;

public class fogs_proto_msg_TaskInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_TaskInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sign_info", get_sign_info, set_sign_info),
			new LuaField("last_daily_time", get_last_daily_time, set_last_daily_time),
			new LuaField("daily_info", get_daily_info, set_daily_info),
			new LuaField("main_info", get_main_info, set_main_info),
			new LuaField("task_list", get_task_list, null),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TaskInfo", typeof(fogs.proto.msg.TaskInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_TaskInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.TaskInfo obj = new fogs.proto.msg.TaskInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.TaskInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.TaskInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_info on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.sign_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_last_daily_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_daily_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_daily_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.last_daily_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_daily_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name daily_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index daily_info on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.daily_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_main_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name main_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index main_info on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.main_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_task_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name task_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index task_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.task_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_info on a nil value");
			}
		}

		obj.sign_info = (fogs.proto.msg.SignInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.SignInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_last_daily_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_daily_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_daily_time on a nil value");
			}
		}

		obj.last_daily_time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_daily_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name daily_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index daily_info on a nil value");
			}
		}

		obj.daily_info = (fogs.proto.msg.TaskConditionDaily)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.TaskConditionDaily));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_main_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TaskInfo obj = (fogs.proto.msg.TaskInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name main_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index main_info on a nil value");
			}
		}

		obj.main_info = (fogs.proto.msg.TaskConditionMain)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.TaskConditionMain));
		return 0;
	}
}

