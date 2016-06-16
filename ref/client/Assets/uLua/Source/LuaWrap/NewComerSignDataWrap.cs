using System;
using LuaInterface;

public class NewComerSignDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateNewComerSignData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("awardType", get_awardType, set_awardType),
			new LuaField("awardNum", get_awardNum, set_awardNum),
			new LuaField("consumeType", get_consumeType, set_consumeType),
			new LuaField("consumeNum", get_consumeNum, set_consumeNum),
			new LuaField("descript", get_descript, set_descript),
		};

		LuaScriptMgr.RegisterLib(L, "NewComerSignData", typeof(NewComerSignData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNewComerSignData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NewComerSignData obj = new NewComerSignData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NewComerSignData.New");
		}

		return 0;
	}

	static Type classType = typeof(NewComerSignData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awardType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awardNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consumeType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumeType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumeType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consumeType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_consumeNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumeNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumeNum on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.consumeNum);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_descript(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name descript");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index descript on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.descript);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardType on a nil value");
			}
		}

		obj.awardType = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardNum on a nil value");
			}
		}

		obj.awardNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consumeType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumeType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumeType on a nil value");
			}
		}

		obj.consumeType = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_consumeNum(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name consumeNum");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index consumeNum on a nil value");
			}
		}

		obj.consumeNum = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_descript(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerSignData obj = (NewComerSignData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name descript");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index descript on a nil value");
			}
		}

		obj.descript = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

