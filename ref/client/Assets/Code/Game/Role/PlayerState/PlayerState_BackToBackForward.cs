using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_BackToBackForward : PlayerState
{
	private	IM.Vector3 m_dirPlayerToBasket;

	public PlayerState_BackToBackForward (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBackToBackForward;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "clutchRunL");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "clutchRunR");

		m_animType = AnimType.B_TYPE_0;
			
		m_validStateTransactions.Add(Command.BackToBack);
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);

        if (m_player.m_eHandWithBall == Player.HandWithBall.eLeft)
            m_animType = AnimType.B_TYPE_0;
        else if (m_player.m_eHandWithBall == Player.HandWithBall.eRight)
            m_animType = AnimType.B_TYPE_1;			

		m_curAction = m_mapAnimType[m_animType];

        m_dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
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

		bool bKnocked = false;
		Player target = m_player.m_defenseTarget;

        if( m_player.m_toSkillInstance == null )
        {
            m_player.m_StateMachine.SetState(State.eStand);
            return;
        }

        if (m_player.moveDirection == IM.Vector3.zero)
        {
            m_player.m_StateMachine.SetState(State.eBackToBack, true);
            return;
        }
        IM.Number angle = IM.Vector3.Angle(m_dirPlayerToBasket, m_player.moveDirection);
        if (angle > new IM.Number(45) || GameUtils.HorizonalDistance(m_player.position, m_basket.m_vShootTarget) < IM.Number.half)
        {
            m_player.m_StateMachine.SetState(State.eBackToBack, true);
            return;
        }

        if ( target != null )
        {
            IM.Vector3 vecPlayerToDefender = target.position - m_player.position;
            IM.Number dist = vecPlayerToDefender.magnitude;
            IM.Vector3 dirPlayerToDefender = vecPlayerToDefender.normalized;
            angle = IM.Vector3.Angle(m_dirPlayerToBasket, dirPlayerToDefender);
            if (angle <= GlobalConst.BACK_TO_BACK_FAN_ANGLE && dist <= GlobalConst.BACK_TO_BACK_FAN_RADIUS)
            {
                State targetState = target.m_StateMachine.m_curState.m_eState;
                if( targetState == State.eDefense || targetState == State.eRun || targetState == State.eStand )
                {						
                    m_player.m_toSkillInstance = m_curExecSkill;
                    m_stateMachine.SetState(State.eBackCompete, true);

                    PlayerState_BackBlock state = target.m_StateMachine.GetState(State.eBackBlock) as PlayerState_BackBlock;
                    state.m_competor = m_player;
                    target.m_StateMachine.SetState(state, true);

                    return;
                }
                else if (targetState == State.eStand || targetState == State.eRun || targetState == State.eRush )
                {
                    Logger.Log("Defender state: " + targetState);
                    bKnocked = true;
                }
            }
        }

		if( bKnocked && target != null )
		{
			target.m_StateMachine.SetState(State.eKnocked, true);
		}

		PlayerMovement.MoveAttribute attr = m_player.mMovements[(int)PlayerMovement.Type.eBackToBackRun].mAttr;
		m_player.MoveTowards(m_dirPlayerToBasket, IM.Number.zero, fDeltaTime, m_dirPlayerToBasket * attr.m_initSpeed);
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_player.m_stamina.m_bEnableRecover = true;
	}
}
