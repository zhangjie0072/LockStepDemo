using fogs.proto.config;
using fogs.proto.msg;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchStateFreeThrowStart : MatchState
{
	bool mainRoleForbiddenPickup;

	public MatchStateFreeThrowStart(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eFreeThrowStart;
	}

	public override void OnEnter(MatchState lastState)
	{
		base.OnEnter(lastState);

		m_match.m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
		m_match.m_mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

		m_match.ResetPlayerPos();

		m_match.m_mainRole.GrabBall(m_match.mCurScene.mBall);
		mainRoleForbiddenPickup = m_match.m_mainRole.m_alwaysForbiddenPickup;
		m_match.m_mainRole.m_alwaysForbiddenPickup = true;
		m_match.m_mainRole.m_enableMovement = false;
		m_match.m_mainRole.m_enableAction = true;

		m_match.m_mainRole.m_defenseTarget.m_aiMgr.m_enable = false;
		m_match.m_mainRole.m_defenseTarget.m_enableMovement = false;
		m_match.m_mainRole.m_defenseTarget.m_enableAction = true;
		m_match.m_mainRole.m_defenseTarget.m_enablePickupDetector = false;
		m_match.m_mainRole.m_defenseTarget.m_StateMachine.SetState(PlayerState.State.eStand, true);

		m_match.ShowAnimTip("gameInterface_text_DetermineBall");
	}

	public override void OnExit()
	{
		base.OnExit();
		m_match.m_mainRole.m_alwaysForbiddenPickup = mainRoleForbiddenPickup;
		m_match.m_mainRole.m_enableMovement = true;
		m_match.HideAnimTip("gameInterface_text_DetermineBall");

		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager.m_Players)
		{
			player.m_StateMachine.attackRandom.Clear();
		}
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Shoot;
	}
}
