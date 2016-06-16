using System;
using UnityEngine;

public class MatchStateBegin_PVP
	: MatchStateBegin
{	

	public MatchStateBegin_PVP(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eBegin;
	}

	public override void OnEnter (MatchState lastState)
	{
		base.OnEnter (lastState);
		Logger.Log("PVP Begin");

		if( m_match.m_bOverTime && m_match.m_uiMatch == null )
		{
            m_match.m_gameMathCountEnable = false;
			m_match.CreateUI();
		}

		if( m_match.m_config.type == GameMatch.Type.ePVP_1PLUS )
		{
			GameMatch_PVP match_pvp = m_match as GameMatch_PVP;
			UBasketball ball = m_match.mCurScene.mBall;
			Player ballOwner = ball.m_owner;
			if (ballOwner != null)
			{
				if (match_pvp.m_mainRole.m_team.m_role == GameMatch.MatchRole.eDefense)
					match_pvp.SwitchMainrole(ballOwner.m_defenseTarget);
				else
					match_pvp.SwitchMainrole(ballOwner);
				m_match.m_cam.Positioning(true);
			}
		}

		double dSyncSrvTime = GameSystem.Instance.mNetworkManager.m_dServerTime;
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( player.m_smcManager == null )
				continue;
			player.m_smcManager.Reset(dSyncSrvTime);
			player.m_InfoVisualizer.SetActive(true);
		}
        m_match.m_mainRole.ShowIndicator(Color.yellow, true);
	}
}
