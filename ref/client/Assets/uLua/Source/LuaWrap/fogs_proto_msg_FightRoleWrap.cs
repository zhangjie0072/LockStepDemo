using System;
using LuaInterface;

public class fogs_proto_msg_FightRoleWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_FightRole),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("role_id", get_role_id, set_role_id),
			new LuaField("status", get_status, set_status),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FightRole", typeof(fogs.proto.msg.FightRole), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_FightRole(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.FightRole obj = new fogs.proto.msg.FightRole();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.FightRole.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.FightRole);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRole obj = (fogs.proto.msg.FightRole)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.role_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_status(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRole obj = (fogs.proto.msg.FightRole)o;

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
	static int set_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRole obj = (fogs.proto.msg.FightRole)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name role_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index role_id on a nil value");
			}
		}

		obj.role_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_status(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRole obj = (fogs.proto.msg.FightRole)o;

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

		obj.status = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.FightStatus));
		return 0;
	}
}

