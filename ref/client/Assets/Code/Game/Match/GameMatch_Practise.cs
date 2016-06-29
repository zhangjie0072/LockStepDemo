using UnityEngine;
using System;
using System.Collections.Generic;

using fogs.proto.msg;
using ProtoBuf;
using System.IO;
using System.Reflection;

/// <summary>
/// 练习赛(所有联系赛都使用这个类控制）
/// </summary>
public class GameMatch_Practise : GameMatch
{
    private GameObject _ui_practise;
    private UIWidget _ui_signal;
    private UIGrid _ui_signal_grid;
    private UILabel _ui_title;
    private GameObject _ui_tip_pane;
	private UITweener[] _tip_open_tweens = new UITweener[2];
	private UITweener[] _tip_close_tweens = new UITweener[2];
    private GameObject _ui_tip_arrow;
    private GameObject _ui_back_button;
	private GameObject _touch_guide_obj;

	private GameObject _button_highlight_prefab;
	private GameObject _icon_tip_prafab;
	private GameObject _touch_guide_prefab;
	private GameObject _touch_guide_up_prefab;

    private UILabel _ui_tip;
    private uint num;
    public string tip
    {
        set
        {
            if (_ui_tip != null)
                _ui_tip.text = value;
        }
    }

    public GameObject ui_tutorial;

	private int _ori_signal_height;

    public UIEventListener.VoidDelegate onTipClick
    {
		set { UIEventListener.Get(_ui_practise).onClick = value; }
		get { return UIEventListener.Get(_ui_practise).onClick; }
    }

	public PractiseData practise { get; private set; }

	public PractiseBehaviour practise_behaviour { get; private set; }

	public System.Action onBehaviourCreated;

    private static Dictionary<uint, string> _practise_behaviour_name = new Dictionary<uint, string>();

	public GameMatch_Practise(Config config)
		:base(config)
	{
        if (_practise_behaviour_name.Count == 0)
        {
            _practise_behaviour_name[10001] = "Move";
            _practise_behaviour_name[10002] = "Rule";
            _practise_behaviour_name[10003] = "Pass";
            _practise_behaviour_name[10004] = "Shoot";
            _practise_behaviour_name[10008] = "LayupDunk";
            _practise_behaviour_name[10009] = "Block";
            _practise_behaviour_name[10010] = "Rebound";
            _practise_behaviour_name[20001] = "Guide";
            _practise_behaviour_name[20002] = "SkillGuide";
        }

		_button_highlight_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TapEffect") as GameObject;
        //_icon_tip_prafab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/PractiseOverhead") as GameObject;
        _touch_guide_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TouchGuide") as GameObject;
        _touch_guide_up_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TouchGuideUp") as GameObject;

        practise = GameSystem.Instance.PractiseConfig.GetConfig(config.extra_info);
        num = practise.awards[2];
		//GameSystem.Instance.mNetworkManager.m_handler.RegisterHandler(MsgID.ExitGameRespID, OnExitPractise);
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
        HideBackButton();
	}

	override protected void _CreatePlayersData()
	{
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		mCurScene.CreateBall();

		GameObject obj = new GameObject("PractiseBehaviour");
        var PractiseBehaviourType = Assembly.GetExecutingAssembly().GetType("PractiseBehaviour" + _practise_behaviour_name[practise.ID]);
        practise_behaviour = obj.AddComponent(PractiseBehaviourType) as PractiseBehaviour;
        practise_behaviour.enabled = false;
		if (onBehaviourCreated != null)
			onBehaviourCreated();

		if( m_config == null )
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}
		
