using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class AIStateTransaction
{
	public AIState 	m_from{ get; private set; }
	public AIState 	m_to{ get; private set; }
	public IM.Number 	m_odds{ get; private set; }
	public bool		m_bForce{ get; private set; }

	public AIStateTransaction (AIState fromState, AIState toState, IM.Number odds, bool forceChange)
	{
		m_from 	= fromState;
		m_to 	= toState;
		m_odds	= odds;
		m_bForce = forceChange;
	}
}

public class AISystem
{
	public AIState 		m_curState{ get; private set; }
	public Player 		m_player{ get; private set; }
	public GameMatch	m_curMatch{ get; private set; }

	private bool 		_enable = true;
    private bool m_isPvp = false;
    public bool IsPvp
    {
       get
        {
            return m_isPvp;
        }
        set
        {
            m_isPvp = value;
        }
    }
	public virtual bool	m_enable
	{
		get{ return _enable; }
		set{
			if (_enable != value)
			{
				_enable = value;

				if (!_enable)
				{
					m_player.m_moveHelper.StopMove();
					m_statesCandidates.Clear();
					_SetAIState(m_arStateList[(int)m_initialState], true);
				}
				/*
				else
				{
					InputDispatcher dispatcher = m_player.m_inputDispatcher;
					if (dispatcher != null && dispatcher.m_enable && !dispatcher.inTakeOver && !dispatcher.disableAIOnAction)
						_enable = false;
				}
				*/
                Debug.LogFormat("AISystem.m_enable: {0} {1} {2}", m_player.m_team.m_side, m_player.m_id, value);
			}
		}
	}

	protected AIState[] m_arStateList;

	private AIState.Type m_initialState;
	
	private List<AIStateTransaction> m_statesCandidates = new List<AIStateTransaction>();
	
	private List< KeyValuePair<IM.Number,AIStateTransaction> > mapStateOdds = new List< KeyValuePair<IM.Number,AIStateTransaction> >();
	private IM.Vector3		m_rim;

	public 	bool	m_bNotDefended{ get; private set; }
	private IM.Number	_defendTime = IM.Number.zero;
	private static IM.Number _defendRefreshingTime = IM.Number.half;
	private GameUtils.Timer	m_randInput;

	private uint AIID
	{
		get { return AI != null ? AI.ID : 0u; }
		set { AI = GameSystem.Instance.AIConfig.GetConfig(value); }
	}
	public AIConfig.AI AI { get; private set; }

	public AISystem(GameMatch match, Player player, AIState.Type initialState = AIState.Type.eInit, uint aiID = 0u)
	{
		AIID = aiID;
		m_player = player;
		m_curMatch = match;

        m_rim = m_curMatch.mCurScene.mBasket.m_rim.center;

		m_arStateList = new AIState[(int)AIState.Type.eMax];
		_BuildAIState();

		m_initialState = initialState;
		_SetAIState(GetState(initialState));

		if( m_player.m_moveHelper != null )
			m_player.m_moveHelper.onMoveToTarget += ArriveAtMoveTarget;

		m_randInput = new GameUtils.Timer(IM.Number.two, _RandomInput);
	}

	void _RandomInput()
	{
		//angle sector from 0 to 16
		m_player.m_curInputDir = IM.Random.Range(-1, 16);
	}

