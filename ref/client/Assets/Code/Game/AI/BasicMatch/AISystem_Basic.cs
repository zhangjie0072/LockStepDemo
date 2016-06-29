public class AISystem_Basic: AISystem
{
	private bool	m_enableColliding;
	private IM.Number 	m_collideRefreshingTime;
	private static IM.Number _collideRefreshingTime = IM.Number.one;

	public AISystem_Basic(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint AIID = 0u)
		: base(match, player, initialState, AIID)
	{
		m_enableColliding = true;
		m_player.m_StateMachine.onStateChanged += OnPlayerStateChanged;
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);
	
		if( !m_enable )
			return;

		if( !m_enableColliding )
			m_collideRefreshingTime += fDeltaTime;
		
		if( m_collideRefreshingTime > _collideRefreshingTime )
			m_enableColliding = true;
	}

	public override void OnSectorCollided (RoadPathManager.Sector colSector)
	{
		if( m_player.m_team.m_role == GameMatch.MatchRole.eDefense )
			return;
		
		if( !m_enableColliding )
			return;
		
		if( m_player.m_team.m_role != GameMatch.MatchRole.eOffense )
			return;

		bool isFinalTime = m_curMatch.IsFinalTime(new IM.Number(3) - AI.devTime);
		if (isFinalTime)
			return;
		
		bool bCollideOnMember = false;
		foreach( Player player in colSector.holders )
		{
			if( player == m_player )
				continue;
			if( player.m_team.m_role != GameMatch.MatchRole.eOffense )
				continue;
			bCollideOnMember = true;
		}
		if( !bCollideOnMember )
			return;
		
		m_enableColliding = false;
		m_collideRefreshingTime = IM.Number.zero;
		
		RoadPathManager.Sector targetSector = RoadPathManager.Instance.Bounce(m_player.position.xz, colSector, m_player.m_bounceSectors);
		RoadPathManager.Instance.AddDrawSector("targetSector", targetSector);
		
		AIState positioning = m_arStateList[(int)AIState.Type.ePositioning];
        positioning.m_moveTarget = targetSector.center.x0z;
		SetTransaction(positioning);
	}

	void OnPlayerStateChanged(PlayerState lastState, PlayerState newState)
	{
		if (newState.m_eState == PlayerState.State.eBePickAndRoll)
		{
			if (!(newState as PlayerState_BePickedAndRolled).IsFallGround())
				return;
		}
		else if (newState.m_eState != PlayerState.State.eFallGround)
			return;
		if (m_player.m_team.m_role != GameMatch.MatchRole.eDefense)
			return;
		if (!m_curMatch.EnableSwitchDefenseTarget())
			return;
		if (m_curMatch.mCurScene.mBall.m_ballState == BallState.eRebound ||
			m_curMatch.mCurScene.mBall.m_ballState == BallState.eLoseBall)
			return;
		IM.Number minDist = IM.Number.max;
		Player mate2Switch = null;
		foreach (Player p in m_player.m_team)
		{
			if (p.m_defTargetSwitched)
				return;
			if (p != m_player)
			{
				IM.Number dist = GameUtils.HorizonalDistance(m_player.position, p.position);
				if (dist < new IM.Number(2,350) && dist < minDist)
				{
					minDist = dist;
					mate2Switch = p;
				}
			}
		}
		if (mate2Switch == null)
			return;
		Player myDefTarget = m_player.m_defenseTarget;
		m_player.m_defenseTarget = mate2Switch.m_defenseTarget;
		mate2Switch.m_defenseTarget = myDefTarget;
		m_player.m_defTargetSwitched = true;
		mate2Switch.m_defTargetSwitched = true;
		m_player.m_defenseTarget.m_defenseTarget = m_player;
		mate2Switch.m_defenseTarget.m_defenseTarget = mate2Switch;
		m_player.m_defenseTarget.m_defTargetSwitched = true;
		mate2Switch.m_defenseTarget.m_defTargetSwitched = true;
        UnityEngine.Debug.Log(string.Format("AISystem, switch defense target between {0} and {1}.", m_player.m_name, mate2Switch.m_name));
	}
}