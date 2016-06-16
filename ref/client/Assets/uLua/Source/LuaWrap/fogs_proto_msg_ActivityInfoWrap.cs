using System;
using LuaInterface;

public class fogs_proto_msg_ActivityInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_ActivityInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("gift", get_gift, null),
			new LuaField("activity", get_activity, set_activity),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.ActivityInfo", typeof(fogs.proto.msg.ActivityInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_ActivityInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.ActivityInfo obj = new fogs.proto.msg.ActivityInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.ActivityInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.ActivityInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gift(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ActivityInfo obj = (fogs.proto.msg.ActivityInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gift");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gift on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.gift);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ActivityInfo obj = (fogs.proto.msg.ActivityInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.activity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.ActivityInfo obj = (fogs.proto.msg.ActivityInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activity on a nil value");
			}
		}

		obj.activity = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

