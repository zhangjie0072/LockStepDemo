using UnityEngine;
using System;
using System.Collections.Generic;

using fogs.proto.msg;

/// <summary>
/// 测试场景。通过GM设置进入，可以自由测试一些操作。
/// </summary>
public class GameMatch_Ready
	:GameMatch
{
	public static uint sceneId;

	public GameMatch_Ready(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
		if (sceneId > 10000)
			config.sceneId = sceneId;
        m_gameMathCountEnable = false;
	}
	
	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		mCurScene.CreateBall();

		if( m_config == null )
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}

		mainRole = GameSystem.Instance.mClient.mPlayerManager.GetPlayerById(uint.Parse(m_config.MainRole.id));
        mainRole.operMode = Player.OperMode.Input;
		mainRole.m_InfoVisualizer = new PlayerInfoVisualizer(mainRole);
		mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
		mainRole.position = GameSystem.Instance.MatchPointsConfig.BeginPos.offenses_transform[0].position;
		mainRole.GrabBall( mCurScene.mBall);

        _UpdateCamera(mainRole);

		_CreateGUI();

        GameMsgSender.SendGameBegin();

		mCurScene.mBasket.onGoal = OnGoal;
	}

    public override void OnGameBegin(GameBeginResp resp)
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
			player.m_InfoVisualizer = new PlayerInfoVisualizer(player);
            player.operMode = Player.OperMode.Input;
        }
        player.m_team.m_role = GameMatch.MatchRole.eOffense;
        return player;
    }

    public void SetMainRole(Player main_role)
    {
        if (main_role != null)
        {
            mainRole.position = GameSystem.Instance.MatchPointsConfig.BeginPos.offenses_transform[0].position;
            mainRole.GrabBall( mCurScene.mBall);
            m_cam.m_trLook = mainRole.transform;
            if (m_uiController == null)
                CreateUIController();
            else
            {
                InputReader.Instance.player = mainRole;
            }

			ResetPlayerPos();
        }
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

    public override bool EnableCounter24()
    {
        return false;
    }

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();
		m_uiMatch.enableBack = false;

		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
		Transform tmBasePanel = UIManager.Instance.m_uiRootBasePanel.transform;
		GameObject button = CommonFunction.InstantiateObject(prefab, tmBasePanel);
		button.transform.localPosition = new Vector3(-580, 326, 0);
		UIEventListener.Get(button).onClick = (go) =>
		{
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIHall");
		};

		GameObject speedUp = CommonFunction.InstantiateObject("Prefab/GUI/SpeedUpButton", tmBasePanel);
		speedUp.GetComponent<UIAnchor>().relativeOffset = new Vector2(0.05f, 0.8f);
	}

	void OnGoal(UBasket basket, UBasketball ball)
	{
		m_homeScore += GetScore(ball.m_pt);
	}
}

