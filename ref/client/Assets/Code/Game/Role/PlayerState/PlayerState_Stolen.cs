using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Stolen:  PlayerState
{
	public bool		m_bLostBall = true;

	public PlayerState_Stolen (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eStolen;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "stolen");
		m_animType = AnimType.B_TYPE_0;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;
		m_curAction = m_mapAnimType[m_animType];
		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eStolen);
		m_player.animMgr.CrossFade(m_curAction, false);
	}
	protected override void _OnActionDone ()
	{
		base._OnActionDone();
		m_stateMachine.SetState(State.eStand);
	}

	public void OnLostBall()
	{		
		if( m_bLostBall )
			m_player.OnLostBall();
	}

	public override void OnExit ()
	{
		m_player.m_lostBallContext.vInitPos = IM.Vector3.zero;
		m_player.m_lostBallContext.vInitVel = IM.Vector3.zero;

		m_bLostBall = true;
	}
}