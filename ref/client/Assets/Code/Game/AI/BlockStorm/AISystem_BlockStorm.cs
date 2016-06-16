public class AISystem_BlockStorm : AISystem
{
	public AISystem_BlockStorm(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eBlockStorm_Idle]	= new AI_BlockStorm_Idle(this);
		m_arStateList[(int)AIState.Type.eBlockStorm_Positioning]	= new AI_BlockStorm_Positioning(this);
		m_arStateList[(int)AIState.Type.eBlockStorm_Layup]	= new AI_BlockStorm_Layup(this);
		m_arStateList[(int)AIState.Type.eBlockStorm_Dunk]	= new AI_BlockStorm_Dunk(this);
	}
}
