public class AISystem_PractiseBaseGuide : AISystem
{
	public AISystem_PractiseBaseGuide(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_Idle]	= new AI_PractiseBaseGuide_Idle(this);
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_RequireBall]	= new AI_PractiseBaseGuide_RequireBall(this);
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_Pass]	= new AI_PractiseBaseGuide_Pass(this);
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_TraceBall]	= new AI_PractiseBaseGuide_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_Positioning]	= new AI_PractiseBaseGuide_Positioning(this);
		m_arStateList[(int)AIState.Type.ePractiseBaseGuide_Layup]	= new AI_PractiseBaseGuide_Layup(this);
	}
}
