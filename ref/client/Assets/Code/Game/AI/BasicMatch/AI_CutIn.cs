using UnityEngine;
using System.Collections.Generic;

public class AI_CutIn : AIState
{
	const uint LEFT_ACTION_ID = 74001;
	const uint RIGHT_ACTION_ID = 74003;

	private AIState m_lastState;
	
	public AI_CutIn(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eCutIn;
	}
	
	public override void OnEnter ( AIState lastState )
	{
		m_lastState = lastState;
		
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.CutIn, true);
		if (m_player.m_toSkillInstance != null)
		{
			if (m_player.position.z < m_match.mCurScene.mGround.mHalfSize.y - 1)
			{
				Player defenser = m_player.GetNearestDefender();
				IM.Vector3 dirPlayerToDefenser = (defenser.position - m_player.position).normalized;
                IM.Number angle = IM.Vector3.FromToAngle(dirPlayerToDefenser, m_player.right);
				IM.Number devAngle = m_system.AI.devAngleDefender;
				angle *= IM.Number.one + IM.Random.Range(-devAngle, devAngle);
				bool left = angle < new IM.Number(90);

				m_player.m_toSkillInstance.curActionId = left ? LEFT_ACTION_ID : RIGHT_ACTION_ID;
			}
			else
				m_player.m_toSkillInstance.curActionId = m_player.position.x > 0 ? LEFT_ACTION_ID : RIGHT_ACTION_ID;
		}
		else
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI CutIn");
	}

    public override void Update(IM.Number fDeltaTime)
	{
		PlayerState state =	m_player.m_StateMachine.m_curState;
		if( state.m_eState != PlayerState.State.eCutIn )
		{
			m_system.SetTransaction(m_lastState);
			return;
		}
	}
	
	public override void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}