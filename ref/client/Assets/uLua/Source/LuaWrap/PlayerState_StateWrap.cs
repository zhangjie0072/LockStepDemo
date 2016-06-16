using System;
using LuaInterface;

public class PlayerState_StateWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("eNone", GeteNone),
		new LuaMethod("eStand", GeteStand),
		new LuaMethod("eHold", GeteHold),
		new LuaMethod("eRun", GeteRun),
		new LuaMethod("eCrossOver", GeteCrossOver),
		new LuaMethod("eSwitchBall", GeteSwitchBall),
		new LuaMethod("eRush", GeteRush),
		new LuaMethod("eRushTurning", GeteRushTurning),
		new LuaMethod("eKnocked", GeteKnocked),
		new LuaMethod("eFallLostBall", GeteFallLostBall),
		new LuaMethod("eFallGround", GeteFallGround),
		new LuaMethod("ePickup", GetePickup),
		new LuaMethod("eCatch", GeteCatch),
		new LuaMethod("eIdlePose", GeteIdlePose),
		new LuaMethod("eStolen", GeteStolen),
		new LuaMethod("eCrossed", GeteCrossed),
		new LuaMethod("eBackToStand", GeteBackToStand),
		new LuaMethod("eBackTurnRun", GeteBackTurnRun),
		new LuaMethod("eBackBlock", GeteBackBlock),
		new LuaMethod("eDefenseCross", GeteDefenseCross),
		new LuaMethod("eInterception", GeteInterception),
		new LuaMethod("eDisturb", GeteDisturb),
		new LuaMethod("eLayupFailed", GeteLayupFailed),
		new LuaMethod("eBackCompete", GeteBackCompete),
		new LuaMethod("eInput", GeteInput),
		new LuaMethod("eRequireBall", GeteRequireBall),
		new LuaMethod("eRebound", GeteRebound),
		new LuaMethod("ePass", GetePass),
		new LuaMethod("eLayup", GeteLayup),
		new LuaMethod("ePrepareToShoot", GetePrepareToShoot),
		new LuaMethod("eShoot", GeteShoot),
		new LuaMethod("eDunk", GeteDunk),
		new LuaMethod("eGoalPose", GeteGoalPose),
		new LuaMethod("eDefense", GeteDefense),
		new LuaMethod("eSteal", GeteSteal),
		new LuaMethod("eBlock", GeteBlock),
		new LuaMethod("eMoveStep", GeteMoveStep),
		new LuaMethod("eResultPose", GeteResultPose),
		new LuaMethod("ePickAndRoll", GetePickAndRoll),
		new LuaMethod("eBePickAndRoll", GeteBePickAndRoll),
		new LuaMethod("eBodyThrowCatch", GeteBodyThrowCatch),
		new LuaMethod("eCutIn", GeteCutIn),
		new LuaMethod("eBackToBack", GeteBackToBack),
		new LuaMethod("eBackToBackForward", GeteBackToBackForward),
		new LuaMethod("eMax", GeteMax),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "PlayerState.State", typeof(PlayerState.State), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eNone);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteStand(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eStand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteHold(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eHold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRun(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eRun);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCrossOver(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eCrossOver);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteSwitchBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eSwitchBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRush(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eRush);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRushTurning(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eRushTurning);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteKnocked(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eKnocked);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteFallLostBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eFallLostBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteFallGround(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eFallGround);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePickup(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.ePickup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCatch(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eCatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteIdlePose(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eIdlePose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteStolen(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eStolen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCrossed(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eCrossed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackToStand(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackToStand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackTurnRun(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackTurnRun);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackBlock(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackBlock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteDefenseCross(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eDefenseCross);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteInterception(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eInterception);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteDisturb(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eDisturb);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteLayupFailed(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eLayupFailed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackCompete(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackCompete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteInput(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eInput);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRequireBall(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eRequireBall);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteRebound(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eRebound);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePass(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.ePass);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteLayup(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eLayup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePrepareToShoot(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.ePrepareToShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteShoot(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eShoot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteDunk(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eDunk);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteGoalPose(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eGoalPose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteDefense(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eDefense);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteSteal(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eSteal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBlock(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBlock);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteMoveStep(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eMoveStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteResultPose(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eResultPose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetePickAndRoll(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.ePickAndRoll);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBePickAndRoll(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBePickAndRoll);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBodyThrowCatch(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBodyThrowCatch);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteCutIn(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eCutIn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackToBack(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackToBack);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteBackToBackForward(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eBackToBackForward);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GeteMax(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayerState.State.eMax);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		PlayerState.State o = (PlayerState.State)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

