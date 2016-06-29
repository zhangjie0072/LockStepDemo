using UnityEngine;
using System;
using System.Collections.Generic;

using fogs.proto.msg;

public class GameMatch_PracticeVs
	:GameMatch_MultiPlayer
{
	public static uint sceneId;
    private bool initDone = false;

	public GameMatch_PracticeVs(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
		if (sceneId > 10000)
			config.sceneId = sceneId;
	}
	
	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		m_needTipOff = true;
		
		mCurScene.CreateBall();
		
		if( m_config == null )
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

		mainRole = GameSystem.Instance.mClient.mPlayerManager.GetPlayerById(uint.Parse(m_config.MainRole.id));
        mainRole.operMode = Player.OperMode.Input;


        //mainRole.m_InfoVisualizer = new PlayerInfoVisualizer(mainRole);
        //mainRole.m_InfoVisualizer.CreateStrengthBar();
        //mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        //mainRole.position = mCurScene.m_matchBegin.offense[0].position;
        //mainRole.GrabBall( mCurScene.mBall);

        
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

        //mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(mainRole);
		//_CreateGUI();
        //m_uiMatch.VisibleScoreBoardUI(false);
      
        //m_stateMachine.SetState(MatchState.State.eBegin);

       // mCurScene.mBasket.onGoal = OnGoal;
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

    public Player CreatePlayer(Config.TeamMember mem)
    {
        if ((FightStatus)mem.pos == FightStatus.FS_MAIN)
        {
            Player role = MainPlayer.Instance.GetRole(uint.Parse(mem.id));
            if (role != null)
                mem.roleInfo = role.m_roleInfo;
        }
		Player player = _GeneratePlayerData(mem);
        if ((FightStatus)mem.pos == FightStatus.FS_MAIN)
        {
			player.m_InfoVisualizer = new PlayerInfoVisualizer(player);
        }
        player.m_team.m_role = GameMatch.MatchRole.eOffense;
        return player;
    }

	protected override void OnRimCollision (UBasket basket, UBasketball ball)
	{
        m_count24Time = gameMatchTime;
        m_count24TimeStop = false;
		m_b24TimeUp = false;
	}

    public override bool EnableCounter24()
    {
        return true;
    }

    public override bool TimmingOnStarting()
    {
        return true;
    }

	public override bool EnableGoalState()
	{
		return true;
	}

	public override bool EnableSwitchRole()
	{
		return true;
	}

    public override bool EnableMatchTips()
    {
        return true;
    }

	public override bool EnableTakeOver()
	{
		return false;
	}

	public override bool EnablePlayerTips()
	{
		return true;
	}

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();
		GameObject button = m_uiMatch.goExit;
        UIEventListener.Get(button).onClick = OnExit;
	}

    private void OnExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = true;
        PopPauseDlg(UIManager.Instance.m_uiRootBasePanel.transform, OnConfirmExit, OnCancelExit);
    }

    private void OnConfirmExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
        LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
        table.Set("uiBack", (object)"UIHall");
        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIPracticeCourt", null, table });
    }
    
    //void OnGoal(UBasket basket, UBasketball ball)
    //{
    //    m_homeScore += GetScore(ball.m_pt);
    //}

    //public override void ResetPlayerPos()
    //{
    //    GameObject obj = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/FreeThrowCenter") as GameObject;
    //    m_mainRole.position = obj.transform.position - Vector3.forward * 0.5f;
    //    m_mainRole.FaceTo(mCurScene.mBasket.m_vShootTarget);

    //    //m_mainRole.m_defenseTarget.position = m_mainRole.position + new Vector3(2f, 0f, 0f);
    //    //m_mainRole.m_defenseTarget.forward =
    //      //  GameUtils.HorizonalNormalized(mCurScene.mBasket.m_vShootTarget, m_mainRole.m_defenseTarget.position);
    //}

}

