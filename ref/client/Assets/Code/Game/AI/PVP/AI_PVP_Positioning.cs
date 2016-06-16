using UnityEngine;

public class AI_PVP_Positioning : AIState
{
	IM.Vector3 basketCenter;

	public AI_PVP_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePVP_Positioning;
	}
	
	override public void OnEnter(AIState lastState)
	{
		basketCenter = m_match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		m_moveTarget = basketCenter;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		//m_system.SetTransaction(AIState.Type.ePVP_Shoot);
		m_system.SetTransaction(AIState.Type.ePVP_Layup);
		//m_system.SetTransaction(AIState.Type.ePVP_Dunk);
	}
}