using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UnityEngine_Experimental_Director_DirectorPlayerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Play", Play),
			new LuaMethod("Stop", Stop),
			new LuaMethod("SetTime", SetTime),
			new LuaMethod("GetTime", GetTime),
			new LuaMethod("SetTimeUpdateMode", SetTimeUpdateMode),
			new LuaMethod("GetTimeUpdateMode", GetTimeUpdateMode),
			new LuaMethod("New", _CreateUnityEngine_Experimental_Director_DirectorPlayer),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.Experimental.Director.DirectorPlayer", typeof(UnityEngine.Experimental.Director.DirectorPlayer), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_Experimental_Director_DirectorPlayer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			UnityEngine.Experimental.Director.DirectorPlayer obj = new UnityEngine.Experimental.Director.DirectorPlayer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.Experimental.Director.DirectorPlayer.New");
		}

		return 0;
	}

	static Type classType = typeof(UnityEngine.Experimental.Director.DirectorPlayer);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
			UnityEngine.Experimental.Director.Playable arg0 = (UnityEngine.Experimental.Director.Playable)LuaScriptMgr.GetNetObject(L, 2, typeof(UnityEngine.Experimental.Director.Playable));
			obj.Play(arg0);
			return 0;
		}
		else if (count == 3)
		{
			UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
			UnityEngine.Experimental.Director.Playable arg0 = (UnityEngine.Experimental.Director.Playable)LuaScriptMgr.GetNetObject(L, 2, typeof(UnityEngine.Experimental.Director.Playable));
			object arg1 = LuaScriptMgr.GetVarObject(L, 3);
			obj.Play(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.Experimental.Director.DirectorPlayer.Play");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
		obj.Stop();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
		double arg0 = (double)LuaScriptMgr.GetNumber(L, 2);
		obj.SetTime(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
		double o = obj.GetTime();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTimeUpdateMode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
		UnityEngine.Experimental.Director.DirectorUpdateMode arg0 = (UnityEngine.Experimental.Director.DirectorUpdateMode)LuaScriptMgr.GetNetObject(L, 2, typeof(UnityEngine.Experimental.Director.DirectorUpdateMode));
		obj.SetTimeUpdateMode(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTimeUpdateMode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Experimental.Director.DirectorPlayer obj = (UnityEngine.Experimental.Director.DirectorPlayer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "UnityEngine.Experimental.Director.DirectorPlayer");
		UnityEngine.Experimental.Director.DirectorUpdateMode o = obj.GetTimeUpdateMode();
		LuaScriptMgr.Push(L, o);
		return 1;
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

