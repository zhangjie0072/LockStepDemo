//using fogs.proto.config;
//using fogs.proto.msg;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIAsynPVPLoading : MonoBehaviour
//{
//    private UILabel Tip;
//    private UIProgressBar Progress;
//    private UIGrid MyTeamRoles;
//    private UIGrid RivalTeamRoles;

//    public float wait_seconds;
//    public float wait_loading_seconds;
//    [HideInInspector]
//    public string scene_name;
//    [HideInInspector]
//    public ulong session_id;
//    [HideInInspector]
//    public GameMatch.LeagueType leagueType;
//    [HideInInspector]
//    public GameMatch.Type matchType;
//    [HideInInspector]
//    public float matchTime;
//    [HideInInspector]
//    public uint gameModeID;
//    [HideInInspector]
//    public bool needPlayPlot;

//    [HideInInspector]
//    public List<MatchInfo> rival_list;
//    [HideInInspector]
//    public Dictionary<FightStatus, uint> my_role_list;

//    public System.Action onComplete;

//    private bool loaded;
//    private bool isStartLoading = false;

//    public bool ready;

//    void Awake()
//    {
//        Tip = transform.FindChild("Window/Tip").GetComponent<UILabel>();
//        Progress = transform.FindChild("Window/Progress").GetComponent<UIProgressBar>();
//        MyTeamRoles = transform.FindChild("Window/MyTeamRoles").GetComponent<UIGrid>();
//        RivalTeamRoles = transform.FindChild("Window/RivalTeamRoles").GetComponent<UIGrid>();

//        (Progress.foregroundWidget as UISprite).spriteName = "red_bar";
//        Progress.value = 0;

//        onComplete = CreateMatch;
//    }

//    void Update()
//    {
//        if (isStartLoading)
//        {
//            Progress.value += RealTime.deltaTime / wait_loading_seconds;
//            if (Progress.value < 0.5)
//            {
//                (Progress.foregroundWidget as UISprite).spriteName = "red_bar";
//                Progress.foregroundWidget.transform.GetChild(0).gameObject.SetActive(false);
//            }
//            else
//            {
//                (Progress.foregroundWidget as UISprite).spriteName = "blue_bar";
//                Progress.foregroundWidget.transform.GetChild(0).gameObject.SetActive(true);
//            }

//            if (Progress.value >= 1f && loaded && ready)
//            {
//                GameSystem.Instance.mClient.pause = false;
//                Object.Destroy(gameObject);
//            }
//        }
//    }

//    void OnLevelWasLoaded(int level)
//    {
//        Scene sceneInfo = GameSystem.Instance.SceneConfig.GetConfig( uint.Parse(scene_name) );
//        if (Application.loadedLevelName == sceneInfo.resourceId)
//        {
//            loaded = true;
//            GameObject oldUIRoot = GameObject.FindGameObjectWithTag("UIRoot");
//            GameObject oldBasePanel = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel;
//            GameSystem.Instance.mClient.mUIManager.CreateUIRoot(true);
//            foreach (Transform tm in oldBasePanel.transform)
//            {
//                tm.parent = GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.transform;
//            }
//            Object.Destroy(oldUIRoot);
//        }
//    }

//    public void Refresh()
//    {
//        int id = Random.Range(1, 5);
//        string str = "STR_LOADING_TIPS_" + id;
//        Tip.text = CommonFunction.GetConstString(str);
//        RefreshMyTeam();
//        RefreshRivalTeam();
//        StartCoroutine(StartLoading());
//        StartCoroutine(CreateMatchLater());
//        StartCoroutine(LoadScene());
//    }

//    private IEnumerator LoadScene()
//    {
//        yield return new WaitForSeconds(wait_seconds);
//        Object.DontDestroyOnLoad(GameObject.FindGameObjectWithTag("UIRoot"));
//        foreach (Transform tm in transform.parent)
//        {
//            if (tm.gameObject != gameObject)
//                Object.Destroy(tm.gameObject);
//        }
//        Scene scene = GameSystem.Instance.SceneConfig.GetConfig(uint.Parse(scene_name));
//        if (scene == null)
//            Logger.LogError("Can not find scene info: " + scene.id);
//        else
//        {
//            Application.LoadLevelAsync(scene.resourceId);
//            ResourceLoadManager.Instance.UnloadDependAB();
//        }
//    }

//    private void RefreshMyTeam()
//    {
//        if (my_role_list == null)
//            return;
//        foreach (KeyValuePair<FightStatus, uint> pair in my_role_list)
//        {
//            InstantiateRoleItem("PVPRoleItemL", MyTeamRoles.transform,
//                            MainPlayer.Instance.Name, MainPlayer.Instance.WinningStreak, pair.Value);

//        }
//        MyTeamRoles.Reposition();
//        MyTeamRoles.repositionNow = true;

//        TweenMyTeamRoles();
//    }
//    //我的队伍角色
//    private void TweenMyTeamRoles()
//    {
//        int x = 0;
//        int y = 0;
//        for (int i = 0; i < MyTeamRoles.transform.childCount; i++)
//        {
//            TweenPosition pos = MyTeamRoles.transform.GetChild(i).GetComponent<TweenPosition>();
//            pos.from = new Vector3(x, y);
//            pos.value = pos.from;
//            pos.to = new Vector3(x + 1450, y);
//            y -= 200;
//        }
//    }
//    //地方队伍角色
//    private void TweenRivalTeamRoles()
//    {
//        int x = 0;
//        int y = 0;
//        for (int i = 0; i < RivalTeamRoles.transform.childCount; i++)
//        {
//            TweenPosition pos = RivalTeamRoles.transform.GetChild(i).GetComponent<TweenPosition>();
//            pos.from = new Vector3(x, y);
//            pos.value = pos.from;
//            pos.to = new Vector3(x - 1450, y);
//            y -= 200;
//        }
//    }

