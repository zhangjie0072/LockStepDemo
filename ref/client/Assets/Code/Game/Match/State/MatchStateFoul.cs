using System;
using UnityEngine;

public class MatchStateFoul
	:MatchState
{
	private IM.Number m_waitTime = IM.Number.two;
	private IM.Number m_timeCounter = IM.Number.zero;
	GameMatch.MatchRole m_homeTeamMatchRole;
	
	public MatchStateFoul(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eFoul;
	}
	
	override public void OnEnter( MatchState lastState )
	{
		m_homeTeamMatchRole = m_match.m_homeTeam.m_role;

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = false;
		}

		if (m_match.m_uiMatch != null)
		{
			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}
	}
	
	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);

		if( m_timeCounter >= m_waitTime )
		{
			m_timeCounter = IM.Number.zero;
			if (m_match.m_uiMatch != null)
				m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,false);
			if (m_match.m_homeTeam.m_role == m_homeTeamMatchRole)	// Whether the match role is already switched for pick up ball.
				m_match.m_ruler.SwitchRole();
			m_stateMachine.SetState(MatchState.State.eBegin);
		}
		else
		{
			m_timeCounter += fDeltaTime;
			if (m_match.m_uiMatch != null)
				m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,true);
		}
	}
	
	override public void OnExit ()
	{
		if (m_match.m_uiMatch != null)
			m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,false);
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
