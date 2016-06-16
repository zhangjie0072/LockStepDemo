using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Crossed:  PlayerState
{
	public bool left;

	public PlayerState_Crossed(PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eCrossed;

		m_mapAnimType[AnimType.N_TYPE_0] = "spasticityR";
		m_mapAnimType[AnimType.N_TYPE_1] = "spasticityL";
		
		m_animType = AnimType.N_TYPE_0;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.CrossFade(m_curAction, false);
	}

	protected override void _OnActionDone()
	{
		m_stateMachine.SetState(PlayerState.State.eStand);
	}
}