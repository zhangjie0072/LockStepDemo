using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using fogs.proto.msg;

public class GameMatch_Guide : GameMatch_3ON3, MatchStateMachine.Listener,GameMatch.Count24Listener
{
	GuideTip guideTip;
	Dictionary<string, string> tips;
	HashSet<string> completedGuide = new HashSet<string>();
	GameUtils.Timer timer;
	string curGuide;
	bool clutchGuiding = false;
	GameObject playerIndicatorPrefab;
	GameObject areaPrefab;
	GameObject layupAreaPrefab;
	List<GameObject> playerIndicators = new List<GameObject>();
	GameObject areaIndicator;
	GameObject _button_highlight_prefab;
	GameObject _circle_highlight_prefab;

	const float TIP_CD = 5f;
	float curTipCD;

	MatchReviseTip matchReviseTip;

    public GameMatch_Guide(Config config)
        : base(config)
    {
		playerIndicatorPrefab = ResourceLoadManager.Instance.GetResources("Prefab/Indicator/ArrowDown") as GameObject;
		areaPrefab = ResourceLoadManager.Instance.GetResources("Prefab/Indicator/Area") as GameObject;
		layupAreaPrefab = ResourceLoadManager.Instance.GetResources("Prefab/Indicator/LayupArea") as GameObject;
		_button_highlight_prefab = ResourceLoadManager.Instance.GetResources("Prefab/GUI/ButtonHighlight") as GameObject;
		_circle_highlight_prefab = ResourceLoadManager.Instance.GetResources("Prefab/GUI/CircleHighlight") as GameObject;
    }

	public static void SetConfig(ref GameMatch.Config config)
	{
		int pos = 1;
		foreach (FightRole role in MainPlayer.Instance.SquadInfo)
		{
			Config.TeamMember mem = new Config.TeamMember();
			mem.id = role.role_id.ToString();
			mem.isRobot = false;
			mem.pos = pos++;
			mem.roleInfo = MainPlayer.Instance.GetRole2(role.role_id);
			mem.team = Team.Side.eHome;
			mem.team_name = MainPlayer.Instance.Name;
			if (mem.pos == 1)
				config.MainRole = mem;
			else
				config.NPCs.Insert(mem.pos - 2, mem);
		}
		config.leagueType = LeagueType.ePractise;
	}

    public override void OnSceneComplete()
    {
        base.OnSceneComplete();
		m_stateMachine.m_matchStateListeners.Add(this);

		mCurScene.mBall.onCatch = OnCatchBall;

		matchReviseTip = new MatchReviseTip(this);

		timer = new GameUtils.Timer(IM.Number.half, () => { ShowGuide("Intro"); }, 1);

		//load tip resource
		guideTip = UIManager.Instance.CreateUI("GuideTip").AddMissingComponent<GuideTip>();
		guideTip.GetComponent<UIPanel>().depth = 1000;
		guideTip.transform.localPosition = new Vector3(79, 230, 0);
		guideTip.instructorVisible = true;
		guideTip.instructor = "effects_guide";
		guideTip.instructorPos = new Vector3(-444, -18, 0);
		guideTip.onFirstButtonClick = OnTipClick;
		guideTip.Hide();
    }

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		if ( m_stateMachine.m_curState.m_eState != MatchState.State.eOpening && m_stateMachine.m_curState.m_eState != MatchState.State.eOverTime )
		{
			curTipCD -= (float)deltaTime;
			if (timer != null)
				timer.Update(deltaTime);
		}

		if (m_stateMachine.m_curState.m_eState != MatchState.State.ePlaying)
			return;

		matchReviseTip.Update((float)deltaTime);

		if (m_mainRole.m_StateMachine.m_curState.m_eState == PlayerState.State.eHold && m_mainRole.m_bMovedWithBall)
			ShowGuide("HoldLock");

