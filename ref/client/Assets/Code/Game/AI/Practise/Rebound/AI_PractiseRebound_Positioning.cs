using UnityEngine;

public class AI_PractiseRebound_Positioning : AIState
{
	GameUtils.Timer timer;
	IM.Vector3 basketCenter;
	bool atMoveTarget;
	PractiseBehaviourRebound behaviour;

	public AI_PractiseRebound_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseRebound_Positioning;
		timer = new GameUtils.Timer(new IM.Number(0,100), OnTimer);
		timer.stop = true;
	}
	
	override public void OnEnter(AIState lastState)
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourRebound;

		basketCenter = m_match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		m_moveTarget = SeekPositioningTarget();
		atMoveTarget = false;
	}
	
	IM.Vector3 SeekPositioningTarget()
	{
		if (behaviour.in_tutorial)
			return new IM.Vector3(new IM.Number(5), IM.Number.zero,new IM.Number(6));

        IM.Number angle = IM.Random.Range(new IM.Number(90),new IM.Number(270));
        IM.Number dist = IM.Random.Range(new IM.Number(4, 500), new IM.Number(7));
		IM.Vector3 dir = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * IM.Vector3.forward;
		IM.Vector3 pos = basketCenter + dir * dist;
        return pos;
	}

	void OnTimer()
	{
		m_system.SetTransaction(AIState.Type.ePractiseRebound_Shoot);
		timer.stop = true;
	}

	public override void ArriveAtMoveTarget()
	{
		if (!atMoveTarget)
		{
			timer.stop = false;
			atMoveTarget = true;
		}
	}
		
	override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if (behaviour.in_tutorial && behaviour.step != PractiseBehaviourRebound.Step.Shoot)
			m_system.SetTransaction(AIState.Type.ePractiseRebound_Idle);
		timer.Update(fDeltaTime);
	}
}