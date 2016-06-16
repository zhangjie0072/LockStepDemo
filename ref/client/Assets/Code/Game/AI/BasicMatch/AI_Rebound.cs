using UnityEngine;
using System.Collections.Generic;

public class AI_Rebound
	: AIState
{
	public AI_Rebound(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eRebound;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Rebound, true);
		if (m_player.m_toSkillInstance == null)
			Logger.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Rebound");
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound )
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
		//m_player.m_bToRebound = false;
		m_player.m_toSkillInstance = null;
	}
}