		if (!(m_mainRole.m_StateMachine.m_curState is PlayerState_Skill) &&
			!m_ruler.m_bToCheckBall &&
			m_mainRole.m_bWithBall && m_mainRole.m_bOnGround)
		{
			bool isMainRoleDefended = m_mainRole.IsDefended();
			if (isMainRoleDefended)
			{
				if (m_mainRole.m_position == PositionType.PT_C || m_mainRole.m_position == PositionType.PT_PF)
				{
					//if (completedGuide.Contains("Clutch") && !clutchGuiding)
						ShowGuide("CrossOver", 4);
					//else
					//{
					//	ShowGuide("Clutch", 5);
					//	clutchGuiding = true;
					//}
				}
				else
					ShowGuide("CrossOver", 4);
			}
			else
				ShowGuide("Undefended", 1);
		}
		else
			clutchGuiding = false;

		if (mCurScene.mBall.m_ballState == BallState.eLoseBall &&
			m_mainRole.m_bOnGround &&
			(m_mainRole.m_position == PositionType.PT_PG || m_mainRole.m_position == PositionType.PT_SG))
		{
			IM.Number catchDistance = PlayerState_BodyThrowCatch.GetMaxDistance(m_mainRole);
			IM.Number curDistance = GameUtils.HorizonalDistance(m_mainRole.position, mCurScene.mBall.position);
			if (curDistance <= catchDistance)
				ShowGuide("BodyThrowCatch", 1);
		}

		if (m_mainRole.m_defenseTarget.m_bWithBall &&
			m_mainRole.m_defenseTarget.m_AOD.GetStateByPos(m_mainRole.position) != AOD.Zone.eInvalid)
		{
			if (GameSystem.Instance.StealConfig.GetRatio(m_mainRole.m_defenseTarget.m_StateMachine.m_curState.m_eState) > IM.Number.zero)
				ShowGuide("Steal", 4);
			else if (AIUtils.CanBlock(m_mainRole, m_mainRole.m_defenseTarget, IM.Number.zero, IM.Number.zero, mCurScene.mBasket.m_vShootTarget))
			{
				if (m_mainRole.m_defenseTarget.m_StateMachine.m_curState.m_eState != PlayerState.State.ePrepareToShoot)
					ShowGuide("Block", 1);
			}
		}

