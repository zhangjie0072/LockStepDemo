using System;
using LuaInterface;

public class fogs_proto_msg_ShootInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_ShootInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mass_ball_times", get_mass_ball_times, set_mass_ball_times),
			new LuaField("grab_zone_times", get_grab_zone_times, set_grab_zone_times),
			new LuaField("grab_point_times", get_grab_point_times, set_grab_point_times),
			new LuaField("mass_ball_complete", get_mass_ball_complete, null),
			new LuaField("grab_zone_complete", get_grab_zone_complete, null),
			new LuaField("grab_point_complete", get_grab_point_complete, null),
			new LuaField("mass_ball_buy_times", get_mass_ball_buy_times, set_mass_ball_buy_times),
			new LuaField("grab_zone_buy_times", get_grab_zone_buy_times, set_grab_zone_buy_times),
			new LuaField("grab_point_buy_times", get_grab_point_buy_times, set_grab_point_buy_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.ShootInfo", typeof(fogs.proto.msg.ShootInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_ShootInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.ShootInfo obj = new fogs.proto.msg.ShootInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.ShootInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.ShootInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mass_ball_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mass_ball_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mass_ball_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mass_ball_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_zone_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_zone_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_zone_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.grab_zone_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_point_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_point_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_point_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.grab_point_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mass_ball_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mass_ball_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mass_ball_complete on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mass_ball_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_zone_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_zone_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_zone_complete on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.grab_zone_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_point_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_point_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_point_complete on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.grab_point_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mass_ball_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mass_ball_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mass_ball_buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mass_ball_buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_zone_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_zone_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_zone_buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.grab_zone_buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grab_point_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_point_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_point_buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.grab_point_buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mass_ball_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mass_ball_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mass_ball_times on a nil value");
			}
		}

		obj.mass_ball_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grab_zone_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_zone_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_zone_times on a nil value");
			}
		}

		obj.grab_zone_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grab_point_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_point_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_point_times on a nil value");
			}
		}

		obj.grab_point_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mass_ball_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mass_ball_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mass_ball_buy_times on a nil value");
			}
		}

		obj.mass_ball_buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grab_zone_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_zone_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_zone_buy_times on a nil value");
			}
		}

		obj.grab_zone_buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grab_point_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ShootInfo obj = (fogs.proto.msg.ShootInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grab_point_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grab_point_buy_times on a nil value");
			}
		}

		obj.grab_point_buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

