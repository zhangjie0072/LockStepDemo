using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMatch_BullFight : GameMatch, GameMatch.Count24Listener, MatchStateMachine.Listener
{
	private IM.Number OFFENSE_TIME_LIMIT;
	private int WIN_SCORE;
	public SlotMachine slotMachine;
	public Player npc;
	private GameUtils.Timer timerReset;
	private GameUtils.Timer timerOver;

	private AISystem mainRoleNormalAISystem;
	private AISystem mainRolePositioningAISystem;
	private AISystem npcNormalAISystem;
	private AISystem npcPositioningAISystem;
	private AI_BullFight_Positioning mainRoleAIState;
	private AI_BullFight_Positioning npcAIState;

	private bool m_needSwitchRole = false;
	private bool m_showShootFarTip = false;

	private bool m_mainRoleStart = false;
	private bool _inPositioning = false;
	private bool m_inPositioning
	{
		get { return _inPositioning; }
		set
		{
			_inPositioning = value;
			if (value)
			{
				m_uiMatch.ShowCounter(false, false);
                m_count24TimeStop = true;
			}
			else
			{
                //TODO 针对PVP修改
				m_uiMatch.ShowCounter(true, mainRole.m_team.m_role == MatchRole.eOffense);
                m_count24Time = MAX_COUNT24_TIME;
                m_count24TimeStop = false;
			}
		}
	}
	private bool m_criticalShoot = false;

	public GameMatch_BullFight(Config config)
		: base(config)
	{
        if( gameMode != null )
        {
            string[] tokens = gameMode.additionalInfo.Split('&');

            OFFENSE_TIME_LIMIT = IM.Number.Parse(tokens[0]);
            WIN_SCORE = int.Parse(tokens[1]);
        }
        else
        {
            OFFENSE_TIME_LIMIT = new IM.Number(14);
            WIN_SCORE = 5;
        }
	

		timerReset = new GameUtils.Timer(IM.Number.two, Reset);
		timerReset.stop = true;
		timerOver = new GameUtils.Timer(IM.Number.two, Over);
		timerOver.stop = true;
		 
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		mCurScene.CreateBall();
		mCurScene.mBall.onShootGoal += OnGoal;
		mCurScene.mBall.onGrab += OnGrab;
		mCurScene.mBall.onDunk += OnDunk;
		mCurScene.mBall.onHitGround += OnHitGround;
		
		if (m_config == null)
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}

        //TODO 针对PVP修改
		//main role
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		//mainRole.m_inputDispatcher = new InputDispatcher(this, mainRole);
		mainRole.m_catchHelper = new CatchHelper(mainRole);
		mainRole.m_catchHelper.ExtractBallLocomotion();
		mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
		mainRoleNormalAISystem = new AISystem_Basic(this, mainRole);
		//mainRole.m_aiMgr = mainRoleNormalAISystem;
		mainRole.m_aiMgr.m_enable = false;
		mainRolePositioningAISystem = new AISystem_BullFight(this, mainRole);
		mainRoleAIState = mainRolePositioningAISystem.GetState(AIState.Type.eBullFight_Positioning) as AI_BullFight_Positioning;
		mainRoleAIState.onArrive += OnArrive;

		//npc
        Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		if (npc.model != null)
			npc.model.EnableGrey();

		npcNormalAISystem = new AISystem_Basic(this, npc, AIState.Type.eInit, m_config.NPCs[0].AIID);
		npcNormalAISystem.ReplaceState(new AI_Positioning_Shoot_on_Hold(npcNormalAISystem));
		npcPositioningAISystem = new AISystem_BullFight(this, npc);
		npcAIState = npcPositioningAISystem.GetState(AIState.Type.eBullFight_Positioning) as AI_BullFight_Positioning;
		npcAIState.onArrive += OnArrive;
		//npc.m_aiMgr = npcNormalAISystem;
		npc.m_aiMgr.m_enable = false;
		npc.m_team.m_role = GameMatch.MatchRole.eDefense;

		AssumeDefenseTarget();
		_UpdateCamera(mainRole);
		//_CreateGUI();

		m_stateMachine.m_matchStateListeners.Add(this);

	}
	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();
		m_stateMachine.SetState(m_config.needPlayPlot ? MatchState.State.ePlotBegin : MatchState.State.eShowRule);
	}

	public override void CreateUI ()
	{
		_CreateGUI();
	}

	public override bool TimmingOnStarting()
	{
		return true;
	}

	public override bool EnableGoalState()
	{
		return m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying;
	}

	public override bool EnableCounter24()
	{
		return false;
	}

	public override bool EnableCheckBall()
	{
		return true;
	}

	public override bool EnableSwitchRole()
	{
		return m_needSwitchRole;
	}

	public override bool EnableTakeOver()
	{
		return false;
	}

	public override bool EnableEnhanceAttr()
	{
		return true;
	}

	public override bool EnablePlayerTips()
	{
		//3分球不提示，显示特殊的远投提示
		return (!(mCurScene.mBall.m_bGoal && mCurScene.mBall.m_pt == GlobalConst.PT_3) || m_showShootFarTip);
	}

	public override bool EnableMatchAchievement()
	{
		return true;
	}

	public override bool IsFinalTime(IM.Number seconds)
	{
        return m_count24Time < seconds;
	}

	public override int GetScore(int score)
	{
		return score == GlobalConst.PT_3 ? 2 : 1;
	}

	public void SetStartingAttacker(int index)
	{
        //TODO 针对PVP修改
		if (index == 0)
		{
			mainRole.m_team.m_role = MatchRole.eOffense;
			npc.m_team.m_role = MatchRole.eDefense;
		}
		else
		{
			mainRole.m_team.m_role = MatchRole.eDefense;
			npc.m_team.m_role = MatchRole.eOffense;
		}
	}

    public override void OnGameBegin(GameBeginResp resp)
    {
        m_stateMachine.SetState(MatchState.State.eFreeThrowStart);
    }

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);
		_RefreshAOD();

        timerReset.Update(deltaTime);
        timerOver.Update(deltaTime);
	}

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();

        m_gameMathCountEnable = false;
		m_gameMatchCountStop = true;
        m_count24Time = OFFENSE_TIME_LIMIT;
		m_uiMatch.winScore = WIN_SCORE;
        AddCount24Listener(this);
		m_uiMatch.timerBoard.isChronograph = false;
		m_uiMatch.leftScoreBoard.gap = 0;
		m_uiMatch.rightScoreBoard.gap = 0;
		m_uiMatch.digitCount = 0;
		m_uiMatch.ShowCounter(false, false);
	}

	private void CreateSlotMachine()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/SlotMachine") as GameObject;
		GameObject obj = CommonFunction.InstantiateObject(prefab, GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform);
		slotMachine = obj.GetComponent<SlotMachine>();
		slotMachine.itemCount = 4;
		NGUITools.SetActive(obj, false);
	}

	private void SwitchRole()
	{
        //TODO 针对PVP修改
		GameMatch.MatchRole temp = mainRole.m_team.m_role;
		mainRole.m_team.m_role = npc.m_team.m_role;
		npc.m_team.m_role = temp;
	}

	private void OnGoal(UBasketball ball)
	{
		if (m_stateMachine.m_curState.m_eState == MatchState.State.eFreeThrowStart)
		{
			if (ball.m_actor == mainRole)
				m_mainRoleStart = true;
		}
		else
		{
			m_needSwitchRole = false;
			m_criticalShoot = false;
			if (ball.m_pt == GlobalConst.PT_3 && ball.m_actor == mainRole)
			{
				m_showShootFarTip = true;
				ShowBasketTip(ball.m_actor, "gameInterface_tip_2point");
				m_showShootFarTip = false;
			}
		}
	}

	private void OnGrab(UBasketball ball)
	{
		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
            BeginPos beginPos = GameSystem.Instance.MatchPointsConfig.BeginPos;
			if (mainRole.m_team.m_role == MatchRole.eOffense)
			{
				if (ball.m_owner != mainRole)	//球被对方抢了
				{
					mainRole.m_inputDispatcher.m_enable = false;
					//mainRole.m_aiMgr = mainRolePositioningAISystem;
					if (mainRole.m_aiAssist != null)
						mainRole.m_aiAssist.Disable();
                    mainRoleAIState.moveTarget = beginPos.defenses_transform[0].position;
					//npc.m_aiMgr = npcPositioningAISystem;
                    npcAIState.moveTarget = beginPos.offenses_transform[0].position;
					m_inPositioning = true;
					m_ruler.SwitchRole();
					m_needSwitchRole = false;

					mainRoleAIState.arrived = false;
					npcAIState.arrived = false;

					ShowAnimTip("gameInterface_text_ChangeBall");
					timerReset.SetTimer(new IM.Number(6));
					timerReset.stop = false;
				} 
				else if (m_criticalShoot)	//球被自己抢了，但已经是绝杀球
					m_stateMachine.SetState(MatchState.State.eFoul);
			} 
			else if (mainRole.m_team.m_role == MatchRole.eDefense)
			{
				if (ball.m_owner == mainRole) //球被对方抢了
				{
					mainRole.m_inputDispatcher.m_enable = false;
					//mainRole.m_aiMgr = mainRolePositioningAISystem;
					if (mainRole.m_aiAssist != null)
						mainRole.m_aiAssist.Disable();
                    mainRoleAIState.moveTarget = beginPos.offenses_transform[0].position;
					//npc.m_aiMgr = npcPositioningAISystem;
                    npcAIState.moveTarget = beginPos.defenses_transform[0].position;
					m_inPositioning = true;
					m_ruler.SwitchRole();
					m_needSwitchRole = false;

					mainRoleAIState.arrived = false;
					npcAIState.arrived = false;

					ShowAnimTip("gameInterface_text_ChangeBall");
					timerReset.SetTimer(new IM.Number(6));
					timerReset.stop = false;
				}
				else if (m_criticalShoot)	//球被自己抢了，但已经是绝杀球
					m_stateMachine.SetState(MatchState.State.eFoul);
			}
		}
	}

	private void OnHitGround(UBasketball ball)
	{
		if (m_homeScore >= WIN_SCORE || m_awayScore >= WIN_SCORE)
		{
			timerOver.stop = false;
		}
		else if (m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
			if (m_criticalShoot)
				m_stateMachine.SetState(MatchState.State.eFoul);
		}
		else if (m_stateMachine.m_curState.m_eState == MatchState.State.eFreeThrowStart)
		{
			timerReset.stop = false;
			m_needSwitchRole = !m_mainRoleStart;
		}
	}

	public void OnTimeUp()
	{
		m_needSwitchRole = true;
		if (mCurScene.mBall.m_ballState == BallState.eUseBall_Shoot)
		{
			m_criticalShoot = true;
		}
		else
		{
			m_stateMachine.SetState(MatchState.State.eFoul);
		}
	}

	private void Reset()
	{
		timerReset.stop = true;
		m_criticalShoot = false;
		if (m_needSwitchRole)
		{
			m_ruler.SwitchRole();
			m_needSwitchRole = false;
		}
		EndSwitchPositioning();
		m_stateMachine.SetState(MatchState.State.eBegin);
        m_count24Time = MAX_COUNT24_TIME;
        m_count24TimeStop = true;
	}

	public override void ResetPlayerPos()
	{
		if (m_stateMachine.m_curState.m_eState == MatchState.State.ePlayerCloseUp ||
			m_stateMachine.m_curState.m_eState == MatchState.State.eFreeThrowStart)
		{
            mainRole.position = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position - IM.Vector3.forward * IM.Number.half;
            mainRole.FaceTo(mCurScene.mBasket.m_vShootTarget);

			mainRole.m_defenseTarget.position = mainRole.position + new IM.Vector3(IM.Number.two, IM.Number.zero, IM.Number.zero);
			mainRole.m_defenseTarget.forward =
                GameUtils.HorizonalNormalized(mCurScene.mBasket.m_vShootTarget, mainRole.m_defenseTarget.position);
		}
		else
		{
			base.ResetPlayerPos();
            m_count24Time = MAX_COUNT24_TIME;
			m_uiMatch.ShowCounter(true, mainRole.m_team.m_role == MatchRole.eOffense);
		}
	}

	private void Over()
	{
		timerOver.stop = true;
		m_stateMachine.SetState(MatchState.State.eOver);
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		if (goal)
			OnGoal(ball);
	}

	private void OnArrive(AI_BullFight_Positioning state)
	{
		if (m_inPositioning && mainRoleAIState.arrived && npcAIState.arrived)
		{
			EndSwitchPositioning();
		}
	}

	private void EndSwitchPositioning()
	{
		timerReset.stop = true;
		//npc.m_aiMgr = npcNormalAISystem;
		//mainRole.m_aiMgr = mainRoleNormalAISystem;
		mainRole.m_aiMgr.m_enable = false;
		mainRole.m_inputDispatcher.m_enable = true;
		m_inPositioning = false;
		HideAnimTip("gameInterface_text_ChangeBall");
	}

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
		if (oldState != null && oldState.m_eState == MatchState.State.eFoul)
		{
			m_needSwitchRole = false;
			Reset();
		}
		else if (newState.m_eState == MatchState.State.ePlayerCloseUp)
		{
			if (m_uiMatch != null)
				m_uiMatch.ShowCounter(false, false);
		}
		else if (newState.m_eState == MatchState.State.eFreeThrowStart)
		{
			if (m_uiMatch != null)
				m_uiMatch.ShowCounter(false, false);
		}
	}
}
