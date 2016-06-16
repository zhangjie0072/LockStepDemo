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

	override public void OnSceneComplete ()
	{
		base.OnSceneComplete();
		UBasketball ball = mCurScene.CreateBall();
		
		if( m_config == null )
		{
			Logger.LogError("Match config file loading failed.");
			return;
		}

		foreach (PlayerData data in notifyGameStart.data)
			_CreateRoomUser(data);
		foreach (PlayerData data in notifyGameStart.rival_data)
			_CreateRoomUser(data);

		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);

		m_homeTeam.SortMember();
		m_awayTeam.SortMember();
		AssumeDefenseTarget();

        Team oppoTeam = m_mainRole.m_team.m_side == Team.Side.eAway ? m_homeTeam : m_awayTeam;
        foreach (Player member in oppoTeam.members)
        {
            if (member.model != null)
				member.model.EnableGrey();
        }

        m_mainRole.m_team.m_role = GameMatch.MatchRole.eOffense;
        if (m_mainRole.m_defenseTarget != null)
            m_mainRole.m_defenseTarget.m_team.m_role = GameMatch.MatchRole.eDefense;

        _UpdateCamera(m_mainRole);

		m_mainTeam = m_mainRole.m_team;

		m_stateMachine.SetState(MatchState.State.eOpening);

		initDone = true;
	}

    public override void HandleGameBegin(Pack pack)
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

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		UBasketball ball = mCurScene.mBall;
		if( ball == null )
			return;

		if( m_uiMatch != null )
		{
			if (ball.m_owner == null)
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(false);
			}
			else if (ball.m_owner.m_team == m_mainRole.m_team)
			{
				m_uiMatch.leftBall.SetActive(true);
				m_uiMatch.rightBall.SetActive(false);
			}
			else
			{
				m_uiMatch.leftBall.SetActive(false);
				m_uiMatch.rightBall.SetActive(true);
			}
		}

		if( !initDone )
			return;

		if( m_stateMachine.m_curState.m_eState != MatchState.State.eOpening && m_stateMachine.m_curState.m_eState != MatchState.State.eOverTime 
		   && ball != null )
		{
			if( ball.m_owner != null )
			{
				Player owner = ball.m_owner;
				SwitchMainrole(owner);
			}
			else if( ball.m_ballState == BallState.eUseBall_Pass )
			{
				Player interceptor = ball.m_interceptor;
				if( interceptor == null )
				{
					Player owner = ball.m_catcher;
					SwitchMainrole(owner);
				}
				else
				{
					if( interceptor.m_team == m_mainRole.m_team )
						SwitchMainrole(interceptor);
					else if( interceptor.m_defenseTarget != null )
						SwitchMainrole(interceptor.m_defenseTarget);
				}
			}
		}
	}
	
	override public void SwitchMainrole( Player target )
	{
		if( m_mainRole == target || m_mainTeam != target.m_team )
			return;
		if( m_stateMachine.m_curState.m_eState == MatchState.State.eOver )
			return;

		target.m_inputDispatcher = new InputDispatcher(this, target);
		target.m_inputDispatcher.TransmitUncontrolInfo(m_mainRole.m_inputDispatcher);
		m_mainRole.m_inputDispatcher = null;
		
		if( m_mainRole.m_aiMgr != null )
			m_mainRole.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;;
		if (target.m_team.m_role == MatchRole.eDefense)
			target.m_inputDispatcher.disableAIOnAction = true;
		else
			if (target.m_aiMgr != null)
				target.m_aiMgr.m_enable = false;

		m_mainRole.m_InfoVisualizer.ShowStaminaBar(false);
		//if( m_mainRole.m_defenseTarget != null )
		//	m_mainRole.m_defenseTarget.m_AOD.visible = false;

		target.m_InfoVisualizer.ShowStaminaBar(true);
		//if( target.m_defenseTarget != null )
		//	target.m_defenseTarget.m_AOD.visible = true;

		m_mainRole = target;
		_UpdateCamera(m_mainRole);
		InputReader.Instance.player = m_mainRole;

		//Logger.Log("current main role: " + m_mainRole.m_id);
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
		return true;
	}

	public override bool EnableSwitchDefenseTarget()
	{
		return true;
	}
}