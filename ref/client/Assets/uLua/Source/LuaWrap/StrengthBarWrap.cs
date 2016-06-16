using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class StrengthBarWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateStrengthBar),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("player", get_player, set_player),
			new LuaField("atlas", get_atlas, set_atlas),
		};

		LuaScriptMgr.RegisterLib(L, "StrengthBar", typeof(StrengthBar), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateStrengthBar(IntPtr L)
	{
		LuaDLL.luaL_error(L, "StrengthBar class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(StrengthBar);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_player(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StrengthBar obj = (StrengthBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.player);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_atlas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StrengthBar obj = (StrengthBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name atlas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index atlas on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.atlas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_player(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StrengthBar obj = (StrengthBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name player");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index player on a nil value");
			}
		}

		obj.player = (Player)LuaScriptMgr.GetNetObject(L, 3, typeof(Player));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_atlas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		StrengthBar obj = (StrengthBar)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name atlas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index atlas on a nil value");
			}
		}

		obj.atlas = (UIAtlas)LuaScriptMgr.GetUnityObject(L, 3, typeof(UIAtlas));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

