using UnityEngine;
using fogs.proto.msg;

public class PlayerState_SwitchBall:  PlayerState
{
	public enum Type
	{
		LToR,
		RToL,
	}
	public Type	m_type;

	private string 	m_curAnim;
	private IM.Number	m_fRunSpeed;
	public PlayerState_SwitchBall (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eSwitchBall;

		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.Pass);
		m_validStateTransactions.Add(Command.CrossOver);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.Pass);
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_curAnim = m_type == Type.LToR ? "runSwitchHandLR" : "runSwitchHandRL";
		IM.Number fRunWithBallSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;
		m_player.animMgr.Play(m_curAnim, fRunWithBallSpeed, false);

		m_fRunSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;
		m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_TurningSpeed;

	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( !m_player.animMgr.IsPlaying(m_curAnim) )
		{
			if( m_type == Type.LToR )
				m_player.m_eHandWithBall = Player.HandWithBall.eRight;
			else
				m_player.m_eHandWithBall = Player.HandWithBall.eLeft;

			m_stateMachine.SetState(PlayerState.State.eRun);
			return;
		}
		else
			m_player.MoveTowards(m_player.moveDirection.normalized, m_turningSpeed, fDeltaTime, m_player.moveDirection.normalized * m_fRunSpeed);

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}
}
