using UnityEngine;
using fogs.proto.msg;

public class PlayerState_Disturb:  PlayerState
{
	public PlayerState_Disturb(PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eDisturb;

		m_mapAnimType[AnimType.N_TYPE_0] = "disturb";
		
		m_animType = AnimType.N_TYPE_0;

		m_validStateTransactions.Add(Command.Steal);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
        m_rotateTo = RotateTo.eDefenseTarget;
        m_rotateType = RotateType.eDirect;
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.CrossFade(m_curAction, false);
	}

    public override void Update(IM.Number fDeltaTime)
    {
        base.Update(fDeltaTime);

        _UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
    }

	protected override void _OnActionDone()
	{
		m_stateMachine.SetState(PlayerState.State.eStand);
	}
}