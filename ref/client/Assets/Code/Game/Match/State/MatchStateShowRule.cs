using System;
using UnityEngine;

public class MatchStateShowRule : MatchState
{
	GameUtils.Timer4View timer;
	GameObject panel;
	string playerPrefName;

	public MatchStateShowRule(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eShowRule;
	}
	
	override public void OnEnter ( MatchState lastState )
	{
		base.OnEnter(lastState);

		if (m_match.m_uiMatch != null)
		{
            m_match.m_count24TimeStop = false;
			m_match.m_gameMatchCountStop = false;
		}

		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			player.m_enableAction = false;
			player.m_enableMovement = false;
			player.m_enablePickupDetector = false;
			player.Hide();
		}

		if (m_match.GetMatchType() != GameMatch.Type.eBullFight &&
		    m_match.GetMatchType() != GameMatch.Type.eGrabPoint &&
			m_match.GetMatchType() != GameMatch.Type.eGrabZone &&
			m_match.GetMatchType() != GameMatch.Type.eMassBall &&
			m_match.GetMatchType() != GameMatch.Type.eUltimate21 &&
			m_match.GetMatchType() != GameMatch.Type.eBlockStorm &&
			m_match.GetMatchType() != GameMatch.Type.eReboundStorm )
		{
			//m_stateMachine.SetState(MatchState.State.ePlayerCloseUp);
			m_stateMachine.SetState(MatchState.State.eOpening);
			return;
		}

		playerPrefName = "DontPromptRule_" + MainPlayer.Instance.AccountID + "_" + m_match.m_config.extra_info;
		bool dontPrompt = (PlayerPrefs.GetInt(playerPrefName) != 0);

		if (!dontPrompt)
		{
			panel = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/MatchRule");
			NGUITools.BringForward(panel);

            //GameObject goRule = panel.transform.FindChild("Window/Rule").gameObject;
            GameObject goRulePane = panel.transform.FindChild("Window/Rule/RulePane").gameObject;
            string total = CommonFunction.GetConstString("MATCH_RULE_" + m_match.GetMatchType());
            string[] rules = total.Split('\n');            
            GameObject[] goItem = new GameObject[rules.Length];
            goItem[0] = goRulePane.transform.FindChild("1").gameObject;
            GameObject last = null;
            for(int i = 0; i< rules.Length; ++i)
            {
                if(i >= 1)
                {
                    goItem[i] = CommonFunction.InstantiateObject(goItem[0], goRulePane.transform);
                }
                goItem[i].transform.FindChild("Round/Num").GetComponent<UILabel>().text = (i + 1).ToString();
                goItem[i].transform.FindChild("Label").GetComponent<UILabel>().text = rules[i];
                UIWidget widget = goItem[i].transform.GetComponent<UIWidget>();
                if (last != null)
                {
                    widget.topAnchor.target = last.transform;
                    widget.topAnchor.Set(0, -12);
                    widget.ResetAnchors();
                }
                last = widget.gameObject;
            }
            goRulePane.transform.FindChild("Title").GetComponent<UILabel>().text =
                CommonFunction.GetConstString("MATCH_TYPE_NAME_" + m_match.GetMatchType().ToString());

			//UISprite title = panel.transform.FindChild("Window/Title").GetComponent<UISprite>();
			//title.spriteName = "gameInterface_ozd_" + m_match.GetMatchType().ToString();
			//title.MakePixelPerfect();
			UIEventListener.Get(panel.transform.FindChild("Window/OK").gameObject).onClick += OnOKClick;

			timer = new GameUtils.Timer4View(GameSystem.Instance.CommonConfig.GetFloat("gRuleDisplayTime"), EndShowRule);
			if (GameSystem.Instance.mClient.pause)
			{
				NGUITools.SetActive(panel.gameObject, false);
				timer.stop = true;
			}
		}
		else
		{
			if (m_match.GetMatchType() == GameMatch.Type.eUltimate21)
				m_stateMachine.SetState(MatchState.State.eSlotMachineUltimate21);
			else
				m_stateMachine.SetState(MatchState.State.eOpening);
				//m_stateMachine.SetState(MatchState.State.ePlayerCloseUp);
		}

		if( m_match.m_uiMatch != null )
		{
            m_match.m_gameMatchCountStop = true;
            m_match.m_count24TimeStop = true;
		}
	}
	
	override public void GameUpdate (IM.Number fDeltaTime)
	{
		base.GameUpdate(fDeltaTime);

		if (!NGUITools.GetActive(panel.gameObject) && !GameSystem.Instance.mClient.pause)
		{
			NGUITools.SetActive(panel.gameObject, true);
			timer.stop = false;
		}

		if (timer != null)
			timer.Update((float)fDeltaTime);
	}
	
	void OnOKClick(GameObject go)
	{
		PlayerPrefs.SetInt(playerPrefName, panel.transform.FindChild("Window/DontPrompt").GetComponent<UIToggle>().value ? 1 : 0);
		EndShowRule();
	}

	void EndShowRule()
	{
		NGUITools.Destroy(panel);

		if (m_match.GetMatchType() == GameMatch.Type.eUltimate21)
			m_stateMachine.SetState(MatchState.State.eSlotMachineUltimate21);
		else
			m_stateMachine.SetState(MatchState.State.eOpening);
			//m_stateMachine.SetState(MatchState.State.ePlayerCloseUp);
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}
}
