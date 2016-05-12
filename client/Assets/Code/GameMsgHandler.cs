using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;

public class GameMsgHandler
	:MsgHandler
{
    struct PackInfo
    {
        public Pack pack;
        public float recvTime;
    }

    const float MAX_SIM_LATENCY = 0.6f;
    const float MIN_SIM_LATENCY = 0.3f;

    float curSimLatency;
    bool enableSimLatency = true;
    Queue<PackInfo> _msgCache = new Queue<PackInfo>();

	public GameMsgHandler()
    {
		m_strId = "gs";

        RegisterHandler(MsgID.EnterPlatRespID, HandleEnterPlatResp);
        RegisterHandler(MsgID.GameBeginRespID, HandleGameBegin);
        RegisterHandler(MsgID.FrameInfoID, HandleNewTurn);
    }

    public override void HandleMsg(Pack pack)
    {
		MsgID msgID = (MsgID)pack.MessageID;
        if (msgID == MsgID.FrameInfoID && (_msgCache.Count > 0 || enableSimLatency))
        {
            PackInfo info = new PackInfo();
            info.pack = pack;
            info.recvTime = Time.time;
            _msgCache.Enqueue(info);
            return;
        }

        base.HandleMsg(pack);
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            enableSimLatency = !enableSimLatency;

        while (_msgCache.Count > 0)
        {
            PackInfo info = _msgCache.Peek();
            float timediff = Time.time - info.recvTime;
            if (timediff < curSimLatency)
                break;
            //Logger.Log("SimLatency: Handle cache msg, delayed time: " + timediff);
            base.HandleMsg(info.pack);
            _msgCache.Dequeue();
        }

        if (enableSimLatency)
            curSimLatency = Random.Range(MIN_SIM_LATENCY, MAX_SIM_LATENCY);
        else
            curSimLatency = 0f;
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
