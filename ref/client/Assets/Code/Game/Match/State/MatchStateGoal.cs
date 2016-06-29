using System;
using UnityEngine;

public class MatchStateGoal
	:MatchState
{
	private IM.Number m_fCurTime = IM.Number.zero; 
	private IM.Number m_fWaitTime = new IM.Number(3);

	protected Player	m_goalOwner;
	protected bool m_bCriticalShoot = false;

	public MatchStateGoal(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eGoal;
	}

	override public void OnEnter(MatchState lastState)
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = false;
            player.m_enablePickupDetector = false;
			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = false;
		}
 
		UBasketball ball = m_match.mCurScene.mBall;
		if( ball.m_actor != null )
			m_goalOwner = ball.m_actor;

		if( m_goalOwner == null && ball.m_owner != null )
			m_goalOwner = ball.m_owner;

		int points =  m_match.GetScore(ball.m_pt);

		if (m_match.m_bTimeUp)	// critical shoot
		{
			if (Mathf.Abs(m_match.m_homeScore - m_match.m_awayScore) < points)
			{
				m_bCriticalShoot = true;
				m_match.ShowMatchTip("gameInterface_tip_critical", true);
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Critical);
				m_goalOwner.mStatistics.data.kill_goal = Convert.ToUInt32(m_bCriticalShoot);
			}
		}
		else if( m_match.m_b24TimeUp )
		{
			if(m_match.m_bOverTime)
			{
				m_bCriticalShoot = true;
				m_match.ShowMatchTip("gameInterface_tip_critical", true);
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Critical);
				m_goalOwner.mStatistics.data.kill_goal = Convert.ToUInt32(m_bCriticalShoot);
			}
			else
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.BuzzerBeater);
		}

		if( m_match.m_offenseTeam.m_side == Team.Side.eHome )
			m_match.m_homeScore += points;
		else
			m_match.m_awayScore += points;

		if (m_match.m_uiMatch != null)
		{
			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}

		m_fCurTime = IM.Number.zero;
	}
	
	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);

		UBasketball ball = m_match.mCurScene.mBall;
		if( ball == null )
			return;
		if( m_fCurTime > m_fWaitTime)
		{
			if( m_match.m_bOverTime )
				m_stateMachine.SetState(MatchState.State.eOver);
			else
			{
				if( !m_match.m_bTimeUp )
					m_stateMachine.SetState(MatchState.State.eBegin);
				else
					m_stateMachine.SetState(MatchState.State.eOver);
				m_fCurTime = IM.Number.zero;
			}
			return;
		}

		//TODO: bug when goal owner's state is not stand and run.
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

		m_fCurTime += fDeltaTime;
	}
	
	override public void OnExit ()
	{
		m_goalOwner = null;
        if (m_match.EnableSwitchRole())
            m_match.m_ruler.SwitchRole();

		m_bCriticalShoot = false;
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
