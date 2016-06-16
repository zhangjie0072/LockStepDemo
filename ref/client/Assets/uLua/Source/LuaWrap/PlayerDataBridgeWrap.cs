using System;
using System.Collections.Generic;
using LuaInterface;

public class PlayerDataBridgeWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreatePlayerDataBridge),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("acc_id", get_acc_id, set_acc_id),
			new LuaField("name", get_name, set_name),
			new LuaField("level", get_level, set_level),
			new LuaField("roles", get_roles, set_roles),
			new LuaField("equipInfos", get_equipInfos, set_equipInfos),
			new LuaField("squadInfos", get_squadInfos, set_squadInfos),
			new LuaField("is_room_master", get_is_room_master, set_is_room_master),
			new LuaField("is_room_ready", get_is_room_ready, set_is_room_ready),
			new LuaField("is_home_field", get_is_home_field, set_is_home_field),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerDataBridge", typeof(PlayerDataBridge), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerDataBridge(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PlayerDataBridge obj = new PlayerDataBridge();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerDataBridge.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerDataBridge);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_acc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name acc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index acc_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.acc_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roles on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.roles);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equipInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipInfos on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.equipInfos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_squadInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name squadInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index squadInfos on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.squadInfos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_room_master(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_master");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_master on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_room_master);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_room_ready(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_ready");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_ready on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_room_ready);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_home_field(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_home_field");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_home_field on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_home_field);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_acc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name acc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index acc_id on a nil value");
			}
		}

		obj.acc_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		obj.name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		obj.level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roles(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roles on a nil value");
			}
		}

		obj.roles = (List<fogs.proto.msg.RoleInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.RoleInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_equipInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipInfos on a nil value");
			}
		}

		obj.equipInfos = (List<fogs.proto.msg.EquipInfo>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.EquipInfo>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_squadInfos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name squadInfos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index squadInfos on a nil value");
			}
		}

		obj.squadInfos = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FightRole>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_room_master(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_master");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_master on a nil value");
			}
		}

		obj.is_room_master = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_room_ready(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_room_ready");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_room_ready on a nil value");
			}
		}

		obj.is_room_ready = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_home_field(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerDataBridge obj = (PlayerDataBridge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_home_field");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_home_field on a nil value");
			}
		}

		obj.is_home_field = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

