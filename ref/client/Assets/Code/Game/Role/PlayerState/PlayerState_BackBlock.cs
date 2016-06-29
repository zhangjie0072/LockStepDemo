using UnityEngine;
using fogs.proto.msg;

public class PlayerState_BackBlock : PlayerState
{
	public Player	m_competor;

	private IM.Vector3 dirAttackerToBasket;
	private bool	isCompeteWin = false;

	public PlayerState_BackBlock(PlayerStateMachine owner, GameMatch match)
		: base(owner, match)
	{
		m_eState = State.eBackBlock;

		m_mapAnimType.Add(AnimType.N_TYPE_0, "clutchBlockL");
		m_mapAnimType.Add(AnimType.N_TYPE_1, "clutchBlockR");

		m_animType = AnimType.N_TYPE_0;
	}

	override public void OnEnter(PlayerState lastState)
	{
        dirAttackerToBasket = GameUtils.HorizonalNormalized(m_basket.m_vShootTarget, m_player.m_defenseTarget.position);

        if (m_player.m_defenseTarget.m_eHandWithBall == Player.HandWithBall.eLeft)
            m_animType = AnimType.N_TYPE_0;
        else if (m_player.m_defenseTarget.m_eHandWithBall == Player.HandWithBall.eRight)
            m_animType = AnimType.N_TYPE_1;

        IM.Number distToAttacker = GameUtils.HorizonalDistance(m_player.position, m_player.m_defenseTarget.position);
        m_player.position = m_player.m_defenseTarget.position + dirAttackerToBasket * distToAttacker;
		m_curAction = m_mapAnimType[m_animType];

		isCompeteWin = (m_match.m_context.m_backToBackWinnerId == m_player.m_roomPosId);
		Debug.Log("BackBlock: " + isCompeteWin + " m_match.m_context.m_backToBackWinnerId: " + m_match.m_context.m_backToBackWinnerId );

        m_player.forward = -dirAttackerToBasket;
		m_player.animMgr.CrossFade(m_curAction, false);
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);

		PlayerMovement.MoveAttribute attr = m_player.mMovements[(int)PlayerMovement.Type.eBackToBackRun].mAttr;
		m_player.MoveTowards(dirAttackerToBasket, IM.Number.zero, fDeltaTime,
            (isCompeteWin ? -IM.Number.one : IM.Number.one) * dirAttackerToBasket * attr.m_initSpeed);

		PlayerState state = m_player.m_defenseTarget.m_StateMachine.m_curState;

		if (state.m_eState != State.eBackCompete)
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
		}
	}
}