		if (mCurScene.mBall.m_ballState == BallState.eRebound)
		{
			IM.Number fDistPlayer2Ball = GameUtils.HorizonalDistance(m_mainRole.position, mCurScene.mBall.position);
			if (fDistPlayer2Ball <= m_mainRole.m_fReboundDist)
			{
				IM.Number minHeight = new IM.Number(1,6);
				IM.Number maxHeight = m_mainRole.m_finalAttrs["rebound_height"] * new IM.Number(0,13) + new IM.Number(3);
				IM.Number ball_height = mCurScene.mBall.position.y;
				bool inReboundRange = minHeight < ball_height && ball_height < maxHeight;
                IM.Vector3 velocity = mCurScene.mBall.curVel;
				if (velocity.y < IM.Number.zero && inReboundRange)
				{
					ShowGuide("Rebound", 1);
				}
			}
		}
	}

	protected override void CreateCustomGUI()
	{
		base.CreateCustomGUI();

		if (GuideSystem.Instance.curModule != null)
			m_uiMatch.enableBack = false;		
	}

	void Pause(bool pause = true)
	{
		GameSystem.Instance.mClient.pause = pause;
	}

	/* btnIndex 0:button of guide tip, 1-5:button of controller */
	void ShowGuide(string guide, int btnIndex = 0, bool ignoreCD = false)
	{
		if (curTipCD > 0f && !ignoreCD)
			return;
		if (!completedGuide.Contains(guide))
		{
			matchReviseTip.HideTip();

			if (btnIndex == 0)
				guideTip.firstButtonVisible = true;
			else
			{
				guideTip.firstButtonVisible = false;
				UIButton btn = m_uiController.m_btns[btnIndex - 1].btn;
				UIEventListener.BoolDelegate onBtnPress = null;
				onBtnPress = (GameObject go, bool pressed) =>
				{
					if (pressed)
					{
						UIEventListener.Get(btn.gameObject).onPress -= onBtnPress;
						OnTipClick(btn.gameObject);
						HighlightButton((uint)(btnIndex - 1), false);
					}
				};
				UIEventListener.Get(btn.gameObject).onPress += onBtnPress;
				HighlightButton((uint)(btnIndex - 1), true);
			}

			guideTip.tip = CommonFunction.GetConstString("MATCH_GUIDE_" + guide);
			guideTip.Show();
			completedGuide.Add(guide);
			curGuide = guide;
			curTipCD = TIP_CD;

			Pause();
		}
	}

	void HighlightButton(uint index, bool highlight = true)
	{
		if (m_uiController != null)
		{
			UIButton btn = m_uiController.m_btns[(int)index].btn;
			if (btn == null)
				return;
			Transform effect = btn.transform.FindChild("ButtonHighlight(Clone)");
			Transform circle = btn.transform.FindChild("CircleHighlight(Clone)");
			if (highlight)
			{
				if (effect == null)
				{
					effect = CommonFunction.InstantiateObject(_button_highlight_prefab, btn.transform).transform;
				}
				if (circle == null)
				{
					circle = CommonFunction.InstantiateObject(_circle_highlight_prefab, btn.transform).transform;
				}
			}
			else
			{
				if (effect != null)
					NGUITools.Destroy(effect.gameObject);
				if (circle != null)
					NGUITools.Destroy(circle.gameObject);
			}
		}
	}

	public void OnMatchStateChange(MatchState oldState, MatchState newState)
	{
		if (newState.m_eState == MatchState.State.eBegin && oldState.m_eState == MatchState.State.eGoal)
			ShowGuide("SwitchRole");
		else if (newState.m_eState == MatchState.State.ePlaying && oldState.m_eState == MatchState.State.eBegin)
		{
			if (m_mainRole.m_team.m_role == MatchRole.eDefense)
				ShowGuide("Defense", 2);
		}
		else if (newState.m_eState == MatchState.State.eTipOff)
			ShowGuide("TipOff", 0, true);
	}

	void OnTipClick(GameObject go)
	{
		guideTip.Hide();
		Pause(false);
		string lastGuide = curGuide;
		curGuide = string.Empty;
		foreach (GameObject indicator in playerIndicators)
		{
			Object.Destroy(indicator);
		}
		playerIndicators.Clear();
		if (areaIndicator != null)
		{
			Object.Destroy(areaIndicator);
			areaIndicator = null;
		}

		if (lastGuide == "Intro")
		{
			PositionIntro("C");
		}
		else if (lastGuide == "CIntro")
		{
			PositionIntro("SF");
		}
		else if (lastGuide == "SFIntro")
		{
			PositionIntro("PGSG");
		}
	}

	void OnCatchBall(UBasketball ball)
	{
		if (ball.m_owner.m_team == m_mainRole.m_team)
			ShowGuide("SwitchMainRole");
	}

	public void OnTimeUp()
	{
		if (m_mainRole.m_team.m_role == MatchRole.eOffense && 
			mCurScene.mBall.m_ballState != BallState.eUseBall_Shoot &&
			m_mainRole.m_bWithBall && gameMatchTime > IM.Number.zero)
		{
			ShowGuide("AttackTimeOut");
		}
	}

	void PositionIntro(string position)
	{
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager.m_Players)
		{
			if ((position == "C" && player.m_position == PositionType.PT_C) ||
				(position == "SF" && player.m_position == PositionType.PT_SF) ||
				(position == "PGSG" && (player.m_position == PositionType.PT_PG || player.m_position == PositionType.PT_SG)) )
			{
				GameObject obj = Object.Instantiate(playerIndicatorPrefab) as GameObject;
				obj.transform.parent = player.model.head;
				obj.transform.localPosition = Vector3.zero;
				playerIndicators.Add(obj);
			}
		}
		if (position == "C")
		{
			areaIndicator = Object.Instantiate(layupAreaPrefab) as GameObject;
		}
		else if (position == "SF")
		{
			areaIndicator = Object.Instantiate(areaPrefab) as GameObject;
			areaIndicator.transform.FindChild("3PT").gameObject.SetActive(false);
		}
		else if (position == "PGSG")
		{
			areaIndicator = Object.Instantiate(areaPrefab) as GameObject;
			areaIndicator.transform.FindChild("2PT").gameObject.SetActive(false);
		}
		ShowGuide(position + "Intro", 0, true);
	}
}
