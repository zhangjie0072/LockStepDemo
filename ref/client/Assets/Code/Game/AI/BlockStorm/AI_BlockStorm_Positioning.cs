using UnityEngine;

public class AI_BlockStorm_Positioning : AIState
{
	IM.Vector3 basketCenter;

	public AI_BlockStorm_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBlockStorm_Positioning;
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

		if (GameUtils.HorizonalDistance(m_player.position, basketCenter) <= new IM.Number(2,400))
		{
			m_system.SetTransaction(AIState.Type.eBlockStorm_Layup, new IM.Number(50));
            m_system.SetTransaction(AIState.Type.eBlockStorm_Dunk, new IM.Number(50));
		}
	}
}