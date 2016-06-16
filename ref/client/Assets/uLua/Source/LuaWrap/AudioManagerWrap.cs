using System;
using UnityEngine;
using LuaInterface;

public class AudioManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetSource", GetSource),
			new LuaMethod("GetClip", GetClip),
			new LuaMethod("PlaySound", PlaySound),
			new LuaMethod("OnLevelWasLoaded", OnLevelWasLoaded),
			new LuaMethod("New", _CreateAudioManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_fVolume", get_m_fVolume, set_m_fVolume),
			new LuaField("mute", get_mute, set_mute),
		};

		LuaScriptMgr.RegisterLib(L, "AudioManager", typeof(AudioManager), regs, fields, typeof(Singleton<AudioManager>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAudioManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AudioManager obj = new AudioManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AudioManager.New");
		}

		return 0;
	}

	static Type classType = typeof(AudioManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_fVolume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fVolume on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_fVolume);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mute(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.mute);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_fVolume(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_fVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_fVolume on a nil value");
			}
		}

		obj.m_fVolume = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_mute(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}

		obj.mute = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSource(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioManager obj = (AudioManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "AudioManager");
		AudioSource o = obj.GetSource();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioManager obj = (AudioManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "AudioManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		AudioClip o = obj.GetClip(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 6);
		AudioManager obj = (AudioManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "AudioManager");
		AudioClip arg0 = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 4);
		float arg3 = (float)LuaScriptMgr.GetNumber(L, 5);
		bool arg4 = LuaScriptMgr.GetBoolean(L, 6);
		obj.PlaySound(arg0,arg1,arg2,arg3,arg4);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLevelWasLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioManager obj = (AudioManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "AudioManager");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLevelWasLoaded(arg0);
		return 0;
	}
}

