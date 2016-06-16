using UnityEngine;

public class AI_PractiseLayupDunk_Positioning : AIState
{
	public delegate void Delegate();
	public Delegate onEnterLayupArea;

	IM.Vector3 basketCenter;

	public AI_PractiseLayupDunk_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseLayupDunk_Positioning;
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

		if (GameUtils.HorizonalDistance(m_player.position, basketCenter) <= new IM.Number(2,700))
		{
			if (onEnterLayupArea != null)
				onEnterLayupArea();
		}
	}
}