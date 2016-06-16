using UnityEngine;

public class AI_Assist_Init : AIState
{
	public Type nextState = Type.eAssistInit;

	public AI_Assist_Init(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eAssistInit;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if( m_system.m_enable )
			m_system.SetTransaction(nextState);
	}
	
	override public void OnExit ()
	{
	}
}