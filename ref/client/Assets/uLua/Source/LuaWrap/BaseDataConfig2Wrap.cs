using System;
using System.Collections.Generic;
using LuaInterface;

public class BaseDataConfig2Wrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Initialize", Initialize),
			new LuaMethod("GetAttrValue", GetAttrValue),
			new LuaMethod("ReadConfig", ReadConfig),
			new LuaMethod("GetConfigData", GetConfigData),
			new LuaMethod("GetTalent", GetTalent),
			new LuaMethod("GetIntTalent", GetIntTalent),
			new LuaMethod("GetConfig", GetConfig),
			new LuaMethod("GetIconList", GetIconList),
			new LuaMethod("GetIcon", GetIcon),
			new LuaMethod("GetIDByIcon", GetIDByIcon),
			new LuaMethod("GetPosition", GetPosition),
			new LuaMethod("GetIntro", GetIntro),
			new LuaMethod("GetInitAnimationID", GetInitAnimationID),
			new LuaMethod("GetOtherAnimationsID", GetOtherAnimationsID),
			new LuaMethod("GetRandomAnimationID", GetRandomAnimationID),
			new LuaMethod("GetPlayerSoundByID", GetPlayerSoundByID),
			new LuaMethod("GetPlayerEffectByID", GetPlayerEffectByID),
			new LuaMethod("New", _CreateBaseDataConfig2),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("roleBaseDatas", get_roleBaseDatas, set_roleBaseDatas),
		};

		LuaScriptMgr.RegisterLib(L, "BaseDataConfig2", typeof(BaseDataConfig2), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBaseDataConfig2(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			BaseDataConfig2 obj = new BaseDataConfig2();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: BaseDataConfig2.New");
		}

		return 0;
	}

	static Type classType = typeof(BaseDataConfig2);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_roleBaseDatas(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, BaseDataConfig2.roleBaseDatas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_roleBaseDatas(IntPtr L)
	{
		BaseDataConfig2.roleBaseDatas = (Dictionary<uint,RoleBaseData2>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<uint,RoleBaseData2>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		obj.Initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAttrValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint arg1 = (uint)LuaScriptMgr.GetNumber(L, 3);
		uint o = obj.GetAttrValue(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		obj.ReadConfig();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfigData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		RoleBaseData2 o = obj.GetConfigData(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTalent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		IM.Number o = obj.GetTalent(arg0);
		LuaScriptMgr.PushValue(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIntTalent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		int o = obj.GetIntTalent(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetConfig(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		List<RoleBaseData2> o = obj.GetConfig();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIconList(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		List<string> o = obj.GetIconList();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIcon(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetIcon(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIDByIcon(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		uint o = obj.GetIDByIcon(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPosition(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		fogs.proto.msg.PositionType o = obj.GetPosition(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIntro(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetIntro(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInitAnimationID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetInitAnimationID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOtherAnimationsID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<uint> o = obj.GetOtherAnimationsID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRandomAnimationID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		uint o = obj.GetRandomAnimationID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlayerSoundByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		List<string> o = obj.GetPlayerSoundByID(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPlayerEffectByID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseDataConfig2 obj = (BaseDataConfig2)LuaScriptMgr.GetNetObjectSelf(L, 1, "BaseDataConfig2");
		uint arg0 = (uint)LuaScriptMgr.GetNumber(L, 2);
		string o = obj.GetPlayerEffectByID(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

