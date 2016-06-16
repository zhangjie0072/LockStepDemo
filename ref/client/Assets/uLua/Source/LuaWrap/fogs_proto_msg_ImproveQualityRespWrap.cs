using System;
using LuaInterface;

public class fogs_proto_msg_ImproveQualityRespWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_ImproveQualityResp),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("result", get_result, set_result),
			new LuaField("role_id", get_role_id, set_role_id),
			new LuaField("new_quality", get_new_quality, set_new_quality),
			new LuaField("cur_pieces", get_cur_pieces, set_cur_pieces),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.ImproveQualityResp", typeof(fogs.proto.msg.ImproveQualityResp), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_ImproveQualityResp(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.ImproveQualityResp obj = new fogs.proto.msg.ImproveQualityResp();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.ImproveQualityResp.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.ImproveQualityResp);

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
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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
	static int get_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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
	static int get_new_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name new_quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index new_quality on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.new_quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cur_pieces(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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
	static int set_role_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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
	static int set_new_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name new_quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index new_quality on a nil value");
			}
		}

		obj.new_quality = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cur_pieces(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ImproveQualityResp obj = (fogs.proto.msg.ImproveQualityResp)o;

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

