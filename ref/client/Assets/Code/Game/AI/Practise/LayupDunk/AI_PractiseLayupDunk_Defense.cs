using UnityEngine;
using System.Collections.Generic;

public class AI_PractiseLayupDunk_Defense : AIState
{
	private  static IM.Number m_fDefenseDist = IM.Number.one;
	
	private IM.Vector3 m_basketCenter;
	
	public AI_PractiseLayupDunk_Defense(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePractiseLayupDunk_Defense;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		m_basketCenter = m_match.mCurScene.mBasket.m_vShootTarget;
		m_basketCenter.y = IM.Number.zero;

		if( m_player.m_moveHelper != null )
			m_player.m_moveHelper.StopMove();
	}
	
	override public void Update(IM.Number fDeltaTime)
	{
		IM.Vector3 newPos = GetNPCStandPos();
		if (GameUtils.HorizonalDistance(newPos, m_moveTarget) > new IM.Number(0,700))
			m_moveTarget = newPos;

		base.Update(fDeltaTime);

		Player defenseTarget = m_player.m_defenseTarget;
		if(defenseTarget != null && defenseTarget.m_bWithBall)
		{
			PlayerState.State eCurState = defenseTarget.m_StateMachine.m_curState.m_eState;
			if( eCurState == PlayerState.State.ePrepareToShoot || 
				eCurState == PlayerState.State.eShoot)
			{
				if( GameUtils.HorizonalDistance(m_player.position, m_player.m_defenseTarget.position) < m_fDefenseDist &&
					defenseTarget.m_bOnGround)
					m_system.SetTransaction(AIState.Type.ePractiseLayupDunk_Block);
			}
			else if(eCurState == PlayerState.State.eDunk ||
				eCurState == PlayerState.State.eLayup )
			{
				m_system.SetTransaction(AIState.Type.ePractiseLayupDunk_Block);
			}
		}
		else
			m_player.m_moveHelper.StopMove();
	}
	
	IM.Vector3 GetNPCStandPos()
	{
		IM.Vector3 dir = m_match.m_mainRole.position - m_basketCenter;
		dir.Normalize();
		return m_basketCenter + dir * IM.Number.two;
	}
}