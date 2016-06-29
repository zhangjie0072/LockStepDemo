using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fogs.proto.msg;

public class PractiseBehaviourSkillGuide : PractiseBehaviour
{
	public enum Step
	{
		Intro,
		Tutor,
		Play,
		Retutor,
		Praise,
		Ending,
		FreePlay,
		Complete,
	}

	private Step _step = Step.Intro;
	public Step step
	{
		get { return _step; }
		private set { _step = value; }
	}

	public static uint SKILL_ID = 3202;

	private GameObject tipOnScene;
	private UILabel tipText;
	private Transform tmBackButton;
    private GameObject guideDunk;

	private Dictionary<string, string> tips = new Dictionary<string, string>();

	private bool jumped;
	private bool jumpedInRightArea;

	private uint oldWeight;

	public delegate void Delegate();
	public Delegate onOver;

	public override bool IsCommandValid(Command command)
	{
		if (command == Command.Shoot && 
			(step == Step.Play || step == Step.FreePlay))
			return true;
		return false;
	}

    public override IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
        return IM.PrecNumber.one; 
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();

		match.HideSignal();
		match.HideTitle();

		match.onTipClick += OnTipClick;
		match.mCurScene.mBall.onShoot += OnShoot;
		match.mCurScene.mBall.onGrab += OnGrab;
		match.mCurScene.mBall.onHitGround += OnHitGround;
		match.mCurScene.mBasket.onGoal += OnGoal;

        match.mainRole.m_InfoVisualizer.ShowStrengthBar(false);

		foreach (string tip in practise.tips)
		{
			string[] tokens = tip.Split(':');
			tips.Add(tokens[0], tokens[1]);
		}

