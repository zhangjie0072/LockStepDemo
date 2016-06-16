using UnityEngine;

public class AI_GrabZone_Shoot : AIState
{
	bool shooted;
	public int shootingZone { get; private set; }

	public AI_GrabZone_Shoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eGrabZone_Shoot;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);
		shooted = false;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if (m_player.m_ball == null )
		{
			m_system.SetTransaction(AIState.Type.eGrabZone_TraceBall);
			return;
		}

		if (shooted &&
			m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot &&
			m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
		{
			if (m_player.m_bWithBall)
				m_system.SetTransaction(AIState.Type.eGrabZone_Positioning);
			else
				m_system.SetTransaction(AIState.Type.eGrabZone_TraceBall);
			return;
		}

		if (!shooted && m_player.m_toSkillInstance == null)
		{
			m_player.m_toSkillInstance = ShootHelper.ShootByArea(m_player, m_match);
			m_player.m_bForceShoot = true;

			m_player.m_ball.onShoot += OnShoot;
			m_player.m_ball.onShootGoal += OnShootOver;
			m_player.m_ball.onRebound += OnShootOver;

			shooted = true;
		}
	}

	void OnShoot(UBasketball ball)
	{
		shootingZone = (m_match as GameMatch_GrabZone).DetectZone(m_player.position);
		ball.onShoot -= OnShoot;
	}

	void OnShootOver(UBasketball ball)
	{
		shootingZone = 0;
		ball.onShootGoal -= OnShootOver;
		ball.onRebound -= OnShootOver;
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}