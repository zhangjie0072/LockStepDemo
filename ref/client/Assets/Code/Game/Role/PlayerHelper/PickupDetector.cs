using UnityEngine;
using System.Collections.Generic;

public class PickupDetector
{
	public bool m_enable;

	public delegate bool OnPickupBallDelegate(UBasketball ball);
	public OnPickupBallDelegate onPickupBall;

	private Player m_owner;
    //ÇòÔ±Éí¸ßÖµ ¿ÉÅäÖÃ
    private static IM.Number BOUND_SIZE_Y = new IM.Number(1, 750);
    //ÇòÔ±ºÍÇòÖ®¼äYÖá·½¼äµÄ¾àÀëÓÐÐ§Öµ ¿ÉÅäÖÃ
    private static IM.Number BOUND_R = IM.Number.one;

	public PickupDetector( Player owner )
	{
		m_owner = owner;
	}

    //TODO FixedUpdate or Update?
	public void Update(IM.Number fDeltTime)
	{
		if( !m_enable )
			return;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		List<UBasketball> balls = match.mCurScene.balls;
        IM.Vector3 playerPos = m_owner.position;
		foreach( UBasketball ball in balls )
		{
            if (!_IsPlayerTouchBall(playerPos, ball.position))
                continue;
			_OnBallCollided(match, ball);
		}
	}

    /**ÅÐ¶ÏÇòÔ±ÊÇ·ñÅöµ½Çò*/
    bool _IsPlayerTouchBall(IM.Vector3 playerPos, IM.Vector3 ballPos)
    {
        IM.Number dist = GameUtils.HorizonalDistance(playerPos, ballPos);
        IM.Number distY = ballPos.y - playerPos.y;
        return dist <= BOUND_R && distY <= BOUND_SIZE_Y && distY > 0;
    }

	void _OnBallCollided(GameMatch match, UBasketball ball) 
	{
		if (!ball.m_pickable)
			return;
		
		if( ball.m_ballState != BallState.eLoseBall && ball.m_ballState != BallState.eRebound )
			return;

		if( match is GameMatch_PVP )
		{
			if( m_owner != match.m_mainRole && !m_owner.m_bIsAI )
				return;
		}

		PlayerState curState = m_owner.m_StateMachine.m_curState;
		if( curState.m_eState != PlayerState.State.eStand 
		   && curState.m_eState != PlayerState.State.eRun 
		   && curState.m_eState != PlayerState.State.eRush 
		   && curState.m_eState != PlayerState.State.eDefense )
			return;

		if( m_owner.m_bWithBall )
			return;

		if (onPickupBall != null && !onPickupBall(ball))
			return;

		if(!m_owner.m_enableAction || !m_owner.m_enableMovement || m_owner.m_alwaysForbiddenPickup)
			return;

		PlayerState_Pickup pickState = m_owner.m_StateMachine.GetState(PlayerState.State.ePickup) as PlayerState_Pickup;
		if( pickState == null )
			return;
		pickState.m_ballToPickup = ball;

		m_owner.m_StateMachine.SetState(pickState);
    }
}
