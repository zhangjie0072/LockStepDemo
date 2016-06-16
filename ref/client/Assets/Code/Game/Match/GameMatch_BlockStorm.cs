using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMatch_BlockStorm : GameMatch, PlayerActionEventHandler.Listener
{
	public Player npc;
	private GameUtils.Timer timerRefresh;
	private GameObject ballConveyor;
	private Queue<UBasketball> ballQ = new Queue<UBasketball>();
	private UBasketball currBall;

	private uint myCombo;

	private bool blocked;
	private bool firstTime = true;

	private IM.Number specialBallRate;
	private int normalBallScore;
	private int specialBallScore;
	private IM.Number npcAttackInterval;
	private int oneStarScore;
	private int twoStarScore;
	private int threeStarScore;

	private UIMatchBlockStorm uiMatch;

	public GameMatch_BlockStorm(Config config)
		: base(config)
	{
		string[] tokens = gameMode.additionalInfo.Split('&');
		specialBallRate = IM.Number.Parse(tokens[0]);
		normalBallScore = int.Parse(tokens[1]);
		specialBallScore = int.Parse(tokens[2]);
		if (!string.IsNullOrEmpty(gameMode.extraLevelInfo))
		{
			tokens = gameMode.extraLevelInfo.Split('&');
			npcAttackInterval = IM.Number.Parse(tokens[0]);
			oneStarScore = int.Parse(tokens[1]);
			twoStarScore = int.Parse(tokens[2]);
			threeStarScore = int.Parse(tokens[3]);
		}

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
	}

	override public void OnSceneComplete()
	{
		base.OnSceneComplete();

		if (m_config == null)
		{
			Logger.LogError("Match config file loading failed.");
			return;
		}

		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		m_mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );

		//m_mainRole.m_inputDispatcher = new InputDispatcher(m_mainRole);
		//m_mainRole.m_inputDispatcher.m_enable = false;

		m_mainRole.m_catchHelper = new CatchHelper(m_mainRole);
		m_mainRole.m_catchHelper.ExtractBallLocomotion();
		m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		m_mainRole.m_InfoVisualizer.CreateStrengthBar();
		m_mainRole.m_InfoVisualizer.ShowStaminaBar (true);
		m_mainRole.m_team.m_role = GameMatch.MatchRole.eDefense;
		m_mainRole.m_alwaysForbiddenPickup = true;
		m_mainRole.eventHandler.AddEventListener(this);
		
		//npc
		npc = pm.GetPlayerById( uint.Parse(m_config.NPCs[0].id) );
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();

		npc.m_aiMgr = new AISystem_BlockStorm(this, npc, AIState.Type.eBlockStorm_Idle, m_config.NPCs[0].AIID);
		npc.m_team.m_role = GameMatch.MatchRole.eOffense;

		npc.m_alwaysForbiddenPickup = true;

		AssumeDefenseTarget();
		_UpdateCamera(m_mainRole);

		m_stateMachine.SetState(m_config.needPlayPlot ? MatchState.State.ePlotBegin : MatchState.State.eShowRule);

		//对手分数不变，当己方得分达到1星分数时达成胜利条件
		m_awayScore = oneStarScore - 1;

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
        IM.Vector3 basketCenter = mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		m_mainRole.position = basketCenter;
		m_mainRole.forward = -IM.Vector3.forward;
        ResetNPC(GameSystem.Instance.MatchPointsConfig.BlockStormPos.npc_transforms[2]);

		if (m_mainRole.m_bWithBall)
			m_mainRole.DropBall(m_mainRole.m_ball);
		if (npc.m_bWithBall)
			npc.DropBall(npc.m_ball);
	}

	private void ResetNPC(IM.Transform trans)
	{
		npc.position = trans.position;
		npc.forward = IM.Vector3.forward;
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
			return command == Command.Block || command == Command.Rush;
		else
			return base.IsCommandValid(command);
	}

    public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
        return IM.BigNumber.one;
	}

	protected override void CreateCustomGUI()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchBlockStorm") as GameObject;
		uiMatch = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<UIMatchBlockStorm>();
		uiMatch.maxScore = oneStarScore;
        gameMatchTime = new IM.Number((int)gameMode.time);
        uiMatch.timerBoard.SetVisible(m_gameMathCountEnable);
        uiMatch.timerBoard.UpdateTime((float)gameMatchTime);
        m_gameMatchCountStop = true;
        if (m_gameMathCountEnable)
        {
            if (m_gameMathCountTimer == null)
            {
                m_gameMathCountTimer = new GameUtils.Timer(gameMatchTime, () => { m_stateMachine.SetState(MatchState.State.eOver); });
            }
            else
            {
                m_gameMathCountTimer.SetTimer(gameMatchTime);
            }
        }
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
			!npc.m_bWithBall && npc.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
		{
			FetchBall(npc);
			firstTime = false;
		}

		/*
		if (blocked && npc.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
			Refresh();
		if (m_mainRole.m_bWithBall && m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eStand)
		{
			UBasketball ball = m_mainRole.m_ball;
			m_mainRole.DropBall(ball);
			mCurScene.DestroyBall(ball);
			Refresh();
		}
		*/

		if (timerRefresh != null)
			timerRefresh.Update(deltaTime);

		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.eOver)
			NGUITools.SetActive(ballConveyor, false);
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
		ball.onShootGoal += OnShootOver;
		ball.onRebound += OnShootOver;
		ball.onDunk += OnDunk;
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
	}

	private void OnShootOver(UBasketball ball)
	{
		if (!blocked)
		{
			myCombo = 0;
			HideCombo();
			HideComboBonus();
		}
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		OnShoot(ball);
		OnShootOver(ball);
	}

	private void OnShoot(UBasketball ball)
	{
		blocked = false;
		HideOpportunity();
	}

	private void OnHitGround(UBasketball ball)
	{
		if (timerRefresh == null)
			timerRefresh = new GameUtils.Timer(npcAttackInterval, OnRefresh);
		timerRefresh.stop = false;
	}

	private void OnGrab(UBasketball ball)
	{
		//block grab ball
		if( m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eBlock )
			OnHitGround(ball);
	}

	private void OnRefresh()
	{
		if( mCurScene.mBall != null )
			mCurScene.DestroyBall(mCurScene.mBall);

		timerRefresh.stop = true;
		blocked = false;
		if (m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			return;
		//ResetPlayerPos();
        List<IM.Transform> npcPos = GameSystem.Instance.MatchPointsConfig.BlockStormPos.npc_transforms;
		ResetNPC(npcPos[Random.Range(0, npcPos.Count - 1)]);
		FetchBall(npc);
	}

	public void OnEvent( PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (sender == m_mainRole && animEvent == PlayerActionEventHandler.AnimEvent.eBlock)
		{
			int score = currBall.m_special ? specialBallScore : normalBallScore;
			++myCombo;
			float bonusRatio = GameSystem.Instance.GameModeConfig.GetComboBonus(GetMatchType(), myCombo);
			m_homeScore += (int)(score * (1 + bonusRatio));
			m_mainRole.mStatistics.success_block_times = (uint)m_homeScore;
			uiMatch.score = m_homeScore;
			if (m_homeScore >= threeStarScore)
			{
				uiMatch.starNum = 3;
				uiMatch.maxScore = threeStarScore;
			}
			else if (m_homeScore >= twoStarScore)
			{
				uiMatch.starNum = 2;
				uiMatch.maxScore = threeStarScore;
			}
			else if (m_homeScore >= oneStarScore)
			{
				uiMatch.starNum = 1;
				uiMatch.maxScore = twoStarScore;
			}
			if (myCombo > 1)
			{
				ShowCombo(myCombo);
				ShowComboBonus(bonusRatio);
			}
			blocked = true;
			HideOpportunity();
		}
	}
}
