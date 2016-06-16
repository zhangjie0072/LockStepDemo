using UnityEngine;

public class AI_PractiseGuide_Face2Mate : AI_PractiseGuide_Base
{
	Player mate;

	public AI_PractiseGuide_Face2Mate(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Face2Mate;
	}
	
	public override void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		m_player.moveDirection = IM.Vector3.zero;
		m_player.m_moveType = fogs.proto.msg.MoveType.eMT_Stand;

		foreach (Player player in m_player.m_team.members)
		{
			if (player != m_player)
			{
				mate = player;
				break;
			}
		}
	}
	
	public override void Update(IM.Number fDeltaTime)
	{
		m_player.forward = GameUtils.HorizonalNormalized(mate.position, m_player.position);

		PractiseStepBehaviour stepBehaviour = behaviour.GetBehaviour(system.index);
		if (stepBehaviour != PractiseStepBehaviour.Face2Mate)
			system.SetTransaction(AIState.Type.ePractiseGuide_Idle);
	}
}