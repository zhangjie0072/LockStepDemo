using System;
using LuaInterface;

public class PracticePveDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreatePracticePveData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("ID", get_ID, set_ID),
			new LuaField("gamemode", get_gamemode, set_gamemode),
			new LuaField("awardpack", get_awardpack, set_awardpack),
		};

		LuaScriptMgr.RegisterLib(L, "PracticePveData", typeof(PracticePveData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePracticePveData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PracticePveData obj = new PracticePveData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PracticePveData.New");
		}

		return 0;
	}

	static Type classType = typeof(PracticePveData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.ID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gamemode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gamemode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gamemode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.gamemode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awardpack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardpack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardpack on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.awardpack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ID on a nil value");
			}
		}

		obj.ID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gamemode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gamemode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gamemode on a nil value");
			}
		}

		obj.gamemode = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awardpack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PracticePveData obj = (PracticePveData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awardpack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awardpack on a nil value");
			}
		}

		obj.awardpack = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

