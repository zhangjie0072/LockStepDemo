using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public enum InputDirType
{
	eJoyStick,
	eBasket,
	ePlayer
}

public enum InputDirection
{
    Null = 0,
    Forward = 1,
    ForwardR,
    ForwardRight,
    FRight,
    Right,
    BRight,
    BackRight,
    BackR,
    Back,
    BackL,
    BackLeft,
    BLeft,
    Left,
    FLeft,
    ForwardLeft,
    ForwardL,
    None,
    Max,
}

public enum Command
{
	Null				= 0,
	Shoot 				= 1,		//include dunk and layup
	Layup				= 2,
	Dunk				= 3,
	Block 				= 4,
	Rebound 			= 5,
	CrossOver 			= 6,
	Pass 				= 7,
	Steal				= 8,
	CutIn 				= 9,
	BodyThrowCatch 		= 10,
	BackToBack 			= 11,
	JockeyForPosition 	= 12,
	PickAndRoll 		= 13,
	RequireBall 		= 14,
	Defense 			= 15,
	MoveStep			= 16,
	TraceBall			= 17,
	Switch				= 20,
	Rush				= 21,
	Interception		= 22,
    None                = 24,
	Max 				= 25
}

public class InputDispatcher
{
	private Player 	m_player;
    private GameMatch m_match;
    private IM.Vector3 m_worldMoveDir;

	private bool _enabled = true;
	public bool m_enable
	{
		get { return _enabled; }
		set
		{
			_enabled = value;
			if (!_enabled)
			{
				bufferedCommand = Command.None;
			}
		}
	}
	public bool m_enableMove = true;

	Command curCommand;

    //扩展加速（松开加速键后一段时间仍然保持加速状态）
	static IM.Number RUSH_EXTENSION_TIME = new IM.Number(1, 300);
	IM.Number rushExtRemain;
	bool rushExtending = false;

    //命令缓存（命令被当前正在执行的技能阻塞时，一定时间内，若技能结束，自动执行该命令）
    static IM.Number COMMAND_BUFFER_TIME = new IM.Number(0, 450);
	Command bufferedCommand;
	IM.Number bufferedTime;

    //无操作AI接管
	static IM.Number MAX_UNCONTROL_TIME = new IM.Number(0, 500);
	IM.Number uncontrolTime;
	public bool inTakeOver { get; private set; }

    //操作关闭接管AI
	static IM.Number MAX_IGNORE_DIR_TIME = new IM.Number(0, 500);
	IM.Number ignoreDirTime;
	bool _disableAIOnAction;
	public bool disableAIOnAction
	{
		get { return _disableAIOnAction; }
		set
		{
			_disableAIOnAction = value;
			if (value)
				ignoreDirTime = IM.Number.zero;
		}
	}
    //自动盯防托管
	public AutoDefenseTakeOver _autoDefTakeOver;

    InputDirection _prevDir = InputDirection.None;
    Command _prevCmd = Command.None;
    public InputDirection dir = InputDirection.None;
    public Command cmd = Command.None;

	public InputDispatcher(GameMatch match, Player receiver)
	{
        m_match = match;
		m_player = receiver;
        _autoDefTakeOver = new AutoDefenseTakeOver(match);
	}
	
