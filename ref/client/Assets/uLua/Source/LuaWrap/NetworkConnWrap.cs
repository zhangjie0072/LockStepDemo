using System;
using LuaInterface;

public class NetworkConnWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("EnableTimeout", EnableTimeout),
			new LuaMethod("Connect", Connect),
			new LuaMethod("IsConnected", IsConnected),
			new LuaMethod("IsSocketConnected", IsSocketConnected),
			new LuaMethod("Close", Close),
			new LuaMethod("Update", Update),
			new LuaMethod("AddEventListener", AddEventListener),
			new LuaMethod("RemoveEventListener", RemoveEventListener),
			new LuaMethod("NotifyAllListener", NotifyAllListener),
			new LuaMethod("SendMsgFromLua", SendMsgFromLua),
			new LuaMethod("encrypt", encrypt),
			new LuaMethod("decrypt", decrypt),
			new LuaMethod("enDecrypt", enDecrypt),
			new LuaMethod("New", _CreateNetworkConn),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_ip", get_m_ip, set_m_ip),
			new LuaField("m_port", get_m_port, set_m_port),
			new LuaField("m_profiler", get_m_profiler, set_m_profiler),
			new LuaField("m_type", get_m_type, null),
		};

		LuaScriptMgr.RegisterLib(L, "NetworkConn", typeof(NetworkConn), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNetworkConn(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			MsgHandler arg0 = (MsgHandler)LuaScriptMgr.GetNetObject(L, 1, typeof(MsgHandler));
			string arg1 = LuaScriptMgr.GetLuaString(L, 2);
			NetworkConn.Type arg2 = (NetworkConn.Type)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkConn.Type));
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			NetworkConn obj = new NetworkConn(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NetworkConn.New");
		}

		return 0;
	}

	static Type classType = typeof(NetworkConn);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_ip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_ip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_port");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_port on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_port);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_profiler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_profiler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_profiler on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.m_profiler);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_type(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_type on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_ip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_ip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_ip on a nil value");
			}
		}

		obj.m_ip = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_port(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_port");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_port on a nil value");
			}
		}

		obj.m_port = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_profiler(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkConn obj = (NetworkConn)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_profiler");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_profiler on a nil value");
			}
		}

		obj.m_profiler = (NetworkProfiler)LuaScriptMgr.GetNetObject(L, 3, typeof(NetworkProfiler));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EnableTimeout(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
		obj.EnableTimeout(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Connect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.Connect(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsConnected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		bool o = obj.IsConnected();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsSocketConnected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		System.Net.Sockets.Socket arg0 = (System.Net.Sockets.Socket)LuaScriptMgr.GetNetObject(L, 2, typeof(System.Net.Sockets.Socket));
		bool o = obj.IsSocketConnected(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		obj.Close();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddEventListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		NetworkConn.Listener arg0 = (NetworkConn.Listener)LuaScriptMgr.GetNetObject(L, 2, typeof(NetworkConn.Listener));
		obj.AddEventListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveEventListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		NetworkConn.Listener arg0 = (NetworkConn.Listener)LuaScriptMgr.GetNetObject(L, 2, typeof(NetworkConn.Listener));
		obj.RemoveEventListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int NotifyAllListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		NetworkConn.NetworkEvent arg0 = (NetworkConn.NetworkEvent)LuaScriptMgr.GetNetObject(L, 2, typeof(NetworkConn.NetworkEvent));
		obj.NotifyAllListener(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMsgFromLua(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		LuaStringBuffer arg1 = LuaScriptMgr.GetStringBuffer(L, 3);
		obj.SendMsgFromLua(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int encrypt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.encrypt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int decrypt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		Pack arg0 = (Pack)LuaScriptMgr.GetNetObject(L, 2, typeof(Pack));
		obj.decrypt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int enDecrypt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		NetworkConn obj = (NetworkConn)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkConn");
		byte[] objs0 = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		bool o = obj.enDecrypt(objs0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

