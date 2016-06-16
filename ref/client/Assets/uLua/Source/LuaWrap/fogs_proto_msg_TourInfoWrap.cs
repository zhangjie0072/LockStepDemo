using System;
using LuaInterface;

public class fogs_proto_msg_TourInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_TourInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("npc", get_npc, null),
			new LuaField("challenge_times", get_challenge_times, set_challenge_times),
			new LuaField("last_challenge_time", get_last_challenge_time, set_last_challenge_time),
			new LuaField("buy_times", get_buy_times, set_buy_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.TourInfo", typeof(fogs.proto.msg.TourInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_TourInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.TourInfo obj = new fogs.proto.msg.TourInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.TourInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.TourInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.npc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_challenge_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challenge_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challenge_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.challenge_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_last_challenge_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_challenge_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_challenge_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.last_challenge_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_challenge_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challenge_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challenge_times on a nil value");
			}
		}

		obj.challenge_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_last_challenge_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name last_challenge_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index last_challenge_time on a nil value");
			}
		}

		obj.last_challenge_time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.TourInfo obj = (fogs.proto.msg.TourInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_times on a nil value");
			}
		}

		obj.buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

