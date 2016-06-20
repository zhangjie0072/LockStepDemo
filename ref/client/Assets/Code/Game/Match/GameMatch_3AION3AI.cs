using UnityEngine;
using System;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 【一种玩法】6个球员都是AI，使用玩家数据进行比赛。
/// </summary>
public class GameMatch_3AION3AI 
	:GameMatch_MultiPlayer
{
	private Team m_mainTeam;
	private bool initDone = false;
	
    public GameMatch_3AION3AI(Config config)
        : base(config)
    {
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
    }

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
            m_mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(m_mainRole);
        
		//_CreateGUI();
        //m_uiMatch.SetMyTeamSide(m_mainRole.m_team.m_side);

		m_mainTeam = m_mainRole.m_team;

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


    public override void HandleGameBegin(Pack pack)
    {
        m_stateMachine.SetState(MatchState.State.eTipOff);
    }

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		if( m_uiMatch != null )
		{
			if (mCurScene.mBall.m_owner == null)
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(false);
			}
			else if (mCurScene.mBall.m_owner.m_team == m_mainRole.m_team)
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

		if( mCurScene.mBall != null && mCurScene.mBall.m_owner != null )
		{
			Player owner = mCurScene.mBall.m_owner;
			SwitchMainrole(owner);
		}
	}

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();
		m_uiMatch.enableBack = false;

		UIManager.Instance.CreateUI("SpeedUpButton");
	}

	protected override void CreateUIController()
	{
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

	public override bool EnableEnhanceAttr()
	{
		return true;
	}

	public override bool IsCommandValid (Command command)
	{
		return false;
	}

	override public void SwitchMainrole( Player ballOwner )
	{

		Player target = ballOwner;
		if (m_mainTeam != ballOwner.m_team)
		{
			if (m_mainRole == ballOwner.m_defenseTarget)
				return;
			target = ballOwner.m_defenseTarget;
		}
		else
		{
			if (m_mainRole == ballOwner)
				return;
		}


		m_mainRole.m_InfoVisualizer.ShowStaminaBar(false);
		//if( m_mainRole.m_defenseTarget != null )
		//	m_mainRole.m_defenseTarget.m_AOD.visible = false;

		target.m_InfoVisualizer.ShowStaminaBar(true);
		//if( target.m_defenseTarget != null )
		//	target.m_defenseTarget.m_AOD.visible = true;

		m_mainRole = target;
		_UpdateCamera(m_mainRole);

		Logger.Log("current main role: " + m_mainRole.m_id);
	}


	public override bool EnableSwitchDefenseTarget()
	{
		return true;
	}
}
