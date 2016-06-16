using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Pickup : PlayerState
{
	public bool	m_bSuccess = false;
	enum Type
	{
		eStand,
		eStoop,
		eRun
	}
	Type m_type = Type.eStoop;

	public	UBasketball	m_ballToPickup;

	private IM.Number 	m_fSpeedRunWithoutBall;
	private IM.Number 	m_fSpeedRunWithBall;

	public PlayerState_Pickup (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.ePickup;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "pickup");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "pickWithFoot");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "pickupRun");
		m_mapAnimType.Add(AnimType.B_TYPE_3, "pickupStand");

		m_animType = AnimType.B_TYPE_0;
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		UBasketball ball = m_ballToPickup;
		if( ball == null )
			ball = m_match.mCurScene.mBall;

        m_fSpeedRunWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
        m_fSpeedRunWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;
        m_turningSpeed = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_TurningSpeed;

		IM.Number fBallHeight = ball.position.y;

		if( !m_player.m_bSimulator )
		{
			IM.Number fProb = new IM.Number(0,800);
			if( m_player.m_position == fogs.proto.msg.PositionType.PT_C )
				fProb = new IM.Number(0,300);
			else if( m_player.m_position == fogs.proto.msg.PositionType.PT_PF || m_player.m_position == fogs.proto.msg.PositionType.PT_SF )
				fProb = IM.Number.half;

            fProb = IM.Number.one;
			m_animType = AnimType.B_TYPE_0;
			if( fBallHeight < m_player.pelvisPos.y)
			{
				if( IM.Random.value < fProb )
					m_animType = AnimType.B_TYPE_1;
			}
			else
			{
				if( lastState.m_eState == PlayerState.State.eRun || lastState.m_eState == PlayerState.State.eRush )
					m_animType = AnimType.B_TYPE_2;
				else if( lastState.m_eState == PlayerState.State.eStand )
					m_animType = AnimType.B_TYPE_3;
			}
			m_bSuccess = true;
		}

		if( m_animType == AnimType.B_TYPE_1 )
		{
			m_type = Type.eStoop;
			m_player.moveDirection = IM.Vector3.zero;
		}
		else if( m_animType == AnimType.B_TYPE_2 )
			m_type = Type.eRun;
		else if( m_animType == AnimType.B_TYPE_3 || m_animType == AnimType.B_TYPE_0 )
		{
			m_player.moveDirection = IM.Vector3.zero;
			m_type = Type.eStand;
		}

		m_curAction = m_mapAnimType[m_animType];

		if( !m_player.m_bSimulator )
			GameMsgSender.SendPickBall(m_player, ball.m_id, m_animType, ball.m_ballState);

		m_speed = IM.Vector3.zero;
		IM.RootMotion rootMotion = m_player.animMgr.Play(m_curAction, true).rootMotion;
        rootMotion.Reset();
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		if( m_type == Type.eRun )
		{
			IM.Number fRunSpeed = m_player.m_bWithBall? m_fSpeedRunWithBall : m_fSpeedRunWithoutBall;
			m_player.MoveTowards(m_player.moveDirection.normalized, m_turningSpeed, fDeltaTime, m_player.forward * fRunSpeed);
		}
	}

	override protected void _OnActionDone()
	{
		if( m_type == Type.eRun )
			m_stateMachine.SetState(PlayerState.State.eRun);
		else 
		{
			Logger.Log("pickup m_bSuccess: " + m_bSuccess);

			if( !m_bSuccess )
				m_stateMachine.SetState(PlayerState.State.eStand);
			else
				m_stateMachine.SetState(PlayerState.State.eHold);
		}
	}

	public override void OnExit ()
	{
        Logger.Log(Time.frameCount + " Exit PlayerState_Pickup.");
		base.OnExit ();
		m_animType = fogs.proto.msg.AnimType.B_TYPE_0;
		m_ballToPickup = null;
		m_bSuccess = false;
	}

}
