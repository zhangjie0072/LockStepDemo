using fogs.proto.config;
using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

using UnityEngine;

public class UpdatePrec
{
    public Transform transform;
    public int PrecValue;
    public int PrecFrame;
    public int NowFrame;
}
    public class UIChallengeLoading : MonoBehaviour
{
	private GameObject	SingleBaseBg;
	private GameObject	MultiBaseBg;
	private GameObject	Window;

    private UILabel Tip;
	private UILabel TipSimple;

    private UILabel MyTeamName;
    private UILabel RivalTeamName;
	private UISprite Title;
	private UITexture BG;
	private UITexture BGSimple;
	private UILabel LblNarrowTip;
	private UILabel LblMultiBaseTip;
    //private UIProgressBar Progress;
    
	//private UIGrid MyTeamRoles;
    //private UIGrid RivalTeamRoles;

    public float wait_seconds;
    public float wait_loading_seconds;
    
	[HideInInspector]
	public bool single = false;
	[HideInInspector]
    public string scene_name;
    [HideInInspector]
    public ulong session_id;
	[HideInInspector]
	public GameMatch.LeagueType leagueType;
	[HideInInspector]
	public GameMatch.Type matchType;
	[HideInInspector]
	public float matchTime;
	[HideInInspector]
	public uint gameModeID;
	[HideInInspector]
	public bool needPlayPlot;

	[HideInInspector]
	public List<RoleInfo> rival_list = new List<RoleInfo>();
    [HideInInspector]
	public List<Player> rival_player_list = new List<Player>();
    [HideInInspector]
    public List<string> rival_name_list;
    [HideInInspector]
    public List<uint> rival_score_list;

	[HideInInspector]
	public List<RoleInfo> my_role_list = new List<RoleInfo>();
    [HideInInspector]
	public List<Player> my_role_player_list = new List<Player>();
	[HideInInspector]
    public List<string> my_role_name_list;
	[HideInInspector]
    public List<uint> my_role_score_list;
    [HideInInspector]
    public string myName;
    [HideInInspector]
    public string rivalName;
	public System.Action onComplete;

	private bool loaded;
    private bool isStartLoading = false;
	private bool loadComplete = false;

	public bool pvp = false;
    public bool disConnected = false;
    //public string offName;
    public PVPEndChallengePlusResp pvpPlusEndResp = null;
    public PVPEndChallengeExResp pvpExEndResp = null;
    public PVPEndQualifyingNewerResp qualifyingNewerResp = null;
	public PVPEndRegularResp pvpRegularEndResp = null;
	public PVPEndQualifyingResp pvpQualifyingEndResp = null;

	private Dictionary<uint, List<LuaComponent>> mapPlayerIdLoadingState = new Dictionary<uint, List<LuaComponent>>();

	private GameUtils.Timer4View	m_wait;
	private Pack			m_matchBeginPack;
	private bool			m_bInitHandler = false;
    private bool m_delayDestroy = false;
    //private List<LuaComponent> m_delayLoadLua = null;

    private List<UpdatePrec> updatePreclist;


	void Awake()
    {

        updatePreclist = new List<UpdatePrec>();
        m_bInitHandler = false;

		loadComplete = false;
		GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
		if( curMatch != null && curMatch.leagueType != GameMatch.LeagueType.ePVP &&
			(curMatch.leagueType != GameMatch.LeagueType.eRegular1V1 || curMatch.GetMatchType() == GameMatch.Type.eCareer3On3) &&
			(curMatch.leagueType != GameMatch.LeagueType.eQualifyingNew || curMatch.GetMatchType() == GameMatch.Type.eCareer3On3))
		{
			PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
			single = pm.m_Players.Count <= 2;
		}

        Tip = transform.FindChild("Window/Bottom/Tip").GetComponent<UILabel>();
		TipSimple = transform.FindChild("Window/Bottom/Tip_Simple").GetComponent<UILabel>();

		Title = transform.FindChild("Window/Top/TopBG/Icon").GetComponent<UISprite>();
		
		MyTeamName = transform.FindChild("Window/Top/WeName").GetComponent<UILabel>();
		RivalTeamName = transform.FindChild("Window/Top/TheyName").GetComponent<UILabel>();

		//MyTeamRoles = transform.FindChild("Window/Middle/Left/Grid").GetComponent<UIGrid>();
		//RivalTeamRoles = transform.FindChild("Window/Middle/Right/Grid").GetComponent<UIGrid>();

		SingleBaseBg = transform.FindChild("Window/Middle/NarrowBase").gameObject;
		MultiBaseBg = transform.FindChild("Window/Middle/WideBase").gameObject;
		LblNarrowTip = SingleBaseBg.transform.FindChild ("Tip").GetComponent<UILabel> ();
		LblMultiBaseTip = MultiBaseBg.transform.FindChild ("Tip").GetComponent<UILabel> ();

		Window = transform.FindChild("Window").gameObject;
		BG = transform.FindChild("Window/Bg").gameObject.GetComponent<UITexture>();
		BGSimple = transform.FindChild("Window/Bg_Simple").gameObject.GetComponent<UITexture>();

		foreach( KeyValuePair<uint, List<LuaComponent>> items in mapPlayerIdLoadingState )
			items.Value.Clear();
		mapPlayerIdLoadingState.Clear();

		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_platConn;
		if( conn != null )
			conn.EnableTimeout(false);
		conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( conn != null )
			conn.EnableTimeout(false);

        GameSystem.Instance.mClient.mUIManager.isInMatchLoading = true;
    }

