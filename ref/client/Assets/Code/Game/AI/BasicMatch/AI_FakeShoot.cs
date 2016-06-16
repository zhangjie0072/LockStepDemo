public class AI_FakeShoot : AI_Shoot
{
	public AI_FakeShoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eFakeShoot;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);
		m_player.m_bForceShoot = false;
	}
}