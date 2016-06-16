using System;
using LuaInterface;

public class GameMatch_TypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("eNone", GeteNone),
		new LuaMethod("ePractise", GetePractise),
		new LuaMethod("eReady", GeteReady),
		new LuaMethod("e1On1", Gete1On1),
		new LuaMethod("e3On3", Gete3On3),
		new LuaMethod("eCareer1On1", GeteCareer1On1),
		new LuaMethod("eCareer3On3", GeteCareer3On3),
		new LuaMethod("eAsynPVP3On3", GeteAsynPVP3On3),
		new LuaMethod("e3AIOn3AI", Gete3AIOn3AI),
		new LuaMethod("ePVP_1PLUS", GetePVP_1PLUS),
		new LuaMethod("ePVP_3On3", GetePVP_3On3),
		new LuaMethod("eGuide", GeteGuide),
		new LuaMethod("eFreePractice", GeteFreePractice),
		new LuaMethod("ePracticeVs", GetePracticeVs),
		new LuaMethod("eReboundStorm", GeteReboundStorm),
		new LuaMethod("eBlockStorm", GeteBlockStorm),
		new LuaMethod("eUltimate21", GeteUltimate21),
		new LuaMethod("eMassBall", GeteMassBall),
		new LuaMethod("eGrabZone", GeteGrabZone),
		new LuaMethod("eGrabPoint", GeteGrabPoint),
		new LuaMethod("eBullFight", GeteBullFight),
		new LuaMethod("ePractice1V1", GetePractice1V1),
		new LuaMethod("eQualifyingNewerAI", GeteQualifyingNewerAI),
		new LuaMethod("eLadderAI", GeteLadderAI),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "GameMatch.Type", typeof(GameMatch.Type), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eNone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePractise(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.ePractise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteReady(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eReady);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Gete1On1(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.e1On1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Gete3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.e3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCareer1On1(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eCareer1On1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCareer3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eCareer3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteAsynPVP3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eAsynPVP3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Gete3AIOn3AI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.e3AIOn3AI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePVP_1PLUS(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.ePVP_1PLUS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePVP_3On3(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.ePVP_3On3);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteGuide(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eGuide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteFreePractice(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eFreePractice);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePracticeVs(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.ePracticeVs);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteReboundStorm(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eReboundStorm);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBlockStorm(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eBlockStorm);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteUltimate21(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eUltimate21);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteMassBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eMassBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteGrabZone(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eGrabZone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteGrabPoint(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eGrabPoint);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBullFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eBullFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePractice1V1(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.ePractice1V1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteQualifyingNewerAI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eQualifyingNewerAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteLadderAI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.Type.eLadderAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		GameMatch.Type o = (GameMatch.Type)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

