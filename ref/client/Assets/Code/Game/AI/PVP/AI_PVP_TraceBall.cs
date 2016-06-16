using UnityEngine;

public class AI_PVP_TraceBall : AIState
{
	public AI_PVP_TraceBall(AISystem owner)
		: base(owner)
	{
		m_eType = AIState.Type.ePVP_TraceBall;
	}

	override public void OnEnter(AIState lastState)
	{
		m_player.moveDirection = IM.Vector3.zero;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_ball.m_owner != null )
			m_system.SetTransaction(AIState.Type.ePVP_Idle);
		else
		{
			IM.Vector3 target = m_ball.position; 
			target.y = IM.Number.zero;
			m_moveTarget = target;
		}
	}
}