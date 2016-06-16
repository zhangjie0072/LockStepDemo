using UnityEngine;

public class AI_PractiseRebound_Idle : AIState
{
	PractiseBehaviourRebound behaviour;

	public AI_PractiseRebound_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseRebound_Idle;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourRebound;
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (behaviour.in_tutorial)
		{
			if (behaviour.step == PractiseBehaviourRebound.Step.Shoot && m_player.m_bWithBall)
				m_system.SetTransaction(AIState.Type.ePractiseRebound_Positioning);
		}
		else
		{
			if (m_player.m_bWithBall)
				m_system.SetTransaction(AIState.Type.ePractiseRebound_Positioning);
		}
	}
}