using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState_PickAndRoll : PlayerState_Skill
{
	public bool	m_bEnablePickAndRoll = false;
    public IM.Number m_fInfluRadius = IM.Number.two;

	public bool	m_bOnCollide = false;
	private GameUtils.Timer prNoMove;

	private bool m_bFrozen = false;

	public PlayerState_PickAndRoll (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_eState = State.ePickAndRoll;
		m_bPersistent = true;

		m_validStateTransactions.Add(Command.PickAndRoll);

		m_mapAnimType[AnimType.N_TYPE_0] = "pickandrollR";
		m_mapAnimType[AnimType.N_TYPE_1] = "pickandrollL";

		prNoMove = new GameUtils.Timer(IM.Number.two, OnEnableMovement, 0);
	}

	void OnEnableMovement()
	{
		m_bFrozen = false;
	}

	override public void OnEnter ( PlayerState lastState )
	{
		base.OnEnter(lastState);
		m_player.animMgr.CrossFade(m_curAction, false);
		m_player.model.SetMainColor(Color.red, false);
		m_player.m_stamina.m_bEnableRecover = false;

		m_player.model.EnableGrey(false);
		m_player.m_moveType = MoveType.eMT_PickAndRoll;

		SkillSpec skillSpec = m_player.GetSkillSpecialAttribute(SkillSpecParam.ePickAndRoll_range);
        m_fInfluRadius = skillSpec.value;

		m_bEnablePickAndRoll = true;
		m_bOnCollide = false;

		prNoMove.stop = false;
		m_bFrozen = true;
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( prNoMove != null )
			prNoMove.Update(fDeltaTime);

		if( m_bFrozen)
			return;

		if( m_player.m_team.m_role != GameMatch.MatchRole.eOffense )
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}

		if( m_bOnCollide )
		{
			if( !m_player.animMgr.IsPlaying(m_curAction) )
			{
				m_stateMachine.SetState(PlayerState.State.eStand);
				return;
			}
		}
		else if( m_player.m_toSkillInstance == null)
		{
			m_stateMachine.SetState(PlayerState.State.eStand);
			return;
		}
	}

	public void OnCollided(Player blockedPlayer)
	{
		if( m_bOnCollide )
			return;
		m_player.m_stamina.ConsumeStamina(new IM.Number((int)m_curExecSkill.skill.levels[m_curExecSkill.level].stama ));

		m_player.model.RestoreMaterial();

		IM.Vector3 player2Blocked = GameUtils.HorizonalNormalized(blockedPlayer.position, m_player.position);
        IM.Number up = IM.Vector3.Cross(m_player.forward, player2Blocked).y;
		if( up > IM.Number.zero )
			m_animType = AnimType.N_TYPE_0;
		else
			m_animType = AnimType.N_TYPE_1;

		m_bOnCollide = true;

		m_curAction = m_mapAnimType[m_animType];

		m_player.mStatistics.SkillUsageSuccess(m_curExecSkill.skill.id, true);

		m_player.animMgr.CrossFade(m_curAction, false);
	}

	public override void OnExit ()
	{
		base.OnExit ();

		m_player.model.RestoreMaterial();
		m_player.model.EnableGrey(m_player.m_team != m_match.mainRole.m_team );
		m_player.m_stamina.m_bEnableRecover = true;
	}
}

public class PlayerState_BePickedAndRolled : PlayerState
{
	private bool	m_bOnCollide = false;
	static HedgingHelper pickAndRollHedging = new HedgingHelper("PickAndRoll");
	
	public PlayerState_BePickedAndRolled (PlayerStateMachine owner, GameMatch match):base(owner,match)
	{
		m_mapAnimType[AnimType.N_TYPE_0] = "bepickandrollB";
		m_mapAnimType[AnimType.N_TYPE_1] = "bepickandrollL";
		m_mapAnimType[AnimType.N_TYPE_2] = "bepickandrollR";
		m_mapAnimType[AnimType.N_TYPE_3] = "fall";
		m_mapAnimType[AnimType.N_TYPE_4] = "fallingBack";

		m_eState = State.eBePickAndRoll;
	}

	public bool IsFallGround()
	{
		return m_animType == AnimType.N_TYPE_3 || m_animType == AnimType.N_TYPE_4;
	}
	
	override public void OnEnter ( PlayerState lastState )
	{
		m_bOnCollide = false;
	}
	
	public void OnCollided(Player blockPlayer)
	{
		if( m_bOnCollide )
			return;

		if (blockPlayer.m_StateMachine.m_curState.m_eState != State.ePickAndRoll)
			return;
		(blockPlayer.m_StateMachine.m_curState as PlayerState_PickAndRoll).m_bEnablePickAndRoll = false; 

		uint strengthAddr = 0; 
		m_player.m_skillSystem.HegdingToValue("addn_strength", ref strengthAddr);
		uint myStrength = m_player.m_finalAttrs["strength"] + strengthAddr;

		blockPlayer.m_skillSystem.HegdingToValue("addn_strength", ref strengthAddr);
		uint playerStrength = blockPlayer.m_finalAttrs["strength"] + strengthAddr;
		IM.Number rate = pickAndRollHedging.Calc(new IM.Number((int)myStrength), new IM.Number((int)playerStrength));
		IM.Number value = IM.Random.value;
		bool bFallDown = value > rate;
		
		IM.Vector3 player2Blocked = GameUtils.HorizonalNormalized(blockPlayer.position, m_player.position);
		if( IM.Vector3.Angle(player2Blocked,m_player.forward) > new IM.Number(90) )
		{
			m_animType = bFallDown ? AnimType.N_TYPE_4 : AnimType.N_TYPE_0;
		}
		else
		{
			if( bFallDown )
				m_animType = AnimType.N_TYPE_3;
			else
			{
				m_player.FaceTo(blockPlayer.position);
				IM.Number up = IM.Vector3.Cross(m_player.forward, player2Blocked).y;
				if( up > IM.Number.zero )
					m_animType = AnimType.N_TYPE_2;
				else
					m_animType = AnimType.N_TYPE_1;
			}
		}
		m_bOnCollide = true;
		
		m_curAction = m_mapAnimType[m_animType];
		m_player.animMgr.Play(m_curAction, true).rootMotion.Reset();
	}

	override public void Update (IM.Number fDeltaTime)
	{
		if( m_bOnCollide )
		{
			if( !m_player.animMgr.IsPlaying(m_curAction) )
				m_stateMachine.SetState(PlayerState.State.eStand);
		}
	}
}