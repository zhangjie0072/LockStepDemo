using UnityEngine;
using fogs.proto.msg;

public class AI_Assist_TraceBall : AIState
{
	public AI_Assist_TraceBall(AISystem system) : base(system)
	{
		m_eType = Type.eAssistTraceBall;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_StateMachine.assistAI.curCommand != Command.TraceBall ||
			m_ball.m_ballState != BallState.eLoseBall)
			m_player.m_StateMachine.assistAI.Disable();

		IM.Vector3 targetPos = m_ball.position;
		IM.Vector3 vecPlayerToTarget = targetPos - m_player.position;
		vecPlayerToTarget.y = IM.Number.zero;
		if (vecPlayerToTarget.magnitude < new IM.Number(0,100))
		{
			m_player.moveDirection = IM.Vector3.zero;
			m_player.m_moveType = MoveType.eMT_Stand;
		}
		else
		{
			int dir;
			IM.Vector3 vel;
            IM.Number angle = IM.Vector3.FromToAngle(IM.Vector3.forward, vecPlayerToTarget.normalized);
			GameUtils.AngleToDir(angle, out dir, out vel);
			m_player.m_dir = dir;
			m_player.m_moveType = MoveType.eMT_Rush;
		}
	}
}
