using UnityEngine;
using System.Collections.Generic;

public class AI_MassBall_TraceBall : AIState
{
	private GameMatch_MassBall match;
	private UBasketball targetBall;

	public AI_MassBall_TraceBall(AISystem owner)
		: base(owner)
	{
		m_eType = Type.eMassBall_TraceBall;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		match = m_match as GameMatch_MassBall;
		m_player.moveDirection = IM.Vector3.zero;
		//m_moveTarget = m_player.position;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if (targetBall != null && !m_player.m_bWithBall)
			m_moveTarget = targetBall.position;
		else
		{
			m_player.moveDirection = IM.Vector3.zero;
			//m_moveTarget = m_player.position;
		}

		base.Update(fDeltaTime);

		if (match.mCurScene.balls.Count == 0 || match.m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			m_player.moveDirection = IM.Vector3.zero;

		if (m_player.m_bWithBall && m_player.m_ball == targetBall)
			targetBall = null;
	}

	protected override void OnTick()
	{
		if (targetBall == null && !m_player.m_bWithBall)
		{
			targetBall = SelectTargetBall();
		}
		else
		{
			if (targetBall != null && targetBall.m_owner != null && targetBall.m_owner != m_player)
			{
				targetBall = null;
			}
		}

		if (m_player.m_bWithBall)
			OnGrab(m_player.m_ball);
	}

	private UBasketball SelectTargetBall()
	{
		if (match.mCurScene.balls.Count == 0)
			return null;

		if (match.level == GameMatch.Level.Easy)
		{
			return SelectRandomBall();
		}
		else
		{
			return SelectBestBall();
		}
	}

	private UBasketball SelectRandomBall()
	{
		List<UBasketball> list = new List<UBasketball>();
		foreach (UBasketball ball in match.mCurScene.balls)
		{
			if (ball.m_ballState == BallState.eLoseBall)
				list.Add(ball);
		}
		if (list.Count == 0)
			return null;
		return list[IM.Random.Range(0, list.Count - 1)];
	}

	private UBasketball SelectBestBall()
	{
		//Sort balls by distance
		List<KeyValuePair<UBasketball, IM.Number>> listMine = AIUtils.SortBallListByDistance(match.mCurScene.balls, m_player.position);
		if (listMine.Count == 0)
			return null;

		List<KeyValuePair<UBasketball, IM.Number>> listRival = AIUtils.SortBallListByDistance(match.mCurScene.balls, match.mainRole.position);
		if (listRival.Count == 0)
			return null;

		UBasketball rivalNearestSpecialBall = match.mainRole.m_bWithBall ? null : listRival.Find(AIUtils.IsSpecial).Key;
		//Find my nearest catchable special ball
		KeyValuePair<UBasketball, IM.Number> target = new KeyValuePair<UBasketball, IM.Number>();
		foreach (KeyValuePair<UBasketball, IM.Number> pair in listMine)
		{
			if (pair.Key.m_special && (pair.Key != rivalNearestSpecialBall || CanArriveBeforePlayer(pair.Key.position)))
			{
				target = pair;
				break;
			}
		}
		if (target.Key == null)
			target = listMine[0];
		else
		{
			//The nearest special ball is farther than max distance, compare it with the nearest non-special ball.
			if (target.Value > match.MAX_SPECIAL_BALL_DIST)
			{
				KeyValuePair<UBasketball, IM.Number> myNearestNonspecial = listMine.Find(AIUtils.NotSpecial);
				if (myNearestNonspecial.Key != null && (target.Value - myNearestNonspecial.Value) > match.SPECIAL_BALL_DIST_DIFF_THRESHOLD)
					target = myNearestNonspecial;
			}
		}
		return target.Key;
	}

	private void OnGrab(UBasketball ball)
	{
		if (match.level == GameMatch.Level.Hard)
			AIUtils.PositionAndShoot(AIState.Type.eMassBall_Positioning, AIState.Type.eMassBall_Shoot, match, m_player);
		else
			m_system.SetTransaction(AIState.Type.eMassBall_Shoot);
	}
}
