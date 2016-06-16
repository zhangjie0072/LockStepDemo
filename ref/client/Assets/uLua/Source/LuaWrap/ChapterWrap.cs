using System;
using System.Collections.Generic;
using LuaInterface;

public class ChapterWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("ChangeData", ChangeData),
			new LuaMethod("CheckSectionComplete", CheckSectionComplete),
			new LuaMethod("CheckAllSectionComplete", CheckAllSectionComplete),
			new LuaMethod("AddFirstSection", AddFirstSection),
			new LuaMethod("AddSection", AddSection),
			new LuaMethod("New", _CreateChapter),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("is_complete", get_is_complete, set_is_complete),
			new LuaField("is_bronze_awarded", get_is_bronze_awarded, set_is_bronze_awarded),
			new LuaField("is_silver_awarded", get_is_silver_awarded, set_is_silver_awarded),
			new LuaField("is_gold_awarded", get_is_gold_awarded, set_is_gold_awarded),
			new LuaField("star_num", get_star_num, set_star_num),
			new LuaField("player_id", get_player_id, set_player_id),
			new LuaField("sections", get_sections, set_sections),
		};

		LuaScriptMgr.RegisterLib(L, "Chapter", typeof(Chapter), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateChapter(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Chapter obj = new Chapter();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Chapter.New");
		}

		return 0;
	}

	static Type classType = typeof(Chapter);

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
		Chapter obj = (Chapter)o;

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
	static int get_is_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_complete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_bronze_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_bronze_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_bronze_awarded on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_bronze_awarded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_silver_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_silver_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_silver_awarded on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_silver_awarded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_gold_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_gold_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_gold_awarded on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_gold_awarded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_star_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.star_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.player_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sections(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sections");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sections on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.sections);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

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
	static int set_is_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_complete on a nil value");
			}
		}

		obj.is_complete = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_bronze_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_bronze_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_bronze_awarded on a nil value");
			}
		}

		obj.is_bronze_awarded = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_silver_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_silver_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_silver_awarded on a nil value");
			}
		}

		obj.is_silver_awarded = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_gold_awarded(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_gold_awarded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_gold_awarded on a nil value");
			}
		}

		obj.is_gold_awarded = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_star_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name star_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index star_num on a nil value");
			}
		}

		obj.star_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_id on a nil value");
			}
		}

		obj.player_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sections(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Chapter obj = (Chapter)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sections");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sections on a nil value");
			}
		}

		obj.sections = (Dictionary<uint,Section>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Section>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		fogs.proto.msg.ChapterProto arg0 = (fogs.proto.msg.ChapterProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.ChapterProto));
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.Init(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		fogs.proto.msg.ChapterProto arg0 = (fogs.proto.msg.ChapterProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.ChapterProto));
		obj.ChangeData(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.CheckSectionComplete(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckAllSectionComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		bool o = obj.CheckAllSectionComplete();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddFirstSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		bool o = obj.AddFirstSection();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddSection(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Chapter obj = (Chapter)LuaScriptMgr.GetNetObjectSelf(L, 1, "Chapter");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		bool o = obj.AddSection(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

