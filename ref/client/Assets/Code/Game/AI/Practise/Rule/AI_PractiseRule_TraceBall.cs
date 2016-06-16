using UnityEngine;
using System.Collections;

public class AI_PractiseRule_TraceBall : AIState
{
	public AI_PractiseRule_TraceBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseRule_TraceBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		
		UBasketball ball = m_match.mCurScene.mBall;
		if( ball.m_owner == m_player )
			m_system.SetTransaction(AIState.Type.ePractiseRule_CheckBall);
		else
		{
			IM.Vector3 vMoveTarget = ball.position;
			vMoveTarget.y = IM.Number.zero;
			m_moveTarget = vMoveTarget;
		}
	}
}

