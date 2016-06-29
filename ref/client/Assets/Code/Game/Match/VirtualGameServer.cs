using System;
using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class VirtualGameServer
{
    public static VirtualGameServer Instance;
    public const float TURN_LENGTH = (float)TurnController.TURN_LENGTH / 1000;
    public float timeScale = 1f;
    float acumulativeTime = 0f;

    uint curTurnID = 0;
    bool running = false;
    List<FrameInfo> turns = new List<FrameInfo>();
    Dictionary<uint, ClientInput> lastInputs = new Dictionary<uint, ClientInput>();
    int playerNum = 1;
    int readyPlayerNum = 0;

    public VirtualGameServer()
    {
        Reset();
    }

    public void Reset()
    {
        curTurnID = 1;
        turns.Clear();
        lastInputs.Clear();
        running = false;
        playerNum = 1;
        readyPlayerNum = 0;
        timeScale = 1f;
    }

    public void Stop()
    {
        running = false;
    }

    public void Resume()
    {
        if (readyPlayerNum < playerNum)
            return;
        running = true;
        // 从暂停中恢复，必将马上执行一帧。主要为确保练习赛中的比赛暂停后恢复能确定执行到对应命令
        acumulativeTime = TURN_LENGTH;
    }

    public void OnGameBegin(GameBeginReq req)
    {
        if (running)
            return;

        ClientInput input = new ClientInput();
        input.acc_id = req.acc_id;
        lastInputs[req.acc_id] = input;

        ++readyPlayerNum;
        if (readyPlayerNum >= playerNum)
        {
            running = true;
            GameBeginResp resp = new GameBeginResp();
            resp.seed = (uint)IM.Random.Range(0L, (long)uint.MaxValue);
            GameMsgSender.SendGameBeginResp(resp);
        }
    }
    
    public void AddInput(ClientInput input)
    {
        //Logger.Log(string.Format("Handle input, {0} {1} running:{2}", 
        //    (InputDirection)input.dir, (Command)input.cmd, running));
        if (!running)
            return;

        ClientInput lastInput = null;
        if (!lastInputs.TryGetValue(input.acc_id, out lastInput))
        {
            lastInput = input;
            lastInputs.Add(lastInput.acc_id, lastInput);
        }
        else
        {
            if (input.dir != 0)
                lastInput.dir = input.dir;
            if (input.cmd != 0)
                lastInput.cmd = input.cmd;
        }
    }

    public void Update(float deltaTime)
    {
        if (!running)
            return;

        acumulativeTime += deltaTime * timeScale;
        if (acumulativeTime >= TURN_LENGTH)
        {
            acumulativeTime -= TURN_LENGTH;
            NewTurn();
        }
    }

    void NewTurn()
    {
        FrameInfo turn = new FrameInfo();
        turn.frameNum = curTurnID++;
        foreach (KeyValuePair<uint, ClientInput> pair in lastInputs)
        {
            ClientInput input = new ClientInput();
            input.acc_id = pair.Value.acc_id;
            input.dir = pair.Value.dir;
            input.cmd = pair.Value.cmd;
            turn.info.Add(input);
        }
        turn.time = Time.time;
        PlayFrame playFrame = new PlayFrame();
        playFrame.frames.Add(turn);

        turns.Add(turn);
        GameMsgSender.SendTurn(playFrame);
    }
}
