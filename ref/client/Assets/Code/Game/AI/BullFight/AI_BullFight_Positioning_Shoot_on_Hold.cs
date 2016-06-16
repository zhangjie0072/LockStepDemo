using UnityEngine;

using System.Collections.Generic;
using System.Linq;

public class AI_Positioning_Shoot_on_Hold : AI_Positioning
{
	public AI_Positioning_Shoot_on_Hold(AISystem owner)
		:base(owner)
	{
	}

	override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold && m_player.m_bMovedWithBall)
		{
			m_system.SetTransaction(AIState.Type.eShoot);
		}
	}
}