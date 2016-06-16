using UnityEngine;
using fogs.proto.msg;

public class AI_BlockStorm_Dunk : AIState
{
	private const int	m_iNearDunk 	= 7100;
	private const int	m_iFarDunk	 	= 7101;

	public AI_BlockStorm_Dunk(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBlockStorm_Dunk;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		Area eArea = m_match.mCurScene.mGround.GetDunkArea(m_player);
		if( eArea == Area.eNear )
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(m_iNearDunk);
		if( eArea == Area.eMiddle )
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(m_iFarDunk);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eDunk)
			m_system.SetTransaction(AIState.Type.eBlockStorm_Idle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}