using System;
using LuaInterface;

public class EquipmentSlotWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateEquipmentSlot),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("equipment_uuid", get_equipment_uuid, set_equipment_uuid),
			new LuaField("equipment_id", get_equipment_id, set_equipment_id),
			new LuaField("equipment_level", get_equipment_level, set_equipment_level),
		};

		LuaScriptMgr.RegisterLib(L, "EquipmentSlot", typeof(fogs.proto.msg.EquipmentSlot), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateEquipmentSlot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.EquipmentSlot obj = new fogs.proto.msg.EquipmentSlot();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.EquipmentSlot.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.EquipmentSlot);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equipment_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_uuid on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.equipment_uuid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equipment_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.equipment_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_equipment_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.equipment_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index id on a nil value");
			}
		}

		obj.id = (fogs.proto.msg.EquipmentSlotID)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.EquipmentSlotID));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_equipment_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_uuid on a nil value");
			}
		}

		obj.equipment_uuid = (ulong)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_equipment_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_id on a nil value");
			}
		}

		obj.equipment_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_equipment_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.EquipmentSlot obj = (fogs.proto.msg.EquipmentSlot)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name equipment_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index equipment_level on a nil value");
			}
		}

		obj.equipment_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

