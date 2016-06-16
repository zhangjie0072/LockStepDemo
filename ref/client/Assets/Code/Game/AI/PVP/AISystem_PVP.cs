public class AISystem_PVP : AISystem
{
	public AISystem_PVP(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePVP_Idle]			= new AI_PVP_Idle(this);
		m_arStateList[(int)AIState.Type.ePVP_Positioning]	= new AI_PVP_Positioning(this);
		m_arStateList[(int)AIState.Type.ePVP_Layup]			= new AI_PVP_Layup(this);
		m_arStateList[(int)AIState.Type.ePVP_Dunk]			= new AI_PVP_Dunk(this);
		m_arStateList[(int)AIState.Type.ePVP_TraceBall]		= new AI_PVP_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePVP_Shoot]			= new AI_PVP_Shoot(this);
	}
}
