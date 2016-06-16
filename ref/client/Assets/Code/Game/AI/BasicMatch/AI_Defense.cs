using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class AI_Defense : AIState
{
	public enum DefensePosition
	{
		Center,
		Left,
		Right,
		Inside,
	}
	public DefensePosition m_defensePosition = DefensePosition.Center;

	private IM.Number m_fDefenseDist = new IM.Number(1,900);
	private IM.Number m_devBlockTime = IM.Number.zero;
	private IM.Number m_blockTimingRatio = IM.Number.zero;
	private bool m_bToBlock = false;

	private bool m_decidedOnPrepareShoot = false;
	private bool m_decidedOnBackToBack = false;
	private bool m_decidedOnHold = false;
	private bool m_decidedToBlock = false;

	private IM.Number m_decidedOnHoldTime;
	
	public AI_Defense(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eDefense;
	}
	
	protected override void OnTick()
	{
        m_decidedOnHoldTime += m_system.AI.delay; 
        if (m_player.m_defenseTarget != null)
        {
            GameMatch match = GameSystem.Instance.mClient.mCurMatch;
            UBasketball ball = match.mCurScene.mBall;
            IM.Vector3 vBasket = match.mCurScene.mBasket.m_vShootTarget;

			if (ball.m_owner != null && ball.m_owner.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
				m_decidedOnPrepareShoot = false;
			if (ball.m_owner != null && ball.m_owner.m_StateMachine.m_curState.m_eState != PlayerState.State.eBackToBackForward)
				m_decidedOnBackToBack = false;
			if (m_decidedOnHold && (ball.m_owner != null &&
				ball.m_owner.m_StateMachine.m_curState.m_eState != PlayerState.State.eHold &&
				ball.m_owner.m_StateMachine.m_curState.m_eState != PlayerState.State.eStand || m_decidedOnHoldTime > IM.Number.two) )
				m_decidedOnHold = false;

            IM.Number distDef = GameUtils.HorizonalDistance(m_player.m_defenseTarget.position, m_player.position);

            if (AIUtils.ShouldTraceBall(ball, m_player))
            {
                m_system.SetTransaction(AIState.Type.eIdle);
                return;
            }

            if (m_player.m_team.m_role == GameMatch.MatchRole.eDefense)
            {
				//��ס����Ŀ��
				m_fDefenseDist = (m_player.m_defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold ||
					m_player.m_defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.ePrepareToShoot) ? new IM.Number(1,500) : new IM.Number(1,500);
                IM.Vector3 defenseDirX = vBasket - m_player.m_defenseTarget.position;
				defenseDirX.y = IM.Number.zero;
				IM.Number distAttackerToBasket = defenseDirX.magnitude;
				defenseDirX.Normalize();

				if (m_defensePosition == DefensePosition.Left)
				{
					defenseDirX = IM.Quaternion.AngleAxis(-m_player.m_defenseTarget.m_AOD.angle / 4, IM.Vector3.up) * defenseDirX;
				}
				else if (m_defensePosition == DefensePosition.Right)
				{
					defenseDirX = IM.Quaternion.AngleAxis(m_player.m_defenseTarget.m_AOD.angle / 4, IM.Vector3.up) * defenseDirX;
				}

                IM.Vector3 targetToMove;
				if (m_defensePosition == DefensePosition.Inside && distAttackerToBasket  > (new IM.Number(1,900) + m_fDefenseDist))
					targetToMove = vBasket + (-defenseDirX) * new IM.Number(1,900);
				else
					targetToMove = m_player.m_defenseTarget.position + defenseDirX * m_fDefenseDist;
                if (GameUtils.HorizonalDistance(m_player.position, targetToMove) > IM.Number.half)
                    m_moveTarget = targetToMove;
				//���ַ��ص���
				if (m_defensePosition == DefensePosition.Center)
				{
					if (distAttackerToBasket > new IM.Number(2,100))
					{
						IM.Number distMoveTargetToBasket = GameUtils.HorizonalDistance(vBasket, m_moveTarget);
						if (distMoveTargetToBasket < new IM.Number(2,100))
						{
							m_moveTarget = vBasket - defenseDirX * new IM.Number(2,100);
						}
					}
				}

				if (m_player.m_defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eBackToBackForward &&
					distDef <= new IM.Number(2,200))
				{
					if (!m_decidedOnBackToBack)
					{
						IM.Number competeWeight = new IM.Number(50);
						switch (m_player.m_position)
						{
							case PositionType.PT_SF:
								competeWeight += new IM.Number(30);
								break;
							case PositionType.PT_PG:
							case PositionType.PT_C:
								competeWeight += new IM.Number(100);
								break;
						}
						m_system.SetTransaction(AIState.Type.eDefenseBack, competeWeight);
						m_system.SetTransaction(AIState.Type.eDefense, new IM.Number(50), true);
						m_decidedOnBackToBack = true;
						return;
					}
				}
				else if ( m_player.m_defenseTarget.m_bWithBall &&
					(m_player.m_defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold ||
					m_player.m_defenseTarget.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand) )
				{
					if (!m_decidedOnHold)
					{
						IM.Number stealRate = AIUtils.GetStealRate(m_player, m_player.m_defenseTarget, m_match);
						if (stealRate > IM.Number.zero)
						{
							m_system.SetTransaction(AIState.Type.eSteal, stealRate * 100);
						}
						m_system.SetTransaction(AIState.Type.eDefense, (IM.Number.one - stealRate) * 100);
						m_decidedOnHold = true;
						m_decidedOnHoldTime = IM.Number.zero;
						return;
					}
				}
				else if (ball.m_ballState != BallState.eLoseBall &&
					IM.Vector3.Angle(m_player.forward, m_player.m_defenseTarget.forward) > new IM.Number(90) && distDef < IM.Number.two)
				{
					List<SkillInstance> defenseSkill = m_player.m_skillSystem.GetBasicSkillsByCommand(Command.Defense);
					m_player.m_toSkillInstance = defenseSkill[0];
				}

				//Э����ñ����
				IM.Number assistBlockRate = IM.Number.zero;
				switch (m_player.m_position)
				{
					case PositionType.PT_C:
					case PositionType.PT_PF:
						assistBlockRate = new IM.Number(0,750);
						break;
					case PositionType.PT_SF:
						assistBlockRate = new IM.Number(0,600);
						break;
					case PositionType.PT_PG:
					case PositionType.PT_SG:
						assistBlockRate = new IM.Number(0,400);
						break;
				}

				if (m_ball.m_owner != null &&
					(m_ball.m_owner == m_player.m_defenseTarget ||
					(IM.Random.value < assistBlockRate && GameUtils.HorizonalDistance(m_ball.m_owner.position, m_player.position) < new IM.Number(3))) &&
					AIUtils.IsAttacking(m_ball.m_owner))
				{
					if (!m_decidedToBlock)
					{
						//m_devBlockTime = Random.Range(0f, m_system.AI.devTimeBlock);
						m_devBlockTime = m_system.AI.devTimeBlock;
						//m_blockTimingRatio = Random.value;
						m_blockTimingRatio = IM.Number.zero;
						//float toBlockRate = Mathf.Clamp(m_player.m_fightingCapacity / m_ball.m_owner.m_fightingCapacity * 0.2f, 0.05f, 0.5f);
					    IM.Number toBlockRate = IM.Number.half;
						IM.Number value = IM.Random.value;
						m_bToBlock = value < toBlockRate;
						Logger.Log(m_player.m_name + "Block time dev:" + m_devBlockTime +
							" To block rate: " + toBlockRate + " " + value + " " + m_bToBlock);
						m_decidedToBlock = true;
					}

					IM.Vector3 owner2Player = m_player.position - m_ball.m_owner.position;
					owner2Player.y = IM.Number.zero;
					IM.Number fDistOwner2Player = owner2Player.magnitude;
					fDistOwner2Player *= IM.Number.one - m_system.AI.devDistBlock;

					if (m_ball.m_owner.m_AOD.GetStateByPos(m_player.position) == AOD.Zone.eInvalid)
					{
						m_moveTarget = m_ball.m_owner.position + m_ball.m_owner.forward * m_fDefenseDist;
						m_system.SetTransaction(Type.eDefense);
					}
					else if (m_ball.m_owner.m_StateMachine.m_curState.m_eState == PlayerState.State.ePrepareToShoot)
					{
						if (!m_decidedOnPrepareShoot && !m_bToBlock)
						{
							if (m_player.m_defenseTarget == m_ball.m_owner)
							{
								IM.Number stealRate = AIUtils.GetStealRate(m_player, m_ball.m_owner, m_match);
								Logger.Log(m_player.m_name + " Steal rate: " + stealRate);
								if (stealRate > IM.Number.zero)
								{
									m_system.SetTransaction(Type.eSteal, stealRate * 100);
								}
								m_system.SetTransaction(Type.eDefense, (IM.Number.one - stealRate) * 100);
							}
							m_decidedOnPrepareShoot = true;
						}
					}

					if (m_bToBlock &&
						m_player.m_StateMachine.m_curState.IsCommandValid(Command.Block) &&
						AIUtils.CanBlock(m_player, m_ball.m_owner, m_devBlockTime, m_blockTimingRatio, m_basket.m_vShootTarget))
						m_system.SetTransaction(Type.eBlock);
				}
				else
				{
					if (m_decidedToBlock)
						m_decidedToBlock = false;
				}
            }
            else
				m_system.SetTransaction(AIState.Type.ePositioning);
        }
	}
}