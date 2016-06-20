using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;

public class GameMsgHandler
	:MsgHandler
{
	public GameMsgHandler()
    {
		m_strId = "gs";

		RegisterHandler(MsgID.BeginGuideRespID, 	GuideSystem.Instance.BeginGuideHandler);
		RegisterHandler(MsgID.EndGuideRespID, 		GuideSystem.Instance.EndGuideHandler);
		RegisterHandler(MsgID.HeartbeatID, 			HeartbeatHandle);
		RegisterHandler(MsgID.GameBeginRespID,		HandleGameBegin);
		RegisterHandler(MsgID.TeamMateStateChangeID, 			OnTeamMateStateChange);
    }

	private void HeartbeatHandle(Pack pack)
	{
		Heartbeat data = Serializer.Deserialize<Heartbeat>(new MemoryStream(pack.buffer));
		GameSystem.mTime = (long)data.server_time + 1;
		GameSystem.Instance.ReceiveHeartbeatMsg();

        //通过PlatMsgHandler验证是否作弊（变速齿轮）
		//if( !CheatingDeath.Instance.mAntiSpeedUp.m_beginWatch )
		//	CheatingDeath.Instance.mAntiSpeedUp.BeginWatch(data.server_time);
		//data.server_time = (uint)CheatingDeath.Instance.mAntiSpeedUp.m_clientTime;

		if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack<Heartbeat>(0, data, MsgID.HeartbeatID);
		GameSystem.Instance.SendHeartbeatMsg();
	}

	private void HandleGameBegin(Pack pack)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match != null )
			match.HandleGameBegin(pack);
	}

	private void OnEnterGameSrv(Pack pack)
	{
		EnterGameSrvResp resp = Serializer.Deserialize<EnterGameSrvResp>(new MemoryStream(pack.buffer));
		if( (ErrorID)resp.result == ErrorID.SUCCESS )
		{
			EnterRoomReq req = new EnterRoomReq();
			req.type = MatchType.MT_1V1;

			if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
				GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.EnterRoomReqID);
		}
		else
		{
			Logger.LogError("Enter game server error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
	}
	private void ExitRoomHandler(Pack pack)
	{
		ExitRoomResp resp = Serializer.Deserialize<ExitRoomResp>(new MemoryStream(pack.buffer));
		if (GameSystem.Instance.mClient.mCurMatch != null)
		{
			GameSystem.Instance.mNetworkManager.CloseGameServerConn();
			GameSystem.Instance.mClient.Reset();
            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_STARTUP);
		}
	}
	
	private void OnTeamMateStateChange(Pack pack)
	{
		TeamMateStateChange msg = Serializer.Deserialize<TeamMateStateChange>(new MemoryStream(pack.buffer));
        uint state = msg.state;
        uint index = msg.index;

        Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByRoomId(index);
        GameMatch gm = GameSystem.Instance.mClient.mCurMatch;
        Logger.Log("1927 - OnTeamMateStateChange state=" + state + " index=" + index);

        if(state == 1) // offline
			player.m_toTakeOver = true;
        else if ( state == 2 ) // online.
        {
			if( player.m_takingOver )
			{
            	GameMatch_PVP pvpMatch = gm as GameMatch_PVP;
            	pvpMatch.m_PlayersToControl.Add(player);
			}
        }
    }

	/*
    void ExitGameRespHandler(Pack pack)
	{
		Logger.Log("exit game");
        Logger.Log("---------------------ExitGameRespHandler");

		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.GameOverID, GameMatch_PVP.HandleGameOver);
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.GameFaulID, GameMatch_PVP.HandleGameFaul);
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.BeginTipOffRespID, 	GameMatch_PVP.HandleTipOffBegin);

		GameSystem.Instance.mNetworkManager.CloseGameServerConn();
        ExitGameResp resp = Serializer.Deserialize<ExitGameResp>(new MemoryStream(pack.buffer));
        if (resp.type == MatchType.MT_REGULAR_RACE)
        {
			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
				match.RegularCompleteHandler(resp.regular);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Logger.Log("Self exit");
                //totalTimes And winTimes add one
                MainPlayer.Instance.pvp_regular.race_times += 1;
                Logger.Log("race time:" + MainPlayer.Instance.pvp_regular.race_times);
				MainPlayer.Instance.pvp_regular.score = resp.regular.score;
                Logger.Log("score:" + MainPlayer.Instance.pvp_regular.score);

				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIHall");
			}
			else
			{
				UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
				if ( goLoading != null)
                {
					goLoading.disConnected = true;
					goLoading.pvpRegularEndResp = resp.regular;
                }
                else
                {
                    UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
                    {
                        GameSystem.Instance.mClient.mCurMatch.m_stateMachine.SetState(MatchState.State.eOver);
                        MatchStateOver matchOver = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
                        matchOver.RegularCompleteHandler(resp.regular);
                    };
                    CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.regular.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
                }
			}
        }
        else if (resp.type == MatchType.MT_QUALIFYING_NEW)
        {
			QualifyingNewInfo qualifying = MainPlayer.Instance.qualifying_new;
			int scoreDelta = (int)resp.qualifying_new.score - (int)qualifying.score;
			qualifying.score = resp.qualifying_new.score;
			qualifying.max_score = (uint)Mathf.Max((int)qualifying.max_score, (int)qualifying.score);
			qualifying.ranking = resp.qualifying_new.ranking;
			qualifying.race_times = resp.qualifying_new.race_times;
			qualifying.win_times = resp.qualifying_new.win_times;
            qualifying.winning_streak = resp.qualifying_new.winning_streak;
			qualifying.max_winning_streak = resp.qualifying_new.max_winning_streak;
			Logger.Log("QualifyingNew score:" + resp.qualifying_new.score + " ranking:" + resp.qualifying_new.ranking);

			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
				match.QualifyingNewCompleteHandler(resp.qualifying_new, scoreDelta);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Logger.Log("Self exit");
				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIQualifyingNew");
			}
			else
			{
				UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
				if ( goLoading != null)
                {
					goLoading.disConnected = true;
					goLoading.pvpQualifyingEndResp = resp.qualifying_new;
                }
                else
                {
                    UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
                    {
                        GameSystem.Instance.mClient.mCurMatch.m_stateMachine.SetState(MatchState.State.eOver);
                        MatchStateOver matchOver = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
                        matchOver.QualifyingNewCompleteHandler(resp.qualifying_new, scoreDelta);
                    };
                    CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.qualifying_new.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
                }
			}
        }
        else if (resp.type == MatchType.MT_PVP_1V1_PLUS)
        {
			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
				match.ChallengeCompleteHandler(resp.challenge_plus);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Logger.Log("Self exit");
                //totalTimes And winTimes add one
                MainPlayer.Instance.PvpPlusInfo.race_times += 1;
                Logger.Log("race time:" + MainPlayer.Instance.PvpPlusInfo.race_times);
                //update challenge score
                MainPlayer.Instance.PvpPlusInfo.score = resp.challenge_plus.score;

                LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
                table.Set("uiBack", (object)"UIPVPEntrance");
                LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UI1V1Plus", null, table });
			}
			else
			{
				UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
				if ( goLoading != null)
                {
					goLoading.disConnected = true;
					goLoading.pvpPlusEndResp = resp.challenge_plus;
                }
                else
                {
                    UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
                    {
                        GameSystem.Instance.mClient.mCurMatch.m_stateMachine.SetState(MatchState.State.eOver);
                        MatchStateOver matchOver = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
                        matchOver.ChallengeCompleteHandler(resp.challenge_plus);
                    };
                    CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.challenge_plus.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
                }
			}
			Logger.Log("pvp data costs: " + GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.m_dataUsage );
			GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.EndRecordDataUsage();
        }
        else if (resp.type == MatchType.MT_PVP_3V3)
        {
            if (resp.exit_type == ExitMatchType.EMT_END)
            {
                MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
                match.ChallengeExCompleteHandler(resp.challenge_ex);
            }
            else if (resp.exit_type == ExitMatchType.EMT_OPTION)
            {
                Logger.Log("Self exit");
            }
            else
            {
				UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
				if (goLoading != null)
                {
					goLoading.GetComponent<UIChallengeLoading>().disConnected = true;
					goLoading.GetComponent<UIChallengeLoading>().pvpExEndResp = resp.challenge_ex;
                }
                else
                {
                    UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
                    {
                        GameSystem.Instance.mClient.mCurMatch.m_stateMachine.SetState(MatchState.State.eOver);
                        MatchStateOver matchOver = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
                        matchOver.ChallengeExCompleteHandler(resp.challenge_ex);
                    };
                    CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.challenge_ex.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
                }
            }
			Logger.Log("pvp data costs: " + GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.m_dataUsage );
			GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.EndRecordDataUsage();
        }
	}
	*/
}

