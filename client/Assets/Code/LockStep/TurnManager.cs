using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    public uint CurTurnID { get; private set; }
    public uint ServerTurnID;
    public int PendingTurnNum { get { return turnQ.Count; } }

    public System.Action<FrameInfo> onNewTurn;

    Queue<FrameInfo> turnQ = new Queue<FrameInfo>();

    float[] turnRecvTime = new float[6];
    public float averageTurnInterval { get; private set; }

    public void NewTurn(FrameInfo info)
    {
        //Logger.Log("Receive new turn: " + info.frameNum + " " + Time.time);
        //foreach (ClientInput input in info.info)
        //{
        //    if (input.dir != 0)
        //        Logger.Log("Input: " + input.acc_id + " " + input.dir + " " + input.cmd);
        //}
        if (info.frameNum != ServerTurnID + 1)
            Logger.LogError("Discontinuous turn ID. " + ServerTurnID + " " + info.frameNum);
        ServerTurnID = info.frameNum;
        turnQ.Enqueue(info);
        if (onNewTurn != null)
            onNewTurn(info);
        CalcAverageTurnInterval(Time.time);
    }

    public FrameInfo NextTurn()
    {
        if (turnQ.Count > 0)
        {
            FrameInfo turn = turnQ.Dequeue();
            CurTurnID = turn.frameNum;
            return turn;
        }
        return null;
    }

    void CalcAverageTurnInterval(float time)
    {
        float total = 0f;
        int count = 0;
        float recvTime = time;
        for (int i = turnRecvTime.Length - 1; i >= 0; --i)
        {
            float curRecvTime = turnRecvTime[i];
            turnRecvTime[i] = recvTime;
            if (Mathf.Approximately(curRecvTime, 0f))
                break;
            float interval = recvTime - curRecvTime;
            recvTime = curRecvTime;
            total += interval;
            ++count;
        }
        if (count > 0)
            averageTurnInterval = total / count;
        //Logger.Log("AverageTurnInterval:" + averageTurnInterval);
    }
}
