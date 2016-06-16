using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseGuide_Layup : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_Layup(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Layup;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Layup);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (!m_player.m_bWithBall)
		{
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
			return;
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}