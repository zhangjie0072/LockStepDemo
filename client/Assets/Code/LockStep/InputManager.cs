using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public enum Direction
{
    Null = 0,
    Forward = 1,
    ForwardR,
    ForwardRight,
    FRight,
    Right,
    BRight,
    BackRight,
    BackR,
    Back,
    BackL,
    BackLeft,
    BLeft,
    Left,
    FLeft,
    ForwardLeft,
    ForwardL,
    None,
    Max,
}


public enum Command
{
    Action1 = 1,
    Action2,
    Action3,
    None,
}

public class InputManager : Singleton<InputManager>
{
    Direction curDir = Direction.None;
    Command curCmd = Command.None;
    public const int DIR_NUM = (int)Direction.Max - 2;
    public const float ANGLE_PER_DIR = 360 / DIR_NUM;

    public void ReadInput()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Direction dir = ConvertToDirection(hori, vert);

        Command cmd = Command.None;
        if (Input.GetKey(KeyCode.J))
            cmd = Command.Action1;
        else if (Input.GetKey(KeyCode.K))
            cmd = Command.Action2;
        else if (Input.GetKey(KeyCode.L))
            cmd = Command.Action3;

        if (curCmd != cmd || curDir != dir)
        {
            //Logger.Log("SendInput " + dir + " " + cmd);
            GameMsgSender.SendInput(dir, cmd);
        }
        curCmd = cmd;
        curDir = dir;
    }

    static Direction ConvertToDirection(float hori, float vert)
    {
        if (Mathf.Approximately(hori, 0f) && Mathf.Approximately(vert, 0f))
            return Direction.None;
        Vector3 vec = new Vector3(hori, 0f, vert);
        vec.Normalize();
        float horiAngle = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
        int dir = (int)(horiAngle / ANGLE_PER_DIR);
        return (Direction)(dir + 1);
    }
}
