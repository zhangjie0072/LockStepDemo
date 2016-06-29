using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;
using LuaInterface;

public class PlatMsgHandler:MsgHandler
{
	public PlatMsgHandler()
    {		
		m_strId = "ps";
		
		//心跳包消息
		RegisterHandler(MsgID.HeartbeatID, HeartbeatHandle);
		
		//Plat消息注册
		RegisterHandler(MsgID.CreateStepInRespID, CreateStepInRespHandler);
		RegisterHandler(MsgID.EnterPlatRespID, PlatNetwork.Instance.EnterPlatRespHandle);
		RegisterHandler(MsgID.ChooseRoleRespID, ChooseRoleRespHandle);
		RegisterHandler(MsgID.LogoutRespID, LogoutRespHandle);
		RegisterHandler(MsgID.UpdatePlayerDataID, UpdatePlayerDataHandle);
		//生涯
		//RegisterHandler(MsgID.StartSectionMatchRespID, StartSectionMatchRespHandle);
		//RegisterHandler(MsgID.EndSectionMatchRespID, EndSectionMatchRespHandle);
		RegisterHandler(MsgID.SweepSectionRespID, SweepSectionRespHandle);
		//RegisterHandler(MsgID.GetChapterStarAwardRespID, GetChapterStarAwardRespHandle);
		//RegisterHandler(MsgID.AddNewChapterID, AddNewChapterHandler); //添加新的章节
		
		//物品
		RegisterHandler(MsgID.UpdateGoodsID, UpdateGoodsHandle);
		/*
        RegisterHandler(MsgID.SellGoodsRespID, SellGoodsRespHandle);
        RegisterHandler(MsgID.CompositeGoodsRespID, CompositeGoodsRespHandle);
        RegisterHandler(MsgID.UseGoodsRespID, UseGoodsRespHandle);
        */
		
		//技能
		//RegisterHandler(MsgID.SkillOperationRespID, SkillOperationHandler);
		
		//球员系统
		RegisterHandler(MsgID.InviteRoleRespID, InvitePlayerRespHandler);
		//RegisterHandler(MsgID.ImproveQualityRespID, ImproveQualityRespHandler);
		//商店系统
		//RegisterHandler(MsgID.OpenStoreRespID, OpenStoreRespHandler);//打开商店
		//RegisterHandler(MsgID.RefreshStoreGoodsRespID, RefreshStoreGoodsRespHandler);//刷新商店
		RegisterHandler(MsgID.BuyStoreGoodsRespID, BuyStoreGoodsRespHandler);//购买物品
		RegisterHandler(MsgID.FashionOperationRespID, FashionOperationRespHandler);//服装商店
		
		//Game消息
		RegisterHandler(MsgID.EnterGameRespID, EnterGameRespHandler);//进入比赛
		RegisterHandler(MsgID.ExitGameRespID, ExitGameRespHandler);//退出比赛
		
		RegisterHandler(MsgID.OnNewDayComeID, OnNewDayComeHandle);	//4点钟

		RegisterHandler(MsgID.UpdateVipInfoID, UpdateVipInfoHandle);
		
		//竞技赛与匹配
		//RegisterHandler(MsgID.EnterRaceRespID, EnterRaceHandler);   //进入竞技赛
		//RegisterHandler(MsgID.BuyRaceTimesRespID, BuyRaceTimesHandler); //购买剩余次数
		//RegisterHandler(MsgID.EnterRoomRespID, EnterRoomHandler);   //进入房间
		//RegisterHandler(MsgID.RankListRespID, RankListHandler);
		
		//任务系统
		//RegisterHandler(MsgID.TaskInfoRespID, TaskInfoHandler);//打开任务界面
		//RegisterHandler(MsgID.GetTaskAwardRespID, GetTaskAwardHandler);//领取任务奖励
		//RegisterHandler(MsgID.UnclaimedTaskNumNotifyID, UnclaimedTaskNumNotifyHandler);//未领取任务奖励通知
		RegisterHandler(MsgID.NotifyTaskFinishID, NotifyTaskFinished);
		
		//邮件系统
		RegisterHandler(MsgID.MailInfoNotifyID, MailInfoNotifyHandle);
		RegisterHandler(MsgID.NewMailNotifyID, NewMailNotifyHandle);
		//RegisterHandler(MsgID.ReadMailRespID, ReadMailHandle);
		//RegisterHandler(MsgID.GetAttachmentRespID, GetAttachmentHandle);
		
		// RegisterHandler(MsgID.BeginPracticeRespID, BeginPractiseHandler);
		
		//队长操作
		RegisterHandler(MsgID.BuyCaptainRespID, BuyCaptainRespHandler);
		RegisterHandler(MsgID.SwitchCaptainRespID, SwitchCaptainRespHandler);
        RegisterHandler(MsgID.EnhanceExerciseRespID, EnhanceExerciseHandler);
		RegisterHandler(MsgID.EnhanceLevelRespID, EnhanceLevelHandler);
        RegisterHandler(MsgID.ImproveQualityRespID, ImproveQualityHandler);
        
		
		//队长训练
		/*
        RegisterHandler(MsgID.StartTrainingRespID, StartTrainingHandler);
        RegisterHandler(MsgID.TrainingUpConfirmRespID, TrainingUpConfirmRespHandler);
        RegisterHandler(MsgID.NotifyTrainingInfoID, NotifyTrainingInfoHandler);
        RegisterHandler(MsgID.OpenTrainingRespID, OpenTrainingHandler);
        */
		
		//纹身
		//RegisterHandler(MsgID.TattooOperationRespID, TattooOperationRespHandler);
		
		//分解物品
		RegisterHandler(MsgID.DecomposeGoodsRespID, DecomposeGoodsRespHandler);
		
		//指引
		RegisterHandler(MsgID.BeginGuideRespID, BeginGuideHandler);
		RegisterHandler(MsgID.EndGuideRespID, EndGuideHandler);
		RegisterHandler(MsgID.ResetRoleRespID, RoleResetHandler);

        //阵容
        RegisterHandler(MsgID.UpdateFightRoleID, UpdateFightRoleHandler);
        //新的一天来临（12点）
        RegisterHandler(MsgID.NewDayComeID, NewDayComeMidNightHandler);
        //公告
        RegisterHandler(MsgID.NoticeToClientID, AnnounceMentHandler);

        //接收聊天广播
        RegisterHandler(MsgID.ChatBroadcastID, ChatBroadCastHandler);
        RegisterHandler(MsgID.ChatLatestInfoRespID, ChatLastestInfoRespHandler);

        //图鉴
        RegisterHandler(MsgID.UpdateRoleMapAddAttrID, UpdateRoleMapInfoHandler);
        //活跃度
        //RegisterHandler(MsgID.UpdateActivityInfoID, UpdateActivityInfoHandler);
        //试炼
        RegisterHandler(MsgID.NewComerTrialNotifyID, UpdateTrialInfo);

        //好友列表维护
        RegisterHandler(MsgID.FriendOperationRespID, FriendOperationResp);

		//刷新排位赛信息
		RegisterHandler(MsgID.RefreshQualifyingNewInfoID, HandleRefreshQualifyingNewInfo);
		//刷新排位赛信息(新版)
		RegisterHandler(MsgID.RefreshQualifyingNewerInfoID, HandleRefreshQualifyingNewerInfo);
        // Pvp room.
        RegisterHandler(MsgID.RoomActionRespID, RoomActionHandler);
        RegisterHandler(MsgID.JoinRoomRespID, JoinRoomHandler);
        RegisterHandler(MsgID.NotifyGameStartID, GameStartHandler);
        RegisterHandler(MsgID.RefreshLadderInfoID, RefreshLadderInfoHandler);
    }

    public void FriendOperationResp(Pack pack)
    {
        FriendOperationResp resp = Serializer.Deserialize<FriendOperationResp>(new MemoryStream(pack.buffer));
        FriendData.Instance.OnFriendOperationResp(resp);
    }

    // TODO: it need move to lua part.
    Queue<RoomActionResp> invitesList = new Queue<RoomActionResp>();
    public void RoomActionHandler(Pack pack)
    {
        Debug.Log("1927 RoomAction Resp --> Handler");
        RoomActionResp resp = Serializer.Deserialize<RoomActionResp>(new MemoryStream(pack.buffer));
        Debug.Log("1927 resp.result= " + resp.result);
        if( resp.result != 0 )
        {
            string str = string.Format("Room Action Type:{0} Error:{1} ", resp.type, resp.result);
            Debug.Log(str);
            return;
        }
        if (resp.type == RoomActionType.RAT_NOTIFY_INVITE)
        {
            if(invitesList.Count >= GameSystem.Instance.CommonConfig.GetUInt("gPVPLadderMaxInviteTimes"))
            {
                return;
            }
            if( GameObject.Find("UIChallengeResult(Clone)") || GameObject.Find("inGameInfoPanel"))
            {
                return;
            }
            invitesList.Enqueue(resp);

            if(invitesList.Count == 1 )
            {
                PopupInviteMsg(invitesList.Peek());
            }
        }
    }
    
    private void PopupInviteMsg(RoomActionResp resp)
    {
        UIEventListener.VoidDelegate InviteRefuse = (GameObject go) =>
            {
                invitesList.Dequeue();
                if (invitesList.Count >= 1)
                {
                    PopupInviteMsg(invitesList.Peek());
                }
            };

        UIEventListener.VoidDelegate InviteAccept = (GameObject go) =>
            {
                uint limitLv;

                string limitLvStr = GameSystem.Instance.FunctionConditionConfig.GetFuncCondition("UIQualifyingNewer").conditionParams[0];
                if (resp.match_type == MatchType.MT_QUALIFYING_NEWER)
                {
                    limitLvStr = GameSystem.Instance.FunctionConditionConfig.GetFuncCondition("UIQualifyingNewer").conditionParams[0];
                    uint.TryParse(limitLvStr, out limitLv);

                    if (MainPlayer.Instance.Level < limitLv)
                    {
                        CommonFunction.ShowTip(CommonFunction.GetConstString("STR_LEVEL_NOT_OK_TO_JOIN_QUALIFYINGNEWER"), null);
                        invitesList.Clear();
                        return;
                    }
                }
                else if (resp.match_type == MatchType.MT_PVP_3V3)
                {
                }

                FriendData.OnListChanged listChanged = null;
                listChanged = (FriendOperationType t) =>
                    {
                        JoinRoomReq joinRoomReq = new JoinRoomReq();
                        joinRoomReq.acc_id = resp.user_info.acc_id;
                        joinRoomReq.type = resp.match_type;
                        //joinRoomReq.plat_id = 
                        Debug.Log("1927 Invite accept acc_id = " + joinRoomReq.acc_id);
                        GameSystem.Instance.mNetworkManager.m_platConn.SendPack<JoinRoomReq>(0, joinRoomReq, MsgID.JoinRoomReqID);
                        invitesList.Clear();
                        FriendData.Instance.UnRegisterOnListChanged(listChanged);
                    };

                FriendData.Instance.RegisterOnListChanged(listChanged);
                FriendData.Instance.SendUpdateFriendList();

            };

        string matchStr = CommonFunction.GetConstString("STR_LADDER_MATCH");
        if (resp.match_type == MatchType.MT_QUALIFYING_NEWER)
        {
            matchStr = CommonFunction.GetConstString("STR_LABBER");
        }
        else if (resp.match_type == MatchType.MT_PVP_3V3)
        {
            matchStr = CommonFunction.GetConstString("STR_REGULAR_MATCH");
        }

        string msg = string.Format(CommonFunction.GetConstString("STR_BEINVITE_MSG"), resp.user_info.name, matchStr);
        CommonFunction.ShowPopupMsg(msg, null, InviteRefuse, InviteAccept, CommonFunction.GetConstString("STR_REFUSE"), CommonFunction.GetConstString("STR_ACCEPT"));
    }

