using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 球场出现1个点，玩家抢到3个点获得1个球权可以进行投篮。最后分值最高者 //速度与激情玩法
/// </summary>
public class GameMatch_GrabPoint 
	: GameMatch
{
	public IM.Number TRACE_POINT_DELAY;
	public IM.Number POINT_CLOSE_TO_ME_RATE = IM.Number.half;
	public GameObject curPoint;
    public IM.Vector3 curPointPosition;
	public uint mainRolePointNum;
	public uint npcPointNum;
	public Player npc;
	uint totalPointNum;

	GameObject homePointSignal;
    GameObject clientPointSignal;
    public enum ROLE_TYPE{
        MAIN_ROLE,
        NPC_ROLE,
        NONE_ROlE,
    }
	public GameMatch_GrabPoint(Config config)
		: base(config)
	{
		string[] tokens = gameMode.extraLevelInfo.Split('&');
		TRACE_POINT_DELAY = IM.Number.Parse(tokens[0]);
		if (tokens.Length > 1)
			POINT_CLOSE_TO_ME_RATE = IM.Number.Parse(tokens[1]);

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
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
		mainRole.m_alwaysForbiddenPickup = true;

		//npc
        Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		
		npc.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(npc.m_StateMachine, this));
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
		npc.m_team.m_role = GameMatch.MatchRole.eDefense;
        npc.operMode = Player.OperMode.AI;

		npc.m_alwaysForbiddenPickup = true;

		_UpdateCamera(mainRole);

		mCurScene.mBasket.onGoal += OnGoal;
	}

    public override AISystem CreateAISystem(Player player)
    {
		return new AISystem_GrabPoint(this, player, AIState.Type.eGrabPoint_Init, m_config.NPCs[0].AIID);
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
		base.CreateUI();
		CreateSpecialGUI();
	}

	private void CreateSpecialGUI()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PointSignal") as GameObject;
        homePointSignal = CommonFunction.InstantiateObject(prefab, m_uiMatch.transform.FindChild("LeftSpeed").transform);
        clientPointSignal = CommonFunction.InstantiateObject(prefab, m_uiMatch.transform.FindChild("RightSpeed").transform);
	}

	private void UpdateSignal(GameObject go, uint pointNum)
	{
		for (int i = 0; i < 3; ++i)
		{
			go.transform.GetChild(0).GetChild(i).GetComponent<UISprite>().spriteName =
                (i < pointNum) ? "gameInterface_ozd_ball-g" : "gameInterface_ozd_ball-gray";
		}
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

	public override bool EnablePlayerTips()
	{
		return false;
	}

	public override int GetScore(int score)
	{
		return 4;
	}

	public override void ResetPlayerPos()
	{
        //TODO 针对PVP修改
        mainRole.position = GameSystem.Instance.MatchPointsConfig.GrabPointPos.mainRole_transform.position;
		mainRole.forward = IM.Vector3.forward;
        npc.position = GameSystem.Instance.MatchPointsConfig.GrabPointPos.npc_transform.position;
		npc.forward = IM.Vector3.forward;
	}

	public override void ConstrainMovementOnBegin(IM.Number fCurTime)
	{
		if (m_ruler.prepareTime < fCurTime)
		{
			return;
		}

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
	}

    public override void ViewUpdate()
    {
        base.ViewUpdate();
        if (m_bTimeUp)
            npc.m_aiMgr.m_enable = false;

        if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying &&
            Camera.main != null)
        {
            if (curPoint == null)
            {
                curPoint = CreatePoint();
                curPoint.SetActive(true);
            }

            if (curPoint != null)
            {
                // if the point is generated at the player's position
                //TODO：之前在SphereCollider取得半径，后面获取值的方式要改，暂时定为常量
                    //TODO 针对PVP修改
                //float radius = curPoint.GetComponent<SphereCollider>().radius;
                if (GameUtils.HorizonalDistance(curPointPosition, mainRole.position) < mainRole.m_Radius && mainRolePointNum<3)
                {
                    if (OnGrabPoint(ROLE_TYPE.MAIN_ROLE))
                    {
                        Object.Destroy(curPoint);
                        curPoint = null;
                    }
                }
                else if (GameUtils.HorizonalDistance(curPointPosition, npc.position) < npc.m_Radius && npcPointNum < 3)
                {
                    if (OnGrabPoint(ROLE_TYPE.NPC_ROLE))
                    {
                        Object.Destroy(curPoint);
                        curPoint = null;
                    }
                }
            }
        }

        if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.eOver)
            HideCanShoot();
    }

	private GameObject CreatePoint()
	{
		GameObject point = new GameObject("Point");
        if (totalPointNum++ == 0)
        {
            curPointPosition = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position;
            point.transform.localPosition = (Vector3)curPointPosition;
            //GameObject freeThrowCenter = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/GrabPoint_Pos") as GameObject;
            //Transform npc = freeThrowCenter.transform.FindChild("NPC");
            //point.transform.localPosition = npc.position;
        }
        else
        {
            curPointPosition = GeneratePointPosition();
            point.transform.localPosition = (Vector3)curPointPosition;
        }
		
		GameObject arrow = Object.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/Position")) as GameObject;
		arrow.transform.parent = point.transform;
		arrow.transform.localPosition = new Vector3(0f, 0.01f, 0f);
        //SphereCollider collider = point.AddComponent<SphereCollider>();
        //collider.isTrigger = true;
        //collider.radius = 0.7f;
        //SceneTrigger trigger = point.AddComponent<SceneTrigger>();
        //trigger.onTrigger += OnGrabPoint;
        //trigger.oneShot = false;
		return point;
	}

	private IM.Vector3 GeneratePointPosition()
	{
        //TODO 针对PVP修改
        IM.Number mainRoleX = mainRole.position.x;
		IM.Number npcX = npc.position.x;
		IM.Number selectedX = IM.Random.value <= POINT_CLOSE_TO_ME_RATE ? mainRoleX : npcX;
		IM.Number xCenter = (mainRoleX + npcX) / 2;
		IM.Vector3 pos;
		do
		{
			pos = GenerateIn3PTPosition();
		}
		while ((pos.x < xCenter && selectedX > xCenter) || (pos.x > xCenter && selectedX < xCenter));
		return pos;
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		OnGoal(ball);
	}

	private void OnGoal(UBasketball ball)
	{
		int score = GetScore(ball.m_pt);
		if (ball.m_actor == mainRole)
		{
			m_homeScore += score;
			Debug.Log("Main role score: " + m_homeScore);
		}
		else if (ball.m_actor == npc)
		{
			m_awayScore += score;
			Debug.Log("NPC score: " + m_awayScore);
		}
	}

	private bool OnGrabPoint(ROLE_TYPE roleType)
	{
		if (roleType == ROLE_TYPE.MAIN_ROLE)
		{
			if (mainRolePointNum < 3)
			{
				++mainRolePointNum;
				UpdateSignal(homePointSignal, mainRolePointNum);
				if (mainRolePointNum >= 3)
				{
					GainBall(mainRole);
					ShowCanShoot();
				}
			}
			++m_homeScore;
			return true;
		}
		else if (roleType == ROLE_TYPE.NPC_ROLE)
		{
			if (npcPointNum < 3)
			{
				++npcPointNum;
				UpdateSignal(clientPointSignal, npcPointNum);
				if (npcPointNum >= 3)
				{
					GainBall(npc);
				}
			}
			++m_awayScore;
			return true;
		}
		else
			return false;
	}

	private void GainBall(Player player)
	{
		player.GrabBall(mCurScene.CreateBall());
		player.m_StateMachine.SetState(PlayerState.State.eStand);
		player.m_ball.onHitGround += OnBallHitGround;
		player.m_ball.onShoot += OnBallShoot;
		player.m_ball.onDunk += OnDunk;
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		OnBallShoot(ball);
	}

	private void OnBallShoot(UBasketball ball)
	{
		if (ball.m_actor == mainRole)
		{
			mainRolePointNum = 0;
			UpdateSignal(homePointSignal, mainRolePointNum);
			HideCanShoot();
		}
		else if (ball.m_actor == npc)
		{
			npcPointNum = 0;
			UpdateSignal(clientPointSignal, npcPointNum);
		}
	}

	private void OnBallHitGround(UBasketball ball)
	{
		if (mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eFallLostBall)
		{
			mainRolePointNum = 0;
			UpdateSignal(homePointSignal, mainRolePointNum);
			HideCanShoot();
		}
		mCurScene.DestroyBall(ball);
	}
}
