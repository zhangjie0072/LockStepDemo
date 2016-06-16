using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class MatchStatePlaying_PVP : MatchStatePlaying, GameMatch.Count24Listener
{
    private UnityEngine.Object mResIndicator;
    private GameObject mGoIndicator;

    private bool mRefreshCounter = false;
    private GameObject m_goRebPlacementTip;

	private List<UBasketball> m_finalHitBalls = new List<UBasketball>();

	public MatchStatePlaying_PVP(MatchStateMachine owner)
        : base(owner)
    {
        m_eState = MatchState.State.ePlaying;
    }

	protected override void _OnTimeUp ()
	{
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);

		UBasketball ball = m_match.mCurScene.mBall;
		if( ball == null )
			return;
		GameMatch_PVP pvpMatch = m_match as GameMatch_PVP;

		if(m_match.m_config.type == GameMatch.Type.ePVP_3On3)
		{
		}
		else
		{
			if( ball.m_ballState == BallState.eUseBall_Pass )
			{
				Player interceptor = ball.m_interceptor;
				if( interceptor == null )
				{
					Player owner = ball.m_catcher;
					pvpMatch.SwitchMainrole(owner);
				}
				else
				{
					if( interceptor.m_team == m_match.m_mainRole.m_team )
						pvpMatch.SwitchMainrole(interceptor);
					else if( interceptor.m_defenseTarget != null )
						pvpMatch.SwitchMainrole(interceptor.m_defenseTarget);
				}
			}
			else if( ball.m_ballState == BallState.eUseBall )
			{
				if( ball.m_owner.m_team == m_match.m_mainRole.m_team )
					pvpMatch.SwitchMainrole(ball.m_owner);
			}
		}
		foreach(Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( !player.m_toTakeOver )
				continue;

			if( player.m_aiMgr == null )
			{
				player.m_aiMgr = new AISystem_Basic(m_match, player, AIState.Type.eInit);
				player.m_aiMgr.IsPvp = true;
			}

			player.m_bSimulator = false;

			if( !player.m_aiMgr.m_enable )
				player.m_aiMgr.m_enable = true;

			player.m_toTakeOver = false;
			player.m_takingOver = true;
		}
	}
}

