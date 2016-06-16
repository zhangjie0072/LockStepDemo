using UnityEngine;
using System.Collections.Generic;

public class AI_GrabZone_TraceBall : AIState
{
	private GameMatch match;
	private UBasketball targetBall;

	public AI_GrabZone_TraceBall(AISystem owner)
		: base(owner)
	{
		m_eType = Type.eGrabZone_TraceBall;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		match = m_match;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if (targetBall != null)
			m_moveTarget = targetBall.position;

		base.Update(fDeltaTime);

		if (match.mCurScene.balls.Count == 0 || match.m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			m_player.moveDirection = IM.Vector3.zero;

		if (targetBall == null)
		{
			targetBall = SelectTargetBall();
		}
		else
		{
			if (targetBall.m_owner != null && targetBall.m_owner != m_player)
			{
				targetBall = null;
				m_player.moveDirection = IM.Vector3.zero;
				//m_moveTarget = m_player.position;
			}
		}

		if (m_player.m_bWithBall)
			OnGrab(m_player.m_ball);
	}

	private UBasketball SelectTargetBall()
	{
		UBasketball target = null;
		List<KeyValuePair<UBasketball, IM.Number>> list = AIUtils.SortBallListByDistance(match.mCurScene.balls, m_player.position);
		if (list.Count > 0)
		{
			target = list[0].Key;
			if (match.level != GameMatch.Level.Easy)
			{
				//Is this ball catchable.
				if (!match.m_mainRole.m_bWithBall)
				{
					if (!CanArriveBeforePlayer(target.position))
					{
						List<KeyValuePair<UBasketball, IM.Number>> listRival = AIUtils.SortBallListByDistance(match.mCurScene.balls, match.m_mainRole.position);
						if (listRival[0].Key == target)
						{
							if (list.Count > 1)
							{
								//Select second nearest ball.
								target = list[1].Key;
							}
						}
					}
				}
			}
		}
		return target;
	}

	private void OnShoot(UBasketball ball)
	{
		//if (ball != targetBall)
		//	Logger.LogError("Not target ball.");

		ball.onShoot -= OnShoot;
		targetBall = null;
	}

	private void OnGrab(UBasketball ball)
	{
		m_system.SetTransaction(AIState.Type.eGrabZone_Positioning);
		if (targetBall == m_ball)
			targetBall = null;
	}

	public override void OnPlayerCollided(Player colPlayer)
	{
		base.OnPlayerCollided(colPlayer);
		Logger.Log("AI_GrabZone_TraceBall.OnPlayerCollided.");
		m_system.SetTransaction(AIState.Type.eGrabZone_AvoidDefender);
	}
}
