public class MatchStateMachine_PVP : MatchStateMachine
{
	public MatchStateMachine_PVP(GameMatch owner)
		: base(owner)
	{
	}

	protected override void CreateStates()
	{
		m_arStateList[(int)MatchState.State.eTipOff] = new MatchStateTipOff(this);
		m_arStateList[(int)MatchState.State.eOpening] = new MatchStateOpening(this);
		m_arStateList[(int)MatchState.State.eBegin] = new MatchStateBegin(this);
        m_arStateList[(int)MatchState.State.ePlaying] = new MatchStatePlaying_PVP(this);
        m_arStateList[(int)MatchState.State.eGoal] = new MatchStateGoal(this);
		m_arStateList[(int)MatchState.State.eOver] = new MatchStateOver_PVP(this);
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
}