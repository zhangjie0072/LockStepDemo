using UnityEngine;
using System.Collections;

public class AI_PractiseRule_CheckBall : AI_CheckBall
{
	PractiseBehaviourRule behaviour;

	public AI_PractiseRule_CheckBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseRule_CheckBall;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourRule;
	}
	
	protected override void OnUpdate(IM.Number fDeltaTime)
	{
		if (behaviour.step == PractiseBehaviourRule.Step.Final)
			m_system.SetTransaction(AIState.Type.ePractiseRule_Idle);
	}
}