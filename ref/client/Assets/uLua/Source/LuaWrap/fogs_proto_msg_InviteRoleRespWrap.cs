using System;
using LuaInterface;

public class fogs_proto_msg_InviteRoleRespWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_InviteRoleResp),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("result", get_result, set_result),
			new LuaField("role", get_role, set_role),
			new LuaField("cur_pieces", get_cur_pieces, set_cur_pieces),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.InviteRoleResp", typeof(fogs.proto.msg.InviteRoleResp), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_InviteRoleResp(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.InviteRoleResp obj = new fogs.proto.msg.InviteRoleResp();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.InviteRoleResp.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.InviteRoleResp);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_result(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name result");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index result on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.result);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

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
	static int get_cur_pieces(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cur_pieces");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cur_pieces on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cur_pieces);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_result(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name result");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index result on a nil value");
			}
		}

		obj.result = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

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

		obj.role = (fogs.proto.msg.RoleInfo)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.RoleInfo));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cur_pieces(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.InviteRoleResp obj = (fogs.proto.msg.InviteRoleResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cur_pieces");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cur_pieces on a nil value");
			}
		}

		obj.cur_pieces = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

