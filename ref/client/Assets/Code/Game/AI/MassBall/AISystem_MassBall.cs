public class AISystem_MassBall : AISystem
{
	public AISystem_MassBall(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eMassBall_Init]	= new AI_MassBall_Init(this);
		m_arStateList[(int)AIState.Type.eMassBall_TraceBall]	= new AI_MassBall_TraceBall(this);
		m_arStateList[(int)AIState.Type.eMassBall_Positioning]	= new AI_MassBall_Positioing(this);
		m_arStateList[(int)AIState.Type.eMassBall_Shoot]	= new AI_MassBall_Shoot(this);
	}
}
