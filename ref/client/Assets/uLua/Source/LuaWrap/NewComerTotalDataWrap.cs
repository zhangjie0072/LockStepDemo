using System;
using System.Collections.Generic;
using LuaInterface;

public class NewComerTotalDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateNewComerTotalData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("totalAwards", get_totalAwards, set_totalAwards),
			new LuaField("descript", get_descript, set_descript),
		};

		LuaScriptMgr.RegisterLib(L, "NewComerTotalData", typeof(NewComerTotalData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNewComerTotalData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NewComerTotalData obj = new NewComerTotalData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NewComerTotalData.New");
		}

		return 0;
	}

	static Type classType = typeof(NewComerTotalData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_totalAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerTotalData obj = (NewComerTotalData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name totalAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index totalAwards on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.totalAwards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_descript(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerTotalData obj = (NewComerTotalData)o;

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
	static int set_totalAwards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerTotalData obj = (NewComerTotalData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name totalAwards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index totalAwards on a nil value");
			}
		}

		obj.totalAwards = (List<string>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<string>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_descript(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NewComerTotalData obj = (NewComerTotalData)o;

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

