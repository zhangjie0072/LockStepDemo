using UnityEngine;
using fogs.proto.msg;

public class MoveToHelper
{
    public System.Action onMoveToTarget;

    public IM.Vector3 targetPosition { private set; get; }
    public IM.Number movingSpeed;

    private Player _owner;
    private bool _bMoveToTarget;

	public MoveToHelper(Player player)
	{
		_owner = player;
	}

	public void MoveTo( IM.Vector3 vTarget )
	{
		targetPosition = vTarget;
		_bMoveToTarget = true;
		_owner.m_moveType = MoveType.eMT_Run;
	}

	public void StopMove(MoveType nextMove = MoveType.eMT_Stand)
	{
		_owner.moveDirection = IM.Vector3.zero;
		_bMoveToTarget = false;
		_owner.m_moveType = nextMove;
	}

	public void Update(IM.Number deltaTime)
	{
		if( !_bMoveToTarget || !_owner.m_enableMovement)
		{
			Debugger.Instance.DrawSphere("move target " + _owner.m_id, (Vector3)targetPosition, Color.green);
			return;
		}

		Debugger.Instance.DrawSphere("move target " + _owner.m_id, (Vector3)targetPosition, Color.red);

		//calculate by delta time
		IM.Number distance = GameUtils.HorizonalDistance(targetPosition, _owner.position);
		IM.Number movingTime = IM.Number.zero;
        if (!IM.Number.Approximately(distance, IM.Number.zero))
        {
            if (movingSpeed == IM.Number.zero)
                movingTime = IM.Number.max;
            else
                movingTime = distance / movingSpeed;
        }
		if( deltaTime > movingTime )
		{
			targetPosition = _owner.position;
			_bMoveToTarget = false;
			_owner.m_dir = -1;

			if( onMoveToTarget != null )
				onMoveToTarget();
			else
				Logger.LogError("no move to msg received.");
		}
		else
		{
			IM.Vector3 dirMove = GameUtils.HorizonalNormalized(targetPosition, _owner.position);
            IM.Number angle = IM.Vector3.FromToAngle(IM.Vector3.forward, dirMove);
			IM.Vector3 vel;
			IM.Number dir;
			GameUtils.AngleToDir(angle, out dir, out vel);
			_owner.m_dir = (int)(float)(dir);
		}
	}
}