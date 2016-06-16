public class AISystem_ReboundStorm : AISystem
{
	public AISystem_ReboundStorm(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eReboundStorm_ShooterIdle]	= new AI_ReboundStorm_ShooterIdle(this);
		m_arStateList[(int)AIState.Type.eReboundStorm_Shoot]	= new AI_ReboundStorm_Shoot(this);
		m_arStateList[(int)AIState.Type.eReboundStorm_Idle]	= new AI_ReboundStorm_Idle(this);
		m_arStateList[(int)AIState.Type.eReboundStorm_Positioning]	= new AI_ReboundStorm_Positioning(this);
		m_arStateList[(int)AIState.Type.eReboundStorm_Rebound]	= new AI_ReboundStorm_Rebound(this);
	}
}
