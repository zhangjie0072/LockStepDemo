using System;
using LuaInterface;

public class fogs_proto_msg_NewComerInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_NewComerInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sign_list", get_sign_list, null),
			new LuaField("open_flag", get_open_flag, set_open_flag),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.NewComerInfo", typeof(fogs.proto.msg.NewComerInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_NewComerInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.NewComerInfo obj = new fogs.proto.msg.NewComerInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.NewComerInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.NewComerInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sign_list(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerInfo obj = (fogs.proto.msg.NewComerInfo)o;

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
	static int get_open_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerInfo obj = (fogs.proto.msg.NewComerInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name open_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index open_flag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.open_flag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_open_flag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.NewComerInfo obj = (fogs.proto.msg.NewComerInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name open_flag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index open_flag on a nil value");
			}
		}

		obj.open_flag = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

