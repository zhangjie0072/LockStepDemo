using System;
using LuaInterface;

public class fogs_proto_msg_BadgeSlotWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_BadgeSlot),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("status", get_status, set_status),
			new LuaField("badge_id", get_badge_id, set_badge_id),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.BadgeSlot", typeof(fogs.proto.msg.BadgeSlot), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_BadgeSlot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.BadgeSlot obj = new fogs.proto.msg.BadgeSlot();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.BadgeSlot.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.BadgeSlot);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_status(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name status");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index status on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.status);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_badge_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badge_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badge_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.badge_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_status(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name status");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index status on a nil value");
			}
		}

		obj.status = (fogs.proto.msg.BadgeSlotStatus)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.BadgeSlotStatus));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_badge_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.BadgeSlot obj = (fogs.proto.msg.BadgeSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name badge_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index badge_id on a nil value");
			}
		}

		obj.badge_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

