public class AISystem_PractiseLayupDunk : AISystem
{
	public AISystem_PractiseLayupDunk(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseLayupDunk_Defense]	= new AI_PractiseLayupDunk_Defense(this);
		m_arStateList[(int)AIState.Type.ePractiseLayupDunk_Block]	= new AI_PractiseLayupDunk_Block(this);
		m_arStateList[(int)AIState.Type.ePractiseLayupDunk_Positioning]	= new AI_PractiseLayupDunk_Positioning(this);
	}
}
