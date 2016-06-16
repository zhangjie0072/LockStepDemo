using fogs.proto.msg;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIReadyRoom_Tour : UIReadyRoom
{
	protected override void OnAwake()
	{
		GameSystem.Instance.mNetworkManager.m_platMsgHandler.RegisterHandler(MsgID.EnterGameRespID, TourStartHandler);   //开始巡回赛
	}

    void OnDestroy()
    {
		if (GameSystem.Instance.mNetworkManager != null)
		{
			GameSystem.Instance.mNetworkManager.m_platMsgHandler.UnregisterHandler(MsgID.EnterGameRespID, TourStartHandler);   //开始巡回赛
		}
    }

    protected override void OnBack(GameObject go)
    {
    }

    protected override void OnStartClicked(GameObject go)
    {
        TourStartReq tour = new TourStartReq();
        EnterGameReq req = new EnterGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_TOUR;
        req.tour = tour;

        //GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.TourStartReqID);
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.EnterGameReqID);
    }

    private void TourStartHandler(Pack pack)
    {
		/*
        TourStartResp resp = Serializer.Deserialize<TourStartResp>(new MemoryStream(pack.buffer));
        if ((ErrorID)resp.result == ErrorID.SUCCESS)
        {
            MainPlayer.Instance.CurTourID = resp.cur_tour_id;
            TourData tour = GameSystem.Instance.TourConfig.GetTourData(MainPlayer.Instance.CurTourID, MainPlayer.Instance.Level);
            GameMode gameMode = GameSystem.Instance.GameModeConfig.GetGameMode(tour.gameModeID);

			UIAsynPVPLoading loading = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/Forms/UIAsynPVPLoading").GetComponent<UIAsynPVPLoading>();
            loading.my_role_list = new Dictionary<FightStatus, uint>();
            loading.my_role_list.Add(FightStatus.FS_MAIN, MainPlayer.Instance.CaptainID);
            if (!_select_role.single)
            {
                loading.my_role_list.Add(FightStatus.FS_ASSIST1, _select_role.assistantsID[0]);
                loading.my_role_list.Add(FightStatus.FS_ASSIST2, _select_role.assistantsID[1]);
            }
            loading.rival_list = new List<MatchInfo>();
            MatchInfo matchInfo = new MatchInfo();
            loading.rival_list.Add(matchInfo);
            if (gameMode.mappedNPC.ContainsKey(MainPlayer.Instance.Captain.m_position))
            {
                MatchInfo.MemberInfo memberInfo = new MatchInfo.MemberInfo();
                memberInfo.id = gameMode.mappedNPC[MainPlayer.Instance.Captain.m_position];
                matchInfo.members.Add(memberInfo);
            }
            foreach (List<uint> candidates in gameMode.unmappedNPC)
            {
                if (candidates != null && candidates.Count > 0)
                {
                    MatchInfo.MemberInfo memberInfo = new MatchInfo.MemberInfo();
                    do
                    {
                        memberInfo.id = candidates[Random.Range(0, candidates.Count - 1)];
                    } while (matchInfo.members.Find((MatchInfo.MemberInfo info) => { return info.id == memberInfo.id; }) != null);
                    matchInfo.members.Add(memberInfo);
                }
            }
            loading.scene_name = gameMode.scene;
            loading.gameModeID = gameMode.ID;
            loading.session_id = resp.session_id;
            loading.leagueType = GameMatch.LeagueType.eTour;
            loading.matchType = gameMode.matchType;
            loading.matchTime = gameMode.time;
            loading.Refresh();
            NGUITools.BringForward(loading.gameObject);
        }
        else
        {
            Logger.Log("Start tour error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result, transform);
        }
		*/
    }
}
