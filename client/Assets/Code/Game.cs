using System.Collections.Generic;
using fogs.proto.msg;
using IM;

public class PlayerInfo
{
    public uint ID;
    public Vector3 position;
    public Vector3 forward;
    public Vector3 force;
    public Vector3 velocity;
    public uint state;
}

public class Game : Singleton<Game>
{
    static List<Vector3> positions = new List<Vector3>();
    static Number PLAYER_MAX_SPEED = new Number(3);
    static Number PLAYER_ACC_SPEED = new Number(3);
    static Number PLAYER_TURN_SPEED = Math.Deg2Rad(new Number(360));
    static Number ANGLE_PER_DIR = new Number(360) / new Number(InputManager.DIR_NUM);
    public Dictionary<uint, PlayerInfo> playerInfos { get; private set; }
    public int CurFrame { get; private set; }

    public void Initialize(List<uint> players)
    {
        positions.Add(Vector3.left);
        positions.Add(Vector3.right);

        playerInfos = new Dictionary<uint, PlayerInfo>();
        for (int i = 0; i < players.Count; ++i)
        {
            PlayerInfo info = new PlayerInfo();
            info.ID = players[i];
            info.position = positions[i];
            info.forward = Vector3.forward;
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
            int d = (int)dir - (int)Direction.Forward;
            Number angle = ANGLE_PER_DIR * new Number(d);
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            info.force = direction;
        }
        else
            info.force = Vector3.zero;

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
            info.position = info.position + info.velocity * dt;

            Number speed = info.velocity.magnitude;
            if (info.force != Vector3.zero)
            {
                info.forward = Vector3.RotateTowards(info.forward, info.force, PLAYER_TURN_SPEED * dt, Number.zero);
                //Logger.Log(string.Format("Force:{0} Rotated dir:{1}", info.force, info.forward));
            }
            if (info.force != Vector3.zero)
            {
                speed += PLAYER_ACC_SPEED * dt;
                if (speed > PLAYER_MAX_SPEED)
                    speed = PLAYER_MAX_SPEED;
                info.velocity = info.force.normalized * speed;
            }
            else
            {
                speed -= PLAYER_ACC_SPEED * dt;
                if (speed < Number.zero)
                    speed = Number.zero;
                info.velocity = info.velocity.normalized * speed;
            }
            //Logger.Log(string.Format("Velocity:{0} {1}", info.velocity, info.velocity.magnitude));
        }

        ++CurFrame;
    }
}
