using System;
using LuaInterface;

public class fogs_proto_config_ChapterConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_config_ChapterConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("next_chapter_id", get_next_chapter_id, set_next_chapter_id),
			new LuaField("first_section_id", get_first_section_id, set_first_section_id),
			new LuaField("area", get_area, set_area),
			new LuaField("unlock_level", get_unlock_level, set_unlock_level),
			new LuaField("bronze_value", get_bronze_value, set_bronze_value),
			new LuaField("bronze_award", get_bronze_award, set_bronze_award),
			new LuaField("silver_value", get_silver_value, set_silver_value),
			new LuaField("silver_award", get_silver_award, set_silver_award),
			new LuaField("gold_value", get_gold_value, set_gold_value),
			new LuaField("gold_award", get_gold_award, set_gold_award),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.config.ChapterConfig", typeof(fogs.proto.config.ChapterConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_config_ChapterConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.config.ChapterConfig obj = new fogs.proto.config.ChapterConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.config.ChapterConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.config.ChapterConfig);

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
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

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
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

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
	static int get_next_chapter_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name next_chapter_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index next_chapter_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.next_chapter_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_first_section_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_section_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_section_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.first_section_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_area(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name area");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index area on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.area);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unlock_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.unlock_level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bronze_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bronze_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bronze_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bronze_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bronze_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bronze_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bronze_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bronze_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_silver_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name silver_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index silver_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.silver_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_silver_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name silver_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index silver_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.silver_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gold_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_value on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gold_value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gold_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_award on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gold_award);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

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
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

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
	static int set_next_chapter_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name next_chapter_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index next_chapter_id on a nil value");
			}
		}

		obj.next_chapter_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_first_section_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_section_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_section_id on a nil value");
			}
		}

		obj.first_section_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_area(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name area");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index area on a nil value");
			}
		}

		obj.area = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unlock_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unlock_level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unlock_level on a nil value");
			}
		}

		obj.unlock_level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bronze_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bronze_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bronze_value on a nil value");
			}
		}

		obj.bronze_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bronze_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bronze_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bronze_award on a nil value");
			}
		}

		obj.bronze_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_silver_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name silver_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index silver_value on a nil value");
			}
		}

		obj.silver_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_silver_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name silver_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index silver_award on a nil value");
			}
		}

		obj.silver_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gold_value(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_value on a nil value");
			}
		}

		obj.gold_value = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gold_award(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.config.ChapterConfig obj = (fogs.proto.config.ChapterConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gold_award");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gold_award on a nil value");
			}
		}

		obj.gold_award = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