    public void Update(IM.Number deltaTime)
    {
		if (!m_enable)
		{
			m_player.moveDirection = IM.Vector3.zero;
			return;
		}

		if( m_player == null )
			return;

        //刷新扩展加速状态
		if (m_player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRush)
		{
			rushExtRemain = IM.Number.zero;
		}

		if (rushExtRemain <= IM.Number.zero)
		{
			rushExtending = false;
		}
		rushExtRemain -= deltaTime;

		if (bufferedCommand != Command.None)
		{
			bufferedTime -= deltaTime;
			//Logger.Log("Cur buffer time:" + bufferedTime);
			if (bufferedTime < IM.Number.zero)
			{
			//	Logger.Log("Clear buffer command for time out.");
				bufferedCommand = Command.None;
			}
		}

        //松开加速键后，进入扩展加速状态
        if (_prevCmd == Command.Rush && cmd != Command.Rush)
        {
			if (m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eRush)
			{
				rushExtRemain = RUSH_EXTENSION_TIME;
				rushExtending = true;
			}
        }
		
		_UpdateInputDirection(dir, cmd);

        //投篮力度条控制
        if (_prevCmd == Command.Shoot && cmd != Command.Shoot)
        {
            if (m_player.m_InfoVisualizer.m_strengthBar != null &&
                m_player.m_StateMachine.m_curState.m_eState == PlayerState.State.eShoot)
            {
                m_player.shootStrength.Stop();
            }
        }

        //自动盯防接管
        if (m_player.m_team.m_role == GameMatch.MatchRole.eDefense
            && _autoDefTakeOver.InTakeOver
            && m_match.mCurScene.mBall.m_ballState != BallState.eLoseBall)
            //&& !curMatch.m_ruler.m_bToCheckBall)
            CommandToAction(Command.Defense);

        //盯防命令启动自动盯防
        if (cmd == Command.Defense)
        {
            if( !_autoDefTakeOver.InTakeOver )
                _autoDefTakeOver.Enabled = true;
            m_player.m_dir = -1;
        }

        //命令转化为技能
        if (cmd != Command.None)
            CommandToAction(cmd);

        //如果没有命令执行
		if (cmd == Command.None)
		{
            //方向操作取消技能
			PlayerState skillState = m_player.m_StateMachine.m_curState;
			if (skillState.m_lstActionId.Count != 0
			   && skillState is PlayerState_Skill)
			{
				if (dir != InputDirection.Null && dir != InputDirection.None)
					CommandToAction(Command.None);
			}
			else if (bufferedCommand != Command.None)   //执行缓存命令
			{
				//Logger.Log("Cast buffered command:" + bufferedCommand);
				CommandToAction(bufferedCommand);
			}
		}

		if (m_match.m_stateMachine.m_curState != null &&
			m_match.m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
			//AI 接管玩家操作
			if (m_match.EnableTakeOver() && m_player == m_match.m_mainRole)
			{
				bool uncontrol = (dir == InputDirection.None && cmd != Command.None);
				if (uncontrol)
				{
					if (m_match.mCurScene.mBall.m_ballState != BallState.eUseBall_Pass ||
						m_match.mCurScene.mBall.m_catcher != m_player)
						uncontrolTime = uncontrolTime + deltaTime;
				}
				else
					uncontrolTime = IM.Number.zero;
				if (!inTakeOver && uncontrol)
				{
					if (uncontrolTime > MAX_UNCONTROL_TIME)
					{
						inTakeOver = true;
						Logger.Log("InputDispatcher, Take over.");
						if (m_player.m_aiMgr != null && m_player.m_aiMgr.m_enable != Debugger.Instance.m_bEnableAI)
							m_player.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;
					}
				}
				else if (inTakeOver && !uncontrol)
				{
					inTakeOver = false;
					Logger.Log("InputDispatcher, Resume.");
					if (m_player.m_aiMgr != null && m_player.m_aiMgr.m_enable)
						m_player.m_aiMgr.m_enable = false;
				}
				else if (inTakeOver && Debugger.Instance.m_bEnableAI && !m_player.m_aiMgr.m_enable)
				{
					if (m_player.m_aiMgr != null && m_player.m_aiMgr.m_enable != Debugger.Instance.m_bEnableAI)
						m_player.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;
				}
			}
			else
				inTakeOver = false;
		}
		else
		{
			uncontrolTime = IM.Number.zero;
			inTakeOver = false;
		}

		//操作关闭AI
		if (disableAIOnAction)
		{
			if (m_player.m_team.m_role == GameMatch.MatchRole.eDefense)
			{
				if (m_player.m_aiMgr != null)
				{
					ignoreDirTime += deltaTime;
					bool disable = false;
					if (ignoreDirTime > MAX_IGNORE_DIR_TIME)
						disable = (dir != InputDirection.None ||
							curCommand == Command.Defense ||
							curCommand == Command.Steal ||
							curCommand == Command.Block);
					else
						disable = (curCommand == Command.Defense ||
							curCommand == Command.Steal ||
							curCommand == Command.Block);
					if (disable)
					{
						if (m_player.m_aiMgr != null && m_player.m_aiMgr.m_enable)
							m_player.m_aiMgr.m_enable = false;
						disableAIOnAction = false;
					}
					else
					{
						m_player.m_dir = -1;
					}
				}
			}
			else if (!inTakeOver)
			{
				if (m_player.m_aiMgr != null && m_player.m_aiMgr.m_enable)
					m_player.m_aiMgr.m_enable = false;
			}
		}

        if (dir != InputDirection.None)
            _autoDefTakeOver.SetControlled();
        //if (m_player.m_dir == -1)
        //    m_player.m_moveType = MoveType.eMT_Stand;

        //自动盯防接管
        if (m_player.m_team.m_role == GameMatch.MatchRole.eDefense)
            _autoDefTakeOver.Update(deltaTime);

        _prevDir = dir;
        _prevCmd = cmd;
	}

