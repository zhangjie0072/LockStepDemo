using UnityEngine;
using System;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 1+2AI   VS  3AI
/// </summary>
public class GameMatch_3ON3
	:GameMatch_MultiPlayer
{

	private bool initDone = false;

    public GameMatch_3ON3(Config config)
        : base(config)
    {
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
    }

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		mCurScene.CreateBall();

        if (m_config == null)
        {
            Debug.LogError("Match config file loading failed.");
            return;
        }

		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		for( int idx = 0; idx != pm.m_Players.Count; idx++ )
		{
			Player player = pm.m_Players[idx];

			player.m_catchHelper = new CatchHelper(player);
			player.m_catchHelper.ExtractBallLocomotion();
			player.m_StateMachine.SetState(PlayerState.State.eStand, true);
			player.m_InfoVisualizer.ShowStaminaBar(false);
            player.operMode = Player.OperMode.AI;
		}

		mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
        mainRole.operMode = Player.OperMode.Input;

		if( !Debugger.Instance.m_bEnableDefenderAction )
		{
			foreach(Player member in GameSystem.Instance.mClient.mPlayerManager)
			{
				if( member.m_team == mainRole.m_team )
					continue;
				member.m_enableAction = false;
				member.m_enableMovement = false;
			}
		}

		m_homeTeam.SortMember();
		m_awayTeam.SortMember();

        AssumeDefenseTarget();

        Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
        foreach (Player member in oppoTeam.members)
        {
            if (member.model != null)
				member.model.EnableGrey();
        }

        mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        if (mainRole.m_defenseTarget != null)
            mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(mainRole);

		/*
        _CreateGUI();
        m_uiMatch.SetMyTeamSide(m_mainRole.m_team.m_side);
		*/
        
		m_needTipOff = true;
    }

	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();
		initDone = true;

		if (m_config.needPlayPlot)
		{
			m_stateMachine.SetState(MatchState.State.ePlotBegin);
		}
		else
		{
			m_stateMachine.SetState(MatchState.State.eOpening);
		}
	}

    public override void OnGameBegin(GameBeginResp resp)
    {
        m_stateMachine.SetState(MatchState.State.eTipOff);
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
		return true;
	}
}
