using UnityEngine;
using System.Collections.Generic;

public class AI_TraceBall_Null : AIState
{
	public AI_TraceBall_Null(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eTraceBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		//m_system.SetTransaction(AIState.Type.eIdle);
	}

	public override void Update(IM.Number fDeltaTime)
	{
		//base.Update(fDeltaTime);

		//m_system.SetTransaction(AIState.Type.eIdle);

		//m_player.m_vVelocity = Vector3.zero;
		//if (m_match.m_stateMachine.m_curState.m_eState == MatchState.State.eBegin)
			m_system.SetTransaction(AIState.Type.eInit);
	}
}