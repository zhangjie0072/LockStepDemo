using UnityEngine;
using fogs.proto.msg;

public class AI_PVP_Shoot : AIState
{
	private	bool	m_bBeginShoot = false;

	public AI_PVP_Shoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePVP_Shoot;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		IM.Vector3 dirShoot = IM.Quaternion.AngleAxis(IM.Random.Range(-new IM.Number(90),new IM.Number(90)), IM.Vector3.up) * (-IM.Vector3.forward);
		IM.Vector3 moveTarget = m_basket.m_vShootTarget + dirShoot * new IM.Number(3);
		moveTarget.y = IM.Number.zero;
		m_moveTarget = moveTarget;
	}

	public override void ArriveAtMoveTarget ()
	{
		m_player.m_toSkillInstance = ShootHelper.ShootByArea(m_player, m_match);
		m_player.m_bForceShoot = true;

		m_bBeginShoot = true;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_bBeginShoot && m_player.m_toSkillInstance == null )
		{
			if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
				m_system.SetTransaction(AIState.Type.ePVP_Idle);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
		m_bBeginShoot = false;
	}
}