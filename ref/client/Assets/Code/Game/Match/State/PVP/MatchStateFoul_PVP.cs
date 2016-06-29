using System;
using UnityEngine;

public class MatchStateFoul_PVP
	:MatchStateFoul
{
	private IM.Number m_waitTime = IM.Number.two;
	private IM.Number m_timeCounter = IM.Number.zero;
	
	public MatchStateFoul_PVP(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eFoul;
	}

	public override void OnEnter (MatchState lastState)
	{
		base.OnEnter (lastState);
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.m_enableAction = true;
			player.m_enablePickupDetector = false;
		}
	}

	override public void GameUpdate (IM.Number fDeltaTime)
	{
        SetRoleCirleColor();
		if( m_timeCounter >= m_waitTime )
		{
			m_timeCounter = IM.Number.zero;
			if (m_match.m_uiMatch != null)
				m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,false);
		}
		else
		{
			m_timeCounter += fDeltaTime;
			if (m_match.m_uiMatch != null)
				m_match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24,true);
		}
	}
	
	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
