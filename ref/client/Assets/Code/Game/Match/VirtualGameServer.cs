using System;
using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class VirtualGameServer : Singleton<VirtualGameServer>
{
    public const float TURN_LENGTH = (float)TurnController.TURN_LENGTH / 1000;
    float acumulativeTime = 0f;

    uint curTurnID = 1;
    bool running = false;
    List<FrameInfo> turns = new List<FrameInfo>();
    Dictionary<uint, ClientInput> lastInputs = new Dictionary<uint, ClientInput>();
    int playerNum = 1;
    int readyPlayerNum = 0;

    public void Reset()
    {
        curTurnID = 1;
        turns.Clear();
        lastInputs.Clear();
        running = false;
        playerNum = 1;
        readyPlayerNum = 0;
    }

    public void Stop()
    {
        running = false;
    }

    public void OnGameBegin(GameBeginReq req)
    {
        if (running)
            return;

        ClientInput input = new ClientInput();
        input.acc_id = req.acc_id;
        lastInputs.Add(req.acc_id, input);

        ++readyPlayerNum;
        if (readyPlayerNum >= playerNum)
        {
            running = true;
            GameBeginResp resp = new GameBeginResp();
            GameMsgSender.SendGameBeginResp(resp);
        }
    }

    public void AddInput(ClientInput input)
    {
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

        acumulativeTime += deltaTime;
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
            turn.info.Add(pair.Value);
        }
        turn.time = Time.time;

        turns.Add(turn);
        GameMsgSender.SendTurn(turn);
    }
}
