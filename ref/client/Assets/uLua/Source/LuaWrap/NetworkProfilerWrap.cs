using System;
using LuaInterface;

public class NetworkProfilerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("BeginRecordDataUsage", BeginRecordDataUsage),
			new LuaMethod("RecvDataCount", RecvDataCount),
			new LuaMethod("EndRecordDataUsage", EndRecordDataUsage),
			new LuaMethod("FixedUpdate", FixedUpdate),
			new LuaMethod("New", _CreateNetworkProfiler),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("m_avgLatency", get_m_avgLatency, null),
			new LuaField("m_dataUsage", get_m_dataUsage, null),
		};

		LuaScriptMgr.RegisterLib(L, "NetworkProfiler", typeof(NetworkProfiler), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateNetworkProfiler(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			NetworkConn arg0 = (NetworkConn)LuaScriptMgr.GetNetObject(L, 1, typeof(NetworkConn));
			NetworkProfiler obj = new NetworkProfiler(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: NetworkProfiler.New");
		}

		return 0;
	}

	static Type classType = typeof(NetworkProfiler);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_avgLatency(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkProfiler obj = (NetworkProfiler)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_avgLatency");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_avgLatency on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_avgLatency);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_dataUsage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		NetworkProfiler obj = (NetworkProfiler)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_dataUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_dataUsage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_dataUsage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkProfiler obj = (NetworkProfiler)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkProfiler");
		obj.Init();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginRecordDataUsage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkProfiler obj = (NetworkProfiler)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkProfiler");
		obj.BeginRecordDataUsage();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RecvDataCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkProfiler obj = (NetworkProfiler)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkProfiler");
		long arg0 = (long)LuaScriptMgr.GetNumber(L, 2);
		obj.RecvDataCount(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndRecordDataUsage(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		NetworkProfiler obj = (NetworkProfiler)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkProfiler");
		obj.EndRecordDataUsage();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FixedUpdate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		NetworkProfiler obj = (NetworkProfiler)LuaScriptMgr.GetNetObjectSelf(L, 1, "NetworkProfiler");
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.FixedUpdate(arg0);
		return 0;
	}
}

