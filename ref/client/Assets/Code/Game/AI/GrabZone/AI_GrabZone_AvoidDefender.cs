using UnityEngine;
using System.Collections.Generic;

public class AI_GrabZone_AvoidDefender : AIState
{
	public AI_GrabZone_AvoidDefender(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eGrabZone_AvoidDefender;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_moveTarget = SeekMoveTarget();
	}
	
	public override void ArriveAtMoveTarget()
	{
		if (m_player.m_bWithBall)
			m_system.SetTransaction(AIState.Type.eGrabZone_Positioning);
		else
			m_system.SetTransaction(AIState.Type.eGrabZone_TraceBall);
	}

	private IM.Vector3 SeekMoveTarget()
	{
		IM.Vector3 moveTarget;
		Player defender = m_match.m_mainRole;
		IM.Vector3 dirPlayer2Target = GameUtils.HorizonalNormalized(m_moveTarget, m_player.position);
		IM.Vector3 dirPlayer2Defender = GameUtils.HorizonalNormalized(defender.position,m_player.position);
		IM.Vector3 dirPlayer2TargetRight = IM.Quaternion.AngleAxis(new IM.Number(90), IM.Vector3.up) * dirPlayer2Target;
        //IM.Number angle = Quaternion.FromToRotation(dirPlayer2Defender, dirPlayer2TargetRight).eulerAngles.y;
        IM.Number angle = IM.Vector3.FromToAngle(dirPlayer2Defender, dirPlayer2TargetRight);
		IM.Number rotAngle = angle > new IM.Number(90) ? new IM.Number(90) : -new IM.Number(90);
		moveTarget = IM.Quaternion.AngleAxis(rotAngle, IM.Vector3.up) * dirPlayer2Target + m_player.position;
		IM.Vector3 target = moveTarget;
        IM.Vector3 itarget = target;
		m_match.mCurScene.mGround.BoundInZone(ref itarget);
		if (target != moveTarget)
			moveTarget = IM.Quaternion.AngleAxis(-rotAngle, IM.Vector3.up) * dirPlayer2Target + m_player.position;
		return moveTarget;
	}
}