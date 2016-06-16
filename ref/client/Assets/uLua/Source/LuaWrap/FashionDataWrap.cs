using System;
using LuaInterface;

public class FashionDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateFashionData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("fashion_id", get_fashion_id, set_fashion_id),
			new LuaField("reset_id", get_reset_id, set_reset_id),
			new LuaField("reset_num", get_reset_num, set_reset_num),
		};

		LuaScriptMgr.RegisterLib(L, "FashionData", typeof(FashionData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateFashionData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			FashionData obj = new FashionData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: FashionData.New");
		}

		return 0;
	}

	static Type classType = typeof(FashionData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fashion_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashion_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashion_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.fashion_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reset_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.reset_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reset_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.reset_num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fashion_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashion_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashion_id on a nil value");
			}
		}

		obj.fashion_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reset_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_id on a nil value");
			}
		}

		obj.reset_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reset_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		FashionData obj = (FashionData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reset_num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reset_num on a nil value");
			}
		}

		obj.reset_num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

