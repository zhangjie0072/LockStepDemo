using System;
using LuaInterface;

public class fogs_proto_msg_LotteryInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_LotteryInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("total_times1", get_total_times1, set_total_times1),
			new LuaField("free_times1", get_free_times1, set_free_times1),
			new LuaField("last_time1", get_last_time1, set_last_time1),
			new LuaField("total_times2", get_total_times2, set_total_times2),
			new LuaField("free_times2", get_free_times2, set_free_times2),
			new LuaField("last_time2", get_last_time2, set_last_time2),
			new LuaField("diamond_multi_times", get_diamond_multi_times, set_diamond_multi_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.LotteryInfo", typeof(fogs.proto.msg.LotteryInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_LotteryInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.LotteryInfo obj = new fogs.proto.msg.LotteryInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.LotteryInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.LotteryInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_total_times1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_times1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_times1 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.total_times1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_free_times1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name free_times1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index free_times1 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.free_times1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_last_time1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_time1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_time1 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.last_time1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_total_times2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_times2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_times2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.total_times2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_free_times2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name free_times2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index free_times2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.free_times2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_last_time2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_time2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_time2 on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.last_time2);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_diamond_multi_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond_multi_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond_multi_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.diamond_multi_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_total_times1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_times1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_times1 on a nil value");
			}
		}

		obj.total_times1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_free_times1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name free_times1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index free_times1 on a nil value");
			}
		}

		obj.free_times1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_last_time1(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_time1");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_time1 on a nil value");
			}
		}

		obj.last_time1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_total_times2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name total_times2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index total_times2 on a nil value");
			}
		}

		obj.total_times2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_free_times2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name free_times2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index free_times2 on a nil value");
			}
		}

		obj.free_times2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_last_time2(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_time2");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_time2 on a nil value");
			}
		}

		obj.last_time2 = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_diamond_multi_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.LotteryInfo obj = (fogs.proto.msg.LotteryInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond_multi_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond_multi_times on a nil value");
			}
		}

		obj.diamond_multi_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

