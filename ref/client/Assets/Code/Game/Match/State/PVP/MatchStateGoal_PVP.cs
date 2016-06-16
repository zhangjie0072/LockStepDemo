using System;
using UnityEngine;

public class MatchStateGoal_PVP
	:MatchStateGoal
{
	public MatchStateGoal_PVP(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eGoal;
	}

	override public void OnEnter(MatchState lastState)
	{
		base.OnEnter(lastState);

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = true;
			player.m_enablePickupDetector = false;
		}

		if( !m_goalOwner.m_bSimulator )
			GameMsgSender.SendGameGoal(m_goalOwner, (uint)m_match.mCurScene.mBall.m_pt, m_bCriticalShoot);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		UBasketball ball = m_match.mCurScene.mBall;
		if( m_goalOwner != null 
		   && ( m_goalOwner.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand
		    || m_goalOwner.m_StateMachine.m_curState.m_eState == PlayerState.State.eRun 
		    || m_goalOwner.m_StateMachine.m_curState.m_eState == PlayerState.State.eRush)
		    )
		{
			if( ball.m_ballState == BallState.eLoseBall )
			{
				m_goalOwner.m_StateMachine.SetState(PlayerState.State.eGoalPose);
				m_goalOwner = null;
			}
		}
	}
	
	override public void OnExit ()
	{
		m_goalOwner = null;
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
