using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class GameSystem : MonoBehaviour
{
    static public GameSystem Instance;
    public bool Pause;
    public uint AccountID;

    const int TURN_LENGTH = 100;        //回合间隔
    const int GAME_UPDATE_PER_TURN = 5; //每回合逻辑帧数
    const int GAME_UPDATE_LENGTH = TURN_LENGTH / GAME_UPDATE_PER_TURN;  //定义逻辑帧间隔
    const int PURSUE_GAME_UPDATE_NUM = GAME_UPDATE_PER_TURN * 20;   //追赶需要的逻辑帧数
    const int PURSUE_TIME_LENGTH = PURSUE_GAME_UPDATE_NUM * GAME_UPDATE_LENGTH; //追赶需要的时长
    int pendingGameUpdateNum = 0;   //待执行的逻辑帧数量
    float minGameUpdateLength = 0f;   //最小逻辑帧间隔
    float exactGameUpdateLength = GAME_UPDATE_LENGTH;   //实际逻辑帧间隔
    float acumulativeTime = 0f;         //累积时间
    float acumulativeTimeServer = 0f;   //累积时间（服务器进度）
    int gameUpdateIndexInTurnServer = 0;    //回合中第几个逻辑帧（服务器进度）

    void Awake()
    {
        minGameUpdateLength = Time.fixedDeltaTime * 1000;
        Instance = this;
        TurnManager.Instance.onNewTurn += OnNewTurn;
        MotionSampleManager.Instance.LoadFromXml();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause = !Pause;
        if (Pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        GameView.Instance.Update();     //逻辑层 -> 显示层
        NetworkManager.Instance.Update();
        InputManager.Instance.ReadInput();
    }

    void FixedUpdate()
    {
        //计数回合内逻辑帧（服务器进度）
        acumulativeTimeServer += Time.fixedDeltaTime * 1000;
        if (acumulativeTimeServer >= GAME_UPDATE_LENGTH)
        {
            acumulativeTimeServer -= GAME_UPDATE_LENGTH;
            ++gameUpdateIndexInTurnServer;
        }

        //如果预测平均回合间隔将大于定义回合间隔，则可能出现等回合。通过减慢逻辑帧速度，平滑等帧的卡顿
        float turnInterval = TurnManager.Instance.averageTurnInterval;
        if (turnInterval > ((float)TURN_LENGTH / 1000 + 0.2f))
        {
            float factor = turnInterval / ((float)TURN_LENGTH / 1000);
            //Logger.Log("Scale exactGameUpdateLength: " + factor);
            exactGameUpdateLength = exactGameUpdateLength * factor;
        }
        //执行逻辑帧
        acumulativeTime += Time.fixedDeltaTime * 1000;
        if (acumulativeTime >= exactGameUpdateLength)
        {
            acumulativeTime -= exactGameUpdateLength;

            //计算需执行的逻辑帧数量（叠帧）
            int gameUpdateNum = CalcGameUpdateNumPerLoop();
            for (int i = 0; i < gameUpdateNum && pendingGameUpdateNum > 0; ++i)
            {
                //处理回合
                bool toProcessTurn = (Game.Instance.CurFrame % GAME_UPDATE_PER_TURN == 0);
                if (toProcessTurn)
                {
                    FrameInfo nextTurn = TurnManager.Instance.NextTurn();
                    if (nextTurn == null)   //等回合，不执行逻辑帧
                        break;
                    Game.Instance.ProcessTurn(nextTurn);
                }

                //Logger.Log("Turn:" + TurnManager.Instance.CurTurnID + " GameUpdate:" + Game.Instance.CurFrame + 
                //    " PendingGameUpdateNum:" + pendingGameUpdateNum);
                //执行逻辑帧
                Game.Instance.Update(GAME_UPDATE_LENGTH);
                //发送关键数据校验
                if (toProcessTurn)
                    GameMsgSender.SendTurnValidate(Game.Instance.playerInfos);

                --pendingGameUpdateNum;
            }
        }

        NetworkManager.Instance.FixedUpdate(Time.fixedDeltaTime);
    }

    void OnNewTurn(FrameInfo info)
    {
        pendingGameUpdateNum += GAME_UPDATE_PER_TURN;
        gameUpdateIndexInTurnServer = 0;
        acumulativeTimeServer = 0f;


        //gameUpdateSpeedUpLevel = Mathf.CeilToInt((float)pendingGameUpdateNum / GAME_UPDATE_PER_TURN);
        //if (gameUpdateSpeedUpLevel != 1)
        //{
        //    Logger.Log("GameUpdateSpeedUpLevel: " + gameUpdateSpeedUpLevel);
        //    Logger.Log("Turn:" + TurnManager.Instance.CurTurnID + " ServerTurn:" + TurnManager.Instance.ServerTurnID
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
