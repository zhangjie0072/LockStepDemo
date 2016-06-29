using UnityEngine;
using System;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMatch_LadderAI
	:GameMatch_MultiPlayer
{

    public GameMatch_LadderAI(Config config)
        : base(config)
    {
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
        _nameIndex = 1; 
    }

    public List<string> _names = new List<string>();

    public uint _nameIndex = 1;
    
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

            player.operMode = Player.OperMode.AI;
			
			player.m_catchHelper = new CatchHelper(player);
			player.m_catchHelper.ExtractBallLocomotion();
			player.m_StateMachine.SetState(PlayerState.State.eStand, true);
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
        {
            mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;
        }

		/*
        _CreateGUI();
        m_uiMatch.SetMyTeamSide(m_mainRole.m_team.m_side);
		*/
        

		m_needTipOff = true;
    }

    public override AISystem CreateAISystem(Player player)
    {
        return new AISystem_Basic(this, player, AIState.Type.eInit, player.m_config.AIID);
    }

	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();

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

	public override bool IsCommandValid(Command command)
	{
		if(  command == Command.Switch )
			return false;

		return base.IsCommandValid(command);
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
		return false;
	}

	public override bool EnableSwitchDefenseTarget()
	{
		return false;
	}



}