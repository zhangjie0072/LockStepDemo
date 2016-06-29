using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 篮板风暴【玩法】
/// </summary>
public class GameMatch_ReboundStorm : GameMatch
{
	public IM.Number npcHeightScale = IM.Number.one;
	public IM.Number playerHeightScale = IM.Number.one;
	public IM.Number ballHeightScale = IM.Number.one;
	public Player npc;
	public Player shooter;
	private GameObject ballConveyor;
	private Queue<UBasketball> ballQ = new Queue<UBasketball>();
	public UBasketball currBall { get; private set; }

	private uint myCombo;
	private uint npcCombo;

	private bool shooting;
	private bool firstTime = true;
	private bool refreshOnReboundOver = false;

	private IM.Number specialBallRate;
	private int normalBallScore = 20;
	private int specialBallScore = 40;

	private UIMatchReboundStorm uiMatch;

	public GameMatch_ReboundStorm(Config config)
		: base(config)
	{
		string[] tokens = gameMode.extraLevelInfo.Split('&');
		npcHeightScale = IM.Number.Parse(tokens[0]);
        playerHeightScale = IM.Number.Parse(tokens[1]);
        ballHeightScale = IM.Number.Parse(tokens[2]);
		if (string.IsNullOrEmpty(gameMode.additionalInfo))
		{
			Debug.LogError("No additional info. GameMode ID: " + gameMode.ID);
		}
		else
		{
			tokens = gameMode.additionalInfo.Split('&');
            specialBallRate = IM.Number.Parse(tokens[0]);
			normalBallScore = int.Parse(tokens[1]);
			specialBallScore = int.Parse(tokens[2]);
		}

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
	}

	protected override void _CreatePlayersData()
	{
		_GeneratePlayerData(m_config.MainRole, m_config.MainRole.id.Length > 4 );
		GameMatch.Config.TeamMember member = m_config.NPCs[0];
		_GeneratePlayerData(member, member.team != m_config.MainRole.team);
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		GameMatch.Config.TeamMember member = m_config.NPCs[1];
		_GeneratePlayerData(member, member.team != m_config.MainRole.team);
		 
		if (m_config == null)
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}

