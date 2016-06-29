using fogs.proto.msg;
using LuaInterface;
using ProtoBuf;
using System;
using System.IO;
using UnityEngine;

public class PlatNetwork : Singleton<PlatNetwork>
{
    VerifyCDKeyResp verifyCDKeyResp = null;

    private System.DateTime mEnterPlatReqTime;

    public bool plat_entered = false;
    public bool enter_plat_requested = false;

	public LuaStringBuffer cachedEnterGameReq;
	public ExitGameReq cachedExitGameReq;

	public System.Action onReconnected;
	public System.Action onDisconnected;

	public EnterPlatResp respInfo;

    //连接PlatServer
    public void ConnectToPS(string platIP, uint platPort)
    {
        if (GlobalConst.IS_NETWORKING)
        {
            ServerIPAndPort serverInfo = GameSystem.Instance.ServerDataConfigData.GetServerIPAndPort();
            string serverIP = platIP;

            GameSystem.Instance.mNetworkManager.ConnectToPS(serverIP, (int)platPort);

            Debug.Log("--- Connet to PlatServer: " + serverIP + " -- " + platPort);
        }
        else
        {
            //TODO: add process here.
        }
    }

    public void SendHeartbeatMsg(Heartbeat data)
    {
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<Heartbeat>(0, data, MsgID.HeartbeatID);
        GameSystem.Instance.SendHeartbeatMsg();
    }

    public void SaveCDKeyRespResult(VerifyCDKeyResp resp)
    {
        verifyCDKeyResp = resp;
    }

    //登录PlatServer请求
    public void EnterPlatReq()
    {
        Debug.Log("---------------------EnterPlatReq");

        if (verifyCDKeyResp != null)
        {
            MainPlayer.Instance.AccountID = verifyCDKeyResp.acc_id;

            EnterPlat enterPlat = new EnterPlat();
            enterPlat.acc_id = verifyCDKeyResp.acc_id;
            enterPlat.session = verifyCDKeyResp.session;
            enterPlat.login_index = verifyCDKeyResp.login_index;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack<EnterPlat>(0, enterPlat, MsgID.EnterPlatID);
            enter_plat_requested = true;
            plat_entered = false;
            mEnterPlatReqTime = System.DateTime.Now;
        }
    }

    public bool IsEnterPlatTimeout()
    {
        return !plat_entered && enter_plat_requested &&
            (System.DateTime.Now - mEnterPlatReqTime).TotalSeconds > GameSystem.Instance.CommonConfig.GetUInt("gConnTimeout");
    }

	//进入PlatServer请求回复消息处理
	public void EnterPlatRespHandle(Pack pack)
	{
		Debug.Log("---------------------EnterPlatRespHandle");
        //CheatingDeath.Instance.mAntiSpeedUp.ResetWatch();

        plat_entered = true;
		
		EnterPlatResp resp = Serializer.Deserialize<EnterPlatResp>(new MemoryStream(pack.buffer));
		ErrorID err = (ErrorID)resp.result;
		Debug.Log("result:" + err);
		if (err != ErrorID.SUCCESS)
		{
			if (err == ErrorID.ACCOUNT_ALREADY_LOGIN)
			{
                Debug.Log("ACCOUNT_ALREADY_LOGIN");
                CommonFunction.ShowTip(CommonFunction.GetConstString("STR_ACCOUNT_ALREAY_LOGIN"), null);

                GameSystem.Instance.mNetworkManager.connPlat = false; // do not reconnect.
                GameSystem.Instance.mNetworkManager.ClosePlatConn();
                GameSystem.Instance.mClient.mUIManager.LoginCtrl.IsLogin = false;
                //if (!GameSystem.Instance.mNetworkManager.isWaittingReLogin)
                //{
                //    GameSystem.Instance.mNetworkManager.isWaittingReLogin = true;
                //    NetLoading.Instance.isReLogin = true;
                //    NetLoading.Instance.play = true;
                //    NetLoading.Instance.onTimeOut = () =>
                //    {
                //        GameSystem.Instance.mNetworkManager.isReconnecting = true;
                //        GameSystem.Instance.mNetworkManager.isWaittingReLogin = false;
                //        LoginNetwork.Instance.ConnectToLS();
                //    };
                //}
				return;
			}
			else if (err == ErrorID.LOGIN_SERVER_BUSY)
			{
				CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("SERVER_MAINTENANCE"));
				return;
			}
			Debug.Log("Error -- EnterPlatResp returns error: " + err);
			CommonFunction.ShowErrorMsg(err);
			return;
		}

