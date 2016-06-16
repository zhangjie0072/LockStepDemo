using System;
using LuaInterface;

public class PlatNetworkWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ConnectToPS", ConnectToPS),
			new LuaMethod("SendHeartbeatMsg", SendHeartbeatMsg),
			new LuaMethod("SaveCDKeyRespResult", SaveCDKeyRespResult),
			new LuaMethod("EnterPlatReq", EnterPlatReq),
			new LuaMethod("IsEnterPlatTimeout", IsEnterPlatTimeout),
			new LuaMethod("EnterPlatRespHandle", EnterPlatRespHandle),
			new LuaMethod("EnterGame", EnterGame),
			new LuaMethod("SendExitGameReq", SendExitGameReq),
			new LuaMethod("EndSectionMatchReq", EndSectionMatchReq),
			new LuaMethod("GMCommondExcuReq", GMCommondExcuReq),
			new LuaMethod("New", _CreatePlatNetwork),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("plat_entered", get_plat_entered, set_plat_entered),
			new LuaField("enter_plat_requested", get_enter_plat_requested, set_enter_plat_requested),
			new LuaField("cachedEnterGameReq", get_cachedEnterGameReq, set_cachedEnterGameReq),
			new LuaField("cachedExitGameReq", get_cachedExitGameReq, set_cachedExitGameReq),
			new LuaField("onReconnected", get_onReconnected, set_onReconnected),
			new LuaField("onDisconnected", get_onDisconnected, set_onDisconnected),
			new LuaField("respInfo", get_respInfo, set_respInfo),
		};

		LuaScriptMgr.RegisterLib(L, "PlatNetwork", typeof(PlatNetwork), regs, fields, typeof(Singleton<PlatNetwork>));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlatNetwork(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			PlatNetwork obj = new PlatNetwork();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlatNetwork.New");
		}

		return 0;
	}

	static Type classType = typeof(PlatNetwork);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_plat_entered(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plat_entered");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plat_entered on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.plat_entered);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enter_plat_requested(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enter_plat_requested");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enter_plat_requested on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.enter_plat_requested);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedEnterGameReq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedEnterGameReq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedEnterGameReq on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cachedEnterGameReq);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedExitGameReq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedExitGameReq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedExitGameReq on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.cachedExitGameReq);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onReconnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onReconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onReconnected on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onReconnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDisconnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDisconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDisconnected on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDisconnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_respInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name respInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index respInfo on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.respInfo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_plat_entered(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name plat_entered");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index plat_entered on a nil value");
			}
		}

		obj.plat_entered = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enter_plat_requested(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enter_plat_requested");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enter_plat_requested on a nil value");
			}
		}

		obj.enter_plat_requested = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cachedEnterGameReq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedEnterGameReq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedEnterGameReq on a nil value");
			}
		}

		obj.cachedEnterGameReq = LuaScriptMgr.GetStringBuffer(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cachedExitGameReq(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedExitGameReq");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedExitGameReq on a nil value");
			}
		}

		obj.cachedExitGameReq = (fogs.proto.msg.ExitGameReq)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.ExitGameReq));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onReconnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onReconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onReconnected on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onReconnected = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onReconnected = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDisconnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDisconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDisconnected on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDisconnected = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onDisconnected = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_respInfo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		PlatNetwork obj = (PlatNetwork)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name respInfo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index respInfo on a nil value");
			}
		}

		obj.respInfo = (fogs.proto.msg.EnterPlatResp)LuaScriptMgr.GetNetObject(L, 3, typeof(fogs.proto.msg.EnterPlatResp));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConnectToPS(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		obj.ConnectToPS(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendHeartbeatMsg(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		fogs.proto.msg.Heartbeat arg0 = (fogs.proto.msg.Heartbeat)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.Heartbeat));
		obj.SendHeartbeatMsg(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SaveCDKeyRespResult(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		fogs.proto.msg.VerifyCDKeyResp arg0 = (fogs.proto.msg.VerifyCDKeyResp)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.VerifyCDKeyResp));
		obj.SaveCDKeyRespResult(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnterPlatReq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		obj.EnterPlatReq();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsEnterPlatTimeout(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		bool o = obj.IsEnterPlatTimeout();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnterPlatRespHandle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.EnterPlatRespHandle(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnterGame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		obj.EnterGame();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendExitGameReq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		fogs.proto.msg.ExitGameReq arg0 = (fogs.proto.msg.ExitGameReq)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.ExitGameReq));
		obj.SendExitGameReq(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndSectionMatchReq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		fogs.proto.msg.EndSectionMatch arg0 = (fogs.proto.msg.EndSectionMatch)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.EndSectionMatch));
		obj.EndSectionMatchReq(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GMCommondExcuReq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PlatNetwork obj = (PlatNetwork)LuaScriptMgr.GetNetObjectSelf(L, 1, "PlatNetwork");
		fogs.proto.msg.GMCommondExcu arg0 = (fogs.proto.msg.GMCommondExcu)LuaScriptMgr.GetNetObject(L, 2, typeof(fogs.proto.msg.GMCommondExcu));
		obj.GMCommondExcuReq(arg0);
		return 0;
	}
}

