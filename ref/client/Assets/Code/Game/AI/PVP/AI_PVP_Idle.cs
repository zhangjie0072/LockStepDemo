using UnityEngine;

public class AI_PVP_Idle : AIState
{
	public AI_PVP_Idle(AISystem owner)
		: base(owner)
	{
		m_eType = AIState.Type.ePVP_Idle;
	}

	override public void OnEnter(AIState lastState)
	{
		m_player.moveDirection = IM.Vector3.zero;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_bWithBall)
			m_system.SetTransaction(AIState.Type.ePVP_Positioning);
		else
		{
			if(m_ball.m_ballState == BallState.eLoseBall)
				m_system.SetTransaction(AIState.Type.ePVP_TraceBall);
		}
	}
}