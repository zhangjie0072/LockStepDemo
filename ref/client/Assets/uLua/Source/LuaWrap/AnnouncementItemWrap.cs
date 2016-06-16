using System;
using LuaInterface;

public class AnnouncementItemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateAnnouncementItem),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("title", get_title, set_title),
			new LuaField("label", get_label, set_label),
			new LuaField("info", get_info, set_info),
			new LuaField("is_open", get_is_open, set_is_open),
			new LuaField("version", get_version, set_version),
		};

		LuaScriptMgr.RegisterLib(L, "AnnouncementItem", typeof(AnnouncementItem), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAnnouncementItem(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AnnouncementItem obj = new AnnouncementItem();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AnnouncementItem.New");
		}

		return 0;
	}

	static Type classType = typeof(AnnouncementItem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.title);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_label(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name label");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index label on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.label);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index info on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.info);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_open(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_open");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_open on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_open);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_version(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name version");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index version on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.version);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		obj.title = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_label(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name label");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index label on a nil value");
			}
		}

		obj.label = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_info(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name info");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index info on a nil value");
			}
		}

		obj.info = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_open(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_open");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_open on a nil value");
			}
		}

		obj.is_open = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_version(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AnnouncementItem obj = (AnnouncementItem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name version");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index version on a nil value");
			}
		}

		obj.version = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

