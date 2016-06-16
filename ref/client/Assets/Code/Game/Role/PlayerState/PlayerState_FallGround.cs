using UnityEngine;
using fogs.proto.msg;

public class PlayerState_FallGround:  PlayerState
{
	public PlayerState_FallGround (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eFallGround;
		m_mapAnimType.Add(AnimType.N_TYPE_0, "falling");
		m_mapAnimType.Add(AnimType.N_TYPE_1, "fallingBack");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_animType = AnimType.N_TYPE_0;
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.FallGround);
	}
	
	protected override void _OnActionDone()
	{
		base._OnActionDone();

		if (m_player.m_moveType == MoveType.eMT_Stand)
			m_stateMachine.SetState(PlayerState.State.eStand);
		else if (m_player.m_moveType == MoveType.eMT_Run)
			m_stateMachine.SetState(PlayerState.State.eRun);
		else if (m_player.m_moveType == MoveType.eMT_Rush)
			m_stateMachine.SetState(PlayerState.State.eRush);
		else
			m_stateMachine.SetState(PlayerState.State.eStand);
	}
}
