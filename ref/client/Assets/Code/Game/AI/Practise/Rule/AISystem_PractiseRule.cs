public class AISystem_PractiseRule : AISystem
{
	public AISystem_PractiseRule(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseRule_Idle]	= new AI_PractiseRule_Idle(this);
		m_arStateList[(int)AIState.Type.ePractiseRule_Shoot]	= new AI_PractiseRule_Shoot(this);
		m_arStateList[(int)AIState.Type.ePractiseRule_TraceBall]	= new AI_PractiseRule_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePractiseRule_CheckBall]	= new AI_PractiseRule_CheckBall(this);
	}
}
