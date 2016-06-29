using UnityEngine;
using System.IO;
using System.Collections.Generic;

using fogs.proto.msg;
using ProtoBuf;

/// <summary>
/// 1+2的方式对决另1个玩家数据生成的AI
/// </summary>
public class GameMatch_AsynPVP3ON3 :GameMatch_MultiPlayer
{
	private bool initDone = false;
	NotifyGameStart notifyGameStart;

	public GameMatch_AsynPVP3ON3(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, "", 1);
	}

	public void SetNotifyGameStart(LuaStringBuffer strBuf)
	{
		notifyGameStart = ProtoBuf.Serializer.Deserialize<NotifyGameStart>(new MemoryStream(strBuf.buffer));
	}

	public uint GetRivalScore()
	{
		if (leagueType == LeagueType.eRegular1V1)
			return notifyGameStart.regular.rival_score;
		else if (leagueType == LeagueType.eQualifyingNew)
			return notifyGameStart.qualifying_new.rival_score;
		return 0;
	}

	public string GetRivalName()
	{
		return notifyGameStart.rival_data[0].name;
	}

	protected override void _OnLoadingCompleteImp ()
	{
		base._OnLoadingCompleteImp ();
		UBasketball ball = mCurScene.CreateBall();
		
		if( m_config == null )
		{
			Debug.LogError("Match config file loading failed.");
			return;
		}

		foreach (PlayerData data in notifyGameStart.data)
			_CreateRoomUser(data);
		foreach (PlayerData data in notifyGameStart.rival_data)
			_CreateRoomUser(data);

		mainRole.m_InfoVisualizer.ShowStaminaBar(true);

		m_homeTeam.SortMember();
		m_awayTeam.SortMember();
		AssumeDefenseTarget();

        Team oppoTeam = mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
        foreach (Player member in oppoTeam.members)
        {
            if (member.model != null)
				member.model.EnableGrey();
        }

        mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        if (mainRole.m_defenseTarget != null)
            mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(mainRole);
	}

	protected override void OnLoadingComplete ()
	{
		base.OnLoadingComplete ();
		initDone = true;

		if (m_config.needPlayPlot)
		{
			m_stateMachine.SetState(MatchState.State.ePlotBegin);
		}
		else
		{
			m_stateMachine.SetState(MatchState.State.eOpening);
		}
	}

    public override void OnGameBegin(GameBeginResp resp)
    {
		m_stateMachine.SetState(MatchState.State.eTipOff);
    }

	protected override void _CreatePlayersData ()
	{
	}

	protected override void OnRimCollision (UBasket basket, UBasketball ball)
	{
        m_count24Time = gameMatchTime;
        m_count24TimeStop = false;
		m_b24TimeUp = false;
	}

	protected override Player _GenerateTeamMember (Config.TeamMember member, string name)
	{
		Team team = (member.team == Team.Side.eAway ? m_awayTeam : m_homeTeam);
		Player player = GameSystem.Instance.mClient.mPlayerManager.CreatePlayer(member.roleInfo, team);
		player.m_config = member;

		return player;
	}

	public override bool EnableTakeOver()
	{
		return false;
	}

	public override bool EnableMatchTips ()
	{
		return true;
	}

	public override bool EnablePlayerTips ()
	{
		return true;
	}

	public override bool EnableMatchAchievement ()
	{
		return true;
	}

	public override bool EnableEnhanceAttr()
	{
		return false;
	}

	public override bool EnableSwitchDefenseTarget()
	{
		return true;
	}
}