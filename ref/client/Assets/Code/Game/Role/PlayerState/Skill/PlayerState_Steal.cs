using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Steal : PlayerState_Skill
{
	public bool forcedByAI;
	public bool m_bGetBall = false;
	public Player 	stealTarget;

	public System.Action onSteal;

	uint 	stealValue;
	IM.Number 	ratio;
	public bool	m_success{get; private set;}

	PseudoRandomGroup random = new PseudoRandomGroup();
	static HedgingHelper hedging = new HedgingHelper("Steal");

	public PlayerState_Steal (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eSteal;
	}

	public override bool PreEnter()
	{
		/*
		if ( (m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS || m_match.GetMatchType() == GameMatch.Type.ePVP_3On3)
		    && m_player != m_match.m_mainRole)
			return true;

		if (!forcedByAI && !InStealPosition(m_player, m_ball) && InAutoPosition(m_player, m_ball))
		{
			m_stateMachine.assistAI.Enable(AIState.Type.eAssistSteal);
			(m_stateMachine.assistAI.GetState(AIState.Type.eAssistSteal) as AI_Assist_Steal).cachedSkill = m_player.m_toSkillInstance;
			return false;
		}
		forcedByAI = false;
		*/
		return true;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		if( !m_player.m_bSimulator )
		{
			stealTarget = null;
			ratio = IM.Number.zero;

			if (m_ball != null)
				m_player.FaceTo(m_ball.position);

			bool bValid;
			m_success = _StealBall(out bValid);
			if( m_success )
			{
				SkillSpec stealSkill = m_player.GetSkillSpecialAttribute(SkillSpecParam.eSteal_get_ball_rate);
				m_bGetBall = IM.Random.value < stealSkill.value;
			}

			GameMsgSender.SendSteal(m_player, stealTarget, m_curExecSkill, m_bGetBall, bValid);
			++m_player.mStatistics.data.steal_times;
			if (bValid)
				++m_player.mStatistics.data.valid_steal_times;
		}

		//m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.Steal);
	}

	public void OnSteal()
	{
		if( !m_player.m_bSimulator )
		{
			if( m_success )
			{
				if(!m_bGetBall)
				{
					stealTarget.m_lostBallContext.vInitPos 	 = m_ball.position;
                    stealTarget.m_lostBallContext.vInitPos.y = IM.Math.Max(m_ball.position.y, m_player.position.y + new IM.Number(0,300));
                    stealTarget.m_lostBallContext.vInitPos = m_player.right.normalized;
				}

				GameMsgSender.SendStolen(m_player, stealTarget, !m_bGetBall, stealTarget.m_lostBallContext.vInitPos.ToUnity2(), stealTarget.m_lostBallContext.vInitVel.ToUnity2());
				PlayerState_Stolen stolen = stealTarget.m_StateMachine.GetState(State.eStolen) as PlayerState_Stolen;
				stolen.m_bLostBall = !m_bGetBall;
				stealTarget.m_StateMachine.SetState(stolen);
			}
		}

		/*
		if(m_bGetBall)
		{
			stealTarget.DropBall(m_ball);
			m_player.GrabBall(m_ball);

			m_player.m_eventHandler.NotifyAllListeners(UPlayerActionEventHandler.AnimEvent.ePickupBall);

			AudioClip clip = AudioManager.Instance.GetClip("Misc/Catch_02");
			if( clip != null )
				AudioManager.Instance.PlaySound(clip);
		}
		*/

		if( m_success )
			m_player.eventHandler.NotifyAllListeners(PlayerActionEventHandler.AnimEvent.eSteal);

		m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, m_success);

		if (onSteal != null)
			onSteal();
	}
	

	/*
	void OnBallLost(UBasketball ball)
	{
		if (stealTarget == null)
			return;
		if (!getBall)
		{
			Vector3 vCurBallPos = stealTarget.m_ballSocket.transform.position;
			vCurBallPos.y = Mathf.Max(vCurBallPos.y, m_player.position.y + 0.3f);
			ball.transform.position = vCurBallPos;
			Vector3 dropPoint = GenerateDropPoint();
			dropPoint.y = ball.m_ballRadius;
			float fallTime = Mathf.Sqrt(2 * (ball.transform.position.y - dropPoint.y) / 9.8f);
			Vector3 dir = dropPoint - ball.transform.position;
			dir.y = 0f;
			float speed = dir.magnitude / fallTime;
			ball.rigidbody.velocity = dir.normalized * speed;
		}
		else
		{
			m_player.GrabBall(ball);
			if (m_match.EnableSwitchRole())
				m_match.m_ruler.SwitchRole();
		}
	}


	Vector3 GenerateDropPoint()
	{
		Vector3 dir = Quaternion.AngleAxis(Random.Range(0f, 180f), Vector3.up) * stealTarget.forward;
		float dist = Random.Range(0.8f, 2f);
		Vector3 point = stealTarget.position + dir * dist;
		point.y = 0f;
		return point;
	}
	*/

	protected override void _OnActionDone()
	{
		if (m_player.m_bWithBall)
		{
			m_player.m_StateMachine.SetState(PlayerState.State.eHold);
		}
		else
		{
			base._OnActionDone();
		}
	}

	public static bool InStealPosition(Player player, UBasketball ball)
	{
		return InFrontOfBallOwner(player, ball, new IM.Number(1,500));
	}

	public static bool InAutoPosition(Player player, UBasketball ball)
	{
        return InFrontOfBallOwner(player, ball, new IM.Number(3));
	}

	static bool InFrontOfBallOwner(Player player, UBasketball ball, IM.Number distThreshold)
	{
		if (ball.m_owner == null)
			return false;

		//distance
		IM.Number dist = GameUtils.HorizonalDistance(player.position, ball.position);
		if (dist > distThreshold)
		{
			Logger.Log("Steal distance: " + dist + " greater than " + distThreshold);
			return false;
		}

		//is in front of target
		IM.Vector3 dir = player.position - ball.m_owner.position;
		dir.Normalize();
		IM.Number angle = IM.Vector3.Angle(ball.m_owner.forward, dir);
		if (angle > new IM.Number(90))
		{
			Logger.Log("Steal angle " + angle);
			return false;
		}

		return true;
	}

	public static IM.Vector3 GetStealPosition(Player stealTarget)
	{
		return stealTarget.forward * new IM.Number(1,400) + stealTarget.position;
	}

	bool _StealBall(out bool bVaild)
	{
		bVaild = false;

		if( m_ball.m_owner == null )
			return false;
		stealTarget = m_ball.m_owner;
		if (!stealTarget.m_bOnGround)
			return false;
		PlayerState.State ps = stealTarget.m_StateMachine.m_curState.m_eState;
		ratio = GameSystem.Instance.StealConfig.GetRatio(ps);
		if (ratio == IM.Number.zero)
		{
			Logger.Log("Steal failed, state: " + ps);
			return false;
		}

		if( m_ball.m_ballState != BallState.eUseBall )
			return false;
		Dictionary<string, uint> targetData = stealTarget.m_finalAttrs;
		Dictionary<string, uint> playerData = m_player.m_finalAttrs;
		
		if (!InStealPosition(m_player, m_ball))
		{
			Logger.Log("Steal failed.");
			return false;
		}

		uint stealValue = 0;
		m_player.m_skillSystem.HegdingToValue("addn_steal", ref stealValue );

		uint control = 0;
		stealTarget.m_skillSystem.HegdingToValue("addn_control", ref control );

		IM.Number antiStealRate = (targetData["control"] + control ) * m_match.GetAttrReduceScale("control", stealTarget);
		IM.Number playerStealRate = (playerData["steal"] + stealValue) * m_match.GetAttrReduceScale("steal", m_player);
        IM.Number stealRate = hedging.Calc(playerStealRate, antiStealRate);

		SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.eSteal_rate);
		if( skillSpec.paramOp == SkillSpecParamOp.eAdd )
            stealRate += skillSpec.value;
		else if( skillSpec.paramOp == SkillSpecParamOp.eMulti )
            stealRate *= skillSpec.value;
		stealRate = stealRate * ratio;
        bool sumValue = random[stealTarget].AdjustRate(ref stealRate);
		Debugger.Instance.m_steamer.message = " Steal ball: stealRate: " + stealRate;
		
		IM.Number fRandomValue = IM.Random.value;
		Debugger.Instance.m_steamer.message += " Random value: " + fRandomValue;

		bVaild = true;
		if( fRandomValue > stealRate )
		{
			Debugger.Instance.m_steamer.message += "steal failed.";
			return false;
		}

		if (sumValue)
			random[stealTarget].SumValue();
		Debugger.Instance.m_steamer.message += ", steal success.";
				
		return true;
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_bGetBall = false;
	}

}