public class VirtualGameMsgHandler
	:GameMsgHandler
{
	private BallState	mBallState;

	private Dictionary<UBasketball, Player>		m_mapToPickupBall = new Dictionary<UBasketball, Player>();

	public VirtualGameMsgHandler()
	{
		m_strId = "vgs";
		 
		RegisterHandler(MsgID.InstructionReqID, GameMatch.HandleBroadcast);
        RegisterHandler(MsgID.ClientInputID, HandleClientInput);
        RegisterHandler(MsgID.FrameInfoID, HandleNewTurn);
        RegisterHandler(MsgID.GameBeginReqID, HandleGameBeginReq);
	}

    //For virtual game server
    void HandleClientInput(Pack pack)
    {
        ClientInput input = ProtoBuf.Serializer.Deserialize<ClientInput>(new MemoryStream(pack.buffer));
        VirtualGameServer.Instance.AddInput(input);
        //Logger.Log("Handle input, " + (InputDirection)input.dir + " " + (Command)input.cmd);
    }

    void HandleNewTurn(Pack pack)
    {
        FrameInfo turn = ProtoBuf.Serializer.Deserialize<FrameInfo>(new MemoryStream(pack.buffer));
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
        match.turnManager.NewTurn(turn);
        //Logger.Log("HandleNewTurn, " + turn.frameNum);
    }

    //For virtual game server
    void HandleGameBeginReq(Pack pack)
    {
		GameBeginReq req = Serializer.Deserialize<GameBeginReq>(new MemoryStream(pack.buffer));
        VirtualGameServer.Instance.OnGameBegin(req);
    }

    void HandleGameBeginResp(Pack pack)
    {
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
        match.HandleGameBegin(pack);
    }
}

