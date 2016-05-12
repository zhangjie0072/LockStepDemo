using IM;

public class MoveController
{
    static Number PLAYER_MAX_SPEED = new Number(3);
    static Number PLAYER_ACC_SPEED = new Number(3);
    static Number PLAYER_TURN_SPEED = Math.TWO_PI;
    static Number ANGLE_PER_DIR = new Number(360) / new Number(InputManager.DIR_NUM);

    public Direction dir
    {
        get { return _dir; }
        set
        {
            _dir = value;
            if (dir != Direction.None && dir != Direction.Null)
            {
                int d = (int)dir - (int)Direction.Forward;
                Number angle = ANGLE_PER_DIR * new Number(d);
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
                force = direction;
            }
            else
                force = Vector3.zero;
        }
    }
    Direction _dir;

    public Vector3 position;
    public Vector3 forward;
    public Vector3 velocity { get; private set; }
    Vector3 force;

    public MoveController()
    {
        forward = Vector3.forward;
    }

    public void Update(Number deltaTime)
    {
        position = position + velocity * deltaTime;

        Number speed = velocity.magnitude;
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
            velocity = force.normalized * speed;
        }
        else
        {
            speed -= PLAYER_ACC_SPEED * deltaTime;
            if (speed < Number.zero)
                speed = Number.zero;
            velocity = velocity.normalized * speed;
        }
        //Logger.Log(string.Format("Velocity:{0} {1}", velocity, velocity.magnitude));
    }
}
