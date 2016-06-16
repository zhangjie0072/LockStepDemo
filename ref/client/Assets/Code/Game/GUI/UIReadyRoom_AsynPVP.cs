using fogs.proto.msg;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIReadyRoom_AsynPVP : UIReadyRoom
{
	protected override void OnAwake()
	{
		GameSystem.Instance.mNetworkManager.m_platMsgHandler.RegisterHandler(MsgID.AsynExitRoomRespID, ExitRoomHandler);   //退出房间
		GameSystem.Instance.mNetworkManager.m_platMsgHandler.RegisterHandler(MsgID.StartMatchRespID, StartMatchHandler);   //开始匹配
		//GameSystem.Instance.mNetworkManager.m_handler.RegisterHandler(MsgID.RomeActionRespID, RoomActionHandler);
	}

    void OnDestroy()
    {
		if (GameSystem.Instance.mNetworkManager != null)
		{
			GameSystem.Instance.mNetworkManager.m_platMsgHandler.UnregisterHandler(MsgID.AsynExitRoomRespID, ExitRoomHandler);   //退出房间
			GameSystem.Instance.mNetworkManager.m_platMsgHandler.UnregisterHandler(MsgID.StartMatchRespID, StartMatchHandler);   //开始匹配
			//GameSystem.Instance.mNetworkManager.m_handler.UnregisterHandler(MsgID.RomeActionRespID, RoomActionHandler);    //房间操作
		}
    }

	protected override void OnBack(GameObject go)
	{
        AsynExitRoomReq req = new AsynExitRoomReq();
		req.type = MatchType.MT_REGULAR;
		GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.AsynExitRoomReqID);
	}

	protected override void OnStartClicked(GameObject go)
	{
        //if (_select_role != null && _select_role.selectCompleted)
        //{
        //    StartMatchReq req = new StartMatchReq();
        //    req.type = MatchType.MT_REGULAR;
        //    FightRoleInfo info = new FightRoleInfo();
        //    info.status = FightStatus.FS_MAIN;
        //    info.role_id = MainPlayer.Instance.CaptainID;
        //    req.fight_list.Add(info);
        //    for (int i = 0; i < 2; ++i)
        //    {
        //        uint id = _select_role.assistantsID[i];
        //        if (id != 0)
        //        {
        //            info = new FightRoleInfo();
        //            info.status = FightStatus.FS_ASSIST1 + i;
        //            info.role_id = id;
        //            req.fight_list.Add(info);
        //        }
        //    }
        //    GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.StartMatchReqID);
        //}
        //else
        //{
        //    CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("PLAYER_UNSELECTED"), transform, null, null,
        //        CommonFunction.GetConstString("BUTTON_OK"), "");
        //}
	}

	protected override void OnChangeRoomClicked(GameObject go)
	{
        // not use
        //RomeActionReq req = new RomeActionReq();
        //req.type = RoomActionType.RAT_CHANGE_ROOM;
		//GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.RomeActionReqID);
	}

    private void ExitRoomHandler(Pack pack)
    {
        AsynExitRoomResp resp = Serializer.Deserialize<AsynExitRoomResp>(new MemoryStream(pack.buffer));
        if ((ErrorID)resp.result == ErrorID.SUCCESS)
        {
            GameSystem.Instance.mClient.Reset();
            GameSystem.Instance.mClient.mUIManager.curLeagueType = GameMatch.LeagueType.eReady4AsynPVP;
            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_MATCH);
        }
        else
        {
            Logger.LogError("Exit room error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result, transform);
        }
    }

    private void StartMatchHandler(Pack pack)
    {
        //StartMatchResp resp = Serializer.Deserialize<StartMatchResp>(new MemoryStream(pack.buffer));
        //if ((ErrorID)resp.result == ErrorID.SUCCESS)
        //{
		//    UIAsynPVPLoading loading = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/Forms/UIAsynPVPLoading").GetComponent<UIAsynPVPLoading>();
        //    loading.my_role_list = new Dictionary<FightStatus, uint>();
        //    loading.my_role_list.Add(FightStatus.FS_MAIN, MainPlayer.Instance.CaptainID);
        //    if (!_select_role.single)
        //    {
        //        loading.my_role_list.Add(FightStatus.FS_ASSIST1, _select_role.assistantsID[0]);
        //        loading.my_role_list.Add(FightStatus.FS_ASSIST2, _select_role.assistantsID[1]);
        //    }
        //    loading.rival_list = resp.rival_info;
        //    loading.scene_name = Random.Range(10001, 10009).ToString();
        //    loading.session_id = resp.session_id;
        //    loading.leagueType = GameMatch.LeagueType.eAsynPVP;
        //    loading.matchType = GameMatch.Type.eAsynPVP3On3;
        //    loading.matchTime = GameSystem.Instance.CommonConfig.GetUInt("gRegularRaceMatchTime");
        //    loading.Refresh();
        //    NGUITools.BringForward(loading.gameObject);
        //}
        //else
        //{
        //    Logger.Log("Start match error: " + ((ErrorID)resp.result).ToString());
        //    CommonFunction.ShowErrorMsg((ErrorID)resp.result, transform);
        //}
    }

    private void RoomActionHandler(Pack pack)
    {
        //RomeActionResp resp = Serializer.Deserialize<RomeActionResp>(new MemoryStream(pack.buffer));
        //if ((ErrorID)resp.result == ErrorID.SUCCESS)
        //{

        //}
        //else
        //{
        //    Logger.Log("Room action error:" + ((ErrorID)resp.result).ToString());
        //    CommonFunction.ShowErrorMsg((ErrorID)resp.result, transform);
        //}
    }
}
