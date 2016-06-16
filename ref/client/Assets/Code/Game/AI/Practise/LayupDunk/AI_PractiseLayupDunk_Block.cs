using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseLayupDunk_Block : AIState
{
	public AI_PractiseLayupDunk_Block(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseLayupDunk_Block;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Block);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eBlock)
			m_system.SetTransaction(AIState.Type.ePractiseLayupDunk_Defense);
	}
	
	override public void OnExit ()
	{
	}
}