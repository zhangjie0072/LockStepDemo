using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class AI_CrossOver
	: AIState
{
	const uint FAKE_CROSS_OVER_ID = 7251;
	const uint SG_SPECIAL_SKILL_ID = 6502;
	const uint SG_SPECIAL_LEFT_ACTION_ID = 65020;
	const uint SG_SPECIAL_RIGHT_ACTION_ID = 65021;
	const uint SF_HORI_SKILL_ID = 6500;
	const uint SF_HORI_LEFT_ACTION_ID = 65000;
	const uint SF_HORI_RIGHT_ACTION_ID = 65001;
	const uint C_VERT_SKILL_ID = 6501;
	const uint C_VERT_LEFT_ACTION_ID = 65010;
	const uint C_VERT_RIGHT_ACTION_ID = 65011;
	const uint HORI_SKILL_ID = 7250;
	const uint HORI_LEFT_ACTION_ID = 72500;
	const uint HORI_RIGHT_ACTION_ID = 72501;
	const uint VERT_SKILL_ID = 7252;
	const uint VERT_LEFT_ACTION_ID = 72502;
	const uint VERT_RIGHT_ACTION_ID = 72503;
	public const int  STAMINA_COST = 57;

	private AIState m_lastState;
	
	public AI_CrossOver(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eCrossOver;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_lastState = lastState;

		uint skillID = 0;
		uint actionID = 0;
		if (m_player.m_position == fogs.proto.msg.PositionType.PT_SG)
		{
			if (m_player.position.z < m_match.mCurScene.mGround.mHalfSize.y - 2)
			{
				IM.Number dist23PT = m_match.mCurScene.mGround.GetDistTo3PTLine(m_player.position.xz);
				if (0 < dist23PT && dist23PT < new IM.Number(0,700))
				{
					skillID = SG_SPECIAL_SKILL_ID;
					actionID = IM.Random.value < IM.Number.half ? SG_SPECIAL_LEFT_ACTION_ID : SG_SPECIAL_RIGHT_ACTION_ID;
				}
			}
		}

		if (skillID == 0)
		{
			Player defenser = m_player.GetNearestDefender();
			IM.Vector3 dirPlayerToDefenser = (defenser.position - m_player.position).normalized;
            IM.Number angle = IM.Vector3.FromToAngle(dirPlayerToDefenser, m_player.right);
			IM.Number devAngle = m_system.AI.devAngleDefender;
			angle *= IM.Number.one + IM.Random.Range(-devAngle, devAngle);
			bool left = angle < new IM.Number(90);

			if (m_player.m_AOD.GetStateByPos(defenser.position) == AOD.Zone.eBest || 
				m_player.m_position == PositionType.PT_SG)
			{
				if (m_player.m_position == PositionType.PT_C)
				{
					skillID = C_VERT_SKILL_ID;
					actionID = left ? C_VERT_LEFT_ACTION_ID : C_VERT_RIGHT_ACTION_ID;
				}
				else
				{
					skillID = VERT_SKILL_ID;
					actionID = left ? VERT_LEFT_ACTION_ID : VERT_RIGHT_ACTION_ID;
				}
			}
			else if (m_player.position.z < m_match.mCurScene.mGround.mHalfSize.y - 1)		// not near to end line
			{
				if (m_player.m_position == PositionType.PT_SF)
				{
					skillID = SF_HORI_SKILL_ID;
					actionID = left ? SF_HORI_LEFT_ACTION_ID : SF_HORI_RIGHT_ACTION_ID;
				}
				else
				{
					skillID = HORI_SKILL_ID;
					actionID = left ? HORI_LEFT_ACTION_ID : HORI_RIGHT_ACTION_ID;
				}
			}
		}

		m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.CrossOver, true, (skillInstance) =>
		{
			return (skillID == 0 || skillID == skillInstance.skill.id) && skillInstance.skill.id != FAKE_CROSS_OVER_ID;
		});
		if (m_player.m_toSkillInstance != null)
		{
			if (m_player.m_toSkillInstance.skill.id == FAKE_CROSS_OVER_ID)
				Debug.LogError("Fake cross over can't be casted by AI, expected skill: " + skillID + " player:" + m_player.m_id);
			if (actionID != 0)
				m_player.m_toSkillInstance.curActionId = actionID;
		}
		else if (skillID != 0)
		{
			m_player.m_toSkillInstance = m_player.m_skillSystem.GetValidSkillInMatch(Command.CrossOver, true);
			if (m_player.m_toSkillInstance != null)
			{
				skillID = m_player.m_toSkillInstance.skill.id;
				if (skillID == SG_SPECIAL_SKILL_ID)
					actionID = m_player.position.x > 0 ? SG_SPECIAL_LEFT_ACTION_ID : SG_SPECIAL_RIGHT_ACTION_ID;
				else if (skillID == HORI_SKILL_ID)
					actionID = m_player.position.x > 0 ? HORI_LEFT_ACTION_ID : HORI_RIGHT_ACTION_ID;
				else if (skillID == VERT_SKILL_ID)
					actionID = m_player.position.x > 0 ? VERT_LEFT_ACTION_ID : VERT_RIGHT_ACTION_ID;
				m_player.m_toSkillInstance.curActionId = actionID;
			}
			else
				Debug.LogWarning("AISkillSystem(" + m_player.m_id + "), no skill for AI CrossOver");
		}
	}

    override public void Update(IM.Number fDeltaTime)
	{
		PlayerState state =	m_player.m_StateMachine.m_curState;
		if( state.m_eState != PlayerState.State.eCrossOver )
		{
			PlayerState_CrossOver stateCross = m_player.m_StateMachine.GetState(PlayerState.State.eCrossOver) as PlayerState_CrossOver;
			if (stateCross.crossed)	// Cross over succeed.
			{
				if (IM.Random.value < new IM.Number(0, 300))	// Attack unreasoningly
				{
					AI_Positioning aiPositioning = m_system.GetState(Type.ePositioning) as AI_Positioning;
					aiPositioning.unreasonable = true;
					Debug.Log("Attack unreasonable.");
				}
				else
				{
					Player defender = m_player.GetDefender();
					if (defender != null)	// Still being defended.
					{
						List<SkillInstance> basicSkills = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.CrossOver);
						uint uBasicSkillCost = basicSkills[0].skill.levels[1].stama;
						if (m_player.m_stamina.m_curStamina >= new IM.Number((int)uBasicSkillCost))	// Has enough stamina to cross again.
						{
							IM.Number crossRate = PlayerState_CrossOver.CalcCrossRate(m_player, defender);	// Cross over success rate.
							if (crossRate > new IM.Number(0,600))
							{
								m_system.SetTransaction(this, new IM.Number(100), true);
								return;
							}
						}
					}
				}
			}
			else	// Cross over failed.
			{
				if (state.m_eState == PlayerState.State.eStand ||
					state.m_eState == PlayerState.State.eRun ||
					state.m_eState == PlayerState.State.eRush ||
					state.m_eState == PlayerState.State.eHold)
				{
					Player defender = m_player.GetDefender();
					if (defender != null)
					{
						IM.Number crossRate = PlayerState_CrossOver.CalcCrossRate(m_player, defender);	// Cross over success rate.
						if (crossRate < new IM.Number(0,200))	// Success rate is too low. Pass to teammate.
						{
							List<Player> teammates = new List<Player>(m_player.m_team.members);
							teammates.Remove(m_player);
							Player target = AIUtils.GetMostPriorPassTarget(teammates, m_player);
							if (target != null)
							{
								AI_Pass passState = m_system.GetState(Type.ePass) as AI_Pass;
								passState.m_toPass = target;
								m_system.SetTransaction(passState, new IM.Number(100), true);
								return;
							}
						}
					}
				}
				else if (state.m_eState == PlayerState.State.eKnocked)	// Wait knocked state over to do passing
					return;
			}
			m_system.SetTransaction(AIState.Type.ePositioning);
		}
	}
	
	override public void OnExit ()
	{
		m_player.m_toSkillInstance = null;
	}
}