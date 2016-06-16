using System;
using System.Collections.Generic;
using LuaInterface;

public class VipPrivilegeConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetVipData", GetVipData),
			new LuaMethod("GetVipRestTimes", GetVipRestTimes),
			new LuaMethod("GetVipCareerResetTimes", GetVipCareerResetTimes),
			new LuaMethod("GetVipState", GetVipState),
			new LuaMethod("GetVipLevel", GetVipLevel),
			new LuaMethod("GetBuyhp_times", GetBuyhp_times),
			new LuaMethod("GetBuygold_times", GetBuygold_times),
			new LuaMethod("GetAppendSignTimes", GetAppendSignTimes),
			new LuaMethod("GetBullFightBuyTimes", GetBullFightBuyTimes),
			new LuaMethod("GetShootGameBuyTimes", GetShootGameBuyTimes),
			new LuaMethod("GetQualifyingBuyTimes", GetQualifyingBuyTimes),
			new LuaMethod("GetCareerBuyTimes", GetCareerBuyTimes),
			new LuaMethod("New", _CreateVipPrivilegeConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Vipdatas", get_Vipdatas, set_Vipdatas),
			new LuaField("vipStates", get_vipStates, set_vipStates),
			new LuaField("recharges", get_recharges, set_recharges),
			new LuaField("maxVip", get_maxVip, set_maxVip),
		};

		LuaScriptMgr.RegisterLib(L, "VipPrivilegeConfig", typeof(VipPrivilegeConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateVipPrivilegeConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			VipPrivilegeConfig obj = new VipPrivilegeConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: VipPrivilegeConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(VipPrivilegeConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Vipdatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Vipdatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Vipdatas on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Vipdatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vipStates(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vipStates");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vipStates on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.vipStates);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recharges(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recharges");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recharges on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.recharges);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxVip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxVip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxVip on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maxVip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Vipdatas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Vipdatas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Vipdatas on a nil value");
			}
		}

		obj.Vipdatas = (Dictionary<uint,VipData>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,VipData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vipStates(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vipStates");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vipStates on a nil value");
			}
		}

		obj.vipStates = (Dictionary<uint,VipState>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,VipState>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recharges(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recharges");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recharges on a nil value");
			}
		}

		obj.recharges = (Dictionary<uint,Recharge>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,Recharge>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxVip(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxVip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxVip on a nil value");
			}
		}

		obj.maxVip = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVipData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		VipData o = obj.GetVipData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVipRestTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetVipRestTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVipCareerResetTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetVipCareerResetTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVipState(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		VipState o = obj.GetVipState(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVipLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetVipLevel(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuyhp_times(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetBuyhp_times(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBuygold_times(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetBuygold_times(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAppendSignTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetAppendSignTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBullFightBuyTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetBullFightBuyTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShootGameBuyTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetShootGameBuyTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualifyingBuyTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetQualifyingBuyTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCareerBuyTimes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		VipPrivilegeConfig obj = (VipPrivilegeConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "VipPrivilegeConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetCareerBuyTimes(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

