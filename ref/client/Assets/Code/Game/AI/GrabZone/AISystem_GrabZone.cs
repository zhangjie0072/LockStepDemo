public class AISystem_GrabZone : AISystem
{
	public AISystem_GrabZone(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eGrabZone_Init]	= new AI_GrabZone_Init(this);
		m_arStateList[(int)AIState.Type.eGrabZone_TraceBall]	= new AI_GrabZone_TraceBall(this);
		m_arStateList[(int)AIState.Type.eGrabZone_Positioning]	= new AI_GrabZone_Positioning(this);
		m_arStateList[(int)AIState.Type.eGrabZone_AvoidDefender]	= new AI_GrabZone_AvoidDefender(this);
		m_arStateList[(int)AIState.Type.eGrabZone_Shoot]	= new AI_GrabZone_Shoot(this);
	}
}
