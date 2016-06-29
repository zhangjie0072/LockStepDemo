using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 1个进攻者，2个防守者 【目前没有这种比赛模式】
/// </summary>
public class GameMatch_Ultimate21 : GameMatch
{
	private IM.Number OFFENSE_TIME_LIMIT;
	private int WIN_SCORE;
	public Player[] players = new Player[3];
	public SlotMachine slotMachine;
	private GameObject scoreBoardPanel;
	private GameObject[] scoreBoards = new GameObject[3];
	private int[] scores = new int[3];
	private List<Player> orderedPlayers = new List<Player>();
	private GameUtils.Timer timerOver;
	private TimerBoard timerBoard;

	private Player attacker
	{
		get
		{
			if (orderedPlayers.Count > 0)
				return orderedPlayers[0];
			else
				return null;
		}
	}
	private GameUtils.Timer timerSwitchRole;
	private bool switchRoleOnShootOver;
	private GameUtils.Timer timerSwitchRoleDelay;

	public GameMatch_Ultimate21(Config config)
		: base(config)
	{
		string[] tokens = gameMode.additionalInfo.Split('&');
		OFFENSE_TIME_LIMIT = IM.Number.Parse(tokens[0]);
		WIN_SCORE = int.Parse(tokens[1]);

		timerSwitchRole = new GameUtils.Timer(OFFENSE_TIME_LIMIT, OnTimerSwitchRole);
		timerSwitchRole.stop = true;

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
		
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		mCurScene.CreateBall();
		mCurScene.mBall.onShootGoal += OnGoal;
		mCurScene.mBall.onDunk += OnDunk;
		mCurScene.mBall.onGrab += OnGrab;

		if (m_config == null)
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}

        //TODO 针对PVP修改
		//main role
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		players[0] = mainRole;
        mainRole.operMode = Player.OperMode.Input;
		mainRole.m_catchHelper = new CatchHelper(mainRole);
		mainRole.m_catchHelper.ExtractBallLocomotion();
		mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		mainRole.m_team.m_role = GameMatch.MatchRole.eDefense;
		mainRole.m_alwaysForbiddenPickup = false;

		//npc1
		Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		Player npc = oppoTeam.GetMember(0);
		players[1] = npc;
		if (npc.model != null)
			npc.model.EnableGrey();
        npc.operMode = Player.OperMode.AI;
		npc.m_team.m_role = GameMatch.MatchRole.eOffense;
		npc.m_alwaysForbiddenPickup = false;