		EnsureTomahawkDunk();
	}

	private void EnsureTomahawkDunk()
	{
		match.mainRole.m_skillSystem.DisableCommand(Command.Layup);	//Disable layup

		foreach (SkillSlotProto proto in match.mainRole.m_roleInfo.skill_slot_info)
		{
			Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TOTAL, proto.skill_uuid);
			if (goods != null && goods.GetID() == SKILL_ID)
			{
				SkillAttr skill = GameSystem.Instance.SkillConfig.GetSkill(goods.GetID());
				if (skill != null)
				{
					SkillLevel lvl = skill.levels[goods.GetLevel()];
					oldWeight = lvl.weight;
					lvl.weight = uint.MaxValue;
				}
			}
		}
	}

	private void CancelEnsureTomahawkDunk()
	{
		match.mainRole.m_skillSystem.CancelDisableCommand(Command.Layup);	//Cancel disable layup

		foreach (SkillSlotProto proto in match.mainRole.m_roleInfo.skill_slot_info)
		{
			Goods goods = MainPlayer.Instance.GetGoods(GoodsCategory.GC_TOTAL, proto.skill_uuid);
			if (goods != null && goods.GetID() == SKILL_ID)
			{
				SkillAttr skill = GameSystem.Instance.SkillConfig.GetSkill(goods.GetID());
				if (skill != null)
				{
					SkillLevel lvl = skill.levels[goods.GetLevel()];
					lvl.weight = oldWeight;
				}
			}
		}
	}

	protected override void OnStart()
	{
		//there's no free mode in this practise
		_free_mode = false;

		base.OnStart();

		Transform tmBasePanel = UIManager.Instance.m_uiRootBasePanel.transform;
		tmBackButton = tmBasePanel.FindChild("UIPractise(Clone)/ButtonBack");
		tmBackButton = UIManager.Instance.CreateUI("ButtonBack", tmBackButton).transform;
		if (tmBackButton != null)
			NGUITools.SetActive(tmBackButton.gameObject, false);

		StartCoroutine(Step_Intro());
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (!match.mainRole.m_bWithBall)
		{
			match.HighlightButton(0, false);
		}

		if (!jumped && !match.mainRole.m_bOnGround)
		{
			jumped = true;
			jumpedInRightArea = (match.mainRole.m_StateMachine.m_curState.m_curExecSkill.skill.id == SKILL_ID);
		}
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.Intro:
				Step_Tutor();
				break;
			case Step.Ending:
				Step_FreePlay();
				break;
		}
	}

	private void OnGrab(UBasketball ball)
	{
		if (step == Step.Play && !jumpedInRightArea)
			Step_Retutor();
	}

	private void OnShoot(UBasketball ball)
	{
		match.HideGuideTip();
		//tipOnScene.SetActive(false);
	}

	private void OnHitGround(UBasketball ball)
	{
		if (step == Step.Play)
		{
			if (!jumpedInRightArea)
				Step_Retutor();
		}
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		if (step == Step.Play && jumpedInRightArea)
			Step_Praise();
	}

	private IEnumerator Step_Intro()
	{
		match.mainRole.m_StateMachine.SetState(PlayerState.State.eStand);
		match.ResetPlayerPos();

		yield return new WaitForSeconds(0.3f);

		match.ShowGuideTip();
		match.tip = tips["Intro"];
		match.ShowTipArrow();
		match.mainRole.m_inputDispatcher.m_enable = false;

		step = Step.Intro;
	}

	private void Step_Tutor()
	{
        GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/pre_SkillGuideArea") as GameObject;
		GameObject area = Object.Instantiate(prefab) as GameObject;
		tipOnScene = CreateUIEffect("Prefab/GUI/TipOnScene", area.transform.GetChild(0));
		tipOnScene.transform.FindChild("BG").gameObject.SetActive(false);
		tipText = tipOnScene.transform.FindChild("Tip").GetComponent<UILabel>();
		tipText.text = tips["AreaTip"];
        match.HideGuideTip();
		step = Step.Tutor;
        guideDunk = UIManager.Instance.CreateUI("UIGuide");
        guideDunk.transform.FindChild("Middle/Texture").GetComponent<UITexture>().mainTexture 
        = ResourceLoadManager.Instance.GetResources("Texture/bg_guide_1", true) as Texture;
		GameObject goBtn = guideDunk.transform.FindChild("Button").gameObject;
		UIEventListener.Get(goBtn).onClick += (go) => Step_Play();
	}

	private void Step_Play()
	{
		jumped = false;
		jumpedInRightArea = false;
		match.mainRole.GrabBall(match.mCurScene.mBall);
		match.ResetPlayerPos();
		match.HideGuideTip();
        NGUITools.Destroy(guideDunk);
        guideDunk = null;
		match.mainRole.m_inputDispatcher.m_enable = true;
		step = Step.Play;
	}

	private void Step_Retutor()
	{
		match.ShowTipArrow();
		match.tip = tips["Retutor"];
		match.mainRole.m_inputDispatcher.m_enable = false;
		step = Step.Retutor;
		guideDunk = UIManager.Instance.CreateUI("UIGuide");
        guideDunk.transform.FindChild("Middle/Texture").GetComponent<UITexture>().mainTexture
        = ResourceLoadManager.Instance.GetResources("Texture/bg_guide_1", true) as Texture;
		GameObject goBtn = guideDunk.transform.FindChild("Button").gameObject;
		UIEventListener.Get(goBtn).onClick += (go) => Step_Play();
	}

	private void Step_Praise()
	{
		match.mainRole.m_inputDispatcher.m_enable = false;
        GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GuideTip_4") as GameObject;
		GameObject tip = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform);
		tip.GetComponent<UIPanel>().depth = 10000;
		UILabel label = tip.transform.FindChild("Tip").GetComponent<UILabel>();
		label.text = tips["Praise"];

		GameObject button = tip.transform.FindChild("Button").gameObject;
		NGUITools.SetActive(button, true);
		string text = tips["Retry"];
		label = button.transform.FindChild("Label").GetComponent<UILabel>();
		label.text = text;
		label.leftAnchor.target = label.rightAnchor.target;
		label.leftAnchor.Set(label.rightAnchor.relative, label.rightAnchor.absolute - text.Length * label.height);
		label.ResetAnchors();
		UIEventListener.Get(button).onClick += (GameObject) => { NGUITools.Destroy(tip); Step_Ending(); };

		GameObject secondButton = tip.transform.FindChild("SecondButton").gameObject;
		NGUITools.SetActive(secondButton, true);
		text = tips["Exit"];
		label = secondButton.transform.FindChild("Label").GetComponent<UILabel>();
		label.text = text;
		label.rightAnchor.target = label.leftAnchor.target;
		label.rightAnchor.Set(label.leftAnchor.relative, label.leftAnchor.absolute + text.Length * label.height);
		label.ResetAnchors();
		UIEventListener.Get(secondButton).onClick += (GameObject) => { NGUITools.Destroy(tip); ReturnToHall(); };

		UISprite sprInstuctor = tip.transform.FindChild("Instructor").GetComponent<UISprite>();
		sprInstuctor.spriteName = "effects_guide";
		sprInstuctor.MakePixelPerfect();
		sprInstuctor.transform.localPosition = new Vector3(-328f, -11f, 0f);
		NGUITools.SetActive(sprInstuctor.gameObject, false);
		NGUITools.SetActive(tip.transform.FindChild("Frame/Arrow").gameObject, false);
		step = Step.Praise;

		if (onOver != null)
			onOver();
	}

	private void Step_Ending()
	{
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = tips["Ending"];
		step = Step.Ending;

		if (tmBackButton != null)
		{
			NGUITools.SetActive(tmBackButton.gameObject, true);
			UIEventListener.Get(tmBackButton.gameObject).onClick = (GameObject) => { ReturnToHall(); };

			List<ControlEffect> effect = new List<ControlEffect>();
			effect.Add(ControlEffect.BlinkArea);
			List<Vector2> pos = new List<Vector2>();
			pos.Add(Vector2.zero);
			GuideSystem.Instance.AddControlEffect(effect, pos, tmBackButton, tmBackButton.GetComponent<UIWidget>());

            GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/GuideTip_3") as GameObject;
			GameObject tip = CommonFunction.InstantiateObject(prefab, tmBackButton);
			tip.transform.localPosition = new Vector3(198f, -90f, 0f);
			tip.GetComponent<UIPanel>().depth = 10000;
			UILabel label = tip.transform.FindChild("Tip").GetComponent<UILabel>();
			label.overflowMethod = UILabel.Overflow.ResizeFreely;
			label.text = tips["ExitButtonTip"];
			NGUITools.SetActive(tip.transform.FindChild("Button").gameObject, false);
			NGUITools.SetActive(tip.transform.FindChild("SecondButton").gameObject, false);
			NGUITools.SetActive(tip.transform.FindChild("Instructor").gameObject, false);
			NGUITools.SetActive(tip.transform.FindChild("Frame/Arrow").gameObject, false);
			UIWidget frame = tip.transform.FindChild("Frame").GetComponent<UIWidget>();
			frame.bottomAnchor.absolute = -frame.topAnchor.absolute;
			frame.ResetAnchors();
		}
	}

	private void Step_FreePlay()
	{
		match.HideGuideTip();
		match.mainRole.m_inputDispatcher.m_enable = true;
		step = Step.FreePlay;
	}

	private void ReturnToHall()
	{
		CancelEnsureTomahawkDunk();
		GameSystem.Instance.mClient.Reset();
        GameSystem.Instance.mClient.mUIManager.curLeagueType = GameMatch.LeagueType.eSkillGuide;
        SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
	}
}
