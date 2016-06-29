using System;
using UnityEngine;

public class MatchStatePlayerCloseUp
	: MatchState
{
    static IM.Number BEGIN_WAIT_TIME = new IM.Number(5);
	GameUtils.Timer4View timer1;
	GameUtils.Timer timer2;

	public MatchStatePlayerCloseUp(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.ePlayerCloseUp;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		base.OnEnter(lastState);

        if (m_match.m_bOverTime)
            m_match.m_gameMathCountEnable = false;

		m_match.CreateUI();

		m_match.ResetPlayerPos();
		m_match.m_cam.Positioning(true);
		m_match.m_cam.m_PositionImmediately = false;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_enablePickupDetector = false;
			player.Show(true);
		}
	
		m_match.m_cam.m_moveSpeed = m_match.m_cam.m_CloseUpRestoreSpeed;

		m_match.m_cam.m_Zoom.SetZoom(m_match.mainRole.gameObject.transform, ZoomType.ePlayerCloseUp);

		if( m_match.m_uiMatch != null )
		{
            m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}

		Team oppoTeam = m_match.mainRole.m_team.m_side == Team.Side.eAway ? m_match.m_homeTeam : m_match.m_awayTeam;
		foreach (Player member in oppoTeam.members)
		{
			if (member.model != null)
				member.model.EnableGrey();
		}

        Color yellow = new Color(1f, 252f / 255, 10f / 255, 1);
        m_match.mainRole.ShowIndicator(yellow, true);

		//if (m_match.m_mainRole.m_inputDispatcher == null && m_match.GetMatchType() != GameMatch.Type.e3AIOn3AI )
		//	m_match.m_mainRole.m_inputDispatcher = new InputDispatcher (m_match, m_match.m_mainRole);

		//reset position
		if( m_match.m_needTipOff )
		{
            TipOffPos tipOffPos = GameSystem.Instance.MatchPointsConfig.TipOffPos;
			int homeCnt = m_match.m_homeTeam.GetMemberCount();
			for(int idx = 0; idx != homeCnt; idx++)
			{
                IM.Transform trOffensePos = tipOffPos.offenses_transform[idx];
                IM.Transform trDefensePos = tipOffPos.defenses_transform[idx];
				Player homePlayer = m_match.m_homeTeam.members[idx];
				if( homePlayer != null )
				{
					homePlayer.position = trOffensePos.position;
					homePlayer.rotation = trOffensePos.rotation;
				}
				Player awayPlayer = m_match.m_awayTeam.members[idx];
				if( awayPlayer != null )
				{
					awayPlayer.position = trDefensePos.position;
					awayPlayer.rotation = trDefensePos.rotation;
				}
			}
			m_match.m_needTipOff = false;
		}

        if (m_match.m_bOverTime)
            timer2 = new GameUtils.Timer(BEGIN_WAIT_TIME, () => m_stateMachine.SetState(State.eTipOff), 1);
        else
            timer1 = new GameUtils.Timer4View((float)BEGIN_WAIT_TIME, GameMsgSender.SendGameBegin, 1);
	}

	public override void ViewUpdate(float fDeltaTime)
	{
		base.ViewUpdate(fDeltaTime);
        timer1.Update(fDeltaTime);
	}

    public override void GameUpdate(IM.Number fDeltaTime)
    {
        base.GameUpdate(fDeltaTime);
        timer2.Update(fDeltaTime);
    }

	public override bool IsCommandValid(Command command)
	{
		return false;
	}

	public override void OnExit()
	{
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			player.m_enableAction = true;
			player.m_enableMovement = true;
			player.m_enablePickupDetector = true;
		}
	}
}