	void OnHandleMatchBeginTimer()
	{
		Logger.Log("match begin.");
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if(conn == null || conn is VirtualNetworkConn || m_matchBeginPack == null)
			return;
		GameSystem.Instance.mNetworkManager.m_gameMsgHandler.UnregisterHandler(MsgID.MatchBeginID, HandleMatchBegin);
		//conn.m_handler.UnregisterHandler(MsgID.PVPLoadCompleteRespID, HandleLoadComplete);
		
		MatchBegin resp = Serializer.Deserialize<MatchBegin>(new MemoryStream(m_matchBeginPack.buffer));
		if( resp == null )
		{
			Debug.LogError("no pvp loading complete resp");
			return;
		}
		
		//close ui
		foreach( KeyValuePair<uint, List<LuaComponent>> items in mapPlayerIdLoadingState )
			items.Value.Clear();
		mapPlayerIdLoadingState.Clear();

		GameSystem.Instance.mClient.pause = false;

		
		GameMatch_PVP match = GameSystem.Instance.mClient.mCurMatch as GameMatch_PVP;
		foreach( FightRole role in resp.home_position.fighters )
			match.SetPlayerPos(TeamType.TT_HOME, role);
		foreach( FightRole role in resp.away_position.fighters )
			match.SetPlayerPos(TeamType.TT_AWAY, role);

		match.OnInitPlayer();
        if (MainPlayer.Instance.inPvpJoining)
        {
            GameMsgSender.SendGameBegin();
            m_delayDestroy = true;
        }
        else
        {
            match.m_stateMachine.SetState(MatchState.State.eOpening);
		    Object.Destroy(gameObject);
        }
	}

