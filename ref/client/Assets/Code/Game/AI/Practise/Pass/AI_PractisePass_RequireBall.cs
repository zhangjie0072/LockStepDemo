using UnityEngine;
using System.Collections.Generic;

public class AI_PractisePass_RequireBall : AIState
{
	private AIState m_lastState;

	public AI_PractisePass_RequireBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractisePass_RequireBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.RequireBall);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];

		if( lastState.m_eType == Type.ePractisePass_Positioning )
		{
			m_lastState = lastState;
			m_moveTarget = lastState.m_moveTarget;
		}
		else
			m_lastState = null;

	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRequireBall)
		{
			if( m_lastState != null )
				m_system.SetTransaction(m_lastState);
			else
				m_system.SetTransaction(Type.ePractisePass_Positioning);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}