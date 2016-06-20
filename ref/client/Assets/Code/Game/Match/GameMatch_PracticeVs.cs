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
	
	override public void OnSceneComplete ()
	{
		base.OnSceneComplete();
		m_needTipOff = true;
		
		mCurScene.CreateBall();
		
		if( m_config == null )
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

		m_mainRole = GameSystem.Instance.mClient.mPlayerManager.GetPlayerById(uint.Parse(m_config.MainRole.id));
        m_mainRole.m_aiMgr.m_enable = false;
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		m_mainRole.m_inputDispatcher = new InputDispatcher(this, m_mainRole);


        //m_mainRole.m_InfoVisualizer = new PlayerInfoVisualizer(m_mainRole);
        //m_mainRole.m_InfoVisualizer.CreateStrengthBar();
        //m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        //m_mainRole.position = mCurScene.m_matchBegin.offense[0].position;
        //m_mainRole.GrabBall( mCurScene.mBall);

        
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

        //m_mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(m_mainRole);
		//_CreateGUI();
        //m_uiMatch.VisibleScoreBoardUI(false);
        m_mainTeam = m_mainRole.m_team;
      
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
    public override void HandleGameBegin(Pack pack)
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
            player.m_inputDispatcher = new InputDispatcher(this, player);
			player.m_InfoVisualizer = new PlayerInfoVisualizer(player);
            player.m_InfoVisualizer.CreateStrengthBar();
			player.m_InfoVisualizer.ShowStaminaBar(true);
        }
        player.m_team.m_role = GameMatch.MatchRole.eOffense;
        return player;
    }

    public void SetMainRole(Player main_role)
    {
        if (main_role != null)
        {
            m_mainRole.position = GameSystem.Instance.MatchPointsConfig.BeginPos.offenses_transform[0].position;
            m_mainRole.GrabBall( mCurScene.mBall);
            m_cam.m_trLook = m_mainRole.transform;
            if (m_uiController == null)
                CreateUIController();
            else
            {
                InputReader.Instance.player = m_mainRole;
            }

			ResetPlayerPos();
        }
    }

    public void RemoveMainRole()
    {
        if (m_mainRole != null)
        {
            if( m_mainRole.m_bWithBall )
                m_mainRole.DropBall( mCurScene.mBall );
            m_mainRole.Release();
            m_mainRole = null;
            m_cam.m_trLook = mCurScene.mBasket.transform;
            NGUITools.Destroy(m_uiController.gameObject);
            m_uiController = null;
        }
    }


    override public void SwitchMainrole( Player target )
    {

        if( m_mainRole == target || m_mainTeam != target.m_team )
        {
            return;
        }

        if( m_stateMachine.m_curState.m_eState == MatchState.State.eOver)
        {
            return;
        }

		base.SwitchMainrole(target);
		

        target.m_inputDispatcher = new InputDispatcher(this, target);
        target.m_inputDispatcher.TransmitUncontrolInfo(m_mainRole.m_inputDispatcher);
        m_mainRole.m_inputDispatcher = null;

        if( m_mainRole.m_aiMgr != null )
        {
            m_mainRole.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;
        }
        if( target.m_team.m_role == GameMatch.MatchRole.eDefense)
        {
            target.m_inputDispatcher.disableAIOnAction = true;
        }
        else
        {
            if( target.m_aiMgr != null )
            {
                target.m_aiMgr.m_enable = false;
            }
        }

        m_mainRole.m_InfoVisualizer.ShowStaminaBar(false);

        m_mainRole = target;
        m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
        _UpdateCamera(m_mainRole);
        InputReader.Instance.player = m_mainRole;
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
    public override void Update(IM.Number deltaTime)
    {
        base.Update(deltaTime);

        UBasketball ball = mCurScene.mBall;
        if (m_uiMatch != null)
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

        if (!initDone)
            return;

        if (m_stateMachine.m_curState.m_eState != MatchState.State.eOpening && m_stateMachine.m_curState.m_eState != MatchState.State.eOverTime
           && ball != null)
        {
            if (ball.m_owner != null)
            {
                Player owner = ball.m_owner;
                SwitchMainrole(owner);
            }
            else if (ball.m_ballState == BallState.eUseBall_Pass)
            {
                Player interceptor = ball.m_interceptor;
                if (interceptor == null)
                {
                    Player owner = ball.m_catcher;
                    SwitchMainrole(owner);
                }
                else
                {
                    if (interceptor.m_team == m_mainRole.m_team)
                        SwitchMainrole(interceptor);
                    else if (interceptor.m_defenseTarget != null)
                        SwitchMainrole(interceptor.m_defenseTarget);
                }
            }
        }
    }

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();
		GameObject button = m_uiMatch.goExit;
		UIEventListener.Get(button).onClick = (go) =>
		{
            LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
            table.Set("uiBack", (object)"UIHall");
            //LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { GameSystem.Instance.mClient.mCurMatch.leagueType, null, table });
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIPracticeCourt", null, table });
			//LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIHall");
		};

        //GameObject speedUp = CommonFunction.InstantiateObject("Prefab/GUI/SpeedUpButton", tmBasePanel);
        //speedUp.GetComponent<UIAnchor>().relativeOffset = new Vector2(0.05f, 0.8f);
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

