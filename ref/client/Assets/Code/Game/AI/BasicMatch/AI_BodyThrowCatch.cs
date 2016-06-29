using UnityEngine;
using System.Collections.Generic;

public class AI_BodyThrowCatch : AIState
{
	public AI_BodyThrowCatch(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBodyThrowCatch;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.BodyThrowCatch, true);
		if (m_player.m_toSkillInstance == null)
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI BodyThrowCatch");
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eBodyThrowCatch)
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
	}
}