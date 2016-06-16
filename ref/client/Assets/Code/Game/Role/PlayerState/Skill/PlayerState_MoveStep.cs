using UnityEngine;

public class PlayerState_MoveStep : PlayerState_Skill
{
	public PlayerState_MoveStep (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eMoveStep;
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone();
		m_stateMachine.SetState(PlayerState.State.eHold);
	}
}