using UnityEngine;
using System.Collections.Generic;

public class PlayerState_PrepareToShoot_ForceShoot : PlayerState_PrepareToShoot
{
	private SkillInstance _inst;

	public PlayerState_PrepareToShoot_ForceShoot (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		_inst = m_player.m_toSkillInstance;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		m_player.m_toSkillInstance = _inst;
		base.Update(fDeltaTime);
	}
}
