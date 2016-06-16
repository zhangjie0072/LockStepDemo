using UnityEngine;

public class AI_ReboundStorm_Shoot : AIState
{
	public AI_ReboundStorm_Shoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eReboundStorm_Shoot;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.m_toSkillInstance = ShootHelper.ShootByArea(m_player, m_match);

		m_player.m_bForceShoot = true;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot &&
			m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
			m_system.SetTransaction(AIState.Type.eReboundStorm_ShooterIdle);
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}