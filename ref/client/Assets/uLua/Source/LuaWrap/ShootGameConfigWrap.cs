using System;
using System.Collections.Generic;
using LuaInterface;

public class ShootGameConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("Read", Read),
			new LuaMethod("ReadConsume", ReadConsume),
			new LuaMethod("GetShootgNPCAttrData", GetShootgNPCAttrData),
			new LuaMethod("GetShootInfo", GetShootInfo),
			new LuaMethod("GetShootGameConsume", GetShootGameConsume),
			new LuaMethod("New", _CreateShootGameConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("shootGame", get_shootGame, set_shootGame),
			new LuaField("shootGameConsumes", get_shootGameConsumes, set_shootGameConsumes),
		};

		LuaScriptMgr.RegisterLib(L, "ShootGameConfig", typeof(ShootGameConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateShootGameConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ShootGameConfig obj = new ShootGameConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ShootGameConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(ShootGameConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shootGame(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ShootGameConfig.shootGame);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shootGameConsumes(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, ShootGameConfig.shootGameConsumes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shootGame(IntPtr L)
	{
		ShootGameConfig.shootGame = (List<ShootGame>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ShootGame>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shootGameConsumes(IntPtr L)
	{
		ShootGameConfig.shootGameConsumes = (List<ShootGameConsume>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<ShootGameConsume>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Read(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		obj.Read();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		obj.ReadConsume();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShootgNPCAttrData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint arg2 = (uint)LuaScriptMgr.GetNumber(L, 4);
		AttrData o = obj.GetShootgNPCAttrData(arg0,arg1,arg2);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShootInfo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		ShootGame o = obj.GetShootInfo(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetShootGameConsume(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ShootGameConfig obj = (ShootGameConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "ShootGameConfig");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		ShootGameConsume o = obj.GetShootGameConsume(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