        //TODO 针对PVP修改
		//main role
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		mainRole.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(mainRole.m_StateMachine, this));
		//mainRole.m_inputDispatcher = new InputDispatcher(mainRole);
		//mainRole.m_inputDispatcher.m_enable = false;

		mainRole.m_catchHelper = new CatchHelper(mainRole);
		mainRole.m_catchHelper.ExtractBallLocomotion();
		mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		mainRole.m_team.m_role = GameMatch.MatchRole.eDefense;
		mainRole.m_alwaysForbiddenPickup = true;

		//npc
		Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
		npc.m_team.m_role = GameMatch.MatchRole.eOffense;
        npc.operMode = Player.OperMode.AI;
		npc.m_alwaysForbiddenPickup = true;

		//shooter
		shooter = oppoTeam.GetMember(1);
		if (shooter.model != null)
			shooter.model.EnableGrey();

		m_auxiliaries.Add(shooter.m_id);

		shooter.m_team.m_role = GameMatch.MatchRole.eOffense;
        shooter.operMode = Player.OperMode.AI;
		shooter.m_alwaysForbiddenPickup = true;

		_UpdateCamera(mainRole);
	}

    public override AISystem CreateAISystem(Player player)
    {
        if (player == npc)
            return new AISystem_ReboundStorm(this, npc, AIState.Type.eReboundStorm_Idle, m_config.NPCs[0].AIID);
        else if (player == shooter)
            return new AISystem_ReboundStorm(this, shooter, AIState.Type.eReboundStorm_ShooterIdle, m_config.NPCs[0].AIID);
        return null;
    }
	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();
		m_stateMachine.SetState(m_config.needPlayPlot ? MatchState.State.ePlotBegin : MatchState.State.eShowRule);
	}

    public override void OnGameBegin(GameBeginResp resp)
    {
        m_stateMachine.SetState(MatchState.State.eBegin);
    }

	public override void CreateUI ()
	{
		_CreateGUI();
		CreateConveyor();
	}

	public override bool TimmingOnStarting()
	{
		return true;
	}

	public override bool EnableGoalState()
	{
		return false;
	}

	public override bool EnableCounter24()
	{
		return false;
	}

	public override bool EnableCheckBall()
	{
		return false;
	}

	public override bool EnableSwitchRole()
	{
		return false;
	}

	public override bool EnablePlayerTips()
	{
		return false;
	}

    public override bool EnableNPCGoalSound()
    {
        return false;
    }

	public override void ResetPlayerPos()
	{
        //TODO 针对PVP修改
        ReboundStormPos reboundStormPos = GameSystem.Instance.MatchPointsConfig.ReboundStormPos;
        mainRole.position = reboundStormPos.mainRole_transform.position;
		mainRole.forward = -IM.Vector3.forward;
        npc.position = reboundStormPos.npc_transform.position;
		npc.forward = -IM.Vector3.forward;
        ResetShooter(reboundStormPos.shoots_transform[3]);

		if (mainRole.m_bWithBall)
			mainRole.DropBall(mainRole.m_ball);
		if (npc.m_bWithBall)
			npc.DropBall(npc.m_ball);
		if (shooter.m_bWithBall)
			shooter.DropBall(shooter.m_ball);
	}

	private void ResetShooter(IM.Transform trans)
	{
        shooter.position = trans.position;
		shooter.forward = IM.Vector3.forward;
	}

	public override void ConstrainMovementOnBegin(IM.Number fCurTime)
	{
		if (m_ruler.prepareTime < fCurTime)
			return;

		ResetPlayerPos();
	}

	public override bool IsCommandValid(Command command)
	{
		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
			return command == Command.Rebound;
		else
			return base.IsCommandValid(command);
	}

	public override IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
        return IM.PrecNumber.zero;
    }

	protected override void CreateCustomGUI()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchReboundStorm") as GameObject;
		uiMatch = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<UIMatchReboundStorm>();
        uiMatch.timerBoard.isChronograph = true;
		gameMatchTime = new IM.Number((int)gameMode.time);
        uiMatch.timerBoard.UpdateTime((float)gameMatchTime);
        if (m_gameMathCountEnable)
        {
            if (m_gameMathCountTimer == null)
            {
                m_gameMathCountTimer = new GameUtils.Timer(gameMatchTime, () => { m_stateMachine.SetState(MatchState.State.eOver); 
                                              NGUITools.SetActive(uiMatch.transform.FindChild("ButtonBack").gameObject, false);
                                              uiMatch.timerBoard.UpdateTime(0f);
                });
            }
            else
            {
                m_gameMathCountTimer.SetTimer(gameMatchTime);
            }
        }
		m_gameMatchCountStop = true;
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (uiMatch != null) {
			if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
				m_gameMatchCountStop = false;
			else
                m_gameMatchCountStop = true;
		}

		if (firstTime &&
			m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying &&
			!shooter.m_bWithBall && shooter.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
		{
			FetchBall(shooter);
			firstTime = false;
		}

        //TODO 针对PVP修改
		if (mainRole != null && npc != null) {
			if (refreshOnReboundOver &&
				mainRole.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound &&
				npc.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound) {
				Refresh ();
			}
		}
        //添加倒计时相关的代码
        if (m_gameMathCountTimer != null && !m_gameMatchCountStop && m_gameMathCountEnable)
        {
            if (uiMatch != null)
            {
                uiMatch.timerBoard.UpdateTime((float)m_gameMathCountTimer.Remaining());
            }
        }
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (uiMatch != null)
            PlayTimeSound((float)gameMatchTime, float.PositiveInfinity);
    }

	private void CreateConveyor()
	{
		ConveyorBall.Clear();
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/BallConveyor") as GameObject;
		ballConveyor = CommonFunction.InstantiateObject(prefab, GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform);
		ConveyorBall firstBall = ballConveyor.GetComponentInChildren<ConveyorBall>();
		OnCreateNewBall(firstBall);
		ConveyorBall.onCreateNewBall = OnCreateNewBall;
		ConveyorBall.fastForward = true;
	}

	private void OnBallReachExit(ConveyorBall conveyorBall)
	{
		if (conveyorBall.index == 0)
		{
			ConveyorBall.fastForward = false;
		}
		ConveyorBall.Pause();
	}

	private void OnCreateNewBall(ConveyorBall conveyorBall)
	{
		conveyorBall.onReachExit += OnBallReachExit;
		UBasketball ball = CreateBall();
		conveyorBall.GetComponent<UISprite>().spriteName = ball.m_special ? conveyorBall.specialBallSprite : conveyorBall.normalBallSprite;
		ballQ.Enqueue(ball);
	}

	private UBasketball CreateBall()
	{
		UBasketball ball = mCurScene.CreateBall();
		ball.onShoot += OnShoot;
		ball.onHitGround += OnHitGround;
		ball.onGrab += OnGrab;
		ball.m_special = IM.Random.value < specialBallRate;
		ball.gameObject.SetActive(false);
		ball.position = new IM.Vector3(IM.Number.zero, IM.Number.zero, -new IM.Number(5));
		return ball;
	}

	private void FetchBall(Player player)
	{
		UBasketball ball = ballQ.Dequeue();
		ball.gameObject.SetActive(true);
		player.GrabBall(ball);
		currBall = ball;
		if (ball.m_special)
			ShowOpportunity();
		ConveyorBall.DestroyFront();
		ConveyorBall.Resume();
		DestroyHandBall(mainRole);
		DestroyHandBall(npc);
	}

	private void OnShoot(UBasketball ball)
	{
		shooting = true;
		HideOpportunity();
	}

	private void DestroyHandBall(Player player)
	{
		if (player.m_bWithBall)
		{
			UBasketball ballInHand = player.m_ball;
			player.DropBall(ballInHand);
			mCurScene.DestroyBall(ballInHand);
			player.m_StateMachine.SetState(PlayerState.State.eStand);
		}
	}

	private void OnHitGround(UBasketball ball)
	{
		if (m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
		{
			mCurScene.DestroyBall(ball);
			myCombo = 0;
			HideCombo();
			HideComboBonus();
			npcCombo = 0;
			refreshOnReboundOver = true;
		}
	}

	private void OnGrab(UBasketball ball)
	{
		if (shooting)
		{
			int score = currBall.m_special ? specialBallScore : normalBallScore;
			refreshOnReboundOver = true;
			if (ball.m_owner == mainRole)
			{
				++myCombo;
				float bonusRatio = GameSystem.Instance.GameModeConfig.GetComboBonus(GetMatchType(), myCombo);
				if (myCombo > 1)
				{
					ShowCombo(myCombo);
					ShowComboBonus(bonusRatio);
				}
				m_homeScore += (int)(score * (1 + bonusRatio));
				mainRole.mStatistics.success_rebound_times = (uint)m_homeScore;
				uiMatch.leftScore = m_homeScore;
				npcCombo = 0;
			}
			else if (ball.m_owner == npc)
			{
				++npcCombo;
				float bonusRatio = GameSystem.Instance.GameModeConfig.GetComboBonus(GetMatchType(), npcCombo);
				m_awayScore += (int)(score * (1 + bonusRatio));
				npc.mStatistics.success_rebound_times = (uint)m_awayScore;
				uiMatch.rightScore = m_awayScore;
				myCombo = 0;
				HideCombo();
				HideComboBonus();
			}
		}
	}

	private void Refresh()
	{
		refreshOnReboundOver = false;
		shooting = false;
		//ResetPlayerPos();
        List<IM.Transform> shooterPos = GameSystem.Instance.MatchPointsConfig.ReboundStormPos.shoots_transform;
		ResetShooter(shooterPos[IM.Random.Range(0, shooterPos.Count - 1)]);
		FetchBall(shooter);
	}
}
