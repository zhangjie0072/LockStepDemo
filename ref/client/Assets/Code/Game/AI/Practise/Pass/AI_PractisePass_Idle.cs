using UnityEngine;

public class AI_PractisePass_Idle : AIState
{
	GameUtils.Timer m_timer;
	PractiseBehaviourPass behaviour;

	public AI_PractisePass_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractisePass_Idle;
		m_timer = new GameUtils.Timer(IM.Number.one, _OnTimer);
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourPass;

        IM.Number idleTime = IM.Random.Range(IM.Number.zero, IM.Number.one);
        m_timer.SetTimer(idleTime);
		m_timer.stop = false;
		
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	void _OnTimer()
	{
		if (!m_player.m_bWithBall)
		{
			m_system.SetTransaction(AIState.Type.ePractisePass_Positioning, new IM.Number(30));
			m_system.SetTransaction(AIState.Type.ePractisePass_Idle, new IM.Number(70));
		}
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		UBasketball ball = m_match.mCurScene.mBall;		
		if(ball != null && ball.m_owner == null && ball.m_ballState != BallState.eUseBall_Pass)
		{
			m_system.SetTransaction(AIState.Type.ePractisePass_TraceBall);
			return;
		}

		if ( m_player.m_bWithBall && 
			(!behaviour.in_tutorial || m_match.m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall))
			m_system.SetTransaction(AIState.Type.ePractisePass_Pass);
		
		m_timer.Update(fDeltaTime);
	}
	
	override public void OnExit ()
	{
		m_timer.Reset();
		m_timer.stop = true;
	}
}