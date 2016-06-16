using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchReviseTip
{
	GameMatch match;
	bool revisingDefense;
	PlayerState.State lastMainRoleState;
	GuideTip guideTip;

	public MatchReviseTip(GameMatch match)
	{
		this.match = match;

		match.mCurScene.mBall.onShoot += OnShoot;

		guideTip = UIManager.Instance.CreateUI("GuideTip").AddMissingComponent<GuideTip>();
		guideTip.GetComponent<UIPanel>().depth = 1100;
		guideTip.transform.localPosition = new Vector3(-20, -262, 0);
		guideTip.firstButtonVisible = false;
		guideTip.instructorVisible = true;
		guideTip.instructor = "effects_guide";
		guideTip.instructorPos = new Vector3(-248, -11, 0);	
		guideTip.Hide();
	}

	public void Update(float deltaTime)
	{
		Player defenseTarget = match.m_mainRole.m_defenseTarget;
		if (defenseTarget != null && defenseTarget.m_bWithBall &&
			defenseTarget.m_AOD.GetStateByPos(match.m_mainRole.position) == AOD.Zone.eInvalid)
		{
			if (!revisingDefense)
			{
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseDefense"));
				revisingDefense = true;
			}
		}
		else
			revisingDefense = false;

		PlayerState curState = match.m_mainRole.m_StateMachine.m_curState;
		if (lastMainRoleState != PlayerState.State.eRebound && curState.m_eState == PlayerState.State.eRebound)
		{
			IM.Number curDist = GameUtils.HorizonalDistance(match.m_mainRole.position, match.mCurScene.mBall.position);
			if (curDist > match.m_mainRole.m_fReboundDist)
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseDistance"));
			else if (!(curState as PlayerState_Rebound).m_success)
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseRebound"));
		}
		else if (lastMainRoleState != PlayerState.State.eSteal && curState.m_eState == PlayerState.State.eSteal)
		{
			if (defenseTarget.m_AOD.GetStateByPos(match.m_mainRole.position) == AOD.Zone.eInvalid)
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseDistance"));
		}
		else if (lastMainRoleState != PlayerState.State.eBlock && curState.m_eState == PlayerState.State.eBlock)
		{
			IM.Number curDist = GameUtils.HorizonalDistance(match.m_mainRole.position, match.mCurScene.mBall.position);
			if (curDist < IM.Number.one || curDist > new IM.Number(5))
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseDistance"));
			else if (!match.m_mainRole.m_defenseTarget.m_blockable.blockable)
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseBlockTimming"));
		}
		else if (lastMainRoleState != PlayerState.State.eBodyThrowCatch && curState.m_eState == PlayerState.State.eBodyThrowCatch)
		{
			IM.Number maxDist = PlayerState_BodyThrowCatch.GetMaxDistance(match.m_mainRole);
			IM.Number curDist = GameUtils.HorizonalDistance(match.m_mainRole.position, match.mCurScene.mBall.position);
			if (curDist > maxDist)
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseDistance"));
		}

		lastMainRoleState = curState.m_eState;
	}

	void OnShoot(UBasketball ball)
	{
		if (ball.m_actor == match.m_mainRole && !ball.m_isLayup)
		{
            IM.Number distToBasket = GameUtils.HorizonalDistance(match.m_mainRole.position, match.mCurScene.mBasket.m_vShootTarget);
			if (distToBasket >new IM.Number(9,150))
				ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseShootDist"));
			//else
			{
				if (ball.m_actor.shootStrength.rate_adjustment < IM.Number.zero)
					ShowTip(CommonFunction.GetConstString("MATCH_GUIDE_ReviseShootTimming"));
			}
		}
	}

	void ShowTip(string tip)
	{
		if (NGUITools.GetActive(guideTip.gameObject))
			return;
		guideTip.Show();
		guideTip.tip = tip;
		guideTip.AutoHide(5f);
	}

	public void HideTip()
	{
		if (!NGUITools.GetActive(guideTip.gameObject))
			return;
		guideTip.Hide();
	}
}
