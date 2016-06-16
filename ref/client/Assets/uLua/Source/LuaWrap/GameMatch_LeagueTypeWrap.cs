using System;
using LuaInterface;

public class GameMatch_LeagueTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("eNone", GeteNone),
		new LuaMethod("ePractise", GetePractise),
		new LuaMethod("eReady4AsynPVP", GeteReady4AsynPVP),
		new LuaMethod("eReady4Tour", GeteReady4Tour),
		new LuaMethod("eCareer", GeteCareer),
		new LuaMethod("eTour", GeteTour),
		new LuaMethod("eAsynPVP", GeteAsynPVP),
		new LuaMethod("eSkillGuide", GeteSkillGuide),
		new LuaMethod("ePVP", GetePVP),
		new LuaMethod("eQualifying", GeteQualifying),
		new LuaMethod("eBullFight", GeteBullFight),
		new LuaMethod("eShoot", GeteShoot),
		new LuaMethod("ePracticeLocal", GetePracticeLocal),
		new LuaMethod("eRegular1V1", GeteRegular1V1),
		new LuaMethod("eQualifyingNew", GeteQualifyingNew),
		new LuaMethod("eQualifyingNewer", GeteQualifyingNewer),
		new LuaMethod("eQualifyingNewerAI", GeteQualifyingNewerAI),
		new LuaMethod("eLadderAI", GeteLadderAI),
		new LuaMethod("eMax", GeteMax),
		new LuaMethod("ePractise1vs1", GetePractise1vs1),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "GameMatch.LeagueType", typeof(GameMatch.LeagueType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eNone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePractise(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.ePractise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteReady4AsynPVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eReady4AsynPVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteReady4Tour(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eReady4Tour);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCareer(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eCareer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteTour(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eTour);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteAsynPVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eAsynPVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteSkillGuide(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eSkillGuide);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePVP(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.ePVP);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteQualifying(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eQualifying);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBullFight(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eBullFight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteShoot(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePracticeLocal(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.ePracticeLocal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRegular1V1(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eRegular1V1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteQualifyingNew(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eQualifyingNew);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteQualifyingNewer(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eQualifyingNewer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteQualifyingNewerAI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eQualifyingNewerAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteLadderAI(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eLadderAI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteMax(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.eMax);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePractise1vs1(IntPtr L)
	{
		LuaScriptMgr.Push(L, GameMatch.LeagueType.ePractise1vs1);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		GameMatch.LeagueType o = (GameMatch.LeagueType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

