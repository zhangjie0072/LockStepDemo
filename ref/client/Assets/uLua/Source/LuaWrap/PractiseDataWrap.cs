using System;
using System.Collections.Generic;
using LuaInterface;

public class PractiseDataWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreatePractiseData),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("ID", get_ID, set_ID),
			new LuaField("type", get_type, set_type),
			new LuaField("diffcult", get_diffcult, set_diffcult),
			new LuaField("title", get_title, set_title),
			new LuaField("intro", get_intro, set_intro),
			new LuaField("tips", get_tips, set_tips),
			new LuaField("num_total", get_num_total, set_num_total),
			new LuaField("num_complete", get_num_complete, set_num_complete),
			new LuaField("self_id", get_self_id, set_self_id),
			new LuaField("npc_id", get_npc_id, set_npc_id),
			new LuaField("scene", get_scene, set_scene),
			new LuaField("complete_sound", get_complete_sound, set_complete_sound),
			new LuaField("failed_sound", get_failed_sound, set_failed_sound),
			new LuaField("awards", get_awards, set_awards),
			new LuaField("is_activity", get_is_activity, set_is_activity),
		};

		LuaScriptMgr.RegisterLib(L, "PractiseData", typeof(PractiseData), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePractiseData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PractiseData obj = new PractiseData();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PractiseData.New");
		}

		return 0;
	}

	static Type classType = typeof(PractiseData);

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
		PractiseData obj = (PractiseData)o;

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
	static int get_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_diffcult(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diffcult");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diffcult on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.diffcult);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.title);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

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
	static int get_tips(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tips on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.tips);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_num_total(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num_total");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num_total on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.num_total);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_num_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num_complete on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.num_complete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_self_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name self_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index self_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.self_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc_id on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.npc_id);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_complete_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name complete_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index complete_sound on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.complete_sound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_failed_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name failed_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index failed_sound on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.failed_sound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_awards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.awards);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_is_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_activity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.is_activity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

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
	static int set_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (PractiseData.Type)LuaScriptMgr.GetNetObject(L, 3, typeof(PractiseData.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_diffcult(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name diffcult");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index diffcult on a nil value");
			}
		}

		obj.diffcult = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_title(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name title");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index title on a nil value");
			}
		}

		obj.title = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_intro(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

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
	static int set_tips(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tips on a nil value");
			}
		}

		obj.tips = LuaScriptMgr.GetArrayString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_num_total(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num_total");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num_total on a nil value");
			}
		}

		obj.num_total = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_num_complete(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name num_complete");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index num_complete on a nil value");
			}
		}

		obj.num_complete = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_self_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name self_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index self_id on a nil value");
			}
		}

		obj.self_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_npc_id(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name npc_id");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index npc_id on a nil value");
			}
		}

		obj.npc_id = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene on a nil value");
			}
		}

		obj.scene = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_complete_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name complete_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index complete_sound on a nil value");
			}
		}

		obj.complete_sound = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_failed_sound(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name failed_sound");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index failed_sound on a nil value");
			}
		}

		obj.failed_sound = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_awards(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name awards");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index awards on a nil value");
			}
		}

		obj.awards = (Dictionary<uint,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_is_activity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PractiseData obj = (PractiseData)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name is_activity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index is_activity on a nil value");
			}
		}

		obj.is_activity = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}
}

