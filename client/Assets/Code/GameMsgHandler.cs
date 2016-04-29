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

        RegisterHandler(MsgID.EnterPlatRespID, HandleEnterPlatResp);
        RegisterHandler(MsgID.GameBeginRespID, HandleGameBegin);
        RegisterHandler(MsgID.FrameInfoID, HandleNewTurn);
    }

    void HandleEnterPlatResp(Pack pack)
    {
        GameMsgSender.SendGameBegin();
    }

    void HandleGameBegin(Pack pack)
    {
        GameBeginResp resp = ProtoBuf.Serializer.Deserialize<GameBeginResp>(new MemoryStream(pack.buffer));
        Game.Instance.Initialize(resp.acc_id);
        GameView.Instance.Initialize();
    }

    void HandleNewTurn(Pack pack)
    {
        FrameInfo turnInfo = ProtoBuf.Serializer.Deserialize<FrameInfo>(new MemoryStream(pack.buffer));
        TurnManager.Instance.NewTurn(turnInfo);
    }
}
