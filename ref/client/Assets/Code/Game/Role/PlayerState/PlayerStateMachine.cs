using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateMachine 
{
	public class Listener
	{
		public virtual void OnBeginDunk(Player player, IM.Number fTimeFromAirToDunk){}
		public virtual void OnDunkLeaveGround(Player player, IM.Number fTimeFromAirToDunk){}
		public virtual void OnDunk(Player player){}
		public virtual void OnDunkDone(Player player){}
	}
	public List<Listener>	m_listeners{ get; protected set; }

	public System.Action<PlayerState, PlayerState> onStateChanged;
	
	public PlayerState m_curState{ get; private set; }
	private AISystem_Assist _assistAI = null;
	public AISystem_Assist assistAI
	{
		get
		{
			if (_assistAI == null)
				_assistAI = new AISystem_Assist(GameSystem.Instance.mClient.mCurMatch, m_owner);
			return _assistAI;
		}
	}
	
	public Player 					m_owner{ get; private set; }
	PlayerState[] 					m_arStateList;

	public PseudoRandom attackRandom = new PseudoRandom();
	
	public PlayerStateMachine( Player player ) 
	{
		m_owner = player;
		m_listeners = new List<Listener>();
		
		//for movement
		_SetAnimMovementSpeed("defenseForward", PlayerMovement.Type.eDefense);
		_SetAnimMovementSpeed("defenseBackward",PlayerMovement.Type.eDefense);
		_SetAnimMovementSpeed("defenseLeft",PlayerMovement.Type.eDefense);
		_SetAnimMovementSpeed("defenseRight",PlayerMovement.Type.eDefense);

		_SetAnimMovementSpeed("backward",PlayerMovement.Type.eRunWithoutBall);
		_SetAnimMovementSpeed("run",PlayerMovement.Type.eRunWithoutBall);
		_SetAnimMovementSpeed("runWithBallL",PlayerMovement.Type.eRunWithBall);
		_SetAnimMovementSpeed("runWithBallR",PlayerMovement.Type.eRunWithBall);
		_SetAnimMovementSpeed("runSwitchHandRL",PlayerMovement.Type.eRunWithBall);
		_SetAnimMovementSpeed("runSwitchHandLR",PlayerMovement.Type.eRunWithBall);

        //TODO 这两个动作不存在
		//_SetAnimMovementSpeed("crossOverLeft",PlayerMovement.Type.eRunWithBall);
		//_SetAnimMovementSpeed("crossOverRight",PlayerMovement.Type.eRunWithBall);

		_SetAnimMovementSpeed("rush",PlayerMovement.Type.eRushWithoutBall);
		_SetAnimMovementSpeed("rushWithBallL",PlayerMovement.Type.eRushWithBall);
		_SetAnimMovementSpeed("rushWithBallR",PlayerMovement.Type.eRushWithBall);

		_CreateStates();
	}
	
	void _SetAnimMovementSpeed( string strClip, PlayerMovement.Type type )
	{
        m_owner.animMgr.SetSpeed(strClip, m_owner.mMovements[(int)type].mAttr.m_playSpeed);
	}
	
	void _CreateStates()
	{
		GameMatch	curMatch = GameSystem.Instance.mClient.mCurMatch;
		if( curMatch == null )
		{
			Logger.LogError("Can not create states without game match.");
			return;
		}

		m_arStateList = new PlayerState[(int)PlayerState.State.eMax];
		m_arStateList[(int)PlayerState.State.eNone] 	= new PlayerState_None(this,curMatch);
		m_arStateList[(int)PlayerState.State.eHold] 	= new PlayerState_Hold(this,curMatch);
		m_arStateList[(int)PlayerState.State.eStand] 	= new PlayerState_Stand(this,curMatch);
		m_arStateList[(int)PlayerState.State.eRun] 		= new PlayerState_Run(this,curMatch);
		m_arStateList[(int)PlayerState.State.eRush] 	= new PlayerState_Rush(this,curMatch);
		//m_arStateList[(int)PlayerState.State.eRushTurning] 	= new PlayerState_RushTurning(this,curMatch);
		m_arStateList[(int)PlayerState.State.eResultPose] 	= new PlayerState_ResultPose(this,curMatch);
		m_arStateList[(int)PlayerState.State.eGoalPose] 	= new PlayerState_GoalPose(this,curMatch);

		m_arStateList[(int)PlayerState.State.eFallGround] 		= new PlayerState_FallGround(this,curMatch);

		m_arStateList[(int)PlayerState.State.ePrepareToShoot] 	= new PlayerState_PrepareToShoot(this,curMatch);
		m_arStateList[(int)PlayerState.State.eShoot] 	= new PlayerState_Shoot(this,curMatch);
		
		m_arStateList[(int)PlayerState.State.ePickup] 	= new PlayerState_Pickup(this,curMatch);
		m_arStateList[(int)PlayerState.State.eSteal] 	= new PlayerState_Steal(this,curMatch);
		m_arStateList[(int)PlayerState.State.eStolen] 	= new PlayerState_Stolen(this,curMatch);
		m_arStateList[(int)PlayerState.State.eCrossed] 	= new PlayerState_Crossed(this,curMatch);
		m_arStateList[(int)PlayerState.State.eDisturb] 	= new PlayerState_Disturb(this,curMatch);
		
		m_arStateList[(int)PlayerState.State.eDefense] 	= new PlayerState_Defense(this,curMatch);
		m_arStateList[(int)PlayerState.State.ePickAndRoll] 	= new PlayerState_PickAndRoll(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBePickAndRoll] 	= new PlayerState_BePickedAndRolled(this,curMatch);
		
		m_arStateList[(int)PlayerState.State.eCrossOver]= new PlayerState_CrossOver(this,curMatch);
		m_arStateList[(int)PlayerState.State.eCutIn]= new PlayerState_CutIn(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackToBack]= new PlayerState_BackToBack(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackToBackForward]= new PlayerState_BackToBackForward(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackCompete]= new PlayerState_BackCompete(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackToStand]= new PlayerState_BackToStand(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackTurnRun]= new PlayerState_BackTurnRun(this,curMatch);
		m_arStateList[(int)PlayerState.State.eBackBlock]= new PlayerState_BackBlock(this,curMatch);
		m_arStateList[(int)PlayerState.State.eDefenseCross]= new PlayerState_DefenseCross(this,curMatch);
		
		m_arStateList[(int)PlayerState.State.eRebound] 	= new PlayerState_Rebound(this,curMatch);
		m_arStateList[(int)PlayerState.State.eDunk] 	= new PlayerState_Dunk(this,curMatch);
		m_arStateList[(int)PlayerState.State.eLayup] 	= new PlayerState_Layup(this,curMatch);
		m_arStateList[(int)PlayerState.State.eLayupFailed] 	= new PlayerState_LayupFailed(this,curMatch);

		m_arStateList[(int)PlayerState.State.eInterception] 	= new PlayerState_Interception(this,curMatch);


		m_arStateList[(int)PlayerState.State.eFallLostBall]	= new PlayerState_FallLostBall(this,curMatch);
		m_arStateList[(int)PlayerState.State.eKnocked] 	= new PlayerState_Knocked(this,curMatch);

		m_arStateList[(int)PlayerState.State.eMoveStep] = new PlayerState_MoveStep(this,curMatch);

		m_arStateList[(int)PlayerState.State.eBlock] 	= new PlayerState_Block(this,curMatch);
		m_arStateList[(int)PlayerState.State.ePass] 	= new PlayerState_Pass(this,curMatch);
		m_arStateList[(int)PlayerState.State.eCatch] 	= new PlayerState_Catch(this,curMatch);
		m_arStateList[(int)PlayerState.State.eRequireBall] 	= new PlayerState_RequireBall(this,curMatch);
		m_arStateList[(int)PlayerState.State.eSwitchBall] 	= new PlayerState_SwitchBall(this,curMatch);

		m_arStateList[(int)PlayerState.State.eBodyThrowCatch] = new PlayerState_BodyThrowCatch(this,curMatch);
	}

	public PlayerState ReplaceState(PlayerState state)
	{
		PlayerState replaced = m_arStateList[(int)state.m_eState];
		m_arStateList[(int)state.m_eState] = state;
		return replaced;
	}
	
	public PlayerState GetState( PlayerState.State state )
	{
		if( (int)state > (int)PlayerState.State.eMax )
			return null;
		return m_arStateList[(int)state];
	}
	
	public void Update( IM.Number fDeltaTime ) 
	{
		if( m_curState == null )
			return;
		m_curState.Update( fDeltaTime );
	}

	public bool SetState( PlayerState newState )
	{
		return SetState(newState, false);
	}

	public bool SetState( PlayerState.State eNewState )
	{
		return SetState(m_arStateList[(int)eNewState], false);
	}

	public bool SetState( PlayerState.State eNewState, bool bForceChange )
	{
		return SetState(m_arStateList[(int)eNewState], bForceChange);
	}

	public bool SetState( PlayerState newState, bool bForceChange )
	{
		if( newState == null )
			return false;

		if( newState.m_eState >= PlayerState.State.eMax )
			return false;
		
		if( m_curState == newState && !bForceChange)
			return true;

		if (newState != null && !newState.PreEnter())
			return true;
		
		if( m_curState != null )
			m_curState.OnExit();
		
		PlayerState lastState = m_curState;

        /* Trace state transmission
        if (m_curState != null)
        {
            Logger.Log(string.Format("Player state trans, Team: {0} Player: {1} From state: {2} To state: {3}",
                m_owner.m_team.m_role, m_owner.m_name, m_curState.m_eState, newState.m_eState));
        }
        //*/
		
		m_curState = newState;
		if( m_curState == null )
		{
			Logger.LogError( string.Format("Can not find state: {1}", newState.m_eState) );
			return false;
		}
		m_curState.OnEnter(lastState);

		if (onStateChanged != null)
			onStateChanged(lastState, m_curState);
		
		return true;
	}
}
