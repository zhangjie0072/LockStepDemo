using System.Collections;
using UnityEngine;
using fogs.proto.msg;

public class PractiseBehaviourRule : PractiseBehaviour,GameMatch.Count24Listener
{
    public enum Step
    {
        Begin,
        MatchTime,
        Score,
        Goal2PT,
        Goal3PT,
        Countdown24,
		Countdown24_3,
		Countdown24_Timeup,
		SwitchRole,
		CheckBall,
		Final,
        Complete,
    }
    private Step _step;
	public Step step { get { return _step; } }

    private const float WAIT_SECONDS = 0.2f;

    private GameObject _arrow_indicator_prefab;
    private GameObject _arrow_indicator;
    private GameObject _box_highlight_prefab;
    private GameObject[] _box_highlight = new GameObject[2];
    private GameObject _indicator_area_prefab;
    private GameObject _indicator_area_2pt;
    private GameObject _indicator_area_3pt;

	public Player npc1 { get; private set; }
	public Player npc2 { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();
        _arrow_indicator_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ArrowIndicator") as GameObject;
        _box_highlight_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/BoxHighlight") as GameObject;
        _indicator_area_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/Area") as GameObject;
    }

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eAway;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseRule_Idle;
	}

	protected override void OnMatchSetted()
	{
		base.OnMatchSetted();

		npc1 = match.m_mainRole;
		npc2 = match.m_awayTeam.GetMember(0);
	}

	public override bool ResetPlayerPos()
	{
		if (_step == Step.Countdown24)
		{
			ShowNPC2();
            npc2.position = GameSystem.Instance.MatchPointsConfig.FreeThrowCenter.transform.position;
			npc2.forward = -IM.Vector3.forward;
            //GameObject centerOf3PT = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/MatchPoints/3PTCenter") as GameObject;
            //npc1.position = new IM.Vector3(centerOf3PT.transform.position);
            //npc1.forward = IM.Vector3.forward;
            npc1.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position;
            npc1.forward = IM.Vector3.forward;
			npc1.GrabBall(match.mCurScene.mBall);
		}
		else if (_step == Step.SwitchRole)
		{
			return false;
		}
		else
		{
            npc1.position = GameSystem.Instance.MatchPointsConfig.ThreePTCenter.transform.position;
			npc1.forward = -IM.Vector3.forward;
			HideNPC2();
		}
		return true;
	}

    public override IM.BigNumber AdjustShootRate(Player shooter, IM.BigNumber rate)
	{
        if (shooter == npc1)
            return IM.BigNumber.one;
        else
            return IM.Number.zero;
	}

    public override ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.BigNumber rate)
	{
		if (shooter == npc2)
			return GameSystem.Instance.shootSolutionManager.GetShootSolution(26, false, 6);
		else
			return null;
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();

        GameObject indicator_area = GameObject.Instantiate(_indicator_area_prefab) as GameObject;
		_indicator_area_2pt = indicator_area.transform.FindChild("2PT").gameObject;
		_indicator_area_3pt = indicator_area.transform.FindChild("3PT").gameObject;
		_indicator_area_2pt.SetActive(false);
		_indicator_area_3pt.SetActive(false);

		NGUITools.SetActive(match.m_uiController.gameObject, false);
		npc1.m_inputDispatcher.m_enable = false;
		npc1.m_InfoVisualizer.ShowStaminaBar(false);
		npc1.m_InfoVisualizer.DestroyStrengthBar();
		npc1.m_aiMgr = new AISystem_PractiseRule(match, npc1, AIState.Type.ePractiseRule_Idle);

		match.mCurScene.mBasket.onGoal += OnGoal;
		match.mCurScene.mBall.onGrab += OnGrab;
	}

    protected override void OnStart()
    {
		_free_mode = false;

        base.OnStart();

        match.HideGuideTip();
        match.HideSignal();
        match.HideTitle();
        match.HideBackButton();
        match.onTipClick = OnTipClick;

		match.mCurScene.mBall.onHitGround += OnHitGround;

        if (match.m_uiMatch == null)
        {
            GameObject uiMatch = GameSystem.Instance.mClient.mUIManager.CreateUI("UIMatch");
            match.m_uiMatch = uiMatch.GetComponent<UIMatch>();
			match.m_uiMatch.digitCount = 2;
			match.m_uiMatch.leftScoreBoard.gap = 8;
			match.m_uiMatch.rightScoreBoard.gap = 8;
        }

        NGUITools.Destroy(_arrow_indicator);
        NGUITools.Destroy(_box_highlight[0]);
        NGUITools.Destroy(_box_highlight[1]);
        match.m_uiMatch.mCounter24.gameObject.SetActive(false);
		_indicator_area_2pt.SetActive(false);
		_indicator_area_3pt.SetActive(false);

		HideNPC2();

        StartCoroutine(Step_Begin());
    }

	protected override void OnUpdate()
	{
		base.OnUpdate();

		if (_step == Step.CheckBall && !match.mCurScene.mGround.In3PointRange(npc1.position.xz, IM.Number.zero))
			StartCoroutine(Step_Final());
	}

	private void ShowNPC2()
	{
		npc2.m_aiMgr.m_enable = true;
		npc2.gameObject.SetActive(true);
		npc2.m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
		npc2.m_StateMachine.SetState(PlayerState.State.eStand, true);
	}

	private void HideNPC2()
	{
		npc2.m_aiMgr.m_enable = false;
		npc2.gameObject.SetActive(false);
		npc2.m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
	}

    private void OnTipClick(GameObject go)
    {
        switch (_step)
        {
            case Step.Begin:
                StartCoroutine(Step_MatchTime());
                break;
            case Step.MatchTime:
                Step_Score();
                break;
            case Step.Score:
				Pause(false);
                StartCoroutine(Step_Goal2PT());
                break;
            case Step.Goal2PT:
				StartCoroutine(Step_Goal3PT());
				break;
			case Step.Goal3PT:
                StartCoroutine(Step_Countdown24());
                break;
            case Step.Countdown24:
				Step_Countdown24_3();
                break;
			case Step.Countdown24_3:
				Pause(false);
				match.HideTipArrow();
				break;
			case Step.Countdown24_Timeup:
				StartCoroutine(Step_SwitchRole());
				break;
			case Step.CheckBall:
				Pause(false);
				match.HideGuideTip();
				break;
			case Step.Final:
				Step_Complete();
				break;
        }
    }

    private IEnumerator Step_Begin()
    {
        _step = Step.Begin;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();
        match.m_gameMatchCountStop = true;
        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
    }

    private IEnumerator Step_MatchTime()
    {
        _step = Step.MatchTime;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

        _arrow_indicator = CommonFunction.InstantiateObject(_arrow_indicator_prefab, match.m_uiMatch.transform);
        _arrow_indicator.transform.FindChild("Tip").GetComponent<UILabel>().text = "比赛\n时间";

        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
    }

    private void Step_Score()
    {
        _step = Step.Score;

        NGUITools.Destroy(_arrow_indicator);
		match.HideGuideTip();
    }

	private void Step_Score_Display()
	{
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

        _box_highlight[0] = CommonFunction.InstantiateObject(_box_highlight_prefab, 
            match.m_uiMatch.transform.FindChild("LeftName").transform);
        _box_highlight[1] = CommonFunction.InstantiateObject(_box_highlight_prefab, 
            match.m_uiMatch.transform.FindChild("RightName").transform);

        match.ShowTipArrow();
	}

    private IEnumerator Step_Goal2PT()
    {
        _step = Step.Goal2PT;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

        NGUITools.Destroy(_box_highlight[0]);
        NGUITools.Destroy(_box_highlight[1]);

		_indicator_area_2pt.SetActive(true);

        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
    }

    private IEnumerator Step_Goal3PT()
    {
        _step = Step.Goal3PT;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

		_indicator_area_2pt.SetActive(false);
		_indicator_area_3pt.SetActive(true);

        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
    }

    private IEnumerator Step_Countdown24()
    {
        _step = Step.Countdown24;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

		_indicator_area_3pt.SetActive(false);

        match.m_uiMatch.ShowCounter(true, true);
        match.m_count24Time = new IM.Number(24);
        match.m_count24TimeStop = true;
        _arrow_indicator = CommonFunction.InstantiateObject(_arrow_indicator_prefab, match.m_uiMatch.mCounter24.transform);
        _arrow_indicator.transform.FindChild("Tip").gameObject.SetActive(false);
        _arrow_indicator.transform.FindChild("TipBG").gameObject.SetActive(false);

		match.ResetPlayerPos();

        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
    }

	private void Step_Countdown24_3()
	{
		_step = Step.Countdown24_3;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();

        match.m_count24Time = new IM.Number(3, 100);
        match.m_count24TimeStop = false;
        match.AddCount24Listener(this);
        match.m_gameMatchCountStop = false;
		Pause();

        match.ShowTipArrow();
	}

	public void OnTimeUp()
	{
        match.m_gameMatchCountStop = true;
		StartCoroutine(Step_Countdown24_Timeup());
	}

	private IEnumerator Step_Countdown24_Timeup()
	{
		_step = Step.Countdown24_Timeup;
        match.tip = practise.tips[(int)_step];
        match.ShowGuideTip();
        match.HideTipArrow();
		match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24, true);
        yield return new WaitForSeconds(WAIT_SECONDS);
        match.ShowTipArrow();
	}

	private IEnumerator Step_SwitchRole()
	{
		_step = Step.SwitchRole;
        match.HideGuideTip();
        match.HideTipArrow();
		match.m_uiMatch.ShowMsg(UIMatch.MSGType.eFoul_24, false);
		NGUITools.Destroy(_arrow_indicator);
		_arrow_indicator = null;
		match.m_ruler.SwitchRole();
        match.m_gameMatchCountStop = false;
		match.ResetPlayerPos();
		if (match.mCurScene.mBall.m_owner != null)
			match.mCurScene.mBall.m_owner.DropBall(match.mCurScene.mBall);
		npc2.GrabBall(match.mCurScene.mBall);
        yield return new WaitForSeconds(WAIT_SECONDS);
	}

	private IEnumerator Step_CheckBall()
	{
		_step = Step.CheckBall;
		match.tip = practise.tips[(int)_step - 1];
		match.ShowGuideTip();
		match.ShowTipArrow();
		Pause();
		yield return new WaitForSeconds(WAIT_SECONDS);
	}

	private IEnumerator Step_Final()
	{
		_step = Step.Final;
		yield return new WaitForSeconds(2f);
		match.tip = practise.tips[(int)_step - 1];
		match.ShowGuideTip();
		match.ShowTipArrow();
		match.m_uiMatch.ShowCounter(false, true);
        match.m_gameMatchCountStop = true;
	}

    private void Step_Complete()
    {
        _step = Step.Complete;
        match.HideGuideTip();

        match.m_uiMatch.mCounter24.gameObject.SetActive(false);
        NGUITools.Destroy(_arrow_indicator);

		if (!_free_mode && !MainPlayer.Instance.IsPractiseCompleted(practise.ID))
			StartCoroutine(base.Step_Complete(true));
		else
			StartCoroutine(ReturnToListLater());
    }

	private IEnumerator ReturnToListLater()
	{
		yield return new WaitForSeconds(2);
		ReturnToList();
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		match.m_homeScore += 2;
	}

	private void OnHitGround(UBasketball ball)
	{
		Step_Score_Display();
		match.mCurScene.mBall.onHitGround -= OnHitGround;
		Pause();
	}

	private void OnGrab(UBasketball ball)
	{
		if (_step == Step.SwitchRole && ball.m_owner == npc1)
		{
			StartCoroutine(Step_CheckBall());
		}
	}
}