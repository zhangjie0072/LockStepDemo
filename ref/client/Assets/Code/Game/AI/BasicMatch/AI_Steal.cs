using UnityEngine;
using System.Collections.Generic;

/**ÇÀ¶Ï*/
public class AI_Steal
	: AIState
{
	bool stealed = false;
	public AI_Steal(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eSteal;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		base.OnEnter(lastState);

		stealed = false;
	}
	
	public override void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if(stealed && m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eSteal)
			m_system.SetTransaction(AIState.Type.eDefense);

		IM.Vector3 dirTargetToMe = m_player.position - m_player.m_defenseTarget.position;
		dirTargetToMe.y = IM.Number.zero;
		if (!stealed)
		{
			if (dirTargetToMe.magnitude * (IM.Number.one - m_system.AI.devDistFront) < new IM.Number(1,500))
			{
				m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Steal, true);
				if (m_player.m_toSkillInstance == null)
					Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Steal");
				(m_player.m_StateMachine.GetState(PlayerState.State.eSteal) as PlayerState_Steal).forcedByAI = true;
				stealed = true;
			}
			else
			{
				m_moveTarget = m_player.m_defenseTarget.position;
			}
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}