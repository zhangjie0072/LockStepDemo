using UnityEngine;
using fogs.proto.msg;

public class PlayerState_RequireBall : PlayerState_Skill
{
	private IM.Number 	m_runningSpeed;
	private int		m_lastMoveDir;
	

	public PlayerState_RequireBall (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eRequireBall;

		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_0, "requireBallStand");
		m_mapAnimType.Add(fogs.proto.msg.AnimType.N_TYPE_1, "requireBallRun");

		m_animType = AnimType.N_TYPE_0;
		m_lastMoveDir = -1;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		m_player.m_moveType = MoveType.eMT_RequireBall;

		if( !m_player.m_bSimulator )
		{
			if( lastState.m_eState == PlayerState.State.eStand )
				m_animType = AnimType.N_TYPE_0;
			else if( lastState.m_eState == PlayerState.State.eRun )
				m_animType = AnimType.N_TYPE_1;

			m_player.m_dir = -1;
			m_lastMoveDir = -1;
			GameMsgSender.SendRequireBall(m_player, m_curExecSkill, m_animType);
		}
		PlayerMovement.MoveAttribute attr = m_player.mMovements[(int)PlayerMovement.Type.eRunWithoutBall].mAttr;
        m_runningSpeed = attr.m_curSpeed;
        m_turningSpeed = attr.m_TurningSpeed;

		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.CrossFade(m_curAction, false);
		
		m_player.eventHandler.OnRequireBall();
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_animType == AnimType.N_TYPE_1 )
		{
			IM.Vector3 dirVelocity = m_player.moveDirection.normalized;
			m_player.MoveTowards(dirVelocity, m_turningSpeed ,fDeltaTime, dirVelocity * m_runningSpeed);

			if( !m_player.m_bSimulator && m_player.m_dir != m_lastMoveDir )
			{
				m_lastMoveDir = m_player.m_dir;
				GameMsgSender.SendMove(m_player, MoveType.eMT_RequireBall, m_animType);
			}
		}
	}
}