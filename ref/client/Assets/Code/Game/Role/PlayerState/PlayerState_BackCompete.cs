using UnityEngine;
using fogs.proto.msg;

public class PlayerState_BackCompete : PlayerState
{
	private static HedgingHelper hedging = new HedgingHelper("BackToBack");
	private	IM.Vector3 m_dirPlayerToBasket;
	private bool isCompeteWin = false;
	private uint curAnimStep = 1;

	public PlayerState_BackCompete(PlayerStateMachine owner, GameMatch match)
		: base(owner, match)
	{
		m_eState = State.eBackCompete;

		m_mapAnimType.Add(AnimType.B_TYPE_0, "clutchAdvanceL");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "clutchAdvanceR");

		m_animType = AnimType.B_TYPE_0;
		m_validStateTransactions.Add(Command.BackToBack);
	}

	bool _Competing(Player attacker, Player defender)
	{
		IM.Number myStrength = new IM.Number((int)attacker.m_finalAttrs["strength"]);
		IM.Number defenderStrength = new IM.Number((int)defender.m_finalAttrs["strength"]);
		IM.Number rate = hedging.Calc(myStrength, defenderStrength);
		IM.Number value = IM.Random.value;
		Debugger.Instance.m_steamer.message = "MyStrength: " + myStrength + " Defender strength: " + defenderStrength
			+ " Random: " + value + " Back to back win rate: " + rate;
		return value < rate;
	}

	override public void OnEnter(PlayerState lastState)
	{
		base.OnEnter(lastState);

        m_dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
		
		if( !m_player.m_bSimulator )
		{
			if( curAnimStep == 1 )
			{
				Player target = m_player.m_defenseTarget;
				if( target != null )
				{
					isCompeteWin = _Competing(m_player, target);
					m_match.m_context.m_backToBackWinnerId = isCompeteWin ? m_player.m_roomPosId : target.m_roomPosId;
				}
				if (m_player.m_eHandWithBall == Player.HandWithBall.eLeft)
					m_animType = AnimType.B_TYPE_0;
				else
					m_animType = AnimType.B_TYPE_1;
			}
			GameMsgSender.SendBackCompete(m_player, m_animType, isCompeteWin);
		}

		isCompeteWin = (m_match.m_context.m_backToBackWinnerId == m_player.m_roomPosId);
		Logger.Log("BackCompete: " + isCompeteWin + " m_match.m_context.m_backToBackWinnerId: " + m_match.m_context.m_backToBackWinnerId );

		m_curAction = m_mapAnimType[m_animType];
		PlayerAnimAttribute.AnimAttr attr = m_player.m_animAttributes.GetAnimAttrById(Command.BackToBack, m_curAction);
		if (attr != null)
		{
			PlayerAnimAttribute.KeyFrame_RotateToBasketAngle keyFrame = attr.GetKeyFrame("RotateToBasketAngle") as PlayerAnimAttribute.KeyFrame_RotateToBasketAngle;
			IM.Vector3 dirFaceTo = IM.Quaternion.AngleAxis(keyFrame.angle, IM.Vector3.up) * m_dirPlayerToBasket;
            m_player.forward = dirFaceTo;
		}
		
		m_player.animMgr.CrossFade(m_curAction, false);
		m_player.m_stamina.m_bEnableRecover = false;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		if( !m_player.m_bSimulator )
		{
			if( m_player.animMgr.GetPlayInfo(m_curAction).normalizedTime > new IM.Number((int)curAnimStep))
			{
				curAnimStep++;
				_OnActionDone();
				return;
			}
				
			if (m_player.m_toSkillInstance == null)
			{
				curAnimStep = 1;
				m_stateMachine.SetState(PlayerState.State.eBackToStand);
				return;
			}

			if ( GameUtils.HorizonalDistance(m_player.position, m_basket.m_vShootTarget) < IM.Number.half)
			{
				m_player.m_StateMachine.SetState(State.eBackToBack, true);
				return;
			}
		}
		PlayerMovement.MoveAttribute attr = m_player.mMovements[(int)PlayerMovement.Type.eBackToBackRun].mAttr;
		m_player.MoveTowards(m_dirPlayerToBasket, IM.Number.zero, fDeltaTime, 
            (isCompeteWin? IM.Number.one : -IM.Number.one) * m_dirPlayerToBasket * attr.m_initSpeed);

	}

	protected override void _OnActionDone ()
	{
		Player target = m_player.m_defenseTarget;
		if( target != null )
		{
			bool bWin = _Competing(m_player, target);
			if( bWin != isCompeteWin )
			{
				isCompeteWin = bWin;
				m_match.m_context.m_backToBackWinnerId = bWin ? m_player.m_roomPosId : target.m_roomPosId;
				m_stateMachine.SetState(this, true);

				PlayerState_BackBlock state = target.m_StateMachine.GetState(State.eBackBlock) as PlayerState_BackBlock;
				state.m_competor = m_player;
				target.m_StateMachine.SetState(state, true);
			}
		}
	}

}