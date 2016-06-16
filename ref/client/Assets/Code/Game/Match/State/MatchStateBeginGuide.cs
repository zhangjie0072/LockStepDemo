using System;
using UnityEngine;

public class MatchStateBeginGuide : MatchStateBegin
{
	public bool forwardToShowSkillGuide = false;
	
	public MatchStateBeginGuide(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eBegin;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		if (forwardToShowSkillGuide)
		{
			m_stateMachine.SetState(MatchState.State.eShowSkillGuide);
		}
		else
		{
			base.OnEnter(lastState);
		}
	}
}
