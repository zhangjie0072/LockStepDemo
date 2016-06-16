using UnityEngine;

public class AI_BlockStorm_Layup : AIState
{
	public AI_BlockStorm_Layup(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBlockStorm_Layup;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(7051);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eLayup)
			m_system.SetTransaction(AIState.Type.eBlockStorm_Idle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}