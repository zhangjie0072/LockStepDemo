using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class GameSystem : MonoBehaviour
{
    static public GameSystem Instance;
    public bool Pause;
    public uint AccountID;

    const float TURN_LENGTH = 0.1f;
    const int GAME_UPDATE_PER_TURN = 5;
    const float GAME_UPDATE_LENGTH = TURN_LENGTH / GAME_UPDATE_PER_TURN;
    const int PURSUE_GAME_UPDATE_NUM = GAME_UPDATE_PER_TURN * 20;
    const float PURSUE_TIME_LENGTH = PURSUE_GAME_UPDATE_NUM * GAME_UPDATE_LENGTH;
    int pendingGameUpdateNum = 0;
    float gameUpdateLengthLimit = 0f;
    float exactGameUpdateLength = GAME_UPDATE_LENGTH;
    float acumulativeTime = 0f;
    int gameUpdateSpeedUpLevel = 1;
    float acumulativeTimeServer = 0f;
    int gameUpdateIndexInTurnServer = 0;

    void Awake()
    {
        gameUpdateLengthLimit = Time.fixedDeltaTime;
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

        NetworkManager.Instance.Update();
        InputManager.Instance.ReadInput();
    }

    void FixedUpdate()
    {
        acumulativeTimeServer += Time.fixedDeltaTime;
        if (acumulativeTimeServer >= GAME_UPDATE_LENGTH)
        {
            acumulativeTimeServer -= GAME_UPDATE_LENGTH;
            ++gameUpdateIndexInTurnServer;
        }

        acumulativeTime += Time.fixedDeltaTime;
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
            GameView.Instance.Update();
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
