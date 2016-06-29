using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PractiseBehaviourGuide : PractiseBehaviour
{
	const float DIALOG_DELAY = 5f;

	private List<Player> playerList { get { return GameSystem.Instance.mClient.mPlayerManager.m_Players; } }
	private UBasketball ball;

	private List<GameObject> tipOnScene = new List<GameObject>();
	private List<GameObject> effects = new List<GameObject>();
	private UIPlayPlot plot;
	private GuideTip guideTip;
	private GameObject paneObjective;
	private GameObject prefabObjFinish;

	private Dictionary<int, GameObject> objectives = new Dictionary<int, GameObject>();
	private Dictionary<int, bool> objStatus = new Dictionary<int, bool>();
	private int lastObjID;
	private PractiseStep curStep;
	private uint readyStepID;
	private bool stepFinished;
	private bool stepFailed;
	private float stepRunningTime;

	public System.Action onOver;

	public PractiseStepBehaviour GetBehaviour(int index)
	{
		PractiseStepBehaviour behaviour = PractiseStepBehaviour.None;
		if (!stepFinished && curStep != null)
		{
			curStep.behaviour.TryGetValue(index, out behaviour);
		}
		return behaviour;
	}

	public override bool IsCommandValid(Command command)
	{
		if (!stepFinished && curStep != null)
			return (curStep.action != null && curStep.action.Contains(command) && 
				(curStep.disabledAction == null || !curStep.disabledAction.Contains(command)));
		return false;
	}

    public override IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
		if (curStep.mustGoal)
            return IM.PrecNumber.one;
		else if (curStep.cantGoal)
            return IM.PrecNumber.zero;
		return rate;
	}

    public override IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
		if (curStep.mustBlock)
            return IM.Number.one;
		return rate;
	}

    public override IM.Number AdjustCrossRate(Player crosser, Player defender, IM.Number rate)
	{
		if (curStep.mustCross)
            return IM.Number.one;
		return rate;
	}
    public override ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.PrecNumber rate)
	{
		if (curStep.mustGoal || curStep.cantGoal)
		{
			if (curStep.shootSolution.Key != 0 || curStep.shootSolution.Value != 0)
				return GameSystem.Instance.shootSolutionManager.GetShootSolution(curStep.shootSolution.Key, curStep.mustGoal, curStep.shootSolution.Value);
		}
		return null;
	}

	public override AIState.Type GetInitialAIState()
	{
		return AIState.Type.ePractiseGuide_Idle;
	}

	public override Team.Side GetNPCSide()
	{
		return Team.Side.eHome;
	}

	public override bool ResetPlayerPos()
	{
		return true;
	}

	protected override void OnMatchSetted()
	{
		base.OnMatchSetted();

		ball = match.mCurScene.mBall;

		for (int i = 0; i < playerList.Count; ++i)
		{
			Player player = playerList[i];
            player.operMode = Player.OperMode.AI;
			(player.m_aiMgr as AISystem_PractiseGuide).index = i;
            player.operMode = Player.OperMode.None;
			if (player.m_AOD == null)
				player.m_AOD = new AOD(player);
			player.m_StateMachine.onStateChanged += OnPlayerStateChanged;
		}

		Animator animator = match.m_uiController.GetComponent<Animator>();
		Object.Destroy(animator);

        match.turnManager.onNewTurn += OnNewTurn;
	}

	protected override void OnFirstStart()
	{
		base.OnFirstStart();

		plot = UIManager.Instance.CreateUI("UIPlayPlot").GetComponent<UIPlayPlot>();
		plot.Hide();
		plot.onNext = OnPlotNext;

		match.HideSignal();
		match.HideTitle();
		match.HideBackButton();

		guideTip = UIManager.Instance.CreateUI("GuideTip_3").AddMissingComponent<GuideTip>();
		guideTip.transform.localPosition = new Vector3(18, -316, 0);
		guideTip.firstButtonVisible = false;
		guideTip.Hide();


		foreach (UController.Button btn in match.m_uiController.m_btns)
		{
			if (btn.btn != null)
				UIEventListener.Get(btn.btn.gameObject).onPress += OnBtnPress;
		}

		match.onTipClick += OnTipClick;
		ball.onGrab += OnGrab;
		ball.onCatch += OnCatch;
		ball.onShoot += OnShoot;
		ball.onHitGround += OnHitGround;
		match.mCurScene.mBasket.onGoal += OnGoal;

		prefabObjFinish = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/MatchGuideObjectiveFinish") as GameObject;
		paneObjective = UIManager.Instance.CreateUI("MatchGuideObjectivePane");
		UIGrid gridObj = paneObjective.transform.FindChild("Grid").GetComponent<UIGrid>();
        GameObject prefabItem = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/MatchGuideObjectiveItem") as GameObject;

		PractiseStep step = GameSystem.Instance.PractiseStepConfig.GetFirstStep();
		readyStepID = step.ID;
		while (true)
		{
			if (step.startObjective.Key != 0)
			{
				lastObjID = step.startObjective.Key;
				GameObject item = CommonFunction.InstantiateObject(prefabItem, gridObj.transform);
				item.GetComponentInChildren<UILabel>().text = step.startObjective.Value;
				objectives.Add(step.startObjective.Key, item);
			}
			if (step.next == 0)
				break;
			else
				step = GameSystem.Instance.PractiseStepConfig.GetStep(step.next);
		}
		gridObj.Reposition();
	}

	protected override void OnStart()
	{
		//there's no free mode in this practise
		_free_mode = false;

		base.OnStart();
	}

    public override void ViewUpdate(float deltaTime)
    {
        base.ViewUpdate(deltaTime);

#if !UNITY_IPHONE && !UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyUp(KeyCode.J))
        {
            if (IsCommandValid(match.m_uiController.m_btns[0].cmd))
                RestoreTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyUp(KeyCode.K))
        {
            if (IsCommandValid(match.m_uiController.m_btns[1].cmd))
                RestoreTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyUp(KeyCode.L))
        {
            if (IsCommandValid(match.m_uiController.m_btns[2].cmd))
                RestoreTimeScale();
        }

        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyUp(KeyCode.I))
        {
            if (IsCommandValid(match.m_uiController.m_btns[3].cmd))
                RestoreTimeScale();
        }

        if (Input.GetKey(KeyCode.J))
            UnhighlightButton(match.m_uiController.m_btns[0].btn.gameObject);
        if (Input.GetKey(KeyCode.K))
            UnhighlightButton(match.m_uiController.m_btns[1].btn.gameObject);
        if (Input.GetKey(KeyCode.L))
            UnhighlightButton(match.m_uiController.m_btns[2].btn.gameObject);
        if (Input.GetKey(KeyCode.I))
            UnhighlightButton(match.m_uiController.m_btns[3].btn.gameObject);

