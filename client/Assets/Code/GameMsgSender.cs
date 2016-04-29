using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;

public static class GameMsgSender
{
    static NetworkConn conn { get { return NetworkManager.Instance.m_gameConn; } }

    public static void SendEnterPlat()
    {
        EnterPlat req = new EnterPlat();
        req.acc_id = GameSystem.Instance.AccountID;
        if (conn != null)
            conn.SendPack(0, req, MsgID.EnterPlatID);
    }

    public static void SendGameBegin()
    {
        GameBeginReq req = new GameBeginReq();
        req.acc_id = GameSystem.Instance.AccountID;
        if (conn != null)
            conn.SendPack(0, req, MsgID.GameBeginReqID);
    }

    public static void SendInput(Direction dir, Command cmd)
    {
        ClientInput input = new ClientInput();
        input.dir = (uint)dir;
        input.cmd = (uint)cmd;
        input.acc_id = GameSystem.Instance.AccountID;
        if (conn != null)
            conn.SendPack(0, input, MsgID.ClientInputID);
    }

    public static void SendTurnValidate(Dictionary<uint, PlayerInfo> playerInfos)
    {
        CheckFrame frameData = new CheckFrame();
        frameData.frameNum = TurnManager.Instance.CurTurnID;
        foreach (var pair in playerInfos)
        {
            PlayerInfo info = pair.Value;
            PlayerKeyState playerData = new PlayerKeyState();
            playerData.acc_id = info.ID;
            playerData.position = GameUtils.Convert(info.position);
            playerData.velocity = GameUtils.Convert(info.velocity);
            playerData.state = info.state;
            frameData.datas.Add(playerData);
        }
        if (conn != null)
            conn.SendPack(0, frameData, MsgID.CheckFrameID);
    }
}
