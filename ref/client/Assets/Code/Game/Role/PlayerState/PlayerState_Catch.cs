using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Catch: PlayerState
{
	private IM.Number m_fSpeedRunWithoutBall;
	private IM.Number m_fSpeedRunWithBall;

	private bool m_isRunning = false;

	public PlayerState_Catch (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = PlayerState.State.eCatch;

        m_fSpeedRunWithoutBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr.m_curSpeed;
        m_fSpeedRunWithBall = m_player.mMovements[(int)PlayerMovement.Type.eRunWithBall].mAttr.m_curSpeed;

		m_mapAnimType.Add(AnimType.N_TYPE_0, "standCatchBallLRChest");
		m_mapAnimType.Add(AnimType.N_TYPE_1, "runCatchBallLRChest");
		m_mapAnimType.Add(AnimType.N_TYPE_2, "runCatchBallRHead");
		m_mapAnimType.Add(AnimType.N_TYPE_3, "standCatchBallRHead");
		m_mapAnimType.Add(AnimType.N_TYPE_4, "runJumpCatchBallRHead");

		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.CrossOver);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Pass);
		m_validStateTransactions.Add(Command.BackToBack);
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		bool bRunning = false;
		if( lastState.m_eState == State.eRun || lastState.m_eState == State.eRush )
			bRunning = true;
		else if( lastState.m_eState == State.eRequireBall )
		{
			PlayerState_RequireBall requireBallState = lastState as PlayerState_RequireBall;
			if( requireBallState.m_animType == AnimType.N_TYPE_1 )
				bRunning = true;
		}

        m_animType = AnimType.N_TYPE_0;

        if( m_player.m_catchHelper.catchType == CatchHelper.CatchType.eChestCatch )
        {
            if( bRunning )
                m_animType = AnimType.N_TYPE_1;
            else
                m_animType = AnimType.N_TYPE_0;
        }
        else if( m_player.m_catchHelper.catchType == CatchHelper.CatchType.eRightCatch )
        {
            if( bRunning )
                m_animType = AnimType.N_TYPE_2;
            else
                m_animType = AnimType.N_TYPE_3;
        }
        else if( m_player.m_catchHelper.catchType == CatchHelper.CatchType.eMissCatch )
        {
            if( bRunning )
                m_animType = AnimType.N_TYPE_4;
            else
                m_animType = AnimType.N_TYPE_3;
        }
        if(!bRunning)
            m_player.FaceTo(m_ball.position);
        else
            m_isRunning = true;

		m_isRunning = bRunning;
		m_curAction = m_mapAnimType[m_animType];
        //apply rootmotion when m_animType == AnimType.N_TYPE_1 
        if (m_animType == AnimType.N_TYPE_1)
            m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();
        else
            m_player.animMgr.Play(m_curAction, false);

		m_player.m_moveType = MoveType.eMT_Catch;
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.PassBallSuc);
	}

    override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
	
		if( !m_player.animMgr.IsPlaying(m_curAction) )
		{
			m_stateMachine.SetState(PlayerState.State.eHold);
			return;
		}
		if(m_isRunning && m_animType != AnimType.N_TYPE_1 )
		{
            IM.Number fRunSpeed = m_player.m_bWithBall ? m_fSpeedRunWithBall : m_fSpeedRunWithoutBall;
			m_player.Move(fDeltaTime, m_player.forward * fRunSpeed);
		}

		if( m_player.m_toSkillInstance != null )
		{
			_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
			return;
		}
	}

	public void OnCatch(UBasketball ball)
	{
		if( ball == null )
			Logger.LogError("Catch: unable to find ball.");

		if( ball.m_castedSkill != null && ball.m_actor != null )
			ball.m_actor.mStatistics.SkillUsageSuccess(ball.m_castedSkill.skill.id, true);

		m_player.GrabBall(ball, true);
		m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eCatch);
		ball.OnCatch();

		Logger.Log("OnCatch");
	}

	public override void OnExit ()
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
			player.m_enablePickupDetector = true;

		m_player.m_bToCatch = false;
	}
}
