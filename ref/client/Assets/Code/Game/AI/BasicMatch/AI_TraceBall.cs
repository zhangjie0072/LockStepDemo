using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

/**追球*/
public class AI_TraceBall
	: AIState
{
	GameUtils.Timer timerRebound;

	public AI_TraceBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eTraceBall;
		timerRebound = new GameUtils.Timer(IM.Number.one, OnTimerRebound, 1);
		timerRebound.stop = true;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);
		timerRebound.stop = true;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_player.CanRebound(m_ball) && (timerRebound == null || timerRebound.stop))
		{
			//Logger.Log("Rebound info of " + m_player.m_name);
			ReboundAttrConfig.ReboundAttr attr = GameSystem.Instance.ReboundAttrConfigData.GetReboundAttr(m_player.m_position);
			if (attr == null)
				Logger.LogError("Rebound height config error.");

			ShootSolution.SShootCurve curve = m_ball.CompleteLastCurve();
			IM.Number ballHeight = m_ball.position.y;
			ballHeight *= (IM.Number.one - m_system.AI.devBallHeight);
			if (ballHeight >= attr.minHeight)
			{
				//Logger.Log("Rebound max height: " + attr.maxHeight +" Ball height: " + ballHeight + " Rebound height scale: " + attr.reboundHeightScale + " Ball height scale: " + attr.ballHeightScale);
				IM.Number npcReboundBallHeight = AIUtils.GetNPCReboundBallHeight(attr.maxHeight, ballHeight, attr.reboundHeightScale, attr.ballHeightScale);
				//Logger.Log("NPC rebound ball height: " + npcReboundBallHeight);
                IM.Number time1, time2;
                curve.GetTimeByHeight(npcReboundBallHeight, out time1, out time2);
                IM.Number ballFlyTime = time2;
				//Logger.Log("Ball fly time: " + ballFlyTime);
				if (ballFlyTime < -new IM.Number(0,1))
					Logger.LogError("Ball fly time error.");

				SkillInstance basicRebound = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound)[0];
				string basicActionId = basicRebound.skill.actions[0].action_id;
                IM.Number frameRate = m_player.animMgr.GetFrameRate(basicActionId);
				Dictionary<string, PlayerAnimAttribute.AnimAttr> rebounds = m_player.m_animAttributes.m_rebound;
				int reboundKey = rebounds[m_player.animMgr.GetOriginName(basicActionId)].GetKeyFrame("OnRebound").frame;
				IM.Number reboundActionTime = reboundKey / frameRate;

				IM.Number reboundDelayTime = ballFlyTime - m_ball.m_fTime - reboundActionTime;
				//Logger.Log("Rebound delay time: " + reboundDelayTime);
				if (reboundDelayTime < IM.Number.zero)
				{
					reboundDelayTime = IM.Number.zero;
				}
				timerRebound.SetTimer(reboundDelayTime);
				timerRebound.stop = false;

				return;
			}
		}

		if (m_ball.m_owner != null)
			m_system.SetTransaction(AIState.Type.eIdle);
		else
		{
			IM.Vector3 vMoveTarget = m_ball.position;
			if (m_ball.m_ballState == BallState.eUseBall_Shoot)
				vMoveTarget = m_match.mCurScene.mBasket.m_vShootTarget;
			vMoveTarget.y = IM.Number.zero;
			m_moveTarget = vMoveTarget;
			m_player.m_moveType = fogs.proto.msg.MoveType.eMT_Rush;

			if (m_ball.m_ballState == BallState.eLoseBall &&
				(m_player.m_position == PositionType.PT_PG || m_player.m_position == PositionType.PT_SG))
			{
				IM.Vector3 ballPos = m_ball.position;
				IM.Number distToBall = GameUtils.HorizonalDistance(ballPos, m_player.position);
				if (distToBall < PlayerState_BodyThrowCatch.GetMaxDistance(m_player) && ballPos.y <= IM.Number.one)
					m_system.SetTransaction(AIState.Type.eBodyThrowCatch);
			}
		}

		if (timerRebound != null)
			timerRebound.Update(fDeltaTime);
	}

	void OnTimerRebound()
	{
		if (m_player.m_StateMachine.m_curState.IsCommandValid(Command.Rebound))
			m_system.SetTransaction(AIState.Type.eRebound);
	}
}

