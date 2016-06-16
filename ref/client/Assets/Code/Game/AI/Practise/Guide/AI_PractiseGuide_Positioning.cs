using UnityEngine;

public class AI_PractiseGuide_Positioning : AI_PractiseGuide_Base
{
	IM.Vector3 shootTarget;

	public AI_PractiseGuide_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Positioning;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);
        shootTarget = m_match.mCurScene.mBasket.m_vShootTarget;

		PractiseStepBehaviour stepBehaviour = behaviour.GetBehaviour(system.index);
		if (stepBehaviour == PractiseStepBehaviour.PickPass2Mate)
		{
			Player mate = m_player.m_team.members.Find(p => p != m_player);
			IM.Vector3 dirPlayer2Mate = GameUtils.HorizonalNormalized(mate.position, m_player.position);
			IM.Vector3 dirPlayer2Basket = GameUtils.HorizonalNormalized(shootTarget, m_player.position);
			bool left = IM.Vector3.Cross(dirPlayer2Basket, dirPlayer2Mate).y < 0;
            IM.Vector3 dirMove = IM.Quaternion.AngleAxis(left ? new IM.Number(90) : -new IM.Number(90), IM.Vector3.up) * dirPlayer2Basket;
			m_moveTarget = m_player.position + dirMove;
		}
	}
	
	public override void Update(IM.Number fDeltaTime)
	{
		PractiseStepBehaviour stepBehaviour = behaviour.GetBehaviour(system.index);
		if (stepBehaviour == PractiseStepBehaviour.Run2Basket)
		{
			m_moveTarget = shootTarget;
		}
		else if (stepBehaviour == PractiseStepBehaviour.PickPass2Mate)
		{ }
		else
		{
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
		}
	}

	public override void ArriveAtMoveTarget()
	{
		base.ArriveAtMoveTarget();
		system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
	}
}