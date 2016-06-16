using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using LuaInterface;

public class UIMatch : MonoBehaviour
{
	public enum MSGType
	{
		eFoul_24,
		eCheckBall,
	}
	public string spritePrefix
	{
		set
		{
			leftScoreBoard.spritePrefix = value;
			rightScoreBoard.spritePrefix = value;
		}
	}

	[HideInInspector]
	public int winScore
	{
		set
		{
			leftScoreBoard.winScore = value;
			rightScoreBoard.winScore = value;
		}
	}
	[HideInInspector]
	public int leftScore { set { leftScoreBoard.Refresh(value); } }
	[HideInInspector]
	public int rightScore { set { rightScoreBoard.Refresh(value); } }
	[HideInInspector]
	public int digitCount
	{
		set
		{
			leftScoreBoard.digitCount = value;
			rightScoreBoard.digitCount = value;
		}
	}

    public void SetTimeBoardVisible(bool b)
    {
        timerBoard.SetVisible(b);
    }

    public void UpdateTime(float time)
    {
        timerBoard.UpdateTime(time);
    }

	UILabel leftNameLabel;
	UILabel rightNameLabel;
    UISprite bg;
	Transform leftScoreNode;
	[HideInInspector]
	public ScoreBoard_new leftScoreBoard { get; private set; }
	Transform rightScoreNode;
	[HideInInspector]
	public ScoreBoard_new rightScoreBoard { get; private set; }
	Transform timerNode;
	[HideInInspector]
	public TimerBoard timerBoard { get; private set; }
	GameObject m_goFoulMsg;
	GameObject m_goCheckBallMsg;
	[HideInInspector]
	public UTimeCounter24 mCounter24;
	Transform mCenterAnchor;
	Transform mTopAnchor;
	[HideInInspector]
	public GameObject leftBall;
	[HideInInspector]
	public GameObject rightBall;
	[HideInInspector]
	public GameObject goExit;
	[HideInInspector]
	public bool enableBack { set {
            NGUITools.SetActive(goExit, value);
        } }

	//short msg
	[HideInInspector]
	public GameObject goShortMsgFolder;
	[HideInInspector]
	public GameObject goCustomMsg;
	[HideInInspector]
	public GameObject goMsg1;
	[HideInInspector]
	public GameObject goMsg2;
	[HideInInspector]
	public UIGrid uiGridCustomMsgs;
	[HideInInspector]
	public UIMatchMessageBlob m_msgHomeBlobs;
	[HideInInspector]
	public UIMatchMessageBlob m_msgAwayBlobs;

	private uint m_curMatchMsgCond;
	private GameMatch m_match;
	private UIButton m_btnCustomItem;
	private uint m_curMatchMsgRoleId = 0;
	private UISprite m_sprWifi;
    private UILabel m_labWifi;
	private GameUtils.Timer4View m_pingTime;
	private bool m_bShowCustomMsg = false;
	private bool m_bShowAll = true;

	private Transform m_leftNode;
	private Transform m_rightNode;

	private UIMatchScoreEffect m_matchScoreEffect;

	class MatchMsgBtn
	{
		public GameObject btn{get;private set;}
		public MatchMsg	msg{get;private set;}
		public bool	valid{get;private set;}

		private float timeCnt;
		private uint curCD;
		private UIButtonScale m_btnScale;
		private UIPlaySound m_btnSound;

		public MatchMsgBtn(GameObject inBtn, MatchMsg inMsg)
		{
			btn = inBtn;
			msg = inMsg;

			valid = true;
			timeCnt = 0;

			m_btnScale = btn.GetComponent<UIButtonScale>();
			m_btnSound = btn.GetComponent<UIPlaySound>();
		}

		public void Reset(uint cd)
		{
			valid = false;
			timeCnt = 0;
			curCD = cd;
		}

		public void Update(float fDeltaTime)
		{
			if( valid )
			{
				GameUtils.SetWidgetColorRecursive(btn.transform, new Color(1.0f, 1.0f, 1.0f, 1.0f));
				if( m_btnScale != null )
					m_btnScale.enabled = true;
				if( m_btnSound != null )
					m_btnSound.enabled = true;

				return;
			}
			GameUtils.SetWidgetColorRecursive(btn.transform, new Color(0.0f, 1.0f, 1.0f, 1.0f));
			if( m_btnScale != null )
				m_btnScale.enabled = false;
			if( m_btnSound != null )
				m_btnSound.enabled = false;

			timeCnt += fDeltaTime;
			if( timeCnt < (float)curCD )
				return;
			valid = true;
			timeCnt = 0;
		}
	}

