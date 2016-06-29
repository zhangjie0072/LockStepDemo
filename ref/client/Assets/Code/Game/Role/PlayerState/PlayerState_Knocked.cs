using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Knocked : PlayerState
{
	public 	bool	m_bKnockedRecover = false;
	public	bool	m_bToHoldBall = false;

	protected float	m_rateHoldBall = 0.3f;

	public PlayerState_Knocked (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eKnocked;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "knockedWithBallL");//but hold
		m_mapAnimType.Add(AnimType.B_TYPE_1, "knockedWithBallL");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "knockedWithBallR");//but hold
		m_mapAnimType.Add(AnimType.B_TYPE_3, "knockedWithBallR");
		m_mapAnimType.Add(AnimType.N_TYPE_0, "knockedWithoutBall");

		m_animType = AnimType.N_TYPE_0;
	}

	override public void OnEnter ( PlayerState lastState )
	{
        if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
				m_animType = m_bToHoldBall ? AnimType.B_TYPE_0 : AnimType.B_TYPE_1;
        else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
				m_animType = m_bToHoldBall ? AnimType.B_TYPE_2 : AnimType.B_TYPE_3;
        else
            m_animType = AnimType.N_TYPE_0;

		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, false);

		m_bKnockedRecover = false;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( !m_player.animMgr.IsPlaying(m_curAction) )
		{
			if( m_animType == AnimType.B_TYPE_0 || m_animType == AnimType.B_TYPE_2 )
				m_stateMachine.SetState(State.eHold);
			else 
				m_stateMachine.SetState(State.eStand);
		}
		else
		{
			if( !m_bKnockedRecover )
				m_player.Move(fDeltaTime, -m_player.forward * IM.Number.two);
		}
	}
	public override void OnExit ()
	{
		base.OnExit ();
		m_bToHoldBall = false;
	}

}


