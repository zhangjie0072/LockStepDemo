using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Defense : PlayerState
{
    IM.Number m_fSpeedDefense = IM.Number.one;
	
	private int			m_lastMoveDir;
	
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
		
		if( !m_player.m_bSimulator )
		{
			m_animType = AnimType.N_TYPE_0;
			GameMsgSender.SendMove( m_player, MoveType.eMT_Defense, m_animType );
		}
		
		m_player.animMgr.CrossFade( m_mapAnimType[m_animType], false);

		m_player.m_moveType = MoveType.eMT_Defense;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( !m_player.m_bSimulator )
		{
			if( m_player.m_team.m_role != GameMatch.MatchRole.eDefense || m_ball.m_ballState == BallState.eLoseBall )
			{
				m_stateMachine.SetState(PlayerState.State.eStand);
				return;
			}

			if( m_player.m_toSkillInstance == null || m_player.m_bWithBall )
			{
				//Logger.Log("defense failed because of skill is null");
				m_stateMachine.SetState(PlayerState.State.eStand);
				return;
			}
			if( m_player.m_dir != m_lastMoveDir )
			{
				m_lastMoveDir = m_player.m_dir;
				GameMsgSender.SendMove(m_player, MoveType.eMT_Defense, m_animType);
			}
		}
		if( m_player.m_moveType == MoveType.eMT_Stand )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}
		
		_GetDefenseAction();
		
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
	
	public override void OnExit ()
	{
		base.OnExit ();
		if( !m_player.m_bSimulator )
			GameMsgSender.SendMove(m_player, MoveType.eMT_Stand, AnimType.N_TYPE_0);
	}
	
}