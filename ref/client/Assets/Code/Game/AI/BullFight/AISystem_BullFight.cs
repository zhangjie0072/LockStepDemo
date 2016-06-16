public class AISystem_BullFight : AISystem
{
	public AISystem_BullFight(GameMatch match, Player player)
		: base(match, player, AIState.Type.eBullFight_Positioning)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eBullFight_Positioning]	= new AI_BullFight_Positioning(this);
	}
}
