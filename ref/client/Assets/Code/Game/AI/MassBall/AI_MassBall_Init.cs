using UnityEngine;

public class AI_MassBall_Init
	: AIState
{
	public AI_MassBall_Init(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eMassBall_Init;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if( m_system.m_enable )
			m_system.SetTransaction(AIState.Type.eMassBall_TraceBall);
	}
	
	override public void OnExit ()
	{
	}
}