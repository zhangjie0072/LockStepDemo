using UnityEngine;
using System.Collections;

public class PractiseBehaviourShoot : PractiseBehaviour, PlayerActionEventHandler.Listener
{
	enum Step
	{
		Tip0,
		Tip1,
		Tip2,
		Tip3,
	}
	private Step step;

	private Player npc;

	private bool shooting;

	public delegate void Delegate();
	public Delegate onTutorialOver;

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eHome;
	}

	public override bool ResetPlayerPos()
	{
        //GameObject center3PT = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/3PTCenter") as GameObject;
        //match.m_mainRole.position = new IM.Vector3(center3PT.transform.position);
        //match.m_mainRole.forward = IM.Vector3.forward;
        match.m_mainRole.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position;
        match.m_mainRole.forward = IM.Vector3.forward;
		IM.Vector3 basketCenter = match.mCurScene.mBasket.m_vShootTarget;
		basketCenter.y = IM.Number.zero;
		npc.position = basketCenter - IM.Vector3.forward * IM.Number.half;
		npc.forward = -IM.Vector3.forward;
		return true;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseShoot_Idle;
	}

	public override bool IsCommandValid(Command command)
	{
		return (command == Command.Shoot && (!in_tutorial || step == Step.Tip1 || step == Step.Tip2));
	}

	protected override void OnMatchSetted()
	{
		match.m_mainRole.m_StateMachine.ReplaceState(new PlayerState_PrepareToShoot_ForceShoot(match.m_mainRole.m_StateMachine, match));
		match.m_mainRole.shootStrength.mode = ShootStrength.Mode.Absolute;
		match.m_mainRole.eventHandler.AddEventListener(this);
		npc = match.m_homeTeam.GetMember(1);
		npc.eventHandler.AddEventListener(this);
		match.mCurScene.mBasket.onGoal = OnGoal;
		match.mCurScene.mBasket.onNoGoal = OnNoGoal;
	}

	protected override void OnStart()
	{
		base.OnStart();

		Reset();
		if (!_free_mode)
		{
			if (in_tutorial)
			{
				match.onTipClick = OnTipClick;
				HideNPC();
				Tip0();
			}
			else
			{
				ShowNPC();
				match.HideGuideTip();
			}
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

        match.HighlightButton(0, in_tutorial && IsCommandValid(Command.Shoot) && match.m_mainRole.m_bWithBall);
	}

	private void ShowNPC()
	{
		npc.m_aiMgr.m_enable = true;
		npc.gameObject.SetActive(true);
		npc.m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
	}

	private void HideNPC()
	{
		npc.m_aiMgr.m_enable = false;
		npc.gameObject.SetActive(false);
		npc.m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
	}

	public void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (animEvent == PlayerActionEventHandler.AnimEvent.eShoot && sender == match.m_mainRole)
			OnShoot();
	}

	private void OnShoot()
	{
		shooting = true;
		match.HighlightButton(0, false);
	}

	private void OnShootOver(UBasketball ball, bool goal)
	{
		if (shooting)
		{
			if (in_tutorial)
			{
				if (goal)
					Tip3();
				else
				{
					Tip2();
					StartCoroutine(ResetLater());
				}
			}
			else
			{
				FinishObjective(goal);
			}
			shooting = false;
		}
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		OnShootOver(ball, true);
	}

	private void OnNoGoal(UBasket basket, UBasketball ball)
	{
		OnShootOver(ball, false);
	}

	private IEnumerator ResetLater()
	{
		yield return new WaitForSeconds(0.2f);
		Reset();
	}

	private void Reset()
	{
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

	private void Tip1()
	{ 
		step = Step.Tip1;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[1];
		match.m_mainRole.m_inputDispatcher.m_enable = true;
	}

	private void Tip2()
	{
		step = Step.Tip2;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[2];
		match.m_mainRole.m_inputDispatcher.m_enable = true;
		match.ShowIconTip(false);
	}

	private void Tip3()
	{
		if (onTutorialOver != null)
			onTutorialOver();
		step = Step.Tip3;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[3];
		match.m_mainRole.m_inputDispatcher.m_enable = false;
		match.ShowIconTip(false);
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.Tip0:
				Tip1();
				break;
			case Step.Tip3:
				match.m_mainRole.m_inputDispatcher.m_enable = true;
				match.HideGuideTip();
				FinishObjective(true);
				ShowNPC();
				break;
		}
	}
}
