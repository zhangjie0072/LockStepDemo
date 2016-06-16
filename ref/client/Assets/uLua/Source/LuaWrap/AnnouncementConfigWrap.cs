using System;
using LuaInterface;

public class AnnouncementConfigWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetOpenItem", GetOpenItem),
			new LuaMethod("New", _CreateAnnouncementConfig),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "AnnouncementConfig", typeof(AnnouncementConfig), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAnnouncementConfig(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AnnouncementConfig obj = new AnnouncementConfig();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AnnouncementConfig.New");
		}

		return 0;
	}

	static Type classType = typeof(AnnouncementConfig);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AnnouncementConfig obj = (AnnouncementConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AnnouncementConfig");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOpenItem(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AnnouncementConfig obj = (AnnouncementConfig)LuaScriptMgr.GetNetObjectSelf(L, 1, "AnnouncementConfig");
		AnnouncementItem o = obj.GetOpenItem();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}
}