//    private void RefreshRivalTeam()
//    {
//        if (rival_list.Count > 1)
//        {
//            foreach (MatchInfo info in rival_list)
//            {
//                InstantiateRoleItem("PVPRoleItemR", RivalTeamRoles.transform,
//                       info.team_name, info.winning_streak, info.members[0].id);
//            }
//        }
//        else if (rival_list.Count > 0)
//        {
//            foreach (MatchInfo.MemberInfo member in rival_list[0].members)
//            {
//                InstantiateRoleItem("PVPRoleItemR", RivalTeamRoles.transform,
//                                    rival_list[0].team_name, 0, member.id);
//            }
//        }
//        else
//            return;
//        RivalTeamRoles.Reposition();

//        TweenRivalTeamRoles();
//    }

//    private void InstantiateRoleItem(string prefab_name, Transform parent, string team_name, uint winning_streak, uint role_id)
//    {
//        GameObject item = GameSystem.Instance.mClient.mUIManager.CreateUI(prefab_name, parent);
//        item.transform.FindChild("TeamName").GetComponent<UILabel>().text = team_name;
//        RoleBaseData2 data = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(role_id);
//        if (data != null)
//        {
//            item.transform.FindChild("RoleName").GetComponent<UILabel>().text = (data.type == 1) ? team_name : data.name;
//            item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = data.icon;
//            item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = ((PositionType)data.position).ToString().Substring(3);
//            if (winning_streak < 2)
//                item.transform.FindChild("WinningStreak").gameObject.SetActive(false);
//            else
//                item.transform.FindChild("WinningStreak").GetComponent<UILabel>().text = winning_streak + CommonFunction.GetConstString("PVP_WINNING_STREAK");
//        }
//        else
//        {
//            NPCConfig config = GameSystem.Instance.NPCConfigData.GetConfigData(role_id);
//            if (config != null)
//            {
//                item.transform.FindChild("RoleName").GetComponent<UILabel>().text = config.name;
//                item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = config.icon;
//                item.transform.FindChild("Position").GetComponent<UISprite>().spriteName = ((PositionType)config.position).ToString().Substring(3);
//                if (winning_streak < 2)
//                    item.transform.FindChild("WinningStreak").gameObject.SetActive(false);
//                else
//                    item.transform.FindChild("WinningStreak").GetComponent<UILabel>().text = winning_streak + CommonFunction.GetConstString("PVP_WINNING_STREAK");
//            }
//            else
//                Logger.LogError("Invalid role id: " + role_id);
//        }
//    }

//    private IEnumerator StartLoading()
//    {
//        Logger.Log("Wait for " + wait_loading_seconds + "seconds");
//        yield return new WaitForSeconds(wait_loading_seconds);
//        isStartLoading = true;
//    }
//    private IEnumerator CreateMatchLater()
//    {
//        yield return new WaitForSeconds(wait_seconds);
//        //GameSystem.Instance.mClient.mUIManager.m_uiRootBasePanel.GetComponent<LoadingShow>().enabled = false;
//        if (onComplete != null)
//            onComplete();
//    }

//    private void CreateMatch()
//    {
//        GameMatch.Config config = new GameMatch.Config();
//        config.leagueType = leagueType;
//        config.type = matchType;
//        config.needPlayPlot = needPlayPlot;
//        config.MatchTime = matchTime;
//        config.sceneId = uint.Parse(scene_name);

//        config.session_id = session_id;
//        config.gameModeID = gameModeID;
//        foreach (KeyValuePair<FightStatus, uint> pair in my_role_list)
//        {
//            GameMatch.Config.TeamMember mem = new GameMatch.Config.TeamMember();
//            mem.id = pair.Value.ToString();
//            mem.team = Team.Side.eHome;
//            mem.team_name = MainPlayer.Instance.Name;
//            switch (pair.Key)
//            {
//                case FightStatus.FS_MAIN:
//                    mem.pos = 1;
//                    config.MainRole = mem;
//                    break;
//                case FightStatus.FS_ASSIST1:
//                    mem.pos = 2;
//                    config.NPCs.Add(mem);
//                    break;
//                case FightStatus.FS_ASSIST2:
//                    mem.pos = 3;
//                    config.NPCs.Add(mem);
//                    break;
//            }
//        }
//        int pos = 1;
//        foreach (MatchInfo info in rival_list)
//        {
//            foreach (MatchInfo.MemberInfo mem_info in info.members)
//            {
//                GameMatch.Config.TeamMember mem = new GameMatch.Config.TeamMember();
//                mem.id = mem_info.id.ToString();
//                mem.team = Team.Side.eAway;
//                mem.pos = pos++;
//                mem.team_name = info.team_name;
//                config.NPCs.Add(mem);
//            }
//        }
//        GameSystem.Instance.mClient.CreateNewMatch(config);
//        GameSystem.Instance.mClient.pause = true;
//        ready = true;
//    }
//}