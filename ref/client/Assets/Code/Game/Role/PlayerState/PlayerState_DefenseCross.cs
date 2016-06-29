using System.Collections.Generic;
using UnityEngine;
using fogs.proto.msg;

public class PlayerState_DefenseCross : PlayerState
{
	public Player 	crosser;
	public IM.Vector3 	targetPos;
	public IM.Number 	time;
	public IM.Vector3 	dirMove;
	public IM.Number	speed;

	public bool		collide;
	public bool		collideToDown;

	private IM.Number	elapsedTime = IM.Number.zero;

	private PseudoRandom random = new PseudoRandom();
	private static 	HedgingHelper colHedging = new HedgingHelper("Collide");
	private static HedgingHelper toCollideHedging = new HedgingHelper("CrossOverToCollide");
	
	public PlayerState_DefenseCross (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eDefenseCross;

		m_mapAnimType[AnimType.N_TYPE_0] = "defencseCrossL";
		m_mapAnimType[AnimType.N_TYPE_1] = "defencseCrossR";

		m_animType = AnimType.N_TYPE_0;
	}

	IM.Number _CalcCrossCollideRate(Player crosser, Player defender)
	{
		uint control = crosser.m_finalAttrs["control"];
		uint steal = defender.m_finalAttrs["steal"];
        return toCollideHedging.Calc(new IM.Number((int)steal), new IM.Number((int)control));
	}

	public void InitState()
	{
		collide = true;
		collideToDown = false;

		IM.Number fCrossCollideRate = _CalcCrossCollideRate(crosser, m_player);
		if (IM.Random.value < fCrossCollideRate)
		{
            IM.Vector3 dirTargetToBasket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, targetPos);
			targetPos = targetPos + dirTargetToBasket * IM.Number.half;
			//collide = false;
		}

		IM.Vector3 vecPlayerToTarget = targetPos - m_player.position;
		vecPlayerToTarget.y = IM.Number.zero;
		dirMove = vecPlayerToTarget.normalized;
		speed = vecPlayerToTarget.magnitude / time;
		
		if (IM.Vector3.Dot(m_player.right, dirMove) > IM.Number.zero)	//right
			m_animType = AnimType.N_TYPE_1;
		else
			m_animType = AnimType.N_TYPE_0;
		m_player.forward = -crosser.forward;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		//Debug.Log("Defense crossed Anim type: " + m_animType);
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, speed * new IM.Number(0,250), false);

		elapsedTime = IM.Number.zero;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( crosser.m_StateMachine.m_curState.m_eState != State.eCrossOver 
		   && crosser.m_StateMachine.m_curState.m_eState != State.eCutIn)
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}
		elapsedTime += fDeltaTime;

		if( elapsedTime > time )
		{
			if( collide )
			{
				//突破/空切倒地几率=0.2+（防守者身体强度-突破者身体强度）*0.0004
				Dictionary<string, uint> crosserData = crosser.m_finalAttrs;
				Dictionary<string, uint> defenderData = m_player.m_finalAttrs;
				if( crosserData == null || defenderData == null)
				{
					Debug.LogError("Can not build player: " + m_player.m_name + " ,can not fight state by id: " + m_player.m_id );
					Debug.LogError("Can not build player: " + crosser.m_name + " ,can not fight state by id: " + crosser.m_id );
					return;
				}
				
				GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
				uint strength = 0;
				crosser.m_skillSystem.HegdingToValue("addn_strength", ref strength);
				IM.Number crosserStrength = (crosserData["strength"] + strength) * curMatch.GetAttrReduceScale("strength", crosser);
				
				m_player.m_skillSystem.HegdingToValue("addn_strength", ref strength);
		        IM.Number defenderStrength = (defenderData["strength"] + strength) * curMatch.GetAttrReduceScale("strength", m_player);

                IM.Number fRate = colHedging.Calc(defenderStrength, crosserStrength);
				fRate = IM.Number.one - fRate;
				
				IM.Number fRandom = IM.Random.value;
				bool sumValue = random.AdjustRate(ref fRate);
				crosser.m_StateMachine.SetState(fRandom < fRate ? PlayerState.State.eFallLostBall : PlayerState.State.eKnocked);
				if (fRandom < fRate && sumValue)
					random.SumValue();
			}
			m_stateMachine.SetState(State.eStand);
			return;
		}
		m_player.MoveTowards(dirMove, m_turningSpeed, fDeltaTime, dirMove * speed);
	}
}