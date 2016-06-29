using UnityEngine;

public class AI_PractiseGuide_Base : AIState
{
	protected PractiseBehaviourGuide behaviour;
	protected AISystem_PractiseGuide system;

	public AI_PractiseGuide_Base(AISystem owner)
		:base(owner)
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourGuide;
		system = owner as AISystem_PractiseGuide;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		Debug.Log("PractiseStep, AI enter " + m_eType + ". " + m_player.m_name);
	}
}