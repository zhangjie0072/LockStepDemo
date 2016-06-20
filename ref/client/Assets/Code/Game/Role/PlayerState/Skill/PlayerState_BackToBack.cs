using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_BackToBack : PlayerState_Skill
{
	private	IM.Vector3 m_dirPlayerToBasket;

	public PlayerState_BackToBack (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBackToBack;
		m_bPersistent = true;

		m_validStateTransactions.Add(Command.BackToBack);
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

		m_player.m_bMovedWithBall = true;

		PlayerAnimAttribute.AnimAttr attr = m_player.m_animAttributes.GetAnimAttrById(Command.BackToBack, m_curAction);
		if (attr != null)
		{
			PlayerAnimAttribute.KeyFrame_RotateToBasketAngle keyFrame = attr.GetKeyFrame("RotateToBasketAngle") as PlayerAnimAttribute.KeyFrame_RotateToBasketAngle;
            IM.Number frameRate = m_player.animMgr.GetFrameRate(m_curAction);

            m_dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
			IM.Vector3 dirFaceTo = IM.Quaternion.AngleAxis(keyFrame.angle, IM.Vector3.up) * m_dirPlayerToBasket;
			IM.Number fAngle = IM.Vector3.Angle(m_player.forward, dirFaceTo);
			m_relAngle = keyFrame.angle;
			m_turningSpeed = IM.Math.Deg2Rad(fAngle) / (keyFrame.frame / frameRate);
			m_rotateTo = RotateTo.eBasket;
			m_rotateType = RotateType.eSmooth;
			m_bMoveForward = false;
		}
		m_player.animMgr.CrossFade(m_curAction, false);

		m_player.m_stamina.m_bEnableRecover = false;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

        if (m_player.m_toSkillInstance == null)
        {
            m_stateMachine.SetState(PlayerState.State.eBackToStand);
            return;
        }

        bool moving = false;
        if (m_player.moveDirection != IM.Vector3.zero)
        {
            IM.Number angle = IM.Vector3.Angle(m_dirPlayerToBasket, m_player.moveDirection);
            if (angle < new IM.Number(45) && GameUtils.HorizonalDistance(m_player.position, m_basket.m_vShootTarget) > IM.Number.half)
                moving = true;
        }
        if( moving )
        {
            m_stateMachine.SetState(State.eBackToBackForward, true);
            return;
        }
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_player.m_stamina.m_bEnableRecover = true;
	}
}
