using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseGuide_Pass : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_Pass(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Pass;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		Player mate = null;
		foreach (Player player in m_player.m_team.members)
		{
			if (player != m_player)
			{
				mate = player;
				break;
			}
		}

		List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Pass);
		if( basicSkills.Count != 0 )
			m_player.m_toSkillInstance = basicSkills[0];
		m_player.m_passTarget = mate;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (!m_player.m_bWithBall && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePass)
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