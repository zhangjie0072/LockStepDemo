using UnityEngine;
using fogs.proto.msg;
using System.Collections.Generic;
using System.Linq;

public class AI_Positioning
		: AIState
{
	public enum Intent
	{
		eNone,
		eShoot,
		eDunk,
		eLayup
	}
	
	public Intent	m_intent;

	bool arriveFirstTarget;
	public bool makePolicyImmediately;
	public bool unreasonable;
	bool positionForPR;	// for pick and roll

	bool lastWithBall;

	IM.Vector3 lastPosition;
	IM.Number stillTime;

	static IM.Number MAX_STILL_TIME = IM.Number.one;
	
	public AI_Positioning(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.ePositioning;
		m_intent = Intent.eNone;
	}

	override public void OnEnter(AIState lastState)
	{
		arriveFirstTarget = false;
		if (makePolicyImmediately)
		{
			arriveFirstTarget = true;
			makePolicyImmediately = false;
		}

		bool inTakeOver = false;
		if (m_player.m_inputDispatcher != null)
			inTakeOver = m_player.m_inputDispatcher.inTakeOver;
		bool isDefended = m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD);

		if ((m_match.mCurScene.mGround.GetArea(m_player) == Area.eNear || !isDefended) &&
			AIUtils.InAttackableDistance(m_match, m_player) && !inTakeOver && m_player.m_bWithBall)
		{
			m_moveTarget = m_player.position;
			arriveFirstTarget = true;
		}
		else
		{
			if ((inTakeOver || stillTime > MAX_STILL_TIME) && isDefended && m_player.m_bWithBall)
			{
				m_moveTarget = GetPositionWithBall(true);
			}
			else if (positionForPR && m_ball.m_owner != null)
			{
				m_moveTarget = GetPositionForPickAndRoll();
			}
			else if (unreasonable)
			{
				IM.Vector3 target = m_match.mCurScene.mBasket.m_vShootTarget;
				target.y = IM.Number.zero;
				m_moveTarget = target;
			}
			else
			{
				if (m_player.m_bWithBall)
					m_moveTarget = GetPositionWithBall();
				else
					m_moveTarget = GetPositionWithOutBall();
			}
		}

		lastPosition = IM.Vector3.zero;
		stillTime = IM.Number.zero;
		unreasonable = false;
		positionForPR = false;
	}

	IM.Vector3 GetPositionWithBall(bool moveHori = false)
	{
		Player defender = m_player.m_defenseTarget;
		IM.Vector3 dirRim2Player = GameUtils.HorizonalNormalized(m_player.position,m_basket.m_vShootTarget);
		IM.Vector3 dirRim2Defender = GameUtils.HorizonalNormalized(defender.position, m_basket.m_vShootTarget);
		bool defenderAtLeft = IM.Vector2.Dot(dirRim2Defender.xy, IM.Vector2.right) < IM.Vector2.Dot(dirRim2Player.xy, IM.Vector2.right);
		IM.Number angle = moveHori ? new IM.Number(90) : IM.Random.Range(new IM.Number(15),new IM.Number(40)) * (defenderAtLeft ? IM.Number.one : -IM.Number.one);
		IM.Vector3 dirPositioning = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * -dirRim2Player;
		IM.Vector3 position = m_player.position + dirPositioning * IM.Number.two;
		if (position.z > m_match.mCurScene.mBasket.m_vShootTarget.z || position.z < (m_match.mCurScene.mGround.mCenter.z + new IM.Number(0,300)) ||
            position.x > (m_match.mCurScene.mGround.mHalfSize.x - new IM.Number(0, 300)) || position.x < -(m_match.mCurScene.mGround.mHalfSize.x - new IM.Number(0, 300)))
		{
			angle = -angle;
			dirPositioning = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * -dirRim2Player;
			position = m_player.position + dirPositioning * IM.Number.two;
		}
		return position;
	}

	IM.Vector3 GetPositionWithOutBall()
	{
		int colomn = RoadPathManager.Instance.m_AngleList.Count;
		IM.Vector2 dirRim2Player = (m_player.position - m_basket.m_rim.center).normalized.xy;
		int sectorIdx = 0;
		int iCol = 0;
		if( IM.Vector2.Dot(dirRim2Player, IM.Vector2.right) > IM.Number.zero )
			iCol = IM.Random.Range(0, (int)m_player.m_favorSectors.range.x / 2);
		else
			iCol = IM.Random.Range((int)m_player.m_favorSectors.range.x / 2, (int)m_player.m_favorSectors.range.x);

		int iRow = (int)m_player.m_favorSectors.start.y + IM.Random.Range(0, (int)m_player.m_favorSectors.range.y);
		sectorIdx = colomn * iRow + iCol;
		
		RoadPathManager.Sector sector = RoadPathManager.Instance.m_Sectors[sectorIdx];
		IM.Number dev = m_system.AI.devFavArea;
		int count = 0;
		do
		{
			IM.Vector3 pos = new IM.Vector3(sector.center.x + IM.Random.Range(-dev, dev), IM.Number.zero, sector.center.y + IM.Random.Range(-dev, dev));
			sectorIdx = RoadPathManager.Instance.CalcSectorIdx(pos);
		} while (sectorIdx == -1 && count++ < 10);
		if (sectorIdx != -1)
			sector = RoadPathManager.Instance.m_Sectors[sectorIdx];
		else
			Debug.LogError("AI_Positioning, sector(-1). Name:" + m_player.m_name +
				" SectorIdx:" + sector.idx + " Center:" + sector.center + " Dev:" + dev);
		RoadPathManager.Instance.AddDrawSector( m_player.m_id + "_ToSector", sector );

        return sector.center.x0z;
	}

	IM.Vector3 GetPositionForPickAndRoll()
	{
		Player defenderOfBallHolder = m_match.mCurScene.mBall.m_owner.m_defenseTarget;
		IM.Vector3 vecPlayer2Defender = defenderOfBallHolder.position - m_player.position;
		IM.Vector3 moveTarget = m_player.position + vecPlayer2Defender * new IM.Number(0,300);
		return moveTarget;
	}

	void _Attack()
	{
		if (!AIUtils.InAttackableDistance(m_match, m_player))
			return;

		IM.Number attackWeight = m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD) ? new IM.Number(15) : new IM.Number(100);
		AIUtils.AttackByPosition(m_player, attackWeight);
	}

	bool Pass()
	{
		IM.Number passWeight = m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD) ? new IM.Number(30) : IM.Number.zero;
		Player target = AIUtils.GetMostPriorPassTarget(m_player.m_team.members, m_player);
		if( target != null && target != m_player)
		{
			//目标要球，权重提高
			if (target.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall)
			{
				IM.Number weightAdj = IM.Number.zero;
				switch (m_player.m_position)
				{
					case PositionType.PT_C:
					case PositionType.PT_PG:
						weightAdj = new IM.Number(200);
						break;
					case PositionType.PT_PF:
						weightAdj = new IM.Number(50);
						break;
					case PositionType.PT_SF:
					case PositionType.PT_SG:
                        weightAdj = new IM.Number(20);
						break;
				}
				passWeight += weightAdj;
			}
			if (passWeight > IM.Number.zero)
			{
				AI_Pass pass = m_system.GetState(Type.ePass) as AI_Pass;
				pass.m_toPass = target;
				m_system.SetTransaction(pass, passWeight);
				return true;
			}
		}
		return false;
	}

	bool CrossOver()
	{
		Player defenser = m_player.m_defenseTarget;
		if (defenser != null)
		{
			if (m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD))
			{
				IM.Vector3 dirToMoveTarget = (m_moveTarget - m_player.position).normalized;
				IM.Vector3 vBasket = m_basket.m_vShootTarget;
				IM.Vector3 dirToBasket = (vBasket - m_player.position).normalized;

				IM.Number fAngleToMoveTarget = IM.Vector3.Angle(dirToMoveTarget, m_player.forward);
				IM.Number fAngleBetweenBasketMoveTarget = IM.Vector3.Angle(dirToMoveTarget, dirToBasket);
				IM.Number fDistToBasket = GameUtils.HorizonalDistance(m_player.position, vBasket);
				//if (fAngleToMoveTarget < 45.0f && fAngleBetweenBasketMoveTarget < 60.0f && fDistToBasket > 3.0f)
				if (m_player.m_stamina.m_curStamina >= AI_CrossOver.STAMINA_COST)
				{
					IM.Number crossOverWeight = new IM.Number(30);
					switch (m_player.m_position)
					{
						case PositionType.PT_PG:
							crossOverWeight += new IM.Number(50);
							break;
						case PositionType.PT_C:
						case PositionType.PT_PF:
							crossOverWeight += new IM.Number(20);
							break;
						case PositionType.PT_SF:
						case PositionType.PT_SG:
							crossOverWeight += new IM.Number(100);
							break;
					}
					m_system.SetTransaction(AIState.Type.eCrossOver, crossOverWeight);
				}
				m_system.SetTransaction(AIState.Type.ePositioning, new IM.Number(20), true);
				m_system.SetTransaction(AIState.Type.eFakeShoot, new IM.Number(10));
				return true;
			}
		}
		return false;
	}

	void Offense()
	{
		if (unreasonable)
		{
			if (m_player.CanLayup())
			{
				m_system.SetTransaction(AIState.Type.eLayup);
				Debug.LogError("Unreasonable layup.");
				return;
			}
		}

		if (!arriveFirstTarget)
			return;

		bool inTakeOver = false;
		if (m_player.m_inputDispatcher != null)
			inTakeOver = m_player.m_inputDispatcher.inTakeOver;

        bool isPvp = m_system.IsPvp;
		if (!inTakeOver)
		{
            if (!isPvp)
            {
                //进攻时间快结束了，投球
                if (m_match.IsFinalTime(new IM.Number(3) - m_system.AI.devTime))
                {
                    AIUtils.AttackByPosition(m_player, new IM.Number(100));
                    return;
                }
                //玩家要球，传给他
                Player mainRole = m_match.GetMainRole(m_player.m_roleInfo.acc_id);
                if (mainRole != null && mainRole.m_team == m_player.m_team && mainRole != m_player &&
                    mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall)
                {
                    AI_Pass pass = m_system.GetState(Type.ePass) as AI_Pass;
                    pass.m_toPass = mainRole;
                    m_system.SetTransaction(pass, new IM.Number(100));
                    return;
                }

                //进攻篮筐
                _Attack();
            }

			//传球
			Pass();

            if( !isPvp)
            {
                Area area = m_match.mCurScene.mGround.GetArea(m_player);
                if (area != Area.eNear)	//非篮下区
                {
                    if (!inTakeOver)
                    {
                        //突破
                        CrossOver();
                        if (!AIUtils.InAttackableDistance(m_match, m_player))
                            m_system.SetTransaction(Type.ePositioning, new IM.Number(50), true);
                    }
                }
            }
		}
		else
            m_system.SetTransaction(Type.ePositioning, new IM.Number(100), true);
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		//状态切换
		if( m_player.m_team.m_role != GameMatch.MatchRole.eOffense )
		{
			m_system.SetTransaction(AIState.Type.eDefense);
			return;
		}
		//到点
        if (GameUtils.HorizonalDistance(m_moveTarget, m_player.position) < new IM.Number(0,100))
		{
			arriveFirstTarget = true;
			OnTick();
			if (!m_player.m_bWithBall)
			{
				RoadPathManager.Sector targetSector = null;
				int iCollideSector = RoadPathManager.Instance.CalcSectorIdx(m_moveTarget);
				if (iCollideSector == -1)
					targetSector = RoadPathManager.Instance.m_Sectors[0];
				else
					targetSector = RoadPathManager.Instance.Bounce(m_player.position.xz, RoadPathManager.Instance.m_Sectors[iCollideSector], m_player.m_bounceSectors);

				IM.Number dev = m_system.AI.devFavArea;
				int count = 0;
				int idx = -1;
				do
				{
					IM.Vector3 pos = targetSector.center.x0z + new IM.Vector3(IM.Random.Range(-dev, dev), IM.Number.zero, IM.Random.Range(-dev, dev));
					idx = RoadPathManager.Instance.CalcSectorIdx(pos);
				} while (idx == -1 && count++ < 10);
				if (idx != -1)
					targetSector = RoadPathManager.Instance.m_Sectors[idx];
				else
					Debug.LogError("AI_Positioning, sector(-1). Name:" + m_player.m_name +
						" SectorIdx:" + targetSector.idx + " Center:" + targetSector.center + " Dev:" + dev);
				RoadPathManager.Instance.AddDrawSector("targetSector", targetSector);
				m_moveTarget = targetSector.center.x0z;
				RoadPathManager.Instance.AddDrawSector(m_player.m_id + "_ToSector", targetSector);
				arriveFirstTarget = false;
			}
		}

		//卡死容错，静止超过一定时间后，强制重新决策
		if (m_match.m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
			if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eRun ||
				m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eRush ||
				m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
			{
				IM.Vector3 curPosition = m_player.position;
				if (IM.Math.Abs(lastPosition.x - curPosition.x) < new IM.Number(0,100) && IM.Math.Abs(lastPosition.z - lastPosition.z) < new IM.Number(0,100))
				{
					stillTime += fDeltaTime;
					if (stillTime > MAX_STILL_TIME)
					{
						//Debug.Log("AI_Positioning, " + m_player.m_name + " still time larger than " + MAX_STILL_TIME + " seconds");
						//arriveFirstTarget = true;
						//OnTick();
						m_system.SetTransaction(AIState.Type.ePositioning, new IM.Number(100), true);
					}
				}
				else
					stillTime = IM.Number.zero;
			}
			else
                stillTime = IM.Number.zero;
		}
		else
            stillTime = IM.Number.zero;
		lastPosition = m_player.position;

		if (m_player.m_bWithBall != lastWithBall)
			arriveFirstTarget = true;
		lastWithBall = m_player.m_bWithBall;
	}

	protected override void OnTick()
	{
		//持球进攻
		if (m_player.m_bWithBall)
		{
			PlayerState.State state = m_player.m_StateMachine.m_curState.m_eState;
			if (m_match.m_ruler.m_bToCheckBall)	//带出三分线
			{
				if (state == PlayerState.State.eStand ||
					state == PlayerState.State.eRun ||
					state == PlayerState.State.eRush ||
					state == PlayerState.State.eHold)
				{
					bool inTakeOver = false;
					if (m_player.m_inputDispatcher != null)
						inTakeOver = m_player.m_inputDispatcher.inTakeOver;
					if (!inTakeOver)
					{
						Player passTarget = AIUtils.ChoosePassTargetCheckBall(m_player, m_match);
						if (passTarget != null)
						{
							AI_Pass pass = m_system.GetState(Type.ePass) as AI_Pass;
							pass.m_toPass = passTarget;
							m_system.SetTransaction(pass);
						}
						else
							m_system.SetTransaction(AIState.Type.eCheckBall);
					}
					else
						m_system.SetTransaction(AIState.Type.eCheckBall);
				}
			}
			else
			{
				if (state == PlayerState.State.eStand ||
					state == PlayerState.State.eRun ||
					state == PlayerState.State.eRush)
				{
					Offense();
				}
				else if (state == PlayerState.State.eHold)
				{
					if ((m_match.mCurScene.mGround.GetArea(m_player) == Area.eNear ||
						!m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD)) &&
						arriveFirstTarget)
						Offense();
					else
						m_system.SetTransaction(AIState.Type.eIdle);
				}
			}
		}
		else	//非持球进攻
		{
			//空切
			if (arriveFirstTarget && m_ball.m_owner != null && IM.Random.value < new IM.Number(0,250) &&
				m_player.IsDefended(m_system.AI.devAngleAttacker, m_system.AI.devDistAOD))
			{
				//if( !(m_match is GameMatch_PVP) )
					m_system.SetTransaction(AIState.Type.eCutIn);
			}
			//要球
			else if (m_ball.m_owner != null && m_ball.m_owner.m_team == m_player.m_team && m_system.m_bNotDefended && !m_match.m_ruler.m_bToCheckBall)
			{
				//if( !(m_match is GameMatch_PVP) )
					m_system.SetTransaction(AIState.Type.eRequireBall);
			}
			//靠近球6米，追球
			else if (AIUtils.ShouldTraceBall(m_ball, m_player))
			{
				m_system.SetTransaction(AIState.Type.eIdle);
			}
			//挡拆
			else
			{
				int result = AIUtils.CanPickAndRoll(m_player);
				if (result == 1 && m_player.m_StateMachine.m_curState.IsCommandValid(Command.PickAndRoll))
					m_system.SetTransaction(AIState.Type.ePickAndRoll);
				else if (result == 2)
				{
					positionForPR = true;
					m_system.SetTransaction(AIState.Type.ePositioning, new IM.Number(100), true);
				}
			}
		}
	}

	public override void OnPlayerCollided(Player colPlayer)
	{
		if (colPlayer.m_team.m_role != m_player.m_team.m_role)
		{
			//Debug.Log(m_player.m_name + " Collide rival team member: " + colPlayer.m_name + ", repositioning.");
			if (!unreasonable)
			{
				arriveFirstTarget = true;
				OnTick();
			}
		}
	}
}