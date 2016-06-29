using UnityEngine;
using System.Collections.Generic;

/**ÒªÇò*/
public class AI_RequireBall
	: AIState
{
	private AIState m_lastState;

	public AI_RequireBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eRequireBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.RequireBall, true);
		if (m_player.m_toSkillInstance == null)
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI RequireBall");

		if( lastState.m_eType == Type.ePositioning )
		{
			m_lastState = lastState;
			m_moveTarget = lastState.m_moveTarget;
		}
		else
			m_lastState = null;

	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if(m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRequireBall)
		{
			if( m_lastState != null )
				m_system.SetTransaction(m_lastState);
			else
				m_system.SetTransaction(Type.eIdle);
		}

		if( GameUtils.HorizonalDistance(m_moveTarget, m_player.position) < new IM.Number(0,100) )
		{
			int iCollideSector = RoadPathManager.Instance.CalcSectorIdx(m_moveTarget);
			if (iCollideSector == -1)
				iCollideSector = 0;
			RoadPathManager.Sector targetSector = RoadPathManager.Instance.Bounce( m_player.position.xz, RoadPathManager.Instance.m_Sectors[iCollideSector], m_player.m_favorSectors );
			RoadPathManager.Instance.AddDrawSector("targetSector", targetSector);
			m_moveTarget = targetSector.center.x0z;
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}