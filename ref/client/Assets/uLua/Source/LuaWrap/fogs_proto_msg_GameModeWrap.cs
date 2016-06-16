using System;
using LuaInterface;

public class fogs_proto_msg_GameModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("GM_None", GetGM_None),
		new LuaMethod("GM_Practise", GetGM_Practise),
		new LuaMethod("GM_Ready", GetGM_Ready),
		new LuaMethod("GM_1On1", GetGM_1On1),
		new LuaMethod("GM_3On3", GetGM_3On3),
		new LuaMethod("GM_Career1On1", GetGM_Career1On1),
		new LuaMethod("GM_Career3On3", GetGM_Career3On3),
		new LuaMethod("GM_AsynPVP3On3", GetGM_AsynPVP3On3),
		new LuaMethod("GM_Practice1On1", GetGM_Practice1On1),
		new LuaMethod("GM_PVP", GetGM_PVP),
		new LuaMethod("GM_PVP_1V1_PLUS", GetGM_PVP_1V1_PLUS),
		new LuaMethod("GM_PVP_3V3", GetGM_PVP_3V3),
		new LuaMethod("GM_PVP_REGULAR", GetGM_PVP_REGULAR),
		new LuaMethod("GM_QUALIFYING", GetGM_QUALIFYING),
		new LuaMethod("GM_QUALIFYING_NEWER", GetGM_QUALIFYING_NEWER),
		new LuaMethod("GM_ReboundStorm", GetGM_ReboundStorm),
		new LuaMethod("GM_BlockStorm", GetGM_BlockStorm),
		new LuaMethod("GM_Ultimate21", GetGM_Ultimate21),
		new LuaMethod("GM_MassBall", GetGM_MassBall),
		new LuaMethod("GM_GrabZone", GetGM_GrabZone),
		new LuaMethod("GM_GrabPoint", GetGM_GrabPoint),
		new LuaMethod("GM_BullFight", GetGM_BullFight),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "fogs.proto.msg.GameMode", typeof(fogs.proto.msg.GameMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_None(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Practise(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Practise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Ready(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Ready);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_1On1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_1On1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Career1On1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Career1On1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Career3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Career3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_AsynPVP3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_AsynPVP3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Practice1On1(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Practice1On1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_PVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_PVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_PVP_1V1_PLUS(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_PVP_1V1_PLUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_PVP_3V3(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_PVP_3V3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_PVP_REGULAR(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_PVP_REGULAR);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_QUALIFYING(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_QUALIFYING);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_QUALIFYING_NEWER(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_QUALIFYING_NEWER);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_ReboundStorm(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_ReboundStorm);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_BlockStorm(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_BlockStorm);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_Ultimate21(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_Ultimate21);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_MassBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_MassBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_GrabZone(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_GrabZone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_GrabPoint(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_GrabPoint);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGM_BullFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, fogs.proto.msg.GameMode.GM_BullFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		fogs.proto.msg.GameMode o = (fogs.proto.msg.GameMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

