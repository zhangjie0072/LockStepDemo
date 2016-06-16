using UnityEngine;

public class AI_ReboundStorm_Idle : AIState
{
	GameMatch_ReboundStorm match;

	public AI_ReboundStorm_Idle(AISystem owner)
		: base(owner)
	{
		m_eType = AIState.Type.eReboundStorm_Idle;
		match = m_match as GameMatch_ReboundStorm;
	}

	override public void OnEnter(AIState lastState)
	{
		m_player.moveDirection = IM.Vector3.zero;
	}

	override public void Update(IM.Number fDeltaTime)
	{
		if (match.currBall != null)
			m_system.SetTransaction(AIState.Type.eReboundStorm_Positioning);
	}
}