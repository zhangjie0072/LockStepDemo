using UnityEngine;

public class AI_PractisePass_Positioning
		: AIState
{
	private GameUtils.Timer m_timer;
	private PractiseBehaviour m_behaviour;
	
	public AI_PractisePass_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractisePass_Positioning;
		m_timer = new GameUtils.Timer(IM.Number.one, _OnTimer);
	}
	
	override public void OnEnter(AIState lastState)
	{
		m_behaviour = (m_match as GameMatch_Practise).practise_behaviour;

		m_timer.SetTimer(IM.Random.Range(new IM.Number(0,100), IM.Number.half));
		m_timer.stop = false;

		if (lastState != null && lastState.m_eType == Type.ePractisePass_RequireBall)
			m_moveTarget = lastState.m_moveTarget;
		else
			m_moveTarget = SeekPositioningTarget();
	}
	
	void _OnTimer()
	{
		if (!m_player.m_bWithBall)
		{
			m_system.SetTransaction(AIState.Type.ePractisePass_Positioning, new IM.Number(70));
			m_system.SetTransaction(AIState.Type.ePractisePass_RequireBall, new IM.Number(30));
		}
	}
	IM.Vector3 SeekPositioningTarget()
	{
		return m_match.GenerateIn3PTPosition();
	}

	public override void ArriveAtMoveTarget()
	{
		m_moveTarget = SeekPositioningTarget();
		m_system.SetTransaction(AIState.Type.ePractisePass_Positioning, new IM.Number(20));
        m_system.SetTransaction(AIState.Type.ePractisePass_Idle, new IM.Number(80));
	}
		
	override public void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_bWithBall)
		{
			if (m_behaviour.in_tutorial)
				m_system.SetTransaction(AIState.Type.ePractisePass_Idle);
			else
				m_system.SetTransaction(AIState.Type.ePractisePass_Pass);
		}

		if (m_behaviour.in_tutorial)
		{
			m_moveTarget = m_player.position;
			m_system.SetTransaction(AIState.Type.ePractisePass_RequireBall);
			return;
		}

		base.Update(fDeltaTime);

		//trace ball
		UBasketball ball = m_match.mCurScene.mBall;
		if (ball.m_ballState != BallState.eUseBall_Pass)
		{
			m_system.SetTransaction(AIState.Type.ePractisePass_TraceBall);
			return;
		}
		
		m_timer.Update(fDeltaTime);
	}
	
	override public void OnExit ()
	{
		m_timer.stop = true;
		m_timer.Reset();
	}
}