using System;
using LuaInterface;

public class BadgeSlotBaseConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateBadgeSlotBaseConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("category", get_category, set_category),
			new LuaField("requireLevel", get_requireLevel, set_requireLevel),
			new LuaField("layoutPosx", get_layoutPosx, set_layoutPosx),
			new LuaField("layoutPosy", get_layoutPosy, set_layoutPosy),
			new LuaField("unlockCostGoodsId", get_unlockCostGoodsId, set_unlockCostGoodsId),
			new LuaField("unlockCostGoodsNum", get_unlockCostGoodsNum, set_unlockCostGoodsNum),
		};

		LuaScriptMgr.RegisterLib(L, "BadgeSlotBaseConfig", typeof(BadgeSlotBaseConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBadgeSlotBaseConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BadgeSlotBaseConfig obj = new BadgeSlotBaseConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BadgeSlotBaseConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(BadgeSlotBaseConfig);

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
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

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
	static int get_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

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
	static int get_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index category on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.category);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_requireLevel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name requireLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index requireLevel on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.requireLevel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layoutPosx(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPosx");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPosx on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.layoutPosx);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layoutPosy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPosy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPosy on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.layoutPosy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlockCostGoodsId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockCostGoodsId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockCostGoodsId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unlockCostGoodsId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlockCostGoodsNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockCostGoodsNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockCostGoodsNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unlockCostGoodsNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

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
	static int set_name(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

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
	static int set_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index category on a nil value");
			}
		}

		obj.category = (fogs.proto.msg.BadgeCG)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.BadgeCG));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_requireLevel(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name requireLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index requireLevel on a nil value");
			}
		}

		obj.requireLevel = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layoutPosx(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPosx");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPosx on a nil value");
			}
		}

		obj.layoutPosx = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layoutPosy(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPosy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPosy on a nil value");
			}
		}

		obj.layoutPosy = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlockCostGoodsId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockCostGoodsId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockCostGoodsId on a nil value");
			}
		}

		obj.unlockCostGoodsId = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlockCostGoodsNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		BadgeSlotBaseConfig obj = (BadgeSlotBaseConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlockCostGoodsNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlockCostGoodsNum on a nil value");
			}
		}

		obj.unlockCostGoodsNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

