using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class MatchStateTipOff_PVP: MatchStateTipOff
{
	public MatchStateTipOff_PVP(MatchStateMachine owner)
        : base(owner)
    {
        m_eState = MatchState.State.eTipOff;
    }

    protected override void _OnCounterDone()
    {
       base._OnCounterDone();

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

		m_match.AssumeDefenseTarget();
	}
}