#else
        foreach (UController.Button btn in match.m_uiController.m_btns)
        {
            if (btn != null && btn.btn != null && btn.btn.isPressed)
                UnhighlightButton(btn.btn.gameObject);
        }
#endif
    }

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		if (curStep != null)
		{
			stepRunningTime += (float)deltaTime;

			if (HasCondition(PractiseStepCondition.OnGround))
			{
				ValidateCondition(PractiseStepCondition.OnGround, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					return player.m_bOnGround;
				});
			}
			if (HasCondition(PractiseStepCondition.Wait))
			{
				ValidateCondition(PractiseStepCondition.Wait, (param) =>
				{
					float waitTime = (float)(param[0]);
					return stepRunningTime >= waitTime;
				});
			}
			if (HasCondition(PractiseStepCondition.InDist))
			{
				ValidateCondition(PractiseStepCondition.InDist, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					GameObject obj = GetSceneObject((string)(param[1]));
					float dist = (float)(param[2]);
					bool value = (bool)(param[3]);
					return (GameUtils.HorizonalDistance(obj.transform.position, (Vector3)player.position) <= dist) == value;
				});
			}
			if (HasCondition(PractiseStepCondition.EnterRow))
			{
				ValidateCondition(PractiseStepCondition.EnterRow, (param) =>
				{
					int index = (int)(param[0]);
					int rowStart = (int)(param[1]);
					int rowEnd = (int)(param[2]);
					Player player = GetPlayer(index);
					int secIdx = RoadPathManager.Instance.CalcSectorIdx(player.position);
					if (secIdx == -1)
						return false;
					int rowIdx = secIdx / RoadPathManager.Instance.m_angleNum;
					return rowStart <= rowIdx && rowIdx <= rowEnd;
				});
			}
			if (HasCondition(PractiseStepCondition.EnterArea))
			{
				ValidateCondition(PractiseStepCondition.EnterArea, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					Area area = (Area)(param[1]);
					return match.mCurScene.mGround.GetArea(player) == area;
				});
			}
            if (HasCondition(PractiseStepCondition.EnterState))
            {
                ValidateCondition(PractiseStepCondition.EnterState, (param) =>
                {
                    int index = (int)(param[0]);
                    Player player = GetPlayer(index);
                    PlayerState.State state = (PlayerState.State)(param[1]);
                    return state == player.m_StateMachine.m_curState.m_eState;
                });
            }
			if (HasCondition(PractiseStepCondition.BlockTiming))
			{
				ValidateCondition(PractiseStepCondition.BlockTiming, (param) =>
				{
					Player attacker = ball.m_owner;
					if (attacker == null)
						attacker = ball.m_actor;
					if (attacker.m_StateMachine.m_curState.m_eState != PlayerState.State.eShoot &&
						attacker.m_StateMachine.m_curState.m_eState != PlayerState.State.eLayup &&
						attacker.m_StateMachine.m_curState.m_eState != PlayerState.State.eDunk)
						return false;
					if (!PlayerState_Block.InBlockArea(attacker, attacker.m_defenseTarget, match.mCurScene.mBasket.m_vShootTarget))
						return false;
					return attacker.m_blockable.blockable;
				});
			}
			if (HasCondition(PractiseStepCondition.Blocked))
			{
				ValidateCondition(PractiseStepCondition.Blocked, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					if (player.m_StateMachine.m_curState.m_eState != PlayerState.State.eBlock)
						return false;
					PlayerState_Block blockState = player.m_StateMachine.m_curState as PlayerState_Block;
					bool success = (bool)(param[1]);
					return blockState.m_success == success;
				});
			}
			if (HasCondition(PractiseStepCondition.BlockInArea))
			{
				ValidateCondition(PractiseStepCondition.BlockInArea, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					Player attacker = ball.m_owner;
					if (attacker == null)
						attacker = ball.m_actor;
					bool inArea = (bool)(param[1]);
                    return PlayerState_Block.InBlockArea(attacker, player, match.mCurScene.mBasket.m_vShootTarget) == inArea;
				});
			}
			if (HasCondition(PractiseStepCondition.BlockTooLate))
			{
				ValidateCondition(PractiseStepCondition.BlockTooLate, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					if (player.m_StateMachine.m_curState.m_eState != PlayerState.State.eBlock)
						return false;
					PlayerState_Block blockState = player.m_StateMachine.m_curState as PlayerState_Block;
					if (blockState.m_success)
						return false;
					Player attacker = ball.m_owner;
					if (attacker == null)
						attacker = ball.m_actor;
					bool tooLate = (bool)(param[1]);
					if (tooLate)
						return blockState.m_failReason == PlayerState_Block.FailReason.TooLate;
					else
						return blockState.m_failReason == PlayerState_Block.FailReason.TooEarly;
				});
			}
			if (HasCondition(PractiseStepCondition.ReboundTiming))
			{
				ValidateCondition(PractiseStepCondition.ReboundTiming, (param) =>
				{
					if (ball.m_ballState == BallState.eRebound)
					{
						IM.Vector3 velocity = ball.curVel;
						if (velocity.y < IM.Number.zero)
						{
							int index = (int)(param[0]);
							Player player = GetPlayer(index);
							IM.Number eventTime = PlayerState_Rebound.GetEventTime(player);
							IM.Vector3 pos;
							ball.GetPositionInAir(ball.m_fTime+ eventTime, out pos);
                            IM.Number ball_height = pos.y;
							IM.Number minHeight, maxHeight;
							PlayerState_Rebound.GetDefaultHeightRange(player, out minHeight, out maxHeight);
							return minHeight < ball_height && ball_height < maxHeight;
						}
					}
					return false;
				});
			}
			if (HasCondition(PractiseStepCondition.Rebound))
			{
				ValidateCondition(PractiseStepCondition.Rebound, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					if (player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound)
						return false;
					PlayerState_Rebound reboundState = player.m_StateMachine.m_curState as PlayerState_Rebound;
					bool success = (bool)(param[1]);
					if( success )
						reboundState.m_toReboundBall = true;
					return reboundState.m_success == success;
				});
			}
			if (HasCondition(PractiseStepCondition.ReboundInArea))
			{
				ValidateCondition(PractiseStepCondition.ReboundInArea, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					IM.Number dist2Ball = GameUtils.HorizonalDistance(player.position, ball.position);
					IM.Number maxDist = PlayerState_Rebound.GetDefaultMaxDist(player);
					bool inArea = (bool)(param[1]);
					return (dist2Ball <= maxDist) == inArea;
				});
			}
			if (HasCondition(PractiseStepCondition.ReboundTooLate))
			{
				ValidateCondition(PractiseStepCondition.ReboundTooLate, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					if (player.m_StateMachine.m_curState.m_eState != PlayerState.State.eRebound)
						return false;
					PlayerState_Rebound reboundState = player.m_StateMachine.m_curState as PlayerState_Rebound;
					if (reboundState.m_success)
						return false;
					bool tooLate = (bool)(param[1]);
					return reboundState.tooLate == tooLate;
				});
			}
			if (HasCondition(PractiseStepCondition.Undefended))
			{
				ValidateCondition(PractiseStepCondition.Undefended, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					return !player.IsDefended();
				});
			}
			if (HasCondition(PractiseStepCondition.Defended))
			{
				ValidateCondition(PractiseStepCondition.Defended, (param) =>
				{
					int index = (int)(param[0]);
					Player player = GetPlayer(index);
					return player.IsDefended();
				});
			}

		}

		if (stepFinished)
			EndStep();

		if (readyStepID != 0)
		{
			curStep = GameSystem.Instance.PractiseStepConfig.GetStep(readyStepID);
			readyStepID = 0;
			RunStep();
			return;
		}
	}

	void RunStep()
	{
		Debug.Log("PractiseStep: Run step:" + curStep.ID + " " + curStep.name);

		stepFailed = false;

		StartObjective();
		ShowDialog();
		ShowVoiceover();
		SetOppo();
		SetCanPick();
		SetVisibility();
		SetBallOwner();
		PositionObjects();
		OrientateObjects();
		SetBehaviour();
		SetActionHint();
		SetDisabledAction();
		SetScreenEffect();
		SetSceneEffect();
		SetSceneTip();
		SetTimeScale();

		stepRunningTime = 0f;
	}

	void EndStep()
	{
		Debug.Log("PractiseStep, End step:" + curStep.ID + " Success:" + !stepFailed);

		if (!stepFailed)
			EndObjective();

		stepFinished = false;

		plot.Hide();
		guideTip.Hide();
		foreach (GameObject tip in tipOnScene)
		{
			DestroyUIEffect(tip);
		}
		tipOnScene.Clear();
		foreach (GameObject effect in effects)
		{
			Object.DestroyImmediate(effect);
		}
		effects.Clear();
		if (curStep.disabledAction != null && InputReader.Instance.player != null)
		{
			foreach (Command cmd in curStep.disabledAction)
			{
				InputReader.Instance.player.m_skillSystem.CancelDisableCommand(cmd);
			}
		}
		UnhighlightButton();

		readyStepID = stepFailed ? curStep.failNext : curStep.next;
		Debug.Log("PractiseStep, Ready step:" + readyStepID);
		curStep = null;
		if (!stepFailed && readyStepID == 0)
			StartCoroutine(Step_Over());
	}

	void StartObjective()
	{
		if (curStep.startObjective.Key == 0)
			return;
		Debug.Log("PractiseStep, step:" + curStep.ID + " Start objective: " + curStep.startObjective.Key + ", " + curStep.startObjective.Value);
		GameObject item;
		if (objectives.TryGetValue(curStep.startObjective.Key, out item))
		{
			item.GetComponentInChildren<UILabel>().color = Color.yellow;
		}
	}

	void EndObjective()
	{
		if (curStep.endObjective == 0)
			return;
		Debug.Log("PractiseStep, step:" + curStep.ID + " End objective: " + curStep.endObjective);
		GameObject effectFinish = CommonFunction.InstantiateObject(prefabObjFinish, UIManager.Instance.m_uiRootBasePanel.transform);
		Object.Destroy(effectFinish, 2f);
		GameObject item;
		if (objectives.TryGetValue(curStep.endObjective, out item))
		{
			item.GetComponentInChildren<Animator>().SetTrigger("Close");
			item.GetComponentInChildren<UILabel>().color = Color.gray;
		}

		FirstFightGuideModuleReq req = new FirstFightGuideModuleReq();
		req.sub_id = (GuideFightAction)curStep.endObjective;
		Debug.Log("sub_id: " + req.sub_id);

		GameSystem.Instance.mNetworkManager.m_platConn.SendPack<FirstFightGuideModuleReq>(0, req, MsgID.FirstFightGuideModuleReqID);

		if (curStep.endObjective == lastObjID)
			StartCoroutine(CloseObjPaneLater(2f));
	}

	IEnumerator CloseObjPaneLater(float delay)
	{
		yield return new WaitForSeconds(delay);
		paneObjective.GetComponent<Animator>().SetTrigger("Close");
	}

	void ShowDialog()
	{
		if (string.IsNullOrEmpty(curStep.dialog))
			return;
		Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByIndex(curStep.dialogRole);
        if (player != null)
            plot.Show(1, player.m_id, curStep.dialog, DIALOG_DELAY);
        else
            Debug.LogError("get player failed by index " + curStep.dialogRole);
	}

	void ShowVoiceover()
	{
		if (string.IsNullOrEmpty(curStep.voiceover))
			return;
		guideTip.tip = curStep.voiceover;
		guideTip.Show();
	}

	void SetOppo()
	{
		if (curStep.oppo != null)
		{
			foreach (Player player in playerList)
			{
				if (player.m_team != null)
				{
					player.m_team.RemoveMember(player);
					player.m_team = null;
				}
				player.m_defenseTarget = null;
			}
			foreach (KeyValuePair<int, int> pair in curStep.oppo)
			{
				Player offenser = null, defender = null;
				if (pair.Key >= 0)
				{
					offenser = GetPlayer(pair.Key);
					match.m_offenseTeam.AddMember(offenser);
					offenser.m_team = match.m_offenseTeam;
				}
				if (pair.Value >= 0)
				{
					defender = GetPlayer(pair.Value);
					match.m_defenseTeam.AddMember(defender);
					defender.m_team = match.m_defenseTeam;
				}
				if (offenser != null && defender != null)
				{
					offenser.m_defenseTarget = defender;
					defender.m_defenseTarget = offenser;
				}
			}
		}
	}

	void SetCanPick()
	{
		for (int i = 0; i < playerList.Count; ++i)
		{
			playerList[i].m_alwaysForbiddenPickup = (curStep.canPick == null || !curStep.canPick.Contains(i));
		}
	}

	void SetVisibility()
	{
		if (curStep.visible == null)
			return;
		foreach (KeyValuePair<int, bool> pair in curStep.visible)
		{
			if (pair.Key == -1)
				ball.gameObject.SetActive(pair.Value);
			else
			{
				Player player = GetPlayer(pair.Key);
				if (pair.Value)
					player.Show();
				else
					player.Hide();
			}
		}
	}

	void SetBallOwner()
	{
		if (curStep.ballOwner == -2)
			return;
		else if (curStep.ballOwner == -1)
		{
			if (ball.m_owner != null)
				ball.m_owner.DropBall(ball);
		}
		else
		{
			Player ballOwner = GetPlayer(curStep.ballOwner);
			if (ballOwner != ball.m_owner)
			{
				if (ball.m_owner != null)
					ball.m_owner.DropBall(ball);
				ballOwner.GrabBall(ball);
			}
		}
	}

	void PositionObjects()
	{
		if (curStep.position == null)
			return;
		foreach (KeyValuePair<int, IM.Vector3> pair in curStep.position)
		{
			if (pair.Key == -1)
			{
				ball.SetInitPos(pair.Value);
			}
			else
			{
				Player player = GetPlayer(pair.Key);
				player.position = pair.Value;
				if (player.m_bWithBall)
					player.m_bMovedWithBall = false;
                if (curStep.timeScale == 0)
                    //player.Update(new IM.Number(Time.deltaTime));//不明白此处的意思，先暂时改为0
                    player.GameUpdate(IM.Number.zero);
                
			}
		}
	}

	void OrientateObjects()
	{
		if (curStep.orientation == null)
			return;
		foreach (KeyValuePair<int, string> pair in curStep.orientation)
		{
			Player player = GetPlayer(pair.Key);
			GameObject faceToObj = GetSceneObject(pair.Value);
            //此处只用于PVE，使用从float转换为IM.Number不会有问题
            Vector3 faceToPoint = faceToObj.transform.position;
            IM.Number x = IM.Number.Raw((int)faceToPoint.x * IM.Math.FACTOR);
            IM.Number y = IM.Number.Raw((int)faceToPoint.y * IM.Math.FACTOR);
            IM.Number z = IM.Number.Raw((int)faceToPoint.z * IM.Math.FACTOR);
			player.FaceTo(new IM.Vector3(x, y, z));
		}
	}

	void SetBehaviour()
	{
		bool hasController = false;
		for (int i = 0; i < playerList.Count; ++i)
		{
			Player player = playerList[i];
			PractiseStepBehaviour behaviour;
			if (curStep.behaviour != null && curStep.behaviour.TryGetValue(i, out behaviour))
			{
				if (behaviour == PractiseStepBehaviour.PlayerControl)
				{
					hasController = true;
                    player.operMode = Player.OperMode.Input;
					player.m_inputDispatcher.m_enableMove = curStep.canMove;
                    InputReader.Instance.player = player;
                    InputReader.Instance.enabled = true;
					if (curStep.timeScale == 0f)
						InputReader.Instance.Update(match);
                    Color yellow = new Color(1f, 252f / 255, 10f / 255, 1);
                    player.ShowIndicator(yellow, true);
					match.m_cam.m_trLook = player.gameObject.transform;
				}
				else
				{
                    player.operMode = Player.OperMode.AI;
					player.HideIndicator();
				}
			}
			else
			{
                player.operMode = Player.OperMode.None;
				player.HideIndicator();
			}
		}
        if (!hasController)
            InputReader.Instance.enabled = false;
	}

	void SetActionHint()
	{
		if (curStep.hintMove)
		{
			match.ShowTouchGuide(null, true, curStep.hintMoveAngle);
		}
		if (curStep.hintAction != null)
		{
			foreach (Command hintCmd in curStep.hintAction)
			{
				UController.Button btn = match.m_uiController.GetButton(hintCmd);
				match.HighlightButton(btn.index, true);
			}
		}
	}

	void SetDisabledAction()
	{
		// set disabled action
		if (curStep.disabledAction != null && InputReader.Instance.player != null)
		{
			foreach (Command cmd in curStep.disabledAction)
			{
				InputReader.Instance.player.m_skillSystem.DisableCommand(cmd);
			}
		}
	}

	void SetScreenEffect()
	{
		if (!string.IsNullOrEmpty(curStep.screenEffect))
		{
			GameObject prefab = ResourceLoadManager.Instance.GetResources("Prefab/GUI/" + curStep.screenEffect) as GameObject;
			if (prefab == null)
				Debug.LogError("PractiseStep, Can not find screen effect:" + curStep.screenEffect + " step: " + curStep.ID);
			GameObject effect = Object.Instantiate(prefab) as GameObject;
			effects.Add(effect);
			effect.transform.SetParent(UIManager.Instance.m_uiRootBasePanel.transform, false);
			if (effect.GetComponent<UIManagedPanel>() != null)
			{
				UIManager.Instance.BringPanelForward(effect);
			}
			if (HasCondition(PractiseStepCondition.ClickScreenEffect))
			{
				BoxCollider collider = effect.GetComponentInChildren<BoxCollider>();
				if (collider != null)
					UIEventListener.Get(collider.gameObject).onClick += OnClickScreenEffect;
				else
					Debug.LogError("No box collider in screen effect. step: " + curStep.ID);
			}
		}
	}

	void SetSceneEffect()
	{
		if (curStep.sceneEffect != null)
		{
			foreach (KeyValuePair<string, string> pair in curStep.sceneEffect)
			{
				GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/Indicator/" + pair.Value);
				if (prefab == null)
					Debug.LogError("PractiseStep, Can not find scene effect:" + pair.Value + " step: " + curStep.ID);
				GameObject effect = Object.Instantiate(prefab) as GameObject;
				effects.Add(effect);
				if (!string.IsNullOrEmpty(pair.Key))
				{
					GameObject parent = GetSceneObject(pair.Key);
					// keep world rotation and scale but local position
					Vector3 localPosition = effect.transform.localPosition;
					effect.transform.SetParent(parent.transform, true);
					effect.transform.localPosition = localPosition;
				}
			}
		}
	}

	void SetSceneTip()
	{
		if (curStep.sceneHint != null)
		{
			foreach (KeyValuePair<string, string> pair in curStep.sceneHint)
			{
				GameObject obj = GetSceneObject(pair.Key);
				GameObject tip = CreateUIEffect("Prefab/GUI/TipOnScene", obj.transform);
				tipOnScene.Add(tip);
				UILabel tipText = tip.transform.FindChild("Tip").GetComponent<UILabel>();
				tipText.text = pair.Value;
			}
		}
	}

	void SetTimeScale()
	{
		GameSystem.Instance.mClient.timeScale = curStep.timeScale;
        VirtualGameServer.Instance.timeScale = curStep.timeScale;
		if (curStep.timeScale == 0f)
		{
			match.m_uiController.UpdateBtnCmd();
		}
	}

    void RestoreTimeScale()
    {
        GameSystem.Instance.mClient.timeScale = 1f;
        VirtualGameServer.Instance.timeScale = 1f;
        VirtualGameServer.Instance.Resume();
    }

	bool HasCondition(PractiseStepCondition cond)
	{
		return !stepFinished && curStep != null && (
			(curStep.endCondition != null && curStep.endCondition.FindAll((pair) => pair.Key == cond).Count > 0) ||
			(curStep.failCondition != null && curStep.failCondition.FindAll((pair) => pair.Key == cond).Count > 0));
	}

	void ValidateCondition(PractiseStepCondition cond, System.Predicate<object[]> validator)
	{
		if (stepFinished || curStep == null)
			return;
		if (curStep.failCondition != null)
		{
			foreach (KeyValuePair<PractiseStepCondition, object[]> pair in curStep.failCondition)
			{
				if (pair.Key == cond && validator(pair.Value))
				{
					Debug.Log("PractiseStep, Fail condition:" + cond + " matched. step:" + curStep.ID);
					stepFailed = true;
					stepFinished = true;
					return;
				}
			}
		}
		if (curStep.endCondition != null)
		{
			foreach (KeyValuePair<PractiseStepCondition, object[]> pair in curStep.endCondition)
			{
				if (pair.Key == cond && validator(pair.Value))
				{
					Debug.Log("PractiseStep, End condition:" + cond + " matched. step:" + curStep.ID);
					stepFinished = true;
					return;
				}
			}
		}
	}

	Player GetPlayer(int index)
	{
		Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByIndex(index);
		if (player == null)
			Debug.LogError("PractiseStep, Can't find player, index: " + index + " step: " + curStep.ID);
		return player;
	}

	GameObject GetSceneObject(string tagOrName)
	{
		GameObject obj;
		try
		{
			obj = GameObject.FindWithTag(tagOrName);
		}
		catch (UnityException ex)
		{
			obj = GameObject.Find(tagOrName);
		}
		if (obj == null)
			Debug.LogError("PractiseStep, Can't find scene obj, name or tag: " + tagOrName + " step:" + curStep.ID);
		return obj;
	}

	private void OnPlotNext()
	{
		if (string.IsNullOrEmpty(curStep.dialog))
			Debug.LogError("PractiseStep, OnPlotNext, cur step has no dialog. Step: " + curStep.ID);
		stepFinished = true;
	}

	private void OnTipClick(GameObject go)
	{
	}

    private void OnNewTurn(FrameInfo turn)
    {
        ClientInput input = turn.info[0];
        Command command = (Command)input.cmd;
        if (command == Command.Block)
            Debug.DebugBreak();
        ValidateCondition(PractiseStepCondition.ButtonPress,
            (param) =>
            {
                Command cmd = (Command)(param[0]);
                return command == cmd;
            });
    }

	private void OnBtnPress(GameObject go, bool press)
	{
		if (stepFinished || curStep == null)
			return;
        foreach (UController.Button btn in match.m_uiController.m_btns)
        {
            if (btn.btn.gameObject == go)
            {
                if (IsCommandValid(btn.cmd))
                    RestoreTimeScale();
            }
        }
		//UnhighlightButton(go);
        /*
		ValidateCondition(press ? PractiseStepCondition.ButtonPress : PractiseStepCondition.ButtonRelease,
			(param) =>
			{
				Command cmd = (Command)(param[0]);
				UController.Button btn = match.m_uiController.GetButton(cmd);
				return btn.btn.gameObject == go && btn.cmd == cmd;
			});
        */
	}

	private void UnhighlightButton(GameObject go = null)
	{
		if (stepFinished || curStep == null)
			return;
		if (curStep.hintAction != null)
		{
			foreach (Command cmd in curStep.hintAction)
			{
				UController.Button btn = match.m_uiController.GetButton(cmd);
				if (btn.btn.gameObject == go || go == null)
					match.HighlightButton(btn.index, false);
			}
		}
	}

	private void OnGrab(UBasketball ball)
	{
		ValidateCondition(PractiseStepCondition.GrabBall, (param) =>
		{
			int index = (int)(param[0]);
			return ball.m_owner == GetPlayer(index);
		});
	}

	private void OnCatch(UBasketball ball)
	{
		ValidateCondition(PractiseStepCondition.CatchBall, (param) =>
		{
			int index = (int)(param[0]);
			return ball.m_owner == GetPlayer(index);
		});
	}

	private void OnShoot(UBasketball ball)
	{
	}

	private void OnHitGround(UBasketball ball)
	{
		ValidateCondition(PractiseStepCondition.HitGround, (param) => true);
	}

	private void OnGoal(UBasket basket, UBasketball ball)
	{
		ValidateCondition(PractiseStepCondition.Goal, (param) => true);
	}

	private void OnPlayerStateChanged(PlayerState oldState, PlayerState newState)
	{
		ValidateCondition(PractiseStepCondition.EnterState, (param) =>
		{
			int index = (int)(param[0]);
			if (newState.m_player != GetPlayer(index))
				return false;
			PlayerState.State state = (PlayerState.State)(param[1]);
			return state == newState.m_eState;
		});
		if (newState.m_eState == PlayerState.State.eHold && newState.m_player.m_bMovedWithBall)
		{
			ValidateCondition(PractiseStepCondition.DoubleDribble, (param) =>
			{
				int index = (int)(param[0]);
				return newState.m_player == GetPlayer(index);
			});
		}
	}

	void OnClickScreenEffect(GameObject go)
	{
		ValidateCondition(PractiseStepCondition.ClickScreenEffect, (param) => true);
	}

	private IEnumerator Step_Over()
	{
		if (onOver != null)
			onOver();
		yield return new WaitForSeconds(2f);
		GameSystem.Instance.mClient.Reset();
		GameSystem.Instance.mClient.mUIManager.curLeagueType = GameMatch.LeagueType.eNone;
		SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);

		//if (MainPlayer.Instance.CreateStep == 2)
		//{
		//	ShowRoleAcquire();
		//}
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
