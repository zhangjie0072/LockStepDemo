using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_CrossOver:  PlayerState_Skill
{
	public bool crossed { get; private set; }

	public delegate void Delegate();

	static HedgingHelper hedging = new HedgingHelper("CrossOver");

	public enum CrossDir
	{
		None,
		Left,
		Right,
	}
	private CrossDir m_crossDir;
	IM.Vector3 targetPos;

	private bool	m_bDefenderOp = false;
	private bool	m_bChecked		= false;

	public PlayerState_CrossOver(PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eCrossOver;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		m_player.m_bMovedWithBall = true;

		m_bDefenderOp = false;
		m_bChecked = false;

		PlayerAnimAttribute.AnimAttr crossoverAttr = m_player.m_animAttributes.GetAnimAttrById(Command.CrossOver, m_curAction);
		if( crossoverAttr == null )
		{
			Debug.LogError("Current action: " + m_curAction + " in crossover id: " + m_curExecSkill.skill.id);
		}
        IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);
        m_player.m_blockable.Init(crossoverAttr, frameRate);

		SkillInput input = m_curExecSkill.curAction.inputs[0];
		if (input.moveDir == EDirection.eLeft || 
			(input.moveDir == EDirection.eForward && input.moveAngleRange.x >= IM.Math.HALF_CIRCLE && input.moveAngleRange.y >= IM.Math.HALF_CIRCLE))
			m_crossDir = CrossDir.Left;
		else if (input.moveDir == EDirection.eRight ||
            (input.moveDir == EDirection.eForward && input.moveAngleRange.x <= IM.Math.HALF_CIRCLE && input.moveAngleRange.y <= IM.Math.HALF_CIRCLE))
			m_crossDir = CrossDir.Right;
		else
			m_crossDir = CrossDir.None;

        m_player.forward = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);

		uint uSkillValue = 0;
		m_player.m_skillSystem.HegdingToValue("cross_speed", ref uSkillValue);
        //float crossSpeed = (m_player.m_finalAttrs["cross_speed"] + uSkillValue) * 0.0036f + 1f;(添加精度更高Number，还末测试)
        IM.PrecNumber crossSpeed = new IM.PrecNumber((int)(m_player.m_finalAttrs["cross_speed"] + uSkillValue)) * new IM.PrecNumber(0, 003600) + IM.PrecNumber.one;

		m_player.animMgr.Play(m_curAction, (IM.Number)crossSpeed, true).rootMotion.Reset();

		targetPos = GetEndPos();

		++m_player.mStatistics.data.cross_times;
	}

	bool _ValidDefender(Player defender)
	{
		if( defender == null )
			return false;
		//if( m_player.m_AOD.GetStateByPos(defender.position) == AOD.Zone.eInvalid )
		//	return false;

		IM.Number angle = new IM.Number(55);
		IM.Number range = new IM.Number(2,600);

		IM.Vector3 dirPlayerToDefender = GameUtils.HorizonalNormalized(defender.position, m_player.position);
		IM.Number fDistance = GameUtils.HorizonalDistance(defender.position, m_player.position);

		if( IM.Vector3.Angle(m_player.forward, dirPlayerToDefender) > angle || fDistance > range )
			return false;

		if( defender.m_StateMachine.m_curState.m_eState != State.eStand 
		 && defender.m_StateMachine.m_curState.m_eState != State.eDefense
		 && defender.m_StateMachine.m_curState.m_eState != State.eRun
		 && defender.m_StateMachine.m_curState.m_eState != State.eRush )
			return false;

		return true;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		m_player.m_blockable.Update(m_time);

		if( m_player.m_blockable.tooEarly )
			return;

		if(!m_bDefenderOp )
		{
			Player defender = m_player.m_defenseTarget;
			if(m_player.m_blockable.blockable && !m_bChecked)
			{
				if( _ValidDefender(defender) && defender.m_curInputDir != -1 )
				{
					IM.Vector3	vDefenderDir = IM.Quaternion.Euler(IM.Number.zero, defender.m_curInputDir * MoveController.ANGLE_PER_DIR, IM.Number.zero) * IM.Vector3.forward;
					IM.Number fAngleToDir = IM.Vector3.Angle( vDefenderDir, m_crossDir == CrossDir.Left ?m_player.right : -m_player.right );
                    bool bMiss = fAngleToDir < new IM.Number(90);
					if( bMiss )
					{
						PlayerState_Crossed state = defender.m_StateMachine.GetState(State.eCrossed) as PlayerState_Crossed;
						state.left = (m_crossDir == CrossDir.Right);
						state.m_animType = state.left ? AnimType.N_TYPE_0 : AnimType.N_TYPE_1;
						defender.m_StateMachine.SetState(state);
						++m_player.mStatistics.data.success_cross_times;
						m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);
						
						m_bDefenderOp = true;
					}
					else if( m_match.AdjustCrossRate(m_player, defender, IM.Number.zero) != IM.Number.one )
					{
                        if (fAngleToDir > new IM.Number(90))
						{
							_DoDefenseCross(defender);
							Debug.Log("match defense cross.");
						}
					}

					m_bChecked = true;
				}
			}
			else
			{
				Debug.Log("match defense cross not in blockable range.");
			}

			if(m_crossDir != CrossDir.None && m_player.m_blockable.tooLate)
			{
				if( _ValidDefender(defender) )
				{
					IM.Number crossRate = CalcCrossRate(m_player, defender);
					crossRate = m_match.AdjustCrossRate(m_player, defender, crossRate);
					IM.Number crossValue = IM.Random.value;
					crossed = crossValue < crossRate;
					m_bDefenderOp = true;
					Debugger.Instance.m_steamer.message = "Cross succeed: " + crossed + " Rate: " + crossRate + " Value: " + crossValue;
					//should use sendmessage
					if (crossed)
					{
						PlayerState_Crossed state = defender.m_StateMachine.GetState(State.eCrossed) as PlayerState_Crossed;
						state.left = (m_crossDir == CrossDir.Right);
						state.m_animType = state.left ? AnimType.N_TYPE_0 : AnimType.N_TYPE_1;
						defender.m_StateMachine.SetState(state);
						++m_player.mStatistics.data.success_cross_times;
						m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);
						
					}
					//else
					//	_DoDefenseCross(defender);
				}
			}
		}
	}

	void _DoDefenseCross(Player defender)
	{
		PlayerState_DefenseCross state = defender.m_StateMachine.GetState(State.eDefenseCross) as PlayerState_DefenseCross;
		state.targetPos = targetPos;
		state.time = m_player.animMgr.GetDuration(m_curAction) / m_player.animMgr.GetSpeed(m_curAction) - m_time;
		state.crosser = m_player;

		state.InitState();
		defender.m_StateMachine.SetState(state);

		m_bDefenderOp = true;
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

	public static IM.Number CalcCrossRate(Player crosser, Player defender)
	{
		IM.Number control = new IM.Number((int)crosser.m_finalAttrs["control"]);
        IM.Number steal = new IM.Number((int)defender.m_finalAttrs["steal"]);
        return hedging.Calc(control, steal);
	}
}