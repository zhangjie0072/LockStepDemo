using UnityEngine;
using fogs.proto.msg;

public class AI_Assist_Defense : AIState
{
	public AI_Assist_Defense(AISystem system) : base(system)
	{
		m_eType = Type.eAssistDefense;
	}

    public override void Update(IM.Number fDeltaTime)
	{
		if (m_player.m_StateMachine.assistAI.curCommand != Command.Defense)
			m_player.m_StateMachine.assistAI.Disable();

		Player target = m_player.m_defenseTarget;
		IM.Vector3 dirTargetToBasket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, target.position);
		IM.Number fDistDefTargetToBasket = GameUtils.HorizonalDistance(target.position, m_match.mCurScene.mBasket.m_vShootTarget);
		IM.Number fDistDefToBasket = GameUtils.HorizonalDistance(m_player.position, m_match.mCurScene.mBasket.m_vShootTarget);

        IM.Vector3 targetPos = target.position + dirTargetToBasket * IM.Math.Max(fDistDefTargetToBasket / new IM.Number(3, 500), IM.Number.half);
		IM.Vector3 vecPlayerToTarget = targetPos - m_player.position;
        IM.Number distToTargetPos = vecPlayerToTarget.magnitude;
		if (distToTargetPos < new IM.Number(0, 100))
		{
			m_player.m_dir = -1;
			m_player.m_moveHelper.StopMove(MoveType.eMT_Defense);
		}
		else
		{
			int dir;
			IM.Vector3 vel;
            //IM.Number angle = IM.Quaternion.FromToRotation(IM.Vector3.forward, vecPlayerToTarget.normalized).eulerAngles.y;
            IM.Number angle = IM.Vector3.FromToAngle(IM.Vector3.forward, vecPlayerToTarget.normalized);
			GameUtils.AngleToDir(angle, out dir, out vel);
			m_player.m_dir = dir;
			m_player.m_moveType = distToTargetPos < new IM.Number(4) ? MoveType.eMT_Run : MoveType.eMT_Rush;
		}

		if (target.m_AOD.GetStateByPos(m_player.position) == AOD.Zone.eInvalid)
		{
			m_player.m_toSkillInstance = null;
		}
        else if (m_player.m_inputDispatcher != null
            && m_player.m_inputDispatcher._autoDefTakeOver.InTakeOver 
            && AIUtils.IsAttacking(target) 
            && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eDisturb
            && !(m_player.m_StateMachine.m_curState is PlayerState_Skill)
            && target.m_blockable.blockable)
        {
            m_player.m_StateMachine.SetState(PlayerState.State.eDisturb);
        }
	}
}
