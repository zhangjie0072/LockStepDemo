using UnityEngine;

public class AI_PractiseBaseGuide_TraceBall : AIState
{
	PractiseBehaviourBaseGuide behaviour;

	public AI_PractiseBaseGuide_TraceBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_TraceBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourBaseGuide;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		m_moveTarget = m_match.mCurScene.mBall.position;

		if (behaviour.step == PractiseBehaviourBaseGuide.Step.ShowMe && m_player.m_bWithBall)
		{
			if (m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePickup)
				m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Pass);
		}
	}
}