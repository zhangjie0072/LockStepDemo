using UnityEngine;
using System.IO;
using System.Collections.Generic;

using fogs.proto.msg;
using ProtoBuf;


public class PlayerDataBridge
{
	public uint 		acc_id;			 
	public string 		name;				 
	public uint 		level;				
	public List<RoleInfo> roles = new List<RoleInfo>();
    public List<EquipInfo> equipInfos = new List<EquipInfo>();
    public List<FightRole> squadInfos = new List<FightRole>();
	public uint		is_room_master;
	public uint		is_room_ready;		 
	public uint		is_home_field;		
}

public class StartPositionInfo
{
	public TeamType			teamType;
	public FightRole	fightRole;
}

/// <summary>
/// 【2种玩法】
/// 1+2VS1+2 （2个真人）
/// 3VS3 （6个真人）
/// </summary>
public class GameMatch_PVP
	:GameMatch_MultiPlayer
{
	public bool	m_bPlayerBuildDone{get; private set;}
	public delegate void OnAllPlayerConnected( List<PlayerData> lstPlayerData );
	public OnAllPlayerConnected onPlayerConnected;
    public List<Player> m_PlayersToControl = new List<Player>();
	public GameUtils.Timer m_overTimer{get; private set;}

	private bool m_bSceneBuild = false;
	private List<PlayerData>	m_playerToBuild = new List<PlayerData>();
	private List<StartPositionInfo>	m_posInfo = new List<StartPositionInfo>();

	public GameMatch_PVP(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.onServerConnected += OnGameServerConn;

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, config.ip, config.port);

		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.EnterGameSrvRespID, 	OnEnterGameSrv);
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.EnterGameRespID, 	OnEnterGame);
		
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.GameFaulID, 			HandleGameFaul);
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.GameOverID, 			HandleGameOver);

		mgr.m_gameMsgHandler.RegisterHandler(MsgID.BeginTipOffRespID, 	HandleTipOffBegin);
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.InstructionBroadcastID, 	HandleBroadcast);

		m_bPlayerBuildDone = false;

		mgr.m_gameConn.m_profiler.BeginRecordDataUsage();

		m_overTimer = new GameUtils.Timer(new IM.Number(3), _HandleGameOver, 1);
		m_overTimer.stop = true;
	}

	void _HandleGameOver()
	{
		m_stateMachine.SetState(MatchState.State.eOver); 
		m_overTimer.stop = true;
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
		m_bSceneBuild = true;
		m_needTipOff = true;
		
		m_overTimer.stop = true;
	}

    override protected void CreateCustomGUI()
    {
        base.CreateCustomGUI();
        if( m_config.type == GameMatch.Type.ePVP_3On3)
        {
            m_uiMatch.enableBack = false;
        }
    }

	protected override void _CreatePlayersData ()
	{
	}

	static public void HandleTipOffBegin(Pack pack)
	{
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		MatchStateTipOff_PVP tipOff = curMatch.m_stateMachine.m_curState as MatchStateTipOff_PVP;
		if( tipOff == null )
			return;
		tipOff.BeginTipOff();
	}

	static public void HandleGameOver(Pack pack)
	{
		GameOver resp = Serializer.Deserialize<GameOver>(new MemoryStream(pack.buffer));
		GameResult result = resp.result;

		Logger.Log("pvp game over");
		GameMatch_PVP curMatch = GameSystem.Instance.mClient.mCurMatch as GameMatch_PVP;
		if( result == GameResult.GR_DRAW )
			curMatch.m_stateMachine.SetState(MatchState.State.eOverTime);
	}

	protected override void OnRimCollision (UBasket basket, UBasketball ball)
	{
        m_count24Time = gameMatchTime;
        m_count24TimeStop = false;
		m_b24TimeUp = false;
	}

	static public void HandleGameFaul(Pack pack)
	{
		GameFaul resp = Serializer.Deserialize<GameFaul>(new MemoryStream(pack.buffer));
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		Logger.Log("pvp game faul");
		curMatch.m_stateMachine.SetState(MatchState.State.eFoul);
	}

	public override void HandleGameBegin(Pack pack)
	{
		Logger.Log("pvp game begin");
        foreach( Player player in m_PlayersToControl)
        {
            player.m_aiMgr.m_enable = false;
            player.m_aiMgr = null;

			if( m_config.type == Type.ePVP_3On3 )
				player.m_bSimulator = (player != m_mainRole);
			else
				player.m_bSimulator = (player.m_team != m_mainTeam);

            player.DropBall(mCurScene.mBall);

            Logger.Log("1927 - reset player's AI player player.m_roleInfo.id=" + 
                player.m_roleInfo.id
                + ", index=" + player.m_roleInfo.index);
        }
        m_PlayersToControl.Clear();

        if( MainPlayer.Instance.inPvpJoining )
        {
            Logger.Log("1927 - To Set inPvapJoint to false");
            MainPlayer.Instance.inPvpJoining = false;
        }

		GameBeginResp resp = Serializer.Deserialize<GameBeginResp>(new MemoryStream(pack.buffer));
		if( resp.on_ball == TeamType.TT_HOME )
			Logger.Log("home team");
		else
			Logger.Log("away team");

		if( resp.tip_off == 0 || MainPlayer.Instance.inPvpJoining)
			_GameBegin(resp);
		else
			_TipOff(resp);
	}

	void _TipOff(GameBeginResp resp)
	{
		if( m_uiMatch == null )
			CreateUI();

		//reposition player
		System.Action<bool> set_position = (isHome) =>
		{
            BeginPos beginPos = GameSystem.Instance.MatchPointsConfig.BeginPos;
			foreach (FightRole fightRole in (isHome ? resp.home_position : resp.away_position).fighters)
			{
				Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByRoomId(fightRole.role_id);
				bool isOffense = (resp.on_ball == TeamType.TT_HOME) == isHome;
  
                List<IM.Transform> posList = isOffense ? beginPos.offenses_transform : beginPos.defenses_transform;

                //TODO: 测试：有时候出现错误，打印输出。
                Logger.Log("1927 fightRole.status - 1=" + (int)(fightRole.status - 1));
                Logger.Log("1927 isHome=" + isHome + ", posList.Count=" + posList.Count);
                for (int i = 0; i < posList.Count; i++ )
                {
                    Logger.Log("1927 posList[" + i + "]=" + posList[i]);
                }
                int pos = (int)fightRole.status - 1;
                Logger.Log("1927 pos =" + pos + ", player=" + player + ",fightRole.role_id=" + fightRole.role_id);

                //player.position = posList[(int)fightRole.status - 1].position;
                player.position = posList[pos].position;
				player.m_startPos = fightRole.status;
				player.m_team.m_role = isOffense ? MatchRole.eOffense : MatchRole.eDefense;
				Logger.Log("Game begin set position, " + player.m_team.m_side + " " + player.m_name 
				           + " is offense: " + isOffense 
				           + " " + fightRole.status + " " + player.position);
				if (m_mainRole.m_team != player.m_team)
					player.model.EnableGrey();
			}
		};
		set_position(true);
		set_position(false);

		m_homeTeam.SortMember(true);
		m_awayTeam.SortMember(true);

		AssumeDefenseTarget();
		
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( player.m_defenseTarget != null )
				player.FaceTo(player.m_defenseTarget.position);
		}

        if (!m_bOverTime)
            gameMatchTime = new IM.Number((int)resp.total_time);
        else
            m_gameMathCountEnable = false;
		
		m_homeScore	= (int)resp.home_score;
		Logger.Log("home score: " + resp.home_score);
		
		m_awayScore	= (int)resp.away_score;
		Logger.Log("away score: " + resp.away_score);
		
		_UpdateCamera(m_mainRole);

		m_stateMachine.SetState(MatchState.State.eTipOff);
	}

	void _GameBegin(GameBeginResp resp)
	{
		//reposition player
		System.Action<bool> set_position = (isHome) =>
		{
            BeginPos beginPos = GameSystem.Instance.MatchPointsConfig.BeginPos;
			foreach (FightRole fightRole in (isHome ? resp.home_position : resp.away_position).fighters)
			{
				Player player = GameSystem.Instance.mClient.mPlayerManager.GetPlayerByRoomId(fightRole.role_id);
				bool isOffense = (resp.on_ball == TeamType.TT_HOME) == isHome;
                List<IM.Transform> posList = isOffense ? beginPos.offenses_transform : beginPos.defenses_transform;
				player.position = posList[(int)fightRole.status - 1].position;
				player.m_startPos = fightRole.status;
				player.m_team.m_role = isOffense ? MatchRole.eOffense : MatchRole.eDefense;
				Logger.Log("Game begin set position, " + player.m_team.m_side + " " + player.m_name 
					+ " is offense: " + isOffense 
					+ " " + fightRole.status + " " + player.position);
				if (m_mainRole.m_team != player.m_team)
					player.model.EnableGrey();
			}
		};
		set_position(true);
		set_position(false);

		AssumeDefenseTarget();
		
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( player.m_defenseTarget != null )
				player.FaceTo(player.m_defenseTarget.position);
		}

        if( m_uiMatch == null )
        {
            CreateUI();
        }

        uint challengeTime = GameSystem.Instance.CommonConfig.GetUInt("gChallengeGameTime");

        if( resp.total_time > challengeTime )
        {
            m_bOverTime = true;
        }

        if (!m_bOverTime)
        {
            gameMatchTime = new IM.Number((int)resp.total_time);
            m_gameMathCountEnable = true;
        }
        else
        {
            m_gameMathCountEnable = false;
        }
         
		
		m_homeScore	= (int)resp.home_score;
		Logger.Log("home score: " + resp.home_score);
		
		m_awayScore	= (int)resp.away_score;
		Logger.Log("away score: " + resp.away_score);
		
		_UpdateCamera(m_mainRole);
		m_stateMachine.SetState(MatchState.State.eBegin);
	}

	public override void AssumeDefenseTarget ()
	{
		int count = m_homeTeam.GetMemberCount();
		for (int idx = 0; idx != count; idx++)
		{
			if (idx >= m_awayTeam.GetMemberCount())
				break;

			Player player = m_homeTeam.GetMember(idx);
			Player defender = m_awayTeam.members.Find( (Player member)=>{ return member.m_startPos == player.m_startPos;} );
			if( defender == null )
				continue;
			player.m_defenseTarget = defender;
			player.m_defTargetSwitched = false;
			defender.m_defenseTarget = player;
			defender.m_defTargetSwitched = false;
			
			if( defender.m_AOD == null )
				defender.m_AOD = new AOD(defender);
			
			if( player.m_AOD == null )
				player.m_AOD = new AOD(player);
		}

		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
			Logger.Log("Player: " + player.m_id + " , defense target: " + player.m_defenseTarget.m_id );
	}

	public void OnNotifyRoomUserExit(NotifyRoomUser resp)
	{
	}

	public void SetPlayerPos(TeamType type, FightRole fightRole)
	{
		StartPositionInfo posInfo = new StartPositionInfo();
		posInfo.teamType = type;
		posInfo.fightRole = fightRole;
		m_posInfo.Add(posInfo);

		Logger.Log("Add player position: " + fightRole.role_id + " team type: " + type);
	}

	protected override Player _GenerateTeamMember (Config.TeamMember member, string name)
	{
		Team team = (member.team == Team.Side.eAway ? m_awayTeam : m_homeTeam);
		Player player = GameSystem.Instance.mClient.mPlayerManager.CreatePlayer(member.roleInfo, team);
		player.m_config = member;

        Logger.Log("1927 - GameMatch_PVP GenerateTeamMember player.m_roleInfo.index = " + player.m_roleInfo.index);

		return player;
	}

	public override void ResetPlayerPos ()
	{
	}

	public override void InitBallHolder ()
	{
	}

	public void OnInitPlayer()
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
			if (player.m_InfoVisualizer != null)
				player.m_InfoVisualizer.ShowStaminaBar(false);


			if( m_config.type != Type.ePVP_3On3 )
			{
				if( player.m_team == m_mainTeam )
				{
					player.m_InfoVisualizer.CreateStrengthBar();
					player.m_aiMgr = new AISystem_Basic(this, player, AIState.Type.eInit);
					player.m_aiMgr.m_enable = true;
				}
			}

			if( m_config.type == Type.ePVP_3On3 )
				player.m_bSimulator = (player != m_mainRole);
			else
				player.m_bSimulator = (player.m_team != m_mainTeam);

		}

		if( m_config.type == Type.ePVP_3On3 )
			m_mainRole.m_InfoVisualizer.CreateStrengthBar();

		m_mainRole.m_inputDispatcher = new InputDispatcher(this, m_mainRole);
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(true);
		if( m_mainRole.m_aiMgr != null )
			m_mainRole.m_aiMgr.m_enable = false;
	}

	public override void Update(IM.Number deltaTime)
	{
		base.Update(deltaTime);

		m_overTimer.Update(deltaTime);

		if( m_bSceneBuild )
		{
			if( (m_config.type == Type.ePVP_1PLUS && m_playerToBuild.Count == 2)
				|| (m_config.type == Type.ePVP_3On3 && m_playerToBuild.Count == 6)
			)
			{
				foreach( PlayerData playerData in m_playerToBuild )
					_CreateRoomUser(playerData);
				m_playerToBuild.Clear();
				m_bPlayerBuildDone = true;
			}
		}

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
	}
	
	override public void SwitchMainrole( Player target )
	{

		if( target == null )
			return;

		if( m_stateMachine.m_curState.m_eState == MatchState.State.eOver )
			return;

		if( m_mainRole == target || m_mainTeam != target.m_team )
			return;

		target.m_inputDispatcher = new InputDispatcher(this, target);
		target.m_inputDispatcher.TransmitUncontrolInfo(m_mainRole.m_inputDispatcher);
		m_mainRole.m_inputDispatcher = null;
		
		if( m_mainRole.m_aiMgr != null )
			m_mainRole.m_aiMgr.m_enable = Debugger.Instance.m_bEnableAI;
		if (target.m_team.m_role == MatchRole.eDefense)
			target.m_inputDispatcher.disableAIOnAction = true;
		else
			if (target.m_aiMgr != null)
				target.m_aiMgr.m_enable = false;
		
		m_mainRole.m_InfoVisualizer.ShowStaminaBar(false);

		target.m_InfoVisualizer.ShowStaminaBar(true);
		
		m_mainRole = target;
		_UpdateCamera(m_mainRole);
		InputReader.Instance.player = m_mainRole;
		
		//Logger.Log("current main role: " + m_mainRole.m_id);
	}

	public void AddPlayerData(PlayerData playerData)
	{
		PlayerManager playerMgr = GameSystem.Instance.mClient.mPlayerManager;
		if( m_playerToBuild.Find( (PlayerData inPlayerData)=>{ return playerData.acc_id == inPlayerData.acc_id; } ) != null )
			return;
		m_playerToBuild.Add(playerData);
	}

	protected override MatchStateMachine CreateMatchStateMachine()
	{
		return new MatchStateMachine_PVP(this);
	}

	public override bool IsCommandValid(Command command)
	{
		if( m_config.type == Type.ePVP_3On3 && command == Command.Switch )
			return false;

		return base.IsCommandValid(command);
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

	private void OnGameServerConn(NetworkConn.Type type)
	{
		if (type == NetworkConn.Type.eGameServer)
		{
			Logger.Log("OnGameServerConn");

			EnterGameSrv req = new EnterGameSrv();
			req.acc_id = MainPlayer.Instance.AccountID;
			req.session = m_config.session_id;
			if (GameSystem.Instance.mNetworkManager.m_gameConn == null)
				return;
			NetworkConn gameConn = GameSystem.Instance.mNetworkManager.m_gameConn;
			gameConn.SendPack(0, req, MsgID.EnterGameSrvID);
		}
	}

	public static MatchType ToMatchType(LeagueType leagueType, Type type)
	{
		MatchType mt = MatchType.MT_PVP_1V1_PLUS;
		if (leagueType == LeagueType.eRegular1V1)
			mt = MatchType.MT_REGULAR_RACE;
		else if (leagueType == LeagueType.eQualifyingNew)
			mt = MatchType.MT_QUALIFYING_NEW;
		else if (type == GameMatch.Type.ePVP_1PLUS)
			mt = MatchType.MT_PVP_1V1_PLUS;
		else if (type == GameMatch.Type.ePVP_3On3)
			mt = MatchType.MT_PVP_3V3;
        else if (leagueType == LeagueType.ePractise1vs1)
            mt = MatchType.MT_PRACTICE_1V1;
        else
			mt = MatchType.MT_PVP_1V1;
		return mt;
	}

	public static fogs.proto.msg.GameMode ToGameMode(LeagueType leagueType)
	{
		switch (leagueType)
		{
			case LeagueType.eQualifyingNew:
				return fogs.proto.msg.GameMode.GM_QUALIFYING;
			case LeagueType.eRegular1V1:
				return fogs.proto.msg.GameMode.GM_PVP_REGULAR;
			default:
				return fogs.proto.msg.GameMode.GM_PVP;
		}
	}

	private void OnEnterGameSrv(Pack pack)
	{
		Logger.Log( "enter game srv resp" );
		EnterGameSrv resp = Serializer.Deserialize<EnterGameSrv>(new MemoryStream(pack.buffer));
		if( resp == null )
		{
			Logger.LogError("no EnterGameSrv resp");
			return;
		}

		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.EnterGameSrvRespID, 	OnEnterGameSrv);

		EnterGameReq req = new EnterGameReq();
		req.acc_id = MainPlayer.Instance.AccountID;
		req.type = ToMatchType(leagueType, m_config.type);
		req.game_mode = ToGameMode(leagueType);
        GameSystem.Instance.mNetworkManager.m_gameConn.SendPack(0, req, MsgID.EnterGameReqID);
	}
			
	private void OnEnterGame(Pack pack)
	{
		Logger.Log("on enter game");
		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.EnterGameRespID, OnEnterGame);

		EnterGameResp resp = Serializer.Deserialize<EnterGameResp>(new MemoryStream(pack.buffer));
		if( resp == null )
		{
			Logger.LogError("EnterGameResp error");
			return;
		}
		foreach( PlayerData playerData in resp.challenge.rival_data )
        {
			AddPlayerData(playerData);

            // TODO: make log for debug
            //Logger.Log("make log for debug--------------");
            //foreach( RoleInfo roleInfo in playerData.roles)
            //{
            //    Logger.Log("1927 roleInfo.index=" + roleInfo.index);
            //}
        }

		//if( onPlayerConnected != null )
		//	onPlayerConnected(m_playerToBuild);
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		GameSystem.Instance.mNetworkManager.onServerConnected -= OnGameServerConn;
	}
}