using UnityEngine;

public class AI_GrabPoint_TracePoint : AIState
{
	private GameMatch_GrabPoint match;
	private GameUtils.Timer timerTracePoint;
	private GameObject targetPoint;

	public AI_GrabPoint_TracePoint(AISystem owner)
		: base(owner)
	{
		m_eType = Type.eGrabPoint_TracePoint;
		match = m_match as GameMatch_GrabPoint;
		if (match != null)
		{
			timerTracePoint = new GameUtils.Timer(match.TRACE_POINT_DELAY, TracePoint);
			timerTracePoint.stop = true;
		}
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		//m_moveTarget = m_player.position;
		targetPoint = null;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if (match.m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			return;

		if (targetPoint != null && match.level != GameMatch.Level.Easy)
		{
            //m_bForceNotRush = CanArriveBeforePlayer(new IM.Vector3(targetPoint.transform.position));
            m_bForceNotRush = CanArriveBeforePlayer(match.curPointPosition);
		}
		else
			m_bForceNotRush = false;

		base.Update(fDeltaTime);

		if (targetPoint != match.curPoint)
		{
			m_moveTarget = m_player.position;
			targetPoint = null;
			timerTracePoint.stop = true;
		}

		timerTracePoint.Update(fDeltaTime);
	}

	protected override void OnTick()
	{
		if (match.curPoint == null)
			return;
		if (match.curPoint != null && match.m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
			if (targetPoint != match.curPoint)
			{
				targetPoint = match.curPoint;
				timerTracePoint.SetTimer(match.TRACE_POINT_DELAY);
				timerTracePoint.stop = false;   
			}
		}
		else
			m_player.moveDirection = IM.Vector3.zero;

		if (m_player.m_bWithBall)
		{
			if (match.level == GameMatch.Level.Easy)
			{
				m_system.SetTransaction(AIState.Type.eGrabPoint_Shoot);
			}
			else if (!CanArriveBeforePlayer(match.curPointPosition))
			{
				if (match.level != GameMatch.Level.Hard)
				{
					m_system.SetTransaction(AIState.Type.eGrabPoint_Shoot);
				}
				else
				{
					AIUtils.PositionAndShoot(AIState.Type.eGrabPoint_Positioning, AIState.Type.eGrabPoint_Shoot, match, m_player);
				}
			}
		}
	}

	private void TracePoint()
	{
		timerTracePoint.stop = true;
		if (match.curPoint == null)
			return;
		m_moveTarget = match.curPointPosition;
	}
}
