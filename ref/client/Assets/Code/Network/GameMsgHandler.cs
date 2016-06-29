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

        RegisterHandler(MsgID.BeginGuideRespID, GuideSystem.Instance.BeginGuideHandler);
        RegisterHandler(MsgID.EndGuideRespID, GuideSystem.Instance.EndGuideHandler);
        RegisterHandler(MsgID.HeartbeatID, HeartbeatHandle);
        RegisterHandler(MsgID.GameBeginRespID, HandleGameBeginResp);
		RegisterHandler(MsgID.InstructionReqID, GameMatch.HandleBroadcast);
        RegisterHandler(MsgID.ClientInputID, HandleClientInput);
        RegisterHandler(MsgID.PlayFrameID, HandleNewTurn);
        RegisterHandler(MsgID.GameBeginReqID, HandleGameBeginReq);
        RegisterHandler(MsgID.NotifyOutSyncID, HandleOutSync);
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

    //For virtual game server
    void HandleClientInput(Pack pack)
    {
        ClientInput input = ProtoBuf.Serializer.Deserialize<ClientInput>(new MemoryStream(pack.buffer));
        VirtualGameServer.Instance.AddInput(input);
        //Debug.Log("Handle input, " + (InputDirection)input.dir + " " + (Command)input.cmd);
    }

    void HandleNewTurn(Pack pack)
    {
        PlayFrame turn = ProtoBuf.Serializer.Deserialize<PlayFrame>(new MemoryStream(pack.buffer));
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

    void HandleOutSync(Pack pack)
    {
        NotifyOutSync outSync = Serializer.Deserialize<NotifyOutSync>(new MemoryStream(pack.buffer));
        int turnID = (int)outSync.frameNum;
        Debug.LogError("Turn out sync, ID: " + turnID + " " + (turnID - 1) * TurnController.GAME_UPDATE_PER_TURN);
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        GameMatch match = GameSystem.Instance.mClient.mCurMatch;
        CheckFrame turnData = match.turnManager.GetTurnCheckData(turnID);
        if (turnData != null)
        {
            foreach (RoleKeyState playerData in turnData.roleDatas)
            {
                Player player = pm.GetPlayerByRoomId(playerData.index);
                Debug.LogError(string.Format("{0} P:{1} Pos:({2},{3},{4}) Angle:{5} State:{6}",
                    player.m_team.m_side, player.m_id, 
                    playerData.position.x, playerData.position.y, playerData.position.z,
                    playerData.hori_angle, (PlayerState.State)playerData.state));
            }
            foreach (BallKeyState ballData in turnData.ballDatas)
            {
                UBasketball ball = match.mCurScene.balls.Find(b => b.m_id == ballData.index);
                Debug.LogError(string.Format("Ball:{0} Pos:({1},{2},{3}) State:{4}",
                    ball.m_id, ballData.position.x, ballData.position.y, ballData.position.z,
                    (BallState)ball.m_ballState));
            }
        }
    }
}
