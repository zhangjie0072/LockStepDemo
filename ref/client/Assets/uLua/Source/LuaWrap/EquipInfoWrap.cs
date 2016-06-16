using System;
using LuaInterface;

public class EquipInfoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateEquipInfo),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("pos", get_pos, set_pos),
			new LuaField("slot_info", get_slot_info, null),
		};

		LuaScriptMgr.RegisterLib(L, "EquipInfo", typeof(fogs.proto.msg.EquipInfo), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateEquipInfo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.EquipInfo obj = new fogs.proto.msg.EquipInfo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.EquipInfo.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.EquipInfo);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipInfo obj = (fogs.proto.msg.EquipInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pos on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_slot_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipInfo obj = (fogs.proto.msg.EquipInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slot_info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slot_info on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.slot_info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipInfo obj = (fogs.proto.msg.EquipInfo)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pos on a nil value");
			}
		}

		obj.pos = (fogs.proto.msg.FightStatus)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.FightStatus));
		return 0;
	}
}

