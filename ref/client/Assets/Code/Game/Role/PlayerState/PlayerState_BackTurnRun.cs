using UnityEngine;
using fogs.proto.msg;
public class PlayerState_BackTurnRun:  PlayerState
{
	public bool isTurnLeft{get; private set;}
	IM.Vector3 dirToBasket;

	public PlayerState_BackTurnRun (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBackTurnRun;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "backTurnRunLL");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "backTurnRunRL");
		m_mapAnimType.Add(AnimType.B_TYPE_2, "backTurnRunLR");
		m_mapAnimType.Add(AnimType.B_TYPE_3, "backTurnRunRR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
        dirToBasket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget, m_player.position);

		if( !m_player.m_bSimulator )
		{
			IM.Number cross = IM.Vector3.Cross(dirToBasket, m_player.moveDirection).y;
			isTurnLeft = (cross < IM.Number.zero);
			if (m_player.m_eHandWithBall == Player.HandWithBall.eLeft)
				m_animType = isTurnLeft ? AnimType.B_TYPE_0 : AnimType.B_TYPE_1;
			else if (m_player.m_eHandWithBall == Player.HandWithBall.eRight)
				m_animType = isTurnLeft ? AnimType.B_TYPE_2 : AnimType.B_TYPE_3;

			m_curAction = m_mapAnimType[m_animType];
			PlayerAnimAttribute.AnimAttr attr = m_player.m_animAttributes.GetAnimAttrById(Command.BackToBack, m_curAction);
			PlayerAnimAttribute.KeyFrame_RotateToBasketAngle keyFrame = attr.GetKeyFrame("RotateToBasketAngle") as PlayerAnimAttribute.KeyFrame_RotateToBasketAngle;
			if (keyFrame != null)
			{
				IM.Vector3 dirFaceTo = IM.Quaternion.AngleAxis(keyFrame.angle, IM.Vector3.up) * dirToBasket;
                m_player.forward = dirFaceTo;
			}
		}
		
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();

		if( !m_player.m_bSimulator )
			GameMsgSender.SendBackTurnRun(m_player, m_animType);
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		if( !m_player.m_bSimulator )
		{
			if (m_player.m_defenseTarget != null)
			{
				if (m_player.m_AOD.GetStateByPos(m_player.m_defenseTarget.position) != AOD.Zone.eInvalid)
				{
					bool crossed = false;
					if (m_player.m_defenseTarget.moveDirection != IM.Vector3.zero)
					{
                        IM.Number angle = IM.Vector3.FromToAngle(dirToBasket, m_player.m_defenseTarget.moveDirection);
						if (new IM.Number(45) < angle && angle < new IM.Number(135) && isTurnLeft)
							crossed = true;
						else if (new IM.Number(225) < angle && angle < new IM.Number(315) && !isTurnLeft)
							crossed = true;
					}
					if (crossed)
					{
						PlayerState_Crossed state = m_player.m_defenseTarget.m_StateMachine.GetState(PlayerState.State.eCrossed) as PlayerState_Crossed;
						state.left = !isTurnLeft;
						state.m_animType = state.left ? AnimType.N_TYPE_0 : AnimType.N_TYPE_1;
						m_player.m_defenseTarget.m_StateMachine.SetState(state);
						GameMsgSender.SendCrossed(m_player.m_defenseTarget, m_player, state.m_animType);
					}
				}
			}
		}
	}

	protected override void _OnActionDone()
	{
		base._OnActionDone();
		m_stateMachine.SetState(PlayerState.State.eStand);
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_player.m_stamina.m_bEnableRecover = true;
	}

}