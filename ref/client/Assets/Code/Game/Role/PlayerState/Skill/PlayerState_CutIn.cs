using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_CutIn:  PlayerState_Skill
{
	PseudoRandom random = new PseudoRandom();
	static HedgingHelper hedging = new HedgingHelper("CrossOver");

	public PlayerState_CutIn(PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eCutIn;
	}

	//public override bool PreEnter()
	//{
	//	if (m_player.m_vVelocity == Vector3.zero)
	//	{
	//		return false;
	//	}
	//	else
	//	{
	//		Vector3 dirPlayerToBasket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, m_player.position);
	//		float moveAngle = Quaternion.FromToRotation(dirPlayerToBasket, m_player.m_vVelocity).eulerAngles.y;
	//		if (135f < moveAngle && moveAngle < 225f)
	//		{
	//			return false;
	//		}
	//	}
	//	return true;
	//}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		if( !m_player.m_bSimulator )
		{
            m_player.forward = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
			
			IM.Vector3 targetPos = GetEndPos();
			Debugger.Instance.DrawSphere("CutInEndPos", (Vector3)targetPos, Color.green);

			GameMsgSender.SendCutIn(m_player, m_curExecSkill); 

			Player defender = m_player.GetDefender();
			if (defender != null && 
				(defender.m_StateMachine.m_curState.m_eState == State.eStand ||
				defender.m_StateMachine.m_curState.m_eState == State.eDefense ||
				defender.m_StateMachine.m_curState.m_eState == State.eRun ||
				defender.m_StateMachine.m_curState.m_eState == State.eRush ))
			{
				IM.Number crossRate = CalcCrossRate(m_player, defender);
				bool sumValue = random.AdjustRate(ref crossRate);
                IM.Number crossValue = IM.Random.value;
				bool crossed = crossValue < crossRate;
				Debugger.Instance.m_steamer.message = "CutIn succeed: " + crossed + " Rate: " + crossRate + " Value: " + crossValue;

				//should use sendmessage
				if (crossed)
				{
					if (sumValue)
						random.SumValue();
					m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);
				}
				else
				{
					PlayerState_DefenseCross state = defender.m_StateMachine.GetState(State.eDefenseCross) as PlayerState_DefenseCross;
					state.targetPos = targetPos;
                    state.time = m_player.animMgr.GetDuration(m_curAction) / m_player.animMgr.GetSpeed(m_curAction);
					state.crosser = m_player; 
					state.InitState();
					defender.m_StateMachine.SetState(state);
				    //后面帧同步框架移过来后，就不需要下面一行代码了	
					GameMsgSender.SendDefenseCross(defender, m_player, state.m_animType, (float)state.speed, (Vector3)state.targetPos, (Vector3)state.dirMove);
				}
			}
		}

		m_player.animMgr.Play(m_curAction, true);
	}

	IM.Vector3 GetEndPos()
	{
		IM.Vector3 initRootPos = IM.Vector3.zero;
		m_player.GetNodePosition(SampleNode.Root, m_curAction, IM.Number.zero, out initRootPos);
		IM.Vector3 lastRootPos = IM.Vector3.zero;
        IM.Number length = m_player.animMgr.GetDuration(m_curAction);
		m_player.GetNodePosition(SampleNode.Root, m_curAction, length, out lastRootPos);
		IM.Vector3 movement = lastRootPos - initRootPos;
		movement = movement / m_player.scale.x;
		return m_player.position + movement;
	}

	static IM.Number CalcCrossRate(Player crosser, Player defender)
	{
		IM.Number control = new IM.Number((int)crosser.m_finalAttrs["control"]);
        IM.Number steal = new IM.Number((int)defender.m_finalAttrs["steal"]);
		return hedging.Calc(steal, control);
	}
}