	public void TransmitUncontrolInfo(InputDispatcher dispatcher)
	{
		inTakeOver = dispatcher.inTakeOver;
		uncontrolTime = dispatcher.uncontrolTime;
		Logger.Log(string.Format("TransmitUncontrolInfo from {0} to {1}, inTakeOver:{2}, uncontrolTime:{3}",
			dispatcher.m_player.m_name, m_player.m_name, inTakeOver, uncontrolTime));
	}

	public EDirection GetMoveDirection(InputDirType type)
	{
		if (!m_enable)
			return EDirection.eNone;

		if( m_worldMoveDir == IM.Vector3.zero )
			return EDirection.eNone;

		IM.Vector3 vHForward = IM.Vector3.zero;
		if( type == InputDirType.ePlayer )
			vHForward = m_player.forward;
		else if( type == InputDirType.eJoyStick )
			vHForward = IM.Vector3.forward;
		else if( type == InputDirType.eBasket )
		{
			UBasket basket = m_match.mCurScene.mBasket;
			if( basket == null )
			{
				vHForward = IM.Vector3.forward;
			}
			else
			{
                IM.Vector3 dirToBasket = basket.m_vShootTarget - m_player.position;
				dirToBasket.y = IM.Number.zero;
				vHForward = dirToBasket.normalized;
			}
		}
		vHForward.y = IM.Number.zero;

		IM.Vector3 dir = m_worldMoveDir;
		if( IM.Vector3.Angle(vHForward , dir) > new IM.Number(135))
			return EDirection.eBack;
		else if( IM.Vector3.Angle(vHForward , dir) < new IM.Number(45))
			return EDirection.eForward;
		
		IM.Number fCW = IM.Vector3.Cross(vHForward, dir).y;
		if( IM.Math.Abs(fCW) < new IM.Number(0,001))
			return EDirection.eNone;
        if (fCW < IM.Number.zero)
            return EDirection.eLeft;
        else if (fCW > IM.Number.zero)
            return EDirection.eRight;

        return EDirection.eNone;
	}
	 
	public void CommandToAction(Command cmd)
	{
		if (!m_enable)
			return;
        //特殊处理：自动盯防生效时，加速键显示，但无效
        if (m_player.m_team.m_role == GameMatch.MatchRole.eDefense
            && _autoDefTakeOver.InTakeOver
            && cmd == Command.Rush)
            return;
		curCommand = cmd;
		//if( !(GameSystem.Instance.mClient.mCurMatch is GameMatch_PVP) )
		{
			if (m_player.m_StateMachine.assistAI != null)
				m_player.m_StateMachine.assistAI.curCommand = cmd;
		}
		//buffer command
		PlayerState curState = m_player.m_StateMachine.m_curState;
		if (curState is PlayerState_Skill)
		{
			Command curCmd = Command.None;
			if (curState.m_curExecSkill != null)
				curCmd = (Command)curState.m_curExecSkill.skill.action_type;
			//Logger.Log("Cur skill type:" + curCmd);
			if (cmd != curCmd)
			{
				if (cmd != bufferedCommand)
				{
					//Logger.Log("Buffer command:" + cmd);
					bufferedCommand = cmd;
					bufferedTime = COMMAND_BUFFER_TIME;
				}
			}
			else
			{
				if (cmd == bufferedCommand)
				{
					//Logger.Log("Clear buffer command for casted.");
					bufferedCommand = Command.None;
				}
			}
		}
		if (cmd == Command.Switch)
		{
			GameMatch_MultiPlayer match = m_match as GameMatch_MultiPlayer; 
			if(match != null)
				match.OnSwitch();
		}
		//for prepare shoot
		if( cmd == Command.Shoot && curState.m_eState == PlayerState.State.ePrepareToShoot )
		{
			//m_player.m_bToShoot = true;
			PlayerState_PrepareToShoot prepareShoot = m_player.m_StateMachine.GetState(PlayerState.State.ePrepareToShoot) as PlayerState_PrepareToShoot;
			m_player.m_toSkillInstance = prepareShoot.mCachedShootSkill;
			return;
		}

		if( cmd == Command.Pass )
		{
			if( m_player.m_passTarget == null )
				m_player.m_passTarget = PassHelper.ChoosePassTarget(m_player);
		}

		SkillInstance skill = m_player.m_skillSystem.GetValidSkillInMatch(cmd);
		//Logger.Log( Time.frameCount + "skill: " + (skill != null ? skill.skill.name : "NULL" ));
		if( skill == null )
		{
			//Logger.LogError("Can not get valid skill, check config.");
			return;
		}

		//failed to dunk
		/*
		if( (Command)skill.skill.action_type == Command.Dunk || (Command)skill.skill.action_type == Command.Layup )
		{
			if( m_player.m_defenseTarget != null )
			{
				Player nearestValidDefender = null;
				float fNearestDist = float.MaxValue;
				foreach(Player pl in m_player.m_defenseTarget.m_team)
				{
					if( m_player.m_AOD.GetStateByPos(pl.position) != AOD.Zone.eInvalid )
					{
						float fDist = GameUtils.HorizonalDistance(m_player.position, pl.position);
						if( fDist > fNearestDist )
							continue;
						fNearestDist = fDist;
						nearestValidDefender = pl;
					}
				}
				if( nearestValidDefender != null )
				{
					float fDist = GameUtils.HorizonalDistance(m_player.position, nearestValidDefender.position);
					if( fDist < skill.skill.attrange )
					{
						//if( !m_player.m_bSimulator )
						//	m_player.m_StateMachine.SetState(PlayerState.State.eLayupFailed);
						if( !m_player.m_bSimulator )
							ShootHelper.ShootByArea(m_player, GameSystem.Instance.mClient.mCurMatch);
						return;
					}
				}
			}
		}
		*/

		skill.curInput.inputType = InputDirType.eBasket;
		skill.curInput.cmd = cmd;
		skill.curInput.moveDir = m_player.m_inputDispatcher.GetMoveDirection(InputDirType.eBasket);

		/*
		SkillInput input = skill.curAction.inputs[0];
		if( (Command)skill.skill.action_type == Command.CrossOver && input.moveDir == EDirection.eNone )
		{
			Player defender = m_player.m_defenseTarget;
			if (defender != null)
			{
				Vector3 dirPlayerToDefenser = (defender.position - m_player.position).normalized;
				float angle = Quaternion.FromToRotation(dirPlayerToDefenser, m_player.right).eulerAngles.y;
				bool horiCross = (45f < angle && angle < 135f);
				bool left = angle < 90f || angle > 270f;
				List<SkillInstance> crossSkills = m_player.m_skillSystem.GetSkillList(SkillType.ACTIVE, Command.CrossOver);
				SkillInstance inst = AIUtils.ChooseCrossSkill(crossSkills, horiCross, left);
				if (m_player.m_stamina.m_curStamina.ToUnity() >= (float)inst.skill.levels[inst.level].stama )
					skill = inst;
			}
		}
		*/

		m_player.m_toSkillInstance = skill;
		//Logger.Log( "skill type: " + m_player.m_toSkillInstance.skill.name );
	}
	
