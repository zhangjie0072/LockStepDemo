
public class AISystem_Assist : AISystem
{
	private bool _enable = true;
	public override bool m_enable
	{
		get { return _enable; }
		set
		{
			base.m_enable = value;
			_enable = value;
		}
	}
	public AI_Assist_Init stateInit { get { return GetState(AIState.Type.eAssistInit) as AI_Assist_Init; } }
	Command _curCommand;
	public Command curCommand
	{
		get { return _curCommand; }
		set
		{
			_curCommand = value;
			if (value == Command.TraceBall)
				Enable(AIState.Type.eAssistTraceBall);
			else if (value == Command.Defense)
				Enable(AIState.Type.eAssistDefense);
		}
	}

	public AISystem_Assist(GameMatch match, Player player)
		: base(match, player, AIState.Type.eAssistInit)
	{
	}

	public void Enable(AIState.Type state)
	{
		stateInit.nextState = state;
		m_player.m_aiAssist = this;
		m_enable = true;
	}

	public void Disable()
	{
		//此处暂时不将player身上的aiMgr置为null，下一帧再设置。确保状态能够回到init
		m_player.m_aiAssist = null;
		m_enable = false;
	}

	public override void Update(IM.Number fDeltaTime)
	{
		base.Update(fDeltaTime);
		curCommand = Command.None;
	}

	protected override void _BuildAIState()
	{
		m_arStateList[(int)AIState.Type.eAssistInit] = new AI_Assist_Init(this);
		//m_arStateList[(int)AIState.Type.eAssistSteal] = new AI_Assist_Steal(this);
		m_arStateList[(int)AIState.Type.eAssistDefense] = new AI_Assist_Defense(this);
		m_arStateList[(int)AIState.Type.eAssistTraceBall] = new AI_Assist_TraceBall(this);
	}
}
