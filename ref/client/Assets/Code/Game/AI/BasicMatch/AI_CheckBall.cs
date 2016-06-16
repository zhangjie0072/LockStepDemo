using UnityEngine;
using System.Collections;
using fogs.proto.msg;

public class AI_CheckBall
	: AIState
{
	public AI_CheckBall(AISystem owner)
		:base(owner)
	{
		m_eType = AIState.Type.eCheckBall;
	}
	
	override public void OnEnter ( AIState lastState )
	{
		m_moveTarget = GetMoveTarget();
	}

	IM.Vector3 GetMoveTarget()
	{
		IM.Vector3 dirBasketToPlayer = GameUtils.HorizonalNormalized(m_player.position, m_basket.m_vShootTarget);

		if( IM.Vector3.Cross(IM.Vector3.right, dirBasketToPlayer).y < IM.Number.zero )
			dirBasketToPlayer = m_player.position.x > IM.Number.zero ? IM.Vector3.right : IM.Vector3.left;

		return m_basket.m_vShootTarget + (m_match.mCurScene.mGround.m3PointBaseLine + IM.Number.one) * dirBasketToPlayer;
	}

    override public void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		OnUpdate(fDeltaTime);
	}

	protected virtual void OnUpdate(IM.Number fDeltaTime)
	{
		if( m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold 
		   && m_player.m_bMovedWithBall )
		{
			m_system.SetTransaction(Type.ePass);
			return;
		}

		if( !m_player.m_bWithBall || !m_match.m_ruler.m_bToCheckBall)
		{
			m_system.SetTransaction(Type.eIdle, new IM.Number(20));
            m_system.SetTransaction(Type.ePositioning, new IM.Number(80));
			return;
		}

		if( m_player.m_team.GetMemberCount() > 1 )
		{
			foreach(Player member in m_player.m_team.members)
			{
				if( member == m_player )
					continue;
				if( member.m_StateMachine.m_curState.m_eState == PlayerState.State.eRequireBall &&
					m_match.mCurScene.mGround.GetArea(member) == Area.eFar )
				{
					AI_Pass pass = m_system.GetState(Type.ePass) as AI_Pass;
					pass.m_toPass = member;
					m_system.SetTransaction(pass);
					return;
				}
			}
		}

		Player defender = m_player.m_defenseTarget;
		if (defender != null && defender.m_AOD.GetStateByPos(m_player.position) != AOD.Zone.eInvalid )
		{
			IM.Vector3 dirBasket2Player = GameUtils.HorizonalNormalized(m_player.position, m_match.mCurScene.mBasket.m_vShootTarget);
			IM.Vector3 dirPlayer2Defender = GameUtils.HorizonalNormalized(defender.position, m_player.position);
            IM.Number angle = IM.Vector3.FromToAngle(dirBasket2Player, dirPlayer2Defender);
			if (0 <= angle && angle < 90)	// Defender at right side.
			{
				IM.Vector3 dirMove = IM.Quaternion.AngleAxis(-new IM.Number(40), IM.Vector3.up) * dirBasket2Player;
				m_moveTarget = m_player.position + dirMove * IM.Number.two;
			}
			else if (270 < angle && angle < 360)	// Defender at left side.
			{
				IM.Vector3 dirMove = IM.Quaternion.AngleAxis(new IM.Number(40), IM.Vector3.up) * dirBasket2Player;
				m_moveTarget = m_player.position + dirMove * IM.Number.two;
			}
		}
		else
			m_moveTarget = GetMoveTarget();
	}
}