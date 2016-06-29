using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Defense : PlayerState
{
    IM.Number m_fSpeedDefense = IM.Number.one;
	
	private int			m_lastMoveDir;

	private IM.Number		m_defenseRadius = IM.Number.half;
	private IM.Number		m_defenseDist = IM.Number.half;

	public PlayerState_Defense (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eDefense;
		
		m_validStateTransactions.Add(Command.Steal);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.Rebound);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
		m_validStateTransactions.Add(Command.Defense);
		
		m_mapAnimType.Add(AnimType.N_TYPE_0, "defenseStand");
		m_mapAnimType.Add(AnimType.N_TYPE_1, "defenseForward");
		m_mapAnimType.Add(AnimType.N_TYPE_2, "defenseLeft");
		m_mapAnimType.Add(AnimType.N_TYPE_3, "defenseRight");
		m_mapAnimType.Add(AnimType.N_TYPE_4, "defenseBackward");

		m_animType = AnimType.N_TYPE_0;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		
		PlayerMovement pm 	= m_player.mMovements[(int)PlayerMovement.Type.eDefense];
        m_fSpeedDefense     = pm.mAttr.m_curSpeed;
        m_turningSpeed      = pm.mAttr.m_TurningSpeed;
		m_lastMoveDir 		= -1;
		
		_GetDefenseAction();
		
        m_animType = AnimType.N_TYPE_0;
		
		m_player.animMgr.CrossFade( m_mapAnimType[m_animType], false);

		m_player.m_moveType = MoveType.eMT_Defense;
	}

	bool _IsDefended(Player target)
	{
		IM.Number fDistance = GameUtils.HorizonalDistance(m_player.position, target.position);
		if(fDistance > m_defenseDist)
			return false;
		PlayerState.State curState = target.m_StateMachine.m_curState.m_eState;
		if(curState != State.eRun && curState != State.eRush)
			return false;
		IM.Vector3 lhs = m_player.position + m_player.right * m_defenseRadius;
		IM.Vector3 rhs = m_player.position - m_player.right * m_defenseRadius;

		IM.Vector3 playerToLhs = GameUtils.HorizonalNormalized(lhs, target.position);
		IM.Vector3 playerToRhs = GameUtils.HorizonalNormalized(rhs, target.position);

		if( IM.Vector3.Dot(target.forward, m_player.forward) > IM.Number.zero)
			return false;

		IM.Number fCrossRet1 = IM.Vector3.Cross(playerToLhs, target.moveDirection).y;
		IM.Number fCrossRet2 = IM.Vector3.Cross(playerToRhs, target.moveDirection).y;
		if( fCrossRet1 * fCrossRet2 > IM.Number.zero)
			return false;

		return true;
	}

	override public void Update (IM.Number fDeltaTime)
	{
        if( m_player.m_team.m_role != GameMatch.MatchRole.eDefense || m_ball.m_ballState == BallState.eLoseBall )
        {
            m_stateMachine.SetState(PlayerState.State.eStand);
            return;
        }

        if( m_player.m_toSkillInstance == null || m_player.m_bWithBall )
        {
				//Debug.Log("defense failed because of skill is null");
            m_stateMachine.SetState(PlayerState.State.eStand);
            return;
        }
        if( m_player.m_dir != m_lastMoveDir )
        {
            m_lastMoveDir = m_player.m_dir;
        }
		if( m_player.m_moveType == MoveType.eMT_Stand )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}
		
		_GetDefenseAction();
		Player target = m_ball.m_owner;
		if( target != null && _IsDefended(target) )
		{
			PlayerState_Knocked knocked = target.m_StateMachine.GetState(State.eKnocked) as PlayerState_Knocked;
			knocked.m_bToHoldBall = false;
			target.m_StateMachine.SetState(knocked);
		}

		IM.Vector3 rotToward;
		if( m_player.m_defenseTarget != null )
			rotToward = (m_player.m_defenseTarget.position - m_player.position).normalized;
		else
			rotToward = m_player.forward;
		m_player.MoveTowards(rotToward, m_turningSpeed ,fDeltaTime, m_player.moveDirection.normalized * m_fSpeedDefense);
		
		m_player.animMgr.CrossFade(m_mapAnimType[m_animType], false);

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}
	
	void _GetDefenseAction()
	{
		//if( (Command)m_player.m_toSkillInstance.skill.action_type == Command.Defense )
		{
			IM.Vector3 moveDir = m_player.moveDirection.normalized;
			if( moveDir == IM.Vector3.zero )
				m_animType = AnimType.N_TYPE_0;
			else
			{				
				IM.Number fAngle = IM.Vector3.Angle(m_player.forward, moveDir);
				if( fAngle > IM.Number.zero && fAngle <= new IM.Number(45) )
					m_animType = AnimType.N_TYPE_1;
                else if (fAngle > new IM.Number(45) && fAngle <= new IM.Number(90))
				{
					if( IM.Vector3.Cross(moveDir, m_player.forward).y > IM.Number.zero )
						m_animType = AnimType.N_TYPE_2;
					else
						m_animType = AnimType.N_TYPE_3;
				}
                else if (fAngle > new IM.Number(90) && fAngle < IM.Math.HALF_CIRCLE)
					m_animType = AnimType.N_TYPE_4;
			}
		}
	}
}