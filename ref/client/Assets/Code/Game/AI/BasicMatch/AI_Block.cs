using UnityEngine;
using System.Collections.Generic;

public class AI_Block
	: AIState
{
	public AI_Block(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBlock;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Block, true);
		if (m_player.m_toSkillInstance == null)
		{
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Block");
		}
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eBlock)
			m_system.SetTransaction(AIState.Type.eDefense);
	}
	
	override public void OnExit ()
	{
	}
}