	private List<MatchMsgBtn> m_matchMsgBtns = new List<MatchMsgBtn>();

	void Awake()
	{
		m_match = GameSystem.Instance.mClient.mCurMatch;
		
		leftNameLabel = transform.FindChild("LeftName").GetComponent<UILabel>();
		rightNameLabel = transform.FindChild("RightName").GetComponent<UILabel>();
        bg = transform.FindChild("Bg").GetComponent<UISprite>();
		leftScoreNode = transform.FindChild("LeftScore");
		rightScoreNode = transform.FindChild("RightScore");
		timerNode = transform.FindChild("TimerNode");

		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/TimerBoard") as GameObject;
		timerBoard = CommonFunction.InstantiateObject(prefab, timerNode).GetComponent<TimerBoard>();
		timerBoard.backgroundVisible = false;

		prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ScoreBoard") as GameObject;
		leftScoreBoard = CommonFunction.InstantiateObject(prefab, leftScoreNode).GetComponent<ScoreBoard_new>();
		leftScoreBoard.isLeft = true;
		rightScoreBoard = CommonFunction.InstantiateObject(prefab, rightScoreNode).GetComponent<ScoreBoard_new>();
		rightScoreBoard.isLeft = false;

		mCounter24 	= GameUtils.FindChildRecursive(transform, "TimeCounter24").GetComponent<UTimeCounter24>();
        m_match.m_count24Time = m_match.MAX_COUNT24_TIME;
		mCounter24.gameObject.SetActive(false);

		mCenterAnchor	= GameUtils.FindChildRecursive(transform, "Anchor_Center");
		mTopAnchor		= GameUtils.FindChildRecursive(transform, "Anchor_Top");

		leftBall = transform.FindChild("LeftBall").gameObject;
		rightBall = transform.FindChild("RightBall").gameObject;

		m_leftNode = transform.Find("LeftNode");
		m_rightNode = transform.Find("RightNode");

		prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/msg/Foul_24") as GameObject;
		m_goFoulMsg = GameObject.Instantiate(prefab) as GameObject;
		m_goFoulMsg.transform.parent = mCenterAnchor;
		m_goFoulMsg.SetActive(false);
		
		prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/msg/Msg_CheckBall") as GameObject;
		m_goCheckBallMsg = GameObject.Instantiate(prefab) as GameObject;
		m_goCheckBallMsg.transform.parent = mCenterAnchor;
		m_goCheckBallMsg.SetActive(false);

        prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/ButtonBack") as GameObject;
        Transform buttonParent = transform.FindChild("ButtonBack");      
        goExit = CommonFunction.InstantiateObject(prefab, buttonParent);
		UIEventListener.Get(goExit).onClick = OnExit;

		goShortMsgFolder = transform.FindChild("ButtonArrow").gameObject;
		goShortMsgFolder.AddComponent<TweenRotation>();

		UIEventListener.Get(goShortMsgFolder).onClick = OnClickMsg;
		
		goCustomMsg = transform.FindChild("LeftButton/ButtonTalk").gameObject;
		UIEventListener.Get(goCustomMsg).onClick = OnClickMsg;
		
		goMsg1 = transform.FindChild("LeftButton/ButtonMark/Icon").gameObject;
		UIEventListener.Get(goMsg1.transform.parent.gameObject).onClick = OnClickMsg;

		goMsg2 = transform.FindChild("LeftButton/ButtonExchange/Icon").gameObject;
		UIEventListener.Get(goMsg2.transform.parent.gameObject).onClick = OnClickMsg;

		GameObject goGrid = transform.FindChild("LeftButton/Grid").gameObject;
		uiGridCustomMsgs = goGrid.GetComponent<UIGrid>();
		GameObject sampleBtn = uiGridCustomMsgs.GetChild(0).gameObject;
		GameObject goBtnItem = GameObject.Instantiate( sampleBtn ) as GameObject;
		m_btnCustomItem = goBtnItem.GetComponent<UIButton>();
		goBtnItem.SetActive(false);
		sampleBtn.SetActive(false);

		m_msgHomeBlobs = transform.FindChild("HomeBlobs").GetComponent<UIMatchMessageBlob>();
		m_msgAwayBlobs = transform.FindChild("AwayBlobs").GetComponent<UIMatchMessageBlob>();

		m_sprWifi = transform.FindChild("RightButton/ButtonWifi/Icon").GetComponent<UISprite>();
        m_labWifi = transform.FindChild("RightButton/ButtonWifi/Num").GetComponent<UILabel>();


        m_curMatchMsgCond = 0;
		m_bShowCustomMsg = false;
		m_bShowAll = true;


		GameMatch.LeagueType leagueType = m_match.GetConfig().leagueType;
		if( leagueType == GameMatch.LeagueType.eRegular1V1 || leagueType == GameMatch.LeagueType.ePVP || leagueType == GameMatch.LeagueType.eQualifyingNew
            || leagueType == GameMatch.LeagueType.eQualifyingNewer
            || leagueType == GameMatch.LeagueType.eQualifyingNewerAI
            || leagueType == GameMatch.LeagueType.eLadderAI
            )
		{
			m_pingTime = new GameUtils.Timer4View(2f, OnPing);
		}
		else
		{
			m_bShowAll = false;
			goShortMsgFolder.SetActive(false);
			transform.FindChild("RightButton").gameObject.SetActive(false);
		}

		m_matchScoreEffect = gameObject.AddComponent<UIMatchScoreEffect>();

        OnPing();
    }

