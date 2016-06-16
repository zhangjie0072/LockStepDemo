using UnityEngine;
using fogs.proto.msg;

public class AI_PVP_Dunk : AIState
{
	private const int	m_iNearDunk 	= 7100;
	private const int	m_iFarDunk	 	= 7101;

	private bool m_bToDunk = false;

	public AI_PVP_Dunk(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePVP_Dunk;
	}
	
	override public void OnEnter ( AIState lastState )
	{
        IM.Vector3 dirShoot = IM.Quaternion.AngleAxis(IM.Random.Range(-new IM.Number(90), new IM.Number(90)), IM.Vector3.up) * (-IM.Vector3.forward);
		IM.Vector3 vMoveTarget = m_basket.m_vShootTarget + dirShoot * new IM.Number(3); 
		vMoveTarget.y = IM.Number.zero;
		m_moveTarget = vMoveTarget;
	}

	public override void ArriveAtMoveTarget ()
	{
		Area eArea = m_match.mCurScene.mGround.GetDunkArea(m_player);
		if( eArea == Area.eNear )
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(m_iNearDunk);
		if( eArea == Area.eMiddle )
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(m_iFarDunk);

		m_bToDunk = true;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		
		if( m_player.m_toSkillInstance == null && m_bToDunk )
		{
			if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eDunk)
				m_system.SetTransaction(AIState.Type.ePVP_Idle);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
		m_bToDunk = false;
	}
}
