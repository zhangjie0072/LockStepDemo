using System;
using LuaInterface;

public class fogs_proto_msg_SignInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_SignInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("signed", get_signed, set_signed),
			new LuaField("append_sign_times", get_append_sign_times, set_append_sign_times),
			new LuaField("sign_award", get_sign_award, set_sign_award),
			new LuaField("sign_list", get_sign_list, null),
			new LuaField("signed_times", get_signed_times, set_signed_times),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.SignInfo", typeof(fogs.proto.msg.SignInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_SignInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.SignInfo obj = new fogs.proto.msg.SignInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.SignInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.SignInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_signed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.signed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_append_sign_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name append_sign_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index append_sign_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.append_sign_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sign_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_list");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_list on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.sign_list);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_signed_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signed_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signed_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.signed_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_signed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signed on a nil value");
			}
		}

		obj.signed = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_append_sign_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name append_sign_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index append_sign_times on a nil value");
			}
		}

		obj.append_sign_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sign_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sign_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sign_award on a nil value");
			}
		}

		obj.sign_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_signed_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SignInfo obj = (fogs.proto.msg.SignInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name signed_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index signed_times on a nil value");
			}
		}

		obj.signed_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

