using UnityEngine;
using fogs.proto.msg;

public class AIState
{
	public enum Type
	{
		eDefense = 1,
		eOffense,
		eCheckBall,
		eTraceBall,
		eBodyThrowCatch,
		eBlock,
		eCrossOver,
		eDefenseCross,
		eDefenseBack,
		eDunk,
		eIdle,
		eLayup,
		ePass,
		ePositioning,
		eRebound,
		eRequireBall,
		eShoot,
		eSteal,
		eInit,
		eCutIn,
		eFakeShoot,
		ePickAndRoll,

		ePractiseRule_Idle,
		ePractiseRule_Shoot,
		ePractiseRule_TraceBall,
		ePractiseRule_CheckBall,
		ePractisePass_Idle,
		ePractisePass_Positioning,
		ePractisePass_RequireBall,
		ePractisePass_TraceBall,
		ePractisePass_Pass,
		ePractiseShoot_Idle,
		ePractiseShoot_TraceBall,
		ePractiseShoot_Pass,
		ePractiseLayupDunk_Defense,
		ePractiseLayupDunk_Block,
		ePractiseLayupDunk_Positioning,
		ePractiseBlock_Idle,
		ePractiseBlock_Positioning,
		ePractiseBlock_Layup,
		ePractiseRebound_Idle,
		ePractiseRebound_Positioning,
		ePractiseRebound_Shoot,
		ePractiseBaseGuide_Idle,
		ePractiseBaseGuide_RequireBall,
		ePractiseBaseGuide_Pass,
		ePractiseBaseGuide_TraceBall,
		ePractiseBaseGuide_Positioning,
		ePractiseBaseGuide_Layup,
		ePractiseGuide_Idle,
		ePractiseGuide_Face2Mate,
		ePractiseGuide_TraceBall,
		ePractiseGuide_Positioning,
		ePractiseGuide_Pass,
		ePractiseGuide_Layup,
		ePractiseGuide_Shoot,
		ePractiseGuide_Defense,
		eReboundStorm_Idle,
		eReboundStorm_ShooterIdle,
		eReboundStorm_Shoot,
		eReboundStorm_Positioning,
		eReboundStorm_Rebound,
		eBlockStorm_Idle,
		eBlockStorm_Positioning,
		eBlockStorm_Layup,
		eBlockStorm_Dunk,
		eMassBall_Init,
		eMassBall_TraceBall,
		eMassBall_Positioning,
		eMassBall_Shoot,
		eGrabZone_Init,
		eGrabZone_TraceBall,
		eGrabZone_Positioning,
		eGrabZone_AvoidDefender,
		eGrabZone_Shoot,
		eGrabPoint_Init,
		eGrabPoint_TracePoint,
		eGrabPoint_Positioning,
		eGrabPoint_Shoot,
		eBullFight_Positioning,

		//Be used to assist main role
		eAssistInit,
		eAssistSteal,
		eAssistDefense,
		eAssistTraceBall,

		eDebug_Init,
		eDebug_Shoot,
		eDebug_Positioning2Basket,

		ePVP_Idle,
		ePVP_Positioning,
		ePVP_Layup,
		ePVP_TraceBall,
		ePVP_Shoot,
		ePVP_Dunk,

		eMax
	}
	
	protected AISystem	m_system;
	protected Player 	m_player;

	public IM.Vector3 	m_moveTarget
	{
		get{ return m_player.m_moveHelper.targetPosition; }
		set{ m_player.m_moveHelper.MoveTo(value); }
	}
	protected bool m_bForceNotRush = false;

	//protected bool		m_bArrived = false;

	protected GameMatch	m_match;
	protected UBasketball m_ball
	{
		get
		{ 
			if( m_player.m_ball != null ) 
				return m_player.m_ball;
			else 
				return m_match.mCurScene.mBall; 
		}
	}
	protected UBasket	m_basket;

	private GameUtils.Timer m_timerTick;

	public Type 	m_eType{get; protected set;}
	public AIState (AISystem owner)
	{
		m_system 	= owner;
		m_player 	= owner.m_player;
		m_match 	= owner.m_curMatch;
		m_basket	= m_match.mCurScene.mBasket;
		m_timerTick = new GameUtils.Timer(m_system.AI.delay, OnTick);

		if( m_player == null )
			Debug.LogError("AIState: player can not be null.");

	}
	
	virtual public void OnEnter ( AIState lastState )
	{
	}
	
	virtual public void Update (IM.Number fDeltaTime)
	{
		if (m_match.level != GameMatch.Level.None)
		{
			bool bRush = false;
			if( m_match.m_stateMachine.m_curState.m_eState == MatchState.State.eBegin )
				bRush = false;

			if (m_bForceNotRush)
				bRush = false;
			else
			{
				if (m_player.m_stamina.m_curRatio >= m_match.gameMode.rushStamina)
					bRush = true;
				else if (m_player.m_stamina.m_curRatio < new IM.Number(0,010))
					bRush = false;
			}
			m_player.m_moveType = bRush ? MoveType.eMT_Rush : MoveType.eMT_Run;
		}

		m_timerTick.Update(fDeltaTime);
	}

	virtual protected void OnTick()
	{
	}
	
	virtual public void OnExit ()
	{
	}

	virtual public void OnPlayerCollided (Player colPlayer)
	{
	}

	virtual public void ArriveAtMoveTarget()
	{
		
	}

	protected bool CanArriveBeforePlayer(IM.Vector3 target)
	{
		return AIUtils.CanArriveBefore(m_player, m_match.mainRole, target);
	}
}