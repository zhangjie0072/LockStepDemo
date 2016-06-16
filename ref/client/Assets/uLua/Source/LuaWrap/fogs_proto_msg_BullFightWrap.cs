using System;
using LuaInterface;

public class fogs_proto_msg_BullFightWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_BullFight),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("times", get_times, set_times),
			new LuaField("complete", get_complete, null),
			new LuaField("bullfight_buy_times", get_bullfight_buy_times, set_bullfight_buy_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.BullFight", typeof(fogs.proto.msg.BullFight), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_BullFight(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.BullFight obj = new fogs.proto.msg.BullFight();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.BullFight.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.BullFight);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BullFight obj = (fogs.proto.msg.BullFight)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BullFight obj = (fogs.proto.msg.BullFight)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index complete on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bullfight_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BullFight obj = (fogs.proto.msg.BullFight)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullfight_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullfight_buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bullfight_buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BullFight obj = (fogs.proto.msg.BullFight)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index times on a nil value");
			}
		}

		obj.times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bullfight_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BullFight obj = (fogs.proto.msg.BullFight)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bullfight_buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bullfight_buy_times on a nil value");
			}
		}

		obj.bullfight_buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

