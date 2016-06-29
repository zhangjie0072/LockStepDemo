using UnityEngine;

public class AI_Dunk
	: AIState
{
	private const int	m_iNearDunk 	= 7100;
	private const int	m_iFarDunk	 	= 7101;

	public AI_Dunk(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eDunk;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Dunk, true);
		if (m_player.m_toSkillInstance == null)
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Dunk");
	}

    override public void Update(IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eDunk)
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}