using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MatchStateMachine
{
	public interface Listener
	{
		void OnMatchStateChange(MatchState oldState, MatchState newState);
	}

	public MatchState m_curState{ get; private set; }
	public GameMatch m_owner{ get; private set; }

	public IEnumerator GetEnumerator(){ return m_arStateList.GetEnumerator(); }

	public List<Listener>	m_matchStateListeners{ get; private set; }
	protected MatchState[] m_arStateList;

	public MatchStateMachine (GameMatch owner)
	{
		m_owner = owner;
		m_arStateList = new MatchState[(int)MatchState.State.eMax];
		CreateStates();
		m_matchStateListeners = new List<Listener>();
	}

	protected virtual void CreateStates()
	{
		m_arStateList[(int)MatchState.State.eOpening] = new MatchStateOpening(this);
		m_arStateList[(int)MatchState.State.eTipOff] = new MatchStateTipOff(this);
		m_arStateList[(int)MatchState.State.eBegin] = new MatchStateBegin(this);
		m_arStateList[(int)MatchState.State.ePlaying] = new MatchStatePlaying(this);
		m_arStateList[(int)MatchState.State.eGoal] = new MatchStateGoal(this);
		m_arStateList[(int)MatchState.State.eOver] = new MatchStateOver(this);
		m_arStateList[(int)MatchState.State.eOverTime] = new MatchStateOverTime(this);
		m_arStateList[(int)MatchState.State.eFoul] = new MatchStateFoul(this);
        m_arStateList[(int)MatchState.State.ePlotBegin] = new MatchStatePlot(this, MatchState.State.ePlotBegin);
        m_arStateList[(int)MatchState.State.ePlotEnd] = new MatchStatePlot(this, MatchState.State.ePlotEnd);
        m_arStateList[(int)MatchState.State.eShowRule] = new MatchStateShowRule(this);
		m_arStateList[(int)MatchState.State.eShowSkillGuide] = new MatchStateShowSkillGuide(this);
        m_arStateList[(int)MatchState.State.eSlotMachineUltimate21] = new MatchStateSlotMachineUltimate21(this);
        m_arStateList[(int)MatchState.State.eFreeThrowStart] = new MatchStateFreeThrowStart(this);
        m_arStateList[(int)MatchState.State.ePlayerCloseUp] = new MatchStatePlayerCloseUp(this);
	}
	
	public MatchState GetState( MatchState.State state )
	{
		if( (int)state > (int)MatchState.State.eMax )
			return null;
		return m_arStateList[(int)state];
	}
	
	public bool SetState( MatchState.State eNewState )
	{
		return SetState(eNewState, false);
	}
	
	public bool SetState( MatchState.State eNewState, bool bForceChange )
	{
		if( m_curState != null && m_curState.m_eState == eNewState && !bForceChange)
			return true;
		
		if( m_curState != null )
			m_curState.OnExit();

		if (m_curState != null)
			Debug.Log(string.Format("MatchState from state: {0} to state: {1}", m_curState.m_eState, eNewState));
		
		MatchState lastState = m_curState;
		m_curState = m_arStateList[(int)eNewState];
		if( m_curState == null )
		{
			Debug.LogError( string.Format("Can not find state: {0}", eNewState) );
			return false;
		}

		m_matchStateListeners.ForEach( delegate(MatchStateMachine.Listener lsn) {
			lsn.OnMatchStateChange(lastState, m_curState);
		});

		m_curState.OnEnter(lastState);
		
		return true;
	}
	
    //逻辑层
	public void GameUpdate( IM.Number fDeltaTime ) 
	{
		if( m_curState == null )
			return;
		m_curState.GameUpdate( fDeltaTime );
	}

    //显示层
    public void ViewUpdate(float deltaTime)
    {
		if( m_curState == null )
			return;
		m_curState.ViewUpdate(deltaTime);
    }
}
