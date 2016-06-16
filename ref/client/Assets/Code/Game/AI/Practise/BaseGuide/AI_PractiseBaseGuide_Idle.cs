using UnityEngine;

public class AI_PractiseBaseGuide_Idle : AIState
{
	PractiseBehaviourBaseGuide behaviour;

	public AI_PractiseBaseGuide_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseBaseGuide_Idle;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		behaviour = (m_match as GameMatch_Practise).practise_behaviour as PractiseBehaviourBaseGuide;

		m_player.moveDirection = IM.Vector3.zero;
		m_player.m_moveType = fogs.proto.msg.MoveType.eMT_Stand;
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		if (behaviour.step == PractiseBehaviourBaseGuide.Step.ShowMe)
		{
			if (!m_player.m_bWithBall && m_ball.m_owner == null)
			{
				m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_TraceBall);
			}
			else if (m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePickup)
			{
				m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Pass);
			}
		}
		else if (behaviour.step == PractiseBehaviourBaseGuide.Step.Layup && m_player.m_bWithBall)
		{
			m_system.SetTransaction(AIState.Type.ePractiseBaseGuide_Positioning);
		}
	}
}