		//Main role 
		if (practise.type == PractiseData.Type.Normal)
		{
			if (m_config.MainRole.id != null)
			{
                if (GameSystem.Instance.PractiseConfig.GetConfig(practise.ID).is_activity == 1)
				    mainRole = CreatePlayer(m_config.MainRole, false);
                else
                    mainRole = CreatePlayer(m_config.MainRole, true);
			}
		}
		else
		{
			if (m_config.MainRole.id.Length >= 5)
				m_config.MainRole.id = MainPlayer.Instance.CaptainID.ToString();
			mainRole = CreatePlayer(m_config.MainRole, false);
		}
        _UpdateCamera(mainRole);
		foreach( GameMatch.Config.TeamMember member in m_config.NPCs )
        {
            Player npc = CreatePlayer(member, true);
            npc.operMode = Player.OperMode.AI;
        }
		m_homeTeam.m_role = (m_homeTeam == mainRole.m_team) ? MatchRole.eOffense : MatchRole.eDefense;
		m_awayTeam.m_role = (m_awayTeam == mainRole.m_team) ? MatchRole.eOffense : MatchRole.eDefense;
		AssumeDefenseTarget();
	}

    public override AISystem CreateAISystem(Player player)
    {
        return (AISystem)Activator.CreateInstance(
            Assembly.GetExecutingAssembly().GetType("AISystem_Practise" + _practise_behaviour_name[practise.ID]), 
            new System.Object[] { this, player, practise_behaviour.GetInitialAIState() });
    }

	public override void ViewUpdate ()
	{
        base.ViewUpdate();

		if( !GameSystem.Instance.mClient.mUIManager.isInMatchLoading && m_uiController == null)
		{
			_CreateGUI();
			PostCreateUI();

            practise_behaviour.match = this;
            practise_behaviour.practise = practise;

            GameMsgSender.SendGameBegin();
        }
	}

    public override void GameUpdate(IM.Number deltaTime)
    {
        base.GameUpdate(deltaTime);

        if (practise_behaviour != null)
            practise_behaviour.GameUpdate(deltaTime);
    }

    public override void OnGameBegin(GameBeginResp resp)
    {
		m_stateMachine.SetState(MatchState.State.eBegin);
		practise_behaviour.enabled = true;
    }

    public Player CreatePlayer(Config.TeamMember mem, bool rival)
    {
        if ((FightStatus)mem.pos != FightStatus.FS_MAIN)
        {
            mem.team = practise_behaviour.GetNPCSide();
        }
		//if (mem.id == MainPlayer.Instance.CaptainID.ToString())
		//	mem.roleInfo = MainPlayer.Instance.Captain.m_roleInfo;
		Player player = _GeneratePlayerData(mem, rival);
		CreateTeamMember(player);

		if ((FightStatus)mem.pos == FightStatus.FS_MAIN)
        {
            player.operMode = Player.OperMode.Input;
        }
        player.m_catchHelper = new CatchHelper(player);
        player.m_catchHelper.ExtractBallLocomotion();
        player.m_StateMachine.SetState(PlayerState.State.eStand, true);

        return player;
    }

    public void HideSignal()
    {
		_ori_signal_height = _ui_signal.height;
        _ui_signal.height = 0;
        NGUITools.SetActive(_ui_signal.gameObject, false);
    }

	public void ShowSignal()
	{
		if (_ori_signal_height > 0)
			_ui_signal.height = _ori_signal_height;
        NGUITools.SetActive(_ui_signal.gameObject, true);
	}

    public void SetSignal(PractiseBehaviour.ObjectiveState[] states)
    {
        while (_ui_signal_grid.transform.childCount > 0)
            NGUITools.Destroy(_ui_signal_grid.GetChild(0).gameObject);

        int i = 0;
        foreach (PractiseBehaviour.ObjectiveState state in states)
        {
            string spriteName = string.Empty;
            switch (state)
            {
                case PractiseBehaviour.ObjectiveState.Undone:
                    spriteName = "practice_w";
                    break;
                case PractiseBehaviour.ObjectiveState.Completed:
                    spriteName = "practice_y";
                    break;
                case PractiseBehaviour.ObjectiveState.Failed:
                    spriteName = "practice_n";
                    break;
            }
            UISprite sprite = NGUITools.AddSprite(_ui_signal_grid.gameObject, ResourceLoadManager.Instance.GetAtlas("Atlas/Practice/Practice"), spriteName);
            sprite.gameObject.name = (++i).ToString();
            sprite.width = (int)_ui_signal_grid.cellWidth;
            sprite.height = (int)_ui_signal_grid.cellHeight;
			sprite.depth = _ui_signal.depth + 1;
        }
        _ui_signal_grid.Reposition();
        _ui_signal.width = (int)(_ui_signal_grid.cellWidth * states.Length + 4);
        _ui_signal_grid.transform.localPosition = 
            new Vector3(-_ui_signal.width / 2 + _ui_signal_grid.cellWidth/2 + 2, _ui_signal.height/2 + 2, 0);
    }

    public void ShowGuideTip()
    {
		if (NGUITools.GetActive(_ui_tip_pane))
		{
			HideGuideTip(DoShowGuideTip);
		}
		else
		{
			DoShowGuideTip();
		}
    }

	public void DoShowGuideTip()
	{
        NGUITools.SetActive(_ui_tip_pane, true);
		foreach (UITweener tween in _tip_open_tweens)
		{
			tween.enabled = true;
			tween.ResetToBeginning();
			tween.PlayForward();
		}
	}

    public void HideGuideTip(EventDelegate.Callback onFinish = null)
    {
		if (NGUITools.GetActive(_ui_tip_pane))
		{
			foreach (UITweener tween in _tip_close_tweens)
			{
				tween.enabled = true;
				tween.ResetToBeginning();
				tween.PlayForward();
			}
			_tip_close_tweens[0].SetOnFinished(() => { NGUITools.SetActive(_ui_tip_pane, false); });
			if (onFinish != null)
				_tip_close_tweens[0].AddOnFinished(onFinish);
		}
    }

    public void ShowTipArrow()
    {
        NGUITools.SetActive(_ui_tip_arrow, true);
		_ui_practise.GetComponent<BoxCollider>().enabled = true;
    }

    public void HideTipArrow()
    {
        NGUITools.SetActive(_ui_tip_arrow, false);
		_ui_practise.GetComponent<BoxCollider>().enabled = false;
    }

    public void HideTitle()
    {
        NGUITools.SetActive(_ui_title.transform.parent.gameObject, false);
    }

    public void HideBackButton()
    {
        NGUITools.SetActive(_ui_back_button, false);
    }

	public void HighlightButton(int index, bool highlight = true)
	{
		if (m_uiController != null)
		{
			UIButton btn = m_uiController.m_btns[index].btn;
			if (btn == null)
				return;
			Transform effect = btn.transform.FindChild("TapEffect(Clone)");
			if (highlight)
			{
				if (effect == null)
				{
					effect = CommonFunction.InstantiateObject(_button_highlight_prefab, btn.transform).transform;
					effect.FindChild("Finger").gameObject.SetActive(false);
				}
			}
			else
			{
				if (effect != null)
					NGUITools.Destroy(effect.gameObject);
			}
		}
	}

	public void ShowIconTip(bool succeed)
	{
        //GameObject goTips = GameObject.Instantiate(_icon_tip_prafab) as GameObject;
        //goTips.GetComponent<UISprite>().spriteName = succeed ? "gameInterface_image_smile" : "gameInterface_image_x";
        //goTips.GetComponent<TweenAlpha>().AddOnFinished(delegate() { NGUITools.Destroy(goTips); });

        //goTips.transform.parent = m_mainRole.m_InfoVisualizer.m_goState.transform;
        //goTips.transform.localScale = Vector3.one;

        //UIManager uiMgr = GameSystem.Instance.mClient.mUIManager;
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(m_mainRole.m_head.position + Vector3.up * 0.5f);
        //goTips.transform.position = uiMgr.m_uiCamera.camera.ViewportToWorldPoint(viewPos);

        //Vector3 pos = goTips.transform.localPosition;
        //pos.x = Mathf.FloorToInt(pos.x);
        //pos.y = Mathf.FloorToInt(pos.y);
        //pos.z = 2.0f;
        //goTips.transform.localPosition = pos;
        if (succeed)
        {
            //_ui_practise.transform.FindChild("HPTip/+").GetComponent<UISprite>().spriteName = "practice_txe_"+num;
			if(num >= 100)
			{
	            _ui_practise.transform.FindChild("HPTip/+").GetComponent<UISprite>().spriteName = "VIP_"+(num/100);
	            _ui_practise.transform.FindChild("HPTip/2").GetComponent<UISprite>().spriteName = "VIP_"+(num%100/10);
	            _ui_practise.transform.FindChild("HPTip/3").GetComponent<UISprite>().spriteName = "VIP_"+(num%100%10);
			}
			else
			{
				_ui_practise.transform.FindChild("HPTip/+").GetComponent<UISprite>().spriteName = "VIP_"+(num/10);
				_ui_practise.transform.FindChild("HPTip/2").GetComponent<UISprite>().spriteName = "VIP_"+(num%10);
				UISprite spr;
				spr = _ui_practise.transform.FindChild("HPTip/3").GetComponent<UISprite>();
				spr.atlas = ResourceLoadManager.Instance.GetAtlas("Atlas/icon/iconGoods");
				spr.SetDimensions(60,60);
				spr.aspectRatio = 1f;
				spr.spriteName = "com_property_gold";
				_ui_practise.transform.FindChild("HPTip/Icon").gameObject.SetActive(false);
			}
            _ui_practise.transform.FindChild("HPTip").GetComponent<Animator>().SetTrigger("HP_go");
        }

		AudioClip clip = AudioManager.Instance.GetClip(succeed ? practise.complete_sound : practise.failed_sound);
		if( clip != null )
			AudioManager.Instance.PlaySound(clip);
	}

	void OnJoyStickPress(GameObject go, bool pressed)
	{
		FadeAway();
	}

	void FadeAway()
	{
		Transform joystick = m_uiController.transform.FindChild("Joystick/Joystick");
		UIEventListener.Get(joystick.gameObject).onPress -= OnJoyStickPress;
		_touch_guide_obj.GetComponentInChildren<TweenAlpha>().PlayForward();
	}

	public void ShowTouchGuide(UIEventListener.BoolDelegate onPress = null, bool up = false, float angle = 0f)
	{
		Transform joystick = m_uiController.transform.FindChild("Joystick/Joystick");
		if (_touch_guide_obj == null)
		{
			_touch_guide_obj = CommonFunction.InstantiateObject(up ? _touch_guide_up_prefab : _touch_guide_prefab, joystick);
			UIManager.Instance.BringWidgetForward(_touch_guide_obj);
			_touch_guide_obj.GetComponentInChildren<TweenAlpha>().AddOnFinished(() =>
			{
				NGUITools.Destroy(_touch_guide_obj);
				_touch_guide_obj = null;
			});
			UIEventListener.Get(joystick.gameObject).onPress += OnJoyStickPress;
			if (onPress != null)
				UIEventListener.Get(joystick.gameObject).onPress += onPress;
		}
		if (up)
		{
			_touch_guide_obj.GetComponent<TouchGuideUp>().angle = angle;
		}
	}

	public void HideTouchGuide()
	{
		if (_touch_guide_obj != null)
		{
			FadeAway();
		}
	}

	protected override void CreateCustomGUI()
	{
		GameObject obj = GameSystem.Instance.mClient.mUIManager.CreateUI("UIPractise");
		NGUITools.BringForward(obj);
	}

    public override bool TimmingOnStarting()
    {
        return false;
    }

	public override bool EnableGoalState()
	{
		return false;
	}

	public override bool EnableSwitchRole()
	{
		return false;
	}

	public override bool EnableCheckBall()
	{
		return false;
	}

	public override bool EnablePlayerTips()
	{
		return false;
	}

	public override void ResetPlayerPos()
	{
		if (!practise_behaviour.ResetPlayerPos())
		{
			base.ResetPlayerPos();
		}
	}

	public override IM.PrecNumber AdjustShootRate(Player shooter, IM.PrecNumber rate)
	{
		return practise_behaviour.AdjustShootRate(shooter, rate);
	}

	public override IM.Number AdjustBlockRate(Player shooter, Player blocker, IM.Number rate)
	{
		return practise_behaviour.AdjustBlockRate(shooter, blocker, rate);
	}

	public override IM.Number AdjustCrossRate(Player crosser, Player defender, IM.Number rate)
	{
		return practise_behaviour.AdjustCrossRate(crosser, defender, rate);
	}

    public override ShootSolution GetShootSolution(UBasket basket, Area area, Player shooter, IM.PrecNumber rate, ShootSolution.Type type)
	{
		ShootSolution solution = practise_behaviour.GetShootSolution(basket, area, shooter, rate);
		if (solution == null)
			solution = base.GetShootSolution(basket, area, shooter, rate, type);
		Debug.Log("Practise shoot solution: " + solution.m_id);
		return solution;
	}

	public override bool IsCommandValid(Command command)
	{
        if (practise_behaviour == null)
            return false;
		return practise_behaviour.IsCommandValid(command);
	}

    private void PostCreateUI()
    {
        _ui_practise = GameObject.FindGameObjectWithTag("UIPractise");
        if (_ui_practise)
        {
            _ui_signal = _ui_practise.transform.FindChild("Signal").GetComponent<UIWidget>();
            _ui_signal_grid = _ui_signal.transform.FindChild("Grid").GetComponent<UIGrid>();
            _ui_title = _ui_practise.transform.FindChild("Title/Label").GetComponent<UILabel>();
            _ui_tip_pane = _ui_practise.transform.FindChild("Tip").gameObject;
			NGUITools.SetActive(_ui_tip_pane, false);
			UITweener[] tweens = _ui_tip_pane.GetComponents<UITweener>();
			_tip_open_tweens[0] = tweens[0];
			_tip_open_tweens[1] = tweens[1];
			_tip_close_tweens[0] = tweens[2];
			_tip_close_tweens[1] = tweens[3];
            _ui_tip = _ui_practise.transform.FindChild("Tip/Text").GetComponent<UILabel>();
            _ui_tip_arrow = _ui_practise.transform.FindChild("Tip/Arrow").gameObject;
            ui_tutorial = _ui_practise.transform.FindChild("Tutorial").gameObject;

            if( GameSystem.Instance.PractiseConfig.GetConfig(practise.ID).is_activity == 1 && LoginIDManager.GetFirstPractice(practise.ID) == 1 )
            {
                GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
                Transform buttonParent = _ui_practise.transform.FindChild("ButtonBack");
                _ui_back_button = CommonFunction.InstantiateObject(prefab, buttonParent);
                UIEventListener.Get(_ui_back_button).onClick = OnBack;
            }
            HideTipArrow();

            _ui_title.text = practise.title;
        }
    }

    private void OnBack(GameObject go)
    {
        GameSystem.Instance.mClient.pause = true;
        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("MATCH_TIPS_EXIT_PRACTISE"), _ui_practise.transform, OnConfirmBack, OnCancelBack);
    }

    private void OnConfirmBack(GameObject go)
    {
		if (!GameSystem.Instance.mNetworkManager.connPlat)
		{
			PlatNetwork.Instance.onReconnected += ExitMatch;
			GameSystem.Instance.mNetworkManager.autoReconnInMatch = true;
			GameSystem.Instance.mNetworkManager.Reconnect();
			return;
		}
		ExitMatch();
	}

	private void ExitMatch()
    {
        GameSystem.Instance.mClient.pause = false;
		bool passed = MainPlayer.Instance.IsPractiseCompleted(practise.ID);
		if (GlobalConst.IS_NETWORKING)
		{
            EndPractice practice = new EndPractice();
            practice.session_id = m_config.session_id;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_PRACTICE;
            req.exit_type = passed ? ExitMatchType.EMT_END : ExitMatchType.EMT_OPTION;
            req.practice = practice;
			GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
		}
		else
		{
			GameSystem.Instance.mClient.Reset();
			GameSystem.Instance.mClient.mUIManager.curLeagueType = GameMatch.LeagueType.ePractise;
            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
		}
		m_stateMachine.m_curState.OnExit();
    }

    //private void OnExitPractise(Pack pack)
    //{
    //    //ExitPracticeResp resp = Serializer.Deserialize<ExitPracticeResp>(new MemoryStream(pack.buffer));
    //    ExitGameResp resp = Serializer.Deserialize<ExitGameResp>(new MemoryStream(pack.buffer));
    //    if ((ErrorID)resp.practice.result == ErrorID.SUCCESS)
    //    {
    //        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", GameMatch.LeagueType.ePractise);
    //    }
    //    else
    //    {
    //        Debug.Log("Exit practise error: " + (ErrorID)resp.practice.result);
    //        CommonFunction.ShowErrorMsg((ErrorID)resp.practice.result);
    //    }
    //}

    private void OnCancelBack(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
    }
}

