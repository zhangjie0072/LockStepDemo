using UnityEngine;
using fogs.proto.msg;

public class PlayerState_PractiseLayupDunk_Stand : PlayerState
{
	bool m_encounterOffense = false;

	public PlayerState_PractiseLayupDunk_Stand (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.eStand;

		m_validStateTransactions.Add(Command.Block);
	}

	public override void OnEnter(PlayerState lastState)
	{
		m_encounterOffense = IsEncounterOffense();
		m_curAction = DetermineAction();
		m_player.animMgr.CrossFade(m_curAction, false);

		m_player.m_bOnGround = true;
	}
	
	protected virtual string DetermineAction()
	{
		string curAction = m_encounterOffense ?  "defense" : "stand";
		return curAction;
	}
	
	override public void Update (IM.Number fDeltaTime)
	{
		if( IM.Number.Approximately(m_player.position.y, IM.Number.zero) )
			m_player.position = new IM.Vector3(m_player.position.x, IM.Number.zero, m_player.position.z);

		if( !IM.Number.Approximately( m_player.moveDirection.magnitude, IM.Number.zero) && m_player.m_toSkillInstance == null )
			m_stateMachine.SetState(State.eRun);

		m_player.FaceTo( m_player.m_defenseTarget.position );

		if(m_encounterOffense != IsEncounterOffense())
			m_stateMachine.SetState(PlayerState.State.eStand, true);

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}

	bool IsEncounterOffense()
	{
		IM.Number dist = GameUtils.HorizonalDistance(m_player.position,m_player.m_defenseTarget.position);
		return dist < IM.Number.one;
	}
}
