using UnityEngine;
using System.Collections.Generic;

public class AI_Pass
	: AIState
{
	public Player	m_toPass;
	bool noPassSkill;
	
	public AI_Pass(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePass;
		m_toPass = null;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		IM.Number fMinDist = IM.Number.max;

		if( m_toPass == null )
		{
            Player mainRole = m_match.GetMainRole(m_player.m_roleInfo.acc_id);
			if (mainRole != null && mainRole.m_team == m_player.m_team && mainRole != m_player &&
				mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall)
				m_toPass = mainRole;
			else
				m_toPass = AIUtils.ChoosePassTarget(m_player);
		}

		m_player.m_passTarget = m_toPass;
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Pass, true);
		noPassSkill = (m_player.m_toSkillInstance == null);
		if (noPassSkill)
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Pass");
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if ((!m_player.m_bWithBall || noPassSkill) && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePass)
		{
			m_system.SetTransaction(AIState.Type.eIdle);
			return;
		}
	}
	
	override public void OnExit ()
	{
		m_toPass = null;
		m_player.m_toSkillInstance = null;
	}
}