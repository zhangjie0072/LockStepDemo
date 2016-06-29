using fogs.proto.msg;

public class MatchStateOver_PVP : MatchStateOver 
{
    public MatchStateOver_PVP(MatchStateMachine owner)
        : base(owner)
    {
        m_eState = State.eOver;
    }

    public override void OnEnter(MatchState lastState)
    {
        base.OnEnter(lastState);

        if (!m_match.IsDraw())
        {
            if (m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
                SendClientEndGame1Plus();
            else if (m_match.GetMatchType() == GameMatch.Type.ePVP_3On3)
                SendClientEndGame3V3();
        }
    }

    void SendClientEndGame1Plus()
    {
        TeamMatchData matchData = new TeamMatchData();
        matchData.acc_id = MainPlayer.Instance.AccountID;
        PlayerMatchData homeData = new PlayerMatchData();
        homeData.acc_id = m_match.m_homeTeam.GetMember(0).m_roleInfo.acc_id;
        homeData.exit_type = ExitMatchType.EMT_END;
        matchData.player_data.Add(homeData);
        foreach (Player player in m_match.m_homeTeam)
        {
            matchData.role_data.Add(player.mStatistics.data);
        }
        PlayerMatchData awayData = new PlayerMatchData();
        awayData.acc_id = m_match.m_awayTeam.GetMember(0).m_roleInfo.acc_id;
        awayData.exit_type = ExitMatchType.EMT_END;
        matchData.player_data.Add(awayData);
        foreach (Player player in m_match.m_awayTeam)
        {
            matchData.role_data.Add(player.mStatistics.data);
        }
        GameMsgSender.SendTeamMatchData(matchData);
    }

    void SendClientEndGame3V3()
    {
        TeamMatchData matchData = new TeamMatchData();
        matchData.acc_id = MainPlayer.Instance.AccountID;
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            PlayerMatchData accData = new PlayerMatchData();
            accData.acc_id = player.m_roleInfo.acc_id;
            accData.exit_type = ExitMatchType.EMT_END;
            matchData.player_data.Add(accData);
            matchData.role_data.Add(player.mStatistics.data);
        }
        GameMsgSender.SendTeamMatchData(matchData);
    }
}
