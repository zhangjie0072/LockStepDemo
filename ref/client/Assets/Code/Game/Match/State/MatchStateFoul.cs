using System;
using UnityEngine;

public class MatchStateFoul
	:MatchState
{
	private IM.Number m_waitTime = IM.Number.two;
	private IM.Number m_timeCounter = IM.Number.zero;
	GameMatch.MatchRole m_mainTeamMatchRole;
	
	public MatchStateFoul(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eFoul;
	}
	
	override public void OnEnter( MatchState lastState )
	{
		m_mainTeamMatchRole = m_match.m_mainRole.m_team.m_role;

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			//player.m_vVelocity = Vector3.zero;
			player.m_enableAction = false;
			
			if( player.m_aiMgr == null )
				continue;
			player.m_aiMgr.m_enable = false;
		}

		if (m_match.m_uiMatch != null)
		{
			m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		if( m_timeCounter >= m_waitTime )
		{
			m_timeCounter = IM.Number.zero;
			if (m_match.m_uiMatch != null)
				m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,false);
			if (m_match.m_mainRole.m_team.m_role == m_mainTeamMatchRole)	// Whether the match role is already switched for pick up ball.
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
