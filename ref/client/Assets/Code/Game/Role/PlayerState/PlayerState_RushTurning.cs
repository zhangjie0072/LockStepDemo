using UnityEngine;
public class PlayerState_RushTurning : PlayerState
{
	private const string _rushWithBallLTurning = "rushWithBallLTurning";
	private const string _rushWithBallRTurning = "rushWithBallRTurning";
	private const string _rushTurning = "rushTurning";

	private IM.Number m_fSpeedRushWithoutBall;
	private IM.Number m_fSpeedRushWithBall;

	private IM.Vector3	_rotateTarget;
	private IM.Number m_turningSpeed;

	public PlayerState_RushTurning (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eRushTurning;

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
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.CutIn);
		m_validStateTransactions.Add(Command.BackToBack);

        m_fSpeedRushWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRushWithoutBall].mAttr.m_curSpeed;
		m_fSpeedRushWithBall 	= m_player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_curSpeed;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
			m_curAction = _rushWithBallLTurning;
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
			m_curAction = _rushWithBallRTurning;
		else
			m_curAction = _rushTurning;

        IM.Number speed = new IM.Number(1, 500);

        IM.Number length = m_player.animMgr.GetDuration(m_curAction);
		IM.Number clipLength = length / speed;

		_rotateTarget 	= -m_player.forward;
        IM.Number angle = IM.Vector3.Angle(m_player.forward, _rotateTarget);
        m_turningSpeed = IM.Math.Deg2Rad(angle) / clipLength;

		//m_player.m_stamina.ConsumeStamina(m_player.m_skillSystem.m_startRushStamina);

		m_player.animMgr.Play(m_curAction, speed, false);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		IM.Number step = m_turningSpeed * fDeltaTime;
		bool bClockWise = true;
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
			bClockWise = true;
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
			bClockWise = false;
		m_player.forward = GameUtils.RotateTowards(m_player.forward, _rotateTarget, step, bClockWise, m_player.position);

		//float fRunSpeed = m_player.m_bWithBall? m_fSpeedRushWithBall : m_fSpeedRushWithoutBall;
		//float fSpeedWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRushWithBall].mAttr.m_playSpeed;
		//m_player.Move(m_player.m_vVelocity.normalized, fDeltaTime, m_player.m_vVelocity.normalized * fRunSpeed);

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}

	protected override void _OnActionDone ()
	{
		if( m_player.m_eHandWithBall == Player.HandWithBall.eLeft )
			m_player.m_eHandWithBall = Player.HandWithBall.eRight;
		else if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
			m_player.m_eHandWithBall = Player.HandWithBall.eLeft;

		m_player.m_StateMachine.SetState(State.eRush);
	}
}