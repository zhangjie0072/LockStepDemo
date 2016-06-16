using System.Collections.Generic;
using UnityEngine;

public class AI_DefenseBack : AIState
{
	bool toBlockMoving;
	bool toDefenseMoving;
	bool defenderWasMoving;
	bool toBlockCrossing;
	bool toDefenseCrossing;

	public AI_DefenseBack(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eDefenseBack;
	}

	public override void OnEnter(AIState lastState)
	{
		base.OnEnter(lastState);
		Logger.Log("Enter defense back");
	}

    public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		Player defenseTarget = m_player.m_defenseTarget;
		PlayerState.State defenseTargetState = defenseTarget.m_StateMachine.m_curState.m_eState;
		switch (defenseTargetState)
		{
			case PlayerState.State.eBackToBack:
			case PlayerState.State.eBackToBackForward:
			case PlayerState.State.eBackCompete:
			/*
				if (defenseTarget.m_StateMachine.m_curState is PlayerState_BackToBackForward)
				{
					DefenseBackMoving();
					defenderWasMoving = true;
				}
				else
				{
					DefenseBackToBack();
					toDefenseMoving = false;
				}
				*/
			DefenseBackMoving();
			defenderWasMoving = true;
				break;
			case PlayerState.State.eBackToStand:
				DefenseBackToStand();
				break;
			case PlayerState.State.eBackTurnRun:
				DefenseBackCrossing();
				break;
			default:
				m_system.SetTransaction(AIState.Type.eDefense);
				break;
		}

		if (defenseTargetState != PlayerState.State.eBackToBackForward)
		{
			defenderWasMoving = false;
			toDefenseMoving = false;
		}
		if (defenseTargetState != PlayerState.State.eBackTurnRun)
		{
			toBlockCrossing = false;
			toDefenseCrossing = false;
		}
	}

	public override void OnExit()
	{
		base.OnExit();
		Logger.Log("Exit defense back.");
	}

	void DefenseBackToBack()
	{
		List<SkillInstance> defenseSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
		m_player.m_toSkillInstance = defenseSkill[0];
		PositioningDefense();
	}

	void DefenseBackMoving()
	{
		if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eBackBlock)	//already in back blocking
			return;

		if (toDefenseMoving)	//already in defensing
		{
			List<SkillInstance> defenseSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
			m_player.m_toSkillInstance = defenseSkill[0];
			PositioningDefense();
			return;
		}

		if (toBlockMoving)
		{
			PositioningBlockMoving();
			return;
		}

		if (IM.Random.value < IM.Number.half)
		{
			toDefenseMoving = true;
			PositioningDefense();
		}
		else
		{
			toBlockMoving = true;
			PositioningBlockMoving();
		}
	}

	void PositioningDefense()
	{
		Player defenseTarget = m_player.m_defenseTarget;
		IM.Vector3 dir = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, defenseTarget.position);
		IM.Vector3 moveTarget = defenseTarget.position + dir * IM.Number.two;
		if (GameUtils.HorizonalDistance(moveTarget, m_player.position) > IM.Number.one)
			m_moveTarget = moveTarget;
	}

	void PositioningBlockMoving()
	{
		Player defenseTarget = m_player.m_defenseTarget;
		IM.Vector3 dir = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, defenseTarget.position);
		IM.Vector3 moveTarget = defenseTarget.position + dir * IM.Number.half;
		m_moveTarget = moveTarget;
	}

	void PositioningDefenseCrossing()
	{
		Player defenseTarget = m_player.m_defenseTarget;
        IM.Vector3 dir = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, defenseTarget.position);
        IM.Vector3 moveTarget = defenseTarget.position + dir * IM.Number.two;
		m_moveTarget = moveTarget;
	}

	void DefenseBackToStand()
	{
		List<SkillInstance> defenseSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
		m_player.m_toSkillInstance = defenseSkill[0];
	}

	void DefenseBackCrossing()
	{
		if (toBlockCrossing)
			return;

		if (toDefenseCrossing)
		{
			List<SkillInstance> defenseSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
			m_player.m_toSkillInstance = defenseSkill[0];
			PositioningDefenseCrossing();
			return;
		}

		IM.Number random = IM.Random.value;

		if (random < IM.Number.zero)	//defense
		{
			toDefenseCrossing = true;
		}
		else	//block, or be crossed
		{
			Player defenseTarget = m_player.m_defenseTarget;
			IM.Vector3 dirToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, defenseTarget.position);
			IM.Vector3 dirToBasketLeft = IM.Quaternion.AngleAxis(-new IM.Number(90), IM.Vector3.up) * dirToBasket;
			PlayerState_BackTurnRun defenderState = defenseTarget.m_StateMachine.m_curState as PlayerState_BackTurnRun;
			bool defenseLeft = defenderState.isTurnLeft;
			if (random > (IM.Number.one - m_system.AI.devRateDefCross))	//to be crossed
				defenseLeft = !defenseLeft;

			m_moveTarget = defenseTarget.position + dirToBasket * new IM.Number(1,500) + dirToBasketLeft * IM.Number.two * (defenseLeft ? IM.Number.one : -IM.Number.one);

			toBlockCrossing = true;
		}
	}

	public override void ArriveAtMoveTarget()
	{
		if (toBlockMoving)
		{
			m_player.m_StateMachine.SetState(PlayerState.State.eBackBlock);
			toBlockMoving = false;
		}
	}
}

