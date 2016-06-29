using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerState
{
	//玩家状态
	public enum State
	{
		eNone = 0,
		//move
		eStand = 8,					//站立,
		eHold = 9,					//持球站立，
		eRun = 10,					//跑，
		eCrossOver = 11,			//突破，
		eSwitchBall = 12,			//交叉球，
		eRush = 13,					//冲刺，
		eRushTurning = 14,			//冲刺转身，

		//被动
		eKnocked = 20,				//被撞，
		eFallLostBall = 21,			//落地失球，
		eFallGround = 22,			//空中被撞倒
		ePickup = 23,				//捡球
		eCatch = 24,				//接球
		eIdlePose = 25,				//闲置动作
		eStolen = 26,				//被偷
		eCrossed = 27,				//被突破
		eBackToStand = 28,			//背打
		eBackTurnRun = 29,			//背打转身
		eBackBlock = 30,			//防守背打
		eDefenseCross = 31,			//防突破
		eInterception = 32,			//抄截
        eDisturb = 33,              //干扰
		eLayupFailed = 34,			//上篮灌篮失败
		eBackCompete = 35,			//背打对抗
		//主动
		eInput = 39,				//输入同步
		eRequireBall = 40,			//要球
		eRebound = 41,				//篮板
		ePass = 42,					//传球
		eLayup	= 43,				//上篮
		ePrepareToShoot = 44,		//准备投篮
		eShoot = 45,				//投篮
		eDunk = 46,					//灌篮
		eGoalPose = 47,				//胜利姿势
		eDefense = 48,				//防守
		eSteal = 49,				//偷球
		eBlock = 50,				//盖帽
		eMoveStep = 51,				//后撤步
		eResultPose = 52,			//结算动作
		ePickAndRoll = 53,			//单挡
		eBePickAndRoll = 54,		//被单挡
		eBodyThrowCatch = 55,		//扑球
		eCutIn = 57,				//空切
		eBackToBack = 58,			//背打等待
		eBackToBackForward = 59,	//背打移动

		eMax = 70,
	}
	
	public State 					m_eState{ get; protected set; }
	
	protected PlayerStateMachine 	m_stateMachine;
	public Player m_player { get; private set; }

	public RotateTo		m_rotateTo 		= RotateTo.eNone;
	public IM.Vector3	m_rotateFreeDir;
	public IM.Number    m_relAngle = IM.Number.zero;

	public RotateType	m_rotateType 	= RotateType.eDirect;
	public List<int>	m_lstActionId 	= new List<int>();

	public IM.Vector3 	m_speed = IM.Vector3.zero;
	public IM.Vector3 	m_accelerate = IM.Vector3.zero;

	public IM.Number	m_turningSpeed = IM.Number.zero;

	public delegate void OnActionDone();
	public OnActionDone		m_onActionDone;

	public SkillInstance		m_curExecSkill;

	protected GameMatch		m_match;
	protected UBasketball	m_ball
	{
		get
		{ 
			if( m_player.m_ball != null ) 
				return m_player.m_ball;
			else 
				return m_match.mCurScene.mBall; 
		}
	}
	protected UBasket		m_basket;
	public string curAction { get { return m_curAction; } }
	protected string m_curAction;
	protected bool			m_bMoveForward = true;

	protected HashSet<Command>		m_validStateTransactions = new HashSet<Command>();
	public IM.Number time { get { return m_time; } }
	protected IM.Number			m_time;

	public AnimType						m_animType;
	protected Dictionary<AnimType, string>	m_mapAnimType = new Dictionary<AnimType, string>();

	protected IM.Number			m_playbackSpeed = IM.Number.one;

	public PlayerState (PlayerStateMachine owner, GameMatch match)
	{
		m_stateMachine = owner;
		m_player 	= m_stateMachine.m_owner;
		m_match 	= match;
		m_basket 	= match.mCurScene.mBasket;

		m_curExecSkill = null;
	}

	virtual public bool PreEnter()
	{
		return true;
	}
	
	virtual public void OnEnter ( PlayerState lastState )
	{
		m_rotateTo 		= RotateTo.eNone;
		m_rotateType 	= RotateType.eDirect;
		m_speed 		= IM.Vector3.zero;
		m_accelerate 	= IM.Vector3.zero;
		m_turningSpeed 	= IM.Number.zero;
		m_bMoveForward 	= true;

		m_playbackSpeed = IM.Number.one;

		m_curExecSkill 	= m_player.m_toSkillInstance;
		if( m_curExecSkill != null )
		{
			m_player.m_stamina.ConsumeStamina(new IM.Number((int)m_curExecSkill.skill.levels[m_curExecSkill.level].stama ));
		}

		m_time = IM.Number.zero;
	}

	virtual public void OnFaceToBasket()
	{
	}

	virtual public void Update (IM.Number fDeltaTime)
	{
		m_time += fDeltaTime;

		if( m_speed != IM.Vector3.zero || m_accelerate != IM.Vector3.zero )
		{
			IM.Vector3 curSpeed = m_speed;
			if( m_accelerate != IM.Vector3.zero )
				m_speed += m_accelerate * fDeltaTime;

			if( !m_bMoveForward )
				m_player.Move( fDeltaTime, m_speed);
			else
			{
                IM.Number m_speedMag = m_speed.magnitude;
                IM.Vector3 m_speedNor = m_speed.normalized;
                IM.Vector3 vPlayerSpace = m_player.rotation * m_speedMag * m_speedNor; 
				m_player.MoveTowards( m_player.forward, m_turningSpeed ,fDeltaTime, vPlayerSpace);
			}
		}

		//rotation
		if( m_rotateTo != RotateTo.eNone )
		{
			IM.Vector3 vRotateTo = IM.Vector3.zero;
            if (m_rotateTo == RotateTo.eBasket)
                vRotateTo = m_basket.m_vShootTarget;
            else if (m_rotateTo == RotateTo.eDefenseTarget)
                vRotateTo = m_player.m_defenseTarget.position;
            else if (m_rotateTo == RotateTo.eBall)
                vRotateTo = m_ball.position;

			vRotateTo = GameUtils.HorizonalNormalized(vRotateTo, m_player.position);
			if (m_relAngle != IM.Number.zero)
			{
				vRotateTo = IM.Quaternion.AngleAxis(m_relAngle, IM.Vector3.up) * vRotateTo;
			}

			if( IM.Vector3.Angle(m_player.forward, vRotateTo) < new IM.Number(0,100) )
				m_rotateTo = RotateTo.eNone;
			else
			{
				if( m_rotateType == RotateType.eDirect )
				{
					m_player.forward = vRotateTo;
					m_rotateTo = RotateTo.eNone;
				}
				else if( m_rotateType == RotateType.eSmooth )
				{
					IM.Number step = m_turningSpeed * fDeltaTime;
                    m_player.forward = IM.Vector3.RotateTowards(m_player.forward, vRotateTo, step, IM.Number.zero);
				}
			}
		}

		if( m_curAction != null && m_curAction.Length > 0 && !m_player.animMgr.IsPlaying(m_curAction) )
			_OnActionDone();
	}

	virtual public void OnLeaveGround()
	{
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.LeaveGround);
	}

	virtual public void OnGround()
	{
		m_speed = IM.Vector3.zero;
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.OnGround);
	}

	virtual public void LateUpdate(IM.Number fDeltaTime)
	{
        /*
        //TODO 这段代码不知道什么意思，待探讨
		if( !m_player.m_bOnGround )
		{
			IM.Number fDelta = new IM.Number((m_player.m_pelvis.position.y - m_player.model.root.position.y)) * (IM.Number.one / m_player.scale.y - IM.Number.one);
			m_player.model.root.position += (IM.Vector3.up * fDelta).ToUnity();

			//m_player.m_pelvis.position = new Vector3( m_player.m_pelvis.position.x, m_player.m_pelvis.position.y / m_player.localScale.y, m_player.m_pelvis.position.z );
			//m_player.m_ballSocket.position = new Vector3( m_player.m_ballSocket.position.x, m_player.m_ballSocket.position.y / m_player.localScale.y, m_player.m_ballSocket.position.z );
		}
		else
		{
			IM.Vector3 vPos = new IM.Vector3(m_player.gameObject.transform.position);
			m_player.gameObject.transform.position = new IM.Vector3(vPos.x, IM.Number.zero, vPos.z).ToUnity();
		}
        */
	}

	public bool IsCommandValid(Command cmd)
	{
		return m_validStateTransactions.Contains(cmd);
	}

	protected void _UpdatePassiveStateTransaction( SkillInstance newSkill )
	{
		if( newSkill == null )
			return;

		//Debug.Log(m_player.m_id + " Execute skill " + newSkill.skill.id + " " + newSkill.skill.name);
		Command cmd = (Command)newSkill.skill.action_type;
		if (!IsCommandValid(cmd))
		{
			Debug.Log("Command " + cmd + " invalid. State: " + m_eState);
			return;
		}

		switch( cmd )
		{
		case Command.Shoot:
				ShootHelper.ShootByInput(m_player);
			break;
		case Command.Block:
			m_stateMachine.SetState(PlayerState.State.eBlock, true);
			break;
		case Command.CrossOver:
			m_stateMachine.SetState(PlayerState.State.eCrossOver, true);
			break;
		case Command.Dunk:
			m_stateMachine.SetState(PlayerState.State.eDunk, true);
			break;
		case Command.Layup:
			m_stateMachine.SetState(PlayerState.State.eLayup, true);
			break;
		case Command.MoveStep:
			m_stateMachine.SetState(PlayerState.State.eMoveStep, true);
			break;
		case Command.Boxout:
			m_stateMachine.SetState(PlayerState.State.eStand);
			break;
		case Command.Steal:
			m_stateMachine.SetState(PlayerState.State.eSteal, true);
			break;
		case Command.Pass:
		{
			if( m_player.m_passTarget == null )
				m_player.m_passTarget = PassHelper.ChoosePassTarget(m_player);
			if( m_player.m_passTarget != null )
				m_stateMachine.SetState(PlayerState.State.ePass, true);
		}
			break;
		case Command.Rebound:
			m_stateMachine.SetState(PlayerState.State.eRebound, true);
			break;
		case Command.RequireBall:
			m_stateMachine.SetState(PlayerState.State.eRequireBall);
			break;
		case Command.CutIn:
			m_stateMachine.SetState(PlayerState.State.eCutIn);
			break;
		case Command.BodyThrowCatch:
			m_stateMachine.SetState(PlayerState.State.eBodyThrowCatch);
			break;
		case Command.BackToBack:
			m_stateMachine.SetState(PlayerState.State.eBackToBack);
			break;
		case Command.PickAndRoll:
			m_stateMachine.SetState(PlayerState.State.ePickAndRoll);
			break;
		case Command.Defense:
			m_stateMachine.SetState(PlayerState.State.eDefense);
			break;
		case Command.Interception:
			m_stateMachine.SetState(PlayerState.State.eInterception);
			break;
		default:
			m_stateMachine.SetState(PlayerState.State.eStand);
			break;
		}
	}

	virtual protected void _OnActionDone()
	{
		if( m_onActionDone != null )
			m_onActionDone();
	}

	virtual public void OnExit ()
	{
		m_rotateTo = RotateTo.eNone;
		m_speed = IM.Vector3.zero;
		m_accelerate = IM.Vector3.zero;
		m_turningSpeed = IM.Number.zero;
		m_curExecSkill = null;
		m_time = IM.Number.zero;

		if( m_onActionDone != null )
			m_onActionDone();

		m_player.m_blockable.Clear();

		m_lstActionId.Clear();
	}
}