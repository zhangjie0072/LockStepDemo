public class AISystem_PractiseRebound : AISystem
{
	public AISystem_PractiseRebound(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseRebound_Idle]	= new AI_PractiseRebound_Idle(this);
		m_arStateList[(int)AIState.Type.ePractiseRebound_Positioning]	= new AI_PractiseRebound_Positioning(this);
		m_arStateList[(int)AIState.Type.ePractiseRebound_Shoot]	= new AI_PractiseRebound_Shoot(this);
	}
}
