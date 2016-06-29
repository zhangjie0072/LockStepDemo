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
    public List<Player> m_PlayersToControl = new List<Player>();
    public List<uint> droppedAccount = new List<uint>();    //掉线玩家账号
	public GameUtils.Timer m_overTimer{get; private set;}

	public bool m_bPlayerDataReady = false;

	private List<PlayerData>	m_playerToBuild = new List<PlayerData>();

	public GameMatch_PVP(Config config)
		:base(config)
	{
		GameSystem.Instance.mNetworkManager.onServerConnected += OnGameServerConn;

		GameSystem.Instance.mNetworkManager.ConnectToGS(config.type, config.ip, config.port);

		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.EnterGameSrvRespID, 	OnEnterGameSrv);    //进入游戏服务器
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.EnterGameRespID, 	OnEnterGame);   //加入玩家
		mgr.m_gameMsgHandler.RegisterHandler(MsgID.InstructionBroadcastID, 	HandleBroadcast);

		mgr.m_gameConn.m_profiler.BeginRecordDataUsage();

		m_overTimer = new GameUtils.Timer(new IM.Number(3), _HandleGameOver, 1);
		m_overTimer.stop = true;
	}

	void _HandleGameOver()
	{
		m_stateMachine.SetState(MatchState.State.eOver); 
		m_overTimer.stop = true;
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

	protected override void OnRimCollision (UBasket basket, UBasketball ball)
	{
        m_count24Time = gameMatchTime;
        m_count24TimeStop = false;
		m_b24TimeUp = false;
	}

    //帧同步以后，这个消息只在开局发一次，中途不再发
	public override void OnGameBegin(GameBeginResp resp)
	{
		Debug.Log("pvp game begin");
        //重连后拿回控制权
        foreach( Player player in m_PlayersToControl)
        {
            player.operMode = Player.OperMode.Input;

            player.DropBall(mCurScene.mBall);

            Debug.Log("1927 - reset player's AI player player.m_roleInfo.id=" + 
                player.m_roleInfo.id
                + ", index=" + player.m_roleInfo.index);
        }
        m_PlayersToControl.Clear();

        if( MainPlayer.Instance.inPvpJoining )
        {
            Debug.Log("1927 - To Set inPvapJoint to false");
            MainPlayer.Instance.inPvpJoining = false;
        }

		if( resp.on_ball == TeamType.TT_HOME )
			Debug.Log("home team");
		else
			Debug.Log("away team");

		if( resp.tip_off == 0 || MainPlayer.Instance.inPvpJoining)
			_GameBegin(resp);
		else
			_TipOff(resp);
	}

	void _TipOff(GameBeginResp resp)
	{
		if( m_uiMatch == null )
			CreateUI();

        /*
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
        */

        /*
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager )
		{
			if( player.m_defenseTarget != null )
				player.FaceTo(player.m_defenseTarget.position);
		}
        */

        if (!m_bOverTime)
            gameMatchTime = new IM.Number((int)resp.total_time);
        else
            m_gameMathCountEnable = false;
		
		m_homeScore	= (int)resp.home_score;
		Debug.Log("home score: " + resp.home_score);
		
		m_awayScore	= (int)resp.away_score;
		Debug.Log("away score: " + resp.away_score);
		
		_UpdateCamera(mainRole);

		m_stateMachine.SetState(MatchState.State.eTipOff);
	}

	void _GameBegin(GameBeginResp resp)
	{
        /*
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
        */

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
		Debug.Log("home score: " + resp.home_score);
		
		m_awayScore	= (int)resp.away_score;
		Debug.Log("away score: " + resp.away_score);
		
		_UpdateCamera(mainRole);
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
			//Player defender = m_awayTeam.members.Find( (Player member)=>{ return member.m_startPos == player.m_startPos;} );
            Player defender = m_awayTeam.GetMember(idx);
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
			Debug.Log("Player: " + player.m_id + " , defense target: " + player.m_defenseTarget.m_id );
	}

	protected override Player _GenerateTeamMember (Config.TeamMember member, string name)
	{
		Team team = (member.team == Team.Side.eAway ? m_awayTeam : m_homeTeam);
		Player player = GameSystem.Instance.mClient.mPlayerManager.CreatePlayer(member.roleInfo, team);
		player.m_config = member;

        Debug.Log("1927 - GameMatch_PVP GenerateTeamMember player.m_roleInfo.index = " + player.m_roleInfo.index);

		return player;
	}

	public void OnInitPlayer()
	{
		foreach( Player player in GameSystem.Instance.mClient.mPlayerManager)
		{
            if (GetMatchType() == Type.ePVP_1PLUS)  //所有人有AI
            {
                if (IsMainRole(player))
                    player.operMode = Player.OperMode.Input;
                else
                    player.operMode = Player.OperMode.AI;
            }
            else if (GetMatchType() == Type.ePVP_3On3)
            {
                player.operMode = Player.OperMode.Input;
            }
		}
	}

	public void LoadPlayers()
	{
		if( (m_config.type == Type.ePVP_1PLUS && m_playerToBuild.Count == 2)
			|| (m_config.type == Type.ePVP_3On3 && m_playerToBuild.Count == 6)
		)
		{
			foreach( PlayerData playerData in m_playerToBuild )
				_CreateRoomUser(playerData);

            m_homeTeam.SortMember();
            m_awayTeam.SortMember();

            AssumeDefenseTarget();

            if (m_config.type == Type.ePVP_1PLUS)   //设置每队第一个为mainrole
            {
                Player p = m_homeTeam.members[0];
                SwitchMainrole(p);
                p = m_awayTeam.members[0];
                SwitchMainrole(p);
            }
            else if (m_config.type == Type.e3On3)   //每个人都是mainrole
            {
                foreach (Player player in m_homeTeam)
                    SwitchMainrole(player);
                foreach (Player player in m_awayTeam)
                    SwitchMainrole(player);
            }

			m_playerToBuild.Clear();
		}
	}
	public void GetUIList(out List<string> lst_uiNames)
	{
		lst_uiNames = new List<string>();
		lst_uiNames.Add("Prefab/GUI/MatchTipAnim");
		lst_uiNames.Add("Prefab/GUI/MatchTipScoreDiff");
		lst_uiNames.Add("Prefab/GUI/PlayerTip");
		lst_uiNames.Add("Prefab/GUI/circle");
		lst_uiNames.Add("Prefab/GUI/GroundDown");
		lst_uiNames.Add("Prefab/GUI/Hit_1");
		lst_uiNames.Add("Prefab/GUI/pre_3pt");
		lst_uiNames.Add("Prefab/GUI/RebPlacement");
	}

	protected override void _OnSceneComplete ()
	{
	}

	public override void GameUpdate(IM.Number deltaTime)
	{
		base.GameUpdate(deltaTime);

		m_overTimer.Update(deltaTime);
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
		return false;
	}

	private void OnGameServerConn(NetworkConn.Type type)
	{
		if (type == NetworkConn.Type.eGameServer)
		{
			Debug.Log("OnGameServerConn");

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
		Debug.Log( "enter game srv resp" );
		EnterGameSrv resp = Serializer.Deserialize<EnterGameSrv>(new MemoryStream(pack.buffer));
		if( resp == null )
		{
			Debug.LogError("no EnterGameSrv resp");
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
		Debug.Log("on enter game");
		NetworkManager mgr = GameSystem.Instance.mNetworkManager;
		mgr.m_gameMsgHandler.UnregisterHandler(MsgID.EnterGameRespID, OnEnterGame);

		EnterGameResp resp = Serializer.Deserialize<EnterGameResp>(new MemoryStream(pack.buffer));
		if( resp == null )
		{
			Debug.LogError("EnterGameResp error");
			return;
		}
		foreach( PlayerData playerData in resp.challenge.rival_data )
			AddPlayerData(playerData);
		m_bPlayerDataReady = true;
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		GameSystem.Instance.mNetworkManager.onServerConnected -= OnGameServerConn;
	}

    public override void ProcessTurn(FrameInfo turn, IM.Number deltaTime)
    {
        base.ProcessTurn(turn, deltaTime);

        foreach (ClientStateChanged state in turn.client_state_list)
        {
            if (state.state == 1)  //掉线
                droppedAccount.Add(state.acc_id);
        }
    }

    public override void SwitchMainrole(Player target)
    {
        Player prevMainRole = GetMainRole(target.m_roleInfo.acc_id);
        base.SwitchMainrole(target);
        Player curMainRole = GetMainRole(target.m_roleInfo.acc_id);

        //转移掉线托管状态
        if (prevMainRole != null && prevMainRole.m_takingOver && prevMainRole != curMainRole)
        {
            prevMainRole.m_takingOver = false;
            prevMainRole.operMode = Player.OperMode.AI;
            prevMainRole.m_aiMgr.IsPvp = false;

            curMainRole.m_takingOver = true;
            curMainRole.operMode = Player.OperMode.AI;
            curMainRole.m_aiMgr.IsPvp = true;
        }
    }
}