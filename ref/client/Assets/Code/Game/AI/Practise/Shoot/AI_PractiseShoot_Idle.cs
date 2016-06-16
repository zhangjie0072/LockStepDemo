using UnityEngine;

public class AI_PractiseShoot_Idle : AIState
{
	public AI_PractiseShoot_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseShoot_Idle;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		UBasketball ball = m_match.mCurScene.mBall;		
		if(ball != null 
		   && ball.m_ballState != BallState.eUseBall
		   && ball.m_ballState != BallState.eUseBall_Pass)
		{
			m_system.SetTransaction(AIState.Type.ePractiseShoot_TraceBall);
			return;
		}

		if (m_player.m_bWithBall && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePickup)
			m_system.SetTransaction(AIState.Type.ePractiseShoot_Pass);
	}
}