		//npc2
		npc = oppoTeam.GetMember(1);
		players[2] = npc;
		if (npc.model != null)
			npc.model.EnableGrey();
        npc.operMode = Player.OperMode.AI;
		npc.m_team.m_role = GameMatch.MatchRole.eOffense;
		npc.m_alwaysForbiddenPickup = false;
		_UpdateCamera(mainRole);
		CreateUI ();


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
		CreateSlotMachine();
	}

	public override bool TimmingOnStarting()
	{
		return true;
	}

	public override bool EnableGoalState()
	{
		return true;
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
		return true;
	}

	public override bool IsFinalTime(IM.Number seconds)
	{
		return timerSwitchRole.Remaining() < seconds;
	}

	public override void ResetPlayerPos()
	{
        TwoDefenderPos twoDefenderPos = GameSystem.Instance.MatchPointsConfig.TwoDefenderPos;
        orderedPlayers[0].position = twoDefenderPos.attacker_transform.position;
		orderedPlayers[0].forward = IM.Vector3.forward;
        orderedPlayers[1].position = twoDefenderPos.defense0_transform.position;
		orderedPlayers[1].forward = -IM.Vector3.forward;
        orderedPlayers[2].position = twoDefenderPos.defense1_transform.position;
		orderedPlayers[2].forward = -IM.Vector3.forward;
	}

	public void SetStartingAttacker(int index)
	{
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
			player.Show( m_config.leagueType != LeagueType.ePVP );

		for (int i = index; i < 3; ++i)
		{
			orderedPlayers.Add(players[i]);
		}
		for (int i = 0; i < index; ++i)
		{
			orderedPlayers.Add(players[i]);
		}

		AllocatePlayers();

		timerSwitchRole.stop = false;
	}

	public override void ConstrainMovementOnBegin(IM.Number fCurTime)
	{
		if (m_ruler.prepareTime < fCurTime)
			return;

		ResetPlayerPos();
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
            timerSwitchRole.Update(deltaTime);
		if (timerOver != null)
			timerOver.Update(deltaTime);
		if (timerSwitchRoleDelay != null)
			timerSwitchRoleDelay.Update(deltaTime);
		if (timerBoard != null)
        {
            gameMatchTime = timerSwitchRole.Remaining();
        }

		if (switchRoleOnShootOver && mCurScene.mBall.m_ballState == BallState.eLoseBall)
		{
			SwitchRole();
			timerSwitchRole.SetTimer(OFFENSE_TIME_LIMIT);
			timerSwitchRole.stop = false;
			switchRoleOnShootOver = false;
		}
	}

	protected override void CreateCustomGUI()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Ultimate21ScoreBoard") as GameObject;
		scoreBoardPanel = CommonFunction.InstantiateObject(prefab, GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform);
		UIGrid boardGrid = scoreBoardPanel.transform.FindChild("Pane/ScoreBoards").GetComponent<UIGrid>();
		scoreBoards[0] = boardGrid.GetChild(0).gameObject;
		scoreBoards[1] = CommonFunction.InstantiateObject(scoreBoards[0], boardGrid.transform);
		scoreBoards[1].name = "1";
		scoreBoards[2] = CommonFunction.InstantiateObject(scoreBoards[0], boardGrid.transform);
		scoreBoards[2].name = "2";
		boardGrid.Reposition();
		Transform timerNode = scoreBoardPanel.transform.FindChild("Pane/TimerBoard");
		prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TimerBoard") as GameObject;
		timerBoard = CommonFunction.InstantiateObject(prefab, timerNode).GetComponent<TimerBoard>();

		for (int i = 0; i < 3; ++i)
		{
			scoreBoards[i].transform.FindChild("Name").GetComponent<UILabel>().text = players[i].m_name;
		}

		prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
		GameObject backButton = CommonFunction.InstantiateObject(prefab, scoreBoardPanel.transform);
		UIEventListener.Get(backButton).onClick = OnBack;
	}

    private void OnBack(GameObject go)
    {
        GameSystem.Instance.mClient.pause = true;
		CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("MATCH_TIPS_EXIT_MATCH"), scoreBoardPanel.transform, OnConfirmBack, OnCancelBack);
    }

    private void OnConfirmBack(GameObject go)
    {
		if (!GameSystem.Instance.mNetworkManager.connPlat)
		{
			PlatNetwork.Instance.onReconnected += ExitMatch;
			GameSystem.Instance.mNetworkManager.autoReconnInMatch = true;
			GameSystem.Instance.mNetworkManager.Reconnect();
			return;
		}
		ExitMatch();
	}

	private void ExitMatch()
    {
        GameSystem.Instance.mClient.pause = false;
		if (GlobalConst.IS_NETWORKING)
		{
            //ExitGameReq req = new ExitGameReq();
            //req.session_id = m_config.session_id;
            //GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            EndSectionMatch career = new EndSectionMatch();
            career.session_id = m_config.session_id;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_CAREER;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.career = career;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            GameSystem.Instance.mClient.mUIManager.curLeagueType = leagueType;
		}
		else
		{
			GameSystem.Instance.mClient.Reset();
            GameSystem.Instance.mClient.mUIManager.curLeagueType = leagueType;
            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
		}
		m_stateMachine.m_curState.OnExit();
    }

    private void OnCancelBack(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
    }

	private void CreateSlotMachine()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/SlotMachine") as GameObject;
		GameObject obj = CommonFunction.InstantiateObject(prefab, GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform);
		slotMachine = obj.GetComponent<SlotMachine>();
		NGUITools.SetActive(obj, false);
	}

	private void OnTimerSwitchRole()
	{
		if( mCurScene.mBall.m_ballState == BallState.eLoseBall ||
			mCurScene.mBall.m_ballState == BallState.eUseBall)
		{
			SwitchRole();
		}
		else
		{
			switchRoleOnShootOver = true;
			timerSwitchRole.SetTimer(IM.Number.zero);
			timerSwitchRole.stop = true;
		}
	}

	private void SwitchRole()
	{
		foreach (Player player in players)
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_enablePickupDetector = false;
		}

		if (timerSwitchRoleDelay == null)
		{
			timerSwitchRoleDelay = new GameUtils.Timer(IM.Number.one, () =>
			{
				timerSwitchRoleDelay.stop = true;
				HideAnimTip("gameInterface_text_ChangeBall");

				Player attacker = orderedPlayers[0];
				orderedPlayers.RemoveAt(0);
				orderedPlayers.Add(attacker);

				AllocatePlayers();
				m_stateMachine.SetState(MatchState.State.eBegin);
			});
		}

		timerSwitchRoleDelay.stop = false;

		ShowAnimTip("gameInterface_text_ChangeBall");
	}

	private void AllocatePlayers()
	{
		bool mainRoleIsAttacker = (attacker == mainRole);
		m_homeTeam.m_role = mainRoleIsAttacker ? MatchRole.eOffense : MatchRole.eDefense;
		m_awayTeam.m_role = mainRoleIsAttacker ? MatchRole.eDefense : MatchRole.eOffense;
		m_homeTeam.Clear();
		m_awayTeam.Clear();

		m_homeTeam.AddMember(players[0]);
		foreach (Player player in players)
		{
			if (player == mainRole)
				continue;
			if (mainRoleIsAttacker || player == attacker)
			{
				player.m_team = m_awayTeam;
				m_awayTeam.AddMember(player);
			}
			else
			{
				player.m_team = m_homeTeam;
				m_homeTeam.AddMember(player);
			}
		}

		foreach (Player player in m_homeTeam.members)
		{
			player.m_defenseTarget = m_awayTeam.GetMember(0);
			if (player.m_AOD == null)
				player.m_AOD = new AOD(player);
		}
		foreach (Player player in m_awayTeam.members)
		{
			player.m_defenseTarget = m_homeTeam.GetMember(0);
			if (player.m_AOD == null)
				player.m_AOD = new AOD(player);
		}

		if (mCurScene.mBall.m_owner != null && mCurScene.mBall.m_owner != attacker)
		{
			Player ballOwner = mCurScene.mBall.m_owner;
			mCurScene.mBall.m_owner.DropBall(mCurScene.mBall);
			ballOwner.m_StateMachine.SetState(PlayerState.State.eStand);
		}
		if (mCurScene.mBall.m_owner != attacker)
			attacker.GrabBall(mCurScene.mBall);

		if (level == Level.Easy)
		{
			if (mainRoleIsAttacker)
			{
				int[] insideDefensePriority = { 0, 2, 3, 1, 5, 4};
				int priority1 = insideDefensePriority[(int)players[1].m_position];
				int priority2 = insideDefensePriority[(int)players[2].m_position];
				if (priority1 < priority2)
				{
					(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Inside;
					(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
				}
				else
				{
					(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
					(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Inside;
				}
			}
			else
			{
				if (attacker == players[1])
				{
					(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
					(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Inside;
				}
				else
				{
					(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Inside;
					(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
				}
			}
		}
		else
		{
			if (mainRoleIsAttacker)
			{
				(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Left;
				(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Right;
			}
			else
			{
				(players[1].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
				(players[2].m_aiMgr.GetState(AIState.Type.eDefense) as AI_Defense).m_defensePosition = AI_Defense.DefensePosition.Center;
			}
		}

		for (int i = 0; i < 3; ++i)
		{
			NGUITools.SetActive(scoreBoards[i].transform.FindChild("Ball").gameObject, players[i] == attacker);
		}
	}

	private void OnGoal(UBasketball ball)
	{
		bool matchOver = false;

		int score = ball.m_pt;
		for (int i = 0; i < 3; ++i)
		{
			if (players[i] == ball.m_actor)
			{
				scores[i] += score;
				m_homeScore = scores[0];
				m_awayScore = scores[1] > scores[2] ? scores[1] : scores[2];
				UISprite tensDigit = scoreBoards[i].transform.FindChild("Tens").GetComponent<UISprite>();
				UISprite unitsDigit = scoreBoards[i].transform.FindChild("Units").GetComponent<UISprite>();
				tensDigit.spriteName = "gameInterface_figure_black" + (scores[i] / 10).ToString();
				unitsDigit.spriteName = "gameInterface_figure_black" + (scores[i] % 10).ToString();
				timerSwitchRole.SetTimer(OFFENSE_TIME_LIMIT);
				if (scores[i] >= WIN_SCORE)
					matchOver = true;
				break;
			}
		}

		if (matchOver)
		{
			foreach (Player player in players)
				player.m_alwaysForbiddenPickup = true;
			if (timerOver == null)
				timerOver = new GameUtils.Timer(IM.Number.two, () => { m_stateMachine.SetState(MatchState.State.eOver);} , 1);
			timerOver.stop = true;
			ball.onHitGround += (GameObject) => { timerOver.stop = false; };
		}
		else
		{
			foreach (Player player in players)
				player.m_alwaysForbiddenPickup = true;
			ball.onHitGround += OnHitGround;
		}
	}

	private void OnHitGround(UBasketball ball)
	{
		ball.onHitGround -= OnHitGround;
		foreach (Player player in players)
			player.m_alwaysForbiddenPickup = false;
		attacker.GrabBall(mCurScene.mBall);
		m_stateMachine.SetState(MatchState.State.eBegin);
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		if (goal)
			OnGoal(ball);
	}

	private void OnGrab(UBasketball ball)
	{
		if (attacker != null && ball.m_owner != attacker)
		{
			Player lastAttacker = attacker;
			orderedPlayers.RemoveAt(0);
			orderedPlayers.Remove(ball.m_owner);
			orderedPlayers.Insert(0, ball.m_owner);
			orderedPlayers.Add(lastAttacker);
			timerSwitchRole.SetTimer(OFFENSE_TIME_LIMIT);
			AllocatePlayers();
			m_ruler.m_toCheckBallTeam = attacker.m_team;
			foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
				player.m_enableAction = true;
		}
	}
}
