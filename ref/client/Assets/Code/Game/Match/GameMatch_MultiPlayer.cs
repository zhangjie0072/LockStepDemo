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
	private IM.Number SWITCH_CD_TIME = IM.Number.half;
	IM.Number[] switchCD = new IM.Number[2];
	private bool restoreCamSpeed;
	private bool ballHandlerRegistered = false;

	private Player m_ballOwner = null;
	private Player m_lastSwitchPlayer = null;
	private Player m_nextSwitchTarget = null;

	public GameMatch_MultiPlayer(Config config)
		:base(config)
	{
		m_stateMachine.m_matchStateListeners.Add(this);
	}

	virtual public void SwitchMainrole(Player target)
	{
        Player curMainRole = GetMainRole(target.m_roleInfo.acc_id);
        //不能自己切自己
		if( curMainRole == target)
			return;

        if (curMainRole != null)
        {
            //不能跨队切
            if (curMainRole.m_team.m_side != target.m_team.m_side)
                return;
            target.operMode = Player.OperMode.Input;
            if (curMainRole.m_inputDispatcher != null)
                target.m_inputDispatcher.TransmitUncontrolInfo(curMainRole.m_inputDispatcher);
            curMainRole.operMode = Player.OperMode.AI;
            Debug.LogFormat("SwitchMainRole, Prev: {0} {1} {2}",
                curMainRole.m_roleInfo.acc_id, curMainRole.m_team.m_side, curMainRole.m_id);
        }
        else
            target.operMode = Player.OperMode.Input;

        SetMainRole(target.m_roleInfo.acc_id, target);
        Debug.LogFormat("SwitchMainRole, Cur: {0} {1} {2}",
            target.m_roleInfo.acc_id, target.m_team.m_side, target.m_id);
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
            SwitchMainrole(offenserWithBall);
            SwitchMainrole(offenserWithBall.m_defenseTarget);
		}
	}

	public override void ResetPlayerPos()
	{
		bool r = IM.Random.value < IM.Number.half;

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

	public void OnSwitch(Team.Side side)
	{
        if (side == Team.Side.eNone)
            return;

		if (switchCD[(int)side - 1] > IM.Number.zero)
			return;
		else
			switchCD[(int)side - 1] = SWITCH_CD_TIME;
		
		//Debug.Log("On Switch");
		
		//choose nearest player closed to ball
		UBasketball ball = mCurScene.mBall;
		if( ball == null )
			return;

		if( m_nextSwitchTarget != null )
		{
			m_lastSwitchPlayer = mainRole;
			SwitchMainrole(m_nextSwitchTarget);
			m_nextSwitchTarget = mainRole.m_team.members.Find( (Player player)=>{ return player != m_lastSwitchPlayer && player != mainRole; } );
		}
		else
		{
			Player target = null;
			IM.Vector3 vBallPos; 
			if( ball.m_owner == null )
				vBallPos = ball.position;
			else
				vBallPos = ball.m_owner.position;
		
            Team team = side == Team.Side.eHome ? m_homeTeam : m_awayTeam;
            Player curMainRole = mainRole;
            IM.Number fMinDistance = IM.Number.max;
            foreach( Player player in team)
			{
                if( player == curMainRole )
					continue;
                IM.Number fDistance = GameUtils.HorizonalDistance(vBallPos, player.position);
				if( fMinDistance < fDistance )
					continue;
				fMinDistance = fDistance;
				target = player;
			}
			if( target == null )
				return;
			
			m_lastSwitchPlayer = mainRole;
			SwitchMainrole(target);
			m_nextSwitchTarget = mainRole.m_team.members.Find( (Player player)=>{ return player != m_lastSwitchPlayer && player != target; } );
		}
		m_cam.m_UseSwitchSpeed = true;
		restoreCamSpeed = true;
	}

	public override void GameUpdate (IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		UBasketball ball = mCurScene.mBall;
		if(ball != null && ball.m_owner != m_ballOwner )
		{
			m_lastSwitchPlayer = null;
			m_nextSwitchTarget = null;
			m_ballOwner = ball.m_owner;
		}

		_RefreshAOD();

		if(switchCD[0] > IM.Number.zero)
			switchCD[0] -= deltaTime;
		if(switchCD[1] > IM.Number.zero)
			switchCD[1] -= deltaTime;

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

		if( m_uiMatch != null )
		{
			if (ball.m_owner == null)
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(false);
			}
			else if (ball.m_owner.m_team == mainRole.m_team)
			{
				m_uiMatch.leftBall.SetActive(true);
				m_uiMatch.rightBall.SetActive(false);
			}
			else
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(true);
			}
		}

		if( m_stateMachine.m_curState.m_eState != MatchState.State.eOpening && m_stateMachine.m_curState.m_eState != MatchState.State.eOverTime 
		   && ball != null )
		{
			if( ball.m_owner != null )
			{
				Player owner = ball.m_owner;
				SwitchMainrole(owner);
			}
			else if( ball.m_ballState == BallState.eUseBall_Pass )
			{
				Player interceptor = ball.m_interceptor;
				if( interceptor == null )
				{
					Player owner = ball.m_catcher;
					SwitchMainrole(owner);
				}
				else
				{
                    SwitchMainrole(interceptor);
                    SwitchMainrole(interceptor.m_defenseTarget);
				}
			}
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
				Debug.Log("Resume defense target.");
				AssumeDefenseTarget();
			}
		}
	}

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
		if (newState.m_eState == MatchState.State.eBegin)
		{
			Debug.Log("Resume defense target.");
			AssumeDefenseTarget();
		}
	}
}
