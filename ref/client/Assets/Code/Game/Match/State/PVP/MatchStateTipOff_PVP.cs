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

	protected override void _OnEnableTipOff ()
	{
		m_stateMachine.SetState(State.ePlaying);
	}

	protected override void _OnCounterDone()
	{
		GameMsgSender.SendTipOff();
	}

	public void BeginTipOff()
	{
		_OnCounterDoneImp();

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
		
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_InfoVisualizer.SetActive(true);
		}
        m_match.m_mainRole.ShowIndicator(Color.yellow, true);

		m_match.AssumeDefenseTarget();
	}
}

