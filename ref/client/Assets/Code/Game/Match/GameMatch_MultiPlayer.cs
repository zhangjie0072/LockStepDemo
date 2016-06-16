using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using fogs.proto.msg;
using fogs.proto.config;

/// <summary>
/// 多人游戏基类
/// </summary>
public abstract class GameMatch_MultiPlayer : GameMatch, MatchStateMachine.Listener
{
    IM.Number SWITCH_CD_TIME = IM.Number.half;
	IM.Number switchCD;

	bool restoreCamSpeed;

	bool ballHandlerRegistered = false;

	public GameMatch_MultiPlayer(Config config)
		:base(config)
	{
		m_stateMachine.m_matchStateListeners.Add(this);
	}

	virtual public void SwitchMainrole(Player target)
	{
		//if( m_mainRole.m_defenseTarget != null )
		//	m_mainRole.m_defenseTarget.m_AOD.visible = false;
	}

	public override void InitBallHolder()
	{
		UBasketball ball = mCurScene.mBall;
		if(ball != null && ball.m_owner != null )
			ball.m_owner.DropBall(ball);
		if (ball != null)
			ball.Reset();
		
		if( m_offenseTeam.GetMemberCount() != 0 )
		{
			Player offenserWithBall = m_offenseTeam.GetInitialBallHolder();
			offenserWithBall.GrabBall(ball);
			offenserWithBall.m_StateMachine.SetState(PlayerState.State.eHold);
			if (offenserWithBall.m_team == m_mainRole.m_team)
				SwitchMainrole(offenserWithBall);
			else
				SwitchMainrole(offenserWithBall.m_defenseTarget);
		}
	}

	public override void ResetPlayerPos()
	{
		bool r = UnityEngine.Random.value < 0.5f;

		Team leftTeam = null;
		if( m_offenseTeam != null )
			leftTeam = m_offenseTeam;
		else
			leftTeam = m_homeTeam;

		foreach (Player member in leftTeam.members)
		{
            BeginPos beginPos = GameSystem.Instance.MatchPointsConfig.BeginPos;
			if (member.m_bWithBall)
			{
                member.position = beginPos.offenses_transform[0].position;
                if (member.m_defenseTarget != null)
                    member.m_defenseTarget.position = beginPos.defenses_transform[0].position;
			}
			else
			{
                member.position = beginPos.offenses_transform[r ? 1 : 2].position;
				if (member.m_defenseTarget != null)
                    member.m_defenseTarget.position = beginPos.defenses_transform[r ? 1 : 2].position;
				r = !r;
			}
		}
        
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            if (player.m_defenseTarget == null)
            {
                player.forward = IM.Vector3.forward;
                continue;
            }
            player.FaceTo(player.m_defenseTarget.position);
        }
	}

	public void OnSwitch()
	{
		if (switchCD > IM.Number.zero)
			return;
		else
			switchCD = SWITCH_CD_TIME;
		
		//Logger.Log("On Switch");
		
		//choose nearest player closed to ball
		UBasketball ball = mCurScene.mBall;
		if( ball == null )
			return;
		
		Player target = null;
		IM.Vector3 vBallPos; 
		if( ball.m_owner == null )
			vBallPos = ball.position;
		else
			vBallPos = ball.m_owner.position;
		
        IM.Number fMinDistance = IM.Number.max;
		foreach( Player player in m_mainRole.m_team )
		{
			if( player == m_mainRole )
				continue;
			IM.Number fDistance = GameUtils.HorizonalDistance(vBallPos, player.position);
			if( fMinDistance < fDistance )
				continue;
			fMinDistance = fDistance;
			target = player;
		}
		if( target == null )
			return;
		SwitchMainrole(target);
		m_cam.m_UseSwitchSpeed = true;
		restoreCamSpeed = true;
	}

	public override void Update (IM.Number deltaTime)
	{
		base.Update (deltaTime);

		_RefreshAOD();

		if(switchCD > IM.Number.zero)
			switchCD -= deltaTime;

		if (restoreCamSpeed && m_cam.m_Staying)
		{
			m_cam.m_UseSwitchSpeed = false;
			restoreCamSpeed = false;
		}

		if (mCurScene.mBall != null && !ballHandlerRegistered)
		{
			mCurScene.mBall.onRebound += OnRebound;
			mCurScene.mBall.onHitGround += OnHitGround;
			ballHandlerRegistered = true;
		}
	}

	void OnRebound(UBasketball ball)
	{
		ResumeDefenseTarget();
	}

	void OnHitGround(UBasketball ball)
	{
		ResumeDefenseTarget();
	}

	void ResumeDefenseTarget()
	{
		if (m_defenseTeam == null)
			return;
		Player p1 = null, p2 = null;
		foreach (Player member in m_defenseTeam.members)
		{
			if (member.m_defTargetSwitched)
			{
				if (p1 == null)
					p1 = member;
				else if (p2 == null)
					p2 = member;
			}
		}
		if (p1 != null)
		{
			IM.Number dist1 = GameUtils.HorizonalDistance(p1.position, p2.m_defenseTarget.position);
			IM.Number dist2 = GameUtils.HorizonalDistance(p2.position, p1.m_defenseTarget.position);
            if (dist1 < new IM.Number(4, 350) && dist2 < new IM.Number(4, 350))
			{
				Logger.Log("Resume defense target.");
				AssumeDefenseTarget();
			}
		}
	}

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
		if (newState.m_eState == MatchState.State.eBegin)
		{
			Logger.Log("Resume defense target.");
			AssumeDefenseTarget();
		}
	}
}
