using UnityEngine;

public class AI_GrabPoint_Positioning : AIState
{
	public AI_GrabPoint_Positioning(AISystem owner)
		: base(owner)
	{
		m_eType = Type.eGrabPoint_Positioning;
	}

	public override void OnEnter(AIState lastState)
	{
	}

	public override void ArriveAtMoveTarget()
	{
		if( m_player.m_bWithBall )
			m_system.SetTransaction(AIState.Type.eGrabPoint_Shoot);
	}
}
