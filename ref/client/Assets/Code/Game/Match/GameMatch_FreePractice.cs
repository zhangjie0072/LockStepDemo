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
		mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        mainRole.position = GameSystem.Instance.MatchPointsConfig.BeginPos.offenses_transform[0].position;
		mainRole.GrabBall( mCurScene.mBall);

        _UpdateCamera(mainRole);

		_CreateGUI();
        m_uiMatch.VisibleScoreBoardUI(false);
   
		m_bShowGoalUIEffect = false;

        GameMsgSender.SendGameBegin();
	}

    public override void OnGameBegin(GameBeginResp resp)
    {
		m_stateMachine.SetState(MatchState.State.eBegin);
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
        UIEventListener.Get(button).onClick = OnExit;
	}

    private void OnExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = true;
        PopPauseDlg(UIManager.Instance.m_uiRootBasePanel.transform, OnConfirmExit, OnCancelExit);
    }

    protected void OnConfirmExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
        LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
        table.Set("uiBack", (object)"UIHall");
        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIPracticeCourt", null, table });
    }


    public override void ResetPlayerPos()
    {
        mainRole.position = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position - IM.Vector3.forward * IM.Number.half;
        mainRole.FaceTo(mCurScene.mBasket.m_vShootTarget);
    }

}

