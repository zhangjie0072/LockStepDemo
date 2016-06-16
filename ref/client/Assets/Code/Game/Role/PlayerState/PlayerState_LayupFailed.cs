using UnityEngine;
using fogs.proto.msg;

public class PlayerState_LayupFailed: PlayerState
{
	public PlayerState_LayupFailed (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = PlayerState.State.eLayupFailed;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_eState = State.eCrossed;

		m_player.m_bMovedWithBall = true;

		m_mapAnimType[AnimType.B_TYPE_0] = "knockedWithBallLayupL";
		m_mapAnimType[AnimType.B_TYPE_1] = "knockedWithBallLayupR";

		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
			m_animType = AnimType.B_TYPE_0;
		else
			m_animType = AnimType.B_TYPE_1;

		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, false);

		if( !m_player.m_bSimulator )
		{
            m_player.FaceTo(m_basket.m_vShootTarget);
			GameMsgSender.SendLayupFailed( m_player, m_animType );
		}
	}

	protected override void _OnActionDone ()
	{
		base._OnActionDone ();
		m_player.m_StateMachine.SetState(State.eHold);
	}
}
