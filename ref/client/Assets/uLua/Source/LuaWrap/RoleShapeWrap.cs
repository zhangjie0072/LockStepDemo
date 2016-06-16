using System;
using LuaInterface;

public class RoleShapeWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateRoleShape),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("roleShapeId", get_roleShapeId, set_roleShapeId),
			new LuaField("height", get_height, set_height),
			new LuaField("body_id", get_body_id, set_body_id),
			new LuaField("hair_id", get_hair_id, set_hair_id),
			new LuaField("upper_id", get_upper_id, set_upper_id),
			new LuaField("lower_id", get_lower_id, set_lower_id),
			new LuaField("shoes_id", get_shoes_id, set_shoes_id),
			new LuaField("back_id", get_back_id, set_back_id),
		};

		LuaScriptMgr.RegisterLib(L, "RoleShape", typeof(RoleShape), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRoleShape(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			RoleShape obj = new RoleShape();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RoleShape.New");
		}

		return 0;
	}

	static Type classType = typeof(RoleShape);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roleShapeId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roleShapeId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roleShapeId on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.roleShapeId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.height);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_body_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name body_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index body_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.body_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hair_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hair_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hair_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.hair_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_upper_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name upper_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index upper_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.upper_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lower_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lower_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lower_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.lower_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shoes_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shoes_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shoes_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.shoes_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_back_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name back_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index back_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.back_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roleShapeId(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name roleShapeId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index roleShapeId on a nil value");
			}
		}

		obj.roleShapeId = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		obj.height = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_body_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name body_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index body_id on a nil value");
			}
		}

		obj.body_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hair_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hair_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hair_id on a nil value");
			}
		}

		obj.hair_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_upper_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name upper_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index upper_id on a nil value");
			}
		}

		obj.upper_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lower_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lower_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lower_id on a nil value");
			}
		}

		obj.lower_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shoes_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shoes_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shoes_id on a nil value");
			}
		}

		obj.shoes_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_back_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RoleShape obj = (RoleShape)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name back_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index back_id on a nil value");
			}
		}

		obj.back_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

