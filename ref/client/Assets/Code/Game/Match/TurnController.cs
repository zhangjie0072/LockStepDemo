using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class TurnController : Singleton<TurnController>
{
    //时间单位：毫秒
    public const int TURN_LENGTH = 100;        //回合间隔
    public const int GAME_UPDATE_PER_TURN = 3; //每回合逻辑帧数
    public const int GAME_UPDATE_LENGTH = TURN_LENGTH / GAME_UPDATE_PER_TURN;  //定义逻辑帧间隔
    public static IM.Number deltaTime = IM.Number.Raw(TurnController.GAME_UPDATE_LENGTH * IM.Math.FACTOR / 1000);
    const int PURSUE_GAME_UPDATE_NUM = GAME_UPDATE_PER_TURN * 20;   //追赶需要的逻辑帧数
    const int PURSUE_TIME_LENGTH = PURSUE_GAME_UPDATE_NUM * GAME_UPDATE_LENGTH; //追赶需要的时长
    int pendingGameUpdateNum = 0;   //待执行的逻辑帧数量
    float minGameUpdateLength = 0f;   //最小逻辑帧间隔
    float exactGameUpdateLength = GAME_UPDATE_LENGTH;   //实际逻辑帧间隔
    float acumulativeTime = 0f;         //累积时间
    float acumulativeTimeServer = 0f;   //累积时间（服务器进度）
    int gameUpdateIndexInTurnServer = 0;    //回合中第几个逻辑帧（服务器进度）

    GameMatch _match;
    public GameMatch match
    {
        get { return _match; }
        set
        {
            _match = value;
            _match.turnManager.onNewTurn += OnNewTurn;
        }
    }

    public TurnController()
    {
        minGameUpdateLength = 33;
    }

    public void Update(float deltaTime)
    {
        if (match == null)
            return;

        //计数回合内逻辑帧（服务器进度）
        acumulativeTimeServer += deltaTime * 1000;
        if (acumulativeTimeServer >= GAME_UPDATE_LENGTH)
        {
            acumulativeTimeServer -= GAME_UPDATE_LENGTH;
            ++gameUpdateIndexInTurnServer;
        }

        //如果预测平均回合间隔将大于定义回合间隔，则可能出现等回合。通过减慢逻辑帧速度，平滑等帧的卡顿(TODO:先注释，因在比赛中如果虚拟服务器手动停止，消息turn之间的间隔过大，下面的运算会让excatGameUpdateLength变成无穷大，影响正常的业务逻辑）
        //float turnInterval = match.turnManager.averageTurnInterval;
        //if (turnInterval > ((float)TURN_LENGTH / 1000 + 0.2f))
        //{
        //    float factor = turnInterval / ((float)TURN_LENGTH / 1000);
        //    //Debug.Log("Scale exactGameUpdateLength: " + factor);
        //    exactGameUpdateLength = exactGameUpdateLength * factor;
        //}
        //执行逻辑帧
        acumulativeTime += deltaTime * 1000;
        if (acumulativeTime >= exactGameUpdateLength)
        {
            acumulativeTime -= exactGameUpdateLength;

            //计算需执行的逻辑帧数量（叠帧）
            int gameUpdateNum = CalcGameUpdateNumPerLoop();
            for (int i = 0; i < gameUpdateNum && pendingGameUpdateNum > 0; ++i)
            {
                //处理回合
                int indexInTurn = match.curGameUpdateID % GAME_UPDATE_PER_TURN;
                bool toProcessTurn = (indexInTurn == 0);
                if (toProcessTurn)
                {
                    FrameInfo nextTurn = match.turnManager.NextTurn();
                    if (nextTurn == null)   //等回合，不执行逻辑帧
                        break;
                    match.ProcessTurn(nextTurn, TurnController.deltaTime);
                }

                //Debug.Log("Turn:" + match.turnManager.CurTurnID + " GameUpdateIndex:" + indexInTurn +
                //    " PendingGameUpdateNum:" + pendingGameUpdateNum);
                //执行逻辑帧
                match.GameUpdate(TurnController.deltaTime);
                match.GameLateUpdate(TurnController.deltaTime);
                //*
                //发送关键数据校验
                if (toProcessTurn && match is GameMatch_PVP)
                    match.turnManager.SendTurnCheckData();
                //*/

                --pendingGameUpdateNum;
            }
        }
    }

    void OnNewTurn(FrameInfo info)
    {
        pendingGameUpdateNum += GAME_UPDATE_PER_TURN;
        gameUpdateIndexInTurnServer = 0;
        acumulativeTimeServer = 0f;


        //gameUpdateSpeedUpLevel = Mathf.CeilToInt((float)pendingGameUpdateNum / GAME_UPDATE_PER_TURN);
        //if (gameUpdateSpeedUpLevel != 1)
        //{
        //    Debug.Log("GameUpdateSpeedUpLevel: " + gameUpdateSpeedUpLevel);
        //    Debug.Log("Turn:" + TurnManager.Instance.CurTurnID + " ServerTurn:" + TurnManager.Instance.ServerTurnID
        //        + " GameUpdate:" + Game.Instance.CurFrame + " PendingGameUpdateNum:" + pendingGameUpdateNum);
        //    //Pause = true;
        //}
    }

    //计算一次执行的逻辑帧数量（叠帧）
    int CalcGameUpdateNumPerLoop()
    {
        //有(GAME_UPDATE_PER_TURN - gameUpdateIndexInTurnServer)个帧是即将执行的，不算delay
        int delayedGameUpdateNum = (pendingGameUpdateNum - (GAME_UPDATE_PER_TURN - gameUpdateIndexInTurnServer));
        //根据追赶帧数计算加速倍数
        float speed = 1 + (float)delayedGameUpdateNum / PURSUE_GAME_UPDATE_NUM;
        //调节实际逻辑帧间隔
        exactGameUpdateLength = GAME_UPDATE_LENGTH / speed;
        //实际逻辑帧间隔缩小到极限，调节叠帧倍数
        if (exactGameUpdateLength < minGameUpdateLength)
        {
            exactGameUpdateLength = minGameUpdateLength;
            float pursueGameUpdateNum = PURSUE_TIME_LENGTH / exactGameUpdateLength;
            speed = 1 + (float)delayedGameUpdateNum / pursueGameUpdateNum;
        }
        return Mathf.CeilToInt(speed);
    }
}
