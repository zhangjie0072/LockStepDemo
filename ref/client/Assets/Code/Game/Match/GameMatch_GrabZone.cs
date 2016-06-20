using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

/// <summary>
/// 抢区域，在规定的区域内投篮。区域归属自己，根据区域的数量决定胜负。
/// </summary>
public class GrabPointConstrain
	: ZoneConstrain
{
	private GameMatch_GrabZone	m_match;

	public GrabPointConstrain(GameMatch_GrabZone match)
	{
		m_match = match;
	}

	public void Constraint(ref IM.Vector3 pos)
	{
		IM.Number limitDist = m_match.RADIAL_FAR;
        IM.Vector3 basketCenter = m_match.basketCenter;
        IM.Vector3 dir = pos - basketCenter;
		if (dir.magnitude <= limitDist)
			return;
		dir.Normalize();
        pos = basketCenter + dir * limitDist;
	}
}

public class GameMatch_GrabZone : GameMatch
{
    private static IM.Number BASKET_DIST = new IM.Number(12, 800);
    private static IM.Number HORI_NEAR = new IM.Number(3, 700);
    private static IM.Number HORI_MIDDLE = new IM.Number(6);
    private static IM.Number HORI_FAR = new IM.Number(8, 500);
    private static IM.Number RADIAL_NEAR = BASKET_DIST - new IM.Number(8, 750);
    private static IM.Number RADIAL_MIDDLE = BASKET_DIST - new IM.Number(6, 200);
    public IM.Number RADIAL_FAR = BASKET_DIST - new IM.Number(3, 650);
    private static IM.Number ANGLE_SEG = new IM.Number(44, 63);
    private static IM.Number ANGLE_SEG_SIDE = new IM.Number(22, 55);
    public static int ZONE_COUNT = 11;

    public IM.Number ZONE_ARRIVE_TIME_DIFF_THRESHOLD;
	public IM.Vector3 basketCenter { get; private set; }

	public Player npc;
	private GameUtils.Timer timer;
	private GameObject zoneIndicator;
	public IM.Vector3[] zonePosition { get; private set; }
	private MeshRenderer[] zoneRenderer = new MeshRenderer[ZONE_COUNT + 1];
	private MeshRenderer currZoneRenderer;
	private Material matSelf;
	private Material matRival;

	private Dictionary<UBasketball, int> shootZones = new Dictionary<UBasketball, int>();
	public int[] zoneOwnership { get; private set; }

	public GameMatch_GrabZone(Config config)
		: base(config)
	{
		ZONE_ARRIVE_TIME_DIFF_THRESHOLD = IM.Number.Parse(gameMode.extraLevelInfo);
		zoneOwnership = new int[ZONE_COUNT + 1];
        basketCenter = new IM.Vector3(IM.Number.zero, IM.Number.zero, BASKET_DIST);

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
		m_mainRole.m_alwaysForbiddenPickup = false;

		//m_mainRole.m_aiMgr = new AISystem_Basic(this, m_mainRole, AIState.Type.eGrabZone_Init);

		//npc
		Team oppoTeam = m_mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
		npc = oppoTeam.GetMember(0);
		npc.m_StateMachine.ReplaceState(new PlayerState_Stand_Simple(npc.m_StateMachine, this));
		npc.m_StateMachine.ReplaceState(new PlayerState_Knocked_NoHold(npc.m_StateMachine, this));
		if (npc.model != null)
			npc.model.EnableGrey();
		npc.m_aiMgr = new AISystem_GrabZone(this, npc, AIState.Type.eGrabZone_Init, m_config.NPCs[0].AIID);
		npc.m_team.m_role = GameMatch.MatchRole.eDefense;

		npc.m_alwaysForbiddenPickup = false;

		_UpdateCamera(m_mainRole);


		CreateZoneIndicator();

		mCurScene.mGround.AddZoneConstrain( new GrabPointConstrain(this) );
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
        GrabZonePos grabZonePos = GameSystem.Instance.MatchPointsConfig.GrabZonePos;
        m_mainRole.position = grabZonePos.mainRole_transform.position;
		m_mainRole.forward = IM.Vector3.forward;
        npc.position = grabZonePos.npc_transform.position;
		npc.forward = IM.Vector3.forward;

		if (mCurScene.balls.Count == 0)
		{
			for (int i = 0; i < 5; ++i)
			{
				UBasketball ball = mCurScene.CreateBall();
				ball.onShoot += OnShoot;
				ball.onShootGoal += OnGoal;
				ball.onDunk += OnDunk;
			}
		}
		for (int i = 0; i < 5; ++i)
		{
			UBasketball ball = mCurScene.balls[i];
			if (ball.m_owner != null)
			{
				ball.m_owner.DropBall(ball);
			}
			ball.SetInitPos(grabZonePos.balls_transform[i].position);
			ball.m_ballState = BallState.eLoseBall;
		}

		zonePosition = new IM.Vector3[ZONE_COUNT + 1];
		for (int i = 0; i < ZONE_COUNT; ++i)
		{
            zonePosition[i] = grabZonePos.zones_transform[i].position;
		}
	}

	public override void ConstrainMovementOnBegin(IM.Number fCurTime)
	{
		if (m_ruler.prepareTime < fCurTime)
			return;
        GrabZonePos grabZonePos = GameSystem.Instance.MatchPointsConfig.GrabZonePos;
        m_mainRole.position = grabZonePos.mainRole_transform.position;
		m_mainRole.forward = IM.Vector3.forward;
        npc.position = grabZonePos.npc_transform.position;
		npc.forward = IM.Vector3.forward;
	}

