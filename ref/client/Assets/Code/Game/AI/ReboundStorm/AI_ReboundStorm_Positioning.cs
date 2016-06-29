using UnityEngine;
using System.Collections.Generic;

public class AI_ReboundStorm_Positioning : AIState
{
	GameMatch_ReboundStorm match;
	ReboundAttrConfig.ReboundAttr npcReboundAttr;
	ReboundAttrConfig.ReboundAttr playerReboundAttr;
	UBasketball targetBall;
	GameUtils.Timer timerRebound;

	public AI_ReboundStorm_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eReboundStorm_Positioning;
		match = m_match as GameMatch_ReboundStorm;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		npcReboundAttr = GameSystem.Instance.ReboundAttrConfigData.GetReboundAttr(m_player.m_position);
		if (npcReboundAttr == null)
		{
			Debug.LogError("Rebound height config error.");
		}
        //TODO Õë¶ÔPVPÐÞ¸Ä
		playerReboundAttr = GameSystem.Instance.ReboundAttrConfigData.GetReboundAttr(match.mainRole.m_position);
		if (playerReboundAttr == null)
		{
			Debug.LogError("Rebound height config error.");
		}
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if (match.currBall != null && match.currBall != targetBall)
		{
			targetBall = match.currBall;
			targetBall.onRebound += OnRebound;
			targetBall.onGrab += OnGrab;
		}

		if (match.currBall != null && match.currBall.m_ballState == BallState.eRebound )
		{
			//TODO: rebound skill distance involoved
			/*
			if (GameUtils.HorizonalDistance(m_player.position, match.currBall.transform.position) > m_player.m_fReboundDist)
			{
				Vector3 vMoveTarget = match.currBall.transform.position;
				vMoveTarget.y = 0.0f;
				m_moveTarget = vMoveTarget;
			}
			*/
		}
		else
		{
			IM.Vector3 vMoveTarget = match.mCurScene.mBasket.m_vShootTarget;
			vMoveTarget.y = IM.Number.zero;
			m_moveTarget = vMoveTarget;
		}

		base.Update(fDeltaTime);

		if (timerRebound != null)
			timerRebound.Update(fDeltaTime);
	}

	void OnRebound(UBasketball ball)
	{
		ShootSolution.SShootCurve curve = ball.CompleteLastCurve();
		IM.Number ballMaxHeight = curve.GetHighestPosition().y;
		//Debug.Log("Ball max height: " + ballMaxHeight);
		IM.Number npcReboundBallHeight = AIUtils.GetNPCReboundBallHeight(npcReboundAttr.maxHeight, playerReboundAttr.maxHeight, ballMaxHeight,
			match.npcHeightScale, match.playerHeightScale, match.ballHeightScale);
		//Debug.Log("NPC rebound ball height: " + npcReboundBallHeight);
        IM.Number time1, time2;
        curve.GetTimeByHeight(npcReboundBallHeight, out time1, out time2);
        IM.Number ballFlyTime = time2;
		//Debug.Log("Ball fly time: " + ballFlyTime);
		if (ballFlyTime < -new IM.Number(0,1))
			Debug.LogError("Ball fly time error.");

		SkillInstance basicRebound = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound)[0];
		string basicActionId = basicRebound.skill.actions[0].action_id;
        IM.Number frameRate = m_player.animMgr.GetFrameRate(basicActionId);
		Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = m_player.m_animAttributes.m_rebound;
		int reboundKey = rebounds[m_player.animMgr.GetOriginName(basicActionId)].GetKeyFrame("OnRebound").frame;
		IM.Number reboundActionTime = reboundKey / frameRate;

		IM.Number reboundDelayTime = ballFlyTime - reboundActionTime;
		if (reboundDelayTime < IM.Number.zero)
		{
			reboundDelayTime = IM.Number.zero;
		}

		if (timerRebound == null)
		{
			timerRebound = new GameUtils.Timer(reboundDelayTime, DoRebound);
		}
		timerRebound.SetTimer(reboundDelayTime);
		timerRebound.stop = false;
	}

	void OnGrab(UBasketball ball)
	{
		if (ball.m_owner != m_player)
			timerRebound.stop = true;
	}

	void DoRebound()
	{
		timerRebound.stop = true;
		m_system.SetTransaction(AIState.Type.eReboundStorm_Rebound);
	}
}