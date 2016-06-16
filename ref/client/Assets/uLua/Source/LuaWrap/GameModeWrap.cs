using System;
using System.Collections.Generic;
using LuaInterface;

public class GameModeWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetMappedNPC", GetMappedNPC),
			new LuaMethod("New", _CreateGameMode),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("ID", get_ID, set_ID),
			new LuaField("level", get_level, set_level),
			new LuaField("matchType", get_matchType, set_matchType),
			new LuaField("time", get_time, set_time),
			new LuaField("scene", get_scene, set_scene),
			new LuaField("mappedNPC", get_mappedNPC, set_mappedNPC),
			new LuaField("unmappedNPC", get_unmappedNPC, set_unmappedNPC),
			new LuaField("additionalInfo", get_additionalInfo, set_additionalInfo),
			new LuaField("AIDelay", get_AIDelay, set_AIDelay),
			new LuaField("rushStamina", get_rushStamina, set_rushStamina),
			new LuaField("repositionDist", get_repositionDist, set_repositionDist),
			new LuaField("skillProbs", get_skillProbs, set_skillProbs),
			new LuaField("extraLevelInfo", get_extraLevelInfo, set_extraLevelInfo),
		};

		LuaScriptMgr.RegisterLib(L, "GameMode", typeof(GameMode), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameMode(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameMode obj = new GameMode();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameMode.New");
		}

		return 0;
	}

	static Type classType = typeof(GameMode);

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
		GameMode obj = (GameMode)o;

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
	static int get_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

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
	static int get_matchType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.matchType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

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
	static int get_mappedNPC(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mappedNPC");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mappedNPC on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mappedNPC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unmappedNPC(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unmappedNPC");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unmappedNPC on a nil value");
			}
		}

		LuaScriptMgr.PushArray(L, obj.unmappedNPC);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_additionalInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additionalInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additionalInfo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.additionalInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AIDelay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AIDelay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AIDelay on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.AIDelay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rushStamina(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rushStamina");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rushStamina on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rushStamina);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_repositionDist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name repositionDist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index repositionDist on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.repositionDist);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_skillProbs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillProbs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillProbs on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.skillProbs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_extraLevelInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extraLevelInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extraLevelInfo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.extraLevelInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

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
	static int set_level(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

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

		obj.level = (GameMatch.Level)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Level));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_matchType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name matchType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index matchType on a nil value");
			}
		}

		obj.matchType = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMatch.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}

		obj.time = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scene(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

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
	static int set_mappedNPC(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mappedNPC");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mappedNPC on a nil value");
			}
		}

		obj.mappedNPC = (Dictionary<fogs.proto.msg.PositionType,uint>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<fogs.proto.msg.PositionType,uint>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_unmappedNPC(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name unmappedNPC");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index unmappedNPC on a nil value");
			}
		}

		obj.unmappedNPC = LuaScriptMgr.GetArrayObject<List<uint>>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_additionalInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additionalInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additionalInfo on a nil value");
			}
		}

		obj.additionalInfo = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AIDelay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name AIDelay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index AIDelay on a nil value");
			}
		}

		obj.AIDelay = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rushStamina(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rushStamina");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rushStamina on a nil value");
			}
		}

		obj.rushStamina = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_repositionDist(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name repositionDist");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index repositionDist on a nil value");
			}
		}

		obj.repositionDist = (IM.Number)LuaScriptMgr.GetNetObject(L, 3, typeof(IM.Number));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_skillProbs(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skillProbs");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skillProbs on a nil value");
			}
		}

		obj.skillProbs = (Dictionary<uint,float>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,float>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_extraLevelInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameMode obj = (GameMode)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name extraLevelInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index extraLevelInfo on a nil value");
			}
		}

		obj.extraLevelInfo = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMappedNPC(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameMode obj = (GameMode)LuaScriptMgr.GetNetObjectSelf(L, 1, "GameMode");
		fogs.proto.msg.PositionType arg0 = (fogs.proto.msg.PositionType)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.PositionType));
		uint o = obj.GetMappedNPC(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

