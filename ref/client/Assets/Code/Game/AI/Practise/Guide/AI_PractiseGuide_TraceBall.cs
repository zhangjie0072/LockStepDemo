using UnityEngine;

public class AI_PractiseGuide_TraceBall : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_TraceBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_TraceBall;
	}
	
	public override void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_bWithBall)
		{
			if (behaviour.GetBehaviour(system.index) == PractiseStepBehaviour.PickPass2Mate &&
				(m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand ||
				m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold) )
				system.SetTransaction(AIState.Type.ePractiseGuide_Pass);
		}
		else if (m_match.mCurScene.mBall.m_owner != null)
		{
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
		}
		else
			m_moveTarget = m_match.mCurScene.mBall.position;
	}

	public override void OnPlayerCollided(Player colPlayer)
	{
		base.OnPlayerCollided(colPlayer);

		m_system.SetTransaction(AIState.Type.ePractiseGuide_Positioning);
	}
}