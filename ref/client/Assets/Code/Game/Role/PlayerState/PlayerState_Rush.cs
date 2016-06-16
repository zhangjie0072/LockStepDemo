using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Rush : PlayerState
{
	private IM.Number m_fSpeedRushWithoutBall;
    private IM.Number m_fSpeedRushWithBall;

	private int	 m_lastMoveDir;
    public IM.Number m_fRushSpeed;

	public PlayerState_Rush (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eRush;

		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.CrossOver);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Pass);
		m_validStateTransactions.Add(Command.Defense);
		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.Rebound);
		m_validStateTransactions.Add(Command.RequireBall);
		m_validStateTransactions.Add(Command.Steal);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.PickAndRoll);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
		m_validStateTransactions.Add(Command.CutIn);
		m_validStateTransactions.Add(Command.BackToBack);
		//m_validStateTransactions.Add(Command.Interception);
		
		
		m_mapAnimType.Add(AnimType.N_TYPE_1, "rush");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "rushWithBallL");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "rushWithBallR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_fSpeedRushWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRushWithoutBall].mAttr.m_curSpeed;
		m_fSpeedRushWithBall 	= m_player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_curSpeed;

		if( m_player.m_bWithBall )
			m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_TurningSpeed;
		else
			m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRushWithoutBall].mAttr.m_TurningSpeed;

		if( !IM.Number.Approximately(m_player.position.y, IM.Number.zero) )
			m_player.position = new IM.Vector3(m_player.position.x,IM.Number.zero, m_player.position.z);
	
		m_animType = AnimType.N_TYPE_1;
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft  )
			m_animType = AnimType.B_TYPE_1;
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight  )
			m_animType = AnimType.B_TYPE_2;

		IM.Number fSpeedWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_playSpeed;
		IM.Number fSpeedWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRushWithoutBall].mAttr.m_playSpeed;
		m_curAction = m_mapAnimType[m_animType];
        m_player.animMgr.CrossFade(m_curAction, m_player.m_bWithBall ? fSpeedWithBall : fSpeedWithoutBall, false);
		m_player.m_bMovedWithBall = true;
		m_player.m_bOnGround = true;

		
		//if( lastState.m_eState != State.eRushTurning )
			m_player.m_stamina.ConsumeStamina(m_player.m_skillSystem.m_startRushStamina);

		m_lastMoveDir = 0;
		m_player.m_moveType = MoveType.eMT_Rush;
		if( !m_player.m_bSimulator )
			GameMsgSender.SendMove(m_player, MoveType.eMT_Rush, m_animType);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_toSkillInstance != null )
		{
			_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
			return;
		}

		IM.Vector3 dirVelocity = m_player.moveDirection.normalized;
        if (!m_player.m_stamina.ConsumeStamina((m_player.m_skillSystem.m_rushStamina * fDeltaTime)))
		{
			if (m_match.m_mainRole == m_player)
				m_match.ShowTips((Vector3)m_player.position + Vector3.up, CommonFunction.GetConstString("MATCH_TIPS_NOT_ENOUGH_STAMINA"), GlobalConst.MATCH_TIP_COLOR_RED);
			m_stateMachine.SetState(PlayerState.State.eRun);
			return;
		}
		if( m_player.moveDirection == IM.Vector3.zero )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}
		else if( m_player.m_moveType == MoveType.eMT_Run  )
		{
			m_stateMachine.SetState(PlayerState.State.eRun);
			return;
		}
		else if( m_player.m_moveType == MoveType.eMT_Defense )
		{
			m_stateMachine.SetState(PlayerState.State.eDefense);
			return;
		}

		/*
		if( Vector3.Angle(m_player.forward, dirVelocity) > 145.0f )
		{
			m_stateMachine.SetState(State.eRushTurning);
			return;
		}
		*/

		m_fRushSpeed = m_player.m_bWithBall? m_fSpeedRushWithBall : m_fSpeedRushWithoutBall;
		m_player.MoveTowards(dirVelocity, m_turningSpeed, fDeltaTime, dirVelocity * m_fRushSpeed);
        m_player.m_moveHelper.movingSpeed = m_fRushSpeed;
		
		if( !m_player.m_bSimulator && m_player.m_dir != m_lastMoveDir )
		{
			m_lastMoveDir = m_player.m_dir;
			GameMsgSender.SendMove(m_player, MoveType.eMT_Rush, m_animType);
		}
	}
}