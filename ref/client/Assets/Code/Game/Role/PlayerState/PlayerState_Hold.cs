using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Hold:  PlayerState
{
	bool tipShowing = false;

	public PlayerState_Hold(PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eHold;

		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Pass);

		m_mapAnimType.Add(AnimType.B_TYPE_0, "standWithBallLR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		tipShowing = false;
		m_animType = AnimType.B_TYPE_0;
	
		if (m_player.m_bMovedWithBall)
			m_validStateTransactions.Remove(Command.BackToBack);
		else
			m_validStateTransactions.Add(Command.BackToBack);

		if( !m_player.m_bSimulator )
			GameMsgSender.SendHold(m_player);

        m_player.animMgr.CrossFade(m_mapAnimType[m_animType], false);
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( !m_player.m_bWithBall )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}

		if( m_player.m_bMovedWithBall )
		{
			//turning
			if( m_player.moveDirection != IM.Vector3.zero )
				m_player.forward = m_player.moveDirection.normalized;
			if (m_player == m_match.m_mainRole && !tipShowing)
			{
				m_match.ShowAnimTip("gameInterface_text_lock");
				tipShowing = true;
			}
		}
		else
		{
			if (m_player.m_dir != -1)
			{
				m_player.m_bMovedWithBall = true;
				m_stateMachine.SetState(PlayerState.State.eRun);
			}
		}

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}

	public override void OnExit()
	{
		base.OnExit();
		if (m_player == m_match.m_mainRole && tipShowing)
			m_match.HideAnimTip("gameInterface_text_lock");
	}
}