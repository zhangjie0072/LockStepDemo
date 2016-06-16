using System;
using LuaInterface;

public class fogs_proto_msg_QualifyingInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_QualifyingInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("battle", get_battle, null),
			new LuaField("max_ranking", get_max_ranking, set_max_ranking),
			new LuaField("run_times", get_run_times, set_run_times),
			new LuaField("role", get_role, null),
			new LuaField("battle_time", get_battle_time, set_battle_time),
			new LuaField("buy_times", get_buy_times, set_buy_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.QualifyingInfo", typeof(fogs.proto.msg.QualifyingInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_QualifyingInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.QualifyingInfo obj = new fogs.proto.msg.QualifyingInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.QualifyingInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.QualifyingInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_battle(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name battle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index battle on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.battle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_max_ranking(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_ranking");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_ranking on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.max_ranking);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_run_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name run_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index run_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.run_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.role);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_battle_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name battle_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index battle_time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.battle_time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

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
	static int set_max_ranking(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name max_ranking");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index max_ranking on a nil value");
			}
		}

		obj.max_ranking = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_run_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name run_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index run_times on a nil value");
			}
		}

		obj.run_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_battle_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name battle_time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index battle_time on a nil value");
			}
		}

		obj.battle_time = (ulong)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.QualifyingInfo obj = (fogs.proto.msg.QualifyingInfo)o;

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