    public void JoinRoomHandler(Pack pack)
    {
        Debug.Log("1927  Join Room RoomAction --> Handler");
        JoinRoomResp resp = Serializer.Deserialize<JoinRoomResp>(new MemoryStream(pack.buffer));
        Debug.Log("1927 resp.result= " + resp.result);

        if( (ErrorID)resp.result == ErrorID.INVITE_OUT_DUE )
        {
            CommonFunction.ShowTip(CommonFunction.GetConstString("STR_INVITE_OUT_DUE_CANNOT_JOIN_ROOM"));
            return;
        }
        if( resp.result != 0 )
        {
            string str = string.Format("Join Room Action Type:{0} Error:{1} ", resp.type, resp.result);
            CommonFunction.ShowTip(str);
            return;
        }

        MatchType type = resp.type;
        RoomInfo roomInfo = resp.info;

        Debug.Log("1927 type =" + type);
        Debug.Log("1927 roomInfo.id =" + roomInfo.id + "roomInfo.type=" + roomInfo.type + "roomInfo.master=" + roomInfo.master);

        LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();

        table.Set("joinRoomBuf", (object)new LuaStringBuffer(pack.buffer));
        table.Set("isMaster", (object)false);
        if( roomInfo.type == MatchType.MT_QUALIFYING_NEWER )
        {
            table.Set("matchType", "MT_QUALIFYING_NEWER");
        }
        else if (roomInfo.type == MatchType.MT_PVP_3V3)
        {
            table.Set("matchType", "MT_PVP_3V3");

        }
        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIQLadder", null, table });
    }

    public void GameStartHandler(Pack pack)
    {
       Debug.Log("1927  GameStartHandler");
       NotifyGameStart notifyGameStart = Serializer.Deserialize<NotifyGameStart>(new MemoryStream(pack.buffer));
       if (notifyGameStart.rejoin != 1)
       {
           Debug.Log("1927 -  not rejoin game reteurn.");
           return;
       }

       MainPlayer.Instance.inPvpJoining = true;
       LuaFunction GameStartFun = LuaScriptMgr.Instance.GetLuaFunction("Ladder.SetGameStartBuf");
       object[] parm = new object[] { new LuaStringBuffer(pack.buffer)};
       GameStartFun.Call(parm);

    }
    public void RefreshLadderInfoHandler(Pack pack)
    {
        PvpLadderInfo ladderInfo = Serializer.Deserialize<PvpLadderInfo>(new MemoryStream(pack.buffer));
        MainPlayer.Instance.pvpLadderInfo = ladderInfo;
    }

    public void HeartbeatHandle(Pack pack)
    {
        Heartbeat data = Serializer.Deserialize<Heartbeat>(new MemoryStream(pack.buffer));
        //回发消息移动到了NetworkConn363行进行收到后的立即回发
        GameSystem.mTime = (long)data.server_time + 1;
        GameSystem.Instance.ReceiveHeartbeatMsg();
        MainPlayer.Instance.onCheckUpdate();
    }

