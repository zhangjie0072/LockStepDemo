using UnityEngine;
using System.Collections;

public class PractiseBehaviourLayupDunk : PractiseBehaviour, PlayerActionEventHandler.Listener
{
	enum Step
	{
		Tip0,
		Run,
		Tip1,
		Tip2,
	}
	private Player defenser;
	private Step step;
	private IM.Vector3 basketCenter;
	private GameObject layupArea;

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eAway;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseLayupDunk_Defense;
	}

    public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
		if (shooter.m_StateMachine.m_curState.m_eState == PlayerState.State.eLayup ||
			shooter.m_StateMachine.m_curState.m_eState == PlayerState.State.eDunk)
			return IM.BigNumber.one;
		else
            return IM.BigNumber.zero;
	}

	protected override void OnMatchSetted()
	{
		match.m_mainRole.eventHandler.AddEventListener(this);
		match.m_mainRole.m_alwaysForbiddenPickup = true;
		defenser = match.m_awayTeam.GetMember(0);
		defenser.eventHandler.AddEventListener(this);
		defenser.m_StateMachine.ReplaceState(new PlayerState_PractiseLayupDunk_Stand(defenser.m_StateMachine, match));
		defenser.m_alwaysForbiddenPickup = true;
		match.mCurScene.mBall.onHitGround += OnHitGround;
	}

	public override bool ResetPlayerPos()
	{
		IM.Number angle = IM.Random.Range(new IM.Number(90), new IM.Number(270));
		IM.Vector3 dir = IM.Quaternion.AngleAxis(angle, IM.Vector3.up) * IM.Vector3.forward;
        match.m_mainRole.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position + dir * IM.Number.two;
		basketCenter = match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		dir = match.m_mainRole.position - basketCenter;
		dir.Normalize();
		defenser.position = basketCenter + dir * IM.Number.two;
		defenser.forward = dir;
		match.m_mainRole.forward = -dir;
		return true;
	}

	public override bool IsCommandValid(Command command)
	{
		return command == Command.Shoot && (!in_tutorial || step == Step.Tip1) && match.m_mainRole.m_bWithBall;
	}

	protected override void OnStart()
	{
		base.OnStart();

		Reset();
		if (!_free_mode)
		{
			if (in_tutorial)
			{
				HideNPC();
				match.onTipClick = OnTipClick;
				Tip0();
			}
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		match.HighlightButton(0, in_tutorial && IsCommandValid(Command.Shoot));
	}

	private void ShowNPC()
	{
		defenser.m_aiMgr.m_enable = true;
		defenser.gameObject.SetActive(true);
		defenser.m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
	}

	private void HideNPC()
	{
		defenser.m_aiMgr.m_enable = false;
		defenser.gameObject.SetActive(false);
		defenser.m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
	}

	public void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (sender == match.m_mainRole &&
			(animEvent == PlayerActionEventHandler.AnimEvent.eLayup ||
			 animEvent == PlayerActionEventHandler.AnimEvent.eDunk))
			OnLayupDunk();
		else if (animEvent == PlayerActionEventHandler.AnimEvent.eShoot && sender == match.m_mainRole)
			OnShoot();
	}

	private void OnLayupDunk()
	{
		if (in_tutorial)
			Tip2();
		else
			FinishObjective(true);
	}

	private void OnShoot()
	{
		if (_curr_obj_index != EXERCISE_OBJ_INDEX)
		{
			FinishObjective(false);
		}
	}

	private void OnHitGround(UBasketball ball)
	{
		if (ball.m_bounceCnt == 1)
			StartCoroutine(ResetLater());
	}

	private IEnumerator ResetLater()
	{
		yield return new WaitForSeconds(2);
		Reset();
	}

	private void Reset()
	{
		StopCoroutine("ResetLater");
		match.m_homeTeam.m_role = GameMatch.MatchRole.eOffense;
		match.m_awayTeam.m_role = GameMatch.MatchRole.eDefense;
		match.m_stateMachine.SetState(MatchState.State.eBegin);
	}

	private void Tip0()
	{
		step = Step.Tip0;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[0];
		match.m_mainRole.m_inputDispatcher.m_enable = false;
	}

	private void Run()
	{
		step = Step.Run;
		match.HideGuideTip();
		match.m_mainRole.m_inputDispatcher.m_enable = false;
		match.m_mainRole.m_aiMgr = new AISystem_PractiseLayupDunk(match, match.m_mainRole, AIState.Type.ePractiseLayupDunk_Positioning);
		AI_PractiseLayupDunk_Positioning state = match.m_mainRole.m_aiMgr.GetState(AIState.Type.ePractiseLayupDunk_Positioning) as AI_PractiseLayupDunk_Positioning;
		state.onEnterLayupArea = Tip1;
	}

	private void Tip1()
	{
		step = Step.Tip1;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[1];
		match.m_mainRole.m_inputDispatcher.m_enable = true;
		match.m_mainRole.m_aiMgr = null;
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/LayupArea") as GameObject;
		layupArea = GameObject.Instantiate(prefab) as GameObject;
		layupArea.transform.localPosition = (Vector3)basketCenter;
		match.ShowTouchGuide(OnTouchJoyStick, true);
	}

	private void Tip2()
	{
		step = Step.Tip2;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[2];
		match.ShowIconTip(false);
		match.m_mainRole.m_inputDispatcher.m_enable = false;
		match.HideTouchGuide();
		Object.Destroy(layupArea);
		layupArea = null;
	}

	private void OnTouchJoyStick(GameObject go, bool pressed)
	{
		if (layupArea)
		{
			Object.Destroy(layupArea);
			layupArea = null;
		}
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.Tip0:
				Run();
				break;
			case Step.Tip2:
				match.m_mainRole.m_inputDispatcher.m_enable = true;
				match.HideGuideTip();
				FinishObjective(true);
				ShowNPC();
				break;
		}
	}
}
