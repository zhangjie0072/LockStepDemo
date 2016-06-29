using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 球场上出现一堆球，捡到球就可以进行投篮。以分值决定胜负。
/// </summary>
public class GameMatch_MassBall : GameMatch
{
	struct RefreshInfo
	{
		public IM.Number interval;
		public uint special;
		public uint normal;
	}
	List<RefreshInfo> refresh_infos = new List<RefreshInfo>();

	public IM.Number MAX_SPECIAL_BALL_DIST;
	public IM.Number SPECIAL_BALL_DIST_DIFF_THRESHOLD;
	public Player npc;
	private GameUtils.Timer timer;
	private int refreshTimeNum;

    private Config _config;

	public GameMatch_MassBall(Config config)
		: base(config)
	{
        _config = config;
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_MASSBALLREFRESHTIME, ReadRefreshInfo);
		//ReadRefreshInfo();		
	}

	void ReadRefreshInfo(string vPath, object obj)
    {
        refresh_infos.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MASSBALLREFRESHTIME, obj);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			RefreshInfo info;
			info.interval = IM.Number.Parse(line.SelectSingleNode("interval").InnerText);
			info.special = uint.Parse(line.SelectSingleNode("special_num").InnerText);
			info.normal = uint.Parse(line.SelectSingleNode("normal_num").InnerText);
			refresh_infos.Add(info);
		}

        string[] tokens = gameMode.extraLevelInfo.Split('&');
        MAX_SPECIAL_BALL_DIST = IM.Number.Parse(tokens[0]);
        SPECIAL_BALL_DIST_DIFF_THRESHOLD = IM.Number.Parse(tokens[1]);

        GameSystem.Instance.mNetworkManager.ConnectToGS(_config.type, "", 1);
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();

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
        mainRole.operMode = Player.OperMode.Input;
		mainRole.m_catchHelper = new CatchHelper(mainRole);
		mainRole.m_catchHelper.ExtractBallLocomotion();
		mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
		mainRole.m_alwaysForbiddenPickup = false;

		//npc
		Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		npc.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(npc.m_StateMachine, this));
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
        npc.operMode = Player.OperMode.AI;
		npc.m_team.m_role = GameMatch.MatchRole.eDefense;

		npc.m_alwaysForbiddenPickup = false;

		mCurScene.mBasket.onGoal += OnGoal;

	}

    public override AISystem CreateAISystem(Player player)
    {
		return new AISystem_MassBall(this, player, AIState.Type.eMassBall_Init, m_config.NPCs[0].AIID);
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

	public override bool EnablePlayerTips()
	{
		return false;
	}

	public override bool EnableSwitchRole()
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
        MassBallPos massBallPos = GameSystem.Instance.MatchPointsConfig.MassBallPos;
        mainRole.position = massBallPos.mainRole_transform.position;
		mainRole.forward = IM.Vector3.forward;
        npc.position = massBallPos.npc_transform.position;
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
			return !m_bTimeUp && (command == Command.Shoot || command == Command.Layup || command == Command.Dunk || command == Command.Rush);
		else
			return base.IsCommandValid(command);
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (m_bTimeUp)
			npc.m_aiMgr.m_enable = false;

		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying && refreshTimeNum == 0)
		{
			timer = new GameUtils.Timer(refresh_infos[0].interval, RefreshBalls);
		}

		//If there is no ball in the scene, or the only ball is hold by main player.
        //TODO 针对PVP修改
		if ((mCurScene.balls.Count == 0 || (mCurScene.balls.Count == 1 && mainRole.m_bWithBall)) &&
			refreshTimeNum == refresh_infos.Count)
		{
			refreshTimeNum = 0;
			RefreshBalls();
		}

		if (timer != null)
            timer.Update(deltaTime);
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
        //TODO 针对PVP修改
		int score = (ball.m_special ? 2 : 1);
		if (ball.m_actor == mainRole)
		{
			m_homeScore += score;
			//Debug.Log("Main role score: " + m_homeScore);
		}
		else if (ball.m_actor == npc)
		{
			m_awayScore += score;
			//Debug.Log("NPC score: " + m_awayScore);
		}
        //碰地删除
		ball.onHitGround += (UBasketball b) => { mCurScene.DestroyBall(b); };
		ball.m_pickable = false;
	}

	private void RefreshBalls()
	{
		for (uint i = 0; i < refresh_infos[refreshTimeNum].normal; ++i)
		{
			UBasketball ball = mCurScene.CreateBall();
            IM.Vector3 tempPos = GenerateIn3PTPosition();
            tempPos.y = ball.m_ballRadius;
			ball.SetInitPos(tempPos);
			ball.m_ballState = BallState.eLoseBall;
		}
		for (uint i = 0; i < refresh_infos[refreshTimeNum].special; ++i)
		{
			UBasketball ball = mCurScene.CreateBall();
            IM.Vector3 tempPos = GenerateIn3PTPosition();
            tempPos.y = ball.m_ballRadius;
			ball.SetInitPos(tempPos);
			ball.m_special = true;
			ball.onGrab += OnGrab;
			ball.m_ballState = BallState.eLoseBall;
		}
		++refreshTimeNum;
		if (refreshTimeNum < 3)
		{
			timer.SetTimer(refresh_infos[refreshTimeNum].interval);
			timer.stop = false;
		}
		else
			timer.stop = true;
	}

	private void OnGrab(UBasketball ball)
	{
        //TODO 针对PVP修改
		if (ball.m_owner == mainRole)
		{
			ShowOpportunity();
			ball.onShoot += OnShoot;
			ball.onDunk += OnDunk;
		}
	}

	private void OnShoot(UBasketball ball)
	{
		ball.onShoot -= OnShoot;
		ball.onDunk -= OnDunk;
		HideOpportunity();
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		OnShoot(ball);
	}
}
