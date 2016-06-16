using System;
using LuaInterface;

public class fogs_proto_config_LotteryConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_LotteryConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("type", get_type, set_type),
			new LuaField("level_min", get_level_min, set_level_min),
			new LuaField("level_max", get_level_max, set_level_max),
			new LuaField("goods_id", get_goods_id, set_goods_id),
			new LuaField("consume_id", get_consume_id, set_consume_id),
			new LuaField("consume_num_single", get_consume_num_single, set_consume_num_single),
			new LuaField("consume_num_multi", get_consume_num_multi, set_consume_num_multi),
			new LuaField("normal_award_pack", get_normal_award_pack, set_normal_award_pack),
			new LuaField("special_award_pack", get_special_award_pack, set_special_award_pack),
			new LuaField("first_multi", get_first_multi, set_first_multi),
			new LuaField("first_lott", get_first_lott, set_first_lott),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.LotteryConfig", typeof(fogs.proto.config.LotteryConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_LotteryConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.LotteryConfig obj = new fogs.proto.config.LotteryConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.LotteryConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.LotteryConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_min on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_min);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_max on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level_max);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_goods_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goods_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goods_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.goods_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume_num_single(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_num_single");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_num_single on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume_num_single);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consume_num_multi(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_num_multi");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_num_multi on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consume_num_multi);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normal_award_pack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normal_award_pack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normal_award_pack on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.normal_award_pack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_special_award_pack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_award_pack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_award_pack on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.special_award_pack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_first_multi(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_multi");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_multi on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.first_multi);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_first_lott(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_lott");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_lott on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.first_lott);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_min(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_min");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_min on a nil value");
			}
		}

		obj.level_min = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level_max(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level_max");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level_max on a nil value");
			}
		}

		obj.level_max = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_goods_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goods_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goods_id on a nil value");
			}
		}

		obj.goods_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_id on a nil value");
			}
		}

		obj.consume_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume_num_single(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_num_single");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_num_single on a nil value");
			}
		}

		obj.consume_num_single = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consume_num_multi(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consume_num_multi");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consume_num_multi on a nil value");
			}
		}

		obj.consume_num_multi = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_normal_award_pack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normal_award_pack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normal_award_pack on a nil value");
			}
		}

		obj.normal_award_pack = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_special_award_pack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name special_award_pack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index special_award_pack on a nil value");
			}
		}

		obj.special_award_pack = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_first_multi(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_multi");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_multi on a nil value");
			}
		}

		obj.first_multi = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_first_lott(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.LotteryConfig obj = (fogs.proto.config.LotteryConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_lott");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_lott on a nil value");
			}
		}

		obj.first_lott = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