	protected virtual void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eInit] 		= new AI_Init(this);
		m_arStateList[(int)AIState.Type.eIdle] 		= new AI_Idle(this);
		m_arStateList[(int)AIState.Type.eDefense]	= new AI_Defense(this);
		m_arStateList[(int)AIState.Type.eTraceBall]	= new AI_TraceBall(this);
		m_arStateList[(int)AIState.Type.eCheckBall]	= new AI_CheckBall(this);
		m_arStateList[(int)AIState.Type.eShoot]		= new AI_Shoot(this);
		m_arStateList[(int)AIState.Type.eFakeShoot]	= new AI_FakeShoot(this);
		m_arStateList[(int)AIState.Type.eCrossOver]	= new AI_CrossOver(this);
		m_arStateList[(int)AIState.Type.eDefenseBack]	= new AI_DefenseBack(this);
		m_arStateList[(int)AIState.Type.ePass]		= new AI_Pass(this);
		m_arStateList[(int)AIState.Type.ePositioning]	= new AI_Positioning(this);
		m_arStateList[(int)AIState.Type.eBlock]		= new AI_Block(this);
		m_arStateList[(int)AIState.Type.eSteal]		= new AI_Steal(this);
		m_arStateList[(int)AIState.Type.eDunk]		= new AI_Dunk(this);
		m_arStateList[(int)AIState.Type.eLayup]		= new AI_Layup(this);
		m_arStateList[(int)AIState.Type.eRebound]	= new AI_Rebound(this);
		m_arStateList[(int)AIState.Type.eRequireBall]	= new AI_RequireBall(this);
		m_arStateList[(int)AIState.Type.eCutIn]		= new AI_CutIn(this);
		m_arStateList[(int)AIState.Type.eBodyThrowCatch]	= new AI_BodyThrowCatch(this);
		m_arStateList[(int)AIState.Type.ePickAndRoll]	= new AI_PickAndRoll(this);
	}

	virtual public void Update(IM.Number fDeltaTime)
	{
		if( !m_enable )
			return;

		if( m_randInput != null )
			m_randInput.Update(fDeltaTime);

		if( m_curState != null )
			m_curState.Update(fDeltaTime);

		_Think();
		_UpdateDefendedState(fDeltaTime);
	}

	public AIState ReplaceState(AIState state)
	{
		AIState oldState = m_arStateList[(int)state.m_eType];
		m_arStateList[(int)state.m_eType] = state;
		return oldState;
	}
	
	public AIState GetState(AIState.Type type)
	{
		if( (int)type > (int)AIState.Type.eMax )
			return null;
		AIState state = m_arStateList[(int)type];
		if( state == null )
			Debug.LogError( "Unsupported state: " + type);
		return state;
	}
	
	void _SetAIState( AIState newState, bool bForceChange )
	{
		if( m_curState == newState && !bForceChange)
			return;
		//if (m_curState != null && newState != null)
		//	Debug.Log(m_player.m_name + ": Change AI state from " + m_curState.m_eType + " to " + newState.m_eType);	
		if( m_curState != null )
			m_curState.OnExit();
		
		//if( m_curState != null )
		//	Debug.Log( string.Format("AIState from state: {0} to state: {1}", m_curState.m_eType, newState.m_eType) );
		
		AIState lastState = m_curState;
		m_curState = m_arStateList[(int)newState.m_eType];
		if( m_curState == null )
		{
			Debug.LogError( string.Format("Can not find state: {0}", newState.m_eType) );
			return;
		}
			
		m_curState.OnEnter(lastState);
		
		return;
	}
	
	void _SetAIState( AIState newState )
	{
		_SetAIState(newState, false);
	}
	
	public void SetTransaction( AIState.Type eNewState )
	{
		AIState newState = GetState(eNewState);
		SetTransaction(newState, new IM.Number(100));
	}
	
	public void SetTransaction( AIState newState )
	{
		SetTransaction(newState, new IM.Number(100));
	}
	
	public void SetTransaction( AIState.Type eNewState, IM.Number odds, bool bForceChange = false )
	{
		AIState newState = GetState(eNewState);
		SetTransaction(newState, odds, bForceChange);
	}
	
	public void SetTransaction( AIState newState, IM.Number odds, bool bForceChange = false )
	{
        if( m_isPvp && ( newState.m_eType == AIState.Type.eShoot
            || newState.m_eType == AIState.Type.eLayup
            || newState.m_eType == AIState.Type.eDunk)
            )
        {
            odds = IM.Number.zero;
        }

		//Debug.Log("AISystem, SetTransaction " + newState.m_eType + " " + odds + " " + bForceChange);
		AIStateTransaction newTransaction = new AIStateTransaction(m_curState, newState, odds, bForceChange);
		AIStateTransaction existTransaction = m_statesCandidates.Find( delegate(AIStateTransaction transaction) {
			return transaction.m_to == newTransaction.m_to;
		}
		);
		
		if(existTransaction != null)
			return;
		
		m_statesCandidates.Add(newTransaction);
	}

	void _UpdateDefendedState(IM.Number fDeltaTime)
	{
		bool bDefended = m_player.IsDefended();
		if( bDefended )
		{
			m_bNotDefended = false;
			_defendTime = IM.Number.zero;
		}
		else
		{
			_defendTime += fDeltaTime;
			_defendTime = IM.Math.Min(_defendTime, new IM.Number(10));
			if( _defendTime > _defendRefreshingTime )
				m_bNotDefended = true;
		}
	}

	virtual public void OnSectorCollided(RoadPathManager.Sector colSector)
	{

	}

	void _Think()
	{
		if( m_statesCandidates.Count == 0 )
        {
			return;
        }
	
		mapStateOdds.Clear();
		
		AIState fromState = m_curState;

		//Debug.Log("AI-Trans(" + m_player.m_id + "):Think---------");
		IM.Number totalOdds = IM.Number.zero;
		foreach( AIStateTransaction transItem in m_statesCandidates )
		{
			//Debug.Log("AI-Trans(" + m_player.m_id + "): from " + transItem.m_from.m_eType + " to " + transItem.m_to.m_eType +
			//	" odds " + transItem.m_odds + " forceChange:" + transItem.m_bForce);
			KeyValuePair<IM.Number,AIStateTransaction> kv = new KeyValuePair<IM.Number, AIStateTransaction>(transItem.m_odds, transItem);
			mapStateOdds.Add(kv);
			totalOdds += transItem.m_odds;
		}
		
		if( IM.Number.Approximately(totalOdds, IM.Number.zero) )
		{
			m_statesCandidates.Clear();
			return;
		}
	
		mapStateOdds.Sort( delegate(KeyValuePair<IM.Number, AIStateTransaction> x, KeyValuePair<IM.Number, AIStateTransaction> y) {
			return x.Key.CompareTo(y.Key);
		});
		IM.Number finalOdds = IM.Random.Range(IM.Number.zero,IM.Number.one);
		IM.Number odds = IM.Number.zero;
		foreach( KeyValuePair<IM.Number,AIStateTransaction> item in mapStateOdds )
		{
			odds += item.Key / totalOdds;
			if( finalOdds < odds )
			{
				//Debug.Log("AI-Trans(" + m_player.m_id + "):Trans to " + item.Value.m_to.m_eType);
				_SetAIState(item.Value.m_to, item.Value.m_bForce);
				break;
			}
		}
		//Debug.Log("AI-Trans(" + m_player.m_id + "):Think*********");

		m_statesCandidates.Clear();
	}

	virtual protected void ArriveAtMoveTarget()
	{
		if (m_curState != null)
			m_curState.ArriveAtMoveTarget();
	}
}