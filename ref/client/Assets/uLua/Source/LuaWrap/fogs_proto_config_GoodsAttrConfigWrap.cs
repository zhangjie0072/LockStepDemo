using System;
using LuaInterface;

public class fogs_proto_config_GoodsAttrConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_GoodsAttrConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("purpose", get_purpose, set_purpose),
			new LuaField("intro", get_intro, set_intro),
			new LuaField("category", get_category, set_category),
			new LuaField("sub_category", get_sub_category, set_sub_category),
			new LuaField("gender", get_gender, set_gender),
			new LuaField("suit_id", get_suit_id, set_suit_id),
			new LuaField("suit_addn_attr", get_suit_addn_attr, set_suit_addn_attr),
			new LuaField("suit_multi_attr", get_suit_multi_attr, set_suit_multi_attr),
			new LuaField("quality", get_quality, set_quality),
			new LuaField("can_use", get_can_use, set_can_use),
			new LuaField("use_result_id", get_use_result_id, set_use_result_id),
			new LuaField("stack_num", get_stack_num, set_stack_num),
			new LuaField("can_sell", get_can_sell, set_can_sell),
			new LuaField("sell_id", get_sell_id, set_sell_id),
			new LuaField("sell_price", get_sell_price, set_sell_price),
			new LuaField("can_composite", get_can_composite, set_can_composite),
			new LuaField("access_way_type", get_access_way_type, set_access_way_type),
			new LuaField("access_way", get_access_way, set_access_way),
			new LuaField("show_special_effect", get_show_special_effect, set_show_special_effect),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.GoodsAttrConfig", typeof(fogs.proto.config.GoodsAttrConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_GoodsAttrConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.GoodsAttrConfig obj = new fogs.proto.config.GoodsAttrConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.GoodsAttrConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.GoodsAttrConfig);

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
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.icon);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_purpose(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name purpose");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index purpose on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.purpose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intro on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.intro);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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
	static int get_sub_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_category on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sub_category);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gender on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_suit_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.suit_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_suit_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_addn_attr on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.suit_addn_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_suit_multi_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_multi_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_multi_attr on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.suit_multi_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_can_use(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_use");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_use on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.can_use);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_use_result_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name use_result_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index use_result_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.use_result_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stack_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stack_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stack_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.stack_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_can_sell(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_sell");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_sell on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.can_sell);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sell_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sell_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sell_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_price on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sell_price);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_can_composite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_composite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_composite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.can_composite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_access_way_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.access_way_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_access_way(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.access_way);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_show_special_effect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name show_special_effect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index show_special_effect on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.show_special_effect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name icon");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index icon on a nil value");
			}
		}

		obj.icon = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_purpose(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name purpose");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index purpose on a nil value");
			}
		}

		obj.purpose = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index intro on a nil value");
			}
		}

		obj.intro = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

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

		obj.category = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sub_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sub_category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sub_category on a nil value");
			}
		}

		obj.sub_category = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gender(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gender");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gender on a nil value");
			}
		}

		obj.gender = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_suit_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_id on a nil value");
			}
		}

		obj.suit_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_suit_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_addn_attr on a nil value");
			}
		}

		obj.suit_addn_attr = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_suit_multi_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suit_multi_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suit_multi_attr on a nil value");
			}
		}

		obj.suit_multi_attr = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		obj.quality = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_can_use(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_use");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_use on a nil value");
			}
		}

		obj.can_use = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_use_result_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name use_result_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index use_result_id on a nil value");
			}
		}

		obj.use_result_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stack_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stack_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stack_num on a nil value");
			}
		}

		obj.stack_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_can_sell(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_sell");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_sell on a nil value");
			}
		}

		obj.can_sell = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sell_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_id on a nil value");
			}
		}

		obj.sell_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sell_price(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sell_price");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sell_price on a nil value");
			}
		}

		obj.sell_price = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_can_composite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name can_composite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index can_composite on a nil value");
			}
		}

		obj.can_composite = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_access_way_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way_type on a nil value");
			}
		}

		obj.access_way_type = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_access_way(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name access_way");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index access_way on a nil value");
			}
		}

		obj.access_way = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_show_special_effect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.GoodsAttrConfig obj = (fogs.proto.config.GoodsAttrConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name show_special_effect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index show_special_effect on a nil value");
			}
		}

		obj.show_special_effect = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

