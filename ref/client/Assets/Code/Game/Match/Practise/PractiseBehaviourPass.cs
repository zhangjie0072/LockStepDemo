using UnityEngine;
using System.Collections;

public class PractiseBehaviourPass : PractiseBehaviour, PlayerActionEventHandler.Listener
{
	enum Step
	{
		TipPassIntro,
		TipPass,
		TipWellDone,
		TipRequireIntro,
		TipRequire,
		TipRepractise,
	}

	bool passing;
	Step step;
	Player npc;
	Player passer;
	GameUtils.Timer4View timer;

	public delegate void Delegate();
	public Delegate onTutorialOver;

	protected override void OnFirstStart()
	{
		base.OnFirstStart();
		match.mainRole.eventHandler.AddEventListener(this);
	}

    protected override void OnStart()
    {
        base.OnStart();

		if (!_free_mode)
		{
			if (in_tutorial)
			{
				match.onTipClick = OnTipClick;
				TipPassIntro();
			}
			else
			{
				match.HideGuideTip();
			}
		}
    }

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		UBasketball ball = match.mCurScene.mBall;
		if (!passing && ball.m_ballState == BallState.eUseBall_Pass)
		{
			passing = true;
			passer = ball.m_actor;
		}
		else if (passing && ball.m_ballState == BallState.eUseBall_Pass)
		{
			passing = false;
			OnPassed();
		}

		match.HighlightButton(1, in_tutorial && (IsCommandValid(Command.Pass) || IsCommandValid(Command.RequireBall)));

		if (timer != null)
			timer.Update(RealTime.deltaTime);
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.TipPassIntro:
				TipPass();
				break;
			case Step.TipRequireIntro:
				TipRequire();
				break;
			case Step.TipRepractise:
				FinishObjective(true);
				match.HideGuideTip();
				InputReader.Instance.enabled = true;
				break;
		}
	}

	private void TipPassIntro()
	{
		step = Step.TipPassIntro;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[0];
		InputReader.Instance.enabled = false;
	}

	private void TipPass()
	{
		step = Step.TipPass;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[1];
		match.mainRole.m_inputDispatcher.m_enable = true;
	}

	private void TipWellDone()
	{
		step = Step.TipWellDone;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[4];
		match.ShowIconTip(true);
		match.mainRole.m_inputDispatcher.m_enable = false;
		timer = new GameUtils.Timer4View(2f, TipRequireIntro);
	}

	private void TipRequireIntro()
	{
		timer.stop = true;
		step = Step.TipRequireIntro;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[2];
		match.mainRole.m_inputDispatcher.m_enable = false;
	}

	private void TipRequire()
	{
		step = Step.TipRequire;
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = practise.tips[3];
		match.mainRole.m_inputDispatcher.m_enable = true;
	}

	private void TipRepractise()
	{
		if (onTutorialOver != null)
			onTutorialOver();
		step = Step.TipRepractise;
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = practise.tips[5];
		match.mainRole.m_inputDispatcher.m_enable = false;
		match.ShowIconTip(true);
	}

	public void OnEvent(PlayerActionEventHandler.AnimEvent animEvent, Player sender, System.Object context)
	{
		if (animEvent == PlayerActionEventHandler.AnimEvent.eLayup ||
			animEvent == PlayerActionEventHandler.AnimEvent.eDunk ||
			animEvent == PlayerActionEventHandler.AnimEvent.eShoot)
		{
			OnOtherAction();
		}
	}

    public override Team.Side GetNPCSide()
    {
        return Team.Side.eHome;
    }

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractisePass_Positioning;
	}

	public override bool IsCommandValid(Command command)
	{
		return (command == Command.Shoot && !in_tutorial) || 
			(command == Command.Pass && (!in_tutorial || step == Step.TipPass)) ||
			(command == Command.RequireBall && (!in_tutorial || step == Step.TipRequire));
	}

	public override bool ResetPlayerPos()
	{
		npc = match.m_homeTeam.GetMember(1);
        npc.position = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position;
		npc.forward = -IM.Vector3.forward;
        //GameObject centerOf3PT = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/3PTCenter") as GameObject;
        //match.m_mainRole.position = new IM.Vector3(centerOf3PT.transform.position);
        //match.m_mainRole.forward = IM.Vector3.forward;
        match.mainRole.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position;
        match.mainRole.forward = IM.Vector3.forward;
		return true;
	}

	private void OnPassed()
	{
		if (in_tutorial)
		{
			if (passer == match.mainRole)
				TipWellDone();
			else
				TipRepractise();
		}
		else
		{
			if (passer == match.mainRole)
				FinishObjective(true);
		}
	}

	private void OnOtherAction()
	{
		if (_curr_obj_index != EXERCISE_OBJ_INDEX)
		{
			FinishObjective(false);
		}
	}
}
