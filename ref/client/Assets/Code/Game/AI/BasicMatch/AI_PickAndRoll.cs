using UnityEngine;
using System.Collections.Generic;

public class AI_PickAndRoll : AIState
{
	GameUtils.Timer timer;

	public AI_PickAndRoll(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePickAndRoll;
		timer = new GameUtils.Timer(new IM.Number(0,300), OnTimer);
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		CastSkill();
	}
	
	public override void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		CastSkill();

		if (m_player.m_bWithBall)
			Exit();
		else if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.ePickAndRoll && 
			(m_player.m_StateMachine.m_curState as PlayerState_PickAndRoll).m_bOnCollide)
			Exit();

		timer.Update(fDeltaTime);
	}

	void CastSkill()
	{
		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.PickAndRoll, true);
		if (m_player.m_toSkillInstance == null)
			Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI PickAndRoll");
	}

	void Exit()
	{
		m_system.SetTransaction(AIState.Type.eIdle);
	}

	void OnTimer()
	{
		if (AIUtils.CanPickAndRoll(m_player) != 1)
			Exit();
	}
	
	public override void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}