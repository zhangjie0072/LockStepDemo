using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class TurnManager
{
    public int curExecTurnID { get; private set; }
    public int curServerTurnID { get; private set; }
    public int PendingTurnNum { get { return turnQ.Count; } }

    public System.Action<FrameInfo> onNewTurn;

    List<FrameInfo> turnQ;
    int headIdx = -1;    //取出位置
    int tailIdx = -1;    //加入位置

    //校验数据
    Queue<CheckFrame> checkData = new Queue<CheckFrame>();

    float[] turnRecvTime = new float[6];
    public float averageTurnInterval { get; private set; }

    public TurnManager()
    {
        ReallocTurnQ(32);
        curExecTurnID = 0;
        curServerTurnID = 0;
    }

    void ReallocTurnQ(int capacity)
    {
        if (turnQ == null)
        {
            int cap = 1;
            while (cap < capacity)
                cap *= 2;
            turnQ = new List<FrameInfo>(cap);
            while (turnQ.Count < turnQ.Capacity)
                turnQ.Add(null);
            headIdx = -1;
            tailIdx = -1;
        }
        else
        {
            if (capacity <= turnQ.Capacity)
                Debug.LogError("Capacity of new turn Q is less than current capacity.");
            int cap = turnQ.Capacity;
            while (cap < capacity)
                cap *= 2;
            List<FrameInfo> newTurnQ = new List<FrameInfo>(cap);
            //将旧turnQ中存储的元素拷贝到新turnQ中
            if (turnQ.Count > 0)
            {
                for (int i = headIdx; i != tailIdx; i = WrapIncIndex(i, 1))
                {
                    newTurnQ.Add(turnQ[i]);
                }
                newTurnQ.Add(turnQ[tailIdx]);
                headIdx = 0;
                tailIdx = newTurnQ.Count - 1;
            }
            while (newTurnQ.Count < newTurnQ.Capacity)
                newTurnQ.Add(null);
            turnQ = newTurnQ;
        }
        Debug.Log(string.Format("Realloc turn Q, cap: {0} <-> {1}", capacity, turnQ.Capacity));
    }

    //新的一帧到来
    public void NewTurn(PlayFrame playFrame)
    {
        if (headIdx == -1)
            headIdx = 0;
        //计算需求的空间
        int maxTurnID = (int)playFrame.frames[playFrame.frames.Count - 1].frameNum;
        int turnNum = maxTurnID - curExecTurnID;
        if (turnNum > turnQ.Capacity) //当前turnQ空间已经存不下，分配更多空间
            ReallocTurnQ(turnNum);
        foreach (FrameInfo turn in playFrame.frames)
        {
            int turnID = (int)turn.frameNum;
            int diff = turnID - curServerTurnID;    //帧编号差距
            int index = WrapIncIndex(tailIdx, diff);     //安放位置
            turnQ[index] = turn;    //安放
            if (diff > 1)    //丢包，请求重发
            {
                List<uint> missedTurnIDs = new List<uint>();
                for (int i = curServerTurnID + 1; i < turnID; ++i)
                    missedTurnIDs.Add((uint)i);
                GameMsgSender.SendMissTurnReq(missedTurnIDs);
            }
            if (diff > 0)   //非补帧
            {
                tailIdx = index;
                curServerTurnID = (int)turn.frameNum;
            }
        //Logger.Log("Receive new turn: " + info.frameNum + " " + Time.time);
            /*
            foreach (ClientInput input in turn.info)
            {
                Logger.Log(string.Format("New turn:{0} Acc:{1} Dir:{2} Cmd:{3}",
        //        Logger.Log("Input: " + input.acc_id + " " + input.dir + " " + input.cmd);
            }
            //*/
            //通知
            if (onNewTurn != null)
                onNewTurn(turn);
        }
        CalcAverageTurnInterval(Time.time);
    }

    //取下一帧执行
    public FrameInfo NextTurn()
    {
        if (headIdx == -1)
            return null;
        FrameInfo turn = null;
        turn = turnQ[headIdx];
        if (turn == null)
        {
            if (tailIdx > headIdx)  //缺帧，请求重发。否则只是新的帧还未到
                GameMsgSender.SendMissTurnReq((uint)curExecTurnID + 1);
        }
        else
        {
            if (turn.frameNum != curExecTurnID + 1)
                Debug.LogError("turn.frameNum != curExecTurnID + 1, Impossible!");
            turnQ[headIdx] = null;
            headIdx = WrapIncIndex(headIdx, 1);
            curExecTurnID = (int)turn.frameNum;
        }
        return turn;
    }

    //增加Index
    int WrapIncIndex(int index, int increment)
    {
        int newIndex = index + increment;
        if (newIndex >= turnQ.Count)
            newIndex %= turnQ.Count;
        return newIndex;
    }

    //计算前几次收包的平均帧间隔时间
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
        //Debug.Log("AverageTurnInterval:" + averageTurnInterval);
    }

    public void SendTurnCheckData()
    {
        CheckFrame turnData = new CheckFrame();
        turnData.frameNum = (uint)curExecTurnID;
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            RoleKeyState data = new RoleKeyState();
            data.index = player.m_roleInfo.index;
            data.position = GameUtils.Convert(player.position);
            data.hori_angle = player.rotation.eulerAngles.y.raw;
            PlayerState curState = player.m_StateMachine.m_curState;
            data.state = curState != null ? (int)curState.m_eState : 0;
            turnData.roleDatas.Add(data);
        }
        foreach (UBasketball ball in GameSystem.Instance.mClient.mCurMatch.mCurScene.balls)
        {
            BallKeyState data = new BallKeyState();
            data.index = ball.m_id;
            data.position = GameUtils.Convert(ball.position);
            data.state = (int)ball.m_ballState;
            turnData.ballDatas.Add(data);
        }
        checkData.Enqueue(turnData);
        while (checkData.Count > 20)
            checkData.Dequeue();
        GameMsgSender.SendTurnCheckData(turnData);
    }

    public CheckFrame GetTurnCheckData(int turnID)
    {
        foreach (CheckFrame data in checkData)
        {
            if (data.frameNum == turnID)
                return data;
        }
        return null;
    }
}
