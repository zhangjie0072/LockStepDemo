using UnityEngine;

public class AI_PractiseRule_Idle : AIState
{
	GameMatch_Practise match;
	PractiseBehaviourRule practise_behaviour;

	public AI_PractiseRule_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseRule_Idle;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;

		match = m_match as GameMatch_Practise;
		practise_behaviour = match.practise_behaviour as PractiseBehaviourRule;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player == practise_behaviour.npc1)
		{
			if (practise_behaviour.step == PractiseBehaviourRule.Step.Score && m_player.m_bWithBall)
			{
				m_system.SetTransaction(AIState.Type.ePractiseRule_Shoot);
			}
			else if (practise_behaviour.step == PractiseBehaviourRule.Step.SwitchRole && m_match.mCurScene.mBall.m_owner == null)
			{
				m_system.SetTransaction(AIState.Type.ePractiseRule_TraceBall);
			}
		}
		else if (m_player == practise_behaviour.npc2)
		{
			if (practise_behaviour.step == PractiseBehaviourRule.Step.SwitchRole && m_player.m_bWithBall)
			{
				m_system.SetTransaction(AIState.Type.ePractiseRule_Shoot);
			}
		}
	}
}