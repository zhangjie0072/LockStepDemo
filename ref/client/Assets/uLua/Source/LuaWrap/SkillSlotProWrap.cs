using System;
using LuaInterface;

public class SkillSlotProWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateSkillSlotPro),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("is_unlock", get_is_unlock, set_is_unlock),
			new LuaField("skill_uuid", get_skill_uuid, set_skill_uuid),
			new LuaField("skill_id", get_skill_id, set_skill_id),
			new LuaField("skill_level", get_skill_level, set_skill_level),
		};

		LuaScriptMgr.RegisterLib(L, "SkillSlotProto", typeof(fogs.proto.msg.SkillSlotProto), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillSlotPro(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.SkillSlotProto obj = new fogs.proto.msg.SkillSlotProto();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.SkillSlotProto.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.SkillSlotProto);

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
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

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
	static int get_is_unlock(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_unlock");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_unlock on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_unlock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skill_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_uuid on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.skill_uuid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skill_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.skill_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skill_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.skill_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

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

		obj.id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_unlock(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_unlock");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_unlock on a nil value");
			}
		}

		obj.is_unlock = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skill_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_uuid on a nil value");
			}
		}

		obj.skill_uuid = (ulong)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skill_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_id on a nil value");
			}
		}

		obj.skill_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skill_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.SkillSlotProto obj = (fogs.proto.msg.SkillSlotProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skill_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skill_level on a nil value");
			}
		}

		obj.skill_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

