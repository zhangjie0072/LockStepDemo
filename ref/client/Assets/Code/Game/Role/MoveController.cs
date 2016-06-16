using IM;

public class MoveController
{
    static Number PLAYER_MAX_SPEED = new Number(3);
    static Number PLAYER_ACC_SPEED = new Number(3);
    static Number PLAYER_TURN_SPEED = Math.TWO_PI;
    static Number ANGLE_PER_DIR = new Number(360) / new Number(8); //TODO:IMath new Number(InputManager.DIR_NUM);

    public EDirection dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            if (dir != EDirection.eNone) //TODO:IMath && dir != Direction.Null)
            {
                int d = (int)dir - (int)EDirection.eForward;
                Number angle = ANGLE_PER_DIR * new Number(d);
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                force = direction;
            }
            else
                force = Vector3.zero;
        }
    }
    EDirection _dir;

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 forward
    {
        get { return rotation * Vector3.forward; }
        set { rotation = Quaternion.LookRotation(value); }
    }
    public Vector3 up
    {
        get { return rotation * Vector3.up; }
    }
    public Vector3 right
    {
        get { return rotation * Vector3.right; }
    }
    public Vector3 scale;
    public Vector3 moveDirection;
    Vector3 force;

    public MoveController()
    {
        forward = Vector3.forward;
    }

    public void Update(Number deltaTime)
    {
        position = position + moveDirection * deltaTime;

        Number speed = moveDirection.magnitude;
        if (force != Vector3.zero)
        {
            forward = Vector3.RotateTowards(forward, force, PLAYER_TURN_SPEED * deltaTime, Number.zero);
            //Logger.Log(string.Format("Force:{0} Rotated dir:{1}", force, forward));
        }
        if (force != Vector3.zero)
        {
            speed += PLAYER_ACC_SPEED * deltaTime;
            if (speed > PLAYER_MAX_SPEED)
                speed = PLAYER_MAX_SPEED;
            moveDirection = force.normalized * speed;
        }
        else
        {
            speed -= PLAYER_ACC_SPEED * deltaTime;
            if (speed < Number.zero)
                speed = Number.zero;
            moveDirection = moveDirection.normalized * speed;
        }
        //Logger.Log(string.Format("Velocity:{0} {1}", velocity, velocity.magnitude));
    }

    public void Move(IM.Vector3 delta)
    {
        position += delta;
    }
}
