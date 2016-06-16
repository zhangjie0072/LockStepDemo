using UnityEngine;
using fogs.proto.msg;
using System.Collections.Generic;

public class AI_Idle
	: AIState
{
	bool makePolicyImmediately;

	public AI_Idle(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eIdle;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_player.moveDirection = IM.Vector3.zero;

		if (lastState.m_eType == Type.eRebound)
			makePolicyImmediately = true;
	}

	protected override void OnTick()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		UBasketball ball = match.mCurScene.mBall;

		bool inTakeOver = false;
		if (m_player.m_inputDispatcher != null)
			inTakeOver = m_player.m_inputDispatcher.inTakeOver;

		if (!inTakeOver)
		{
			//进攻时间快结束了，投球
			if (m_player.m_bWithBall && match.IsFinalTime(new IM.Number(3) - m_system.AI.devTime) && !m_match.m_ruler.m_bToCheckBall )
			{
				AIUtils.AttackByPosition(m_player, new IM.Number(100));
				return;
			}
		}

		//球在飞，追球
		if (m_ball.m_owner == null && m_ball.m_ballState != BallState.eUseBall_Pass)
		{
			if (AIUtils.ShouldTraceBall(m_ball, m_player))
			{
				m_system.SetTransaction(AIState.Type.eTraceBall);
			}
			else if (m_match.m_stateMachine.m_curState.m_eState != MatchState.State.eTipOff)
			{
				m_system.SetTransaction(AIState.Type.ePositioning);
			}
			return;
		}

		if (!inTakeOver)
		{
			//抱球时
			Area area = m_match.mCurScene.mGround.GetArea(m_player);
			if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold)
			{
				if (m_player.m_bMovedWithBall)	//二次运球
				{
					if (m_player.IsDefended())	//被防守
					{
                        if (!m_system.IsPvp)
                        {
                            AIUtils.AttackByPosition(m_player, new IM.Number(15));
                            m_system.SetTransaction(Type.eFakeShoot, new IM.Number(10));
                        }
                      
                        List<Player> teammates = new List<Player>(m_player.m_team.members);
                        teammates.Remove(m_player);
                        Player passTarget = AIUtils.GetMostPriorPassTarget(teammates, m_player);
                        if (passTarget != null)
                        {
                            AI_Pass pass = m_system.GetState(Type.ePass) as AI_Pass;
                            pass.m_toPass = passTarget;
                            m_system.SetTransaction(Type.ePass, new IM.Number(75));
                        }				
					}
					else	//未被防守
					{
						AIUtils.AttackByPosition(m_player, new IM.Number(100));
					}
				}
				else	//非二次运球
				{
					m_system.SetTransaction(Type.ePositioning);
				}

				return;
			}
		}

		//攻防转换
		if (m_player.m_team.m_role == GameMatch.MatchRole.eOffense)
		{
			if (makePolicyImmediately)
			{
				AI_Positioning aiPositioning = m_system.GetState(AIState.Type.ePositioning) as AI_Positioning;
				aiPositioning.makePolicyImmediately = true;
				makePolicyImmediately = false;
			}
			m_system.SetTransaction(AIState.Type.ePositioning);
		}
		else
			m_system.SetTransaction(AIState.Type.eDefense);
	}
}