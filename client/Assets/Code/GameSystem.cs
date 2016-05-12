using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class GameSystem : MonoBehaviour
{
    static public GameSystem Instance;
    public bool Pause;
    public uint AccountID;

    const int TURN_LENGTH = 100;
    const int GAME_UPDATE_PER_TURN = 5;
    const int GAME_UPDATE_LENGTH = TURN_LENGTH / GAME_UPDATE_PER_TURN;
    const int PURSUE_GAME_UPDATE_NUM = GAME_UPDATE_PER_TURN * 20;
    const int PURSUE_TIME_LENGTH = PURSUE_GAME_UPDATE_NUM * GAME_UPDATE_LENGTH;
    int pendingGameUpdateNum = 0;
    float gameUpdateLengthLimit = 0f;
    float exactGameUpdateLength = GAME_UPDATE_LENGTH;
    float acumulativeTime = 0f;
    float acumulativeTimeServer = 0f;
    int gameUpdateIndexInTurnServer = 0;

    void Awake()
    {
        gameUpdateLengthLimit = Time.fixedDeltaTime * 1000;
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

        GameView.Instance.Update();
        NetworkManager.Instance.Update();
        InputManager.Instance.ReadInput();
    }

    void FixedUpdate()
    {
        acumulativeTimeServer += Time.fixedDeltaTime * 1000;
        if (acumulativeTimeServer >= GAME_UPDATE_LENGTH)
        {
            acumulativeTimeServer -= GAME_UPDATE_LENGTH;
            ++gameUpdateIndexInTurnServer;
        }

        float turnInterval = TurnManager.Instance.averageTurnInterval;
        if (turnInterval > ((float)TURN_LENGTH / 1000 + 0.2f))
        {
            float factor = turnInterval / ((float)TURN_LENGTH / 1000);
            //Logger.Log("Scale exactGameUpdateLength: " + factor);
            exactGameUpdateLength = exactGameUpdateLength * factor;
        }
        acumulativeTime += Time.fixedDeltaTime * 1000;
        if (acumulativeTime >= exactGameUpdateLength)
        {
            acumulativeTime -= exactGameUpdateLength;

            int gameUpdateNum = CalcGameUpdateNumPerLoop();
            for (int i = 0; i < gameUpdateNum && pendingGameUpdateNum > 0; ++i)
            {
                bool toProcessTurn = (Game.Instance.CurFrame % GAME_UPDATE_PER_TURN == 0);
                if (toProcessTurn)
                {
                    FrameInfo nextTurn = TurnManager.Instance.NextTurn();
                    if (nextTurn != null)
                    {
                        Game.Instance.ProcessTurn(nextTurn);
                    }
                }

                //Logger.Log("Turn:" + TurnManager.Instance.CurTurnID + " GameUpdate:" + Game.Instance.CurFrame + 
                //    " PendingGameUpdateNum:" + pendingGameUpdateNum);
                Game.Instance.Update(GAME_UPDATE_LENGTH);
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

    int CalcGameUpdateNumPerLoop()
    {
        int delayedGameUpdateNum = (pendingGameUpdateNum - GAME_UPDATE_PER_TURN + gameUpdateIndexInTurnServer);
        float speed = 1 + (float)delayedGameUpdateNum / PURSUE_GAME_UPDATE_NUM;
        exactGameUpdateLength = GAME_UPDATE_LENGTH / speed;
        if (exactGameUpdateLength < gameUpdateLengthLimit)
        {
            exactGameUpdateLength = gameUpdateLengthLimit;
            float pursueGameUpdateNum = PURSUE_TIME_LENGTH / exactGameUpdateLength;
            speed = 1 + (float)delayedGameUpdateNum / pursueGameUpdateNum;
        }
        return Mathf.CeilToInt(speed);
    }
}
