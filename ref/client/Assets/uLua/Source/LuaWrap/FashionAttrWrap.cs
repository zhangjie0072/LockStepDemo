using System;
using LuaInterface;

public class FashionAttrWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateFashionAttr),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("attr_id", get_attr_id, set_attr_id),
			new LuaField("player_attr_id", get_player_attr_id, set_player_attr_id),
			new LuaField("player_attr_num", get_player_attr_num, set_player_attr_num),
		};

		LuaScriptMgr.RegisterLib(L, "FashionAttr", typeof(FashionAttr), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFashionAttr(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FashionAttr obj = new FashionAttr();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionAttr.New");
		}

		return 0;
	}

	static Type classType = typeof(FashionAttr);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_attr_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attr_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attr_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.attr_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player_attr_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_attr_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_attr_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.player_attr_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player_attr_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_attr_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_attr_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.player_attr_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_attr_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attr_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attr_id on a nil value");
			}
		}

		obj.attr_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player_attr_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_attr_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_attr_id on a nil value");
			}
		}

		obj.player_attr_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player_attr_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionAttr obj = (FashionAttr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_attr_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_attr_num on a nil value");
			}
		}

		obj.player_attr_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

