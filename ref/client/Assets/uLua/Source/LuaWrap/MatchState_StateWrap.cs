using System;
using LuaInterface;

public class MatchState_StateWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("eNone", GeteNone),
		new LuaMethod("eOpening", GeteOpening),
		new LuaMethod("eBegin", GeteBegin),
		new LuaMethod("ePlaying", GetePlaying),
		new LuaMethod("eGoal", GeteGoal),
		new LuaMethod("eFoul", GeteFoul),
		new LuaMethod("eOver", GeteOver),
		new LuaMethod("eTipOff", GeteTipOff),
		new LuaMethod("ePlotBegin", GetePlotBegin),
		new LuaMethod("ePlotEnd", GetePlotEnd),
		new LuaMethod("eShowRule", GeteShowRule),
		new LuaMethod("eSlotMachineUltimate21", GeteSlotMachineUltimate21),
		new LuaMethod("eSlotMachineBullFight", GeteSlotMachineBullFight),
		new LuaMethod("eShowSkillGuide", GeteShowSkillGuide),
		new LuaMethod("eFreeThrowStart", GeteFreeThrowStart),
		new LuaMethod("ePlayerCloseUp", GetePlayerCloseUp),
		new LuaMethod("eOverTime", GeteOverTime),
		new LuaMethod("eMax", GeteMax),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "MatchState.State", typeof(MatchState.State), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eNone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteOpening(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eOpening);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBegin(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eBegin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePlaying(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.ePlaying);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteGoal(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eGoal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteFoul(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eFoul);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteOver(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eOver);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteTipOff(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eTipOff);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePlotBegin(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.ePlotBegin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePlotEnd(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.ePlotEnd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteShowRule(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eShowRule);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteSlotMachineUltimate21(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eSlotMachineUltimate21);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteSlotMachineBullFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eSlotMachineBullFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteShowSkillGuide(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eShowSkillGuide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteFreeThrowStart(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eFreeThrowStart);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePlayerCloseUp(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.ePlayerCloseUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteOverTime(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eOverTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteMax(IntPtr L)
	{
		LuaScriptMgr.Push(L, MatchState.State.eMax);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		MatchState.State o = (MatchState.State)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

