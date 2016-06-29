using System;
using UnityEngine;
using fogs.proto.msg;

public class MatchStateBegin
	: MatchState
{	
	protected	IM.Number m_fCurTime = IM.Number.zero;
	private GameObject m_goBeginUI;
	
	public MatchStateBegin(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eBegin;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		UBasketball ball = m_match.mCurScene.mBall;
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
			player.DropBall(ball);

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_bMovedWithBall = false;

			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = true;
			
			player.m_toSkillInstance = null;
			player.m_StateMachine.SetState(PlayerState.State.eStand, true);

            //if( player.m_startPos == FightStatus.FS_MAIN && player.m_team.m_role == GameMatch.MatchRole.eOffense && ball != null )
            //    player.GrabBall(ball);
			
			player.m_stamina.ResetStamina();
		}

		m_match.InitBallHolder();
		m_match.ResetPlayerPos();

		//if (m_match.m_mainRole.m_inputDispatcher != null)
		//	m_match.m_mainRole.m_inputDispatcher.m_enable = false;

        if (m_match.TimmingOnStarting())
        {
            if (m_goBeginUI == null)
                m_goBeginUI = GameSystem.Instance.mClient.mUIManager.CreateUI("UIBeginCounter");
            if (m_goBeginUI == null)
            {
                Debug.Log("Error -- can not find ui resource " + "UIBeginCounter");
                return;
            }
            Animation anim = m_goBeginUI.GetComponentInChildren<Animation>();
            anim.Stop();
            anim.Play("counter");
        }

		m_match.m_cam.enabled = true;
		m_match.m_cam.m_PositionImmediately = true;
		m_match.m_cam.Positioning(true);

		if( m_match.m_camFollowPath != null )
			m_match.m_camFollowPath.enabled = false;

        if( ball != null && ball.m_owner != null && m_match.m_uiMatch != null && m_match.EnableCounter24())
		{
            m_match.m_uiMatch.ShowCounter(true, ball.m_owner.m_team == m_match.m_homeTeam);
            m_match.m_count24TimeStop = true;
			m_match.m_gameMatchCountStop = true;
		}

		if (m_match.EnableEnhanceAttr())
			m_match.EnhanceAttr();

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.BeginCounting);
	}

	virtual protected void _StateGuade(IM.Number fDeltaTime)
	{
		if (!m_match.TimmingOnStarting())
		{
			m_stateMachine.SetState(MatchState.State.ePlaying);
		}
		else
		{
			m_match.ConstrainMovementOnBegin(m_fCurTime);
            IM.Number prepareTime = GameSystem.Instance.mClient.mCurMatch.m_ruler.prepareTime;
			m_fCurTime += fDeltaTime;
            if (m_fCurTime > prepareTime)
            {
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.ReadyGo);
                m_stateMachine.SetState(MatchState.State.ePlaying);
            }
		}
	}

	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);
		_StateGuade(fDeltaTime);
	}
	
	override public void OnExit ()
	{
		m_fCurTime = IM.Number.zero;
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.GameBegin);
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Switch;
	}
}
