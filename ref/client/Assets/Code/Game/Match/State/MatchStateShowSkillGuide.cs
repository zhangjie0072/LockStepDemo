using System;
using UnityEngine;

public class MatchStateShowSkillGuide : MatchState
{
	public uint guideStepID;

	public delegate void Delegate();
	public Delegate onOver;

	GameUtils.Timer timer;
	UIPanel maskPanel;
	GameObject indicator;

	public MatchStateShowSkillGuide(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eShowSkillGuide;
		timer = new GameUtils.Timer(new IM.Number(3), OnTimer);
		timer.stop = true;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		base.OnEnter(lastState);

		GuideStep step = GameSystem.Instance.GuideConfig.GetStep(guideStepID);
        GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GuideMask") as GameObject;
		maskPanel = NGUITools.AddChild<UIPanel>(GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel);
		maskPanel.gameObject.AddComponent<UIManagedPanel>();
		NGUITools.BringForward(maskPanel.gameObject);
		GameObject obj = CommonFunction.InstantiateObject(prefab, maskPanel.transform);
		obj.GetComponent<UIWidget>().alpha = 0f;
		UIEventListener.Get(obj).onClick += OnClick;
		GuideSystem.Instance.ShowTip(GuideSystem.Instance.curStep);
        GameObject areaPrefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/pre_SkillGuideArea") as GameObject;
		indicator = GameObject.Instantiate(areaPrefab) as GameObject;

		timer.stop = false;
	}
	
	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);

		timer.Update(fDeltaTime);
	}

	void OnTimer()
	{
		if (onOver != null)
			onOver();
		timer.stop = true;
		NGUITools.Destroy(maskPanel.gameObject);
		MatchStateBeginGuide state = m_stateMachine.GetState(MatchState.State.eBegin) as MatchStateBeginGuide;
		state.forwardToShowSkillGuide = false;
		m_stateMachine.SetState(MatchState.State.eBegin);
	}

	void OnClick(GameObject go)
	{
		OnTimer();
	}
}
