using UnityEngine;
using System.Collections.Generic;

public class AI_PractisePass_Pass : AIState
{
	public AI_PractisePass_Pass(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractisePass_Pass;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Pass);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
		m_player.m_passTarget = m_match.mainRole;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePass)
			m_system.SetTransaction(AIState.Type.ePractisePass_Idle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}