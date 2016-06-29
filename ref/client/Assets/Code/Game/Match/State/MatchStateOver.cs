using UnityEngine;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System.IO;
using fogs.proto.config;

public class MatchStateOver
	: MatchState
{
	public struct FirstWinAssist
	{
		public List<KeyValueData> assist_awards ;
		public uint shiwakan_percent ;
		public uint assist_first_win_times ;
		public uint assist_num ;
	}
	private Team	m_winTeam;
	private Team	m_loseTeam;

	private ulong m_origSessionID;
	private bool m_resultShowing = false;
	private bool m_matchResultSent = false;
	public bool matchResultSent { get { return m_matchResultSent; } }
	private bool m_serverResponsed = false;

	private GameScene.Ending m_ending;
	private MatchResultCameraControl	m_matchResultCamCtrl;

	private TourEndResp tourCompleteResp;
	private EndSectionMatchResp endSectionMatchResp;
	private QualifyingEndResp qualifyingEndResp;
    private EndBullFightResp bullFightResp;
    private EndShootResp shootResp;
    private PVPEndChallengePlusResp challengePlusResp;
    private PVPEndChallengeExResp challengeExResp;
	private PVPEndQualifyingNewerResp qualifyingNewerResp;
	private FirstWinAssist qualifyingNewerAssist;
	private PVPEndRegularResp regularResp;
	private FirstWinAssist regularAssist;
	private PVPEndQualifyingResp qualifyingNewEndResp;
    private EndPracticePveResp endPracticePveResp;
    private int qualifyingScoreDelta;
    private List<KeyValueData> maxDaliyIncomeData;

	private bool m_bOnEnd = false;

	public MatchStateOver(MatchStateMachine owner)
		:base(owner)
	{
		m_eState = MatchState.State.eOver;
	}

	void _UpdateResult()
	{
		m_winTeam  = m_match.GetWinTeam();
		m_loseTeam = m_match.GetLoseTeam();
	}

	public override bool IsCommandValid(Command command)
	{
		return false;
	}

	override public void OnEnter(MatchState lastState)
	{
        m_origSessionID = 0;
		//m_serverResponsed = false;
		//m_matchResultSent = false;
		m_bOnEnd = false;

		if( m_match.leagueType != GameMatch.LeagueType.ePVP )
			m_match.HideAllTip();

		m_match.m_ruler.m_toCheckBallTeam = null;

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			player.model.EnableGrey(false);

			if( player.m_catchHelper != null )
				player.m_catchHelper.enabled = false;

			player.m_enablePickupDetector = false;
			player.m_enableMovement = false;
			player.m_enableAction = false;
		}

		if( m_match.m_uiMatch != null )
			GameObject.Destroy(m_match.m_uiMatch.gameObject);
		if( m_match.m_uiController != null )
			m_match.m_uiController.visible = false;

		_UpdateResult();

		foreach (UBasketball ball in m_match.mCurScene.balls)
		{
			if (ball != null)
			{
				if (ball.m_owner != null)
					ball.m_owner.DropBall(ball);
				ball.Reset();
				ball.SetInitPos(IM.Vector3.zero);
			}
		}

		if( m_match is GameMatch_MultiPlayer && m_match.IsDraw() )
		{
			m_stateMachine.SetState(State.eOverTime);
			return;
		}

		bool isWin = (m_match.mainRole.m_team == m_winTeam);
		List<GameScene.Ending> endings = m_match.mCurScene.m_endings;
		m_ending = endings[Random.Range(0, endings.Count)];
	
		_TeamPos(m_winTeam, true);
		_TeamPos(m_loseTeam, false);

		m_matchResultCamCtrl = new MatchResultCameraControl(m_match, m_ending, isWin);

      
		Debugger.Instance.m_steamer.message  = "Main role " 	+ m_match.mainRole.m_id 					+ " statistics: \n";
		Debugger.Instance.m_steamer.message += "rebound: " 		+ m_match.mainRole.mStatistics.success_rebound_times 	+ " \n";
		Debugger.Instance.m_steamer.message += "block: " 		+ m_match.mainRole.mStatistics.success_block_times 	+ " \n";
		Debugger.Instance.m_steamer.message += "steal: " 		+ m_match.mainRole.mStatistics.success_steal_times 	+ " \n";
		Debugger.Instance.m_steamer.message += "assist: " 		+ m_match.mainRole.mStatistics.secondary_attack 	+ " \n";
		Debugger.Instance.m_steamer.message += "skill: " 		+ m_match.mainRole.mStatistics.skill 	+ " \n";
		Debugger.Instance.m_steamer.message += "shoot near: " 	+ m_match.mainRole.mStatistics.near_score + " \n";
		Debugger.Instance.m_steamer.message += "shoot middle: " + m_match.mainRole.mStatistics.mid_score + " \n";
		Debugger.Instance.m_steamer.message += "shoot far: " 	+ m_match.mainRole.mStatistics.far_score + " \n";

		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.MatchOverPose);
		PlaySoundManager.Instance.PlaySound(MatchSoundEvent.GameOver);

		m_match.m_bgSoundPlayer.Stop();
	}

	void SendMatchResult()
	{
        if (m_match.leagueType == GameMatch.LeagueType.eCareer)
            SendCareerResult(m_match);
        else if (m_match.leagueType == GameMatch.LeagueType.ePractise1vs1)
            SendPractise1vs1Result(m_match);
        else if (m_match.leagueType == GameMatch.LeagueType.eTour)
            SendTourResult();
        else if (m_match.leagueType == GameMatch.LeagueType.eQualifying)
            SendQualifyingResult();
		else if (m_match.leagueType == GameMatch.LeagueType.ePractise)
		{
			HandlePractiseComplete();
		}
		else if (m_match.leagueType == GameMatch.LeagueType.ePracticeLocal)
		{
            m_serverResponsed = true;
		}
        else if (m_match.leagueType == GameMatch.LeagueType.eBullFight)
        {
            if (m_match.m_homeScore > m_match.m_awayScore)
            {
                MainPlayer.Instance.SetBullFightCompleteByClient();
            }
            SendBullFightResult();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eShoot)
        {
            if (m_match.m_homeScore > m_match.m_awayScore)
            {
                MainPlayer.Instance.SetShootCompleteByClient();
            }
            SendShootResult();
        }
		else if (m_match.leagueType == GameMatch.LeagueType.eRegular1V1)
		{
			SendRegular1V1Result();
		}
		else if (m_match.leagueType == GameMatch.LeagueType.eQualifyingNew)
		{
			SendQualifyingNewResult();
		}
        else if (m_match.leagueType == GameMatch.LeagueType.eQualifyingNewerAI)
        {
            SendQualifyingNewerResult();
        }
        else if( m_match.leagueType == GameMatch.LeagueType.eLadderAI)
        {
            SendLadderResult();
        }
	}

	void _TeamPos(Team team, bool isWin)
	{
		int iPos = 0;
		UResultPose[] pos = isWin ? m_ending.winPoses : m_ending.losePoses;
		
		foreach (Player member in team)
		{
            member.m_applyLogicPostion = false;
			member.HideIndicator();
			member.m_InfoVisualizer.SetActive(false);
			if (member.m_AOD != null)
				member.m_AOD.visible = false;
			
			UResultPose posePosition = pos[iPos];
			if (posePosition == null)
				continue;
			member.transform.position = posePosition.transform.position;
			member.transform.forward = posePosition.transform.forward;
			string pose = posePosition.pose;
			if (member.m_gender == GenderType.GT_FEMALE)
			{
				if (member.animMgr.GetPlayInfo(pose + "G1") != null)
					pose += "G1";
			}
			else
			{
				if (member.animMgr.GetPlayInfo(pose + "B1") != null)
					pose += "B1";
			}
			
			
			PlayerState_ResultPose resultPose = member.m_StateMachine.GetState(PlayerState.State.eResultPose) as PlayerState_ResultPose;
			resultPose.pose = pose;
			resultPose.withBall = posePosition.withBall;
			member.m_StateMachine.SetState(resultPose, true);
			iPos++;
		}
	}

	override public void ViewUpdate (float deltaTime)
	{
		if (m_matchResultCamCtrl.m_finished &&
			!GameSystem.Instance.mNetworkManager.connPlat &&
			!GameSystem.Instance.mNetworkManager.isReconnecting)
		{
			m_origSessionID = m_match.m_config.session_id;
			GameSystem.Instance.mNetworkManager.Reconnect();
		}

		if (!m_matchResultSent && GameSystem.Instance.mNetworkManager.connPlat &&
			(m_origSessionID == 0 || m_match.m_config.session_id != m_origSessionID))
		{
			SendMatchResult();
			m_matchResultSent = true;
        }

		if (m_matchResultCamCtrl.m_finished)
            m_match.m_cam.m_Zoom.ReleaseZoom();

		//if( m_matchResultCamCtrl.m_finished && m_matchResultSent && m_serverResponsed)
		if( m_matchResultCamCtrl.m_finished && m_serverResponsed)
			OnEnd();
	}

	void OnEnd()
	{
		bool isWin = (m_winTeam == m_match.mainRole.m_team);
		if (m_match.m_config.needPlayPlot && isWin)
		{
			m_stateMachine.SetState(MatchState.State.ePlotEnd);
			return;
		}

		//keep reulting once
		if( m_resultShowing && !m_bOnEnd )
		{
			if (isWin)
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.MatchWin);
			else
				PlaySoundManager.Instance.PlaySound(MatchSoundEvent.MatchLose);
			m_bOnEnd = true;
		}

        if (m_match.leagueType == GameMatch.LeagueType.eRegular1V1)
		{
			HandleRegularComplete();
		}
        else if (m_match.leagueType == GameMatch.LeagueType.eQualifyingNew)
		{
			HandleQualifyingNewComplete();
		}
		else if ( m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
		{
            //ShowAsynPVPResult(isWin);
			//m_resultShowing = true;
            HandleChallengeComplete();
		}
        else if (m_match.leagueType == GameMatch.LeagueType.eQualifyingNewer)
		{
			HandleQualifyingNewerComplete();
		}
        else if (m_match.GetMatchType() == GameMatch.Type.ePVP_3On3)
        {
            HandleChallengeExComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eTour)
        {
            HandleTourComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eCareer)
        {
            HandleSectionComplete();
        }
        else if (m_match.GetMatchType() == GameMatch.Type.eQualifyingNewerAI)
        {
            HandleQualifyingNewerComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eQualifying)
        {
            HandleQualifyingComplete();
        }
        else if (m_match.GetMatchType() == GameMatch.Type.eLadderAI)
        {
            HandleChallengeExComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eBullFight)
        {
            HandleBullFightComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.eShoot)
        {
            HandleShootComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.ePracticeLocal)
        {
            HandlePracticeLocalComplete();
        }
        else if (m_match.leagueType == GameMatch.LeagueType.ePractise1vs1)
        {
            HandlePracticePveComplete();
        }
        else if (m_match.GetMatchType() == GameMatch.Type.e1On1 || m_match.GetMatchType() == GameMatch.Type.e3On3)
        {
            GameSystem.Instance.mClient.Reset();
            GameSystem.Instance.mClient.mUIManager.curLeagueType = m_match.leagueType;
            SceneManager.Instance.ChangeScene(GlobalConst.SCENE_HALL);
        }
	}
	
	override public void OnExit ()
	{
		if( m_match.m_camFollowPath != null )
			m_match.m_camFollowPath.Stop();
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            player.m_applyLogicPostion = true;
        }
		m_serverResponsed = false;
		m_matchResultSent = false;
		m_bOnEnd = false;
	}
    public void SendPractise1vs1Result(GameMatch match)
    {
        EndPracticePve endPracticePve = new EndPracticePve();
        endPracticePve.session_id = match.m_config.session_id;
        endPracticePve.main_role_side = 0; //主队Or客队
        endPracticePve.score_home = (uint)match.m_homeScore; //主队得分
        endPracticePve.score_away = (uint)match.m_awayScore; //客队得分

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_PRACTICE_1V1;
        req.exit_type = ExitMatchType.EMT_END;
        req.practice_pve = endPracticePve;
        PlatNetwork.Instance.SendExitGameReq(req);
    }

    public void SendCareerResult(GameMatch match)
    {
        EndSectionMatch endSectionMatch = new EndSectionMatch();
        endSectionMatch.session_id = match.m_config.session_id;
        if (match != null)
        {
            endSectionMatch.score_home = (uint)match.m_homeScore;
            endSectionMatch.score_away = (uint)match.m_awayScore;

            GameMatch.Config config = match.GetConfig();
            Team.Side mainSide = config.MainRole.team;
            if (mainSide == Team.Side.eHome)
            {
                endSectionMatch.main_role_side = (uint)Team.Side.eHome;
                if (match.m_homeScore > match.m_awayScore)
                {
                    //主队（己方）比赛数据
                    for (int i = 0; i < match.m_homeTeam.GetMemberCount(); ++i)
                    {
                        RoleMatchData roleData = new RoleMatchData();
                        Player player = match.m_homeTeam.GetMember(i);
                        if (player != null)
                        {
                            roleData.role_id = player.m_roleInfo.id;
                            roleData.score_near = (uint)player.mStatistics.near_score; //内线得分
                            roleData.score_middle = (uint)player.mStatistics.mid_score; //中投得分
                            roleData.score_far = (uint)player.mStatistics.far_score; //三分球得分
                            //roleData.score_total = roleData.score_near + roleData.score_middle + roleData.score_far; //己方总得分
							roleData.score_total = (uint)match.m_homeScore;
                            roleData.rebound = (uint)player.mStatistics.success_rebound_times; //篮板个数
                            roleData.block = (uint)player.mStatistics.success_block_times; //盖帽个数
                            roleData.assist = (uint)player.mStatistics.secondary_attack; //助攻
                            roleData.steal = (uint)player.mStatistics.success_steal_times; //抢断
                            roleData.skill_use = (uint)player.mStatistics.skill; //使用花式技能次数
                        }
                        endSectionMatch.role_data.Add(roleData);
                    }
                    //客队（对方）比赛数据
                    uint opposite_score_near = 0;
                    uint opposite_score_middle = 0;
                    uint opposite_score_shoot_far = 0;
                    for (int i = 0; i < match.m_awayTeam.GetMemberCount(); ++i)
                    {
                        Player player = match.m_awayTeam.GetMember(i);
                        if (player != null)
                        {
                            opposite_score_near += (uint)player.mStatistics.near_score; //内线得分
                            opposite_score_middle += (uint)player.mStatistics.mid_score; //中投得分
                            opposite_score_shoot_far += (uint)player.mStatistics.far_score; //三分球得分
                        }
                    }
                    endSectionMatch.opposite_score_near = opposite_score_near * 2;
                    endSectionMatch.opposite_score_middle = opposite_score_middle * 2;
                    endSectionMatch.opposite_score_shoot_far = opposite_score_shoot_far * 3;
                }
            }
            else
            {
                endSectionMatch.main_role_side = (uint)Team.Side.eAway;
                if (match.m_awayScore > match.m_homeScore)
                {
                    //客队（己方）比赛数据
                    for (int i = 0; i < match.m_awayTeam.GetMemberCount(); ++i)
                    {
                        RoleMatchData roleData = new RoleMatchData();
                        Player player = match.m_awayTeam.GetMember(i);
                        if (player != null)
                        {
                            roleData.role_id = player.m_roleInfo.id;
                            roleData.score_near = (uint)player.mStatistics.near_score; //内线得分
                            roleData.score_middle = (uint)player.mStatistics.mid_score; //中投得分
                            roleData.score_far = (uint)player.mStatistics.far_score; //三分球得分
                            //roleData.score_total = roleData.score_near + roleData.score_middle + roleData.score_far; //己方总得分
							roleData.score_total = (uint)match.m_awayScore;
                            roleData.rebound = (uint)player.mStatistics.success_rebound_times; //篮板个数
                            roleData.block = (uint)player.mStatistics.success_block_times; //盖帽个数
                            roleData.assist = (uint)player.mStatistics.secondary_attack; //助攻
                            roleData.steal = (uint)player.mStatistics.success_steal_times; //抢断
                            roleData.skill_use = (uint)player.mStatistics.skill; //使用花式技能次数
                        }
                        endSectionMatch.role_data.Add(roleData);
                    }
                    //主队（对方）比赛数据
                    uint opposite_score_near = 0;
                    uint opposite_score_middle = 0;
                    uint opposite_score_shoot_far = 0;
                    for (int i = 0; i < match.m_homeTeam.GetMemberCount(); ++i)
                    {
                        Player player = match.m_homeTeam.GetMember(i);
                        if (player != null)
                        {
                            opposite_score_near += (uint)player.mStatistics.near_score; //内线得分
                            opposite_score_middle += (uint)player.mStatistics.mid_score; //中投得分
                            opposite_score_shoot_far += (uint)player.mStatistics.far_score; //三分球得分
                        }
                    }
                    endSectionMatch.opposite_score_near = opposite_score_near * 2;
                    endSectionMatch.opposite_score_middle = opposite_score_middle * 2;
                    endSectionMatch.opposite_score_shoot_far = opposite_score_shoot_far * 3;
                }
            }
        }
        PlatNetwork.Instance.EndSectionMatchReq(endSectionMatch);
    }

	private void SendTourResult()
	{
        TourEndReq tour = new TourEndReq();
		tour.direct_clear = 0u;
		tour.session_id = m_match.m_config.session_id;
		tour.succeed = (m_winTeam == m_match.mainRole.m_team) ? 1u : 0u;

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_TOUR;
        req.exit_type = ExitMatchType.EMT_END;
        req.tour = tour;
		PlatNetwork.Instance.SendExitGameReq(req);
	}

	private void SendRegular1V1Result()
	{
		if (m_match.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
			return;
		PVPEndRegularReq regular = new PVPEndRegularReq();
		regular.score_home = (uint)m_match.m_homeScore;
		regular.score_away = (uint)m_match.m_awayScore;
		regular.main_role_side = (uint)Team.Side.eHome;
		regular.session_id = m_match.m_config.session_id;
		if (m_match.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
		{
			GameMatch_AsynPVP3ON3 match = m_match as GameMatch_AsynPVP3ON3;
			regular.rival_score = match.GetRivalScore();
			regular.flag = 1;
		}
		else if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			regular.flag = 2;
		}
		uint index = 1;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			MatchRoleData data = player.mStatistics.data;
			if (data.role_id >= 10000)
			{
				NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(data.role_id);
				data.role_id = config.shape;
			}
			regular.data.Add(data);
		}

		ExitGameReq req = new ExitGameReq();
		req.acc_id = MainPlayer.Instance.AccountID;
		req.type = MatchType.MT_REGULAR_RACE;
		req.exit_type = ExitMatchType.EMT_END;
		req.regular = regular;
		PlatNetwork.Instance.SendExitGameReq(req);
		Debug.Log("SendRegular1V1Result");
	}

	private void SendQualifyingNewResult()
	{
		PVPEndQualifyingReq qualifying_new = new PVPEndQualifyingReq();
		qualifying_new.score_home = (uint)m_match.m_homeScore;
		qualifying_new.score_away = (uint)m_match.m_awayScore;
		qualifying_new.main_role_side = (uint)Team.Side.eHome;
		qualifying_new.session_id = m_match.m_config.session_id;
		if (m_match.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
		{
			GameMatch_AsynPVP3ON3 match = m_match as GameMatch_AsynPVP3ON3;
			qualifying_new.rival_score = match.GetRivalScore();
			qualifying_new.flag = 1;
		}
		else if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			qualifying_new.flag = 2;
		}
		uint index = 1;
		foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			MatchRoleData data = player.mStatistics.data;
			if (data.role_id >= 10000)
			{
				NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(data.role_id);
				data.role_id = config.shape;
			}
			qualifying_new.data.Add(data);
		}

		ExitGameReq req = new ExitGameReq();
		req.acc_id = MainPlayer.Instance.AccountID;
		req.type = MatchType.MT_QUALIFYING_NEW;
		req.exit_type = ExitMatchType.EMT_END;
		req.qualifying_new = qualifying_new;
		PlatNetwork.Instance.SendExitGameReq(req);
		Debug.Log("SendQualifyingNewResult");
	}


    private void SendQualifyingNewerResult()
    {
        PVPEndQualifyingNewerReq qualifying_new = new PVPEndQualifyingNewerReq();
        qualifying_new.score_home = (uint)m_match.m_homeScore;
        qualifying_new.score_away = (uint)m_match.m_awayScore;
        qualifying_new.main_role_side = (uint)Team.Side.eHome;
        qualifying_new.session_id = m_match.m_config.session_id;
        if (m_match.GetMatchType() == GameMatch.Type.eQualifyingNewerAI)
        {
            qualifying_new.flag = 2;
        }
        uint index = 1;
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            MatchRoleData data = player.mStatistics.data;
            if (data.role_id >= 10000)
            {
                NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(data.role_id);
                data.role_id = config.shape;
            }
            qualifying_new.data.Add(data);
        }
        GameMatch_QualifyingNewerAI aiMatch = m_match as GameMatch_QualifyingNewerAI;
        aiMatch._names.Clear();
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)

        {
            aiMatch._names.Add(player.m_name);
        }

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_QUALIFYING_NEWER;
        req.exit_type = ExitMatchType.EMT_END;
        req.qualifying_newer = qualifying_new;
        PlatNetwork.Instance.SendExitGameReq(req);
        Debug.Log("Send QualifyingNewer");
    }

    private void SendLadderResult()
    {
        PVPEndChallengeExReq chellengeEx = new PVPEndChallengeExReq();
        chellengeEx.score_home = (uint)m_match.m_homeScore;
        chellengeEx.score_away = (uint)m_match.m_awayScore;
        chellengeEx.main_role_side = (uint)Team.Side.eHome;
        chellengeEx.session_id = m_match.m_config.session_id;
        if (m_match.GetMatchType() == GameMatch.Type.eLadderAI)
        {
            chellengeEx.flag = 2;
        }
        uint index = 1;
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            MatchRoleData data = player.mStatistics.data;
            if (data.role_id >= 10000)
            {
                NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(data.role_id);
                data.role_id = config.shape;
            }
            chellengeEx.data.Add(data);
        }
        GameMatch_LadderAI aiMatch = m_match as GameMatch_LadderAI;
        aiMatch._names.Clear();
        foreach (Player player in GameSystem.Instance.mClient.mPlayerManager)
        {
            aiMatch._names.Add(player.m_name);
        }

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_PVP_3V3;
        req.exit_type = ExitMatchType.EMT_END;
        req.challenge_ex = chellengeEx;
        PlatNetwork.Instance.SendExitGameReq(req);
        Debug.Log("Send challenge_ex");
    }


    private void SendBullFightResult()
    {
        EndBullFight endBullFight = new EndBullFight();
        endBullFight.session_id = m_match.m_config.session_id;
        endBullFight.main_role_side = 1;
        endBullFight.score_home = (uint)m_match.m_homeScore;
        endBullFight.score_away = (uint)m_match.m_awayScore;

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_BULLFIGHT;
        req.exit_type = ExitMatchType.EMT_END;
        req.bull_fight = endBullFight;
		PlatNetwork.Instance.SendExitGameReq(req);
    }

    private void SendShootResult()
    {
        EndShoot endShoot = new EndShoot();
        endShoot.session_id = m_match.m_config.session_id;
        endShoot.main_role_side = 1;
        endShoot.score_home = (uint)m_match.m_homeScore;
        endShoot.score_away = (uint)m_match.m_awayScore;

        ExitGameReq req = new ExitGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = MatchType.MT_SHOOT;
        
        req.exit_type = ExitMatchType.EMT_END;
        req.shoot = endShoot;
		PlatNetwork.Instance.SendExitGameReq(req);
    }
	
	
	private void SendQualifyingResult()
	{
		QualifyingEndReq qualifying = new QualifyingEndReq();
		qualifying.session_id = m_match.m_config.session_id;
		qualifying.type = MatchType.MT_QUALIFYING;
		qualifying.main_role_side = 1;
		qualifying.score_home = (uint)m_match.m_homeScore;
		qualifying.score_away = (uint)m_match.m_awayScore;

		ExitGameReq req = new ExitGameReq();
		req.acc_id = MainPlayer.Instance.AccountID;
		req.type = MatchType.MT_QUALIFYING;
		req.exit_type = ExitMatchType.EMT_END;
		req.qualifying = qualifying;
		PlatNetwork.Instance.SendExitGameReq(req);
	}

	public void SendPractiseResult()
	{
		EndPractice practice = new EndPractice();
		practice.session_id = m_match.m_config.session_id;

		ExitGameReq req = new ExitGameReq();
		req.acc_id = MainPlayer.Instance.AccountID;
		req.type = MatchType.MT_PRACTICE;
		req.exit_type = ExitMatchType.EMT_END;
		req.practice = practice;
		PlatNetwork.Instance.SendExitGameReq(req);
	}

    public void SendEnterGamePractise1vs1(EnterGameReq req)
    {
        if (m_origSessionID == 0)
        {
            Debug.LogWarning("origSessionID is zero in MatchStateOver");
            return;
        }
        GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.EnterGameReqID);
        //LuaHelper.SendPlatMsgFromLua((uint)MsgID.EnterGameReqID, req);
    }

    public void SendEnterGame( MatchType type)
    {
        if( m_origSessionID == 0 )
        {
            Debug.LogWarning("origSessionID is zero in MatchStateOver");
            return;
        }

        Debug.Log("SendEnterGame, type =" + type);

        EnterGameReq req = new EnterGameReq();
        req.acc_id = MainPlayer.Instance.AccountID;
        req.type = type;

        GameSystem.Instance.mNetworkManager.m_platConn.SendPack(0, req, MsgID.EnterGameReqID);
        //LuaHelper.SendPlatMsgFromLua((uint)MsgID.EnterGameReqID, req);
    }

    public void TourCompleteHandler(TourEndResp resp)
	{
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
		{
			tourCompleteResp = resp;
		}
		else
		{
			Debug.Log("Complete tour error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
		m_serverResponsed = true;
	}

	public void QualifyingCompleteHandler(QualifyingEndResp resp)
	{
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
		{
			qualifyingEndResp = resp;
		}
		else
		{
			Debug.Log("Complete qualifying error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
		m_serverResponsed = true;
	}


    public void BullFightCompleteHandler(EndBullFightResp resp)
    {
        ErrorID err = (ErrorID)resp.result;
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            bullFightResp = resp;
        }
        else
        {
            Debug.Log("Complete qualifying error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
		m_serverResponsed = true;
    }

    public void ShootCompleteHandler(EndShootResp resp)
    {
        ErrorID err = (ErrorID)resp.result;
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            shootResp = resp;
        }
        else
        {
            Debug.Log("Complete qualifying error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
		m_serverResponsed = true;
    }

    public void ChallengeCompleteHandler(PVPEndChallengePlusResp resp)
    {
        ErrorID err = (ErrorID)resp.result;
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            challengePlusResp = resp;
        }
        else
        {
            Debug.Log("Complete challengePlusResp error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
		m_serverResponsed = true;
    }

    public void ChallengeExCompleteHandler(PVPEndChallengeExResp resp, List<KeyValueData> maxIncomeData = null)
    {
        maxDaliyIncomeData = maxIncomeData;
        ErrorID err = (ErrorID)resp.result;
        Debug.Log("1927 ChallengeExCompleteHandler err=" + err);
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            challengeExResp = resp;
        }
        else
        {
            Debug.Log("Complete challengeExResp error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
		m_serverResponsed = true;
    }

	public void QualifyingNewerCompleteHandler(PVPEndQualifyingNewerResp resp, List<KeyValueData> maxIncomeData = null,List<KeyValueData> assist_awards = null,uint shiwakan_percent = 0,uint assist_first_win_times = 0,uint assist_num = 0)
    {
        maxDaliyIncomeData = maxIncomeData;
        ErrorID err = (ErrorID)resp.result;
        Debug.Log("1927 ChallengeExCompleteHandler err=" + err);
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            qualifyingNewerResp = resp;

			if (err == ErrorID.SUCCESS)
			{
				qualifyingNewerAssist = new FirstWinAssist();
				qualifyingNewerAssist.assist_awards = assist_awards;
				qualifyingNewerAssist.assist_first_win_times = assist_first_win_times;
				qualifyingNewerAssist.shiwakan_percent = shiwakan_percent;
				qualifyingNewerAssist.assist_num = assist_num;
				Debug.Log("qualifyingNewerAssist.assist_first_win_times "+qualifyingNewerAssist.assist_first_win_times);
				Debug.Log("qualifyingNewerAssist.shiwakan_percent "+qualifyingNewerAssist.shiwakan_percent);
				Debug.Log("qualifyingNewerAssist.assist_num "+qualifyingNewerAssist.assist_num);
				foreach(KeyValueData i in assist_awards)
				{
					Debug.Log("qualifyingNewerAssist.assist_awards id "+i.id+",value "+i.value);
				}
			}
//			Logger.
        }
        else
        {
            Debug.Log("Complete QualifyingNewer error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
		m_serverResponsed = true;
    }



	private void HandleTourComplete()
	{
		if (m_resultShowing) return;

        TourEndResp resp = tourCompleteResp;
		if (resp == null) return;
		m_resultShowing = true;

		ErrorID err = (ErrorID)resp.result;
		GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchResult") as GameObject;
		LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
		UIManager.Instance.BringPanelForward(luaCom.gameObject);
		luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
		luaCom.table.Set("leagueType", GameMatch.LeagueType.eTour);
		luaCom.table.Set("awards", resp.awards);
		luaCom.table.Set("maxTourID", resp.max_tour_id);
	}

	public void SectionCompleteHandler(EndSectionMatchResp resp)
	{
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
		{
			endSectionMatchResp = resp;
		}
		else
		{
			Debug.Log("Complete section error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
		m_serverResponsed = true;
	}
    public void PracticePveCompleteHandler(EndPracticePveResp resp)
    {
        ErrorID err = (ErrorID)resp.result;
        if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
        {
            endPracticePveResp = resp;
        }
        else
        {
            Debug.Log("Complete section error: " + ((ErrorID)resp.result).ToString());
            CommonFunction.ShowErrorMsg((ErrorID)resp.result);
        }
        m_serverResponsed = true;
    }

    public void HandlePracticePveComplete()
    {
        if (m_resultShowing) return;

        EndPracticePveResp resp = endPracticePveResp;
        if (resp == null) return;
        m_resultShowing = true;

        ErrorID err = (ErrorID)resp.result;
        LuaComponent luaCom = UIManager.Instance.CreateUI("Prefab/GUI/UIChallengeResult").GetComponent<LuaComponent>();
        UIManager.Instance.BringPanelForward(luaCom.gameObject);
        bool isWin = true;
        if (resp.result != 0) {
            isWin = false;
        }
        luaCom.table.Set("isWin", isWin);
        luaCom.table.Set("leagueType", GameMatch.LeagueType.ePractise1vs1);
        luaCom.table.Set("homeScore", m_match.m_homeScore);
        luaCom.table.Set("awayScore", m_match.m_awayScore);
        luaCom.table.Set("awards", resp.awards);
        LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.ShowResult", luaCom.table);
    }

	public void HandleQualifyingNewerAIComplete()
	{
		if (m_resultShowing) return;

		EndSectionMatchResp resp = endSectionMatchResp;
		if (resp == null) return;
		m_resultShowing = true;

		ErrorID err = (ErrorID)resp.result;
		LuaComponent luaCom = UIManager.Instance.CreateUI("Prefab/GUI/UIChallengeResult").GetComponent<LuaComponent>();
		UIManager.Instance.BringPanelForward(luaCom.gameObject);
		luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
		luaCom.table.Set("leagueType", GameMatch.LeagueType.eCareer);
		luaCom.table.Set("homeScore", m_match.m_homeScore);
		luaCom.table.Set("awayScore", m_match.m_awayScore);
		luaCom.table.Set("starNum", resp.star_value);
        if (resp.info.Count > 0)
            luaCom.table.Set("expDelta", resp.info[0].exp);
        luaCom.table.Set("awards", resp.awards);

		if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			for (int i = 0; i < m_match.m_homeTeam.GetMemberCount(); ++i)
			{
				Player player = m_match.m_homeTeam.GetMember(i);
				PlayerStatistics statistics = player.mStatistics;
				LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
					luaCom.table, player.m_roleInfo, player.m_id, statistics.total_score,
					statistics.infield_shoot, statistics.near_goal + statistics.mid_goal,
					statistics.outfield_shoot, statistics.far_goal,
					statistics.secondary_attack, statistics.success_rebound_times,
					statistics.success_steal_times, statistics.success_block_times,
					0, i + 1, true, null);
			}
			for (int i = 0; i < m_match.m_awayTeam.GetMemberCount(); ++i)
			{
				Player player = m_match.m_awayTeam.GetMember(i);
				PlayerStatistics statistics = player.mStatistics;
				LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
					luaCom.table, player.m_roleInfo, player.m_id, statistics.total_score,
					statistics.infield_shoot, statistics.near_goal + statistics.mid_goal,
					statistics.outfield_shoot, statistics.far_goal,
					statistics.secondary_attack, statistics.success_rebound_times,
					statistics.success_steal_times, statistics.success_block_times,
					0, i + 1, false, null);
			}
		}
		else
			LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.ShowResult", luaCom.table);

		/*
        object obj2 = LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionComplete"];
        bool complete = (bool)(object)obj2;
        object obj = LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionID"];
        uint curID = (uint)(double)obj;
        object obj1 = LuaScriptMgr.Instance.GetLuaTable("_G")["CurChapterID"];
        uint curChapterID = (uint)(double)obj1;
         if ( (((curID % 10000) % 100) == 10) && (err == ErrorID.SUCCESS) && (complete == false))
         {
             luaCom.table.Set("firstComplete", true);
         }
        uint isGift = GameSystem.Instance.CareerConfigData.GetSectionData(curID).role_gift;
        uint get_role = MainPlayer.Instance.CheckGetRole(curChapterID, curID);
        if (isGift != 0 && (err == ErrorID.SUCCESS) && (get_role == 0))
        {
            List<uint> list = GameSystem.Instance.roleGiftConfig.GetRoleGiftList(isGift);
            GameObject UIRolePresented = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIRolePresented") as GameObject;
            LuaComponent luaCom1 = CommonFunction.InstantiateObject(UIRolePresented, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();      
            luaCom1.table.Set("roleList", list);
        }
		*/
	}
	public void HandleSectionComplete()
	{
		if (m_resultShowing) return;

		EndSectionMatchResp resp = endSectionMatchResp;
		if (resp == null) return;
		m_resultShowing = true;

		ErrorID err = (ErrorID)resp.result;
		LuaComponent luaCom = UIManager.Instance.CreateUI("Prefab/GUI/UIChallengeResult").GetComponent<LuaComponent>();
		UIManager.Instance.BringPanelForward(luaCom.gameObject);
		luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
		luaCom.table.Set("leagueType", GameMatch.LeagueType.eCareer);
		luaCom.table.Set("homeScore", m_match.m_homeScore);
		luaCom.table.Set("awayScore", m_match.m_awayScore);
		luaCom.table.Set("starNum", resp.star_value);
        if (resp.info.Count > 0)
            luaCom.table.Set("expDelta", resp.info[0].exp);
        luaCom.table.Set("awards", resp.awards);

		if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			for (int i = 0; i < m_match.m_homeTeam.GetMemberCount(); ++i)
			{
				Player player = m_match.m_homeTeam.GetMember(i);
				PlayerStatistics statistics = player.mStatistics;
				LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
					luaCom.table, player.m_roleInfo, player.m_id, statistics.total_score,
					statistics.infield_shoot, statistics.near_goal + statistics.mid_goal,
					statistics.outfield_shoot, statistics.far_goal,
					statistics.secondary_attack, statistics.success_rebound_times,
					statistics.success_steal_times, statistics.success_block_times,
					0, i + 1, true, null);
			}
			for (int i = 0; i < m_match.m_awayTeam.GetMemberCount(); ++i)
			{
				Player player = m_match.m_awayTeam.GetMember(i);
				PlayerStatistics statistics = player.mStatistics;
				LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
					luaCom.table, player.m_roleInfo, player.m_id, statistics.total_score,
					statistics.infield_shoot, statistics.near_goal + statistics.mid_goal,
					statistics.outfield_shoot, statistics.far_goal,
					statistics.secondary_attack, statistics.success_rebound_times,
					statistics.success_steal_times, statistics.success_block_times,
					0, i + 1, false, null);
			}
		}
		else
			LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.ShowResult", luaCom.table);

		/*
        object obj2 = LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionComplete"];
        bool complete = (bool)(object)obj2;
        object obj = LuaScriptMgr.Instance.GetLuaTable("_G")["CurSectionID"];
        uint curID = (uint)(double)obj;
        object obj1 = LuaScriptMgr.Instance.GetLuaTable("_G")["CurChapterID"];
        uint curChapterID = (uint)(double)obj1;
         if ( (((curID % 10000) % 100) == 10) && (err == ErrorID.SUCCESS) && (complete == false))
         {
             luaCom.table.Set("firstComplete", true);
         }
        uint isGift = GameSystem.Instance.CareerConfigData.GetSectionData(curID).role_gift;
        uint get_role = MainPlayer.Instance.CheckGetRole(curChapterID, curID);
        if (isGift != 0 && (err == ErrorID.SUCCESS) && (get_role == 0))
        {
            List<uint> list = GameSystem.Instance.roleGiftConfig.GetRoleGiftList(isGift);
            GameObject UIRolePresented = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIRolePresented") as GameObject;
            LuaComponent luaCom1 = CommonFunction.InstantiateObject(UIRolePresented, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();      
            luaCom1.table.Set("roleList", list);
        }
		*/
	}


	public void HandleQualifyingComplete()
	{
		if (m_resultShowing) return;

		QualifyingEndResp resp = qualifyingEndResp;
		if (resp == null) return;
		m_resultShowing = true;
        ErrorID err = (ErrorID)resp.result;
        int upRank = 0;
        if ((err == ErrorID.SUCCESS) && MainPlayer.Instance.QualifyingRanking == 0)
            upRank = (int)Random.Range((float)(20000 - resp.cur_ranking), (float)(20000 - resp.cur_ranking + 1000));
        else
            upRank = (int)(MainPlayer.Instance.QualifyingRanking - resp.cur_ranking);
        MainPlayer.Instance.QualifyingRanking = resp.cur_ranking;
		if (upRank <= 0)
        {
            GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchResult") as GameObject;
            LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
            UIManager.Instance.BringPanelForward(luaCom.gameObject);
            luaCom.table.Set("awards", resp.awards);
            luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
            luaCom.table.Set("leagueType", GameMatch.LeagueType.eQualifying);
        }
        else
        {
            List<RoleInfo> homeRoles = new List<RoleInfo>();
            List<RoleInfo> awayRoles = new List<RoleInfo>();
            for (int i = 0; i < m_match.m_homeTeam.members.Count; ++i)
                homeRoles.Add(m_match.m_homeTeam.members[i].m_roleInfo);
            for (int i = 0; i < m_match.m_awayTeam.members.Count; ++i)
                awayRoles.Add(m_match.m_awayTeam.members[i].m_roleInfo);
   
            GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/QualifyingResult") as GameObject;
            LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
            UIManager.Instance.BringPanelForward(luaCom.gameObject);
            luaCom.table.Set("upRank", upRank);
            luaCom.table.Set("homeTeam", homeRoles);
            luaCom.table.Set("homeName", m_match.m_homeTeam.members[0].m_teamName);
            luaCom.table.Set("homeRank", MainPlayer.Instance.QualifyingRanking);
            luaCom.table.Set("awayTeam", awayRoles);
            luaCom.table.Set("awayName", m_match.m_awayTeam.members[0].m_teamName);
            luaCom.table.Set("awayRank", MainPlayer.Instance.QualifyingRanking + upRank);
            luaCom.table.Set("awards", resp.awards);
            luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
            luaCom.table.Set("leagueType", GameMatch.LeagueType.eQualifying);
        }
		
	}

    public void HandleBullFightComplete()
    {
        if (m_resultShowing) return;

        EndBullFightResp resp = bullFightResp;
        if (resp == null) return;
        m_resultShowing = true;
        
        ErrorID err = (ErrorID)resp.result;
        uint fightTimes = resp.times;

        MainPlayer.Instance.SetBullFightTimes(fightTimes);
        GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchResult") as GameObject;
        LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
        UIManager.Instance.BringPanelForward(luaCom.gameObject);
        luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
        luaCom.table.Set("leagueType", GameMatch.LeagueType.eBullFight);
        luaCom.table.Set("awards", resp.awards);
        if (resp.info.Count > 0)
            luaCom.table.Set("increaseExp", resp.info[0].exp);

    }


    public void HandleShootComplete()
    {
        if (m_resultShowing) return;

        EndShootResp resp = shootResp;
        if (resp == null) return;
        m_resultShowing = true;

        ErrorID err = (ErrorID)resp.result;
        uint fightTimes = resp.times;
        MainPlayer.Instance.SetShootedTimes(fightTimes);
        if (err == ErrorID.SUCCESS)
        {
            GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UITurnover") as GameObject;
            LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
            luaCom.table.Set("awardsNum", resp.awards_times);
            if (resp.info.Count > 0)
                luaCom.table.Set("increaseExp", resp.info[0].exp);
            luaCom.table.Set("awards", resp.awards);
            luaCom.table.Set("diamondAwards", resp.diamond_awards);
        }
        else
        {
            GameObject prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/UIMatchResult") as GameObject;
            LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
            UIManager.Instance.BringPanelForward(luaCom.gameObject);
            luaCom.table.Set("isWin", err == ErrorID.SUCCESS);
            luaCom.table.Set("leagueType", GameMatch.LeagueType.eShoot);
            luaCom.table.Set("awards", resp.awards);
            if (resp.info.Count > 0)
                luaCom.table.Set("increaseExp", resp.info[0].exp);
        }

    }

	public void HandlePractiseComplete()
	{
        GameObject prefab = ResourceLoadManager.Instance.GetResources("Prefab/GUI/UIMatchResult") as GameObject;
        LuaComponent luaCom = CommonFunction.InstantiateObject(prefab, UIManager.Instance.m_uiRootBasePanel.transform).GetComponent<LuaComponent>();
        UIManager.Instance.BringPanelForward(luaCom.gameObject);
        luaCom.table.Set("isWin", m_match.m_homeScore > m_match.m_awayScore);
        luaCom.table.Set("leagueType", GameMatch.LeagueType.ePractise);
		luaCom.table.Set("increaseExp", 0);
	}
    public void HandlePracticeLocalComplete()
    {
        LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
        table.Set("uiBack", (object)"UIHall");
        LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIPracticeCourt", null, table });
    }

    public void HandleChallengeComplete()
    {
        uint index = 1;
        if (m_resultShowing) return;
        PVPEndChallengePlusResp resp = challengePlusResp;
        if (resp == null) return;
        m_resultShowing = true;
        ErrorID err = (ErrorID)resp.result;
        bool isWin = (err == ErrorID.SUCCESS);
        GameObject go = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIChallengeResult");
        Transform result = go.transform.FindChild("Window/Result1");
        Transform details = go.transform.FindChild("Window/Details");
        NGUITools.SetActive(result.gameObject, false);
        NGUITools.SetActive(details.gameObject, true);

        Transform winGrid = details.transform.FindChild("We/ListGrid");
        Transform loseGrid = details.transform.FindChild("They/ListGrid");
        Transform homeGrid = winGrid;
        Transform awayGrid = loseGrid;
        List<KeyValueData> homeAwards = resp.home_awards;
        List<KeyValueData> awayAwards = resp.away_awards;
        bool mainData = true;
        bool awayData = false;
        string homeName = resp.main_data[0].name;
        string awayName = resp.away_data[0].name;
        uint homeScore = resp.main_score;
        uint awayScore = resp.away_score;
        uint challengeScore = resp.score;
        if ((uint)m_match.mainRole.m_team.m_side == (uint)Team.Side.eAway)
        {
            homeGrid = loseGrid;
            awayGrid = winGrid;
            homeName = resp.away_data[0].name;
            homeAwards = resp.away_awards;
            awayName = resp.main_data[0].name;
            awayAwards = resp.home_awards;
            homeScore = resp.away_score;
            awayScore = resp.main_score;
            mainData = false;
            awayData = true;
        }
        LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.SetMatchResult",
            go.transform,
            isWin,
            m_match.GetConfig().session_id,
            (uint)m_match.mainRole.m_team.m_side,
            homeName,
            homeScore,
            awayName,
            awayScore,
            homeAwards,
            awayAwards,
            challengeScore);
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        for (int i =0; i < resp.main_data.Count; ++i)
        {
            uint role_id = resp.main_data[i].role_id;
            uint two_score = resp.main_data[i].two_score; //得2分次数
            uint shoot_times = resp.main_data[i].shoot_times;//2 points shoot times
            uint three_score = resp.main_data[i].three_score;// 3 points ontarget
            uint far_shoot_times = resp.main_data[i].far_shoot_times;//3 points shoot times
            uint steal = resp.main_data[i].steal;
            uint block = resp.main_data[i].block;
            uint rebound = resp.main_data[i].rebound;
            uint assist = resp.main_data[i].assist;
            uint mvp = resp.main_data[i].mvp;
            uint totalScore = two_score*2 + three_score*3;
            uint farOnTarget = three_score;
            uint rindex = resp.main_data[i].index;

            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                pm.GetPlayerByRoomId(rindex).m_roleInfo,
                homeName,
                awayName,
                go.transform,
                role_id,
                totalScore.ToString(),
                shoot_times.ToString(),
                two_score.ToString(),
                far_shoot_times.ToString(),
                farOnTarget.ToString(),
                assist.ToString(),
                rebound.ToString(),
                steal.ToString(),
                block.ToString(),
                mvp.ToString(),
                index,
                mainData,
                null);
            ++index;
        }
        index = 1;
        for (int i = 0; i < resp.away_data.Count; ++i)
        {
            uint role_id = resp.away_data[i].role_id;
            uint two_score = resp.away_data[i].two_score; //得2分次数
            uint shoot_times = resp.away_data[i].shoot_times;//2 points shoot times
            uint three_score = resp.away_data[i].three_score;// 3 points ontarget
            uint far_shoot_times = resp.away_data[i].far_shoot_times;//3 points shoot times
            uint steal = resp.away_data[i].steal;
            uint block = resp.away_data[i].block;
            uint rebound = resp.away_data[i].rebound;
            uint assist = resp.away_data[i].assist;
            uint mvp = resp.away_data[i].mvp;
            uint totalTimes = shoot_times + far_shoot_times;
            uint totalScore = two_score * 2 + three_score * 3;
            uint farOnTarget = three_score;
            uint rindex = resp.away_data[i].index;
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                pm.GetPlayerByRoomId(rindex).m_roleInfo,
                homeName,
                awayName,
                go.transform,
                role_id,
                totalScore.ToString(),
                shoot_times.ToString(),
                two_score.ToString(),
                far_shoot_times.ToString(),
                farOnTarget.ToString(),
                assist.ToString(),
                rebound.ToString(),
                steal.ToString(),
                block.ToString(),
                mvp.ToString(),
                index,
                awayData,
                null);
            ++index;
        }
        //totalTimes And winTimes add one
        MainPlayer.Instance.PvpPlusInfo.race_times += 1;
        if (isWin)
            MainPlayer.Instance.PvpPlusInfo.win_times += 1;
        //update challenge score
        MainPlayer.Instance.PvpPlusInfo.score = resp.score;
    }
    public void HandleQualifyingNewerComplete()
    {
        //Debug.Log("1927 HHandleQualifyingNewerComplete called m_resultShowing= " + m_resultShowing + " qualifyingNewerResp=" + qualifyingNewerResp);

        int index = 1;
        if (m_resultShowing) return;
        PVPEndQualifyingNewerResp resp = qualifyingNewerResp;
        if (resp == null) return;
        m_resultShowing = true;
        ErrorID err = (ErrorID)resp.result;
        bool isWin = (err == ErrorID.SUCCESS);
        GameObject go = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIChallengeResult");
        LuaComponent luaCom = go.GetComponent<LuaComponent>();

        List<KeyValueData> homeAwards = resp.home_awards;
        List<KeyValueData> awayAwards = resp.away_awards;
        List<GameData> homeName = resp.main_data;
        List<GameData> awayName = resp.away_data;
        bool mainData = true;
        bool awayData = false;
        uint homeScore = resp.main_score;
        uint awayScore = resp.away_score;
        uint oriLadderScore = MainPlayer.Instance.QualifyingNewerScore;
        MainPlayer.Instance.QualifyingNewerScore = resp.score;
        MainPlayer.Instance.QualifyingNewerInfo.league_info.Clear();
        MainPlayer.Instance.QualifyingNewerInfo.league_info.AddRange(resp.league_info);
        MainPlayer.Instance.QualifyingNewerInfo.race_times = resp.race_times;
        MainPlayer.Instance.QualifyingNewerInfo.win_times = resp.win_times;
        MainPlayer.Instance.QualifyingNewerInfo.max_winning_streak = resp.max_winning_streak;
        MainPlayer.Instance.QualifyingNewerInfo.league_awards_flag = resp.league_extra_score;
        MainPlayer.Instance.QualifyingNewerInfo.grade_awards = resp.grade_awards;
        MainPlayer.Instance.QualifyingNewerInfo.grade_awards_flag = resp.grade_awards_flag;
        MainPlayer.Instance.QualifyingNewerInfo.combo_win = resp.combo_win;



        // Log
        Debug.Log("MainPlayer.Instance.QualifyingNewerInfo.league_awards_flag = " + MainPlayer.Instance.QualifyingNewerInfo.league_awards_flag);
        Debug.Log("MainPlayer.Instance.QualifyingNewerInfo.grade_awards= " + MainPlayer.Instance.QualifyingNewerInfo.grade_awards);
        Debug.Log("MainPlayer.Instance.QualifyingNewerInfo.grade_awards_flag= " + MainPlayer.Instance.QualifyingNewerInfo.grade_awards_flag);

        
        int num = 0;
        int winNum = 0;
        foreach( var v in resp.league_info)
        {
            num++;
            if( v == 1)
            {
                winNum++;
            }
        }
        int ladderShowWin = -1;
        if( num == 5)
        {
            ladderShowWin = winNum;
        }

        //LadderLevel oriLv = GameSystem.Instance.ladderConfig.GetLevelByScore(oriLadderScore);

        ////MainPlayer.Instance.pvpLadderScore = 1800;
        //LadderLevel lv = GameSystem.Instance.ladderConfig.GetLevelByScore(MainPlayer.Instance.pvpLadderScore);


        int ladderLevelState = 0;
        //if( oriLv.level < lv.level )
        //{
        //    ladderLevelState = 1;
        //}
        //else if ( oriLv.level > lv.level )
        //{
        //    ladderLevelState = -1;
        //}
        
       // MainPlayer.Instance.pvpLadderInfo.league_info = resp.league_info;

        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        if ((uint)m_match.mainRole.m_team.m_side == (uint)Team.Side.eAway)
        {
            //homeGrid = loseGrid;
            //awayGrid = winGrid;
            homeScore = resp.away_score;
            awayScore = resp.main_score;
            mainData = false;
            awayData = true;
            homeAwards = resp.away_awards;
            awayAwards = resp.home_awards;
            homeName = resp.away_data;
            awayName = resp.main_data;
        }
        Debug.Log("isWin =" + isWin);
        luaCom.table.Set("isWin", isWin);
        // sessionID
        luaCom.table.Set("homeName", CommonFunction.GetConstString("STR_HOME"));
        luaCom.table.Set("homeScore", homeScore);
        luaCom.table.Set("awayName", CommonFunction.GetConstString("STR_PEER"));
        luaCom.table.Set("awayScore", awayScore);
        luaCom.table.Set("awards", homeAwards);
        luaCom.table.Set("leagueType", GameMatch.LeagueType.eQualifyingNewer);
		//帮助好友拿首胜
		if ((qualifyingNewerAssist.assist_awards!=null && qualifyingNewerAssist.assist_awards.Count>0) || qualifyingNewerAssist.assist_first_win_times>0 || qualifyingNewerAssist.shiwakan_percent>0)
		{
			luaCom.table.Set("assist_awards",qualifyingNewerAssist.assist_awards);
			luaCom.table.Set("assist_first_win_times",qualifyingNewerAssist.assist_first_win_times);
			luaCom.table.Set("shiwakan_percent",qualifyingNewerAssist.shiwakan_percent);
			luaCom.table.Set("assist_num",qualifyingNewerAssist.assist_num);
		}
		else
		{
			luaCom.table.Set("assist_awards",null);
			luaCom.table.Set("assist_first_win_times",0);
			luaCom.table.Set("shiwakan_percent",0);
			luaCom.table.Set("assist_num",0);
		}
        int curScore = (int)MainPlayer.Instance.QualifyingNewerScore;


        int oriScore = (int)oriLadderScore;
        luaCom.table.Set("scoreDelta", curScore - oriScore);

        // TODO: test.
        //ladderLevelState = 1;
        //resp.league_extra_score = 1;

        luaCom.table.Set("ladderLevelState", ladderLevelState);
        //TODO: test
        //ladderShowWin = 2;
        luaCom.table.Set("ladderRewardWin", resp.league_info);
        luaCom.table.Set("league_extra_score",resp.league_extra_score);

        luaCom.table.Set("maxDailyIncomeData", maxDaliyIncomeData);

        bool findMyNameInMain = false;
        for (int i = 0; i < resp.main_data.Count; i++)
        {
            if( resp.main_data[i].name.CompareTo(MainPlayer.Instance.Name) == 0 )
            {
                findMyNameInMain = true;
                break;
            }
        }

            for (int i = 0; i < resp.main_data.Count; ++i)
            {
                uint role_id = resp.main_data[i].role_id;
                uint two_score = resp.main_data[i].two_score; //得2分次数
                uint shoot_times = resp.main_data[i].shoot_times;//2 points shoot times
                uint three_score = resp.main_data[i].three_score;// 3 points ontarget
                uint far_shoot_times = resp.main_data[i].far_shoot_times;//3 points shoot times
                uint steal = resp.main_data[i].steal;
                uint block = resp.main_data[i].block;
                uint rebound = resp.main_data[i].rebound;
                uint assist = resp.main_data[i].assist;
                uint mvp = resp.main_data[i].mvp;
                uint totalScore = two_score * 2 + three_score * 3;
                uint farOnTarget = three_score;
                string name = resp.main_data[i].name;
                uint rindex = resp.main_data[i].index;
                if( string.IsNullOrEmpty(name))
                {
                    GameMatch_QualifyingNewerAI aiMatch = m_match as GameMatch_QualifyingNewerAI;
                    name = aiMatch._names[i];
                    if( name == MainPlayer.Instance.Name)
                    {
                        findMyNameInMain = true;
                    }
                }
                LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                    luaCom.table,
                    pm.GetPlayerByRoomId(rindex).m_roleInfo,
                    role_id,
                    totalScore,
                    shoot_times,
                    two_score,
                    far_shoot_times,
                    three_score,
                    assist,
                    rebound,
                    steal,
                    block,
                    mvp,
                    index,
                    findMyNameInMain,  // home
                    name);
                ++index;
            }
        index = 1;
        for (int i = 0; i < resp.away_data.Count; ++i)
        {
            uint role_id = resp.away_data[i].role_id;
            uint two_score = resp.away_data[i].two_score; //得2分次数
            uint shoot_times = resp.away_data[i].shoot_times;//2 points shoot times
            uint three_score = resp.away_data[i].three_score;// 3 points ontarget
            uint far_shoot_times = resp.away_data[i].far_shoot_times;//3 points shoot times
            uint steal = resp.away_data[i].steal;
            uint block = resp.away_data[i].block;
            uint rebound = resp.away_data[i].rebound;
            uint assist = resp.away_data[i].assist;
            uint mvp = resp.away_data[i].mvp;
            uint totalTimes = shoot_times + far_shoot_times;
            uint totalScore = two_score * 2 + three_score * 3;
            uint farOnTarget = three_score;
            string name = resp.away_data[i].name;
            uint rindex = resp.away_data[i].index;
            if (string.IsNullOrEmpty(name))
            {
                GameMatch_QualifyingNewerAI aiMatch = m_match as GameMatch_QualifyingNewerAI;
                name = aiMatch._names[i + 3 ];
            }

            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table,
                pm.GetPlayerByRoomId(rindex).m_roleInfo,
                role_id,
                totalScore,
                shoot_times,
                two_score,
                far_shoot_times,
                three_score,
                assist,
                rebound,
                steal,
                block,
                mvp,
                index,
                !findMyNameInMain, // away.
                name);
            ++index;
        }
    }


    public void HandleChallengeExComplete()
    {
        //Debug.Log("1927 HandleChallengeExComplete called m_resultShowing= " + m_resultShowing + " challengeExResp =" + challengeExResp);
        int index = 1;
        if (m_resultShowing) return;
        PVPEndChallengeExResp resp = challengeExResp;
        if (resp == null) return;
        m_resultShowing = true;
        ErrorID err = (ErrorID)resp.result;
        bool isWin = (err == ErrorID.SUCCESS);
        GameObject go = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIChallengeResult");
        LuaComponent luaCom = go.GetComponent<LuaComponent>();

        List<KeyValueData> homeAwards = resp.home_awards;
        List<KeyValueData> awayAwards = resp.away_awards;
        List<GameData> homeName = resp.main_data;
        List<GameData> awayName = resp.away_data;
        bool mainData = true;
        bool awayData = false;
        uint homeScore = resp.main_score;
        uint awayScore = resp.away_score;
        uint oriLadderScore = MainPlayer.Instance.pvpLadderScore;
        MainPlayer.Instance.pvpLadderScore = resp.score;
        MainPlayer.Instance.pvpLadderInfo.league_info.Clear();
        MainPlayer.Instance.pvpLadderInfo.league_info.AddRange(resp.league_info);
        MainPlayer.Instance.pvpLadderInfo.race_times = resp.race_times;
        MainPlayer.Instance.pvpLadderInfo.win_times = resp.win_times;
        MainPlayer.Instance.pvpLadderInfo.max_winning_streak = resp.max_winning_streak;
        
        int num = 0;
        int winNum = 0;
        foreach( var v in resp.league_info)
        {
            num++;
            if( v == 1)
            {
                winNum++;
            }
        }
        int ladderShowWin = -1;
        if( num == 5)
        {
            ladderShowWin = winNum;
        }

        LadderLevel oriLv = GameSystem.Instance.ladderConfig.GetLevelByScore(oriLadderScore);

        // TODO: Test for 
        //MainPlayer.Instance.pvpLadderScore = 1800;
        LadderLevel lv = GameSystem.Instance.ladderConfig.GetLevelByScore(MainPlayer.Instance.pvpLadderScore);



        int ladderLevelState = 0;
        if( oriLv.level < lv.level )
        {
            ladderLevelState = 1;
        }
        else if ( oriLv.level > lv.level )
        {
            ladderLevelState = -1;
        }
        

       // MainPlayer.Instance.pvpLadderInfo.league_info = resp.league_info;

        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        if ((uint)m_match.mainRole.m_team.m_side == (uint)Team.Side.eAway)
        {
            //homeGrid = loseGrid;
            //awayGrid = winGrid;
            homeScore = resp.away_score;
            awayScore = resp.main_score;
            mainData = false;
            awayData = true;
            homeAwards = resp.away_awards;
            awayAwards = resp.home_awards;
            homeName = resp.away_data;
            awayName = resp.main_data;
        }
        Debug.Log("isWin =" + isWin);
        luaCom.table.Set("isWin", isWin);
        // sessionID
        luaCom.table.Set("homeName", CommonFunction.GetConstString("STR_HOME"));
        luaCom.table.Set("homeScore", homeScore);
        luaCom.table.Set("awayName", CommonFunction.GetConstString("STR_PEER"));
        luaCom.table.Set("awayScore", awayScore);
        luaCom.table.Set("awards", homeAwards);
        luaCom.table.Set("leagueType", GameMatch.LeagueType.ePVP);

        // TODO: test.
        //ladderLevelState = 1;
        //resp.league_extra_score = 1;

        luaCom.table.Set("ladderLevelState", ladderLevelState);
        //TODO: test
        //ladderShowWin = 2;
        luaCom.table.Set("ladderRewardWin", resp.league_info);
        luaCom.table.Set("league_extra_score",resp.league_extra_score);

        luaCom.table.Set("maxDailyIncomeData", maxDaliyIncomeData);

        bool findMyNameInMain = false;
        for (int i = 0; i < resp.main_data.Count; i++)
        {
            if( resp.main_data[i].name.CompareTo(MainPlayer.Instance.Name) == 0 )
            {
                findMyNameInMain = true;
                break;
            }
        }

            for (int i = 0; i < resp.main_data.Count; ++i)
            {
                uint role_id = resp.main_data[i].role_id;
                uint two_score = resp.main_data[i].two_score; //得2分次数
                uint shoot_times = resp.main_data[i].shoot_times;//2 points shoot times
                uint three_score = resp.main_data[i].three_score;// 3 points ontarget
                uint far_shoot_times = resp.main_data[i].far_shoot_times;//3 points shoot times
                uint steal = resp.main_data[i].steal;
                uint block = resp.main_data[i].block;
                uint rebound = resp.main_data[i].rebound;
                uint assist = resp.main_data[i].assist;
                uint mvp = resp.main_data[i].mvp;
                uint totalScore = two_score * 2 + three_score * 3;
                uint farOnTarget = three_score;
                string name = resp.main_data[i].name;
                uint rindex = resp.main_data[i].index;

                if( string.IsNullOrEmpty(name))
                {
                    GameMatch_LadderAI aiMatch = m_match as GameMatch_LadderAI;
                    name = aiMatch._names[i];
                    if( name == MainPlayer.Instance.Name)
                    {
                        findMyNameInMain = true;
                    }
                }
                LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                    luaCom.table,
                    pm.GetPlayerByRoomId(rindex).m_roleInfo,
                    role_id,
                    totalScore,
                    shoot_times,
                    two_score,
                    far_shoot_times,
                    three_score,
                    assist,
                    rebound,
                    steal,
                    block,
                    mvp,
                    index,
                    findMyNameInMain,  // home
                    name);
                ++index;
            }
        index = 1;
        for (int i = 0; i < resp.away_data.Count; ++i)
        {
            uint role_id = resp.away_data[i].role_id;
            uint two_score = resp.away_data[i].two_score; //得2分次数
            uint shoot_times = resp.away_data[i].shoot_times;//2 points shoot times
            uint three_score = resp.away_data[i].three_score;// 3 points ontarget
            uint far_shoot_times = resp.away_data[i].far_shoot_times;//3 points shoot times
            uint steal = resp.away_data[i].steal;
            uint block = resp.away_data[i].block;
            uint rebound = resp.away_data[i].rebound;
            uint assist = resp.away_data[i].assist;
            uint mvp = resp.away_data[i].mvp;
            uint totalTimes = shoot_times + far_shoot_times;
            uint totalScore = two_score * 2 + three_score * 3;
            uint farOnTarget = three_score;
            string name = resp.away_data[i].name;
            uint rindex = resp.away_data[i].index;

            if (string.IsNullOrEmpty(name))
            {
                GameMatch_LadderAI aiMatch = m_match as GameMatch_LadderAI;
                name = aiMatch._names[i + 3 ];
            }
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table,
                pm.GetPlayerByRoomId(rindex).m_roleInfo,
                role_id,
                totalScore,
                shoot_times,
                two_score,
                far_shoot_times,
                three_score,
                assist,
                rebound,
                steal,
                block,
                mvp,
                index,
                !findMyNameInMain, // away.
                name);
            ++index;
        }
    }
	public void RegularCompleteHandler(PVPEndRegularResp resp,List<KeyValueData> maxIncomeData = null,List<KeyValueData> assist_awards = null,uint shiwakan_percent = 0,uint assist_first_win_times = 0,uint assist_num = 0)
	{
        maxDaliyIncomeData = maxIncomeData;
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
		{
			regularResp = resp;
			MainPlayer.Instance.pvp_regular.score = resp.score;
			if (err == ErrorID.SUCCESS)
			{
				regularAssist = new FirstWinAssist();
				regularAssist.assist_awards = assist_awards;
				regularAssist.assist_first_win_times = assist_first_win_times;
				regularAssist.shiwakan_percent = shiwakan_percent;
				regularAssist.assist_num = assist_num;
			}
			Debug.Log("RegularCompleteHandler, score: " + resp.score);
		}
		else
		{
			Debug.Log("Complete section error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
		m_serverResponsed = true;
	}

	private void HandleRegularComplete()
	{
		if (m_resultShowing) return;

		PVPEndRegularResp resp = regularResp;
		if (resp == null) return;
		m_resultShowing = true;

        int index = 1;
        ErrorID err = (ErrorID)resp.result;
        bool isWin = (err == ErrorID.SUCCESS);

        GameObject go = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIChallengeResult");
		LuaComponent luaCom = go.GetComponent<LuaComponent>();

        List<KeyValueData> homeAwards = resp.home_awards;
        List<KeyValueData> awayAwards = resp.away_awards;
        bool mainData = true;
        bool awayData = false;
		string homeName = string.Empty;
		string awayName = string.Empty;
		if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			homeName = MainPlayer.Instance.Name;
			awayName = (string)(LuaScriptMgr.Instance.GetLuaTable("Regular1V1Handler")["npcRivalName"]);
		}
		else if (m_match.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
		{
			homeName = MainPlayer.Instance.Name;
			awayName = (m_match as GameMatch_AsynPVP3ON3).GetRivalName();
		}
		else
		{
			homeName = resp.main_data[0].name;
			awayName = resp.away_data[0].name;
		}
        uint homeScore = resp.main_score;
        uint awayScore = resp.away_score;
        if ((uint)m_match.mainRole.m_team.m_side == (uint)Team.Side.eAway)
        {
            homeAwards = resp.away_awards;
            awayAwards = resp.home_awards;
			string tmpHomeName = homeName;
			homeName = awayName;
			awayName = tmpHomeName;
            homeScore = resp.away_score;
            awayScore = resp.main_score;
            mainData = false;
            awayData = true;
        }
		if (m_match.GetMatchType() != GameMatch.Type.ePVP_1PLUS)
		{
			homeScore = (uint)(m_match.m_homeScore);
			awayScore = (uint)(m_match.m_awayScore);
		}
		luaCom.table.Set("isWin", isWin);
		luaCom.table.Set("leagueType", m_match.leagueType);
		luaCom.table.Set("homeScore", homeScore);
		luaCom.table.Set("homeName", homeName);
		luaCom.table.Set("awayScore", awayScore);
		luaCom.table.Set("awayName", awayName);
		luaCom.table.Set("awards", homeAwards);
        luaCom.table.Set("maxDailyIncomeData", maxDaliyIncomeData);
		//帮助好友拿首胜
		if ((regularAssist.assist_awards!=null && regularAssist.assist_awards.Count>0) || regularAssist.assist_first_win_times>0 || regularAssist.shiwakan_percent>0)
		{
			luaCom.table.Set("assist_awards",regularAssist.assist_awards);
			luaCom.table.Set("assist_first_win_times",regularAssist.assist_first_win_times);
			luaCom.table.Set("shiwakan_percent",regularAssist.shiwakan_percent);
			luaCom.table.Set("assist_num",regularAssist.assist_num);
		}
		else
		{
			luaCom.table.Set("assist_awards",null);
			luaCom.table.Set("assist_first_win_times",0);
			luaCom.table.Set("shiwakan_percent",0);
			luaCom.table.Set("assist_num",0);
		}
        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        for (int i =0; i < resp.main_data.Count; ++i)
        {
			GameData data = resp.main_data[i];
			Player player = pm.GetPlayerByRoomId(data.index);
            uint totalScore = data.two_score * 2 + data.three_score * 3;
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table, player.m_roleInfo, player.m_id, totalScore,
                data.shoot_times, data.two_score, data.far_shoot_times, data.three_score,
                data.assist, data.rebound, data.steal, data.block, data.mvp,
                index, mainData, null);
            ++index;
        }
        index = 1;
        for (int i = 0; i < resp.away_data.Count; ++i)
        {
			GameData data = resp.away_data[i];
			Player player = pm.GetPlayerByRoomId(data.index);
            uint totalScore = data.two_score * 2 + data.three_score * 3;
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table, player.m_roleInfo, player.m_id, totalScore,
                data.shoot_times, data.two_score, data.far_shoot_times, data.three_score,
                data.assist, data.rebound, data.steal, data.block, data.mvp,
                index, awayData, null);
            ++index;
        }
	}

	public void QualifyingNewCompleteHandler(PVPEndQualifyingResp resp, int scoreDelta,List<KeyValueData> maxIncomeData = null)
	{
        maxDaliyIncomeData = maxIncomeData;
		ErrorID err = (ErrorID)resp.result;
		if (err == ErrorID.SUCCESS || err == ErrorID.MATCH_LOSE)
		{
			qualifyingNewEndResp = resp;
			qualifyingScoreDelta = scoreDelta;
		}
		else
		{
			Debug.Log("Complete section error: " + ((ErrorID)resp.result).ToString());
			CommonFunction.ShowErrorMsg((ErrorID)resp.result);
		}
		m_serverResponsed = true;
	}

	private void HandleQualifyingNewComplete()
	{
		if (m_resultShowing) return;

		PVPEndQualifyingResp resp = qualifyingNewEndResp;
		if (resp == null) return;
		m_resultShowing = true;

        int index = 1;
        ErrorID err = (ErrorID)resp.result;
        bool isWin = (err == ErrorID.SUCCESS);

        GameObject go = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIChallengeResult");
		LuaComponent luaCom = go.GetComponent<LuaComponent>();

        List<KeyValueData> homeAwards = resp.home_awards;
        List<KeyValueData> awayAwards = resp.away_awards;
        bool mainData = true;
        bool awayData = false;
		string homeName = string.Empty;
		string awayName = string.Empty;
		if (m_match.GetMatchType() == GameMatch.Type.eCareer3On3)
		{
			homeName = MainPlayer.Instance.Name;
			awayName = (string)(LuaScriptMgr.Instance.GetLuaTable("UIQualifyingNew")["npcRivalName"]);
		}
		else if (m_match.GetMatchType() == GameMatch.Type.eAsynPVP3On3)
		{
			homeName = MainPlayer.Instance.Name;
			awayName = (m_match as GameMatch_AsynPVP3ON3).GetRivalName();
		}
		else
		{
			homeName = resp.main_data[0].name;
			awayName = resp.away_data[0].name;
		}
        uint homeScore = resp.main_score;
        uint awayScore = resp.away_score;
        if ((uint)m_match.mainRole.m_team.m_side == (uint)Team.Side.eAway)
        {
            homeAwards = resp.away_awards;
            awayAwards = resp.home_awards;
			string tmpHomeName = homeName;
			homeName = awayName;
			awayName = tmpHomeName;
            homeScore = resp.away_score;
            awayScore = resp.main_score;
            mainData = false;
            awayData = true;
        }
		if (m_match.GetMatchType() != GameMatch.Type.ePVP_1PLUS)
		{
			homeScore = (uint)(m_match.m_homeScore);
			awayScore = (uint)(m_match.m_awayScore);
		}
		luaCom.table.Set("isWin", isWin);
		luaCom.table.Set("leagueType", m_match.leagueType);
		//luaCom.table.Set("score", resp.score);
		luaCom.table.Set("scoreDelta", qualifyingScoreDelta);
		luaCom.table.Set("homeScore", homeScore);
		luaCom.table.Set("homeName", homeName);
		luaCom.table.Set("awayScore", awayScore);
		luaCom.table.Set("awayName", awayName);
		luaCom.table.Set("awards", homeAwards);
        luaCom.table.Set("maxDailyIncomeData", maxDaliyIncomeData);

        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        for (int i =0; i < resp.main_data.Count; ++i)
        {
			GameData data = resp.main_data[i];
			Player player = pm.GetPlayerByRoomId(data.index);
            uint totalScore = data.two_score * 2 + data.three_score * 3;
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table, player.m_roleInfo, player.m_id, totalScore,
                data.shoot_times, data.two_score, data.far_shoot_times, data.three_score,
                data.assist, data.rebound, data.steal, data.block, data.mvp,
                index, mainData, null);
            ++index;
        }
        index = 1;
        for (int i = 0; i < resp.away_data.Count; ++i)
        {
			GameData data = resp.away_data[i];
			Player player = pm.GetPlayerByRoomId(data.index);
            uint totalScore = data.two_score * 2 + data.three_score * 3;
            LuaScriptMgr.Instance.CallLuaFunction("UIChallengeResult.InitPlayerMatchData",
                luaCom.table, player.m_roleInfo, player.m_id, totalScore,
                data.shoot_times, data.two_score, data.far_shoot_times, data.three_score,
                data.assist, data.rebound, data.steal, data.block, data.mvp,
                index, awayData, null);
            ++index;
        }

		if (resp.is_upgrade == 1)
		{
			LuaInterface.LuaCSFunction funcOnConfirm = (System.IntPtr L) =>
			{
				GameObject goUpgrade = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UIQualifyingUpgrade");
				LuaComponent luaComUpgrade = goUpgrade.GetComponent<LuaComponent>();
				luaComUpgrade.table.Set("success", isWin);
				luaComUpgrade.table.Set("score", MainPlayer.Instance.qualifying_new.score);
				luaComUpgrade.table.Set("scoreDelta", qualifyingScoreDelta);
				//luaComUpgrade.table.Set("ranking", MainPlayer.Instance.qualifying_new.ranking);
				UIManager.Instance.BringPanelForward(goUpgrade);
				return 0;
			};
			luaCom.table.Set("onConfirm", funcOnConfirm);
		}
	}
}
