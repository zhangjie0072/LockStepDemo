using System;
using UnityEngine;
using LuaInterface;

public class PlayerInfoVisualizerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetActive", SetActive),
			new LuaMethod("Update", Update),
			new LuaMethod("ShowStaminaBar", ShowStaminaBar),
			new LuaMethod("New", _CreatePlayerInfoVisualizer),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_uiName", get_m_uiName, set_m_uiName),
			new LuaField("m_goPlayerInfo", get_m_goPlayerInfo, set_m_goPlayerInfo),
			new LuaField("m_goState", get_m_goState, set_m_goState),
		};

		LuaScriptMgr.RegisterLib(L, "PlayerInfoVisualizer", typeof(PlayerInfoVisualizer), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerInfoVisualizer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Player arg0 = (Player)LuaScriptMgr.GetNetObject(L, 1, typeof(Player));
			PlayerInfoVisualizer obj = new PlayerInfoVisualizer(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerInfoVisualizer.New");
		}

		return 0;
	}

	static Type classType = typeof(PlayerInfoVisualizer);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_uiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_uiName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_goPlayerInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_goPlayerInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_goPlayerInfo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_goPlayerInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_goState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_goState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_goState on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_goState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_uiName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_uiName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_uiName on a nil value");
			}
		}

		obj.m_uiName = (UILabel)LuaScriptMgr.GetUnityObject(L, 3, typeof(UILabel));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_goPlayerInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_goPlayerInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_goPlayerInfo on a nil value");
			}
		}

		obj.m_goPlayerInfo = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_goState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_goState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_goState on a nil value");
			}
		}

		obj.m_goState = (GameObject)LuaScriptMgr.GetUnityObject(L, 3, typeof(GameObject));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetActive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerInfoVisualizer");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.SetActive(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerInfoVisualizer");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowStaminaBar(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlayerInfoVisualizer obj = (PlayerInfoVisualizer)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlayerInfoVisualizer");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.ShowStaminaBar(arg0);
		return 0;
	}
}

