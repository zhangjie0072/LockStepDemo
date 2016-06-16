using System;
using UnityEngine;
using fogs.proto.config;
using System.Collections.Generic;
using fogs.proto.msg;

public class MatchStatePlot
    : MatchState
{
    private UIPlayPlot plotUI;
    private SectionConfig m_sectionConfig;
    private List<PlotConfig> m_plotConfig;
    uint m_nextDialogID = 0;

    public MatchStatePlot(MatchStateMachine owner, State state)
        : base(owner)
    {
        m_eState = state;
    }

    override public void OnEnter(MatchState lastState)
    {
        GameMatch match = m_match;
        if (match == null || match.mCurScene == null)
            return;

        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            player.m_enableAction = false;
            player.m_enableMovement = false;

            if (player.m_aiMgr != null)
                player.m_aiMgr.m_enable = false;

            if (player.m_catchHelper != null)
                player.m_catchHelper.enabled = false;

            player.m_bToCatch = false;
            if (player.m_pickupDetector != null)
            {
                player.m_enablePickupDetector = false;
                Logger.Log("MatchStatePlotBegin:  pickup detector sets to false.");
            }

            //player.m_enbleMovement = false;

            //player.m_StateMachine.SetState(PlayerState.State.eStand, true);

            player.Hide();
        }

        if (plotUI == null)
        {
            plotUI = GameSystem.Instance.mClient.mUIManager.CreateUI("UIPlayPlot").GetComponent<UIPlayPlot>();
			plotUI.onNext = OnNextDialogClick;
			if (plotUI == null)
			{
				Logger.Log("Error -- can not find UI resource " + "UIPlayPlot");
				return;
			}
        }

        uint sectionID = (uint)(double)(LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionID"]);
        m_sectionConfig = GameSystem.Instance.CareerConfigData.GetSectionData(sectionID);
        uint firstDialog = 0;
        if (m_eState == State.ePlotBegin)
        {
            firstDialog = m_sectionConfig.plot_begin_id;
        }
        else if (m_eState == State.ePlotEnd)
        {
            firstDialog = m_sectionConfig.plot_end_id;
        }
        if (CareerConfig.plotConfig.ContainsKey(firstDialog) == false)
        {
            if (m_eState == State.ePlotBegin)
            {
                m_stateMachine.SetState(MatchState.State.eShowRule);
            }
            else
            {
                OnEndResult();
            }
            return;
        }
        m_plotConfig = CareerConfig.plotConfig[firstDialog];
        m_nextDialogID = m_plotConfig[0].dialog_id;
        ShowDialog(m_nextDialogID);

        for (int i = 0; i < match.m_homeTeam.GetMemberCount(); ++i)
        {
            match.m_homeTeam.GetMember(i).m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
        }
        for (int i = 0; i < match.m_awayTeam.GetMemberCount(); ++i)
        {
            match.m_awayTeam.GetMember(i).m_InfoVisualizer.m_goPlayerInfo.SetActive(false);
        }

		if( match.m_uiMatch != null )
		{
            match.m_gameMatchCountStop = true;
            match.m_count24TimeStop = true;
		}

    }

    override public void OnExit()
    {
		plotUI.Hide();
    }

    public void ShowDialog(uint dialogID)
    {
        if (dialogID == 0)
        {
            if (m_eState == State.ePlotBegin)
            {
                GameMatch match = GameSystem.Instance.mClient.mCurMatch;
                for (int i = 0; i < match.m_homeTeam.GetMemberCount(); ++i)
                {
                    match.m_homeTeam.GetMember(i).m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
                }
                for (int i = 0; i < match.m_awayTeam.GetMemberCount(); ++i)
                {
                    match.m_awayTeam.GetMember(i).m_InfoVisualizer.m_goPlayerInfo.SetActive(true);
                }
                m_stateMachine.SetState(MatchState.State.eShowRule);
            }
            else if (m_eState == State.ePlotEnd)
            {
                //m_stateMachine.SetState(MatchState.State.eTipOff); 
				plotUI.Hide();
                OnEndResult();
            }
            return;
        }
        PlotConfig config = null;
        for (int i = 0; i < m_plotConfig.Count; ++i)
        {
            if (m_plotConfig[i].dialog_id == dialogID)
                config = m_plotConfig[i];
        }
        if (config == null)
        {
            if (m_eState == State.ePlotBegin)
            {
                m_stateMachine.SetState(MatchState.State.eShowRule);
            }
            else if (m_eState == State.ePlotEnd)
            {
                //m_stateMachine.SetState(MatchState.State.eTipOff);
                OnEndResult();
            }
            return;
        }
        //
        if (config.icon == "self")
        {
			plotUI.Show(0, m_match.m_mainRole.m_roleInfo.id, config.content);
        }
        else
        {
            uint npcID = 0;
            uint.TryParse(config.icon, out npcID);
			plotUI.Show(1, npcID, config.content);
        }
        m_nextDialogID = config.next_dialog_id;
    }

    public void OnNextDialogClick()
    {
        ShowDialog(m_nextDialogID);
    }

    public void OnEndResult()
    {
        MatchStateOver match = m_stateMachine.GetState(MatchState.State.eOver) as MatchStateOver;
        match.HandleSectionComplete();
    }

    public override bool IsCommandValid(Command command)
    {
        return false;
    }
}
