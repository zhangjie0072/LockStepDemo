using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PractiseBehaviourBaseGuide : PractiseBehaviour
{
	const float DIALOG_DELAY = 5f;
	Color oppoColor = new Color(0.2f, 0.2f, 0.2f);
	Color mainColor = new Color(0.5f, 0.5f, 0.5f);

	public enum Step
	{
		None,
		AskPickBall,
		AnswerPickBall,
		GrabBall,
		PassTutor,
		SwitchOnCatch,
		ShootTutor,
		ShootRetutor,
		ShootPraise,
		ShowMe,
		ShowMeYourAttack,
		DunkTutor,
		DunkFailed,
		DunkRetutor,
		DialogInvite1,
		DialogInvite2,
		DialogInvite3,
		DialogInvite4,
		DialogInvite5,
		DefenseTutor,
		DefenseRetutor,
		Layup,
		BlockTutor,
		DialogJoin1,
		DialogJoin2,
		DialogJoin3,
		DialogJoin4,
		Complete,
	}
    //private Transform ballPos;
    //private Transform movePos;
    //private Transform mainRolePos;
    //private Transform npcPos;
    //private Transform mainRolePos1;
    //private Transform npcPos1;
	private BoxCollider collider1;
	private BoxCollider collider2;

	private Step _step = Step.None;
	public Step step
	{
		get { return _step; }
		private set { _step = value; }
	}

	private GameObject layupArea;
	private GameObject positionEffect;
	private GameObject tipOnScene;
	private UILabel tipText;
	private GameObject tipBorder;
	private GameObject npcTip;
	private UILabel npcTipText;
	private UIPlayPlot plot;

	private Dictionary<string, string> tips = new Dictionary<string, string>();

	public Player mainRole { get; private set; }
	public Player npc { get; private set; }

	private bool layupDunkFailed;

	public System.Action onOver;

	public override bool IsCommandValid(Command command)
	{
		if (command == Command.Pass && step == Step.PassTutor)
			return true;
		else if (command == Command.Shoot &&
			(step == Step.ShootTutor || step == Step.ShootRetutor ||
			step == Step.DunkTutor || step == Step.DunkRetutor))
			return true;
		else if (command == Command.Block && step == Step.BlockTutor)
			return true;
		else if (command == Command.Defense &&
			(step == Step.DefenseTutor || step == Step.Layup))
			return true;
		return false;
	}

    public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
		if (step == Step.ShowMeYourAttack || step == Step.DunkTutor || step == Step.DunkRetutor)
		{
            return layupDunkFailed ? IM.BigNumber.zero : IM.BigNumber.one;
		}
        return IM.BigNumber.one;
	}

    public override IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
		if (step == Step.ShowMeYourAttack || step == Step.DunkTutor || step == Step.DunkRetutor)
            return IM.Number.zero;
		else if (step == Step.BlockTutor)
			return IM.Number.one;
		return rate;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseBaseGuide_Idle;
	}

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eHome;
	}

	protected override void OnMatchSetted()
	{
		base.OnMatchSetted();

		mainRole = match.m_homeTeam.GetMember(0);
		mainRole.m_inputDispatcher.m_enable = false;
		npc = match.m_homeTeam.GetMember(1);
		npc.m_aiMgr = null;
	}

	public override bool ResetPlayerPos()
	{
        PractiseMovePos practiseMovePos = GameSystem.Instance.MatchPointsConfig.PractiseMovePos;
		if (step == Step.DunkRetutor)
		{
            mainRole.position = practiseMovePos.mainRole2_transform.position;
            mainRole.forward = IM.Vector3.forward;
            npc.position = practiseMovePos.npc2_transform.position;
            npc.forward = IM.Vector3.forward;
			return true;
		}
		else if (step == Step.DefenseTutor || step == Step.Layup)
		{
            npc.position = practiseMovePos.mainRole2_transform.position;
            IM.Vector3 dirNPC2Basket = GameUtils.HorizonalNormalized(match.mCurScene.mBasket.m_vShootTarget, npc.position);
            npc.position = practiseMovePos.mainRole2_transform.position +dirNPC2Basket * IM.Number.half;
			npc.forward = dirNPC2Basket;
			mainRole.position = npc.position + dirNPC2Basket * IM.Number.two;
			mainRole.forward = -dirNPC2Basket;
			return true;
		}
        mainRole.position = practiseMovePos.mainRole1_transform.position;
        npc.position = practiseMovePos.npc1_transform.position;
		mainRole.forward = GameUtils.HorizonalNormalized(npc.position, mainRole.position);
		npc.forward = -mainRole.forward;
		return true;
	}

	protected override void OnAwake()
	{
		base.OnAwake();

        //GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/PractiseMovePos") as GameObject;
        //GameObject pos_obj = CommonFunction.InstantiateObject(prefab, transform);
        //ballPos = pos_obj.transform.FindChild("Ball");
        //movePos = pos_obj.transform.FindChild("1");
        //mainRolePos = pos_obj.transform.FindChild("MainRole");
        //npcPos = pos_obj.transform.FindChild("NPC");
        //mainRolePos1 = pos_obj.transform.FindChild("MainRole1");
        //npcPos1 = pos_obj.transform.FindChild("NPC1");
	}

	private void OnBlockPress(GameObject go, bool pressed)
	{
		if (step == Step.BlockTutor)
		{
			Pause(false);
			match.HideGuideTip();
		}
	}

	private void OnDefensePress(GameObject go, bool pressed)
	{
		if (step == Step.DefenseTutor)
		{
			Step_Layup();
		}
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();

#if UNITY_IPHONE || UNITY_ANDROID
		UIEventListener.Get(match.m_uiController.m_btns[0].btn.gameObject).onPress += OnBlockPress;
		UIEventListener.Get(match.m_uiController.m_btns[1].btn.gameObject).onPress += OnDefensePress;
#endif

		plot = UIManager.Instance.CreateUI("UIPlayPlot").GetComponent<UIPlayPlot>();
		plot.Hide();
		plot.onNext = OnPlotNext;

		positionEffect = Object.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/Position")) as GameObject;
		match.HideSignal();
		match.HideTitle();
		match.HideBackButton();

		match.onTipClick += OnTipClick;
		match.mCurScene.mBall.onGrab += OnGrab;
		match.mCurScene.mBall.onCatch += OnCatch;
		match.mCurScene.mBall.onShoot += OnShoot;
		match.mCurScene.mBall.onHitGround += OnHitGround;
		match.mCurScene.mBasket.onGoal += OnGoal;

		match.m_mainRole.m_InfoVisualizer.m_strengthBar = null;

		foreach (string tip in practise.tips)
		{
			string[] tokens = tip.Split(':');
			tips.Add(tokens[0], tokens[1]);
		}
	}

	protected override void OnStart()
	{
		//there's no free mode in this practise
		_free_mode = false;

		base.OnStart();

		HideNPC();
		StartCoroutine(Step_AskPickBall());
	}

	private void ShowNPC()
	{
		//npc.m_aiMgr.m_enable = true;
		npc.m_StateMachine.SetState(PlayerState.State.eStand, true);
		npc.gameObject.SetActive(true);
		npc.m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
        npc.position = GameSystem.Instance.MatchPointsConfig.PractiseMovePos.npc1_transform.position;
		npc.forward = GameUtils.HorizonalNormalized(mainRole.position, npc.position);
	}

	private void HideNPC()
	{
		//npc.m_aiMgr.m_enable = false;
		npc.gameObject.SetActive(false);
		npc.m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		if ((step == Step.ShootTutor || step == Step.ShootRetutor) && !mainRole.m_bWithBall)
			match.HighlightButton(0, false);
		if ((step == Step.DunkTutor || step == Step.DunkRetutor) && !npc.m_bWithBall)
			match.HighlightButton(0, false);
		if (step == Step.PassTutor && !npc.m_bWithBall)
			match.HighlightButton(2, false);

		if (step == Step.GrabBall && match.mCurScene.mBall.m_owner != null)
			Step_PassTutor();
		else if (step == Step.ShootTutor && 
			(mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold && mainRole.m_bMovedWithBall))
			Step_ShootRetutor();
		else if ((step == Step.ShowMeYourAttack || step == Step.DunkTutor || step == Step.DunkRetutor) &&
			npc.m_StateMachine.m_curState.m_eState == PlayerState.State.eShoot)
			layupDunkFailed = true;
		else if (step == Step.Layup &&
		         AIUtils.CanBlock(mainRole, npc, IM.Number.zero, IM.Number.zero, match.mCurScene.mBasket.m_vShootTarget))
			Step_BlockTutor();
#if !UNITY_IPHONE && !UNITY_ANDROID
		else if (step == Step.BlockTutor && Input.GetKey(KeyCode.J))
		{
			Pause(false);
			match.HideGuideTip();
		}
		else if (step == Step.DefenseTutor && Input.GetKey(KeyCode.K))
		{
			Step_Layup();
		}
#endif
	}

	private void OnPlotNext()
	{
		switch(step)
		{
			case Step.AskPickBall:
				Step_AnswerPickBall();
				break;
			case Step.AnswerPickBall:
				Step_GrabBall();
				break;
			case Step.SwitchOnCatch:
				Step_ShootTutor();
				break;
			case Step.ShootPraise:
				Step_ShowMe();
				break;
			case Step.ShowMe:
				plot.Hide();
				break;
			case Step.ShowMeYourAttack:
				Step_DunkTutor();
				break;
			case Step.DialogInvite1:
				Step_DialogInvite2();
				break;
			case Step.DialogInvite2:
				Step_DialogInvite3();
				break;
			case Step.DialogInvite3:
				Step_DialogInvite4();
				break;
			case Step.DialogInvite4:
				Step_DialogInvite5();
				break;
			case Step.DialogInvite5:
				Step_DefenseTutor();
				break;
			case Step.DialogJoin1:
				Step_DialogJoin2();
				break;
			case Step.DialogJoin2:
				Step_DialogJoin3();
				break;
			case Step.DialogJoin3:
				Step_DialogJoin4();
				break;
			case Step.DialogJoin4:
				StartCoroutine(Step_Over());
				break;
			case Step.DefenseRetutor:
				Step_DefenseTutor();
				break;
		}
	}

	private void OnTipClick(GameObject go)
	{
		switch (step)
		{
			case Step.DunkFailed:
				Step_DunkRetutor();
				break;
		}
	}

	private void OnGrab(UBasketball ball)
	{
		switch (step)
		{
			case Step.DunkTutor:
			case Step.DunkRetutor:
				if (layupDunkFailed)
					Step_DunkFailed();
				break;
		}
	}

	private void OnCatch(UBasketball ball)
	{
		switch (step)
		{
			case Step.PassTutor:
				Step_SwitchOnCatch();
				break;
			case Step.ShowMe:
				if (ball.m_owner == npc)
					Step_ShowMeYourAttack();
				break;
		}
	}

	private void OnShoot(UBasketball ball)
	{
		match.HideGuideTip();
		tipOnScene.SetActive(false);
		npcTip.SetActive(false);
	}

	private void OnHitGround(UBasketball ball)
	{
		if (step == Step.DunkTutor || step == Step.DunkRetutor)
		{
			if (layupDunkFailed)
				Step_DunkFailed();
		}
		else if (step == Step.BlockTutor)
		{
			Step_DialogJoin1();
		}
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		if (step == Step.ShootTutor || step == Step.ShootRetutor)
			Step_ShootPraise();
		else if (step == Step.ShowMeYourAttack || step == Step.DunkTutor || step == Step.DunkRetutor)
		{
			if (!layupDunkFailed)
				Step_DialogInvite1();
		}
		else if (step == Step.Layup)
		{
			Step_DefenseRetutor();
		}
	}

	private IEnumerator Step_AskPickBall()
	{
		match.m_mainRole.DropBall(match.mCurScene.mBall);
		match.m_mainRole.m_StateMachine.SetState(PlayerState.State.eStand);
		match.mCurScene.mBall.SetInitPos(GameSystem.Instance.MatchPointsConfig.PractiseMovePos.ball_transform.position);
		match.ResetPlayerPos();

		yield return new WaitForSeconds(0.3f);

		ShowNPC();
		mainRole.m_inputDispatcher.m_enable = false;
		match.mainRole = npc;
		npc.m_inputDispatcher = new InputDispatcher(match, match.mainRole);
		npc.m_inputDispatcher.m_enable = false;
		plot.Show(0, mainRole.m_id, tips["AskPickBall"], DIALOG_DELAY);
		step = Step.AskPickBall;
	}

	private void Step_AnswerPickBall()
	{
		plot.Show(1, npc.m_id, tips["AnswerPickBall"], DIALOG_DELAY);
		step = Step.AnswerPickBall;
	}

	private void Step_GrabBall()
	{
		plot.Hide();
		match.ShowTouchGuide(null, true, 90f);
		match.ShowGuideTip();
		match.tip = tips["TipPickUp"];
        GameObject ballposGo = new GameObject("ballPos");
        ballposGo.transform.position = (Vector3)GameSystem.Instance.MatchPointsConfig.PractiseMovePos.ball_transform.position;
        ballposGo.transform.rotation = (Quaternion)GameSystem.Instance.MatchPointsConfig.PractiseMovePos.ball_transform.rotation;
        positionEffect.transform.parent = ballposGo.transform;
		positionEffect.transform.localPosition = Vector3.zero;
		positionEffect.SetActive(true);
        tipOnScene = CreateUIEffect("Prefab/GUI/TipOnScene", ballposGo.transform);
		tipText = tipOnScene.transform.FindChild("Tip").GetComponent<UILabel>();
		tipText.text = tips["BallTipPickUp"];
		npc.m_inputDispatcher.m_enable = true;

		step = Step.GrabBall;
	}

	private void Step_PassTutor()
	{
		match.HideTouchGuide();
		tipOnScene.SetActive(false);
		positionEffect.SetActive(false);
		match.tip = tips["TipPass"];
		match.ShowGuideTip();
		match.HideTipArrow();
		step = Step.PassTutor;
		match.HighlightButton(2, true);
		match.mainRole.m_inputDispatcher.m_enable = true;
		match.mainRole.m_enableMovement = false;
	}

	private void Step_SwitchOnCatch()
	{
		npcTip = CreateUIEffect("Prefab/GUI/NPCTip", mainRole.gameObject.transform);
		npcTipText = npcTip.transform.FindChild("Text").GetComponent<UILabel>();
		npcTipText.text = tips["RoleTipSwitchOnCatch"];
		match.HideGuideTip();
		match.HideTipArrow();
		npc.m_inputDispatcher.m_enable = false;
		match.mainRole = mainRole;
		mainRole.m_inputDispatcher.m_enable = false;
		plot.Show(0, mainRole.m_id, tips["LookMeShoot"], DIALOG_DELAY);
		step = Step.SwitchOnCatch;
	}

	private void Step_ShootTutor()
	{
		plot.Hide();
		npcTip.SetActive(false);
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = tips["TipShootTutor"];
		step = Step.ShootTutor;
		match.HighlightButton(0, true);
		mainRole.m_inputDispatcher.m_enable = true;
	}

	private void Step_ShootRetutor()
	{
		match.tip = tips["TipShootRetutor"];
		step = Step.ShootRetutor;
		match.HighlightButton(0, true);
	}

	private void Step_ShootPraise()
	{
		match.HideGuideTip();
		plot.Show(1, npc.m_id, tips["NiceShoot"], DIALOG_DELAY);
		step = Step.ShootPraise;
		mainRole.m_inputDispatcher.m_enable = false;
		match.HighlightButton(0, false);
	}

	private void Step_ShowMe()
	{
		plot.Show(0, mainRole.m_id, tips["ShowMe"], DIALOG_DELAY);
		if (mainRole.m_aiMgr == null)
			mainRole.m_aiMgr = new AISystem_PractiseBaseGuide(match, mainRole, AIState.Type.ePractiseBaseGuide_Idle);
		mainRole.m_inputDispatcher.m_enable = false;
		step = Step.ShowMe;
	}

	private void Step_ShowMeYourAttack()
	{
		plot.Show(0, mainRole.m_id, tips["ShowMeYourAttack"], DIALOG_DELAY);
		match.mainRole.m_inputDispatcher.m_enable = false;
		match.mainRole = npc;
		match.mainRole.m_inputDispatcher = new InputDispatcher(match , match.mainRole);
		match.mainRole.m_inputDispatcher.m_enable = false;
		match.m_homeTeam.RemoveMember(npc);
		match.m_awayTeam.AddMember(npc);
		npc.m_team = match.m_awayTeam;
		match.m_homeTeam.m_role = GameMatch.MatchRole.eDefense;
		match.m_awayTeam.m_role = GameMatch.MatchRole.eOffense;
		mainRole.m_defenseTarget = npc;
		npc.m_defenseTarget = mainRole;
		mainRole.m_AOD = new AOD(mainRole);
		npc.m_AOD = new AOD(npc);
		npc.m_enableMovement = true;
		mainRole.m_aiMgr = new AISystem_PractiseLayupDunk(match, mainRole, AIState.Type.ePractiseLayupDunk_Defense);
		mainRole.m_alwaysForbiddenPickup = true;
		step = Step.ShowMeYourAttack;
	}

	private void Step_DunkTutor()
	{
		plot.Hide();
		match.ShowTouchGuide(null, true);
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = tips["TipDunkTutor"];
		step = Step.DunkTutor;
		layupDunkFailed = false;
		match.ShowTouchGuide(null, true, -45f);
		match.HighlightButton(0, true);
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/LayupArea") as GameObject;
		layupArea = Object.Instantiate(prefab) as GameObject;
		npcTip.SetActive(true);
		npcTipText.text = tips["RoleTipDunkTutor"];
		npc.m_inputDispatcher.m_enable = true;
	}

	private void Step_DunkFailed()
	{
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.tip = tips["TipDunkRetutor"];
		step = Step.DunkFailed;
		npc.m_inputDispatcher.m_enable = false;
		match.HighlightButton(0, false);
	}

	private void Step_DunkRetutor()
	{
		step = Step.DunkRetutor;
		npcTip.SetActive(true);
		match.ShowTouchGuide(null, true);
		match.ResetPlayerPos();
		match.HideGuideTip();
		match.HideTipArrow();
		layupDunkFailed = false;
		match.m_mainRole.GrabBall(match.mCurScene.mBall);
		match.ShowTouchGuide(null, true, -45f);
		match.HighlightButton(0, true);
		npc.m_inputDispatcher.m_enable = true;
	}

	private void Step_DialogInvite1()
	{
		npcTip.SetActive(false);
		layupArea.SetActive(false);
		match.HideTouchGuide();
		match.HideGuideTip();
		match.HighlightButton(0, false);
		mainRole.m_inputDispatcher.m_enable = false;
		npc.m_inputDispatcher.m_enable = false;
		mainRole.m_aiMgr = null;
		plot.Show(0, mainRole.m_id, tips["DialogInvite1"], DIALOG_DELAY);
		step = Step.DialogInvite1;
	}

	private void Step_DialogInvite2()
	{
		plot.Show(1, npc.m_id, tips["DialogInvite2"], DIALOG_DELAY);
		step = Step.DialogInvite2;
	}

	private void Step_DialogInvite3()
	{
		plot.Show(0, mainRole.m_id, tips["DialogInvite3"], DIALOG_DELAY);
		step = Step.DialogInvite3;
	}

	private void Step_DialogInvite4()
	{
		plot.Show(1, npc.m_id, tips["DialogInvite4"], DIALOG_DELAY);
		step = Step.DialogInvite4;
	}

	private void Step_DialogInvite5()
	{
		plot.Show(0, mainRole.m_id, tips["DialogInvite5"], DIALOG_DELAY);
		step = Step.DialogInvite5;
	}

	private void Step_DefenseTutor()
	{
		step = Step.DefenseTutor;
		ResetPlayerPos();
		match.tip = tips["TipDefenseTutor"];
		match.ShowGuideTip();
		SetUIEffect(npcTip, npc.gameObject.transform);
		npcTipText.text = tips["RoleTipDefense"];
		npcTip.SetActive(true);
		plot.Hide();
		match.HighlightButton(1, true);
		mainRole.m_inputDispatcher = new InputDispatcher(match, mainRole);
		mainRole.m_inputDispatcher.m_enable = false;
		match.mainRole = mainRole;
		npc.GrabBall(match.mCurScene.mBall);
		npc.m_inputDispatcher.m_enable = false;
		npc.m_aiMgr = new AISystem_PractiseBaseGuide(match, npc, AIState.Type.ePractiseBaseGuide_Idle);
	}

	private void Step_DefenseRetutor()
	{
		step = Step.DefenseRetutor;
		plot.Show(1, npc.m_id, tips["DialogRedefense"], DIALOG_DELAY);
		mainRole.m_inputDispatcher.m_enable = false;
		match.HighlightButton(1, false);
	}

	private void Step_Layup()
	{
		step = Step.Layup;
		ResetPlayerPos();
		plot.Hide();
		layupArea.SetActive(true);
		npcTip.SetActive(false);
		match.HighlightButton(1, false);
		mainRole.m_inputDispatcher.m_enable = true;
		npc.GrabBall(match.mCurScene.mBall);
	}

	private void Step_BlockTutor()
	{
		match.ShowGuideTip();
		match.HideTipArrow();
		match.tip = tips["TipBlockTutor"];
		step = Step.BlockTutor;
		Pause();
		InputReader.Instance.enabled = true;
		InputReader.Instance.Update(match);
		match.m_uiController.UpdateBtnCmd();
		mainRole.m_alwaysForbiddenPickup = true;
		npc.m_alwaysForbiddenPickup = true;
		match.HighlightButton(0, true);
	}

	private void Step_DialogJoin1()
	{
		match.HighlightButton(0, false);
		npcTip.SetActive(false);
		Object.Destroy(layupArea);
		mainRole.m_inputDispatcher.m_enable = false;
		npc.m_inputDispatcher.m_enable = false;
		plot.Show(1, npc.m_id, tips["DialogJoin1"], DIALOG_DELAY);
		step = Step.DialogJoin1;
	}

	private void Step_DialogJoin2()
	{
		plot.Show(0, mainRole.m_id, tips["DialogJoin2"], DIALOG_DELAY);
		step = Step.DialogJoin2;
	}

	private void Step_DialogJoin3()
	{
		plot.Show(1, npc.m_id, tips["DialogJoin3"], DIALOG_DELAY);
		step = Step.DialogJoin3;
	}

	private void Step_DialogJoin4()
	{
		plot.Show(0, mainRole.m_id, tips["DialogJoin4"], DIALOG_DELAY);
		step = Step.DialogJoin4;
	}

	private IEnumerator Step_Over()
	{
		step = Step.Complete;
		if (onOver != null)
			onOver();
		plot.Hide();
		match.HideTouchGuide();
		match.HighlightButton(0, false);
		match.HideGuideTip();
		yield return new WaitForSeconds(2f);
		if (MainPlayer.Instance.CreateStep == 2)
		{
			ShowRoleAcquire();
		}
	}

	void ShowRoleAcquire()
	{
		LuaComponent luaCom = UIManager.Instance.CreateUI("Prefab/GUI/RoleAcquirePopup").GetComponent<LuaComponent>();
		var func = luaCom.table["SetData"] as LuaInterface.LuaFunction;
		func.Call(new object[]{luaCom.table, MainPlayer.Instance.PlayerList[0].m_roleInfo.id});
		System.Action onClose = () => {
			NGUITools.Destroy(luaCom.gameObject);
			GameSystem.Instance.mClient.Reset();
			GameSystem.Instance.mClient.mUIManager.curLeagueType = GameMatch.LeagueType.eNone;
			SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
		};
		luaCom.table.Set("onClose", onClose);
		UIManager.Instance.BringPanelForward(luaCom.gameObject);
	}
}
