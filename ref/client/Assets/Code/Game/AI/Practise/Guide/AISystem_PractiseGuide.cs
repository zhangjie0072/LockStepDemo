public class AISystem_PractiseGuide : AISystem
{
	public int index;

	public AISystem_PractiseGuide(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseGuide_Idle]	= new AI_PractiseGuide_Idle(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Face2Mate]	= new AI_PractiseGuide_Face2Mate(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_TraceBall]	= new AI_PractiseGuide_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Positioning]	= new AI_PractiseGuide_Positioning(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Pass]	= new AI_PractiseGuide_Pass(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Layup]	= new AI_PractiseGuide_Layup(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Shoot]	= new AI_PractiseGuide_Shoot(this);
		m_arStateList[(int)AIState.Type.ePractiseGuide_Defense]	= new AI_PractiseGuide_Defense(this);
	}
}