	void _UpdateInputDirection(InputDirection dir, Command cmd)
	{
		if (!m_enableMove)
		{
			m_worldMoveDir = IM.Vector3.zero;
			m_player.m_moveHelper.StopMove();
			return;
		}
        //InputManager input = GameSystem.Instance.mClient.mInputManager;
        //bool bRush = (input.m_CmdBtn2Click && match.m_uiController.m_btns[1].cmd == Command.Rush);
        bool bRush = (cmd == Command.Rush);

		IM.Vector3 stickDir = Dir2Vec3(dir);
		if (dir == InputDirection.Null || dir == InputDirection.None) // dead zone
		{
			m_player.moveDirection = IM.Vector3.zero;
			m_player.m_dir	 	= -1;

			if( m_player.m_moveType != MoveType.eMT_Defense && m_player.m_moveType != MoveType.eMT_Stand )
				m_player.m_moveType = (bRush || rushExtending) ? MoveType.eMT_Rush : MoveType.eMT_Run;

			m_worldMoveDir = IM.Vector3.zero;
		}
		else
		{
            /*
            //TODO 根据摄像头位置对操作方向进行转换
			Vector3 camDir = Vector3.forward;
			if( match != null )
				camDir = match.m_origCameraForward;
			camDir.y = 0.0f;
	        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, camDir);
	
	        // Convert joystick input in WorldSpace coordinates
			m_worldMoveDir = referentialShift * stickDir;
            */
            m_worldMoveDir = stickDir;

            m_player.m_dir = Dir2Dir(dir);
            m_player.m_curInputDir = m_player.m_dir;

			if( m_player.m_moveType != MoveType.eMT_Defense )
				m_player.m_moveType = (bRush || rushExtending) ? MoveType.eMT_Rush : MoveType.eMT_Run;
		}
	}

    static IM.Vector3 Dir2Vec3(InputDirection dir)
    {
        IM.Number horiAngle = Dir2Dir(dir) * GlobalConst.ROTATE_ANGLE_SEC;
        IM.Vector3 vec3 = IM.Quaternion.Euler(IM.Number.zero, horiAngle, IM.Number.zero) * IM.Vector3.forward;
        return vec3;
    }

    static int Dir2Dir(InputDirection dir)
    {
        return (int)dir - 1;
    }
}