using UnityEngine;
using System.Collections.Generic;

public class AI_BullFight_Positioning : AIState
{
	public IM.Vector3 moveTarget;
	public bool arrived;
	public delegate void Delegate(AI_BullFight_Positioning state);
	public Delegate onArrive;

	public AI_BullFight_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eBullFight_Positioning;
		m_bForceNotRush = true;
	}

	public override void OnEnter(AIState lastState)
	{
		arrived = false;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		m_moveTarget = moveTarget;
	}

	public override void ArriveAtMoveTarget()
	{
		arrived = true;
		if (onArrive != null)
			onArrive(this);
	}
}