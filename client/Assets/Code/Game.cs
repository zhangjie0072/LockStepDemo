using System.Collections.Generic;
using fogs.proto.msg;
using IM;

public class PlayerInfo
{
    public uint ID;
    public MoveController moveCtrl;
    public uint state;
}

public class Game : Singleton<Game>
{
    static List<Vector3> initialPositions = new List<Vector3>();
    public Dictionary<uint, PlayerInfo> playerInfos { get; private set; }
    public int CurFrame { get; private set; }

    public void Initialize(List<uint> players)
    {
        initialPositions.Add(Vector3.left);
        initialPositions.Add(Vector3.right);

        playerInfos = new Dictionary<uint, PlayerInfo>();
        for (int i = 0; i < players.Count; ++i)
        {
            PlayerInfo info = new PlayerInfo();
            info.ID = players[i];
            info.moveCtrl = new MoveController();
            info.moveCtrl.position = initialPositions[i];
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

        info.moveCtrl.dir = dir;

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

    // deltaTime in milliseconds
    public void Update(int deltaTime)
    {
        Number dt = new Number(0, deltaTime);
        foreach (var pair in playerInfos)
        {
            PlayerInfo info = pair.Value;
            info.moveCtrl.Update(dt);
        }

        ++CurFrame;
    }
}