	void OnPing()
	{
		if( m_sprWifi == null )
			return;

		double latency = 0.0;
		if( m_match is GameMatch_PVP )
		{
			if( GameSystem.Instance.mNetworkManager.m_gameConn == null )
				return;
		}
		else
		{
			if( GameSystem.Instance.mNetworkManager.m_platConn == null )
				return;
		}

        latency = GameSystem.Instance.mNetworkManager.m_platConn.m_profiler.m_avgLatency * 1000;

        if (latency > 200.0)
        {
            m_sprWifi.spriteName = "wifi_1";
            m_labWifi.color = Color.red;
        }
        else if (latency > 100.0)
        {
            m_sprWifi.spriteName = "wifi_2";
            m_labWifi.color = Color.yellow;
        }
        else
        {
            m_sprWifi.spriteName = "wifi_3";
            m_labWifi.color = Color.green;
        }
        m_labWifi.text = string.Format("{0}ms", Mathf.RoundToInt((float)latency));
    }

	void OnClickMsg(GameObject go)
	{
		if( go == goShortMsgFolder )
		{
			m_bShowAll = !m_bShowAll;
			TweenRotation.Begin(goShortMsgFolder, 0.2f, Quaternion.AngleAxis(m_bShowAll ? 0.0f : 90.0f, new Vector3(0.0f, 0.0f, 1.0f))).method = UITweener.Method.EaseOut;
			return;
		}

		MatchMsgBtn msgBtn = m_matchMsgBtns.Find( (MatchMsgBtn matchBtn)=>{ return matchBtn.btn == go; } );
		if( msgBtn == null )
			return;
		if( !msgBtn.valid )
			return;
		if( go == goCustomMsg )
		{
			m_bShowCustomMsg = !m_bShowCustomMsg;
			return;
		}

		foreach( MatchMsgBtn btn in m_matchMsgBtns )
			btn.Reset( msgBtn.msg.cd );
		m_bShowCustomMsg = false;
		GameMsgSender.SendGameShortMsg( m_match.m_mainRole, msgBtn.msg.id, (uint)msgBtn.msg.recvType );
	}

	void FixedUpdate()
	{
		if( m_pingTime != null )
			m_pingTime.Update(Time.fixedDeltaTime);
	}

