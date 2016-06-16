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
		RegisterHandler(MsgID.GameMsgID, 			OnGameMsg);
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
	
	private void OnGameMsg(Pack pack)
	{
		GameMsg msg = Serializer.Deserialize<GameMsg>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mCurMatch.AddGameMsg(msg);
		List<Player> players = GameSystem.Instance.mClient.mPlayerManager.m_Players;
        //if( Application.isEditor)
        //{
        //    Logger.Log("game msg: sender: " + msg.senderID + " state: " + msg.eState + " action type: " + msg.eStateType);
        //}
		Player sender = players.Find( (Player player)=>{ return player.m_roomPosId == msg.senderID; } );
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if(match == null || match.m_stateMachine == null || match.m_stateMachine.m_curState == null)
			return;
		 
		if( sender == null && 
		   (match.m_config.type == GameMatch.Type.ePVP_1PLUS || match.m_config.type == GameMatch.Type.ePVP_3On3)
		   )
		{
			Logger.LogError("Can not find sender: " + msg.senderID + " for command: " + msg.eState);
			return;
		}
		{
			SimulateCommand cmd = match.GetSmcCommandByGameMsg(sender, msg);
			if( cmd != null && sender.m_smcManager != null)
			{
				//NetworkManager nm = GameSystem.Instance.mNetworkManager;
                //if( Application.isEditor)
                //{
                //    Logger.Log("Command: " + cmd.m_state + " time consume: " + string.Format("{0:f4}", GameSystem.Instance.mNetworkManager.m_dServerTime + (Time.time - nm.m_dLocalTime) - msg.curTime));
                //}
				sender.m_smcManager.AddCommand(cmd);
			}
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

	ReboundHelper reboundHelper = new ReboundHelper();

	public VirtualGameMsgHandler()
	{
		m_strId = "vgs";
		 
		RegisterHandler(MsgID.StandID, 			DummyHandler);
		RegisterHandler(MsgID.MoveID, 			CovertToGameMsg);
		RegisterHandler(MsgID.StolenID,			CovertToGameMsg);
		RegisterHandler(MsgID.CrossOverID,		DummyHandler);
		RegisterHandler(MsgID.AttackID,			CovertToGameMsg);
		RegisterHandler(MsgID.PrepareToShootID,	DummyHandler);
		RegisterHandler(MsgID.DownID,			DummyHandler);
		RegisterHandler(MsgID.InputID,			DummyHandler);

		RegisterHandler(MsgID.BackToBackID,		CovertToGameMsg);
		RegisterHandler(MsgID.BackBlockID,		CovertToGameMsg);
		RegisterHandler(MsgID.BackToStandID,	CovertToGameMsg);
		RegisterHandler(MsgID.BackCompeteID,	CovertToGameMsg);
		RegisterHandler(MsgID.BackTurnRunID,	CovertToGameMsg);

		RegisterHandler(MsgID.ReboundID,		CovertToGameMsg);
		RegisterHandler(MsgID.BodyThrowCatchID,	CovertToGameMsg);
		RegisterHandler(MsgID.RequireBallID,	CovertToGameMsg);
		RegisterHandler(MsgID.CutInID,			CovertToGameMsg);

		RegisterHandler(MsgID.PickBallID,		CovertToGameMsg);

		RegisterHandler(MsgID.InstructionReqID, GameMatch.HandleBroadcast);
        RegisterHandler(MsgID.ClientInputID, HandleClientInput);
        RegisterHandler(MsgID.FrameInfoID, HandleNewTurn);
        RegisterHandler(MsgID.GameBeginReqID, HandleGameBeginReq);
	}

	void DummyHandler(Pack pack)
	{
		//Logger.Log("dummy handler.");
	}

	public override void Update()
	{
		base.Update();
		reboundHelper.Update();
	}

	void CovertToGameMsg(Pack pack)
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;
		GameMsg msg = new GameMsg();
		msg.curTime = float.MinValue;
		if( pack.MessageID == (uint)MsgID.PickBallID )
		{
			PickBall pickBall = Serializer.Deserialize<PickBall>(new MemoryStream(pack.buffer));
			msg.senderID 	= pickBall.char_id;
			msg.eStateType  = pickBall.actionType;
			msg.pos 		= pickBall.pos;
			msg.rotate		= pickBall.rotate;
			msg.eState 		= CharacterState.ePickup;
			msg.ballId		= pickBall.ball_id;

			//Logger.Log("Handle msg: PickBall. Player Id: " + pickBall.char_id); 

			Player playerToPickup = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player player)=>{ return player.m_roomPosId == pickBall.char_id; });
			ball = match.mCurScene.balls.Find((UBasketball inBall)=>{return inBall.m_id == pickBall.ball_id;});
			if( ball == null )
			{
				Logger.Log("Can not find ball id: " + pickBall.ball_id);
				return;
			}

			if(ball != null)
			{
				if( ball.m_owner != null )
					msg.nSuccess = 0;
				else if( ball.m_picker == null )
				{
					msg.nSuccess = 1;
					ball.m_picker = playerToPickup;
				}
				else
				{
					if( ball.m_picker == playerToPickup )
						msg.nSuccess = 1;
					else
						msg.nSuccess = 0;
				}
			}
			else
				msg.nSuccess = 0;

			if( msg.nSuccess == 0 )
				Logger.Log("Player: " + playerToPickup.m_id + " pickup failed.");
			else
				Logger.Log("Player: " + playerToPickup.m_id + " pickup success.");

			SimulateCommand cmd = match.GetSmcCommandByGameMsg(playerToPickup, msg);
			if( cmd != null && playerToPickup.m_smcManager != null )
				playerToPickup.m_smcManager.AddCommand(cmd);
		}
		else if( pack.MessageID == (uint)MsgID.ReboundID )
		{
			Logger.Log("handle rebound.");

			Rebound rebound = Serializer.Deserialize<Rebound>(new MemoryStream(pack.buffer));
			if (rebound.success == 1)
				reboundHelper.AddRebounder(rebound);

			//Player playerToRebound = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player player)=>{ return player.m_roomPosId == rebound.char_id; });
			//ball = match.mCurScene.mBall;
			//if(ball != null && msg.nSuccess == 1)
			//{
			//	if( ball.m_owner != null )
			//		msg.nSuccess = 0;
			//	else if( ball.m_picker == null )
			//	{
			//		msg.nSuccess = 1;
			//		ball.m_picker = playerToRebound;
			//	}
			//	else
			//	{
			//		if( ball.m_picker == playerToRebound )
			//			msg.nSuccess = 1;
			//		else
			//			msg.nSuccess = 0;
			//	}
			//}
			//else
			//	msg.nSuccess = 0;
			//if( msg.nSuccess == 0 )
			//	Logger.Log("Player: " + playerToRebound.m_id + " rebound failed.");
			//else
			//	Logger.Log("Player: " + playerToRebound.m_id + " rebound success.");
		}
		else if( pack.MessageID == (uint)MsgID.BodyThrowCatchID )
		{
			BodyThrowCatch bodyThrowCatch = Serializer.Deserialize<BodyThrowCatch>(new MemoryStream(pack.buffer));
			msg.senderID 	= bodyThrowCatch.char_id;
			msg.eStateType  = bodyThrowCatch.actionType;
			msg.pos 		= bodyThrowCatch.pos;
			msg.rotate		= bodyThrowCatch.rotate;
			msg.eState 		= CharacterState.eBodyThrowCatch;
			msg.velocity	= bodyThrowCatch.velocity;
			msg.nSuccess	= bodyThrowCatch.success;
			
			Player playerToAct = GameSystem.Instance.mClient.mPlayerManager.m_Players.Find( (Player player)=>{ return player.m_roomPosId == bodyThrowCatch.char_id; });
			ball = match.mCurScene.mBall;
			if(ball != null && msg.nSuccess == 1 )
			{
				if( ball.m_owner != null )
					msg.nSuccess = 0;
				else if( ball.m_picker == null )
				{
					msg.nSuccess = 1;
					ball.m_picker = playerToAct;
				}
				else
				{
					if( ball.m_picker == playerToAct )
						msg.nSuccess = 1;
					else
						msg.nSuccess = 0;
				}
			}
			else
				msg.nSuccess = 0;
			
			if( msg.nSuccess == 0 )
				Logger.Log("Player: " + playerToAct.m_id + " body throw catch failed.");
			else
				Logger.Log("Player: " + playerToAct.m_id + " body throw catch success.");

			SimulateCommand cmd = match.GetSmcCommandByGameMsg(playerToAct, msg);
			if( cmd != null && playerToAct.m_smcManager != null )
				playerToAct.m_smcManager.AddCommand(cmd);
		}
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

