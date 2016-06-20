using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Run : PlayerState
{
	public enum Type
	{
		eFree,
		eBackward,
	}

	public 	Type	m_type;
	private bool	m_bOrigWithBall = false;

	private IM.Number 	m_fSpeedRunWithoutBall;
	private IM.Number 	m_fSpeedRunWithBall;

    private IM.Vector3 m_lastMoveDir;

	enum SwitchType
	{
        None,
		LToR,
		RToL,
	}
	private IM.Number	m_fRunSpeed;
	private string 	m_switchBallAnim;
	private bool	m_bSwitchBall = false;
	private SwitchType m_switchType;

	public PlayerState_Run (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eRun;
		m_type = Type.eFree;

		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.CrossOver);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Pass);
		m_validStateTransactions.Add(Command.Defense);
		m_validStateTransactions.Add(Command.Rebound);
		m_validStateTransactions.Add(Command.RequireBall);
		m_validStateTransactions.Add(Command.Steal);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.PickAndRoll);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
		m_validStateTransactions.Add(Command.CutIn);
		m_validStateTransactions.Add(Command.BackToBack);
		//m_validStateTransactions.Add(Command.Interception);
		
		m_mapAnimType.Add(AnimType.N_TYPE_1, "run");
		m_mapAnimType.Add(AnimType.N_TYPE_2, "backward");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "runWithBallL");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "runWithBallR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_fSpeedRunWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
        m_fSpeedRunWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed ;

		if( !IM.Number.Approximately(m_player.position.y, IM.Number.zero) )
			m_player.position = new IM.Vector3(m_player.position.x, IM.Number.zero, m_player.position.z);
	
		if( lastState.m_eState != State.eRun )
			m_type = _GetRunAction();

		m_animType = AnimType.N_TYPE_1;
        if (m_player.m_eHandWithBall == Player.HandWithBall.eLeft)
        {
            m_animType = AnimType.B_TYPE_1;
            m_player.m_bMovedWithBall = true;
            m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_TurningSpeed;
        }
        else if (m_player.m_eHandWithBall == Player.HandWithBall.eRight)
        {
            m_animType = AnimType.B_TYPE_2;
            m_player.m_bMovedWithBall = true;
            m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_TurningSpeed;
        }
        else if (m_type == Type.eBackward)
        {
            m_animType = AnimType.N_TYPE_2;
            m_player.m_bMovedWithBall = false;
            m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_TurningSpeed;
        }
        else
            m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_TurningSpeed;


		IM.Number fSpeedWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_playSpeed;
		IM.Number fSpeedWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_playSpeed;

		m_curAction = m_mapAnimType[m_animType];
        //如果上一个动作是换手，则不与其混合。否则动画会有卡顿效果
        if (lastState.m_eState == State.eRun && m_switchType != SwitchType.None)
            m_player.animMgr.Play(m_curAction, m_player.m_bWithBall ? fSpeedWithBall : fSpeedWithoutBall, false);
        else
            m_player.animMgr.CrossFade(m_curAction, m_player.m_bWithBall ? fSpeedWithBall : fSpeedWithoutBall, false);

		m_bOrigWithBall = m_player.m_bWithBall;
		m_player.m_bOnGround = true;

        m_switchType = SwitchType.None;
		m_bSwitchBall = false;

		m_player.m_moveType = MoveType.eMT_Run;
		if(lastState.m_eState != State.eRun )
		{
			m_lastMoveDir = m_player.moveDirection;
		}
	}

	Type _GetRunAction()
	{
		if( m_player.m_team.m_role == GameMatch.MatchRole.eDefense && m_player.m_defenseTarget != null && m_ball.m_owner != null )
		{
			Player defenseTarget = m_player.m_defenseTarget;
			IM.Vector3 dirDefTargetToPlayer = GameUtils.HorizonalNormalized(defenseTarget.position, m_player.position);
			//debug
			{
				IM.Vector3 vPlayer = new IM.Vector3(m_player.position.x, m_player.position.y, m_player.position.z);
				vPlayer.y = IM.Number.one;
				Debug.DrawLine( (Vector3)vPlayer, (Vector3)vPlayer + (Vector3)dirDefTargetToPlayer * 2f, Color.red);
				Debug.DrawLine( (Vector3)vPlayer, (Vector3)vPlayer + (Vector3)m_player.moveDirection * 2f, Color.blue);
				Debug.DrawLine( (Vector3)vPlayer, (Vector3)vPlayer + (Vector3)m_player.forward * 2, Color.green);
			}
			IM.Number fAngleInputToPlayerDef = IM.Vector3.Angle(dirDefTargetToPlayer, m_player.moveDirection.normalized);
			
			Type type = Type.eFree;
			if( fAngleInputToPlayerDef > new IM.Number(135) )
				type = Type.eBackward;
			
			return type;
		}
		else
			return Type.eFree;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_toSkillInstance != null )
		{
			_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
			return;
		}

		if( IM.Number.Approximately( m_player.moveDirection.magnitude, IM.Number.zero) )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}

		m_fRunSpeed = m_player.m_bWithBall? m_fSpeedRunWithBall : m_fSpeedRunWithoutBall;
		//if( m_type == Type.eBackward )
		//	fRunSpeed *= 0.7f;

		if( m_player.m_moveType == MoveType.eMT_Rush )
		{
			if( m_player.m_stamina.m_curStamina >= m_player.m_skillSystem.m_startRushStamina )
			{
				m_stateMachine.SetState(PlayerState.State.eRush);
				return;
			}
			else if( m_match.m_mainRole == m_player || m_player.m_bIsAI )
                m_match.ShowTips((Vector3)m_player.position + Vector3.up, CommonFunction.GetConstString("MATCH_TIPS_NOT_ENOUGH_STAMINA"), GlobalConst.MATCH_TIP_COLOR_RED);
		}
		else if( m_player.m_moveType == MoveType.eMT_Defense )
		{
			m_stateMachine.SetState(PlayerState.State.eDefense);
			return;
		}

		Type newType = _GetRunAction();
		if( newType != m_type )
		{
			//Logger.Log("change run type: " + m_type + " to: " + newType );
			m_type = newType;
			m_player.m_StateMachine.SetState(this, true);
		}

		if( m_player.m_bWithBall !=  m_bOrigWithBall )
		{
			m_stateMachine.SetState(PlayerState.State.eRun, true);
			return;
		}

        //*
		//switch ball
		if(m_player.m_bWithBall && m_basket != null)
		{
			if( !m_bSwitchBall )
			{
                IM.Vector3 dirBasketToPlayer = (m_player.position - m_basket.m_vShootTarget).normalized;
				PlayerState_SwitchBall sbState = m_stateMachine.GetState(State.eSwitchBall) as PlayerState_SwitchBall;
				if( IM.Vector3.Cross(dirBasketToPlayer, m_player.forward).y < IM.Number.zero && m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
				{
					_SwitchBall(SwitchType.LToR);
					return;
				}
                else if (IM.Vector3.Cross(dirBasketToPlayer, m_player.forward).y > IM.Number.zero && m_player.m_eHandWithBall == Player.HandWithBall.eRight)
				{
					_SwitchBall(SwitchType.RToL);
					return;
				}
			}
			else if( !m_player.animMgr.IsPlaying(m_switchBallAnim) )
			{
				if( m_switchType == SwitchType.LToR )
					m_player.m_eHandWithBall = Player.HandWithBall.eRight;
				else
					m_player.m_eHandWithBall = Player.HandWithBall.eLeft;

				m_bSwitchBall = false;

				m_stateMachine.SetState(State.eRun, true);
				return;
			}
			//else
			//	m_player.MoveTowards(m_player.m_vVelocity.normalized, m_turningSpeed, fDeltaTime, m_player.m_vVelocity.normalized * m_fRunSpeed);
		}
        //*/

		IM.Vector3 dirMove = m_player.moveDirection.normalized;
		m_player.m_moveHelper.movingSpeed = m_fRunSpeed;

		if( m_type == Type.eFree )
			m_player.MoveTowards(dirMove, m_turningSpeed ,fDeltaTime, dirMove * m_fRunSpeed);
		else
			m_player.MoveTowards(new IM.Vector3(-dirMove.x, IM.Number.zero, -dirMove.z), m_turningSpeed, fDeltaTime, dirMove * m_fRunSpeed);

        if( IM.Vector3.Angle(m_player.moveDirection, m_lastMoveDir) > GlobalConst.ROTATE_ANGLE_SEC - IM.Number.one )
        {
            m_lastMoveDir = m_player.moveDirection;
        }
	}

	void _SwitchBall(SwitchType type)
	{
		m_switchBallAnim = type == SwitchType.LToR ? "runSwitchHandLR" : "runSwitchHandRL";
		IM.Number playSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;

        m_fRunSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;
		m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_TurningSpeed;
		
        m_player.animMgr.Play(m_switchBallAnim, playSpeed, false);

		m_switchType = type;
		m_bSwitchBall = true;
	}

}