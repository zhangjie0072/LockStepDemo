using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 球场出现1个点，玩家抢到3个点获得1个球权可以进行投篮。最后分值最高者获胜。
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

    

	public GameMatch_GrabPoint(Config config)
		: base(config)
	{
		string[] tokens = gameMode.extraLevelInfo.Split('&');
		TRACE_POINT_DELAY = IM.Number.Parse(tokens[0]);
		if (tokens.Length > 1)
			POINT_CLOSE_TO_ME_RATE = IM.Number.Parse(tokens[1]);

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

		//main role
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		m_mainRole = pm.GetPlayerById( uint.Parse(m_config.MainRole.id) );
		m_mainRole.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(m_mainRole.m_StateMachine, this));
		m_mainRole.m_inputDispatcher = new InputDispatcher(this, m_mainRole);
		m_mainRole.m_catchHelper = new CatchHelper(m_mainRole);
		m_mainRole.m_catchHelper.ExtractBallLocomotion();
		m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand, true);
		m_mainRole.m_InfoVisualizer.CreateStrengthBar();
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
		m_mainRole.m_alwaysForbiddenPickup = true;

		//npc
        Team oppoTeam = m_mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		
		npc.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(npc.m_StateMachine, this));
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
		npc.m_aiMgr = new AISystem_GrabPoint(this, npc, AIState.Type.eGrabPoint_Init, m_config.NPCs[0].AIID);
		npc.m_team.m_role = GameMatch.MatchRole.eDefense;

		npc.m_alwaysForbiddenPickup = true;

		_UpdateCamera(m_mainRole);

		m_stateMachine.SetState(m_config.needPlayPlot ? MatchState.State.ePlotBegin : MatchState.State.eShowRule);

		mCurScene.mBasket.onGoal += OnGoal;
	}

    public override void HandleGameBegin(Pack pack)
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
        m_mainRole.position = GameSystem.Instance.MatchPointsConfig.GrabPointPos.mainRole_transform.position;
		m_mainRole.forward = IM.Vector3.forward;
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

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		if (m_bTimeUp)
			npc.m_aiMgr.m_enable = false;

		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying &&
			Camera.main != null)
		{
			if (curPoint == null)
			{
				curPoint = CreatePoint();
				if (curPoint != null)
				{
					curPoint.SetActive(true);
					// if the point is generated at the player's position
                    //TODO：之前在SphereCollider取得半径，后面获取值的方式要改，暂时定为常量
                    //float radius = curPoint.GetComponent<SphereCollider>().radius;
                    IM.Number radius = new IM.Number(0,700);
					if (GameUtils.HorizonalDistance(curPointPosition, m_mainRole.position) < radius)
					{
						if (OnGrabPoint(curPoint.gameObject, m_mainRole.gameObject.GetComponent<Collider>()))
							Object.Destroy(curPoint);
					}
					else if (GameUtils.HorizonalDistance(curPointPosition, npc.position) < radius)
					{
						if (OnGrabPoint(curPoint.gameObject, npc.gameObject.GetComponent<Collider>()))
							Object.Destroy(curPoint);
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
            point.transform.localPosition = (Vector3)GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position;
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
		SphereCollider collider = point.AddComponent<SphereCollider>();
		collider.isTrigger = true;
		collider.radius = 0.7f;
		SceneTrigger trigger = point.AddComponent<SceneTrigger>();
		trigger.onTrigger += OnGrabPoint;
		trigger.oneShot = false;
		return point;
	}

	private IM.Vector3 GeneratePointPosition()
	{
        IM.Number mainRoleX = m_mainRole.position.x;
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
		if (ball.m_actor == m_mainRole)
		{
			m_homeScore += score;
			Logger.Log("Main role score: " + m_homeScore);
		}
		else if (ball.m_actor == npc)
		{
			m_awayScore += score;
			Logger.Log("NPC score: " + m_awayScore);
		}
	}

	private bool OnGrabPoint(GameObject source, Collider collider)
	{
		if (collider.gameObject == m_mainRole.gameObject)
		{
			if (mainRolePointNum < 3)
			{
				++mainRolePointNum;
				UpdateSignal(homePointSignal, mainRolePointNum);
				if (mainRolePointNum >= 3)
				{
					GainBall(m_mainRole);
					ShowCanShoot();
				}
			}
			++m_homeScore;
			return true;
		}
		else if (collider.gameObject == npc.gameObject)
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
		if (ball.m_actor == m_mainRole)
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
		if (m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eFallLostBall)
		{
			mainRolePointNum = 0;
			UpdateSignal(homePointSignal, mainRolePointNum);
			HideCanShoot();
		}
		mCurScene.DestroyBall(ball);
	}
}
