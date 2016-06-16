using UnityEngine;
using System;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMatch_QualifyingNewerAI
	:GameMatch_MultiPlayer
{

	private bool initDone = false;

    public GameMatch_QualifyingNewerAI(Config config)
        : base(config)
    {
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
        _nameIndex = 1; 
    }

    public List<string> _names = new List<string>();
    public int _nameIndex = 1; 



    
    override public void OnSceneComplete()
    {
        base.OnSceneComplete();
		mCurScene.CreateBall();

        if (m_config == null)
        {
            Logger.LogError("Match config file loading failed.");
            return;
        }

		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		for( int idx = 0; idx != pm.m_Players.Count; idx++ )
		{
			Player player = pm.m_Players[idx];

			player.m_aiMgr = new AISystem_Basic(this, player, AIState.Type.eInit, player.m_config.AIID);
			player.m_aiMgr.m_enable = true;
			
			player.m_catchHelper = new CatchHelper(player);
			player.m_catchHelper.ExtractBallLocomotion();
			player.m_StateMachine.SetState(PlayerState.State.eStand, true);
			player.m_InfoVisualizer.CreateStrengthBar();
			player.m_InfoVisualizer.ShowStaminaBar(false);
		}

		m_mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		m_mainRole.m_aiMgr.m_enable = false;
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		m_mainRole.m_inputDispatcher = new InputDispatcher(this, m_mainRole);

		if( !Debugger.Instance.m_bEnableDefenderAction )
		{
			foreach(Player member in GameSystem.Instance.mClient.mPlayerManager)
			{
				if( member.m_team == m_mainRole.m_team )
					continue;
				member.m_enableAction = false;
				member.m_enableMovement = false;
			}
		}

		m_homeTeam.SortMember();
		m_awayTeam.SortMember();

        AssumeDefenseTarget();

        Team oppoTeam = m_mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
        foreach (Player member in oppoTeam.members)
        {
            if (member.model != null)
				member.model.EnableGrey();
        }

        m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        if (m_mainRole.m_defenseTarget != null)
        {
            m_mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;
        }

        _UpdateCamera(m_mainRole);

		/*
        _CreateGUI();
        m_uiMatch.SetMyTeamSide(m_mainRole.m_team.m_side);
		*/
        
		m_mainTeam = m_mainRole.m_team;

        if (m_config.needPlayPlot)
        {
            m_stateMachine.SetState(MatchState.State.ePlotBegin);
        }
        else
        {
			m_stateMachine.SetState(MatchState.State.eOpening);
        }

		initDone = true;
		m_needTipOff = true;
    }

	public override bool IsCommandValid(Command command)
	{
		if( command == Command.Switch )
			return false;

		return base.IsCommandValid(command);
	}

    public override void HandleGameBegin(Pack pack)
    {
        m_stateMachine.SetState(MatchState.State.eTipOff);
    }

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		UBasketball ball = mCurScene.mBall;
		if( m_uiMatch != null )
		{
			if (ball.m_owner == null)
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(false);
			}
			else if (ball.m_owner.m_team == m_mainRole.m_team)
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

		if( !initDone )
			return;

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
					if( interceptor.m_team == m_mainRole.m_team )
						SwitchMainrole(interceptor);
					else if( interceptor.m_defenseTarget != null )
						SwitchMainrole(interceptor.m_defenseTarget);
				}
			}
		}
	}

	public override bool EnableMatchAchievement()
	{
		return true;
	}

	public override bool EnableMatchTips()
	{
		return true;
	}

	public override bool EnablePlayerTips()
	{
		return true;
	}

	protected override void OnRimCollision (UBasket basket, UBasketball ball)
	{
        m_count24Time = gameMatchTime;
        m_count24TimeStop = false;
		m_b24TimeUp = false;
	}

	override public void SwitchMainrole( Player target )
	{
        //if( m_mainRole == target || m_mainTeam != target.m_team )
        //    return;
        //if( m_stateMachine.m_curState.m_eState == MatchState.State.eOver )
        //    return;

        //target.m_inputDispatcher = new InputDispatcher(target);
        //target.m_inputDispatcher.TransmitUncontrolInfo(m_mainRole.m_inputDispatcher);
        //m_mainRole.m_inputDispatcher = null;
		
        //if( m_mainRole.m_aiMgr != null )
        //    m_mainRole.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;;
        //if (target.m_team.m_role == MatchRole.eDefense)
        //    target.m_inputDispatcher.disableAIOnAction = true;
        //else
        //    if (target.m_aiMgr != null)
        //        target.m_aiMgr.m_enable = false;

        //m_mainRole.m_InfoVisualizer.ShowStaminaBar(false);

        //target.m_InfoVisualizer.ShowStaminaBar(true);
        ////if( target.m_defenseTarget != null && target.m_team.m_role == MatchRole.eDefense )
        ////	target.m_defenseTarget.m_AOD.visible = true;

        //m_mainRole = target;
        //_UpdateCamera(m_mainRole);
        //m_uiController.m_ctrlPlayer = m_mainRole;

		//Logger.Log("current main role: " + m_mainRole.m_id);
	}

	public override bool EnableTakeOver()
	{
		return false;
	}

	public override bool EnableEnhanceAttr()
	{
		return true;
	}

	public override bool EnableSwitchDefenseTarget()
	{
		return false;
	}



}