	void Update()
	{
		if( uiGridCustomMsgs != null )
			uiGridCustomMsgs.gameObject.SetActive(m_bShowCustomMsg);

		if( goCustomMsg != null )
			goCustomMsg.SetActive(m_bShowAll);
		if( goMsg1.transform.parent != null )
			goMsg1.transform.parent.gameObject.SetActive(m_bShowAll);
		if( goMsg2.transform.parent != null )
			goMsg2.transform.parent.gameObject.SetActive(m_bShowAll);

		if( uiGridCustomMsgs != null && m_bShowCustomMsg )
			uiGridCustomMsgs.gameObject.SetActive(m_bShowAll);

		m_matchMsgBtns.ForEach( (MatchMsgBtn btn)=>{btn.Update(Time.deltaTime);} );
		UBasketball ball = m_match.mCurScene.mBall;
		if( ball == null )
			return;

		uint curMatchMsgCond = 0;
		if( ball.m_ballState != BallState.eUseBall )
			curMatchMsgCond = 1;
		else
		{
			if( ball.m_owner.m_team == m_match.m_mainRole.m_team )
			{
				if( ball.m_owner == m_match.m_mainRole )
					curMatchMsgCond = 4;
				else
					curMatchMsgCond = 3;
			}
			else
				curMatchMsgCond = 2;
		}

		if( (curMatchMsgCond != m_curMatchMsgCond && curMatchMsgCond != 0) 
		   || (m_match.m_mainRole != null && m_curMatchMsgRoleId != m_match.m_mainRole.m_id )
		   )
		{
			m_matchMsgBtns.Clear();
			m_curMatchMsgRoleId = m_match.m_mainRole.m_id;

			List<MatchMsg> matchedmsgs = GameSystem.Instance.matchMsgConfig.matchMsgs.FindAll( (MatchMsg msg)=>{return msg.conds.Contains(curMatchMsgCond);} );
			if( m_match.m_mainRole != null )
			{
				uint id = m_match.m_mainRole.m_id;
				RoleBaseData2 rd = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id);
				if( rd == null )
					Logger.LogError("Unable to find role base: " + id);

				m_matchMsgBtns.Add(new MatchMsgBtn(goCustomMsg, null));

				List<MatchMsg> filteredMsgs = new List<MatchMsg>();
				foreach( uint msg_id in rd.match_msg_ids )
				{
					MatchMsg filteredMsg = matchedmsgs.Find( (MatchMsg msg)=>{return msg.id == msg_id;} );
					if( filteredMsg == null )
						continue;
					filteredMsgs.Add(filteredMsg);
				}
				//update ui
				MatchMsg finalMsg = filteredMsgs.Find( (MatchMsg msg)=>{return msg.menu_type == 1;} );
				if( finalMsg != null )
				{
					GameObject btnMsg1 = goMsg1.transform.parent.gameObject;
					MatchMsgBtn matchBtn = new MatchMsgBtn(btnMsg1, finalMsg);
					m_matchMsgBtns.Add(matchBtn);
					if( !m_bShowAll )
						btnMsg1.SetActive(true);
					UISprite icon = goMsg1.GetComponent<UISprite>();
					icon.spriteName = finalMsg.desc;
					if( !m_bShowAll )
						btnMsg1.SetActive(false);
				}
				finalMsg = filteredMsgs.Find( (MatchMsg msg)=>{return msg.menu_type == 2;} );
				if( finalMsg != null )
				{
					GameObject btnMsg2 = goMsg2.transform.parent.gameObject;
					MatchMsgBtn matchBtn = new MatchMsgBtn(btnMsg2, finalMsg);
					m_matchMsgBtns.Add(matchBtn);
					if( !m_bShowAll )
						btnMsg2.SetActive(true);
					UISprite icon = goMsg2.GetComponent<UISprite>();
					icon.spriteName = finalMsg.desc;
					if( !m_bShowAll )
						btnMsg2.SetActive(false);
				}

				if( !m_bShowCustomMsg || !m_bShowAll )
					uiGridCustomMsgs.gameObject.SetActive(true);

				CommonFunction.ClearGridChild(uiGridCustomMsgs.transform);
				List<MatchMsg> finalMsgs = filteredMsgs.FindAll( (MatchMsg msg)=>{return msg.menu_type == 3;} );
				foreach(MatchMsg msg in finalMsgs)
				{
					GameObject goChild = GameObject.Instantiate(m_btnCustomItem.gameObject) as GameObject;
					goChild.SetActive(true);
					uiGridCustomMsgs.AddChild(goChild.transform);

					MatchMsgBtn matchBtn = new MatchMsgBtn(goChild, msg);
					m_matchMsgBtns.Add(matchBtn);

					UILabel label = goChild.GetComponentInChildren<UILabel>();
					label.text = msg.desc;
					UIEventListener.Get(goChild).onClick = OnClickMsg;

					goChild.transform.localScale = Vector3.one;
				}

				if( !m_bShowCustomMsg || !m_bShowAll )
					uiGridCustomMsgs.gameObject.SetActive(false);
			}
			m_curMatchMsgCond = curMatchMsgCond;
		}
	}

	public void ShowCounter(bool bShow, bool bHome)
	{
		if (!bShow)
		{
			mCounter24.gameObject.SetActive(false);
            m_match.m_count24TimeStop = true;
			return;
		}
		
		if( bHome )
			mCounter24.transform.parent = m_leftNode.transform;
		else
			mCounter24.transform.parent = m_rightNode.transform;

		mCounter24.transform.localPosition = Vector3.zero;

		if( !Debugger.Instance.m_bEnableTiming )
			return;

		mCounter24.gameObject.SetActive(true);
        m_match.m_count24Time = m_match.gameMatchTime;
	}
	
	public void ShowMsg(MSGType msg, bool bShow)
	{
		switch(msg)
		{
		case MSGType.eFoul_24:
			m_goFoulMsg.SetActive(bShow);
			break;
			
		case MSGType.eCheckBall:
			m_goCheckBallMsg.SetActive(bShow);
			break;
		}
	}
    public void VisibleScoreBoardUI(bool visible)
    {
        bg.gameObject.SetActive(visible);
        leftNameLabel.gameObject.SetActive(visible);
        rightNameLabel.gameObject.SetActive(visible);
        leftScoreNode.gameObject.SetActive(visible);
        rightScoreNode.gameObject.SetActive(visible);
        timerNode.gameObject.SetActive(visible);
    }
    private void OnExit(GameObject go)
    {
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if (curMatch.GetMatchType() != GameMatch.Type.ePVP_1PLUS && 
			curMatch.GetMatchType() != GameMatch.Type.ePVP_3On3)
			GameSystem.Instance.mClient.pause = true;
        CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("MATCH_TIPS_EXIT_MATCH"), 
			UIManager.Instance.m_uiRootBasePanel.transform, OnConfirmExit, OnCancelExit);
    }

	private void OnConfirmExit(GameObject go)
	{
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if (curMatch.m_stateMachine.m_curState.m_eState == MatchState.State.eOver)
		{
			if ((curMatch.m_stateMachine.m_curState as MatchStateOver).matchResultSent)
				return;
		}

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
		PlatNetwork.Instance.onReconnected -= ExitMatch;

		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
        GameMatch.LeagueType type = curMatch.leagueType;
        if (type == GameMatch.LeagueType.eCareer)
        {
            EndSectionMatch career = new EndSectionMatch();
            career.session_id = curMatch.m_config.session_id;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_CAREER;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.career = career;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            GameSystem.Instance.mClient.mUIManager.curLeagueType = curMatch.leagueType;
        }
        else if (type == GameMatch.LeagueType.ePractise1vs1)
        {
            EndPracticePve endPracticePve = new EndPracticePve();
            endPracticePve.session_id = curMatch.m_config.session_id;
            endPracticePve.main_role_side = 0; //Ö÷¶ÓOr¿Í¶Ó
            endPracticePve.score_home = (uint)curMatch.m_homeScore; //Ö÷¶ÓµÃ·Ö
            endPracticePve.score_away = (uint)curMatch.m_awayScore; //¿Í¶ÓµÃ·Ö

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_PRACTICE_1V1;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.practice_pve = endPracticePve;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            GameSystem.Instance.mClient.mUIManager.curLeagueType = curMatch.leagueType;
        }
        else if (type == GameMatch.LeagueType.eQualifying)
        {
            QualifyingEndReq qualifying = new QualifyingEndReq();
            qualifying.session_id = curMatch.m_config.session_id;
			qualifying.type = MatchType.MT_QUALIFYING;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_QUALIFYING;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.qualifying = qualifying;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            GameSystem.Instance.mClient.mUIManager.curLeagueType = curMatch.leagueType;
        }
		else if (type == GameMatch.LeagueType.eTour)
		{
            //TourExitReq req = new TourExitReq();
            //req.session_id = curMatch.m_config.session_id;
            TourEndReq tour = new TourEndReq();
            tour.session_id = curMatch.m_config.session_id;
            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_TOUR;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.tour = tour;
            GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
            MainPlayer.Instance.TourFailTimes++;
            LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
            table.Set("uiBack", (object)"UICompetition");
			LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[]{curMatch.leagueType, null, table});
		}
		else if (type == GameMatch.LeagueType.ePVP)
		{
			ExitGameReq req = new ExitGameReq();
			req.acc_id = MainPlayer.Instance.AccountID;
            req.type = GameMatch_PVP.ToMatchType(curMatch.leagueType, curMatch.m_config.type);
            req.exit_type = ExitMatchType.EMT_OPTION;

			if( GameSystem.Instance.mNetworkManager.m_gameConn != null )
			{
				Logger.Log("send exit game req");
				GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.ExitGameReqID);
			}
			
			GameSystem.Instance.mClient.mUIManager.curLeagueType = curMatch.leagueType;
            LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
            if (req.type == MatchType.MT_PVP_1V1_PLUS)
            {
                if (GameSystem.Instance.mNetworkManager.m_gameConn == null)
                {
                    table.Set("uiBack", (object)"UIPVPEntrance");
                    LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UI1V1Plus", null, table });
                }
            }
            else if (req.type == MatchType.MT_PVP_3V3)
            {
                table.Set("nextShowUI", (object)"UIPVPEntrance");
                LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIChallenge", null, table });
            }
		}
		else if (type == GameMatch.LeagueType.eRegular1V1)
		{
			ExitGameReq req = new ExitGameReq();
			req.acc_id = MainPlayer.Instance.AccountID;
			req.type = MatchType.MT_REGULAR_RACE;
			req.exit_type = ExitMatchType.EMT_OPTION;
			req.regular = new PVPEndRegularReq();
			req.regular.session_id = curMatch.m_config.session_id;

			if(curMatch.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
			{
				if (GameSystem.Instance.mNetworkManager.m_gameConn != null)
				{
					Logger.Log("send exit game req to game server");
					GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.ExitGameReqID);
				}
			}
			else if (curMatch.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
			{
				req.regular.rival_score = (curMatch as GameMatch_AsynPVP3ON3).GetRivalScore();
				GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
			}
			else
			{
				Logger.Log("send exit game req to plat server");
				GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
			}
		}
		else if (type == GameMatch.LeagueType.eQualifyingNew)
		{
			ExitGameReq req = new ExitGameReq();
			req.acc_id = MainPlayer.Instance.AccountID;
			req.type = MatchType.MT_QUALIFYING_NEW;
			req.exit_type = ExitMatchType.EMT_OPTION;
			req.qualifying_new = new PVPEndQualifyingReq();
			req.qualifying_new.session_id = curMatch.m_config.session_id;

			if(curMatch.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
			{
				if (GameSystem.Instance.mNetworkManager.m_gameConn != null)
				{
					Logger.Log("send exit game req to game server");
					GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.ExitGameReqID);
				}
			}
			else if (curMatch.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
			{
				req.qualifying_new.rival_score = (curMatch as GameMatch_AsynPVP3ON3).GetRivalScore();
				GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
			}
			else
			{
				Logger.Log("send exit game req to plat server");
				GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
			}
		}
		else if (type == GameMatch.LeagueType.ePractise)
		{
            EndPractice practice = new EndPractice();
            practice.session_id = curMatch.m_config.session_id;

            ExitGameReq req = new ExitGameReq();
            req.acc_id = MainPlayer.Instance.AccountID;
            req.type = MatchType.MT_PRACTICE;
            req.exit_type = ExitMatchType.EMT_OPTION;
            req.practice = practice;
			GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.ExitGameReqID);
		}
        else if( type == GameMatch.LeagueType.eBullFight)
        {
            LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
            table.Set("uiBack", (object)"UICompetition");
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { curMatch.leagueType, null, table });
        }
        else
        {
            LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
            table.Set("uiBack", (object)"UICompetition");
            LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { curMatch.leagueType, null, table });
        }

		curMatch.m_stateMachine.m_curState.OnExit();
        GameSystem.Instance.mClient.pause = false;
    }

    private void OnCancelExit(GameObject go)
    {
        GameSystem.Instance.mClient.pause = false;
    }

	public void ShowScoreGoal(UBasketball ball)
	{
		m_matchScoreEffect.Play(m_match, this, ball, 3);
	}
}
