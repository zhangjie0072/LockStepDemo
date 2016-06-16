using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_Skill : PlayerState
{
	public bool	m_bPersistent = false;
	protected Area 	m_skillArea;

	protected float				m_fSpeedComp;

	public PlayerState_Skill (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		/*
		m_validStateTransactions.Add(Command.Steal);
		m_validStateTransactions.Add(Command.Shoot);
		m_validStateTransactions.Add(Command.Rebound);
		m_validStateTransactions.Add(Command.RequireBall);
		m_validStateTransactions.Add(Command.PickAndRoll);
		m_validStateTransactions.Add(Command.Pass);
		m_validStateTransactions.Add(Command.Layup);
		m_validStateTransactions.Add(Command.Dunk);
		m_validStateTransactions.Add(Command.BackToBack);
		m_validStateTransactions.Add(Command.Block);
		m_validStateTransactions.Add(Command.BodyThrowCatch);
		m_validStateTransactions.Add(Command.CrossOver);
		m_validStateTransactions.Add(Command.CutIn);
		m_validStateTransactions.Add(Command.Defense);
		m_validStateTransactions.Add(Command.JockeyForPosition);
		m_validStateTransactions.Add(Command.MoveStep);
		*/
	}
		
	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		if( m_curExecSkill == null )
		{
			Logger.LogError("SkillState: no reference skill, check config.");
			return;
		}
		m_curAction = _ParseAction(m_curExecSkill.curAction.action_id, m_curExecSkill.matchedKeyIdx);

		//Logger.Log("current execute skill: " + m_curExecSkill.skill.id + " action id: " + m_curExecSkill.curAction.action_id + " type: " + m_curExecSkill.skill.action_type);
		//double animLength = (double)m_stateMachine.m_Animation[m_curAction].length;
		//m_fSpeedComp = (float)(animLength / (animLength - m_curExecSkill.networkTimeComp));
		//m_fSpeedComp = Mathf.Clamp(m_fSpeedComp, 0.0f, 2.0f);
		//m_stateMachine.m_Animation[m_curAction].speed = m_fSpeedComp;
		//Logger.Log("Complement time is: " + m_curExecSkill.networkTimeComp + " anim speed: " + m_fSpeedComp);
		m_player.m_skillSystem.ResetSkillEffects(m_curExecSkill, m_curAction);
		if (!string.IsNullOrEmpty(m_curExecSkill.curAction.camera_animation) && 
			(m_player == m_match.m_mainRole || m_player.m_team == m_match.m_mainRole.m_team) 
            && !MainPlayer.Instance.inPvpJoining)
        {
			UCamCtrl_SkillAction.Play(m_player, m_curExecSkill.curAction.camera_animation);
        }

		if( m_eState != State.ePrepareToShoot )
			m_player.mStatistics.AddSkillUsage(m_curExecSkill.skill.id);
	}

	virtual protected string _OnIntepretAction(string lHandActionId, string rHandActionId)
	{
		string strAction = lHandActionId;
		if( m_player.m_eHandWithBall == Player.HandWithBall.eRight )
			strAction = rHandActionId;
		return strAction;
	}

	protected string _ParseAction(string actionConfig, int idx)
	{
		string[] strActions = actionConfig.Split('/');
		string lHandActionId = "" , rHandActionId = "";

		string resultAction = "";
		if( strActions.Length > 1 )
			resultAction = strActions[idx];
		else if( strActions.Length == 1 )
			resultAction = actionConfig;
		else
			Logger.LogError("Invalid action input.");

		string[] hands = resultAction.Split('&');
		if( hands.Length > 1 )
		{
			foreach( string strHand in hands )
			{
				if( strHand.StartsWith("L:") )
					lHandActionId = strHand.Substring(2);
				else if( strHand.StartsWith("R:") )
					rHandActionId = strHand.Substring(2);
			}
			return _OnIntepretAction(lHandActionId, rHandActionId);			
		}
		else
			return resultAction;
	}

	override protected void _OnActionDone()
	{
		base._OnActionDone();
		m_player.m_skillSystem.ClearEffects();

		if (m_player.m_moveType == MoveType.eMT_Stand)
			m_stateMachine.SetState(PlayerState.State.eStand);
		else if (m_player.m_moveType == MoveType.eMT_Run && !m_player.m_bMovedWithBall )
			m_stateMachine.SetState(PlayerState.State.eRun);
		else if (m_player.m_moveType == MoveType.eMT_Rush  && !m_player.m_bMovedWithBall )
			m_stateMachine.SetState(PlayerState.State.eRush);
		else
			m_stateMachine.SetState(PlayerState.State.eStand);
	}

	public override void Update (IM.Number fDeltaTime)
	{
		base.Update (fDeltaTime);
		m_player.m_skillSystem.Update(fDeltaTime);

		_UpdatePassiveStateTransaction(m_player.m_toSkillInstance);
	}

	public override void OnExit ()
	{
		base.OnExit ();
		m_player.m_skillSystem.ClearEffects();
	}

	protected bool _CheckOpenShoot()
	{
		if( m_player.m_defenseTarget == null )
			return true;
		
		foreach(Player oppo in m_player.m_defenseTarget.m_team)
		{
			IM.Number fDistance = GameUtils.HorizonalDistance( oppo.position, m_player.position );
			if( fDistance < GlobalConst.OPEN_SHOOT_CYCLE_RADIUS) 
				return false;
			IM.Vector3 dirPlayerToBasket = GameUtils.HorizonalNormalized(m_match.mCurScene.mBasket.m_vShootTarget,m_player.position );
			IM.Vector3 dirPlayerToOppo = GameUtils.HorizonalNormalized( oppo.position, m_player.position );
			
			if( IM.Vector3.Angle(dirPlayerToBasket, dirPlayerToOppo) > GlobalConst.OPEN_SHOOT_FAN_ANGLE) 
				continue;
			if( fDistance < GlobalConst.OPEN_SHOOT_FAN_RADIUS )
				return false;
		}
		return true;
	}

}
