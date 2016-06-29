using UnityEngine;
using System;
using System.Collections.Generic;
using LuaInterface;

using fogs.proto.msg;

/// <summary>
/// 1v1比赛类型  目前只有【PVE】生涯第一�?
/// </summary>
public class GameMatch_1ON1 :GameMatch, MatchStateMachine.Listener
{
	bool enableGuide = true;
	LuaTable guide;
	LuaFunction funcUpdate;
	LuaFunction funcOnMatchStateChange;

    //TODO 针对PVP修改
	Player rival { get { return mainRole.m_defenseTarget; } }

	public GameMatch_1ON1(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();

		mCurScene.CreateBall();

		m_needTipOff = true;

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

        //TODO 针对PVP修改
		mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		mainRole.m_InfoVisualizer.ShowStaminaBar(true);
        mainRole.operMode = Player.OperMode.Input;

        AssumeDefenseTarget();

		if (rival.model != null)
			rival.model.EnableGrey();
		if( !Debugger.Instance.m_bEnableDefenderAction )
		{
			rival.m_enableAction = false;
			rival.m_enableMovement = false;
		}

        mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        if (rival != null)
            rival.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(mainRole);

		m_stateMachine.m_matchStateListeners.Add(this);

		LoadLua();
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

	void LoadLua()
	{
		if (enableGuide)
		{
			object[] retVals = LuaScriptMgr.Instance.DoFile("Custom/Match/MatchGuide.lua");
			guide = retVals[0] as LuaTable;

			funcUpdate = (LuaFunction)(guide["Update"]);
			funcOnMatchStateChange = (LuaFunction)(guide["OnMatchStateChange"]);
			LuaFunction funcInit = (LuaFunction)(guide["Init"]);
			funcInit.Call(new object[] { guide, this });
		}
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);
		_RefreshAOD();

		UBasketball ball = mCurScene.mBall;
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

		if (enableGuide)
			funcUpdate.Call(new object[] { guide, (float)deltaTime });
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
	

	public override void InitBallHolder()
	{
		UBasketball ball = mCurScene.mBall;
		if(ball != null && ball.m_owner != null )
			ball.m_owner.DropBall(ball);
		if (ball != null)
			ball.Reset();
		
		if( m_offenseTeam.GetMemberCount() != 0 )
		{
			Player offenserWithBall = m_offenseTeam.GetMember(0);
			//if (m_offenseTeam == m_homeTeam)
			//	offenserWithBall = m_mainRole;
			offenserWithBall.GrabBall(ball);
			offenserWithBall.m_StateMachine.SetState(PlayerState.State.eHold);
		}
	}

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
		if (enableGuide)
			funcOnMatchStateChange.Call(new object[] { guide, oldState, newState });
	}
}
