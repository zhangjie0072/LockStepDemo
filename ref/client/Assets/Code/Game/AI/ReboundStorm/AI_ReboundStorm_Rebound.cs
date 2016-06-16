using UnityEngine;
using System.Collections.Generic;

public class AI_ReboundStorm_Rebound : AIState
{
	public AI_ReboundStorm_Rebound(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eReboundStorm_Rebound;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Rebound);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound )
			m_system.SetTransaction(AIState.Type.eReboundStorm_Positioning);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}