	void Start()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if(match != null && match.leagueType != GameMatch.LeagueType.ePractise)
		{
			SingleBaseBg.SetActive(single);
			MultiBaseBg.SetActive(!single);
		}
	}

	void _InitGameMsgHandler()
	{
		if( m_bInitHandler )
			return;
		NetworkManager conn = GameSystem.Instance.mNetworkManager;
        conn.m_gameMsgHandler.RegisterHandler(MsgID.PVPLoadCompleteRespID, HandleLoadComplete);
        conn.m_gameMsgHandler.RegisterHandler(MsgID.PVPLoadProgressBroadcastID, HandleLoadProgress);
		conn.m_gameMsgHandler.RegisterHandler(MsgID.MatchBeginID, HandleMatchBegin);

		m_bInitHandler = true;
	}
    /// <summary>
    /// 加载进度设置
    /// </summary>
    /// <param name="pack"></param>
    void HandleLoadProgress(Pack pack)
    {
        PVPLoadProgressBroadcast resp = Serializer.Deserialize<PVPLoadProgressBroadcast>(new MemoryStream(pack.buffer));
        RefreshPlayerPercentagePVP((int)resp.progress,resp.acc_id);
    }

    void HandleLoadComplete(Pack pack)
	{
		NetworkConn conn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if(conn == null || conn is VirtualNetworkConn)
			return;
		PVPLoadCompleteResp resp = Serializer.Deserialize<PVPLoadCompleteResp>(new MemoryStream(pack.buffer));
		if( resp == null )
		{
			Debug.LogError("no pvp loading complete resp");
			return;
		}

		Logger.Log("Player: " + resp.load_acc_id + " load complete.");
		
		List<LuaComponent> items;
		if( !mapPlayerIdLoadingState.TryGetValue( resp.load_acc_id, out items ) )
			return;
        if (!MainPlayer.Instance.inPvpJoining)
        {
            items.ForEach((LuaComponent item) =>
                          {
                              item.table.Set("loadingState", CommonFunction.GetConstString("STR_LOAD_COMPLETE"));
                              item.table.Set("loadDone", true);
                          });
        }
        else
        {
            //m_delayLoadLua = items;
        }
	}

	void HandleMatchBegin(Pack pack)
	{
		m_matchBeginPack = pack;
		m_wait = new GameUtils.Timer4View(3f, OnHandleMatchBeginTimer, 0);
	}

    void Update()
    {
        for (int i = updatePreclist.Count - 1; i >= 0; i--)
        {
            updatePreclist[i].NowFrame++;
            if (updatePreclist[i].NowFrame== updatePreclist[i].PrecFrame)
            {
                updatePreclist[i].PrecValue--;
                updatePreclist[i].NowFrame = 0;
                RefreshLoadingUiPercentage(updatePreclist[i].transform, 1);
            }
            if (updatePreclist[i].PrecValue == 0)
            {
                updatePreclist.RemoveAt(i);
            }
        }

		_InitGameMsgHandler();

        if (!isStartLoading)
			return;

		if (m_wait != null)
			m_wait.Update(Time.deltaTime);

		GameMatch_PVP match = GameSystem.Instance.mClient.mCurMatch as GameMatch_PVP;
		if( match == null )
		{
			if (loaded)
			{
				GameSystem.Instance.mClient.pause = false;
				if( onComplete != null )
					onComplete();
				loadComplete = true;
				Object.Destroy(gameObject);
			}
		}
		else
		{
			if (loaded && match.m_bPlayerBuildDone && !loadComplete)
			{
				if( onComplete != null )
					onComplete();
				GameMsgSender.SendLoadingComplete(match.GetMatchType());
				loadComplete = true;
			}

			if (loaded && disConnected)
			{
				disConnected = false;
				ShowOffLine();
			}

            if( m_delayDestroy && !MainPlayer.Instance.inPvpJoining )
            {
//               if( m_delayLoadLua != null)
//               {
//                   m_delayLoadLua.ForEach((LuaComponent item) =>
//                                 {
//                                     item.table.Set("loadingState", CommonFunction.GetConstString("STR_LOAD_COMPLETE"));
//                                     item.table.Set("loadDone", true);
//                                 });

//               }
                Object.Destroy(gameObject);
            }
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		Logger.Log("UIChallengeLoading.OnLevelWasLoaded:" + Application.loadedLevelName);
		if (Application.loadedLevelName == scene_name) {
            loaded = false;
            if (pvp)
            {
                GameMsgSender.SendPVPLoadProgress(50);
            }
            else
            {
                RefreshUiPercentage(50);
            }
        }
		else
			Object.Destroy(gameObject);
	}

	void OnDestroy()
	{
		Logger.Log("UIChallengeLoading.OnDestroy");
		NetworkManager conn = GameSystem.Instance.mNetworkManager;
		if(conn == null)
			return;
		conn.m_gameMsgHandler.UnregisterHandler(MsgID.PVPLoadCompleteRespID, HandleLoadComplete);
        conn.m_gameMsgHandler.UnregisterHandler(MsgID.PVPLoadProgressBroadcastID, HandleLoadProgress);
        
        NetworkConn platConn = GameSystem.Instance.mNetworkManager.m_platConn;
		if( platConn != null )
			platConn.EnableTimeout(true);
		NetworkConn gameConn = GameSystem.Instance.mNetworkManager.m_gameConn;
		if( gameConn != null )
			gameConn.EnableTimeout(true);

        GameSystem.Instance.mClient.mUIManager.isInMatchLoading = false;
        GameSystem.Instance.mClient.mUIManager.ShowMask(false);
	}

	public void LoadFromMatch(string strSceneId, GameMatch match)
	{
		scene_name 	= strSceneId;
		GameMatch.Config config = match.m_config;
		PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
		Player mainPlayer = null;
		foreach( Player player in pm )
		{
			if( player.m_id != uint.Parse(config.MainRole.id) )
				continue;
			mainPlayer = player;
			break;
		}

		if( mainPlayer == null )
			return;

		foreach( Player player in mainPlayer.m_team )
			my_role_player_list.Add(player);

		foreach( Player player in pm )
		{
			if( player.m_team == mainPlayer.m_team )
				continue;
			rival_player_list.Add(player);
		}
	}

	void _OnLoadTexFinish(Object go)
	{
		TipSimple.transform.parent.gameObject.SetActive(true);
		TipSimple.gameObject.SetActive(true);

		BGSimple.gameObject.SetActive(true);
		BGSimple.mainTexture = go as Texture;

		StartCoroutine(StartLoading());
		StartCoroutine(LoadScene());
	}

    public void Refresh(bool bOnlyPic)
    {
		int id = Random.Range(1, 26);
		string str = "STR_LOADING_TIPS_" + id;
		Tip.text = CommonFunction.GetConstString(str);
		TipSimple.text = CommonFunction.GetConstString(str);
		LblNarrowTip.text = TipSimple.text;
		LblMultiBaseTip.text = TipSimple.text;
		
		if( bOnlyPic )
		{
			GetComponent<Animator>().enabled = false;
			NGUITools.SetActiveChildren(Window, false);
			ResourceLoadManager.Instance.LoadAloneImage(GameSystem.Instance.CommonConfig.GetString("gPracticeBg"), _OnLoadTexFinish);
			if (GameSystem.Instance.isNewPlayer)
			{
				BGSimple.gameObject.SetActive(false);
				TipSimple.gameObject.SetActive(false);
			}
		}
		else 
		{
			TipSimple.gameObject.SetActive(false);
			BGSimple.gameObject.SetActive(false);

			GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
			Title.spriteName = curMatch.LeagueTypeToSpriteName();
			Title.MakePixelPerfect ();
	        
			Logger.Log("Refresh team");
			if( pvp )
			{
				if (myName != null && rivalName != null)
				{
					MyTeamName.text = myName;
					RivalTeamName.text = rivalName;
				}

				RefreshMyTeam_PVP();
				RefreshRivalTeam_PVP();
				GameMatch_PVP match_pvp = curMatch as GameMatch_PVP;

                if(curMatch.leagueType == GameMatch.LeagueType.eQualifyingNewer
                    || curMatch.leagueType == GameMatch.LeagueType.ePVP 
                    )
                {
					MyTeamName.text = CommonFunction.GetConstString("STR_HOME");
					RivalTeamName.text = CommonFunction.GetConstString("STR_PEER");
                }
			}
			else
			{
				if (curMatch.leagueType == GameMatch.LeagueType.eRegular1V1 && curMatch.GetMatchType() == GameMatch.Type.eCareer3On3)
				{
					MyTeamName.text = MainPlayer.Instance.Name;
					RivalTeamName.text = (string)(LuaScriptMgr.Instance.GetLuaTable("Regular1V1Handler")["npcRivalName"]);
				}
				else if (curMatch.leagueType == GameMatch.LeagueType.eQualifyingNew && curMatch.GetMatchType() == GameMatch.Type.eCareer3On3)
				{
					MyTeamName.text = MainPlayer.Instance.Name;
					RivalTeamName.text = (string)(LuaScriptMgr.Instance.GetLuaTable("UIQualifyingNew")["npcRivalName"]);
				}
	        	RefreshMyTeam();
	        	RefreshRivalTeam();
			}
	        SetTeamMatesName();
	        SetRivalName();
            SetTeamMatesScore();
            SetRivalScore();
			StartCoroutine(StartLoading());
			StartCoroutine(LoadScene());
		}
    }

    public void LoadResources(List<string> uiNames, PlayerManager pm)
    {
        StartCoroutine(LoadUi(uiNames, pm));
    }

    private IEnumerator LoadUi(List<string> uiNames, PlayerManager pm)
    {
        yield return new WaitForSeconds(2.5f);
        foreach (Player player in pm)
        {
            //GameSystem.Instance.mClient.mCurMatch._CreateTeamMember(player);
            if (!pvp)
            {
                RefreshPlayerPercentage(30, player.m_id);
               
            }
            yield return new WaitForEndOfFrame();
        }
        if (pvp)
        {
            GameMsgSender.SendPVPLoadProgress(30);
        }

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < uiNames.Count; i++)
        {
            ResourceLoadManager.Instance.LoadPrefab(uiNames[i]);
          
            yield return new WaitForEndOfFrame();
        }
        if (pvp)
        {
            GameMsgSender.SendPVPLoadProgress((uint)20);
        }
        else
        {
            RefreshUiPercentage(20);
        }
        yield return new WaitForSeconds(1f);
        loaded = true;
    }

    private void SetPrecValueAndTime(Transform transform,int addPerc)
    {
        int time = 0;
        if (addPerc==50)
        {
            time = 25;
        }else if (addPerc==20)
        {
            time = 10;
        }
        else if (addPerc == 30)
        {
            time = 15;
        }
        UpdatePrec up = new UpdatePrec();
        up.PrecFrame = time * 3/ addPerc;
        up.PrecValue = addPerc;
        up.transform = transform;
        updatePreclist.Add(up);
    }

    /// <summary>
    /// PvP更新加载百分比
    /// </summary>
    /// <param name="addPerc"></param>
    void RefreshPlayerPercentagePVP(int addPerc, uint id)
    {
        //直接刷新面板
        if (my_role_list != null && my_role_list.Count > 0)
        {
            if (single)
            {
                string str = "LeftItem2";
                Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                RoleInfo ri = my_role_list[0];
                if (ri.acc_id == id)
                {
                    SetPrecValueAndTime(role,addPerc);
                    return;
                }
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "LeftItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                    if (k >= my_role_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    RoleInfo ri = my_role_list[k];
                    if (ri.acc_id == id)
                    {
                        SetPrecValueAndTime(role, addPerc);
                        return;
                    }
                }
            }
        }
        if (rival_list != null && rival_list.Count > 0)
        {
            if (single)
            {
                string str = "RightItem2";
                Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                role.gameObject.SetActive(true);
                RoleInfo ri = rival_list[0];
                if (ri.acc_id == id)
                {
                    SetPrecValueAndTime(role, addPerc);
                    return;
                }
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "RightItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                    if (k >= rival_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    role.gameObject.SetActive(true);
                    RoleInfo ri = rival_list[k];
                    if (ri.acc_id == id)
                    {
                        SetPrecValueAndTime(role, addPerc);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// pve更新加载百分比
    /// </summary>
    /// <param name="addPerc"></param>
    void RefreshPlayerPercentage(int addPerc, uint id)
    {
        //直接刷新面板
        if (my_role_player_list != null && my_role_player_list.Count > 0)
        {
            if (single)
            {
                string str = "LeftItem2";
                Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                Player ri = my_role_player_list[0];
                if (ri.m_id == id)
                {
                    SetPrecValueAndTime(role, addPerc);
                    return;
                }
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "LeftItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                    if (k >= my_role_player_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    Player ri = my_role_player_list[k];
                    if (ri.m_id == id)
                    {
                        SetPrecValueAndTime(role, addPerc);
                        return;
                    }
                }
            }
        }
        if (rival_player_list != null && rival_player_list.Count > 0)
        {
            if (single)
            {
                string str = "RightItem2";
                Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                role.gameObject.SetActive(true);
                Player ri = rival_player_list[0];
                if (ri.m_id == id)
                {
                    SetPrecValueAndTime(role, addPerc);
                    return;
                }
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "RightItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                    if (k >= rival_player_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    role.gameObject.SetActive(true);
                    Player ri = rival_player_list[k];
                    if (ri.m_id == id)
                    {
                        SetPrecValueAndTime(role, addPerc);
                        return;
                    }
                }
            }
        }
    }
    /// <summary>
    ///打电脑更新Ui加载百分比
    /// </summary>
    /// <param name="addPerc">百分比值</param>
    void RefreshUiPercentage(int addPerc)
    {
        //直接刷新面板
        if (my_role_player_list != null&& my_role_player_list.Count>0)
        {
            if (single)
            {
                string str = "LeftItem2";
                Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                role.gameObject.SetActive(true);
                Player ri = my_role_player_list[0];
                SetPrecValueAndTime(role, addPerc);
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "LeftItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                    if (k >= my_role_player_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    role.gameObject.SetActive(true);
                    Player ri = my_role_player_list[k];
                    if (ri == null)
                        continue;
                    SetPrecValueAndTime(role, addPerc);
                }
            }
        }
        if (rival_player_list != null && rival_player_list.Count > 0)
        {

            if (single)
            {
                string str = "RightItem2";
                Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                role.gameObject.SetActive(true);
                Player ri = rival_player_list[0];
                SetPrecValueAndTime(role, addPerc);
            }
            else
            {
                for (int k = 0; k < 3; k++)
                {
                    string str = "RightItem" + (k + 1);
                    Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                    if (k >= rival_player_list.Count && role != null)
                    {
                        role.gameObject.SetActive(false);
                        continue;
                    }
                    role.gameObject.SetActive(true);
                    Player ri = rival_player_list[k];
                    if (ri == null)
                        continue;
                    SetPrecValueAndTime(role, addPerc);
                }
            }
        }
    }


    void RefreshLoadingUiPercentage(Transform role,float Perc)
    {
        LuaComponent loadingItem = role.GetComponent<LuaComponent>();
        if (loadingItem != null && loadingItem.table!=null)
        {
            string loadingState = loadingItem.table["loadingState"].ToString();
            if (loadingState.Length>5) {
                float ShowPerc = float.Parse(loadingState.Substring(4, loadingState.Length - 5)) + Perc;
                if (ShowPerc > 100)
                {
                    ShowPerc = 100;
                }
                loadingItem.table.Set("loadingState", CommonFunction.GetConstString("STR_LOADING") + ShowPerc + "%");
            }
        }
    }

    private IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(wait_seconds);
		uint sceneId = 0;
		if( !uint.TryParse(scene_name, out sceneId) )
		{
			Logger.Log("UIChallengeLoading.LoadScene " + scene_name);
			Application.LoadLevelAsync(scene_name);
		}
		else
		{
			Scene scene = GameSystem.Instance.SceneConfig.GetConfig(sceneId);
			if (scene == null)
            	Debug.LogError("Can not find scene info: " + scene.id);
			else
			{
				Logger.Log("UIChallengeLoading.LoadScene " + scene.resourceId);
                Application.LoadLevelAsync(scene.resourceId);
				scene_name = scene.resourceId;
			}
		}        
        ResourceLoadManager.Instance.UnloadDependAB();
	}

	private void _RefreshLoadingItem(Transform role, Player ri, bool bMySide = true)
	{
		role.transform.FindChild("Name").GetComponent<UILabel>().text = ri.m_name;
		role.transform.FindChild("Position").GetComponent<UISprite>().spriteName = "PT_" + (ri.m_position).ToString().Substring(3); 
		
		LuaComponent loadingItem = role.GetComponent<LuaComponent>();
		loadingItem.table.Set("id", ri.m_roleInfo.id);
		if( bMySide )
			loadingItem.table.Set("loadingState", CommonFunction.GetConstString("STR_LOADING") +"0%");
		else
		{
			loadingItem.table.Set("loadingState", CommonFunction.GetConstString("STR_LOADING") + "0%");
			loadingItem.table.Set("loadDone", true);
		}
   
		loadingItem.table.Set("power", ri.m_fightingCapacity.floorToInt);
		
		LuaComponent luaCom = role.FindChild("Icon/CareerRoleIcon").GetComponent<LuaComponent>();
		luaCom.table.Set("showPosition", false);  
		luaCom.table.Set("npc", ri.m_config.bIsNPC );
        if( (GameSystem.Instance.mClient.mCurMatch.leagueType == GameMatch.LeagueType.eTour ||
             GameSystem.Instance.mClient.mCurMatch.leagueType == GameMatch.LeagueType.eQualifying) && !bMySide)
        {
            luaCom.table.Set("qStar", ri.m_roleInfo.star);
            luaCom.table.Set("qLevel", ri.m_roleInfo.level);
            luaCom.table.Set("qQuality", ri.m_roleInfo.quality);
        }
        if( pvp )
        {
		    luaCom.table.Set("otherInfo", ri.m_roleInfo);
        }
	}

	private void _RefreshLoadingItem(Transform role, RoleInfo ri, bool bMySide = true)
	{
		RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(ri.id);
		role.transform.FindChild("Name").GetComponent<UILabel>().text = data.name;
		role.transform.FindChild("Position").GetComponent<UISprite>().spriteName = "PT_" + ((PositionType)data.position).ToString().Substring(3); 
		
		LuaComponent loadingItem = role.GetComponent<LuaComponent>();
		loadingItem.table.Set("id", data.id);
		loadingItem.table.Set("loadingState", CommonFunction.GetConstString("STR_LOADING") + "0%");
		
		loadingItem.table.Set("power", ri.fight_power);
		LuaComponent luaCom = role.FindChild("Icon/CareerRoleIcon").GetComponent<LuaComponent>();
		luaCom.table.Set("showPosition", false);  
		luaCom.table.Set("npc", false );
        if( pvp )
        {
    		luaCom.table.Set("otherInfo", ri);
        }

		List<LuaComponent> items;
		if( !mapPlayerIdLoadingState.TryGetValue(ri.acc_id, out items) )
		{
			items = new List<LuaComponent>();
			items.Add(loadingItem);
			mapPlayerIdLoadingState.Add(ri.acc_id, items);
		}
		else
		{
			items.Add(loadingItem);
			mapPlayerIdLoadingState[ri.acc_id] = items;
		}

        if( MainPlayer.Instance.inPvpJoining 
            && MainPlayer.Instance.AccountID != ri.acc_id)
        {
            items.ForEach((LuaComponent item) =>
                          {
                              item.table.Set("loadingState", CommonFunction.GetConstString("STR_LOAD_COMPLETE"));
                              item.table.Set("loadDone", true);
                          });
        }
	}
	
	private void RefreshMyTeam()
    {
		if (my_role_player_list == null)
			return;
        
		if( single )
		{
			string str = "LeftItem2";
			Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
			role.gameObject.SetActive(true);
			Player ri = my_role_player_list[0];
			_RefreshLoadingItem(role, ri);
		}
		else
		{
			for (int i = 0; i < 3; i++ )
			{
				string str = "LeftItem" + ( i + 1);
				Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
				if( i >= my_role_player_list.Count && role != null )
				{
					role.gameObject.SetActive(false);
					continue;
				}
				role.gameObject.SetActive(true);
				Player ri = my_role_player_list[i];
				if( ri == null )
					continue;
				_RefreshLoadingItem(role, ri);
			}
		}
		//MyTeamRoles.Reposition();
		//MyTeamRoles.repositionNow = true;
       // TweenMyTeamRoles();
    }

	private void RefreshMyTeam_PVP()
	{
		if (my_role_list == null)
			return;

		if( single )
		{
			string str = "LeftItem2";
			Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
			role.gameObject.SetActive(true);
			RoleInfo ri = my_role_list[0];
			_RefreshLoadingItem(role, ri);
		}
		else
		{
			for (int i = 0; i < 3; i++ )
			{
				string str = "LeftItem" + ( i + 1);
				Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
				if( i >= my_role_list.Count && role != null )
				{
					role.gameObject.SetActive(false);
					continue;
				}
				role.gameObject.SetActive(true);
				RoleInfo ri = my_role_list[i];
				if( ri == null )
					continue;
				_RefreshLoadingItem(role, ri);
			}
		}
		
		//MyTeamRoles.Reposition();
		//MyTeamRoles.repositionNow = true;
		// TweenMyTeamRoles();
	}

	private void RefreshRivalTeam()
	{
		if (rival_player_list == null)
			return;

		if( single )
		{
			string str = "RightItem2";
			Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
			role.gameObject.SetActive(true);
			Player ri = rival_player_list[0];
			_RefreshLoadingItem(role, ri, false);
		}
		else
		{
			for (int i = 0; i < 3; i++ )
			{
				string str = "RightItem" + ( i + 1);
				Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
				if( i >= rival_player_list.Count && role != null )
				{
					role.gameObject.SetActive(false);
					continue;
				}
				role.gameObject.SetActive(true);
				Player ri = rival_player_list[i];
				if( ri == null )
					continue;
				_RefreshLoadingItem(role, ri, false);
			}
		}

		//RivalTeamRoles.Reposition();
		//RivalTeamRoles.repositionNow = true;
		//TweenRivalTeamRoles();
	}

	private void RefreshRivalTeam_PVP()
	{
		if (rival_list == null)
			return;
		
		if( single )
		{
			string str = "RightItem2";
			Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
			role.gameObject.SetActive(true);
			RoleInfo ri = rival_list[0];
			_RefreshLoadingItem(role, ri, false);
		}
		else
		{
			for (int i = 0; i < 3; i++ )
			{
				string str = "RightItem" + ( i + 1);
				Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
				if( i >= rival_list.Count && role != null )
				{
					role.gameObject.SetActive(false);
					continue;
				}
				role.gameObject.SetActive(true);
				RoleInfo ri = rival_list[i];
				if( ri == null )
					continue;
				_RefreshLoadingItem(role, ri, false);
			}
		}
		
		//RivalTeamRoles.Reposition();
		//RivalTeamRoles.repositionNow = true;
		//TweenRivalTeamRoles();
	}

	//我的队伍角色
	/*
	private void TweenMyTeamRoles()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < MyTeamRoles.transform.childCount; i++)
        {
            TweenPosition pos = MyTeamRoles.transform.GetChild(i).GetComponent<TweenPosition>();
            pos.from = new Vector3(x, y);
            pos.value = pos.from;
            pos.to = new Vector3(x + 1450, y);
            y -= 200;
        }
    }
    //地方队伍角色
    private void TweenRivalTeamRoles()
    {
        int x = 0;
        int y = 0;
        for (int i = 0; i < RivalTeamRoles.transform.childCount; i++)
        {
            TweenPosition pos = RivalTeamRoles.transform.GetChild(i).GetComponent<TweenPosition>();
            pos.from = new Vector3(x, y);
            pos.value = pos.from;
            pos.to = new Vector3(x - 1450, y);
            y -= 200;
        }
    }
    */
	
    private void SetTeamMatesName()
    {
        if (my_role_name_list.Count > 0)
        {
            NGUITools.SetActive(MyTeamName.gameObject, true);
            for (int i = 0; i < my_role_name_list.Count; ++i)
            {
                string str = "LeftItem" + (i + 1);
                Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                GameObject label = role.transform.FindChild("Name").gameObject;
                NGUITools.SetActive(label, true);
                label.GetComponent<UILabel>().text = my_role_name_list[i];
            }
        }
    }

    private void SetRivalName()
    {
        if (rival_name_list.Count > 0)
        {
            NGUITools.SetActive(RivalTeamName.gameObject, true);
            for (int i = 0; i < rival_name_list.Count; ++i)
            {
                string str = "RightItem" + (i + 1);
                Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                GameObject label = role.transform.FindChild("Name").gameObject;
                NGUITools.SetActive(label, true);
                label.GetComponent<UILabel>().text = rival_name_list[i];
            }
        }
    }

    private void SetTeamMatesScore()
    {
        GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
        Logger.Log("SetTeamMateScore curMatch.leagueType =" + curMatch.leagueType);


        if (my_role_score_list.Count > 0 
            && curMatch != null 
            && curMatch.leagueType == GameMatch.LeagueType.eQualifyingNewer)
        {
            for (int i = 0; i < my_role_score_list.Count; ++i)
            {
                string str = "LeftItem" + (i + 1);
                Transform role = transform.FindChild("Window/Middle/Left/Grid/" + str + "/ChallengeLeftItem");
                GameObject score = role.transform.FindChild("Dan").gameObject;
                NGUITools.SetActive(score, true);
                fogs.proto.config.QualifyingNewerConfig grade = GameSystem.Instance.qualifyingNewerConfig.GetGrade(my_role_score_list[i]);

                score.GetComponent<UISprite>().spriteName = grade.icon_small;
            }
        }
    }

    private void SetRivalScore()
    {
        GameMatch curMatch = GameSystem.Instance.mClient.mCurMatch;
        Logger.Log("SetRival MateScore curMatch.leagueType =" + curMatch.leagueType);

        if (rival_score_list.Count > 0 
            && curMatch != null 
            && curMatch.leagueType == GameMatch.LeagueType.eQualifyingNewer)
        {
            for (int i = 0; i < rival_score_list.Count; ++i)
            {
                string str = "RightItem" + (i + 1);
                Transform role = transform.FindChild("Window/Middle/Right/Grid/" + str + "/ChallengeRightItem");
                GameObject score = role.transform.FindChild("Dan").gameObject;
                NGUITools.SetActive(score, true);

                fogs.proto.config.QualifyingNewerConfig  grade = GameSystem.Instance.qualifyingNewerConfig.GetGrade(rival_score_list[i]);
                score.GetComponent<UISprite>().spriteName = grade.icon_small;
            }
        }
    }


    private void InstantiateRoleItem(string prefab_name, Transform parent,uint role_id)
    {
        GameObject item = GameSystem.Instance.mClient.mUIManager.CreateUI(prefab_name, parent);
        Transform grid = item.transform.FindChild("Icon");
        LuaComponent luaCom = CommonFunction.InstantiateObject("Prefab/GUI/careerRoleIcon", grid).GetComponent<LuaComponent>();
        luaCom.table.Set("id", role_id);
        luaCom.table.Set("showPosition", false);
        RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(role_id);
        item.transform.FindChild("Name").GetComponent<UILabel>().text = data.name;
        item.transform.FindChild("Profession").GetComponent<UISprite>().spriteName = ((PositionType)data.position).ToString().Substring(3);  
    }

    private IEnumerator StartLoading()
    {
		Logger.Log("Wait for " + wait_loading_seconds + "seconds");
        yield return new WaitForSeconds(wait_loading_seconds);
        isStartLoading = true;
    }

    private void ShowOffLine()
    {
        UIEventListener.VoidDelegate onConfirmExit = (GameObject go) =>
        {
			UIManager uiManager = GameSystem.Instance.mClient.mUIManager;
			GameMatch match = GameSystem.Instance.mClient.mCurMatch;
			if (match.leagueType == GameMatch.LeagueType.eRegular1V1)
			{
				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIHall");
			}
			else if (match.leagueType == GameMatch.LeagueType.eQualifyingNew)
			{
				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", "UIQualifyingNew");
			}
			else if (match.GetMatchType() == GameMatch.Type.ePVP_1PLUS)
			{
				LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
				table.Set("uiBack", (object)"UIPVPEntrance");
				table.Set("autoMatch", (object)true);
				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UI1V1Plus", null, table });
			}
			else if (match.GetMatchType() == GameMatch.Type.ePVP_3On3)
			{
				LuaInterface.LuaTable table = LuaScriptMgr.Instance.lua.NewTable();
//				table.Set("nextShowUI", (object)"UIPVPEntrance");
//				table.Set("autoMatch", (object)true);
				LuaScriptMgr.Instance.CallLuaFunction("jumpToUI", new object[] { "UIHall", null, table });
			}
        };

        bool isNotify = false;
        if (!string.IsNullOrEmpty(pvpPlusEndResp.off_name))
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), pvpPlusEndResp.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            isNotify = true;
        }
        else if (!string.IsNullOrEmpty(pvpExEndResp.off_name))
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), pvpExEndResp.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            isNotify = true;
        }
        else if (!string.IsNullOrEmpty(qualifyingNewerResp.off_name))
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), qualifyingNewerResp.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            isNotify = true;
        }
       else if (!string.IsNullOrEmpty(pvpRegularEndResp.off_name))
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), pvpRegularEndResp.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            isNotify = true;
       }
        else if (!string.IsNullOrEmpty(pvpQualifyingEndResp.off_name))
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), pvpQualifyingEndResp.off_name), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            isNotify = true;
        }

        if( MainPlayer.Instance.inPvpJoining)
        {
            CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("GAME_OVER"), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
            MainPlayer.Instance.inPvpJoining = false;
            isNotify = true;
        }

        if( !isNotify )
        {
            CommonFunction.ShowPopupMsg(string.Format(CommonFunction.GetConstString("STR_PVP_DISCONNECT"), ""), UIManager.Instance.m_uiRootBasePanel.transform, onConfirmExit);
        }
    }
}