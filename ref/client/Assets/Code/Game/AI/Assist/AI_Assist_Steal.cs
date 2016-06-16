
public class AI_Assist_Steal : AIState
{
	public SkillInstance cachedSkill;

	bool castSkill;

	public AI_Assist_Steal(AISystem system) : base(system)
	{
		m_eType = Type.eAssistSteal;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);

		if( m_ball.m_owner == null )
			m_moveTarget = m_player.position;
		else
			m_moveTarget = PlayerState_Steal.GetStealPosition(m_ball.m_owner);

		castSkill = false;
		if (m_player.m_inputDispatcher != null)
			m_player.m_inputDispatcher.m_enable = false;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if (m_ball.m_owner == null)
		{
			CloseAI();
			return;
		}
		if (m_player.m_team.m_role == GameMatch.MatchRole.eOffense)
		{
			CloseAI();
			return;
		}

		m_player.m_toSkillInstance = null;
		if (castSkill)
		{
			m_player.m_toSkillInstance = cachedSkill;
			(m_player.m_StateMachine.GetState(PlayerState.State.eSteal) as PlayerState_Steal).forcedByAI = true;
			if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eSteal)
				CloseAI();
		}
	}

	public override void ArriveAtMoveTarget()
	{
		base.ArriveAtMoveTarget();
		castSkill = true;
	}

	public override void OnExit()
	{
		base.OnExit();
		if (m_player.m_inputDispatcher != null)
			m_player.m_inputDispatcher.m_enable = true;
	}

	void CloseAI()
	{
		m_player.m_StateMachine.assistAI.Disable();
	}
}
