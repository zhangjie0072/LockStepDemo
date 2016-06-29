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
		Debug.Log("PVP Begin");

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
                match_pvp.SwitchMainrole(ballOwner.m_defenseTarget);
                match_pvp.SwitchMainrole(ballOwner);
				m_match.m_cam.Positioning(true);
			}
		}

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_InfoVisualizer.SetActive(true);
		}
        Color yellow = new Color(1f, 252f / 255, 10f / 255, 1);
        m_match.mainRole.ShowIndicator(yellow, true);
	}
}
