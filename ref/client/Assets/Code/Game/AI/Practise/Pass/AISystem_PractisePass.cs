public class AISystem_PractisePass : AISystem
{
	public AISystem_PractisePass(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractisePass_Idle]	= new AI_PractisePass_Idle(this);
		m_arStateList[(int)AIState.Type.ePractisePass_Positioning]	= new AI_PractisePass_Positioning(this);
		m_arStateList[(int)AIState.Type.ePractisePass_RequireBall]	= new AI_PractisePass_RequireBall(this);
		m_arStateList[(int)AIState.Type.ePractisePass_TraceBall]	= new AI_PractisePass_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePractisePass_Pass]	= new AI_PractisePass_Pass(this);
	}
}