	private void OnServerConnected( NetworkConn.Type serverType )
	{
		if( serverType == NetworkConn.Type.eGameServer )
		{
			EnterGameSrv req = new EnterGameSrv();
			req.acc_id = MainPlayer.Instance.AccountID;
			if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
				return;
			GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.EnterGameSrvID);
		}
	}

	public void CreateStepInRespHandler(Pack pack)
	{
		CreateStepInResp resp = Serializer.Deserialize<CreateStepInResp>(new MemoryStream(pack.buffer));
		if (resp.result == 0)
		{
            // Never add  player here.
            //if(MainPlayer.Instance.PlayerList.Count < 1)
            if (resp.role != null)
            {
                MainPlayer.Instance.AddInviteRoleInList(resp.role);
            }
			MainPlayer.Instance.CreateStep = resp.step;
			if (resp.step == 1)
				GameSystem.Instance.isNewPlayer = true;
			else
				GameSystem.Instance.isNewPlayer = false;
		}
		else
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	}

	public void LogoutRespHandle(Pack pack)
	{
		Debug.Log("---------------------LogoutRespHandle");
		
		LogoutResp resp = Serializer.Deserialize<LogoutResp>(new MemoryStream(pack.buffer));
		ErrorID err = (ErrorID)resp.result;
		Debug.Log("result:" + err + " type:" + resp.type);
		if (err == ErrorID.LOGIN_ANOTHER_PLAYER)
		{
            // Ignore this message if server sends.
            // Now cannot login for ths second device.

            //if (!GameSystem.Instance.mNetworkManager.isReconnecting)
            //{
            //    if (!GameSystem.Instance.mNetworkManager.isPushedOut)
            //    {
            //        Debug.Log("LOGIN_ANOTHER_PLAYER");
            //        GameSystem.Instance.mNetworkManager.isPushedOut = true;
            //        GameSystem.Instance.mNetworkManager.ReconnectPrompt(CommonFunction.GetConstString("LOGIN_ANOTHER_DEVICE"));
            //    }
            //}
			return;
		}
		else if (err == ErrorID.KICK_OFFLINE)
		{
			GameSystem.Instance.mNetworkManager.isKickOut = true;
			GameSystem.Instance.mNetworkManager.DisconnectPrompt(CommonFunction.GetConstString("KICK_OFFLINE"));
			return;
		}
		else if (err == ErrorID.ERROR_CHEAT)
		{
			GameSystem.Instance.mNetworkManager.isKickOut = true;
			GameSystem.Instance.mNetworkManager.DisconnectPrompt(CommonFunction.GetConstString("KICK_CHEAT"));
			return;
		}
		else if (err != ErrorID.SUCCESS)
		{
			Debug.Log("Error -- LogoutResp returns error: " + err);
		}


		if (resp.type == 5)	// Kick out by server for no heartbeat
			return;
		
		GameSystem.Instance.mNetworkManager.Exit();
		GameSystem.Instance.mClient.Reset();

		LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
		tScene.Set("targetUI", null);
		tScene.Set("subID", null);
		tScene.Set("params", null);
		SceneManager.Instance.ChangeScene(GlobalConst.SCENE_STARTUP);
	}
	
	//Ñ¡ÔñÇòÔ±ÇëÇó»Ø¸´ÏûÏ¢´¦Àí
	public void ChooseRoleRespHandle(Pack pack)
	{
		Debug.Log("---------------------ChooseRoleRespHandle");
		
		ChooseRoleResp resp = Serializer.Deserialize<ChooseRoleResp>(new MemoryStream(pack.buffer));
		if (resp.result != 0)
		{
			Debug.Log("Error -- ChooseRoleResp returns error: " + resp.result);
			
			//TODO: add process here.
			return;
		}
	}

    public void UpdateVipInfoHandle(Pack pack)
    {
        Debug.Log("---------------------UpdateVipInfoHandle");
        UpdateVipInfo info = Serializer.Deserialize<UpdateVipInfo>(new MemoryStream(pack.buffer));
        MainPlayer.Instance.VipGifts.Clear();
        foreach (giftInfo item in info.info.gift)
        {
			MainPlayer.Instance.VipGifts.Add(item.level);
        }
        MainPlayer.Instance.VipRechargeList = info.info.recharge_rec;
        MainPlayer.Instance.VipExp = info.info.exp;
    }

    public void UpdatePlayerDataHandle(Pack pack)
    {
        Debug.Log("---------------------UpdatePlayerDataHandle");

        UpdatePlayerData dataInfo = Serializer.Deserialize<UpdatePlayerData>(new MemoryStream(pack.buffer));
        for (int i = 0; i < dataInfo.player_data.Count; ++i)
        {
            DataInfo.DataType dataType = (DataInfo.DataType)dataInfo.player_data[i].name;

            Debug.Log("UpdatePlayerDataHandle : " + dataType.ToString());
            switch (dataType)
            {
                case DataInfo.DataType.DIAMOND_FREE:
                    {
                        uint preValue = MainPlayer.Instance.DiamondFree;
                        MainPlayer.Instance.DiamondFree = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.DiamondFree > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendGoodsLog("1", "1", "DiamondFree", (int)(MainPlayer.Instance.DiamondFree - preValue), "Add Diamond");
#if IOS_SDK || ANDROID_SDK
                            TDGAVirtualCurrency.OnReward(MainPlayer.Instance.DiamondFree - preValue, "Get Diamond Free");
#endif
                        }
                        else
                        {
                            MainPlayer.Instance.SendGoodsLog("0", "1", "DiamondFree", (int)(preValue - MainPlayer.Instance.DiamondFree), "Reduce Diamond");
                        }
                    }
                    break;
                case DataInfo.DataType.GOLD:
                    {
                        uint preValue = MainPlayer.Instance.Gold;
                        MainPlayer.Instance.Gold = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Gold > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendGoodsLog("1", "2", "Gold", (int)(MainPlayer.Instance.Gold - preValue), "Add Gold");
#if IOS_SDK || ANDROID_SDK
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("GetGold", MainPlayer.Instance.Gold - preValue);

                    TalkingDataGA.OnEvent("GetGold", dic);
#endif
                        }
                        else
                        {
                            MainPlayer.Instance.SendGoodsLog("0", "2", "Gold", (int)(preValue - MainPlayer.Instance.Gold), "Reduce Gold");
                        }
                    }
                    break;
                case DataInfo.DataType.HONOR:
                    {
                        uint preValue = MainPlayer.Instance.Honor;
                        MainPlayer.Instance.Honor = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Honor > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Honor", MainPlayer.Instance.Honor.ToString(), ((int)(MainPlayer.Instance.Honor - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Honor", MainPlayer.Instance.Honor.ToString(), (((int)(preValue - MainPlayer.Instance.Honor)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.HONOR2:
                    {
                        uint preValue = MainPlayer.Instance.Honor2;
                        MainPlayer.Instance.Honor2 = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Honor2 > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Honor", MainPlayer.Instance.Honor2.ToString(), ((int)(MainPlayer.Instance.Honor2 - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Honor", MainPlayer.Instance.Honor2.ToString(), (((int)(preValue - MainPlayer.Instance.Honor2)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.HP:
                    {
                        uint preValue = MainPlayer.Instance.Hp;
                        MainPlayer.Instance.Hp = (uint)dataInfo.player_data[i].value_int;
                        MainPlayer.Instance.HpRestoreTime = DateTime.Now + new TimeSpan(0, 0, (int)GameSystem.Instance.CommonConfig.GetUInt("gHpRestoreTime"));
                        bool isAdd = MainPlayer.Instance.Hp > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Hp", MainPlayer.Instance.Hp.ToString(), ((int)(MainPlayer.Instance.Hp - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Hp", MainPlayer.Instance.Hp.ToString(), (((int)(preValue - MainPlayer.Instance.Hp)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.PRESTIGE:  //
                    {
                        uint preValue = MainPlayer.Instance.Prestige;
                        MainPlayer.Instance.Prestige = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Prestige > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Prestige", MainPlayer.Instance.Prestige.ToString(), ((int)(MainPlayer.Instance.Prestige - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Prestige", MainPlayer.Instance.Prestige.ToString(), (((int)(preValue - MainPlayer.Instance.Prestige)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.PRESTIGE2:
                    {
                        uint preValue = MainPlayer.Instance.Prestige2;
                        MainPlayer.Instance.Prestige2 = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Prestige2 > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Prestige", MainPlayer.Instance.Prestige2.ToString(), ((int)(MainPlayer.Instance.Prestige2 - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Prestige", MainPlayer.Instance.Prestige2.ToString(), (((int)(preValue - MainPlayer.Instance.Prestige2)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.REPUTATION:
                    {
                        uint preValue = MainPlayer.Instance.Reputation;
                        MainPlayer.Instance.Reputation = (uint)dataInfo.player_data[i].value_int;
                        bool isAdd = MainPlayer.Instance.Reputation > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendPlayerLog("Reputation", MainPlayer.Instance.Reputation.ToString(), ((int)(MainPlayer.Instance.Reputation - preValue)).ToString());
                        }
                        else
                        {
                            MainPlayer.Instance.SendPlayerLog("Reputation", MainPlayer.Instance.Reputation.ToString(), (((int)(preValue - MainPlayer.Instance.Reputation)) * -1).ToString());
                        }
                    }
                    break;
                case DataInfo.DataType.EXP:
                    {
                        uint preValue = MainPlayer.Instance.Exp;
                        LuaScriptMgr.Instance.GetLuaTable("_G").Set("PreExp", (object)preValue);
                        MainPlayer.Instance.Exp = (uint)dataInfo.player_data[i].value_int;
                        MainPlayer.Instance.SendPlayerLog("Exp", MainPlayer.Instance.Exp.ToString(), (MainPlayer.Instance.Exp - preValue).ToString());
                    }
                    break;
                case DataInfo.DataType.LEVEL:
                    {
                        uint preValue = MainPlayer.Instance.Level;
                        MainPlayer.Instance.Level = (uint)dataInfo.player_data[i].value_int;
                        GameSystem.Instance.mClient.mUIManager.showTeamUpgrade = true;
                        MainPlayer.Instance.SendPlayerLog("Level", MainPlayer.Instance.Level.ToString(), (MainPlayer.Instance.Level - preValue).ToString());
                        UpdateSkillTipByLevel();
                    }
                    break;
                case DataInfo.DataType.VIP:
                    {
                        uint preValue = MainPlayer.Instance.Vip;
                        MainPlayer.Instance.Vip = (uint)dataInfo.player_data[i].value_int;
                        MainPlayer.Instance.SendPlayerLog("Vip", MainPlayer.Instance.Vip.ToString(), (MainPlayer.Instance.Vip - preValue).ToString());
                    }
                    break;
                case DataInfo.DataType.DIAMOND_BUY:
                    {
                        uint preValue = MainPlayer.Instance.DiamondBuy;
                        MainPlayer.Instance.DiamondBuy = (uint)dataInfo.player_data[i].value_int;

                        bool isAdd = MainPlayer.Instance.DiamondBuy > preValue;
                        if (isAdd)
                        {
                            MainPlayer.Instance.SendGoodsLog("1", "1", "DiamondBuy", (int)(MainPlayer.Instance.DiamondBuy - preValue), "Add Diamond Buy");
#if IOS_SDK || ANDROID_SDK
                    TDGAVirtualCurrency.OnReward(MainPlayer.Instance.DiamondBuy - preValue, "Get Diamond Buy");
#endif
                        }
                        else
                        {
                            MainPlayer.Instance.SendGoodsLog("0", "1", "DiamondBuy", (int)(preValue - MainPlayer.Instance.DiamondBuy), "Reduce Diamond Buy");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

	//¸üÐÂÎïÆ·ÐÅÏ¢
	public void UpdateGoodsHandle(Pack pack)
	{
		Debug.Log("---------------------UpdateGoodsHandle");
		
		UpdateGoods msg = Serializer.Deserialize<UpdateGoods>(new MemoryStream(pack.buffer));
        bool operaGoods = false; uint newFashionType = 0;
		for (int i = 0; i < msg.goods_list.Count; ++i)
		{
			UpdateGoodsInfo info = msg.goods_list[i];
			
			switch (info.type)
			{
			case GoodsUpdateType.GUT_NEW:
			{
				Goods goods = new Goods();
				goods.Init(info.data);
				MainPlayer.Instance.AddGoods(goods);
                MainPlayer.Instance.SendGoodsLog("1", goods.GetID().ToString(), goods.GetName(),(int)goods.GetNum(), "add");
                //if ((goods.GetCategory() == GoodsCategory.GC_EQUIPMENT && goods.GetSubCategory() != EquipmentType.ET_EQUIPMENTPIECE) ||
                //    (goods.GetCategory() == GoodsCategory.GC_CONSUME && ((int)goods.GetSubCategory() == 3 || (int)goods.GetSubCategory() == 5)) ||
                //    (goods.GetCategory() == GoodsCategory.GC_FAVORITE))
                //    operaGoods = true;
                if (goods.GetCategory() == GoodsCategory.GC_CONSUME && (int)goods.GetSubCategory() == 2)
                    operaGoods = true;
                if (goods.GetCategory() == GoodsCategory.GC_FASHION)
                    newFashionType = (goods.GetFashionSubCategory())[0];
			}
				break;
			case GoodsUpdateType.GUT_UPDATE:
			{
				Goods goods = MainPlayer.Instance.GetGoods(info.data.category, info.data.uuid);
                int origNum = 0;
                if( goods != null )
                {
                    origNum = (int)goods.GetNum();   
                }
				if (goods == null)
				{
					goods = new Goods();
				}
				goods.Init(info.data);
                int nowNum = (int)goods.GetNum();
                if( nowNum > origNum )
                {
                    MainPlayer.Instance.SendGoodsLog("1", goods.GetID().ToString(), goods.GetName(),(int)(nowNum - origNum), "add");
                }
                else if( nowNum < origNum )
                {
                    MainPlayer.Instance.SendGoodsLog("0", goods.GetID().ToString(), goods.GetName(), (int)(origNum - nowNum), "delete");
                }

                //if ((goods.GetCategory() == GoodsCategory.GC_EQUIPMENT && goods.GetSubCategory() != EquipmentType.ET_EQUIPMENTPIECE) ||
                //    (goods.GetCategory() == GoodsCategory.GC_CONSUME && ((int)goods.GetSubCategory() == 3 || (int)goods.GetSubCategory() == 5)) ||
                //    (goods.GetCategory() == GoodsCategory.GC_FAVORITE))
                //    operaGoods = true;
			}
				break;
			case GoodsUpdateType.GUT_DELETE:
			{
                Goods goods = MainPlayer.Instance.GetGoods(info.data.category, info.data.uuid);
                MainPlayer.Instance.SendGoodsLog("0", goods.GetID().ToString(), goods.GetName(), (int)goods.GetNum(), "delete");
				MainPlayer.Instance.DelGoods(info.data.category, info.data.uuid);

                //if ((goods.GetCategory() == GoodsCategory.GC_EQUIPMENT && goods.GetSubCategory() != EquipmentType.ET_EQUIPMENTPIECE) ||
                //    (goods.GetCategory() == GoodsCategory.GC_CONSUME && ((int)goods.GetSubCategory() == 3 || (int)goods.GetSubCategory() == 5)) ||
                //    (goods.GetCategory() == GoodsCategory.GC_FAVORITE))
                //    operaGoods = true;
			}
				break;
			}
		}

        ////更新物品后刷新小红点
        //LuaScriptMgr.Instance.CallLuaFunction("ButtonMenu.Refresh");
        ////更新装备物品后刷新小红点
        if (operaGoods)
        {
            LuaScriptMgr.Instance.GetLuaTable("_G").Set("NeedGetGift", true);
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Package");
            //LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Role");
            //LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Squad");
            //LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "RoleDetail");
        }
        if (newFashionType != 0) 
        {
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.OperateFashionTip", "Add", newFashionType);
            //LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Fashion");
        }
	}

    public void UpdateSkillTipByLevel()
    {
        uint level = MainPlayer.Instance.Level;
        List<StoreGoodsData> datas = GameSystem.Instance.StoreGoodsConfigData.GetStoreGoodsDataList(2);

        foreach(var data in datas)
        {
            if( data.apply_min_level == level )
            {
                SkillAttr sAttr = GameSystem.Instance.SkillConfig.GetSkill((uint)data.store_good_id);

                uint atype = sAttr.action_type;
                int skillType = 0;

                if(atype == 1 || atype == 2) 
                {
                    skillType = 1; // 投篮
                }
                else if( atype == 3)
                {
                    skillType = 2;  // 扣篮
                }
                else if( atype == 6)
                {
                    skillType = 3;  // 突破
                }
                else if( atype == 5)
                {
                    skillType = 4; // 篮板
                }
                else if( atype == 4)
                {
                    skillType = 5; // 盖帽
                }
                else if( atype == 7 )
                {
                    skillType = 6; // 传球
                }

                if (skillType != 0)
                {
                    LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.OperateSkillTip", "Add", skillType);
                }
            }
        }




    }
	
	//¹Ø¿¨±ÈÈü¿ªÊ¼ÇëÇó»Ø¸´ÏûÏ¢´¦Àí
	public void StartSectionMatchRespHandle(Pack pack)
	{
		Debug.Log("---------------------StartSectionRespHandle");
		
		CommonFunction.HideWaitMask();
		StartSectionMatchResp resp = Serializer.Deserialize<StartSectionMatchResp>(new MemoryStream(pack.buffer));
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS)
		{
			//GameSystem.Instance.mClient.mUIManager.CareerCtrl.SectionStart(resp.session_id);
		}
		else
		{
			Debug.Log("Error -- StartSectionMatchResp returns error: " + resp.result);
			CommonFunction.ShowErrorMsg(err);
		}
	}
	
	//¹Ø¿¨±ÈÈü½áÊøÇëÇó»Ø¸´ÏûÏ¢´¦Àí
	//public void EndSectionMatchRespHandle(Pack pack)
	//{
	//    Debug.Log("---------------------EndSectionMatchRespHandle");
	
	//    EndSectionMatchResp resp = Serializer.Deserialize<EndSectionMatchResp>(new MemoryStream(pack.buffer));
	//    Debug.Log("----result " + resp.result);
	//    if (resp.result == 0)
	//    {
	//        MainPlayer.Instance.ChangeChaptersData(resp.chapters);
	//    }
	
	//    if (GameSystem.Instance.mClient.mCurMatch != null)
	//    {
	//        MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
	//        match.SectionCompleteHandler(resp);
	//    }
	//}
	
	public void EndSectionMatchRespHandle(EndSectionMatchResp resp)
	{
		Debug.Log("----result " + resp.result);
		if (resp.result == 0)
		{
			MainPlayer.Instance.ChangeChaptersData(resp.chapters);
		}
		
		if (GameSystem.Instance.mClient.mCurMatch != null)
		{
			MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
			match.SectionCompleteHandler(resp);
		}
	}

    public void EndEndPracticePveRespHandle(EndPracticePveResp resp)
    {
        Debug.Log("----result " + resp.result);

 
        if (GameSystem.Instance.mClient.mCurMatch != null)
        {
            MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
            match.PracticePveCompleteHandler(resp);
        }
    }
    
    //¹Ø¿¨É¨µ´ÇëÇó»Ø¸´ÏûÏ¢´¦Àí
    public void SweepSectionRespHandle(Pack pack)
	{
		Debug.Log("---------------------SweepSectionRespHandle");
		
		SweepSectionResp resp = Serializer.Deserialize<SweepSectionResp>(new MemoryStream(pack.buffer));
		if (resp.result == 0 || resp.result == 314 || resp.result == 315)
		{
			MainPlayer.Instance.ChangeChaptersData(resp.chapters);
		}
	}

	private void EnterGameRespHandler(Pack pack)
	{
	EnterGameResp resp = Serializer.Deserialize<EnterGameResp>(new MemoryStream(pack.buffer));

		ErrorID err = (ErrorID)resp.result;
		if (resp.type == MatchType.MT_QUALIFYING)	//只处理排位赛
		{
			if (err == ErrorID.SUCCESS)
			{
                GameSystem.Instance.mClient.CreateNewQualifyingMatch(resp.qualifying.session_id, resp.qualifying.fight_list.fighters, resp.qualifying.rival_info);
			}
			else
			{
				CommonFunction.ShowErrorMsg(err);
			}
		}
		else
		{
			GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
			if (curMatch != null)	// In match
			{
				if (err == ErrorID.SUCCESS)
				{
					ulong session_id = 0;
					ExitGameReq cachedExitGameReq = PlatNetwork.Instance.cachedExitGameReq;
					if (resp.type == MatchType.MT_CAREER)
					{
						session_id = resp.career.session_id;
						if (cachedExitGameReq != null)
                        {
							cachedExitGameReq.career.session_id = session_id;
                        }
					}
					else if (resp.type == MatchType.MT_PRACTICE)
					{
						session_id = resp.practice.session_id;
						if (cachedExitGameReq != null)
							cachedExitGameReq.practice.session_id = session_id;
					}
					else if (resp.type == MatchType.MT_BULLFIGHT)
					{
						session_id = resp.bull_fight.session_id;
						if (cachedExitGameReq != null)
							cachedExitGameReq.bull_fight.session_id = session_id;
					}
					else if (resp.type == MatchType.MT_SHOOT)
					{
						session_id = resp.shoot.session_id;
						if (cachedExitGameReq != null)
							cachedExitGameReq.shoot.session_id = session_id;
					}
					else if (resp.type == MatchType.MT_TOUR)
					{
						session_id = resp.tour.session_id;
						if (cachedExitGameReq != null)
							cachedExitGameReq.tour.session_id = session_id;
					}
					else if (resp.type == MatchType.MT_QUALIFYING_NEW)
					{
						session_id = resp.new_qualifying.session_id;
						if (cachedExitGameReq != null)
							cachedExitGameReq.qualifying_new.session_id = session_id;
					}
                    else if (resp.type == MatchType.MT_PRACTICE_1V1)
                    {
                        session_id = resp.practice_pve.session_id;
                        if (cachedExitGameReq != null)
                            cachedExitGameReq.regular.session_id = session_id;
                    }

                    curMatch.GetConfig().session_id = session_id;

					if (cachedExitGameReq != null)
					{
						GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, cachedExitGameReq, MsgID.ExitGameReqID);
						PlatNetwork.Instance.cachedExitGameReq = null;
					}
				}
				else
				{
					CommonFunction.ShowErrorMsg(err);
				}
			}
		}
	}
	
	private void ExitGameRespHandler(Pack pack)
	{
		Debug.Log("---------------------ExitGameRespHandler");

		GameSystem.Instance.mNetworkManager.CloseGameServerConn();
		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		if( mgr.m_gameConn != null )
		{
			mgr.m_gameMsgHandler.UnregisterHandler(MsgID.InstructionBroadcastID, GameMatch.HandleBroadcast);
		}
		ExitGameResp resp = Serializer.Deserialize<ExitGameResp>(new MemoryStream(pack.buffer));
		if (resp.type == MatchType.MT_PRACTICE)
		{
            if ((ErrorID)resp.practice.result != ErrorID.SUCCESS)
            {
                CommonFunction.ShowErrorMsg((ErrorID)resp.practice.result);
                return;
            }

            uint practiceID = resp.practice.id;
			if (resp.exit_type == ExitMatchType.EMT_END && GameSystem.Instance.PractiseConfig.GetConfig(practiceID).is_activity == 1)
			{
				MainPlayer.Instance.SetPractiseCompleted(practiceID, true);
				MainPlayer.Instance.practice_cd = resp.practice.time;

				if (LoginIDManager.GetFirstPractice(practiceID) != 1)
					LoginIDManager.SetFirstPractice(practiceID);


				uint id = 0, num = 0;
				foreach (KeyValueData goods in resp.practice.awards)
				{
					id = goods.id;
					num = goods.value;
				}
				if (num > 0)
				{
					GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GoodsAcquirePopup") as GameObject;
					LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
					luaCom.table.Set("jumpToPractise", (object)true);
					LuaScriptMgr.Instance.CallLuaFunction("GoodsAcquirePopup.SetGoodsData", luaCom.table, id, num);
				}
				else
                {
                    LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
                    table.Set("nextShowUI", (object)"UIPracticeCourt");

                    LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] {
						GameMatch.LeagueType.ePractise,null,table
					});
                }
			}
			else
			{
                LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
                table.Set("nextShowUI", (object)"UIPracticeCourt");

                MainPlayer.Instance.practice_cd = resp.practice.time;
                LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] {
					GameMatch.LeagueType.ePractise,null,table
				});
			}
		}
        else if (resp.type == MatchType.MT_CAREER)
        {

            if (resp.exit_type == ExitMatchType.EMT_END)
            {

                EndSectionMatchRespHandle(resp.career);
                return;
            }
            object obj = LuaScriptMgr.Instance.GetLuaTable("_G")["CurChapterID"];
            uint curChapterID = (uint)(double)obj;
            //uint subID = curChapterID % 10000;
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eCareer, curChapterID);
        }
        else if (resp.type == MatchType.MT_PRACTICE_1V1)
		{
			if (resp.exit_type == ExitMatchType.EMT_END)
			{
                EndEndPracticePveRespHandle(resp.practice_pve);
				return;
			}
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.ePractise1vs1);
		}
		else if (resp.type == MatchType.MT_TOUR)
		{
			if (resp.tour.direct_clear != 1)
			{
				if (GameSystem.Instance.mClient.mCurMatch != null)
				{
					MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
					match.TourCompleteHandler(resp.tour);
				}
				return;
			}
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eTour);
		}
		else if (resp.type == MatchType.MT_QUALIFYING)
		{
            MainPlayer.Instance.QualifyingInfo.run_times = resp.qualifying.run_times;
            MainPlayer.Instance.QualifyingInfo.battle_time = resp.qualifying.battle_time;
            //MainPlayer.Instance.QualifyingInfo.max_ranking = resp.qualifying.max_ranking;
            //MainPlayer.Instance.QualifyingRanking = resp.qualifying.cur_ranking;
            if (resp.exit_type == ExitMatchType.EMT_END)
            {
                if (GameSystem.Instance.mClient.mCurMatch != null)
                {
                    MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
                    match.QualifyingCompleteHandler(resp.qualifying);
                }
                return;
            }
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eQualifying);
		}
		else if (resp.type == MatchType.MT_BULLFIGHT)
		{

			if (resp.exit_type == ExitMatchType.EMT_END)
			{
				if (GameSystem.Instance.mClient.mCurMatch != null)
				{
					MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
					match.BullFightCompleteHandler(resp.bull_fight);
				}
				return;
			}
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eBullFight);
		}
		else if (resp.type == MatchType.MT_SHOOT)
		{

			if (resp.exit_type == ExitMatchType.EMT_END)
			{
				if (GameSystem.Instance.mClient.mCurMatch != null)
				{
					MatchStateOver match = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState as MatchStateOver;
					match.ShootCompleteHandler(resp.shoot);
				}
				return;
			}
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eShoot);
		}
		else if (resp.type == MatchType.MT_REGULAR)
		{
			if ((ErrorID)resp.honorCompetition.result != ErrorID.SUCCESS)
			{
				CommonFunction.ShowErrorMsg((ErrorID)resp.honorCompetition.result);
				return;
			}
			//LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.eAsynPVP);
		}
		else if (resp.type == MatchType.MT_REGULAR_RACE)
		{
			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				if( curMatch.m_bOverTime )
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
				}
				else
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
					else
						curMatch.m_stateMachine.SetState(MatchState.State.eOver);
				}
					

				MatchStateOver match = curMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
				match.RegularCompleteHandler(resp.regular,resp.max_income,resp.assist_awards,resp.shiwakan_percent,resp.assist_first_win_times,resp.assist_num);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Debug.Log("Self exit");
				//totalTimes And winTimes add one
				MainPlayer.Instance.pvp_regular.race_times += 1;
				Debug.Log("race time:" + MainPlayer.Instance.pvp_regular.race_times);
				MainPlayer.Instance.pvp_regular.score = resp.regular.score;
				Debug.Log("score:" + MainPlayer.Instance.pvp_regular.score);
				
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
						matchOver.RegularCompleteHandler(resp.regular,resp.max_income,resp.assist_awards,resp.shiwakan_percent,resp.assist_first_win_times,resp.assist_num);
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
			//qualifying.ranking = resp.qualifying_new.ranking;
			qualifying.race_times = resp.qualifying_new.race_times;
			qualifying.win_times = resp.qualifying_new.win_times;
			qualifying.winning_streak = resp.qualifying_new.winning_streak;
			qualifying.max_winning_streak = resp.qualifying_new.max_winning_streak;
			qualifying.grade_awards.Clear();
			qualifying.grade_awards.AddRange(resp.qualifying_new.grade_awards);
//			foreach(uint it in resp.qualifying_new.grade_awards)
//			{
//				qualifying.grade_awards.Add(it);
//			}
			Debug.Log("QualifyingNew score:" + resp.qualifying_new.score + " ranking:" + resp.qualifying_new.ranking);

			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				if( curMatch is GameMatch_PVP )
				{
					GameMatch_PVP pvp = curMatch as GameMatch_PVP;
					pvp.m_overTimer.stop = false;
				}
				else
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
					else
						curMatch.m_stateMachine.SetState(MatchState.State.eOver);
				}
				
				MatchStateOver match = curMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
				match.QualifyingNewCompleteHandler(resp.qualifying_new, scoreDelta,resp.max_income);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Debug.Log("Self exit");
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
						matchOver.QualifyingNewCompleteHandler(resp.qualifying_new, scoreDelta, resp.max_income);
					};
					CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.qualifying_new.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
				}
			}
		}
		else if (resp.type == MatchType.MT_PVP_1V1_PLUS)
		{
			if( resp.exit_type == ExitMatchType.EMT_END )
			{
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				if( curMatch is GameMatch_PVP )
				{
					GameMatch_PVP pvp = curMatch as GameMatch_PVP;
					pvp.m_overTimer.stop = false;
				}
				else
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
					else
						curMatch.m_stateMachine.SetState(MatchState.State.eOver);
				}
				
				MatchStateOver match = curMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
				match.ChallengeCompleteHandler(resp.challenge_plus);
			}
			else if( resp.exit_type == ExitMatchType.EMT_OPTION )
			{
				Debug.Log("Self exit");
				//totalTimes And winTimes add one
				MainPlayer.Instance.PvpPlusInfo.race_times += 1;
				Debug.Log("race time:" + MainPlayer.Instance.PvpPlusInfo.race_times);
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
			Debug.Log("pvp data costs: " + GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.m_dataUsage );
			GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.EndRecordDataUsage();
		}
		else if (resp.type == MatchType.MT_PVP_3V3)
		{
			if (resp.exit_type == ExitMatchType.EMT_END)
			{
                if (MainPlayer.Instance.inPvpJoining)
                {
                    UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
                    if (goLoading != null)
                    {
                        goLoading.disConnected = true;
                        return;
                    }
                }
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				if( curMatch is GameMatch_PVP )
				{
					GameMatch_PVP pvp = curMatch as GameMatch_PVP;
					pvp.m_overTimer.stop = false;
				}
				else
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
					else
						curMatch.m_stateMachine.SetState(MatchState.State.eOver);
				}
				
				MatchStateOver match = curMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
				match.ChallengeExCompleteHandler(resp.challenge_ex,resp.max_income);
			}
			else if (resp.exit_type == ExitMatchType.EMT_OPTION)
			{
				Debug.Log("Self exit");
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
						matchOver.ChallengeExCompleteHandler(resp.challenge_ex,resp.max_income);
					};
					CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.challenge_ex.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
				}
			}
            if(GameSystem.Instance.mNetworkManager.m_gameConn != null
                && GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler != null)
            {
                Debug.Log("pvp data costs: " + GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.m_dataUsage);
                GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.EndRecordDataUsage();
            }
            else
            {
                Debug.LogWarning("m_profiler is null, please check");
            }
		}		
		else if (resp.type == MatchType.MT_QUALIFYING_NEWER)
		{
			if (resp.exit_type == ExitMatchType.EMT_END)
			{
                if (MainPlayer.Instance.inPvpJoining)
                {
                    UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
                    if (goLoading != null)
                    {
                        goLoading.disConnected = true;
                        return;
                    }
                }
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				if( curMatch is GameMatch_PVP )
				{
					GameMatch_PVP pvp = curMatch as GameMatch_PVP;
					pvp.m_overTimer.stop = false;
				}
				else
				{
					if( curMatch is GameMatch_PVP )
					{
						GameMatch_PVP pvp = curMatch as GameMatch_PVP;
						pvp.m_overTimer.stop = false;
					}
					else
						curMatch.m_stateMachine.SetState(MatchState.State.eOver);
				}
				
				MatchStateOver match = curMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
				match.QualifyingNewerCompleteHandler(resp.qualifying_newer,resp.max_income,resp.assist_awards,resp.shiwakan_percent,resp.assist_first_win_times,resp.assist_num);
			}
			else if (resp.exit_type == ExitMatchType.EMT_OPTION)
			{
				Debug.Log("Self exit");
			}
			else
			{
				UIChallengeLoading goLoading = UIManager.Instance.m_uiRootBasePanel.GetComponentInChildren<UIChallengeLoading>();
				if (goLoading != null)
				{
					goLoading.GetComponent<UIChallengeLoading>().disConnected = true;
					goLoading.GetComponent<UIChallengeLoading>().qualifyingNewerResp = resp.qualifying_newer;
				}
				else
				{
					UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
					{
						GameSystem.Instance.mClient.mCurMatch.m_stateMachine.SetState(MatchState.State.eOver);
						MatchStateOver matchOver = GameSystem.Instance.mClient.mCurMatch.m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
						matchOver.QualifyingNewerCompleteHandler(resp.qualifying_newer,resp.max_income,resp.assist_awards,resp.shiwakan_percent,resp.assist_first_win_times,resp.assist_num);
					};
					CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), resp.qualifying_newer.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
				}
			}
            if(GameSystem.Instance.mNetworkManager.m_gameConn != null
                && GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler != null)
            {
                Debug.Log("pvp data costs: " + GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.m_dataUsage);
                GameSystem.Instance.mNetworkManager.m_gameConn.m_profiler.EndRecordDataUsage();
            }
            else
            {
                Debug.LogWarning("m_profiler is null, please check");
            }
		}		
		else
		{
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", UIManager.Instance.curLeagueType);
		}
	}
	
	//ÁìÈ¡ÕÂ½ÚÐÇ¼¶½±ÀøÇëÇó»Ø¸´ÏûÏ¢´¦Àí
	public void GetChapterStarAwardRespHandle(Pack pack)
	{
		Debug.Log("---------------------GetChapterStarAwardRespHandle");
		
		GetChapterStarAwardResp resp = Serializer.Deserialize<GetChapterStarAwardResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.CareerCtrl.GetStarAwardResp(resp);
	}
	
	// Ìí¼ÓÐÂµÄÕÂ½ÚÊý¾Ý
	public void AddNewChapterHandler(Pack pack)
	{
		AddNewChapter resp = Serializer.Deserialize<AddNewChapter>(new MemoryStream(pack.buffer));
		MainPlayer.Instance.AddChaptersData(resp.chapter);
	}
	
	
	//Áõ¼ÑÁ¦ 10-28
	
	/// <summary>
	/// ÇòÔ±ÑûÇë·þÎñÆ÷·µ»ØÏûÏ¢
	/// </summary>
	/// <param name="go"></param>
	public void InvitePlayerRespHandler(Pack pack)
    {
        Debug.Log("---------------------InvitePlayerRespHandler");
		
		InviteRoleResp resp = Serializer.Deserialize<InviteRoleResp>(new MemoryStream(pack.buffer));
		
		// Handel the data in csharp module, and update ui in lua module.
		if ((ErrorID)resp.result != ErrorID.SUCCESS)
		{
			//ÌáÊ¾²Ù×÷Ê§°Ü
			string strError = ((ErrorID)resp.result).ToString();
            CommonFunction.ShowErrorMsg((ErrorID)(resp.result));
			return;
		}
        //bool isInClude = MainPlayer.Instance.HasRole(resp.role.id);
        //List<uint> list = MainPlayer.Instance.GetRoleIDList();

		MainPlayer.Instance.AddInviteRoleInList(resp.role);
        
        ////如果组成了新图鉴
        //if (MainPlayer.Instance.NewMapIDList.Count > 0)
        //{
        //    foreach (uint id in MainPlayer.Instance.NewMapIDList)
        //    {
        //        GameObject goodsAcquire = UIManager.Instance.CreateUI("GoodsAcquirePopup");
        //        LuaComponent luaComponent = goodsAcquire.GetComponent<LuaComponent>();
        //        LuaFunction function = luaComponent.table["SetNewMapData"] as LuaFunction;
        //        function.Call(new object[] { luaComponent.table, id });
        //        UIManager.Instance.BringPanelForward(goodsAcquire);
        //    }
        //}
        object isMap = LuaScriptMgr.Instance.GetLuaTable("_G")["IsRefreshMap"];
        if ((uint)((double)isMap) <= 1)
            LuaScriptMgr.Instance.GetLuaTable("_G").Set("IsRefreshMap", 1);
        Transform newSignTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UINewSign(Clone)");
        Transform vipPopupTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("VipPopup(Clone)");
        if (newSignTrans && newSignTrans.gameObject.activeSelf == true)
        {
            LuaComponent memberLua = newSignTrans.GetComponent<LuaComponent>();
            LuaTable table = memberLua.table;
            object o = table["InvitePlayerResp"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }
        else if (vipPopupTrans && vipPopupTrans.gameObject.activeSelf == true)
        {
            LuaComponent memberLua = vipPopupTrans.GetComponent<LuaComponent>();
            LuaTable table = memberLua.table;
            object o = table["InvitePlayerResp"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }

        #region 以前的获得球员(注释掉)
        //if (list.Count >= 3)
        //{
        //    Transform memberTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UIRole(Clone)");
        //    Transform lotteryTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UILottery(Clone)");
        //    Transform signTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UISign(Clone)");
        //    Transform newSignTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UINewSign(Clone)");
        //    Transform treasureBoxTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("TreasureBox(Clone)");
        //    if (memberTrans && memberTrans.gameObject.activeSelf == true)
        //    {
        //        GameObject roleAcquire = CommonFunction.InstantiateObject("Prefab/GUI/RoleAcquirePopup", memberTrans);
        //        LuaComponent roleAcquireLua = roleAcquire.GetComponent<LuaComponent>();
        //        LuaTable roleAcquireTable = roleAcquireLua.table;
        //        roleAcquireTable.Set("IsInClude", isInClude);
        //        uint goodsID = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(resp.role.id).recruit_output_id;
        //        uint goodsNum = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(resp.role.id).recruit_output_value;
        //        string goodsName = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(goodsID).name;
        //        roleAcquireTable.Set("contentStr", string.Format(CommonFunction.GetConstString("STR_ROLE_RECTUIT_AWARDS"), goodsName, goodsNum));
        //        LuaFunction func = roleAcquireTable["SetData"] as LuaFunction;
        //        func.Call(new object[] { roleAcquireTable, resp.role.id });
        //        UIManager.Instance.BringPanelForward(roleAcquire);

        //        LuaComponent memberLua = memberTrans.GetComponent<LuaComponent>();
        //        LuaTable table = memberLua.table;
        //        object o = table["OnInviteRespFromCsharp"];
        //        LuaFunction fun = o as LuaFunction;
        //        fun.Call(table, resp);
        //    }
        //    if (lotteryTrans && lotteryTrans.gameObject.activeSelf == true)
        //    {
        //        LuaComponent lotteryLua = lotteryTrans.GetComponent<LuaComponent>();
        //        LuaTable table = lotteryLua.table;
        //        LuaFunction func = table["GetNewRole"] as LuaFunction;
        //        func.Call(new object[] { table, resp.role.id });
        //    }
        //    if (signTrans && signTrans.gameObject.activeSelf == true)
        //    {
        //        LuaComponent signLua = signTrans.GetComponent<LuaComponent>();
        //        LuaTable table = signLua.table;
        //        LuaFunction func = table["GetNewRole"] as LuaFunction;
        //        func.Call(new object[] { table, resp.role.id });
        //    }
        //    if (newSignTrans && newSignTrans.gameObject.activeSelf == true)
        //    {
        //        LuaComponent signLua = newSignTrans.GetComponent<LuaComponent>();
        //        LuaTable table = signLua.table;
        //        LuaFunction func = table["GetNewRole"] as LuaFunction;
        //        func.Call(new object[] { table, resp.role.id });
        //    }
        //    if (treasureBoxTrans && treasureBoxTrans.gameObject.activeSelf == true)
        //    {
        //        LuaComponent signLua = treasureBoxTrans.GetComponent<LuaComponent>();
        //        LuaTable table = signLua.table;
        //        LuaFunction func = table["GetNewRole"] as LuaFunction;
        //        func.Call(new object[] { table, resp.role.id });
        //    }
        //}
        #endregion




        //GameSystem.Instance.mClient.mUIManager.MainCtrl._ui.teamHandler.OnInviteRoleResp(resp);
	}
	/// <summary>
	/// ÌáÉýÆ·½×·þÎñÆ÷·µ»ØÏûÏ¢
	/// </summary>
	public void ImproveQualityRespHandler(Pack pack)
	{
		ImproveQualityResp resp = Serializer.Deserialize<ImproveQualityResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.MainCtrl._ui.teamHandler.OnImproveQualityResp(resp);
	}
	/// <summary>
	/// ´ò¿ªÉÌµê·þÎñÆ÷·µ»ØÏûÏ¢
	/// </summary>
	/// <param name="pack"></param>
	public void OpenStoreRespHandler(Pack pack)
	{
		//OpenStoreResp openStoreResp = Serializer.Deserialize<OpenStoreResp>(new MemoryStream(pack.buffer));
		//Debug.Log("-----openstoreResp()");
		//GameSystem.Instance.mClient.mUIManager.StoreCtrl.GetOpenStoreResp(openStoreResp);
		//GameSystem.Instance.mClient.mUIManager.StoreCtrl.ShowUIForm();
	}
	
	/// <summary>
	/// Ë¢ÐÂÉÌµê·µ»ØÏûÏ¢
	/// </summary>
	/// <param name="pack"></param>
	public void RefreshStoreGoodsRespHandler(Pack pack)
	{
		RefreshStoreGoodsResp resp = Serializer.Deserialize<RefreshStoreGoodsResp>(new MemoryStream(pack.buffer));
		if ((ErrorID)resp.result != ErrorID.SUCCESS)
		{
			//ÌáÊ¾²Ù×÷Ê§°Ü
			string strError = ((ErrorID)resp.result).ToString();
			CommonFunction.ShowTip(CommonFunction.GetConstString(strError), GameSystem.Instance.mClient.mUIManager.GetActiveForm().transform);
			return;
		}
		
		//if (resp.oper_type == StoreRefreshType.SRT_OPEN)
		//GameSystem.Instance.mClient.mUIManager.MainCtrl._ui.storeHandler.OpenStoreRespProc(resp);
		//else if (GameSystem.Instance.mClient.mUIManager.StoreCtrl._ui != null)
		//GameSystem.Instance.mClient.mUIManager.StoreCtrl.OnRefreshStoreGoodsResp(resp);
	}
	
	/// <summary>
	/// ¹ºÂòÉÌµêÎïÆ··µ»ØÏûÏ¢
	/// </summary>
	/// <param name="pack"></param>
	public void BuyStoreGoodsRespHandler(Pack pack)
	{
        //BuyStoreGoodsResp resp = Serializer.Deserialize<BuyStoreGoodsResp>(new MemoryStream(pack.buffer));
		
        //if ((ErrorID)resp.result != ErrorID.SUCCESS)
        //{
        //    //ÌáÊ¾²Ù×÷Ê§°Ü
        //    string strError = ((ErrorID)resp.result).ToString();
        //    //CommonFunction.ShowTip(CommonFunction.GetConstString(strError));
        //    return;
        //}
		
		
        //// GameSystem.Instance.mClient.mUIManager.StoreCtrl.OnBuyStoreGoodsResp(resp);
        //if (UIStoreFashion.tempIntance != null)
        //{
        //    UIStoreFashion.tempIntance.OnBuyStoreGoodsResp(resp);
        //}
	}
	
	public void FashionOperationRespHandler(Pack pack)
	{
        //FashionOperationResp resp = Serializer.Deserialize<FashionOperationResp>(new MemoryStream(pack.buffer));
		
        //foreach( fogs.proto.msg.FashionOperationRespInfo respInfo in resp.resp_info )
        //{
        //    if ((ErrorID)respInfo.result != ErrorID.SUCCESS)
        //    {
        //        //ÌáÊ¾²Ù×÷Ê§°Ü
        //        string strError = ((ErrorID)respInfo.result).ToString();
        //        CommonFunction.ShowTip(CommonFunction.GetConstString(strError));
        //        return;
        //    }
        //}
		
		
		
        //// UIManager.Instance.StoreCtrl.OnFashionOperation(resp);
        //if (UIStoreFashion.tempIntance != null)
        //{
        //    UIStoreFashion.tempIntance.OnFashionOperation(resp);
        //}
	}
	
	
	/// <summary>
	/// ´ò¿ªÈÎÎñ½çÃæ·µ»ØÏûÏ¢
	/// </summary>
	/// <param name="pack"></param>
	//public void TaskInfoHandler(Pack pack)
	//{
	//    Debug.Log("-----TaskInfoResp");
	//    TaskInfoResp taskInfoResp = Serializer.Deserialize<TaskInfoResp>(new MemoryStream(pack.buffer));
	//    GameSystem.Instance.mClient.mUIManager.TaskCtrl.GetTaskInfoResp(taskInfoResp);
	//}
	/// <summary>
	/// ÁìÈ¡ÈÎÎñ½±Àø
	/// </summary>
	/// <param name="pack"></param>
	//public void GetTaskAwardHandler(Pack pack)
	//{
	//    Debug.Log("-----GetTaskAwardResp");
	//    GetTaskAwardResp getTaskAwardResp = Serializer.Deserialize<GetTaskAwardResp>(new MemoryStream(pack.buffer));
	//    if (getTaskAwardResp.result == 0)
	//    {
	//        GameSystem.Instance.mClient.mUIManager.TaskCtrl.GetTaskAwardResp(getTaskAwardResp);
	//    }
	//    else
	//    {
	//        Debug.Log("getTaskAward error:result " + getTaskAwardResp.result);
	//    }
	//}
	/// <summary>
	/// ÒÑÍê³ÉÈÎÎñÌáÊ¾
	/// </summary>
	/// <param name="pack"></param>
    //public void UnclaimedTaskNumNotifyHandler(Pack pack)
    //{
        //Debug.Log("-----UnclaimedTaskNumNotifyResp");
        //UnclaimedTaskNumNotify resp = Serializer.Deserialize<UnclaimedTaskNumNotify>(new MemoryStream(pack.buffer));
        //bool showTips = resp.num > 0 ? true : false;
        //MainPlayer.Instance.unclaimedTaskID = resp.task_id;
        //UIManager.Instance.TaskCtrl.HandelUnClaimedTask(resp.task_id);
        //if (GameSystem.Instance.mClient.mUIManager.MainCtrl != null && 
        //    GameSystem.Instance.mClient.mUIManager.MainCtrl._ui.hallHandler != null)
        //{
        //    GameSystem.Instance.mClient.mUIManager.MainCtrl._ui.hallHandler.RefreshLeftFuncTips(HallLeftFuncType.HLFT_Task, showTips);
        //}
    //}

	public void NotifyTaskFinished(Pack pack)
	{
        NotifyTaskFinish resp = Serializer.Deserialize<NotifyTaskFinish>(new MemoryStream(pack.buffer));

        if (MainPlayer.Instance.taskInfo == null || MainPlayer.Instance.taskInfo.task_list == null)
            return;

        TaskData taskData = MainPlayer.Instance.taskInfo.task_list.Find(task => task.id == resp.task_info.id);
        if (taskData == null)
        {
            taskData = resp.task_info;
            taskData.state = 2;
            MainPlayer.Instance.taskInfo.task_list.Add(taskData);
        }
        else 
        {
            taskData.state = 2;
        }

        if (resp.task_info.type == 2)
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Task");
        else if (resp.task_info.type == 3)
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Daily");
        //MainPlayer.Instance.SetTaskFinished(resp.task_info.id);
	}
	
	/// <summary>
	/// ¿ªÊ¼ÑµÁ·»Ø¸´
	/// </summary>
	/// <param name="pack"></param>
	public void StartTrainingHandler(Pack pack)
	{
		Debug.Log("-----StartTrainingResp");
		StartTrainingResp resp = Serializer.Deserialize<StartTrainingResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.TrainingCtrl.OnStartTrainingResp(resp);
	}
	
	/// <summary>
	/// ÑµÁ·ÏîÄ¿Éý¼¶È·ÈÏ»Ø¸´
	/// </summary>
	/// <param name="pack"></param>
	public void TrainingUpConfirmRespHandler(Pack pack)
	{
		Debug.Log("-----TrainingUpConfirmResp");
		//TrainingUpConfirmResp resp = Serializer.Deserialize<TrainingUpConfirmResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.TrainingCtrl.OnTrainingUpConfirmResp(resp);
	}
	
	/// <summary>
	/// Í¨ÖªÑµÁ·ÐÅÏ¢
	/// </summary>
	/// <param name="pack"></param>
	public void NotifyTrainingInfoHandler(Pack pack)
	{
		Debug.Log("-----NotifyTrainingInfoResp");
		NotifyTrainingInfo resp = Serializer.Deserialize<NotifyTrainingInfo>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.TrainingCtrl.OnNotifyTrainingInfo(resp);
	}
	
	public void OpenTrainingHandler(Pack pack)
	{

        // NOT USE TRAINING.
        //Debug.Log("-----OpenTrainingResp");
        //OpenTrainingResp resp = Serializer.Deserialize<OpenTrainingResp>(new MemoryStream(pack.buffer));
        //if ((ErrorID)resp.result == ErrorID.SUCCESS)
        //{
        //    RoleInfo captainInfo = MainPlayer.Instance.GetCaptainInfo(resp.captain_id);
        //    if (captainInfo != null)
        //    {
        //        captainInfo.training.Clear();
        //        for (int i = 0; i < resp.training.Count; ++i)
        //        {
        //            captainInfo.training.Add(resp.training[i]);
        //        }
        //        //GameSystem.Instance.mClient.mUIManager.TrainingCtrl.ShowUIForm();
        //    }
        //}
        //else
        //{
        //    CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        //}
	}
	
	public void TattooOperationRespHandler(Pack pack)
	{
		Debug.Log("-----TattooOperationResp");
		TattooOperationResp resp = Serializer.Deserialize<TattooOperationResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.TattooCtrl.OnTattooOperationResp(resp);
	}
	
	public void DecomposeGoodsRespHandler(Pack pack)
	{
		Debug.Log("-----DecomposeGoodsResp");
		DecomposeGoodsResp resp = Serializer.Deserialize<DecomposeGoodsResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.TattooCtrl.OnDecomposeGoodsResp(resp);
	}
	
	
	/// <summary>
	/// ÐÂµÄÒ»ÌìÀ´µ½
	/// </summary>
	/// <param name="pack"></param>
	public void OnNewDayComeHandle(Pack pack)
	{
		OnNewDayCome msg = Serializer.Deserialize<OnNewDayCome>(new MemoryStream(pack.buffer));
		MainPlayer.Instance.OnNewDayCome();
	}
	
	//private void EnterRaceHandler(Pack pack)
	//{
	//    EnterRaceResp resp = Serializer.Deserialize<EnterRaceResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        GameSystem.Instance.mClient.mUIManager.AsynPVPRankCtrl.ShowUIForm();
	//    }
	//    else
	//    {
	//        Debug.LogError("Enter race error: " + ((ErrorID)resp.result).ToString());
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}
	
	//private void BuyRaceTimesHandler(Pack pack)
	//{
	//    BuyRaceTimesResp resp = Serializer.Deserialize<BuyRaceTimesResp>(new MemoryStream(pack.buffer));
	//    Debug.Log("------buyraceTimes");
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        if (resp.type == MatchType.MT_REGULAR)
	//        {
	//            MainPlayer.Instance.PvpRunTimes = 0;
	//            MainPlayer.Instance.PvpPointBuyTimes++;
	//            GameSystem.Instance.mClient.mUIManager.AsynPVPRankCtrl.RefreshPVPPoint();
	//        }
	//    }
	//    else
	//    {
	//        Debug.LogError("Buy race times error: " + ((ErrorID)resp.result).ToString());
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}
	
	//private void EnterRoomHandler(Pack pack)
	//{
	//    EnterRoomResp resp = Serializer.Deserialize<EnterRoomResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        if (resp.type == MatchType.MT_REGULAR)
	//        {
	//            GameSystem.Instance.mClient.mUIManager.AsynPVPRankCtrl.HandleEnterRoom(resp);
	//        }
	//    }
	//    else
	//    {
	//        Debug.LogError("Enter room error: " + ((ErrorID)resp.result).ToString());
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}
	
	//private void RankListHandler(Pack pack)
	//{
	//    RankListResp resp = Serializer.Deserialize<RankListResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        if (resp.type == RankType.RT_CUR_REGULAR_POINTS)
	//        {
	//            MainPlayer.Instance.CurrRankList = resp.rank_list;
	//            MainPlayer.SortRankList(ref MainPlayer.Instance.CurrRankList);
	//        }
	//        else if (resp.type == RankType.RT_LAST_REGULAR_POINTS)
	//        {
	//            MainPlayer.Instance.LastRankList = resp.rank_list;
	//            MainPlayer.SortRankList(ref MainPlayer.Instance.LastRankList);
	//        }
	//        GameSystem.Instance.mClient.mUIManager.AsynPVPRankCtrl.OnRankListUpdate();
	//    }
	//    else
	//    {
	//        Debug.LogError("Rank list update error:" + ((ErrorID)resp.result).ToString());
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}
	
	//
    private void MailInfoNotifyHandle(Pack pack)
    {
        MailInfoNotify notify = Serializer.Deserialize<MailInfoNotify>(new MemoryStream(pack.buffer));
        if (notify.result == (uint)ErrorID.SUCCESS)
        {
            MainPlayer.Instance.InitMailInfo(notify.mail_list);
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Mail");
        }
        else
        {
            Debug.LogError("MailInfoNotify returns error: " + notify.result);
        }
    }
	
	//ÐÂÓÊ¼þÍ¨Öª
    private void NewMailNotifyHandle(Pack pack)
    {
        NewMailNotify notify = Serializer.Deserialize<NewMailNotify>(new MemoryStream(pack.buffer));
        if (MainPlayer.Instance.MailList != null)
        {
            MainPlayer.Instance.AddNewMail(notify.mail);
            LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Mail");
        }
        //if (GameSystem.Instance.mClient.mUIManager.MailCtrl != null)
        //{
        //    GameSystem.Instance.mClient.mUIManager.MailCtrl.NewMailNotify();
        //}
    }
	
	//¶ÁÈ¡ÓÊ¼þ»Ø¸´
	//private void ReadMailHandle(Pack pack)
	//{
	//    ReadMailResp resp = Serializer.Deserialize<ReadMailResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        MainPlayer.Instance.ReadMail(resp.mail_id);
	//        GameSystem.Instance.mClient.mUIManager.MailCtrl.OnOpenMail(resp.mail_id);
	//    }
	//    else
	//    {
	//        Debug.LogError("ReadMailHandle returns error: " + resp.result);
	//    }
	//}
	
	//ÁìÈ¡¸½¼þ»Ø¸´
	//private void GetAttachmentHandle(Pack pack)
	//{
	//    GetAttachmentResp resp = Serializer.Deserialize<GetAttachmentResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        MainPlayer.Instance.GetMailAttachment(resp.mail_id);
	//        GameSystem.Instance.mClient.mUIManager.MailCtrl.OnGetAttachment(resp.mail_id);
	//    }
	//    else
	//    {
	//        Debug.LogError("GetAttachmentHandle returns error: " + resp.result);
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}
	
	//private void BeginPractiseHandler(Pack pack)
	//{
	//    CommonFunction.HideWaitMask();
	//    BeginPracticeResp resp = Serializer.Deserialize<BeginPracticeResp>(new MemoryStream(pack.buffer));
	//    if ((ErrorID)resp.result == ErrorID.SUCCESS)
	//    {
	//        GameSystem.Instance.mClient.mUIManager.PractiseListCtrl.OnBeginPractise(resp);
	//    }
	//    else
	//    {
	//        Debug.LogError("Begin practise error: " + resp.result);
	//        CommonFunction.ShowErrorMsg((ErrorID)resp.result);
	//    }
	//}

    private void EnhanceExerciseHandler(Pack pack)
    {
        CommonFunction.HideWaitMask();
        EnhanceExerciseResp resp = Serializer.Deserialize<EnhanceExerciseResp>(new MemoryStream(pack.buffer));
        uint result = resp.result; 

		if( result == 0 )
		{
			uint role_id = resp.role_id;
            foreach( var item in resp.exercise )
            {
                uint exercise_id = item.id;
                MainPlayer.Instance.SetExerciseInfo(role_id, exercise_id, item);
                ExerciseInfo exerciseInfo = MainPlayer.Instance.GetExerciseInfo(role_id, exercise_id);
                exerciseInfo = item;
            }	
		}


        Transform captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UIRole(Clone)/RoleDetail(Clone)");
        if (captainTrans == null )
        {
            captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UISquad(Clone)/RoleDetail(Clone)");
        }
        if (captainTrans)
        {
            LuaComponent captainLua = captainTrans.GetComponent<LuaComponent>();
            LuaTable table = captainLua.table;
            object o = table["EnhanceExerciseFromC"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }
    }


	private void EnhanceLevelHandler(Pack pack)
    {
        CommonFunction.HideWaitMask();
        EnhanceLevelResp resp = Serializer.Deserialize<EnhanceLevelResp>(new MemoryStream(pack.buffer));
        uint result = resp.result; 

		if( result == 0 )
		{
             MainPlayer.Instance.SetRoleInfo(resp.role);
		}


        Transform captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UIRole(Clone)/RoleDetail(Clone)");
        if (captainTrans == null)
        {
            captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UISquad(Clone)/RoleDetail(Clone)");
        }

        if (captainTrans)
        {
            LuaComponent captainLua = captainTrans.GetComponent<LuaComponent>();
            LuaTable table = captainLua.table;
            object o = table["EnhanceLevelFromC"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }
    }

	private void RoleResetHandler(Pack pack)
	{
        CommonFunction.HideWaitMask();
		ResetRoleResp resp = Serializer.Deserialize<ResetRoleResp>(new MemoryStream(pack.buffer));
        uint result = resp.result;
        if (result == 0)
        {
			MainPlayer.Instance.SetRoleInfo(resp.role);
        }

        Transform trans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UIRole(Clone)");
        if (trans)
        {
            LuaComponent luaComp = trans.GetComponent<LuaComponent>();
            LuaTable table = luaComp.table;
            object o = table["OnResetFromC"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }			
	}
	
    private void ImproveQualityHandler(Pack pack)
    {
        ImproveQualityResp resp = Serializer.Deserialize<ImproveQualityResp>(new MemoryStream(pack.buffer));
        uint result = resp.result;

        if (result == 0)
        {

            uint role_id = resp.role_id;
            uint new_quality = resp.new_quality;
            MainPlayer.Instance.SetRoleQuality(role_id, new_quality);

            // uint exercise_id = resp.exercise.id;
            // MainPlayer.Instance.SetExerciseInfo(role_id, exercise_id, resp.exercise);
            // ExerciseInfo exerciseInfo = MainPlayer.Instance.GetExerciseInfo( role_id,exercise_id );
            // exerciseInfo = resp.exercise;

        }


        Transform captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UIRole(Clone)/RoleDetail(Clone)");
        if (captainTrans == null)
        {
            captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UISquad(Clone)/RoleDetail(Clone)");
        }

        if (captainTrans)
        {
            LuaComponent captainLua = captainTrans.GetComponent<LuaComponent>();
            LuaTable table = captainLua.table;
            object o = table["improveQualityFromC"];
            LuaFunction fun = o as LuaFunction;
            fun.Call(table, resp);
        }
    }

	private void BuyCaptainRespHandler(Pack pack)
	{
		BuyCaptainResp resp = Serializer.Deserialize<BuyCaptainResp>(new MemoryStream(pack.buffer));
		
		
		//Ìí¼ÓÐÂ¶Ó³¤ÐÅÏ¢
		RoleInfo newCaptain = new RoleInfo();
		newCaptain.id = resp.id;
        newCaptain.acc_id = MainPlayer.Instance.AccountID;
		//newCaptain.bias = resp.bias;
		newCaptain.quality = (uint)QualityType.QT_NONE;
		for (int i = 0; i < resp.skill_slot_info.Count; ++i)
		{
			newCaptain.skill_slot_info.Add(resp.skill_slot_info[i]);
		}
		for (int i = 0; i < resp.skill_goods.Count; ++i)
		{
			Goods skillGoods = new Goods();
			skillGoods.Init(resp.skill_goods[i]);
			MainPlayer.Instance.AddGoods(skillGoods);
		}
		//for (int i = 1; i <= 4; ++i)
		//{
		//	TattooSlotProto tattoo = new TattooSlotProto();
		//	tattoo.id = (uint)i;
		//	newCaptain.tattoo_slot_info.Add(tattoo);
		//}
		
		MainPlayer.Instance.CaptainInfos.Add(newCaptain);
		
		Transform captainTrans = UIManager.Instance.m_uiRootBasePanel.transform.FindChild("UICaptain(Clone)");
		if (captainTrans)
		{
			LuaComponent captainLua = captainTrans.GetComponent<LuaComponent>();
			LuaTable table = captainLua.table;
			object o = table["on_buy_resp_from_csharp"];
			LuaFunction fun = o as LuaFunction;
			fun.Call(table, resp);
		}
		
		
		
		//GameSystem.Instance.mClient.mUIManager.MainCtrl.OnBuyCaptainResp(resp);
	}
	
	private void SwitchCaptainRespHandler(Pack pack)
	{
		SwitchCaptainResp resp = Serializer.Deserialize<SwitchCaptainResp>(new MemoryStream(pack.buffer));
		//GameSystem.Instance.mClient.mUIManager.MainCtrl.OnSwitchCaptainResp(resp);
	}

    //指引
    private void BeginGuideHandler(Pack pack)
    {
        GuideSystem.Instance.BeginGuideHandler(pack);
    }

    private void EndGuideHandler(Pack pack)
    {
        GuideSystem.Instance.EndGuideHandler(pack);
    }

    private void UpdateFightRoleHandler(Pack pack)
    {
        UpdateFightRole resp = Serializer.Deserialize<UpdateFightRole>(new MemoryStream(pack.buffer));
        MainPlayer.Instance.SetFightRole(resp.info);
    }
    //新的一天来临(12点)
    private void NewDayComeMidNightHandler(Pack pack)
    {
        NewDayCome resp = Serializer.Deserialize<NewDayCome>(new MemoryStream(pack.buffer));
		MainPlayer.Instance.OnNewDayComeMidNight(resp);
    }
    //公告
    private void AnnounceMentHandler(Pack pack) 
    {
        Debug.Log("AnnounceMentHandler ---------------------- >>>>>");
        NoticeToClient resp = Serializer.Deserialize<NoticeToClient>(new MemoryStream(pack.buffer));
        if (resp == null)
            return;

        if (System.Text.RegularExpressions.Regex.IsMatch(resp.content, @"{.*}"))
        {
            string str = resp.content;
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex("(?<replaceStr>{.*})");
            foreach (var item in rex.Match(resp.content).Groups)
            {
                string oldStr = item.ToString();
                string goodsID = oldStr.Substring(1, oldStr.Length - 2);
                string newStr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(uint.Parse(goodsID)).name;
                str = str.Replace(oldStr, newStr);
            }
            resp.content = str;
        }
        
        if (resp.content.Substring(0, 1) == "#")
        {
            resp.content = resp.content.Substring(1, resp.content.Length - 1);
        }
        else if (GameSystem.Instance.mClient.mCurMatch != null)
        {
            return;
        }
        MainPlayer.Instance.AnnouncementList.Add(resp.content);

        //ChatBroadcast systemChat = new ChatBroadcast();
        //ChatContent info = new ChatContent();
        //info.content = resp.content;
        //systemChat.info = info;
        //systemChat.type = (uint)(ChatChannelType.CCT_SYSTEM);
        //MainPlayer.Instance.WorldChatList.Add(systemChat);
        //if (MainPlayer.Instance.WorldChatList.Count > 100)
        //    MainPlayer.Instance.WorldChatList.Remove(MainPlayer.Instance.WorldChatList[0]);
        //if (MainPlayer.Instance.onNewChatMessage != null)
        //    MainPlayer.Instance.onNewChatMessage();
        
        if (MainPlayer.Instance.onAnnounce != null && MainPlayer.Instance.AnnouncementList.Count <= 1)
            MainPlayer.Instance.onAnnounce();
    }
    //聊天广播处理
    private void ChatBroadCastHandler(Pack pack)
    {
        ChatBroadcast chatBroadCast = Serializer.Deserialize<ChatBroadcast>(new MemoryStream(pack.buffer));
        if (chatBroadCast == null)
            return;
        if (chatBroadCast.info.content != null && chatBroadCast.info.content != "")
        {
			chatBroadCast.time = (uint)(Time.realtimeSinceStartup*10);
			//好友聊天和其他聊天分开处理
			if(chatBroadCast.type == (uint)ChatChannelType.CCT_PRIVATE)
			{
				MainPlayer.Instance.FriendChatMessage = chatBroadCast;
				if(MainPlayer.Instance.onFriendChatMessage!=null)
				{
					MainPlayer.Instance.onFriendChatMessage();
				}
			}
			//队伍聊天
			else if(chatBroadCast.type == (uint)ChatChannelType.CCT_TEAM)
			{
				MainPlayer.Instance.TeamChatMessage = chatBroadCast;
				if(MainPlayer.Instance.OnTeamChatMessage!=null)
				{
					MainPlayer.Instance.OnTeamChatMessage();
				}
			}
			//联盟频道
			else if(chatBroadCast.type == (uint)ChatChannelType.CCT_LEAGUE)
			{
				MainPlayer.Instance.LeagueChatMessage = chatBroadCast;
				if(MainPlayer.Instance.OnLeagueChatMessage!=null)
				{
					MainPlayer.Instance.OnLeagueChatMessage();
				}
			}
			else{
				MainPlayer.Instance.WorldChatList.Add(chatBroadCast);
				if (MainPlayer.Instance.WorldChatList.Count > 100)
					MainPlayer.Instance.WorldChatList.Remove(MainPlayer.Instance.WorldChatList[0]);
				if (MainPlayer.Instance.onNewChatMessage != null)
					MainPlayer.Instance.onNewChatMessage();
			}
        }
    }

    private void ChatLastestInfoRespHandler(Pack pack)
    {
        ChatLatestInfoResp chatLastResp = Serializer.Deserialize<ChatLatestInfoResp>(new MemoryStream(pack.buffer));
        if (chatLastResp == null)
            return;
        foreach (var info in chatLastResp.info)
        {
            ChatBroadcast chatBroadCast = new ChatBroadcast();
            chatBroadCast.type = (uint)ChatChannelType.CCT_WORLD;
            chatBroadCast.info = info;
            MainPlayer.Instance.WorldChatList.Add(chatBroadCast);
        }

    }

    private void UpdateRoleMapInfoHandler(Pack pack) 
    {
        UpdateRoleMapAddAttr roleMapResp = Serializer.Deserialize<UpdateRoleMapAddAttr>(new MemoryStream(pack.buffer));
        if (roleMapResp == null)
            return;

        MainPlayer.Instance.NewMapIDList.Clear();
        foreach (KeyValueData data in roleMapResp.info)
        {
            if (MainPlayer.Instance.MapIDInfo.Contains(data.id))
                continue;
            else
            {
                if (!MainPlayer.Instance.InitMap)
                {
                    MainPlayer.Instance.NewMapIDList.Add(data.id);
                    LuaScriptMgr.Instance.GetLuaTable("_G").Set("IsRefreshMap", 2);
                    LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "Role");
                    LuaScriptMgr.Instance.CallLuaFunction("UpdateRedDotHandler.MessageHandler", "RoleDetail");
                }
                MainPlayer.Instance.MapIDInfo.Add(data.id);
            }
        }
        if (MainPlayer.Instance.InitMap)
        {
            MainPlayer.Instance.InitMap = false;
            LuaScriptMgr.Instance.GetLuaTable("_G").Set("IsRefreshMap", 0);
        }
    }

    private void UpdateActivityInfoHandler(Pack pack)
    {
        UpdateActivityInfo updateActivityInfo = Serializer.Deserialize<UpdateActivityInfo>(new MemoryStream(pack.buffer));
        if (updateActivityInfo == null)
            return;

        MainPlayer.Instance.activityInfo = updateActivityInfo.info;
    }

    private void UpdateTrialInfo(Pack pack)
    {
        NewComerTrialNotify newComerTrialList = Serializer.Deserialize<NewComerTrialNotify>(new MemoryStream(pack.buffer));
        if (newComerTrialList == null)
            return;

//        MainPlayer.Instance.newComerTrialInfo = newComerTrialList.info;


    }

	private void HandleRefreshQualifyingNewInfo(Pack pack)
	{
		QualifyingNewInfo info = Serializer.Deserialize<QualifyingNewInfo>(new MemoryStream(pack.buffer));
		MainPlayer.Instance.qualifying_new = info;
	}
	private void HandleRefreshQualifyingNewerInfo(Pack pack)
	{
        Debug.Log("HandleRefreshQualifyingNewerInfo c#"); 
		RefreshQualifyingNewerInfo info = Serializer.Deserialize<RefreshQualifyingNewerInfo>(new MemoryStream(pack.buffer));
		MainPlayer.Instance.QualifyingNewerInfo = info.info;
        MainPlayer.Instance.QualifyingNewerScore = info.qualifying_newer_score;
	}
}
