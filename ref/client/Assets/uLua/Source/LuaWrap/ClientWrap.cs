using System;
using System.Collections.Generic;
using LuaInterface;

public class ClientWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Reset", Reset),
			new LuaMethod("Update", Update),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("LateUpdate", LateUpdate),
			new LuaMethod("CreateNewMatch", CreateNewMatch),
			new LuaMethod("CreateMatch", CreateMatch),
			new LuaMethod("CreateBullFightMatch", CreateBullFightMatch),
			new LuaMethod("CreateShootMatch", CreateShootMatch),
			new LuaMethod("CreateNewQualifyingMatch", CreateNewQualifyingMatch),
			new LuaMethod("CreateFreePracticeMatch", CreateFreePracticeMatch),
			new LuaMethod("CreatePracticeVsMatch", CreatePracticeVsMatch),
			new LuaMethod("UpdateAssets", UpdateAssets),
			new LuaMethod("Exit", Exit),
			new LuaMethod("OnLevelWasLoaded", OnLevelWasLoaded),
			new LuaMethod("IsConnected", IsConnected),
			new LuaMethod("New", _CreateClient),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("pause", get_pause, set_pause),
			new LuaField("timeScale", get_timeScale, set_timeScale),
			new LuaField("bStartGuide", get_bStartGuide, set_bStartGuide),
			new LuaField("curClientState", get_curClientState, set_curClientState),
			new LuaField("mCurMatch", get_mCurMatch, null),
			new LuaField("mInputManager", get_mInputManager, null),
			new LuaField("mUIManager", get_mUIManager, null),
			new LuaField("mPlayerManager", get_mPlayerManager, null),
		};

		LuaScriptMgr.RegisterLib(L, "Client", typeof(Client), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateClient(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Client obj = new Client();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Client.New");
		}

		return 0;
	}

	static Type classType = typeof(Client);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pause(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pause on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pause);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.timeScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bStartGuide(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bStartGuide");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bStartGuide on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bStartGuide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_curClientState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curClientState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curClientState on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.curClientState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mCurMatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mCurMatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mCurMatch on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mCurMatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mInputManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mInputManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mInputManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mInputManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mUIManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mUIManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mUIManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mUIManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mPlayerManager(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mPlayerManager");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mPlayerManager on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.mPlayerManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pause(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pause on a nil value");
			}
		}

		obj.pause = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_timeScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeScale on a nil value");
			}
		}

		obj.timeScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bStartGuide(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bStartGuide");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bStartGuide on a nil value");
			}
		}

		obj.bStartGuide = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_curClientState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Client obj = (Client)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name curClientState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index curClientState on a nil value");
			}
		}

		obj.curClientState = (Client.State)LuaScriptMgr.GetNetObject(L, 3, typeof(Client.State));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.Reset();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.Update();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.FixedUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LateUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.LateUpdate();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateNewMatch(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Client), typeof(GameMatch.Config)))
		{
			Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
			GameMatch.Config arg0 = (GameMatch.Config)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.CreateNewMatch(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Client), typeof(GameMatch.Type)))
		{
			Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
			GameMatch.Type arg0 = (GameMatch.Type)LuaScriptMgr.GetLuaObject(L, 2);
			bool o = obj.CreateNewMatch(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
			GameMatch.Type arg2 = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 4, typeof(GameMatch.Type));
			bool o = obj.CreateNewMatch(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
			uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
			ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
			bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
			GameMatch.LeagueType arg3 = (GameMatch.LeagueType)LuaScriptMgr.GetNetObject(L, 5, typeof(GameMatch.LeagueType));
			List<uint> arg4 = (List<uint>)LuaScriptMgr.GetNetObject(L, 6, typeof(List<uint>));
			List<uint> arg5 = (List<uint>)LuaScriptMgr.GetNetObject(L, 7, typeof(List<uint>));
			obj.CreateNewMatch(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Client.CreateNewMatch");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		PractiseData arg0 = (PractiseData)LuaScriptMgr.GetNetObject(L, 2, typeof(PractiseData));
		ulong arg1 = (ulong)LuaScriptMgr.GetNumber(L, 3);
		obj.CreateMatch(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateBullFightMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint arg2 = (uint)LuaScriptMgr.GetNumber(L, 4);
		obj.CreateBullFightMatch(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateShootMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.GameMode arg1 = (fogs.proto.msg.GameMode)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.GameMode));
		uint arg2 = (uint)LuaScriptMgr.GetNumber(L, 4);
		uint arg3 = (uint)LuaScriptMgr.GetNumber(L, 5);
		uint arg4 = (uint)LuaScriptMgr.GetNumber(L, 6);
		uint arg5 = (uint)LuaScriptMgr.GetNumber(L, 7);
		obj.CreateShootMatch(arg0,arg1,arg2,arg3,arg4,arg5);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateNewQualifyingMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		ulong arg0 = (ulong)LuaScriptMgr.GetNumber(L, 2);
		List<fogs.proto.msg.FightRole> arg1 = (List<fogs.proto.msg.FightRole>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<fogs.proto.msg.FightRole>));
		fogs.proto.msg.RivalInfo arg2 = (fogs.proto.msg.RivalInfo)LuaScriptMgr.GetNetObject(L, 4, typeof(fogs.proto.msg.RivalInfo));
		obj.CreateNewQualifyingMatch(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreateFreePracticeMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		obj.CreateFreePracticeMatch(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePracticeVsMatch(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		List<uint> arg0 = (List<uint>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<uint>));
		obj.CreatePracticeVsMatch(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateAssets(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.UpdateAssets();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Exit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		obj.Exit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLevelWasLoaded(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLevelWasLoaded(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsConnected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Client obj = (Client)LuaScriptMgr.GetNetObjectSelf(L, 1, "Client");
		bool o = obj.IsConnected();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