        respInfo = resp;
		GameSystem.Instance.mNetworkManager.isReconnecting = false;
		GameSystem.Instance.mNetworkManager.autoReconnInMatch = false;
		GameSystem.Instance.mClient.pause = false;
		NetLoading.Instance.play = false;
		FriendData.Instance.Init();

        if (resp.create_step == 1)
            GameSystem.Instance.isNewPlayer = true;
        else
            GameSystem.Instance.isNewPlayer = false;

		if (GameSystem.Instance.loadConfigFinish)
			EnterGame();
		else
			GameSystem.Instance.mClient.mUIManager.LoadingCtrl.ShowUIForm();

		if (PlatNetwork.Instance.onReconnected != null)
			PlatNetwork.Instance.onReconnected();
	}

    public void EnterGame()
    {
        if (respInfo == null)
        {
            Debug.LogError("Enter game failed with empty info");
            return;
        }

        MainPlayer.Instance.AccountID = respInfo.acc_id;
        MainPlayer.Instance.CreateStep = respInfo.create_step;
        MainPlayer.Instance.SetBaseInfo(respInfo.info);

		Debug.Log("EnterGame, create_step:" + respInfo.create_step);
		Debug.Log("EnterGame, loadedLevel:" + Application.loadedLevel);
        if (Application.loadedLevelName == GlobalConst.SCENE_STARTUP)	// scene startup
		{
			GameSystem.Instance.mClient.Reset();
			if (respInfo.create_step == 1)	//进入基础操作练习
			{
				PractiseData practise = GameSystem.Instance.PractiseConfig.GetConfig(20001);
				GameSystem.Instance.mClient.CreateMatch(practise, 0ul);
				GameMatch_Practise match = GameSystem.Instance.mClient.mCurMatch as GameMatch_Practise;
				match.onBehaviourCreated = () =>
				{
					PractiseBehaviourGuide behaviour = match.practise_behaviour as PractiseBehaviourGuide;
					behaviour.onOver = () =>
					{
						CreateStepIn req = new CreateStepIn();
						req.acc_id = respInfo.acc_id;
						GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.CreateStepInID);
					};
				};
			}
			else
			{
				MainPlayer.Instance.HpRestoreTime = DateTime.Now + new TimeSpan(0, 0, (int)respInfo.hp_restore_remain);

				//老玩家，进入大厅界面
                LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
                tScene.Set("targetUI", "UIHall");
                tScene.Set("subID", null);
                tScene.Set("params", null);
                SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
			}
		}
        else
        {
			GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
			if (curMatch == null)
            	GameSystem.Instance.mClient.Reset();
			else	// In match, resend EnterGameReq
			{
				Debug.Log("EnterGame, Curr league type:" + curMatch.GetConfig().leagueType);
				if (curMatch.GetConfig().leagueType == GameMatch.LeagueType.eQualifying)
				{
					GameSystem.Instance.mClient.Reset();
					LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
					tScene.Set("targetUI", "UIQualifying");
					tScene.Set("subID", null);
					tScene.Set("params", null);
					SceneManager.Instance.ChangeScene(GlobalConst.SCENE_MATCH);
				}
				else if (curMatch.GetConfig().leagueType == GameMatch.LeagueType.ePVP)
				{
					LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[]{"UIHall", null, null});
				}
				else if (curMatch.GetConfig().leagueType == GameMatch.LeagueType.eQualifyingNewer)
				{
                    LuaTable tQualifyingNewer = LuaScriptMgr.Instance.GetLuaTable("QualifyingNewer");
                    tQualifyingNewer.Set("inBackToLadder", false);
                    tQualifyingNewer.Set("isJoinLadder", false);
                    tQualifyingNewer.Set("isWinShowIncStarAnim", false);


					LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[]{"UIHall", null, null});
				}
				else if (curMatch.leagueType == GameMatch.LeagueType.eRegular1V1 ||
					curMatch.leagueType == GameMatch.LeagueType.eQualifyingNew
                    || curMatch.leagueType == GameMatch.LeagueType.ePractise1vs1)
				{
                    MatchStateOver over = curMatch.m_stateMachine.m_curState as MatchStateOver;
                    if( over == null)
                    {
                        GameSystem.Instance.mClient.Reset();
                        if (curMatch.leagueType == GameMatch.LeagueType.eRegular1V1)
                            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
                        else if (curMatch.leagueType == GameMatch.LeagueType.eQualifyingNew)
                        {
                            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIHall", null, null });
                        }
                    }
                    else
                    {
                        if (curMatch.m_stateMachine.m_curState.m_eState != MatchState.State.eOver ||
                           (over != null && !over.matchResultSent)
                            )
                        {
                            Debug.Log("Resend cached EnterGameReq from new");
                            MatchType type = GameMatch_PVP.ToMatchType(curMatch.leagueType, curMatch.m_config.type);
                            if (curMatch.leagueType == GameMatch.LeagueType.ePractise1vs1) {
                                EnterGameReq req = new EnterGameReq();
                                req.acc_id = MainPlayer.Instance.AccountID;
                                req.type = type;
                                req.game_mode = fogs.proto.msg.GameMode.GM_Practice1On1;
                                req.practice_pve = new BeginPracticePve();
                                req.practice_pve.id = 1;
                                req.practice_pve.fight_list = new FightRoleInfo();
                                req.practice_pve.fight_list.game_mode = fogs.proto.msg.GameMode.GM_Practice1On1;
                                FightRole fr = new FightRole();
                                fr.role_id = curMatch.mainRole.m_id;
                                fr.status = FightStatus.FS_MAIN;
                                req.practice_pve.fight_list.fighters.Add(fr);
                                over.SendEnterGamePractise1vs1(req);
                            }
                            else {
                                over.SendEnterGame(type);
                            }

                           
                        }
                    }
				}
				else if (PlatNetwork.Instance.cachedEnterGameReq != null)
				{
					if (curMatch.m_stateMachine.m_curState.m_eState != MatchState.State.eOver ||
						!(curMatch.m_stateMachine.m_curState as MatchStateOver).matchResultSent)
					{
						Debug.Log("Resend cached EnterGameReq");
						LuaHelper.SendPlatMsgFromLua((uint)MsgID.EnterGameReqID, PlatNetwork.Instance.cachedEnterGameReq);
					}
				}
			}
        }

		if (respInfo.create_step >= GameSystem.Instance.CommonConfig.GetUInt("gMaxCreateStep"))
		{
			// after renaming step( the last step), mark as login.
			MainPlayer.Instance.AddCreateNewRoleLog(true);
		}
        else
        {
            MainPlayer.Instance.AddCreateNewRoleLog(false);
        }
    }

	public void SendExitGameReq(ExitGameReq req)
	{
		if (GameSystem.Instance.mNetworkManager.isReconnecting)
			cachedExitGameReq = req;
		else
			GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
	}

    //关卡比赛结束请求
    public void EndSectionMatchReq(EndSectionMatch career)
    {
        Debug.Log("---------------------EndSectionMatchReq");

        if (GlobalConst.IS_NETWORKING)
        {
            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_CAREER;
            req.exit_type = ExitMatchType.EMT_END;
            req.career = career;
			SendExitGameReq(req);
        }
        else
        {
            //GameSystem.Instance.mClient.mUIManager.CareerCtrl.showMatchResult = true;
        }
    }

    ///
    ///
    //脚本接口
    public void GMCommondExcuReq(GMCommondExcu req)
    {
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<GMCommondExcu>(0, req, MsgID.GMCommondExcuID);
    }
}