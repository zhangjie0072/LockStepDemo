using UnityEngine;

public class AI_Init
	: AIState
{
	public AI_Init(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eInit;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if( m_system.m_enable )
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
	}
}