using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseBaseGuide_Layup : AIState
{
	PractiseBehaviourBaseGuide behaviour;

	public AI_PractiseBaseGuide_Layup(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_Layup;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourBaseGuide;

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Layup);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (behaviour.step == PractiseBehaviourBaseGuide.Step.DefenseRetutor)
			m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Idle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}