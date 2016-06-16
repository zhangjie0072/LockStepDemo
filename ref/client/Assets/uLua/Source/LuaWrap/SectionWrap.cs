using System;
using LuaInterface;

public class SectionWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("ChangeData", ChangeData),
			new LuaMethod("New", _CreateSection),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("id", get_id, set_id),
			new LuaField("is_complete", get_is_complete, set_is_complete),
			new LuaField("challenge_times", get_challenge_times, set_challenge_times),
			new LuaField("buy_times", get_buy_times, set_buy_times),
			new LuaField("medal_rank", get_medal_rank, set_medal_rank),
			new LuaField("player_id", get_player_id, set_player_id),
			new LuaField("get_role", get_get_role, set_get_role),
			new LuaField("is_activate", get_is_activate, set_is_activate),
		};

		LuaScriptMgr.RegisterLib(L, "Section", typeof(Section), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSection(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Section obj = new Section();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Section.New");
		}

		return 0;
	}

	static Type classType = typeof(Section);

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
		Section obj = (Section)o;

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
	static int get_is_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_complete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_challenge_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challenge_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challenge_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.challenge_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_times on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.buy_times);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_medal_rank(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name medal_rank");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index medal_rank on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.medal_rank);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.player_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_get_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_role");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_role on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.get_role);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_activate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_activate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_activate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_activate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

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
	static int set_is_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_complete on a nil value");
			}
		}

		obj.is_complete = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_challenge_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name challenge_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index challenge_times on a nil value");
			}
		}

		obj.challenge_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_buy_times(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buy_times");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buy_times on a nil value");
			}
		}

		obj.buy_times = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_medal_rank(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name medal_rank");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index medal_rank on a nil value");
			}
		}

		obj.medal_rank = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player_id on a nil value");
			}
		}

		obj.player_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_get_role(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name get_role");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index get_role on a nil value");
			}
		}

		obj.get_role = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_activate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Section obj = (Section)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_activate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_activate on a nil value");
			}
		}

		obj.is_activate = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Section obj = (Section)LuaScriptMgr.GetNetObjectSelf(L, 1, "Section");
		fogs.proto.msg.SectionProto arg0 = (fogs.proto.msg.SectionProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.SectionProto));
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.Init(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangeData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Section obj = (Section)LuaScriptMgr.GetNetObjectSelf(L, 1, "Section");
		fogs.proto.msg.SectionProto arg0 = (fogs.proto.msg.SectionProto)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.SectionProto));
		obj.ChangeData(arg0);
		return 0;
	}
}

