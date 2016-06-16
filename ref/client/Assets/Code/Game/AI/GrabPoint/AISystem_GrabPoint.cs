public class AISystem_GrabPoint : AISystem
{
	public AISystem_GrabPoint(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eGrabPoint_Init]	= new AI_GrabPoint_Init(this);
		m_arStateList[(int)AIState.Type.eGrabPoint_TracePoint]	= new AI_GrabPoint_TracePoint(this);
		m_arStateList[(int)AIState.Type.eGrabPoint_Positioning]	= new AI_GrabPoint_Positioning(this);
		m_arStateList[(int)AIState.Type.eGrabPoint_Shoot]	= new AI_GrabPoint_Shoot(this);
	}
}
