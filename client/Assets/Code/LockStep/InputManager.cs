using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public enum Direction
{
    Null = 0,
    Forward = 1,
    ForwardRight = 2,
    Right = 3,
    BackRight = 4,
    Back = 5,
    BackLeft = 6,
    Left = 7,
    ForwardLeft = 8,
    None = 9,
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

    public void ReadInput()
    {
        Direction dir = Direction.None;
        bool forward = Input.GetKey(KeyCode.W);
        bool left = Input.GetKey(KeyCode.A);
        bool back = Input.GetKey(KeyCode.S);
        bool right = Input.GetKey(KeyCode.D);

        if (forward && left)
            dir = Direction.ForwardLeft;
        else if (forward && right)
            dir = Direction.ForwardRight;
        else if (back && left)
            dir = Direction.BackLeft;
        else if (back && right)
            dir = Direction.BackRight;
        else if (forward)
            dir = Direction.Forward;
        else if (back)
            dir = Direction.Back;
        else if (left)
            dir = Direction.Left;
        else if (right)
            dir = Direction.Right;

        Command cmd = Command.None;
        if (Input.GetKey(KeyCode.J))
            cmd = Command.Action1;
        else if (Input.GetKey(KeyCode.K))
            cmd = Command.Action2;
        else if (Input.GetKey(KeyCode.L))
            cmd = Command.Action3;

        if (curCmd != cmd || curDir != dir)
        {
            Logger.Log("SendInput " + dir + " " + cmd);
            GameMsgSender.SendInput(dir, cmd);
        }
        curCmd = cmd;
        curDir = dir;
    }
}
