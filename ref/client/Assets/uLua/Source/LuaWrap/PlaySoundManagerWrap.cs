using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class PlaySoundManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("PlaySound", PlaySound),
			new LuaMethod("ClosePlaySound", ClosePlaySound),
			new LuaMethod("Update", Update),
			new LuaMethod("ClearSoundName", ClearSoundName),
			new LuaMethod("OnSoundPlay", OnSoundPlay),
			new LuaMethod("PlaySoundOneShot", PlaySoundOneShot),
			new LuaMethod("OnOneShotPlay", OnOneShotPlay),
			new LuaMethod("SetMusicVolume", SetMusicVolume),
			new LuaMethod("GetMusicIsOpen", GetMusicIsOpen),
			new LuaMethod("SetMusicIsOpen", SetMusicIsOpen),
			new LuaMethod("SetPlayerSoundVolume", SetPlayerSoundVolume),
			new LuaMethod("GetPlayerMusicVolume", GetPlayerMusicVolume),
			new LuaMethod("GetPlayerSoundVolume", GetPlayerSoundVolume),
			new LuaMethod("New", _CreatePlaySoundManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Mute", get_Mute, set_Mute),
			new LuaField("isCloseSound", get_isCloseSound, set_isCloseSound),
		};

		LuaScriptMgr.RegisterLib(L, "PlaySoundManager", typeof(PlaySoundManager), regs, fields, typeof(Singleton<PlaySoundManager>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlaySoundManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PlaySoundManager obj = new PlaySoundManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlaySoundManager.New");
		}

		return 0;
	}

	static Type classType = typeof(PlaySoundManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Mute(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlaySoundManager.Mute);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isCloseSound(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlaySoundManager.isCloseSound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Mute(IntPtr L)
	{
		PlaySoundManager.Mute = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isCloseSound(IntPtr L)
	{
		PlaySoundManager.isCloseSound = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySound(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			PlaySoundManager.PlaySound(arg0);
			return 0;
		}
		else if (count == 2)
		{
			string arg0 = LuaScriptMgr.GetLuaString(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			PlaySoundManager.PlaySound(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(PlaySoundManager), typeof(MatchSoundEvent), typeof(bool)))
		{
			PlaySoundManager obj = (PlaySoundManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlaySoundManager");
			MatchSoundEvent arg0 = (MatchSoundEvent)LuaScriptMgr.GetLuaObject(L, 2);
			bool arg1 = LuaDLL.lua_toboolean(L, 3);
			obj.PlaySound(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(AudioClip), typeof(float), typeof(float)))
		{
			AudioClip arg0 = (AudioClip)LuaScriptMgr.GetLuaObject(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			AudioSource o = PlaySoundManager.PlaySound(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlaySoundManager.PlaySound");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClosePlaySound(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		PlaySoundManager.ClosePlaySound();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlaySoundManager obj = (PlaySoundManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlaySoundManager");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearSoundName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		PlaySoundManager.ClearSoundName();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSoundPlay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Object arg0 = (Object)LuaScriptMgr.GetUnityObject(L, 1, typeof(Object));
		PlaySoundManager.OnSoundPlay(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlaySoundOneShot(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		PlaySoundManager.PlaySoundOneShot(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnOneShotPlay(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Object arg0 = (Object)LuaScriptMgr.GetUnityObject(L, 1, typeof(Object));
		PlaySoundManager.OnOneShotPlay(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetMusicVolume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 1);
		PlaySoundManager.SetMusicVolume(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMusicIsOpen(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		bool o = PlaySoundManager.GetMusicIsOpen();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetMusicIsOpen(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		bool arg0 = LuaScriptMgr.GetBoolean(L, 1);
		PlaySoundManager.SetMusicIsOpen(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPlayerSoundVolume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 1);
		PlaySoundManager.SetPlayerSoundVolume(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlayerMusicVolume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		float o = PlaySoundManager.GetPlayerMusicVolume();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlayerSoundVolume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		float o = PlaySoundManager.GetPlayerSoundVolume();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

