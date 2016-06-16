using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseGuide_Shoot : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_Shoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Shoot;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		m_player.m_toSkillInstance = ShootHelper.ShootByArea(m_player, m_match);
		m_player.m_bForceShoot = true;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot &&
			m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
		{
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}