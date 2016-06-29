using IM;

public class MoveController
{
    public static Number ANGLE_PER_DIR = new Number(360) / new Number(InputReader.DIR_NUM);

    private Vector3 _position;
    public Vector3 position{
        get{return _position;}
        set
        {
            /*
            UnityEngine.Debug.LogFormat("Set player position, {0} {1} {2} -> {3}", 
                player.m_team.m_side, player.m_id, _position, value);
            //*/
            _position = value;
        }
    }
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
    Player player;

    public MoveController(Player player)
    {
        forward = Vector3.forward;
        this.player = player;
            //Logger.Log(string.Format("Force:{0} Rotated dir:{1}", force, forward));
        //Logger.Log(string.Format("Velocity:{0} {1}", velocity, velocity.magnitude));
    }

    public void Move(IM.Vector3 delta)
    {
        position += delta;
    }
}
