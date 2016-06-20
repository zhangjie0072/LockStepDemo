using UnityEngine;
using System;
using System.Collections.Generic;

using fogs.proto.msg;

public class GameMatch_FreePractice
	:GameMatch
{
	public static uint sceneId;

	public GameMatch_FreePractice(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
		if (sceneId > 10000)
			config.sceneId = sceneId;
	}
	
	override public void OnSceneComplete ()
	{
		base.OnSceneComplete();
		mCurScene.CreateBall();

		if( m_config == null )
		{
			Logger.LogError("Match config file loading failed.");
			return;
		}

		m_mainRole = GameSystem.Instance.mClient.mPlayerManager.GetPlayerById(uint.Parse(m_config.MainRole.id));
		m_mainRole.m_inputDispatcher = new InputDispatcher(this, m_mainRole);
        //m_mainRole.m_InfoVisualizer = new PlayerInfoVisualizer(m_mainRole);
		m_mainRole.m_InfoVisualizer.CreateStrengthBar();
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        m_mainRole.position = GameSystem.Instance.MatchPointsConfig.BeginPos.offenses_transform[0].position;
		m_mainRole.GrabBall( mCurScene.mBall);

        _UpdateCamera(m_mainRole);
		_CreateGUI();
        m_uiMatch.VisibleScoreBoardUI(false);
   
		m_bShowGoalUIEffect = false;

        GameMsgSender.SendGameBegin();
	}

    public override void HandleGameBegin(Pack pack)
    {
		m_stateMachine.SetState(MatchState.State.eBegin);
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

    public override bool EnableCounter24()
    {
        return false;
    }

    public override bool TimmingOnStarting()
    {
        return false;
    }

	public override bool EnableGoalState()
	{
		return false;
	}

	public override bool EnableSwitchRole()
	{
		return false;
	}

	public override bool EnablePlayerTips()
	{
		return true;
	}

	protected override void CreateCustomGUI()
	{
        m_gameMathCountEnable = false;
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


    public override void ResetPlayerPos()
    {
        m_mainRole.position = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position - IM.Vector3.forward * IM.Number.half;
        m_mainRole.FaceTo(mCurScene.mBasket.m_vShootTarget);

        //m_mainRole.m_defenseTarget.position = m_mainRole.position + new Vector3(2f, 0f, 0f);
        //m_mainRole.m_defenseTarget.forward =
          //  GameUtils.HorizonalNormalized(mCurScene.mBasket.m_vShootTarget, m_mainRole.m_defenseTarget.position);
    }

}

