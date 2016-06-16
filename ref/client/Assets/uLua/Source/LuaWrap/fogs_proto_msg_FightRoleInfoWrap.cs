using System;
using LuaInterface;

public class fogs_proto_msg_FightRoleInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_FightRoleInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("game_mode", get_game_mode, set_game_mode),
			new LuaField("fighters", get_fighters, null),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.FightRoleInfo", typeof(fogs.proto.msg.FightRoleInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_FightRoleInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.FightRoleInfo obj = new fogs.proto.msg.FightRoleInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.FightRoleInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.FightRoleInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_game_mode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRoleInfo obj = (fogs.proto.msg.FightRoleInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name game_mode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index game_mode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.game_mode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fighters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRoleInfo obj = (fogs.proto.msg.FightRoleInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fighters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fighters on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fighters);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_game_mode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.FightRoleInfo obj = (fogs.proto.msg.FightRoleInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name game_mode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index game_mode on a nil value");
			}
		}

		obj.game_mode = (fogs.proto.msg.GameMode)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GameMode));
		return 0;
	}
}

