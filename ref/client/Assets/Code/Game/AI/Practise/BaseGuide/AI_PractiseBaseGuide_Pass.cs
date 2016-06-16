using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseBaseGuide_Pass : AIState
{
	PractiseBehaviourBaseGuide behaviour;

	public AI_PractiseBaseGuide_Pass(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_Pass;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourBaseGuide;

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Pass);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
		m_player.m_passTarget = behaviour.npc;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePass)
			m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Idle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}