	public override bool IsCommandValid(Command command)
	{
		if (m_stateMachine.m_curState != null && m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
			return !m_bTimeUp && 
				(command == Command.Shoot ||
				command == Command.Layup ||
				command == Command.Dunk ||
				command == Command.Rush);
		else
			return base.IsCommandValid(command);
	}

	/*
	public override void FixedUpdate ()
	{
		if( m_mainRole == null || npc == null )
			return;

		if( mCurScene == null )
			return;

		Vector3 curPos = m_mainRole.position;
		mCurScene.mGround.BoundInZone(ref curPos);
		m_mainRole.position = curPos;
		
		curPos = npc.position;
		mCurScene.mGround.BoundInZone(ref curPos);
		npc.position = curPos;
	}
	*/

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		if (m_bTimeUp)
			npc.m_aiMgr.m_enable = false;

		//LimitPlayer(m_mainRole);
		//LimitPlayer(npc);

		//foreach (UBasketball ball in mCurScene.balls)
		//	LimitBall(ball);

		if (timer != null)
            timer.Update(deltaTime);
	}

	/*
	void LimitPlayer(Player player)
	{
		if (m_mainRole == null || m_mainRole.m_goPlayer == null)
			return;

		CapsuleCollider collider = player.m_goPlayer.GetComponent<CapsuleCollider>();
		float limitDist = RADIAL_FAR - collider.radius;
		Vector3 dir = player.position - basketCenter;
		if (dir.magnitude <= limitDist)
			return;

		dir.Normalize();
		player.position = basketCenter + dir * limitDist;
	}
	*/

	private void CreateZoneIndicator()
	{
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/Zone") as GameObject;
		zoneIndicator = Object.Instantiate(prefab) as GameObject;
		for (int i = 1; i <= ZONE_COUNT; ++i)
		{
			MeshRenderer renderer = zoneIndicator.transform.FindChild(i.ToString()).GetComponent<MeshRenderer>();
			zoneRenderer[i] = renderer;
			renderer.material.renderQueue = RenderQueue.IndicatorAod + 2;

			renderer.gameObject.SetActive(false);
		}

		matSelf = ResourceLoadManager.Instance.GetResources("Prefab/Indicator/Zone_Self") as Material;
		matSelf.renderQueue = RenderQueue.IndicatorAod + 1;
		matRival = ResourceLoadManager.Instance.GetResources("Prefab/Indicator/Zone_Rival") as Material;
		matRival.renderQueue = RenderQueue.IndicatorAod + 1;
	}

	public int DetectZone(IM.Vector3 pos)
	{
		int zone = 0;
		IM.Vector3 dir = pos - basketCenter;
		dir.y = IM.Number.zero;
		IM.Number radialDist = dir.magnitude;
        IM.Number horiDist = IM.Math.Abs(pos.x);
		dir.Normalize();
        IM.Number angle = IM.Vector3.Angle(IM.Vector3.right, dir);
		int radialSection = ((angle - ANGLE_SEG_SIDE) / ANGLE_SEG).floorToInt + 1;
		//Logger.Log("section: " + radialSection);
		if (radialSection == 0 || radialSection == 4)
		{
			if (horiDist <= HORI_NEAR)
				zone = 1;
			else if (horiDist <= HORI_MIDDLE)
				zone = pos.x > 0 ? 2 : 10;
			else if (horiDist <= HORI_FAR)
				zone = pos.x > 0 ? 3 : 11;
		}
		else if (radialSection == 1 || radialSection == 2 || radialSection == 3)
		{
			if (radialDist <= RADIAL_NEAR)
				zone = 1;
			else if (radialDist <= RADIAL_MIDDLE)
				zone = (radialSection + 1) * 2;
			else if (radialDist <= RADIAL_FAR)
				zone = (radialSection + 1) * 2 + 1;
		}
		return zone;
	}

	private void OnGoal(UBasketball ball)
	{
		int zone = shootZones[ball];
		if (zone != 0)
		{
			MeshRenderer renderer = zoneRenderer[zone];
			int owner = zoneOwnership[zone];
			if (ball.m_actor == m_mainRole)
			{
				if (owner == 0)
				{
					++m_homeScore;
					renderer.material = matSelf;
					renderer.gameObject.SetActive(true);
					zoneOwnership[zone] = 1;
				}
				else if (owner == 2)
				{
					--m_awayScore;
					renderer.gameObject.SetActive(false);
					zoneOwnership[zone] = 0;
				}
			}
			else
			{
				if (owner == 0)
				{
					++m_awayScore;
					renderer.material = matRival;
					renderer.gameObject.SetActive(true);
					zoneOwnership[zone] = 2;
				}
				else if (owner == 1)
				{
					--m_homeScore;
					renderer.gameObject.SetActive(false);
					zoneOwnership[zone] = 0;
				}
			}
		}
	}

	private void OnDunk(UBasketball ball, bool goal)
	{
		OnShoot(ball);

		if (goal)
			OnGoal(ball);
	}

	private void OnShoot(UBasketball ball)
	{
		if (ball.m_actor == m_mainRole)
			shootZones[ball] = DetectZone(m_mainRole.position);
		else
			shootZones[ball] = DetectZone(npc.position);
	}
}
