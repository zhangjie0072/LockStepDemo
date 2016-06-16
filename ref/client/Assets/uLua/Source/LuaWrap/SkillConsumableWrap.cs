using System;
using LuaInterface;

public class SkillConsumableWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateSkillConsumable),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("consumable_id", get_consumable_id, set_consumable_id),
			new LuaField("consumable_quantity", get_consumable_quantity, set_consumable_quantity),
		};

		LuaScriptMgr.RegisterLib(L, "SkillConsumable", typeof(SkillConsumable), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSkillConsumable(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SkillConsumable obj = new SkillConsumable();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SkillConsumable.New");
		}

		return 0;
	}

	static Type classType = typeof(SkillConsumable);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consumable_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConsumable obj = (SkillConsumable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumable_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumable_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consumable_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consumable_quantity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConsumable obj = (SkillConsumable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumable_quantity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumable_quantity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consumable_quantity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consumable_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConsumable obj = (SkillConsumable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumable_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumable_id on a nil value");
			}
		}

		obj.consumable_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consumable_quantity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SkillConsumable obj = (SkillConsumable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumable_quantity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumable_quantity on a nil value");
			}
		}

		obj.consumable_quantity = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

