using UnityEngine;

public class AI_ReboundStorm_ShooterIdle : AIState
{
	public AI_ReboundStorm_ShooterIdle(AISystem owner)
		: base(owner)
	{
		m_eType = AIState.Type.eReboundStorm_ShooterIdle;
	}

	override public void OnEnter(AIState lastState)
	{
		m_player.moveDirection = IM.Vector3.zero;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_bWithBall)
			m_system.SetTransaction(AIState.Type.eReboundStorm_Shoot);
	}
}