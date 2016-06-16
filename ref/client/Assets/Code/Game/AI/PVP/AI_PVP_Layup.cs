using UnityEngine;

public class AI_PVP_Layup : AIState
{
	private bool m_bToLayup = false;

	public AI_PVP_Layup(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePVP_Layup;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		IM.Vector3 dirShoot = IM.Quaternion.AngleAxis(IM.Random.Range(-new IM.Number(90),new IM.Number(90)), IM.Vector3.up) * (-IM.Vector3.forward);
		IM.Vector3 vMoveTarget = m_basket.m_vShootTarget + dirShoot * new IM.Number(3);
		vMoveTarget.y = IM.Number.zero;
		m_moveTarget = vMoveTarget;
	}

	public override void ArriveAtMoveTarget ()
	{
		m_bToLayup = true;
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetSkillById(7051);
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		
		if( m_player.m_toSkillInstance == null && m_bToLayup )
		{
			if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eLayup)
				m_system.SetTransaction(AIState.Type.ePVP_Idle);
		}
	}
	
	override public void OnExit ()
	{
		m_bToLayup = false;
		m_player.m_toSkillInstance = null;
	}
}