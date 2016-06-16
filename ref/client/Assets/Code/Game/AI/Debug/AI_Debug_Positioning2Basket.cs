using UnityEngine;

public class AI_Debug_Positioning2Basket : AIState
{
	public AI_Debug_Positioning2Basket(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eDebug_Positioning2Basket;
	}

	public override void OnEnter ( AIState lastState )
	{
        IM.Vector3 target = m_match.mCurScene.mBasket.m_vShootTarget;
		target.y = IM.Number.zero;
		//target.x += 1f;
		m_moveTarget = target;
		m_bForceNotRush = true;
	}

	public override void OnPlayerCollided(Player colPlayer)
	{
		Logger.Log("Player collide: " + colPlayer.m_name);
	}
}
