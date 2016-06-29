using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using ProtoBuf;
using fogs.proto.msg;
using LuaInterface;

public class Client
{
    public enum State
    {
        none,
        updateAssets,
        login,
        gaming,
        logout,
        exit
    };
    private State mCurClientState;
    public State curClientState { get { return mCurClientState; } set { _OnClientStateChange(value); } }

    public GameMatch mCurMatch { get; private set; }
    public InputManager mInputManager { get; private set; }
    public UIManager mUIManager { get; private set; }
    public PlayerManager mPlayerManager { get; private set; }

    private bool _pause = false;
    public bool pause { 
        get { return _pause; }
        set { 
            _pause = value;
            //*
            if (VirtualGameServer.Instance != null)
            {
                if (pause)
                    VirtualGameServer.Instance.Stop();
                else
                    VirtualGameServer.Instance.Resume();
            }
            //*/
        }
    }
	public float timeScale = 1f;

    public bool bStartGuide = false;
	
    public Client()
    {
        mCurClientState = State.none;

        DateTime time = System.DateTime.Now;
        mInputManager = new InputManager();
        Debug.Log("【Time】Client>>>new InputManager=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        time = System.DateTime.Now;
        mUIManager = new UIManager();
        Debug.Log("【Time】Client>>>new UIManager=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        time = System.DateTime.Now;
        mPlayerManager = new PlayerManager();
        Debug.Log("【Time】Client>>>new PlayerManager=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        //m_rooms = new Dictionary<uint,Room>();
    }

    public void Reset()
    {
		Debug.Log("Client.Reset");
        mCurClientState = State.none;

        if (mCurMatch != null)
        {
            mCurMatch.OnDestroy();
            mCurMatch = null;
        }

        mPlayerManager.RemoveAllPlayers();
        mUIManager.RemoveAll();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            pause = !pause;

        if (pause || Mathf.Approximately(timeScale, 0f))
        {
			// Keep heart beat.
			if (GameSystem.Instance.mNetworkManager != null)
				GameSystem.Instance.mNetworkManager.FixedUpdate(Time.unscaledDeltaTime);
            Time.timeScale = 0.0f;
            return;
        }
        else
            Time.timeScale = timeScale;

        if (mUIManager != null)
            mUIManager.OnUpdate(Time.deltaTime);

		if (mCurMatch != null && mCurMatch.m_bLoadingComplete)
        {
            TurnController.Instance.Update(Time.deltaTime);
            mCurMatch.ViewUpdate();
        }

		PlaySoundManager.Instance.Update(Time.deltaTime);
    }

	public void FixedUpdate()
	{
		if (mCurMatch == null || !mCurMatch.m_bLoadingComplete )
			return;

		mCurMatch.FixedUpdate();
	}

    public void LateUpdate()
    {
        if (mCurMatch != null && mCurMatch.m_bLoadingComplete)
			mCurMatch.ViewLateUpdate();

        if (bStartGuide)
            GuideSystem.Instance.Update();
    }

    public bool CreateNewMatch(GameMatch.Type gameType)
    {
        string configName;
        switch (gameType)
        {
            case GameMatch.Type.e1On1:
                configName = GlobalConst.DIR_XML_MATCH_SINGLE;
                break;
			case GameMatch.Type.ePVP_3On3:
			case GameMatch.Type.ePVP_1PLUS:
				configName = GlobalConst.DIR_XML_MATCH_PVP;
			break;
            case GameMatch.Type.eReady:
                configName = GlobalConst.DIR_XML_MATCH_READY;
                break;
            case GameMatch.Type.eFreePractice:
                configName = GlobalConst.DIR_XML_MATCH_FREE_PRACTICE;
                break;
            case GameMatch.Type.e3On3:
                configName = GlobalConst.DIR_XML_MATCH_MULTIPLY;
                break;
            case GameMatch.Type.eGuide:
                configName = GlobalConst.DIR_XML_MATCH_GUIDE;
                break;
            default:
                configName = GlobalConst.DIR_XML_MATCH_SINGLE;
                break;
        }

        return CreateNewMatch(configName, 0ul, gameType);
    }

    public bool CreateNewMatch(string configName, ulong session_id = 0ul, GameMatch.Type matchType = GameMatch.Type.eNone)
    {
        if (mPlayerManager != null)
            mPlayerManager.RemoveAllPlayers();

        if (mCurMatch != null)
            mCurMatch = null;

        GameMatch.Config config = new GameMatch.Config();
        GameSystem.Instance.gameMatchConfig.LoadMatchConfig(ref config, GameMatch.Type.eNone);
        GameSystem.Instance.gameMatchConfig.LoadMatchConfig(ref config, matchType);
		config.session_id = session_id;
		if( config.MainRole == null )
		{
			config.MainRole = new GameMatch.Config.TeamMember();
			config.MainRole.id = (MainPlayer.Instance.CaptainID).ToString();
		}

        GameMatch match = null;
        switch (config.type)
        {
            case GameMatch.Type.e1On1:
                match = new GameMatch_1ON1(config);
                break;

            case GameMatch.Type.eReady:
				object o = LuaScriptMgr.Instance.GetLuaTable("_G")["TestScene"];
				if (o != null)
					config.sceneId = (uint)(double)o;
                match = new GameMatch_Ready(config);
                break;
            case GameMatch.Type.eFreePractice:
                match = new GameMatch_FreePractice(config);
                break;

            case GameMatch.Type.e3On3:
                match = new GameMatch_3ON3(config);
                break;

			case GameMatch.Type.eGuide:
				GameMatch_Guide.SetConfig(ref config);
				match = new GameMatch_Guide(config);
				break;

			case GameMatch.Type.ePVP_1PLUS:
			case GameMatch.Type.ePVP_3On3:
				//match = new GameMatch_PVP(config);
				match = new GameMatch_PVP(config);
				break;
        }
        if (match == null)
        {
            Debug.LogError("Unsupported match type is detected when creating a new match.");
            return false;
        }

        mCurMatch = match;
        mCurMatch.Build();

        return true;
    }

    public bool CreateNewMatch(GameMatch.Config config)
    {
        if (mPlayerManager != null)
            mPlayerManager.RemoveAllPlayers();

        if (mCurMatch != null)
            mCurMatch = null;

		//涂鸦数据
		GameMatch.Config.TeamMember mainRole = config.MainRole;
		if (mainRole != null)
		{
			RoleInfo role = MainPlayer.Instance.GetRole2 (uint.Parse (mainRole.id));
			if (role != null && role.badge_book_id != 0) {
				BadgeBook book = MainPlayer.Instance.badgeSystemInfo.GetBadgeBookByBookId (role.badge_book_id);
				mainRole.badgeBook = book;
			}
			for (int i = 0; i < config.NPCs.Count; ++i) {
				GameMatch.Config.TeamMember teamMate = config.NPCs [i];
				if (teamMate != null)
				{
					RoleInfo roleTeammate = MainPlayer.Instance.GetRole2 (uint.Parse (teamMate.id));
					if (roleTeammate != null && roleTeammate.badge_book_id != 0) {
						BadgeBook book = MainPlayer.Instance.badgeSystemInfo.GetBadgeBookByBookId (roleTeammate.badge_book_id);
						teamMate.badgeBook = book;
					}
				}
			}
		}

        GameMatch match = null;
        switch (config.type)
        {
			case GameMatch.Type.ePVP_1PLUS:
			case GameMatch.Type.ePVP_3On3:
				match = new GameMatch_PVP(config);
				break;
            case GameMatch.Type.e1On1:
            case GameMatch.Type.eCareer1On1:
                match = new GameMatch_1ON1(config);
                break;
            case GameMatch.Type.ePractise:
                match = new GameMatch_Practise(config);
                break;
            case GameMatch.Type.eReady:
                match = new GameMatch_Ready(config);
                break;
            case GameMatch.Type.eFreePractice:
                match = new GameMatch_FreePractice(config);
                break;
            case GameMatch.Type.ePracticeVs:
                match = new GameMatch_PracticeVs(config);
                break;
            case GameMatch.Type.e3On3:
            case GameMatch.Type.eCareer3On3:
                match = new GameMatch_3ON3(config);
                break;
            case GameMatch.Type.eAsynPVP3On3:
				match = new GameMatch_AsynPVP3ON3(config);
				break;
			case GameMatch.Type.e3AIOn3AI:
				match = new GameMatch_3AION3AI(config);
				break;
			case GameMatch.Type.eReboundStorm:
				match = new GameMatch_ReboundStorm(config);
				break;
			case GameMatch.Type.eBlockStorm:
				match = new GameMatch_BlockStorm(config);
				break;
			case GameMatch.Type.eUltimate21:
				match = new GameMatch_Ultimate21(config);
				break;
			case GameMatch.Type.eMassBall:
				match = new GameMatch_MassBall(config);
				break;
			case GameMatch.Type.eGrabZone:
				match = new GameMatch_GrabZone(config);
				break;
			case GameMatch.Type.eGrabPoint:
				match = new GameMatch_GrabPoint(config);
				break;
			case GameMatch.Type.eBullFight:
				match = new GameMatch_BullFight(config);
				break;
            case GameMatch.Type.ePractice1V1:
                match = new GameMatch_Practice1V1(config);
                break;
            case GameMatch.Type.eQualifyingNewerAI:
                match = new GameMatch_QualifyingNewerAI(config);
                break;

            case GameMatch.Type.eLadderAI:
                match = new GameMatch_LadderAI(config);
                break;

        }
        if (match == null)
        {
            Debug.LogError("Unsupported match type is detected when creating a new match.");
            return false;
        }

        mCurMatch = match;
        mCurMatch.Build();

        return true;
    }



    public void CreateMatch(PractiseData practise, ulong session_id)
    {
		if (practise.ID == 10001)
		{
            GameSystem.Instance.mClient.CreateNewMatch(GlobalConst.DIR_XML_MATCH_GUIDE, session_id, GameMatch.Type.eGuide);
			return;
		}
        GameMatch.Type type = GameMatch.Type.ePractise;
        GameMatch.Config config = new GameMatch.Config();
        config.leagueType = GameMatch.LeagueType.ePractise;
        config.type = type;
        config.needPlayPlot = false;
        config.MatchTime = IM.Number.zero;
        config.sceneId = uint.Parse(practise.scene);
        config.extra_info = practise.ID;
        config.session_id = session_id;

       
        if (GameSystem.Instance.PractiseConfig.GetConfig(practise.ID).is_activity == 1)
        {
            GameMatch.Config.TeamMember mem = new GameMatch.Config.TeamMember();
            List<uint> list = MainPlayer.Instance.GetRoleIDList();
            int max = list.Count;
            int index = UnityEngine.Random.Range(0, max - 1);
            mem.id = list[index].ToString();
            mem.team = Team.Side.eHome;
            mem.pos = 1;
            config.MainRole = mem;
        }
        else {
            if (practise.self_id != 0)
            {
                GameMatch.Config.TeamMember mem = new GameMatch.Config.TeamMember();
                mem.id = practise.self_id.ToString();
                mem.team = Team.Side.eHome;
                mem.pos = 1;
                config.MainRole = mem;
            }            
        }

        if (practise.npc_id != 0)
        {
            GameMatch.Config.TeamMember npc = new GameMatch.Config.TeamMember();
            npc.id = practise.npc_id.ToString();
            npc.team = Team.Side.eAway;
            npc.pos = 2;
            config.NPCs.Add(npc);
        }
        GameSystem.Instance.mClient.CreateNewMatch(config);
    }

    public void CreateNewMatch(uint gameModeID, ulong sessionID, bool needPlayPlot, GameMatch.LeagueType leagueType, List<uint> teammates = null, List<uint> npcList = null)
	{
		GameMode gameMode = GameSystem.Instance.GameModeConfig.GetGameMode(gameModeID);

        if (teammates.Count < 1
            && (gameMode.matchType == GameMatch.Type.e3On3 || gameMode.matchType == GameMatch.Type.eAsynPVP3On3 || gameMode.matchType == GameMatch.Type.ePractice1V1))
            return;

		GameMatch.Config config = new GameMatch.Config();
		config.leagueType = leagueType;
		config.type = gameMode.matchType;
		//config.type = GameMatch.Type.e3AIOn3AI;
		config.sceneId = uint.Parse(gameMode.scene);
		config.MatchTime = new IM.Number((int)gameMode.time);
		config.session_id = sessionID;

		config.gameModeID = gameModeID;
		config.needPlayPlot = needPlayPlot;
		config.level = gameMode.level;

		int pos = 1;
		config.MainRole = new GameMatch.Config.TeamMember();
        config.MainRole.id = teammates[0].ToString();
		config.MainRole.pos = pos++;
		config.MainRole.team = Team.Side.eHome;
		if (teammates != null)
		{
			foreach (uint id in teammates)
			{
                if (id.ToString() != config.MainRole.id)
                {
                    GameMatch.Config.TeamMember mem_npc = new GameMatch.Config.TeamMember();
                    mem_npc.id = id.ToString();
                    mem_npc.pos = pos++;
                    mem_npc.team = Team.Side.eHome;
                    config.NPCs.Add(mem_npc);
                }
			}
		}

		List<uint> npcs = new List<uint>();
        if (npcList != null)
        {
            npcs = npcList;
        }
        else
        {
            if (gameMode.mappedNPC.ContainsKey(MainPlayer.Instance.Captain.m_position))
                npcs.Add(gameMode.mappedNPC[MainPlayer.Instance.Captain.m_position]);
            foreach (List<uint> candidates in gameMode.unmappedNPC)
            {
                if (candidates != null && candidates.Count > 0)
                {
                    uint id = 0;
                    do
                    {
                        id = candidates[UnityEngine.Random.Range(0, candidates.Count - 1)];
                    } while (npcs.Contains(id));
                    npcs.Add(id);
                }
            }
        }
        

		foreach (uint ID in npcs)
		{
			GameMatch.Config.TeamMember mem_npc = new GameMatch.Config.TeamMember();
			mem_npc.id = ID.ToString();
			mem_npc.pos = pos++;
			mem_npc.team = Team.Side.eAway;
			config.NPCs.Add(mem_npc);
		}

		CreateNewMatch(config);
	}


    public void CreateBullFightMatch( ulong sessionID, uint roleId, uint npcId)
    {
        GameMatch.Config config = new GameMatch.Config();
        config.leagueType = GameMatch.LeagueType.eBullFight;
        config.type = GameMatch.Type.eBullFight;
        List<uint> sceneIDs = new List<uint>(GameSystem.Instance.SceneConfig.configs.Keys);
        config.sceneId = sceneIDs[UnityEngine.Random.Range(0, sceneIDs.Count)];
        config.MatchTime = new IM.Number(120);
        config.session_id = sessionID;

        var member = new GameMatch.Config.TeamMember();
        member.id = roleId.ToString();
        member.pos = (int)fogs.proto.msg.FightStatus.FS_MAIN;
        member.team = Team.Side.eHome;
        member.team_name = MainPlayer.Instance.Name;
        member.roleInfo = MainPlayer.Instance.GetRole(roleId).m_roleInfo;

        config.MainRole = member;
   
        var enemy = new GameMatch.Config.TeamMember();
        enemy.id = npcId.ToString();
        enemy.pos = 4;
        enemy.team = Team.Side.eAway;

        enemy.roleInfo = null;
        config.NPCs.Add(enemy);
 
        CreateNewMatch(config);
    }



    public void CreateShootMatch(ulong sessionID, fogs.proto.msg.GameMode gameMode, uint roleId, uint npcId, uint gameModeId ,uint mode_type_id )
    {
        GameMatch.Config config = new GameMatch.Config();
        config.gameModeID = gameModeId;
        config.leagueType = GameMatch.LeagueType.eShoot;
        if (gameMode == fogs.proto.msg.GameMode.GM_GrabZone )
        {
            config.type = GameMatch.Type.eGrabZone;
        }
        else if( gameMode == fogs.proto.msg.GameMode.GM_MassBall )
        {
            config.type = GameMatch.Type.eMassBall;
        }
        else if (gameMode == fogs.proto.msg.GameMode.GM_GrabPoint)
        {
            config.type = GameMatch.Type.eGrabPoint;
        }
        
        List<uint> sceneIDs = new List<uint>(GameSystem.Instance.SceneConfig.configs.Keys);
		GameMode confGameMode = GameSystem.Instance.GameModeConfig.GetGameMode(gameModeId);
		
		config.sceneId = uint.Parse(confGameMode.scene);
        config.MatchTime = new IM.Number((int)confGameMode.time);
        config.session_id = sessionID;

        var member = new GameMatch.Config.TeamMember();
        member.id = roleId.ToString();
        member.pos = (int)fogs.proto.msg.FightStatus.FS_MAIN;
        member.team = Team.Side.eHome;
        member.team_name = MainPlayer.Instance.Name;
        member.roleInfo = MainPlayer.Instance.GetRole(roleId).m_roleInfo;

        config.MainRole = member;

        var enemy = new GameMatch.Config.TeamMember();
        enemy.id = npcId.ToString();
        enemy.pos = 4;
        enemy.team = Team.Side.eAway;
        enemy.mode_type_id = mode_type_id;

        enemy.roleInfo = null;
        config.NPCs.Add(enemy);
        CreateNewMatch(config);
    }


	public void CreateNewQualifyingMatch(ulong sessionID, List<FightRole> fightList, RivalInfo rivalInfo)
	{
		GameMatch.Config config = new GameMatch.Config();
		config.leagueType = GameMatch.LeagueType.eQualifying;
		config.type = GameMatch.Type.e3AIOn3AI;
		List<uint> sceneIDs = new List<uint>(GameSystem.Instance.SceneConfig.configs.Keys);
		config.sceneId = sceneIDs[UnityEngine.Random.Range(0, sceneIDs.Count)];
		config.MatchTime = new IM.Number(120);
		config.session_id = sessionID;
        //object obj = LuaScriptMgr.Instance.GetLuaTable("_G")["CurLoadingImage"];
        string str = "Texture/LoadShow";
        object obj = (object)str;
        LuaScriptMgr.Instance.GetLuaTable("_G").Set("CurLoadingImage", obj);

		foreach (FightRole info in fightList)
		{
			var member = new GameMatch.Config.TeamMember();
			member.id = info.role_id.ToString();
			member.pos = (int)info.status;
			member.team = Team.Side.eHome;
			member.team_name = MainPlayer.Instance.Name;
			member.roleInfo = MainPlayer.Instance.GetRole(info.role_id).m_roleInfo;
			if (info.status == FightStatus.FS_MAIN)
			{
				config.MainRole = member;
			}
			else
			{
				config.NPCs.Add(member);
			}
		}

		int pos = 4;
		foreach (RoleInfo info in rivalInfo.role_info)
		{
			var member = new GameMatch.Config.TeamMember();
			member.id = info.id.ToString();
			member.pos = pos++;
			member.team = Team.Side.eAway;
			member.team_name = rivalInfo.name;
			member.roleInfo = info;
            member.isRobot = rivalInfo.player_type == CharacterType.ROBOT ? true : false;
            if (member.isRobot == false)
            {
                member.equipInfo = rivalInfo.equipments;
                member.squadInfo = rivalInfo.squad;
            }
			config.NPCs.Add(member);
		}

		CreateNewMatch(config);
	}

    public void CreateFreePracticeMatch( uint roleId)
    {
        GameMatch.Config config = new GameMatch.Config();
        config.type = GameMatch.Type.eFreePractice;
        config.leagueType = GameMatch.LeagueType.ePractise;
        config.MainRole = new GameMatch.Config.TeamMember();
        config.MainRole.id = roleId.ToString();
        config.MainRole.team = Team.Side.eHome;
        string[] scences = GameSystem.Instance.CommonConfig.GetString("gFreePracticeScene").Split('&');
        int index = UnityEngine.Random.Range(0, scences.Length);
        config.sceneId = uint.Parse(scences[index]);
        CreateNewMatch(config); 
        //GameSystem.Instance.mClient.CreateNewMatch(GameMatch.Type.eFreePractice);
    }

    public void CreatePracticeVsMatch( List<uint> roleIds)
    {
        GameMatch.Config config = new GameMatch.Config();
        config.type = GameMatch.Type.ePracticeVs;
        config.leagueType = GameMatch.LeagueType.ePracticeLocal;
        string[] scences = GameSystem.Instance.CommonConfig.GetString("gPracticeVsScene").Split('&');
        int index = UnityEngine.Random.Range(0, scences.Length);
        config.sceneId = uint.Parse(scences[index]);
        config.MatchTime = new IM.Number(3 * 60);
        //config.MatchTime = 7; 

        int pos = 1;
        config.MainRole = new GameMatch.Config.TeamMember();
        config.MainRole.id = roleIds[0].ToString();
        config.MainRole.pos = pos++;
        config.MainRole.team = Team.Side.eHome;

        foreach(uint id in roleIds)
        {
            if(id.ToString() != config.MainRole.id)
            {
                GameMatch.Config.TeamMember npc = new GameMatch.Config.TeamMember();
                npc.id = id.ToString();
                npc.pos = pos++;
                npc.team = Team.Side.eHome;
                config.NPCs.Add(npc);
            }
        }

        List<uint> rdList = new List<uint>();
        foreach( var kv in BaseDataConfig2.roleBaseDatas)
        {
            if( kv.Value.display == 1)
            {
                rdList.Add(kv.Key);
            }
        }

        List<uint> npcList = new List<uint>();
        for( var i = 0; i < 3; i++)
        {
            int r = UnityEngine.Random.Range(0, rdList.Count);
            npcList.Add(rdList[r]);
            rdList.RemoveAt(r);
        }

        foreach( var id in npcList )
        {
            GameMatch.Config.TeamMember npc = new GameMatch.Config.TeamMember();
            npc.id = id.ToString();
            npc.pos = pos++;
            npc.team = Team.Side.eAway;
            RoleInfo r = new RoleInfo();
            r.id = id;
            r.acc_id = MainPlayer.Instance.AccountID;
            RoleBaseData2 rd = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(id);
            r.star = (uint)rd.init_star;
            r.level = 1;
            r.quality = 1;
            npc.roleInfo = r;
            config.NPCs.Add(npc);

        }
        CreateNewMatch(config);
    }

    /*
	public void CreateNewMatchWithLoading(uint gameModeID, ulong sessionID, GameMatch.LeagueType leagueType, List<uint> teammates = null, List<uint> npcList = null)
    {
		GameMode gameMode = GameSystem.Instance.GameModeConfig.GetGameMode(gameModeID);

		UIAsynPVPLoading loading = UIManager.Instance.CreateUI("Prefab/GUI/UIAsynPVPLoading").GetComponent<UIAsynPVPLoading>();
		loading.my_role_list = new Dictionary<FightStatus, uint>();

        //if (teammates.Count <= 1)
            //npcList = null;
        if (teammates != null) 
        {
            loading.my_role_list.Add(FightStatus.FS_MAIN, teammates[0]);
        }
		if (teammates != null && teammates.Count > 1)
		{
			loading.my_role_list.Add(FightStatus.FS_ASSIST1, teammates[1]);
			loading.my_role_list.Add(FightStatus.FS_ASSIST2, teammates[2]);
		}
		loading.rival_list = new List<MatchInfo>();
		MatchInfo matchInfo = new MatchInfo();
		loading.rival_list.Add(matchInfo);

        Player singlePlayer = MainPlayer.Instance.GetRole(teammates[0]);
        if (npcList == null)
        {
            if (gameMode.mappedNPC.ContainsKey(singlePlayer.m_position))
            {
                MatchInfo.MemberInfo memberInfo = new MatchInfo.MemberInfo();
                memberInfo.id = gameMode.mappedNPC[singlePlayer.m_position];
                matchInfo.members.Add(memberInfo);
            }
            foreach (List<uint> candidates in gameMode.unmappedNPC)
            {
                if (candidates != null && candidates.Count > 0)
                {
                    MatchInfo.MemberInfo memberInfo = new MatchInfo.MemberInfo();

                    do
                    {
                        memberInfo.id = candidates[UnityEngine.Random.Range(0, candidates.Count - 1)];
                    } while (matchInfo.members.Find((MatchInfo.MemberInfo info) => { return info.id == memberInfo.id; }) != null);
                    matchInfo.members.Add(memberInfo);
                }
            }
        }
        else
        {
            
            for (int i = 0; i < npcList.Count; i++)
            {
                MatchInfo.MemberInfo memberInfo = new MatchInfo.MemberInfo();
                memberInfo.id = npcList[i];
                matchInfo.members.Add(memberInfo);
            }
            
        }
		loading.scene_name = gameMode.scene;
		loading.gameModeID = gameMode.ID;
		loading.session_id = sessionID;
		loading.leagueType = leagueType;
		loading.matchType = gameMode.matchType;
		loading.matchTime = gameMode.time;
		loading.Refresh();
		NGUITools.BringForward(loading.gameObject);
    }

    public void CreateNewHonorMatchWithLoading(LuaInterface.LuaTable rivalinfo, LuaInterface.LuaTable member1, LuaInterface.LuaTable member2, LuaInterface.LuaTable member3, ulong sessionID, List<uint> teammates = null)
    {
		UIAsynPVPLoading loading = UIManager.Instance.CreateUI("Prefab/GUI/UIAsynPVPLoading").GetComponent<UIAsynPVPLoading>();
        loading.my_role_list = new Dictionary<FightStatus, uint>();
        loading.my_role_list.Add(FightStatus.FS_MAIN, MainPlayer.Instance.CaptainID);
        if (teammates != null)
        {
            loading.my_role_list.Add(FightStatus.FS_ASSIST1, teammates[0]);
            loading.my_role_list.Add(FightStatus.FS_ASSIST2, teammates[1]);
        }
        //暂时采用这种方法
        LuaInterface.LuaTable info = rivalinfo;//[1] as LuaInterface.LuaTable;
        MatchInfo matchInfo = new MatchInfo();
        matchInfo.acc_id = (uint)(double)(info["acc_id"]);
        matchInfo.team_level = (uint)(double)(info["team_level"]);
        matchInfo.team_name = (string)(info["team_name"]);
        matchInfo.team_icon = (string)(info["team_icon"]);
        matchInfo.winning_streak = (uint)(double)(info["winning_streak"]);
        matchInfo.pos = (uint)(double)(info["pos"]);

        MatchInfo.MemberInfo memberinfo = new MatchInfo.MemberInfo();
        memberinfo.id = (uint)(double)(member1["id"]);
        memberinfo.level = (uint)(double)(member1["level"]);
        memberinfo.quality = (uint)(double)(member1["quality"]);
        matchInfo.members.Add(memberinfo);
        memberinfo = new MatchInfo.MemberInfo();
        memberinfo.id = (uint)(double)(member2["id"]);
        memberinfo.level = (uint)(double)(member2["level"]);
        memberinfo.quality = (uint)(double)(member2["quality"]);
        matchInfo.members.Add(memberinfo);
        memberinfo = new MatchInfo.MemberInfo();
        memberinfo.id = (uint)(double)(member3["id"]);
        memberinfo.level = (uint)(double)(member3["level"]);
        memberinfo.quality = (uint)(double)(member3["quality"]);
        matchInfo.members.Add(memberinfo);

        loading.rival_list = new List<MatchInfo>();
        loading.rival_list.Add(matchInfo);
        loading.scene_name = UnityEngine.Random.Range(10001, 10009).ToString();
        loading.session_id = sessionID;
        loading.leagueType = GameMatch.LeagueType.eAsynPVP;
        loading.matchType = GameMatch.Type.eAsynPVP3On3;
        loading.matchTime = GameSystem.Instance.CommonConfig.GetUInt("gRegularRaceMatchTime");
        loading.Refresh();
        NGUITools.BringForward(loading.gameObject);
    }
    */

    public void UpdateAssets()
    {
    }

    //state.exit
    public void Exit()
    {
        curClientState = State.exit;
    }

    public void OnLevelWasLoaded(int level)
    {
        if (mUIManager != null)
            mUIManager.OnLevelWasLoaded(level);

        if (mCurMatch != null && mCurMatch.mCurScene != null)
            mCurMatch.mCurScene.OnLevelWasLoaded(level);
        
        ResourceLoadManager.Instance.UnloadDependAB();

		GuideSystem.Instance.OnLevelWasLoaded(level);
    }

    void _OnClientStateChange(State newState)
    {
        string strStateChange = string.Format("Change state: {0} to {1}", mCurClientState, newState);
        mCurClientState = newState;
       // Debug.Log(strStateChange);
    }

	public bool IsConnected()
	{
//#if UNITY_ANDROID
//		if (Application.platform == RuntimePlatform.Android)
//		{
//			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

//			bool isNetworkConnected = jo.Call<bool>("isNetworkConnected");
//			Debug.Log("isNetworkConnected=" + isNetworkConnected);
//			return isNetworkConnected;
//		}
//#endif
		return true;
	}
}
