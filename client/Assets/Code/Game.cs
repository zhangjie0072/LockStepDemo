using System;
using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class PlayerInfo
{
    public uint ID;
    public Vector3 position;
    public Vector3 velocity;
    public uint state;
}

public class Game : Singleton<Game>
{
    static List<Vector3> positions = new List<Vector3>();
    const float PLAYER_SPEED = 10f;
    public Dictionary<uint, PlayerInfo> playerInfos { get; private set; }
    public int CurFrame { get; private set; }

    public void Initialize(List<uint> players)
    {
        positions.Add(new Vector3() { x = -1f, y = 0f, z = 0f });
        positions.Add(new Vector3() { x = 1f, y = 0f, z = 0f });

        playerInfos = new Dictionary<uint, PlayerInfo>();
        for (int i = 0; i < players.Count; ++i)
        {
            PlayerInfo info = new PlayerInfo();
            info.ID = players[i];
            info.position = positions[i];
            playerInfos.Add(info.ID, info);
        }
    }

    public void ProcessTurn(FrameInfo turn)
    {
        foreach (var info in playerInfos)
        {
            List<ClientInput> inputs = turn.info;
            ClientInput input = inputs.Find(i => i.acc_id == info.Value.ID);
            ProcessPlayerInput(input, info.Value);
        }
    }

    void ProcessPlayerInput(ClientInput input, PlayerInfo info)
    {
        Direction dir = Direction.None;
        Command cmd = Command.None;
        if (input != null)
        {
            dir = (Direction)input.dir;
            cmd = (Command)input.cmd;
        }

        //Logger.Log("ProcessPlayerInput " + info.ID + " " + dir + " " + cmd);

        if (dir != Direction.None && dir != Direction.Null)
        {
            float angle = 0f;
            switch (dir)
            {
                case Direction.Forward:
                    angle = 0f;
                    break;
                case Direction.Back:
                    angle = 180f;
                    break;
                case Direction.Left:
                    angle = 270f;
                    break;
                case Direction.Right:
                    angle = 90f;
                    break;
                case Direction.ForwardLeft:
                    angle = 315f;
                    break;
                case Direction.ForwardRight:
                    angle = 45f;
                    break;
                case Direction.BackLeft:
                    angle = 225f;
                    break;
                case Direction.BackRight:
                    angle = 135f;
                    break;
            }

            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            info.velocity = direction * PLAYER_SPEED;
        }
        else
            info.velocity = Vector3.zero;

        switch (cmd)
        {
            case Command.Action1:
                info.state = 1;
                break;
            case Command.Action2:
                info.state = 2;
                break;
            case Command.Action3:
                info.state = 3;
                break;
            case Command.None:
                info.state = 0;
                break;
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var pair in playerInfos)
        {
            PlayerInfo info = pair.Value;
            info.position = info.position + info.velocity * deltaTime;
        }

        ++CurFrame;
    }
}
