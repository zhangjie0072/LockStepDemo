using System;
using LuaInterface;

public class RechargeWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateRecharge),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("name", get_name, set_name),
			new LuaField("recharge", get_recharge, set_recharge),
			new LuaField("diamond", get_diamond, set_diamond),
			new LuaField("ext_diamond", get_ext_diamond, set_ext_diamond),
			new LuaField("first_intro", get_first_intro, set_first_intro),
			new LuaField("intro", get_intro, set_intro),
			new LuaField("recommend", get_recommend, set_recommend),
			new LuaField("icon", get_icon, set_icon),
			new LuaField("des", get_des, set_des),
		};

		LuaScriptMgr.RegisterLib(L, "Recharge", typeof(Recharge), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRecharge(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Recharge obj = new Recharge();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Recharge.New");
		}

		return 0;
	}

	static Type classType = typeof(Recharge);

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
		Recharge obj = (Recharge)o;

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
		Recharge obj = (Recharge)o;

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
	static int get_recharge(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recharge");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recharge on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.recharge);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_diamond(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.diamond);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ext_diamond(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ext_diamond");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ext_diamond on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ext_diamond);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_first_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_intro on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.first_intro);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

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
	static int get_recommend(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recommend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recommend on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.recommend);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

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
	static int get_des(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name des");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index des on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.des);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

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
		Recharge obj = (Recharge)o;

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
	static int set_recharge(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recharge");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recharge on a nil value");
			}
		}

		obj.recharge = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_diamond(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diamond");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diamond on a nil value");
			}
		}

		obj.diamond = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ext_diamond(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ext_diamond");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ext_diamond on a nil value");
			}
		}

		obj.ext_diamond = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_first_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name first_intro");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index first_intro on a nil value");
			}
		}

		obj.first_intro = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

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
	static int set_recommend(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recommend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recommend on a nil value");
			}
		}

		obj.recommend = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_icon(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

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
	static int set_des(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Recharge obj = (Recharge)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name des");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index des on a nil value");
			}
		}

		obj.des = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

