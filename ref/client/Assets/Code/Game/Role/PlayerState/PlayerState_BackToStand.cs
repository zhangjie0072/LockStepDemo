using UnityEngine;
using fogs.proto.msg;
public class PlayerState_BackToStand:  PlayerState
{
	public PlayerState_BackToStand (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eBackToStand;

		m_validStateTransactions.Remove(Command.Shoot);
		m_validStateTransactions.Remove(Command.Layup);
		m_validStateTransactions.Remove(Command.Dunk);
		m_validStateTransactions.Remove(Command.CrossOver);
		m_validStateTransactions.Remove(Command.Pass);

		m_mapAnimType.Add(AnimType.B_TYPE_0, "backToStandL");
		m_mapAnimType.Add(AnimType.B_TYPE_1, "backToStandR");
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
        if (m_player.m_eHandWithBall == Player.HandWithBall.eLeft)
            m_animType = AnimType.B_TYPE_0;
        else if (m_player.m_eHandWithBall == Player.HandWithBall.eRight)
            m_animType = AnimType.B_TYPE_1;

		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.CrossFade(m_curAction, true).rootMotion.Reset();
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
        if (m_player.moveDirection != IM.Vector3.zero)
        {
            IM.Vector3 dirPlayerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.position);
            IM.Number angle = IM.Vector3.Angle(dirPlayerToBasket, m_player.moveDirection);
            if (new IM.Number(45) <= angle && angle <= new IM.Number(135))
                m_stateMachine.SetState(PlayerState.State.eBackTurnRun);
        }
	}

	protected override void _OnActionDone()
	{
		m_stateMachine.SetState(PlayerState.State.eStand);
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_player.m_stamina.m_bEnableRecover = true;
	}
}