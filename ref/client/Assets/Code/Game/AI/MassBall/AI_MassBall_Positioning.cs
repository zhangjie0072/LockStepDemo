using UnityEngine;

public class AI_MassBall_Positioing : AIState
{
	public AI_MassBall_Positioing(AISystem owner)
		: base(owner)
	{
		m_eType = Type.eMassBall_Positioning;
	}

	public override void OnEnter(AIState lastState)
	{
	}

	public override void ArriveAtMoveTarget()
	{
		m_system.SetTransaction(AIState.Type.eMassBall_Shoot);
	}
}
