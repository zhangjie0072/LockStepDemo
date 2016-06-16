using UnityEngine;
using System.Collections;

public class AI_PractiseShoot_TraceBall : AIState
{
	public AI_PractiseShoot_TraceBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseShoot_TraceBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		
		UBasketball ball = m_match.mCurScene.mBall;
		if( ball.m_owner != null )
			m_system.SetTransaction(AIState.Type.ePractiseShoot_Idle);
		else
		{
			IM.Vector3 vMoveTarget = ball.position;
			vMoveTarget.y = IM.Number.zero;
			m_moveTarget = vMoveTarget;
		}
	}
}

