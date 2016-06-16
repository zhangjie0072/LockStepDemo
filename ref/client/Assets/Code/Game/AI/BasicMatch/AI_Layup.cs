using UnityEngine;

public class AI_Layup
	: AIState
{
	private const int	m_iNearLayup 	= 7050;
	private const int	m_iFarLayup	 	= 7051;

	public AI_Layup(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eLayup;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Layup, true);
		if (m_player.m_toSkillInstance == null)
			Logger.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Layup");
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eLayup)
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}