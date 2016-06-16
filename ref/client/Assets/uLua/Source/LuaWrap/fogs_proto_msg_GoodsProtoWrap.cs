using System;
using LuaInterface;

public class fogs_proto_msg_GoodsProtoWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _Createfogs_proto_msg_GoodsProto),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("uuid", get_uuid, set_uuid),
			new LuaField("id", get_id, set_id),
			new LuaField("category", get_category, set_category),
			new LuaField("num", get_num, set_num),
			new LuaField("level", get_level, set_level),
			new LuaField("quality", get_quality, set_quality),
			new LuaField("is_equip", get_is_equip, set_is_equip),
			new LuaField("time_left", get_time_left, set_time_left),
			new LuaField("is_used", get_is_used, set_is_used),
			new LuaField("exp", get_exp, set_exp),
			new LuaField("fashion_attr_id", get_fashion_attr_id, null),
		};

		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GoodsProto", typeof(fogs.proto.msg.GoodsProto), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createfogs_proto_msg_GoodsProto(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			fogs.proto.msg.GoodsProto obj = new fogs.proto.msg.GoodsProto();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: fogs.proto.msg.GoodsProto.New");
		}

		return 0;
	}

	static Type classType = typeof(fogs.proto.msg.GoodsProto);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uuid on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.uuid);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

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
	static int get_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index category on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.category);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.num);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.level);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.quality);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_equip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_equip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_equip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_equip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time_left(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time_left");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time_left on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.time_left);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_used(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_used");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_used on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_used);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.exp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fashion_attr_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fashion_attr_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fashion_attr_id on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.fashion_attr_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uuid(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uuid");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uuid on a nil value");
			}
		}

		obj.uuid = (ulong)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

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
	static int set_category(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name category");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index category on a nil value");
			}
		}

		obj.category = (fogs.proto.msg.GoodsCategory)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GoodsCategory));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_num(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num on a nil value");
			}
		}

		obj.num = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name level");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index level on a nil value");
			}
		}

		obj.level = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_quality(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}

		obj.quality = (fogs.proto.msg.GoodsQuality)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GoodsQuality));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_equip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_equip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_equip on a nil value");
			}
		}

		obj.is_equip = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time_left(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time_left");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time_left on a nil value");
			}
		}

		obj.time_left = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_used(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_used");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_used on a nil value");
			}
		}

		obj.is_used = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_exp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		fogs.proto.msg.GoodsProto obj = (fogs.proto.msg.GoodsProto)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name exp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index exp on a nil value");
			}
		}

		obj.exp = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

