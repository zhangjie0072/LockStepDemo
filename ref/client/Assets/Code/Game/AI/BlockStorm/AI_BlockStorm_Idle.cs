using UnityEngine;

public class AI_BlockStorm_Idle : AIState
{
	public AI_BlockStorm_Idle(AISystem owner)
		: base(owner)
	{
		m_eType = AIState.Type.eBlockStorm_Idle;
	}

	override public void OnEnter(AIState lastState)
	{
		m_player.moveDirection = IM.Vector3.zero;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_bWithBall)
			m_system.SetTransaction(AIState.Type.eBlockStorm_Positioning);
	}
}