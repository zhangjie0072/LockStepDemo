using System;
using LuaInterface;

public class fogs_proto_msg_GameModeInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_GameModeInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("game_mode", get_game_mode, set_game_mode),
			new LuaField("times", get_times, set_times),
			new LuaField("npc", get_npc, set_npc),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GameModeInfo", typeof(fogs.proto.msg.GameModeInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_GameModeInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.GameModeInfo obj = new fogs.proto.msg.GameModeInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.GameModeInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.GameModeInfo);

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
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

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
	static int get_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

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
	static int get_npc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.npc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_game_mode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

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

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

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
	static int set_npc(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GameModeInfo obj = (fogs.proto.msg.GameModeInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc on a nil value");
			}
		}

		obj.npc = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

