using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseBaseGuide_RequireBall : AIState
{
	public AI_PractiseBaseGuide_RequireBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_RequireBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.RequireBall);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRequireBall)
		{
			m_system.SetTransaction(Type.ePractiseBaseGuide_Idle);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}