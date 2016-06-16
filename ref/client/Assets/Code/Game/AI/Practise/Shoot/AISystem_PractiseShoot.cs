public class AISystem_PractiseShoot : AISystem
{
	public AISystem_PractiseShoot(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit)
		: base(match, player, initialState)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.ePractiseShoot_Idle]	= new AI_PractiseShoot_Idle(this);
		m_arStateList[(int)AIState.Type.ePractiseShoot_TraceBall]	= new AI_PractiseShoot_TraceBall(this);
		m_arStateList[(int)AIState.Type.ePractiseShoot_Pass]	= new AI_PractiseShoot_Pass(this);
	}
}
