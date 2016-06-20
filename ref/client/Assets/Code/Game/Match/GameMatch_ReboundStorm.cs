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
			Logger.LogError("No additional info. GameMode ID: " + gameMode.ID);
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

	override public void OnSceneComplete()
	{
		GameMatch.Config.TeamMember member = m_config.NPCs[1];
		_GeneratePlayerData(member, member.team != m_config.MainRole.team);

		base.OnSceneComplete();
		 
		if (m_config == null)
		{
			Logger.LogError("Match config file loading failed.");
			return;
		}

		//main role
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		m_mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		m_mainRole.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(m_mainRole.m_StateMachine, this));
		//m_mainRole.m_inputDispatcher = new InputDispatcher(m_mainRole);
		//m_mainRole.m_inputDispatcher.m_enable = false;

		m_mainRole.m_catchHelper = new CatchHelper(m_mainRole);
		m_mainRole.m_catchHelper.ExtractBallLocomotion();
		m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		m_mainRole.m_InfoVisualizer.CreateStrengthBar();
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		m_mainRole.m_team.m_role = GameMatch.MatchRole.eDefense;
		m_mainRole.m_alwaysForbiddenPickup = true;

		//npc
		Team oppoTeam = m_mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
		npc.m_aiMgr = new AISystem_ReboundStorm(this, npc, AIState.Type.eReboundStorm_Idle, m_config.NPCs[0].AIID);
		npc.m_team.m_role = GameMatch.MatchRole.eOffense;
		npc.m_alwaysForbiddenPickup = true;

		//shooter
		shooter = oppoTeam.GetMember(1);
		if (shooter.model != null)
			shooter.model.EnableGrey();

		m_auxiliaries.Add(shooter.m_id);

		shooter.m_aiMgr = new AISystem_ReboundStorm(this, shooter, AIState.Type.eReboundStorm_ShooterIdle, m_config.NPCs[0].AIID);
		shooter.m_team.m_role = GameMatch.MatchRole.eOffense;
		shooter.m_alwaysForbiddenPickup = true;

		_UpdateCamera(m_mainRole);



	}
	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();
		m_stateMachine.SetState(m_config.needPlayPlot ? MatchState.State.ePlotBegin : MatchState.State.eShowRule);
	}

    public override void HandleGameBegin(Pack pack)
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
        ReboundStormPos reboundStormPos = GameSystem.Instance.MatchPointsConfig.ReboundStormPos;
        m_mainRole.position = reboundStormPos.mainRole_transform.position;
		m_mainRole.forward = -IM.Vector3.forward;
        npc.position = reboundStormPos.npc_transform.position;
		npc.forward = -IM.Vector3.forward;
        ResetShooter(reboundStormPos.shoots_transform[3]);

		if (m_mainRole.m_bWithBall)
			m_mainRole.DropBall(m_mainRole.m_ball);
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

	public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
		return IM.BigNumber.one;
	}

	protected override void CreateCustomGUI()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchReboundStorm") as GameObject;
		uiMatch = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<UIMatchReboundStorm>();
		gameMatchTime = new IM.Number((int)gameMode.time);
        if (m_gameMathCountEnable)
        {
            if (m_gameMathCountTimer == null)
            {
                m_gameMathCountTimer = new GameUtils.Timer(gameMatchTime, () => { m_stateMachine.SetState(MatchState.State.eOver); 
                                              NGUITools.SetActive(uiMatch.transform.FindChild("ButtonBack").gameObject, false); });
            }
            else
            {
                m_gameMathCountTimer.SetTimer(gameMatchTime);
            }
        }
		m_gameMatchCountStop = true;
	}

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

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

		if (m_mainRole != null && npc != null) {
			if (refreshOnReboundOver &&
				m_mainRole.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound &&
				npc.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound) {
				Refresh ();
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
		DestroyHandBall(m_mainRole);
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
			if (ball.m_owner == m_mainRole)
			{
				++myCombo;
				float bonusRatio = GameSystem.Instance.GameModeConfig.GetComboBonus(GetMatchType(), myCombo);
				if (myCombo > 1)
				{
					ShowCombo(myCombo);
					ShowComboBonus(bonusRatio);
				}
				m_homeScore += (int)(score * (1 + bonusRatio));
				m_mainRole.mStatistics.success_rebound_times = (uint)m_homeScore;
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
		ResetShooter(shooterPos[Random.Range(0, shooterPos.Count - 1)]);
		FetchBall(shooter);
	}
}
