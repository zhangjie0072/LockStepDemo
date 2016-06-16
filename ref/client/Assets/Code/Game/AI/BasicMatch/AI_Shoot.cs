using UnityEngine;

public class AI_Shoot : AIState
{
	const uint DRIFT_SHOOT_ID = 3000;
	const uint BACK_SHOOT_ID = 3001;
	const uint LEFT_ACTION_ID = 30000;
	const uint RIGHT_ACTION_ID = 30001;

	public AI_Shoot(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eShoot;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		uint skillID = 0;
		uint actionID = 0;
		Player defender = m_player.m_defenseTarget;
		if (defender != null)
		{
			bool hasLeftDefender = false, hasRightDefender = false, hasOuterDefender = false;
			IM.Vector3 dirPlayer2Basket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, m_player.position);
            IM.Number fAngle = IM.Vector3.FromToAngle(dirPlayer2Basket, IM.Vector3.forward);
            IM.Quaternion rot = IM.Quaternion.Euler(IM.Number.zero, fAngle, IM.Number.zero);
			foreach (Player player in defender.m_team.members)
			{
				IM.Vector3 vecPlayer2Defender = player.position - m_player.position;
				IM.Vector3 dirPlayer2Defender = rot * vecPlayer2Defender.normalized;
				vecPlayer2Defender = dirPlayer2Defender * vecPlayer2Defender.magnitude;
				if (0 < vecPlayer2Defender.z && vecPlayer2Defender.z < new IM.Number(1,500))
				{
					if (new IM.Number(0,800) < vecPlayer2Defender.x && vecPlayer2Defender.x < 2)
						hasRightDefender = true;
                    if (-2 < vecPlayer2Defender.x && vecPlayer2Defender.x < -new IM.Number(0, 800))
						hasLeftDefender = true;
				}
				if (m_player.m_AOD.GetStateByPos(player.position) == AOD.Zone.eValid)
					hasOuterDefender = true;
			}
			if (hasRightDefender)
			{
				skillID = DRIFT_SHOOT_ID;
				actionID = LEFT_ACTION_ID;
				Logger.Log("AIShoot, left drift shoot.");
			}
			else if (hasLeftDefender)
			{
				skillID = DRIFT_SHOOT_ID;
				actionID = RIGHT_ACTION_ID;
				Logger.Log("AIShoot, right drift shoot.");
			}
			else if (hasOuterDefender)
			{
				skillID = BACK_SHOOT_ID;
				Logger.Log("AIShoot, back shoot.");
			}
		}

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Shoot, true, (skillInstance) =>
		{
			return skillID == 0 || skillInstance.skill.id == skillID;
		});
		if (m_player.m_toSkillInstance != null)
		{
			if (actionID != 0)
				m_player.m_toSkillInstance.curActionId = actionID;
		}
		else if (skillID != 0)
		{
			Logger.Log("AIShoot, get shoot skill without filter.");
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.Shoot, true);
		}
		if (m_player.m_toSkillInstance == null)
			Logger.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI Shoot");
		m_player.m_bForceShoot = true;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot &&
			m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
			m_system.SetTransaction(AIState.Type.eIdle);
	}
	
	override public void OnExit ()
	{
		//m_player.m_bToShoot = false;
		m_player.m_toSkillInstance = null;
	}
}