using UnityEngine;

public class AI_PractiseGuide_Idle : AI_PractiseGuide_Base
{
	public AI_PractiseGuide_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseGuide_Idle;
	}
	
	public override void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		m_player.m_moveHelper.StopMove();
	}
	
	public override void Update(IM.Number fDeltaTime)
	{
		PractiseStepBehaviour stepBehaviour = behaviour.GetBehaviour(system.index);
		switch (stepBehaviour)
		{
			case PractiseStepBehaviour.Face2Mate:
				system.SetTransaction(AIState.Type.ePractiseGuide_Face2Mate);
				break;
			case PractiseStepBehaviour.PickPass2Mate:
				if (m_ball.m_ballState == BallState.eLoseBall)
					system.SetTransaction(AIState.Type.ePractiseGuide_TraceBall);
				break;
			case PractiseStepBehaviour.Run2Basket:
				system.SetTransaction(AIState.Type.ePractiseGuide_Positioning);
				break;
			case PractiseStepBehaviour.Layup:
				if (m_player.m_bWithBall)
					system.SetTransaction(AIState.Type.ePractiseGuide_Layup);
				break;
			case PractiseStepBehaviour.Shoot:
				if (m_player.m_bWithBall)
					system.SetTransaction(AIState.Type.ePractiseGuide_Shoot);
				break;
			case PractiseStepBehaviour.Defense:
				system.SetTransaction(AIState.Type.ePractiseGuide_Defense);
				break;
		}
	}
}