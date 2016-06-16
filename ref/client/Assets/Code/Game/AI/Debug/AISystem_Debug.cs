public class AISystem_Debug : AISystem
{
	public AISystem_Debug(GameMatch match, Player player)
		: base(match, player, AIState.Type.eDebug_Init)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eDebug_Init]	= new AI_Debug_Init(this);
		m_arStateList[(int)AIState.Type.eDebug_Shoot]	= new AI_Debug_Shoot(this);
		m_arStateList[(int)AIState.Type.eDebug_Positioning2Basket]	= new AI_Debug_Positioning2Basket(this);
	}
}
