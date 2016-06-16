using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Stand:  PlayerState
{
	private bool	m_bOrigWithBall = false;

	public PlayerState_Stand (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eStand;

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
		m_validStateTransactions.Add(Command.PickAndRoll);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
		m_validStateTransactions.Add(Command.CutIn);
		m_validStateTransactions.Add(Command.BackToBack);
		//m_validStateTransactions.Add(Command.Interception);
		
		
		m_mapAnimType.Add(AnimType.N_TYPE_1, "stand");
		m_mapAnimType.Add(AnimType.N_TYPE_2, "defense");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "standWithBallL");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "standWithBallR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_curAction = DetermineAction();
        m_player.animMgr.CrossFade(m_curAction, false);

		m_bOrigWithBall = m_player.m_bWithBall;
		m_player.m_bOnGround = true;
		m_player.m_dir = -1;
		m_player.m_curInputDir = -1;

		m_player.m_moveType = MoveType.eMT_Stand;
		
		if( !m_player.m_bSimulator )
			GameMsgSender.SendStand(m_player);
	}

	protected virtual string DetermineAction()
	{
		m_animType = AnimType.N_TYPE_1;
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft  )
			m_animType = AnimType.B_TYPE_1;
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight  )
			m_animType = AnimType.B_TYPE_2;
		else if( m_player.m_team.m_role == GameMatch.MatchRole.eDefense && 
			m_ball != null && m_ball.m_owner != null && m_player.m_defenseTarget != null)
			m_animType = AnimType.N_TYPE_2;

		return m_mapAnimType[m_animType];
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if (m_player.m_toSkillInstance != null)
		{
			_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
			return;
		}

		if( IM.Number.Approximately(m_player.position.y, IM.Number.zero) )
			m_player.position = new IM.Vector3(m_player.position.x, IM.Number.zero, m_player.position.z);

		if( m_player.moveDirection != IM.Vector3.zero && m_player.m_toSkillInstance == null )
		//if( m_player.m_dir != EDirection.eNone && m_player.m_toSkillInstance == null )
		{
			if (m_player.m_moveType == MoveType.eMT_Run)
			{
				m_stateMachine.SetState(State.eRun);
				return;
			}
			else if (m_player.m_moveType == MoveType.eMT_Rush)
			{
				m_stateMachine.SetState(State.eRush);
				return;
			}
		}
		if( m_player.m_moveType == MoveType.eMT_Defense )
		{
			m_stateMachine.SetState(PlayerState.State.eDefense);
			return;
		}

		if (m_player.m_bWithBall != m_bOrigWithBall)
		{
			m_stateMachine.SetState(PlayerState.State.eStand, true);
			return;
		}

		if( m_animType == AnimType.N_TYPE_2 )
		{
			m_player.FaceTo( m_player.m_defenseTarget.position );

			if (m_ball.m_owner == null)
			{
				m_stateMachine.SetState(PlayerState.State.eStand, true);
				return;
			}
		}
	}
}
