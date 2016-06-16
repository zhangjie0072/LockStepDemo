using System;
using UnityEngine;
using LuaInterface;

public class NetworkManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Getloginconn", Getloginconn),
			new LuaMethod("ConnectToLS", ConnectToLS),
			new LuaMethod("ConnectToPS", ConnectToPS),
			new LuaMethod("ConnectToGS", ConnectToGS),
			new LuaMethod("CloseLoginConn", CloseLoginConn),
			new LuaMethod("ClosePlatConn", ClosePlatConn),
			new LuaMethod("CloseGameServerConn", CloseGameServerConn),
			new LuaMethod("Exit", Exit),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("Update", Update),
			new LuaMethod("OnEvent", OnEvent),
			new LuaMethod("CanAutoReconn", CanAutoReconn),
			new LuaMethod("InNormalState", InNormalState),
			new LuaMethod("StopAutoReconn", StopAutoReconn),
			new LuaMethod("ReconnectPrompt", ReconnectPrompt),
			new LuaMethod("DisconnectPrompt", DisconnectPrompt),
			new LuaMethod("Reconnect", Reconnect),
			new LuaMethod("ReturnToLogin", ReturnToLogin),
			new LuaMethod("ForceToLogin", ForceToLogin),
			new LuaMethod("OnApplicationPause", OnApplicationPause),
			new LuaMethod("New", _CreateNetworkManager),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_loginConn", get_m_loginConn, set_m_loginConn),
			new LuaField("m_gameConn", get_m_gameConn, set_m_gameConn),
			new LuaField("m_platConn", get_m_platConn, set_m_platConn),
			new LuaField("m_loginMsgHandler", get_m_loginMsgHandler, set_m_loginMsgHandler),
			new LuaField("m_platMsgHandler", get_m_platMsgHandler, set_m_platMsgHandler),
			new LuaField("m_gameMsgHandler", get_m_gameMsgHandler, set_m_gameMsgHandler),
			new LuaField("onServerConnected", get_onServerConnected, set_onServerConnected),
			new LuaField("m_dServerTime", get_m_dServerTime, set_m_dServerTime),
			new LuaField("m_dLocalTime", get_m_dLocalTime, set_m_dLocalTime),
			new LuaField("connLogin", get_connLogin, set_connLogin),
			new LuaField("connPlat", get_connPlat, set_connPlat),
			new LuaField("connGS", get_connGS, set_connGS),
			new LuaField("autoReconnInMatch", get_autoReconnInMatch, set_autoReconnInMatch),
			new LuaField("isReconnecting", get_isReconnecting, set_isReconnecting),
			new LuaField("isPushedOut", get_isPushedOut, set_isPushedOut),
			new LuaField("isWaittingReLogin", get_isWaittingReLogin, set_isWaittingReLogin),
			new LuaField("isKickOut", get_isKickOut, set_isKickOut),
		};

		LuaScriptMgr.RegisterLib(L, "NetworkManager", typeof(NetworkManager), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNetworkManager(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			NetworkManager obj = new NetworkManager();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NetworkManager.New");
		}

		return 0;
	}

	static Type classType = typeof(NetworkManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_loginConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loginConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loginConn on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_loginConn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gameConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameConn on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_gameConn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_platConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_platConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_platConn on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_platConn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_loginMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loginMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loginMsgHandler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_loginMsgHandler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_platMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_platMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_platMsgHandler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_platMsgHandler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_gameMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMsgHandler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_gameMsgHandler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onServerConnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onServerConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onServerConnected on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onServerConnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dServerTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dServerTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dServerTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_dServerTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dLocalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dLocalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dLocalTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_dLocalTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_connLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connLogin on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.connLogin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_connPlat(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connPlat");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connPlat on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.connPlat);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_connGS(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connGS");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connGS on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.connGS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoReconnInMatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoReconnInMatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoReconnInMatch on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.autoReconnInMatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isReconnecting(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReconnecting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReconnecting on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isReconnecting);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPushedOut(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPushedOut");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPushedOut on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isPushedOut);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isWaittingReLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isWaittingReLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isWaittingReLogin on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isWaittingReLogin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isKickOut(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isKickOut");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isKickOut on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isKickOut);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_loginConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loginConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loginConn on a nil value");
			}
		}

		obj.m_loginConn = (NetworkConn)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkConn));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_gameConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameConn on a nil value");
			}
		}

		obj.m_gameConn = (NetworkConn)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkConn));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_platConn(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_platConn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_platConn on a nil value");
			}
		}

		obj.m_platConn = (NetworkConn)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkConn));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_loginMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_loginMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_loginMsgHandler on a nil value");
			}
		}

		obj.m_loginMsgHandler = (LoginMsgHandler)LuaScriptMgr.GetNetObject(L, 3, typeof(LoginMsgHandler));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_platMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_platMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_platMsgHandler on a nil value");
			}
		}

		obj.m_platMsgHandler = (PlatMsgHandler)LuaScriptMgr.GetNetObject(L, 3, typeof(PlatMsgHandler));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_gameMsgHandler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_gameMsgHandler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_gameMsgHandler on a nil value");
			}
		}

		obj.m_gameMsgHandler = (GameMsgHandler)LuaScriptMgr.GetNetObject(L, 3, typeof(GameMsgHandler));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onServerConnected(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onServerConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onServerConnected on a nil value");
			}
		}

		LuaTypes funcType = LuaDLL.lua_type(L, 3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onServerConnected = (NetworkManager.OnServerConnected)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkManager.OnServerConnected));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			obj.onServerConnected = (param0) =>
			{
				int top = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_dServerTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dServerTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dServerTime on a nil value");
			}
		}

		obj.m_dServerTime = (double)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_dLocalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dLocalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dLocalTime on a nil value");
			}
		}

		obj.m_dLocalTime = (double)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_connLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connLogin on a nil value");
			}
		}

		obj.connLogin = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_connPlat(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connPlat");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connPlat on a nil value");
			}
		}

		obj.connPlat = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_connGS(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name connGS");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index connGS on a nil value");
			}
		}

		obj.connGS = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoReconnInMatch(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoReconnInMatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoReconnInMatch on a nil value");
			}
		}

		obj.autoReconnInMatch = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isReconnecting(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isReconnecting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isReconnecting on a nil value");
			}
		}

		obj.isReconnecting = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isPushedOut(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPushedOut");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPushedOut on a nil value");
			}
		}

		obj.isPushedOut = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isWaittingReLogin(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isWaittingReLogin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isWaittingReLogin on a nil value");
			}
		}

		obj.isWaittingReLogin = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isKickOut(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkManager obj = (NetworkManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isKickOut");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isKickOut on a nil value");
			}
		}

		obj.isKickOut = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Getloginconn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.Getloginconn(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConnectToLS(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.ConnectToLS(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConnectToPS(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.ConnectToPS(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ConnectToGS(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		GameMatch.Type arg0 = (GameMatch.Type)LuaScriptMgr.GetNetObject(L, 2, typeof(GameMatch.Type));
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
		obj.ConnectToGS(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseLoginConn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.CloseLoginConn();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClosePlatConn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.ClosePlatConn();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseGameServerConn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.CloseGameServerConn();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Exit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.Exit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.FixedUpdate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		NetworkConn.NetworkEvent arg0 = (NetworkConn.NetworkEvent)LuaScriptMgr.GetNetObject(L, 2, typeof(NetworkConn.NetworkEvent));
		NetworkConn arg1 = (NetworkConn)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkConn));
		obj.OnEvent(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanAutoReconn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		bool o = obj.CanAutoReconn();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InNormalState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		bool o = obj.InNormalState();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAutoReconn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.StopAutoReconn();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReconnectPrompt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.ReconnectPrompt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DisconnectPrompt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.DisconnectPrompt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reconnect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.Reconnect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReturnToLogin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		GameObject arg0 = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		obj.ReturnToLogin(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceToLogin(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		obj.ForceToLogin();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnApplicationPause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkManager obj = (NetworkManager)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkManager");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.OnApplicationPause(arg0);
		return 0;
	}
}

