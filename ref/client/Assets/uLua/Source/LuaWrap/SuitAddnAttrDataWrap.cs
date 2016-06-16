using System;
using System.Collections.Generic;
using LuaInterface;

public class SuitAddnAttrDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateSuitAddnAttrData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("goodsID", get_goodsID, set_goodsID),
			new LuaField("suitID", get_suitID, set_suitID),
			new LuaField("parts", get_parts, set_parts),
			new LuaField("addn_attr", get_addn_attr, set_addn_attr),
			new LuaField("multi_attr", get_multi_attr, set_multi_attr),
		};

		LuaScriptMgr.RegisterLib(L, "SuitAddnAttrData", typeof(SuitAddnAttrData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSuitAddnAttrData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			SuitAddnAttrData obj = new SuitAddnAttrData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SuitAddnAttrData.New");
		}

		return 0;
	}

	static Type classType = typeof(SuitAddnAttrData);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_goodsID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goodsID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goodsID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.goodsID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_suitID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suitID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suitID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.suitID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parts on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.parts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addn_attr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.addn_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_multi_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name multi_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index multi_attr on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.multi_attr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_goodsID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name goodsID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index goodsID on a nil value");
			}
		}

		obj.goodsID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_suitID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name suitID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index suitID on a nil value");
			}
		}

		obj.suitID = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_parts(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parts on a nil value");
			}
		}

		obj.parts = (List<uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_addn_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name addn_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index addn_attr on a nil value");
			}
		}

		obj.addn_attr = (Dictionary<uint,Dictionary<uint,uint>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Dictionary<uint,uint>>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_multi_attr(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		SuitAddnAttrData obj = (SuitAddnAttrData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name multi_attr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index multi_attr on a nil value");
			}
		}

		obj.multi_attr = (Dictionary<uint,Dictionary<uint,uint>>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Dictionary<uint,uint>>));
		return 0;
	}
}

