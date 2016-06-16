using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using fogs.proto.msg;
using System.Threading;
using System.Runtime.InteropServices;

public class UILoginControl
{
    GameObject goUI;
    public UILogin _ui;

    public string Account;
    public string Password;
    public string Name;

    private bool isInitialized = false;
    private Dictionary<string, string> ServerTextmap = new Dictionary<string, string>();

    private GameObject loginServerPopup;
    private Dictionary<int, List<ServerListInfo>> eachGroupList = new Dictionary<int, List<ServerListInfo>>();
    private UIGrid leftGrid;
    private UIGrid rightGrid;
    private UIButton closeBtn;
    private UILabel lastServerArea;
    private UILabel lastServerName;
    private UILabel lastLevel;
    private UISprite lastLoad;
    private Animator popupAnimator;
    private bool isConnect = false;
    public bool isGetServerList = false;
    public bool canSwitch = false;
    public bool isDefaultServer = false;

    public Thread thread1;
    public Thread thread2;
	public bool isSetLastServer = false;

	private bool _isInput = false;
	private bool _isLogin = false;
    public bool IsLogin
    {
        set
        {
            _isLogin = value;
        }
    }

    private float _time;

    public void Initialize()
    {
        if (_ui == null)
        {
            _ui = new UILogin();
        }

        goUI = GameSystem.Instance.mClient.mUIManager.CreateUI("Prefab/GUI/UILogin");
        _ui.Initialize(goUI);
        UIEventListener.Get(_ui.ButtonOK.gameObject).onClick = onOKClick;
        UIEventListener.Get(_ui.ButtonNotice.gameObject).onClick = showAnnounce;
        UIEventListener.Get(_ui.ButtonSwitch.gameObject).onClick = OnSwitch;
        UIEventListener.Get(_ui.ButtonCancle.gameObject).onClick = OnCancle;

        //_ui.ButtonOK.GetComponent<UIButton>().enabled = false;
        //NGUITools.SetActive(_ui.ButtonOK.gameObject, false);
        //区分第一二次登录 显示不同界面
        InitLoginUI();
        
        _ui.lblVersion.text = string.Format(_ui.lblVersion.text, GlobalConst.GAME_VERSION);

        thread1 = new Thread(GameSystem.Instance.ParseCommonConfig);
        thread2 = new Thread(GameSystem.Instance.ParseConfig);

        Scheduler.Instance.AddUpdator(Update);
        _time = 1.0f;
        if( !MainPlayer.Instance.CanSwitchAccount)
        {
//            _ui.lbSwitch.gameObject.SetActive(false);
        }

        isInitialized = true;
        GameSystem.Instance.mClient.bStartGuide = true;

#if ANDROID_SDK || IOS_SDK
#else
		_ui.InputAccount.onSubmit.Add( new EventDelegate( OnSubmitNum ));
		_ui.BtnInputOk.onClick.Add(new EventDelegate(OnNameInputOK));
#endif

#if ANDROID_SDK
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("Login");
        }
#endif
    }

    public void OnSubmitNum()
	{
#if ANDROID_SDK || IOS_SDK
        if (GameSystem.Instance.mNetworkManager.m_loginConn != null)
        {
            isConnect = true;
        }
        Logger.Log("UILoginControl isConnect=" + isConnect); 
#else
		LoginNetwork.Instance.authorizationCode = _ui.InputAccount.value;
#endif
        isDefaultServer = true;
		isSetLastServer = true;
		
		if (isConnect) {
			LoginNetwork.Instance.ServerInfoReq();
		}
		else 
		{
			ServerIPAndPort serverInfo = GameSystem.Instance.ServerDataConfigData.GetServerIPAndPort();
			string serverIP = serverInfo.IP;
			int port = serverInfo.EndPoint;
			GameSystem.Instance.mNetworkManager.Getloginconn(serverIP, port);
			isConnect = true;
		}
	}

    public void InputChange()
    {
#if ANDROID_SDK || IOS_SDK
#else
        if (_ui.InputAccount.value.Length == 0)
        {
//            _ui.lblText.effectStyle = UILabel.Effect.Outline;
//            _ui.lblText.effectColor = new Color(0.67f, 0.67f, 0.67f);
		}
		LoginNetwork.Instance.authorizationCode = _ui.InputAccount.value;
#endif
		Debug.Log("UILOginControl,input change");
    }
    /// <summary>
    /// 每帧更新
    /// </summary>
    public void Update()
    {
        if (GameSystem.Instance.canLoadConfig)
        {
            GameSystem.Instance.canLoadConfig = false;

            GameSystem.Instance.LoadConfig();

            GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread1.Start();
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.thread2.Start();
        }

        _time -= Time.deltaTime;
        if (_time < 0)
        {
            Scheduler.Instance.RemoveUpdator(Update);
#if IOS_SDK || ANDROID_SDK
            LoginNetwork.Instance.DoConnectToLS();
#endif

            //_ui.ButtonOK.GetComponent<UIButton>().enabled = true;
            //NGUITools.SetActive(_ui.ButtonOK.gameObject, true);

        }
    }

    public void ShowUIForm(uint param = 0)
    {
		GameSystem.Instance.mClient.pause = false;
        if (isInitialized == false)
        {
            Initialize();
        }
        if (goUI)
        {
            UIForm form = goUI.GetComponent<UIForm>();
            GameSystem.Instance.mClient.mUIManager.ShowUIForm(form, UIForm.ShowHideDirection.none);
        }
    }

    public void InitLoginUI()
    {
        if (_ui == null)
            return;
		string account_name = "";
        if (LoginIDManager.GetFirstLoginState()) //第一次登录
        {
        }
        else //第二次登录
        {
			account_name = LoginIDManager.GetAccount();
#if IOS_SDK || ANDROID_SDK
            string userName = LoginIDManager.GetUserName();
//            if (userName == "")
//            {
                //userName = CommonFunction.GetConstString("STR_TOURIST");
//                userName = _ui.lblText.text;
//            }
            //            _ui.lblText.text = userName;
            SetAccountName(userName);

#else
		SetAccountName(account_name);
#endif
        }
        _ui.lblServerLabel.text = LoginIDManager.GetPlatServerName();
        //_ui.goServerList.SetActive(false);
        SetDisplayServerLabel(LoginIDManager.GetPlatDisplayServerID(),LoginIDManager.GetPlatServerName());

        //UIEventListener.Get(_ui.goBg).onClick = onBgClick;
		UIEventListener.Get(_ui.lblText.gameObject).onClick = onInputClick;
        UIEventListener.Get(_ui.goServer).onClick = onChangeServer;
    }

    public void ResetLoginConnect()
    {
        isConnect = false;
    }

    public void ResetInput()
    {
        //_ui.Account.value = "";
        //_ui.Password.value = "";
    }

    public void SetNoticeActive()
    {
        _ui.ButtonNotice.gameObject.SetActive(true);
    }

    public void showAnnounce(GameObject go)
    {
        showAnnounce();
    }
    public void OnSwitch(GameObject go)
    {
        Logger.Log("1927 c# OnSwitch called");
#if IOS_SDK || ANDROID_SDK
        if( !MainPlayer.Instance.CanSwitchAccount)
        {
            return;
        }
#endif

#if IOS_SDK
        switchAccount();
#endif

#if ANDROID_SDK
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("SwitchAccount");
            canSwitch = false;
        }
#endif
        
    }

    public void OnCancle(GameObject go)
    {
		_isInput = false;
        Logger.Log("1927 c# OnCancle is called");
#if ANDROID_SDK
       AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
       AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
       jo.Call("Logout");
       Logger.Log("1927 c# called Logout");
#endif

       _ui.ButtonSwitch.gameObject.SetActive(false);
       _ui.ButtonCancle.gameObject.SetActive(false);

       LoginIDManager.SetUserName("");
       LoginIDManager.SetAccount("");
//       _ui.lblText.text = "";
		SetAccountName("");
       MainPlayer.Instance.SDKLogin = false;
       LoginNetwork.Instance.isVerifySDK = true;
	   OnSwitch(go);
    }
    void showAnnounce()
    {
        GameObject popup_message_prefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/NoticePopup") as GameObject;
        GameObject popup_message_obj = CommonFunction.InstantiateObject(popup_message_prefab, UIManager.Instance.m_uiRootBasePanel.transform);
        Vector3 position = popup_message_obj.transform.localPosition;
        position.z = -500;
        popup_message_obj.transform.localPosition = position;
    }

    public void onOKClick(GameObject go)
    {
#if ANDROID_SDK
        if( !MainPlayer.Instance.SDKLogin)
        {
			if(_isInput)
				_isInput = false;
            onInputClick(go);
            return;
        }
#endif
        onOKClick();
    }

    void onOKClick()
    {
        //if (!isLoadConfig)
        //{
        //    Logger.LogWarning("Config is not Loading finish. return click action");
        //    return;
        //}
          
        if (Account == "")
        {
            SetErrorTips(CommonFunction.GetConstString("LOGIN_TIPS"));
            return;
        }
        if (_ui.lblServerLabel.text == "")
        {
            SetErrorTips(CommonFunction.GetConstString("LOGIN_SEVER_QUEST"));
            return;
        }
        if (GameSystem.Instance.mNetworkManager.m_loginConn == null)
            isConnect = false;

#if IOS_SDK || ANDROID_SDK
        if (GameSystem.Instance.mNetworkManager.m_loginConn != null)
        {
            isConnect = true;
        }
#endif

        bool verifyOk = true;
        if (isConnect)
        {
            verifyOk = LoginNetwork.Instance.VerifyCDKeyReq();
        }
        else
        {
            ConnectLoginServer();
        }

        if( verifyOk )
        {
            GameSystem.Instance.mNetworkManager.isReconnecting = false;
			_isLogin = true;
//            _ui.goServer.SetActive(false);
            _ui.ButtonOK.GetComponent<UIButton>().enabled = false;
//            _ui.ButtonOK.gameObject.SetActive(false);
//            _ui.InputAccount.gameObject.SetActive(false);
        }
		UIWait.ShowWait();
    }


#if IOS_SDK
	[DllImport("__Internal")]
	private static extern void switchAccount();
#endif 

    public void onInputClick(GameObject go)
    {
		if(_isLogin)
			return;
		if(_isInput)
			return;
		_isInput = true;
        Logger.Log("onInputClick called");
        //if(!isLoadConfig)
        //{
        //    Logger.LogWarning("Config is not Loading finish. return click action");
        //    return;
        //}

#if IOS_SDK || ANDROID_SDK
        if( !MainPlayer.Instance.CanSwitchAccount)
        {
            return;
        }
#else
		if(_ui.goInput!=null)
		{
			_ui.InputAccount.value = Account;
			_ui.goInput.SetActive(true);
		}
#endif


#if IOS_SDK
        switchAccount();
#endif

#if ANDROID_SDK
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("SwitchAccount");
            canSwitch = false;
        }
#endif
    }

    public void Uninitialize()
    {
        if (_ui != null)
        {
            _ui.Uninitialize();
            _ui = null;
        }
        isInitialized = false;
    }

    public void onChangeServer(GameObject go)
    {
		if(_isLogin)
			return;
        //if (!isLoadConfig)
        //{
        //    Logger.LogWarning("Config is not Loading finish. return click action");
        //    return;
        //}
#if ANDROID_SDK
        if( !MainPlayer.Instance.SDKLogin)
        {
            onInputClick(go);
            return;
        }
#endif 

        if (_ui.lblText.text == "")
        {
            SetErrorTips(CommonFunction.GetConstString("LOGIN_TIPS"));
            return;
        }

        if (GameSystem.Instance.mNetworkManager.m_loginConn == null)
            isConnect = false;

        if (isConnect)
        {
            LoginNetwork.Instance.ServerInfoReq();
        }
        isGetServerList = true;
        ConnectLoginServer();
		UIWait.ShowWait();
    }

    public void DisplayServerList(List<ServerListInfo> serverList)
    {
        if (loginServerPopup != null || goUI == null)
        {
            //NGUITools.SetActive(loginServerPopup.gameObject, true);
            return;
        }

        loginServerPopup = CommonFunction.InstantiateObject("Prefab/GUI/LoginServerPopup", goUI.transform);
        popupAnimator = loginServerPopup.GetComponent<Animator>();
        leftGrid = loginServerPopup.transform.FindChild("Window/Left/Scroll/Grid").GetComponent<UIGrid>();
        rightGrid = loginServerPopup.transform.FindChild("Window/Right/Scroll/Grid").GetComponent<UIGrid>();
        lastServerArea = loginServerPopup.transform.FindChild("Window/Right/Up/Server").GetComponent<UILabel>();
        lastServerName = loginServerPopup.transform.FindChild("Window/Right/Up/Name").GetComponent<UILabel>();
        lastLevel = loginServerPopup.transform.FindChild("Window/Right/Up/Lever").GetComponent<UILabel>();
        lastLoad = loginServerPopup.transform.FindChild("Window/Right/Up/Icon").GetComponent<UISprite>();
        closeBtn = loginServerPopup.transform.FindChild("Window/ButtonClose/ButtonClose").GetComponent<UIButton>();
        UIEventListener.Get(closeBtn.gameObject).onClick = (go)
            =>
            {
                popupAnimator.enabled = true;
                popupAnimator.SetBool("Close", true);
            };
        SetLastLoginInfo(serverList);

        eachGroupList.Clear();
        int serverCount = serverList.Count;
        int nameIndex = (serverCount / 4) * 4 <= serverCount ? serverCount / 4 + 1 : serverCount / 4;
        int itemIndex = 0;
        bool isShowIcon = false;

        do
        {
            List<ServerListInfo> servers = new List<ServerListInfo>();
            int count = serverCount % 4;
            if (count > 0)
            {
                for (int i = serverCount - 1; i >= serverCount - count; i--)
                {
                    servers.Add(serverList[i]);
                    if (serverList[i].name != null && serverList[i].name != "")
                        isShowIcon = true;
                }
                servers.Reverse();
                serverCount = serverCount - count;
            }
            else if (count == 0 && serverCount > 0)
            {
                for (int i = serverCount - 1; i >= serverCount - 4; i--)
                {
                    servers.Add(serverList[i]);
                    if (serverList[i].name != null && serverList[i].name != "")
                        isShowIcon = true;
                }
                servers.Reverse();
                serverCount = serverCount - 4;
            }

            eachGroupList[itemIndex] = servers;
            GameObject serverBtn = CommonFunction.InstantiateObject("Prefab/GUI/ServerButton", leftGrid.transform);
            MultiLabel title = serverBtn.transform.FindChild("Title").GetComponent<MultiLabel>();
            GameObject icon = serverBtn.transform.FindChild("Icon").gameObject;
            serverBtn.transform.name = nameIndex.ToString();
            int max = nameIndex * 4 > serverList.Count ? serverList.Count : nameIndex * 4;
            title.SetText(string.Format(CommonFunction.GetConstString("LOGIN_SEVER_AREA"), string.Format("{0} — {1}", (nameIndex - 1) * 4 + 1, max)));
            icon.SetActive(isShowIcon);

            UIEventListener.Get(serverBtn).onClick = RefreshServerList;
            nameIndex--;
            itemIndex++;
            isShowIcon = false;
        } while (serverCount > 0);

        leftGrid.repositionNow = true;
        leftGrid.Reposition();
        leftGrid.transform.GetChild(0).GetComponent<UIToggle>().startsActive = true;
        RefreshServerList(leftGrid.transform.GetChild(0).gameObject);
    }


	public void SetAccount(string account )
	{
		Logger.Log ("UILoginControl SetAccount account =" + account);
		Account = account;
		
		LoginIDManager.SetAccount(Account);
		LoginIDManager.SetFirstLoginState(1);
	}

    public void SetUserName(string userName)
    {
		_isInput = false;
        if (_ui != null)
        {
//            _ui.lblText.text = userName;
			SetAccountName(userName);
#if ANDROID_SDK || IOS_SDK

#else
            if (_ui.InputAccount!=null)
            {
                _ui.InputAccount.value = userName;
            }
#endif
        }
        LoginIDManager.SetUserName(userName);
    }

    public void RefreshServerList(GameObject go)
    {
        GameObject childGo = null;
        for (int i = 0; i < leftGrid.transform.childCount; i++)
        {
            childGo = leftGrid.transform.GetChild(i).gameObject;
            if (go == childGo)
            {
                childGo.transform.FindChild("Sele").GetComponent<UISprite>().spriteName = "login_yellow5";
                while (rightGrid.transform.childCount > 0)
                {
                    NGUITools.Destroy(rightGrid.transform.GetChild(0).gameObject);
                }
                for (int j = 0; j < eachGroupList[i].Count; j++)
                {
                    GameObject serverItem = CommonFunction.InstantiateObject("Prefab/GUI/ServerItem", rightGrid.transform);
                    GameObject level = serverItem.transform.FindChild("Level").gameObject;
                    MultiLabel levelText = level.transform.FindChild("Text").GetComponent<MultiLabel>();
                    MultiLabel serverID = serverItem.transform.FindChild("Server").GetComponent<MultiLabel>();
                    MultiLabel serverName = serverItem.transform.FindChild("Name").GetComponent<MultiLabel>();
                    UISprite serverIcon = serverItem.transform.FindChild("Icon").GetComponent<UISprite>();
                    UISprite backShade = serverItem.transform.FindChild("BackShade").GetComponent<UISprite>();
                    serverItem.name = eachGroupList[i][j].server_name;
                    serverID.SetText(string.Format(CommonFunction.GetConstString("LOGIN_SEVER_AREA"), eachGroupList[i][j].display_server_id));
                    serverName.SetText(eachGroupList[i][j].server_name);
                    if (eachGroupList[i][j].name == null || eachGroupList[i][j].name == "")
                        NGUITools.SetActive(level, false);
                    else
                    {
                        levelText.SetText(CommonFunction.GetConstString("UI_HALL_LEVEL_1") + eachGroupList[i][j].level.ToString());
                        if (eachGroupList[i][j].server_name == LoginIDManager.GetPlatServerName())
                        {
                            NGUITools.SetActive(backShade.gameObject, true);
                            if (eachGroupList[i][j].level != LoginIDManager.GetLastLevel())
                                lastLevel.text = CommonFunction.GetConstString("UI_HALL_LEVEL_1") + eachGroupList[i][j].level.ToString();
                        }
                    }
                    uint state = eachGroupList[i][j].load;
                    if (state == (uint)ServerState.SS_BUSY)
                    {
                        serverIcon.spriteName = "login_login_Orange";
                    }
                    else if (state == (uint)ServerState.SS_CLOSE)
                    {
                        serverIcon.spriteName = "login_login_Gray";
                    }
                    else if (state == (uint)ServerState.SS_FULL)
                    {
                        serverIcon.spriteName = "login_login_Red";
                    }
                    else if (state == (uint)ServerState.SS_IDLE)
                    {
                        serverIcon.spriteName = "login_login_Green";
                    }
                    if (eachGroupList[i][j].server_name == LoginIDManager.GetPlatServerName())
                        lastLoad.spriteName = serverIcon.spriteName;

                    UIEventListener.Get(serverItem).onClick = OnChooseServer;
                }
            }
        }
        rightGrid.repositionNow = true;
        rightGrid.Reposition();
    }

    public void OnChooseServer(GameObject go)
    {
        if (go)
        {
            ServerListInfo infos;
            foreach (var item in eachGroupList.Values)
            {
                infos = item.Find((ServerListInfo info) => { return info.server_name == go.transform.name; });
                if (infos != null)
                {
                    LoginIDManager.SetPlatServerID(infos.server_id);
                    LoginIDManager.SetPlatDisplayServerID(infos.display_server_id);
                    LoginIDManager.SetPlatServerName(infos.server_name);
                    LoginIDManager.SetLastLevel(infos.level);
                    //LoginIDManager.SetServerState(infos.load);
                    _ui.lblServerLabel.text = infos.server_name;
                    //NGUITools.SetActive(loginServerPopup.gameObject, false);
                    //NGUITools.Destroy(loginServerPopup.gameObject);
                    SetDisplayServerLabel(infos.display_server_id, infos.server_name);
                    popupAnimator.enabled = true;
                    popupAnimator.SetBool("Close", true);
                }
            }
        }
    }

    public void ConnectLoginServer()
    {
        if (!isConnect)
        {

#if IOS_SDK || ANDROID_SDK

#else
//            Account = _ui.lblText.text;
            LoginIDManager.SetAccount(Account);
			LoginNetwork.Instance.authorizationCode = Account;
#endif

            LoginIDManager.SetFirstLoginState(1);
            //LoginIDManager.SetServerIP(GameSystem.Instance.serverIP);


            LoginNetwork.Instance.ConnectToLS();
            isConnect = true;
        }
    }

    public void SetLastLoginInfo(List<ServerListInfo> serverLis)
    {
        uint lastServerId = LoginIDManager.GetPlatServerID();
        string lastServer = LoginIDManager.GetPlatServerName();
        uint lastServerLevel = LoginIDManager.GetLastLevel();
        //uint lastServerLoad = LoginIDManager.GetServerState();
        Logger.Log("lastServerId = " + lastServerId);
        Logger.Log("lastServer = " + lastServer);
        Logger.Log("lastServerLevel = " + lastServerLevel);
        if (lastServerId > 0)
        {
            ServerListInfo lastInfo = serverLis.Find((ServerListInfo info) => { return info.server_id == lastServerId; });
            if( lastInfo != null )
            {
                lastServerArea.text = string.Format(CommonFunction.GetConstString("LOGIN_SEVER_AREA"), lastInfo.display_server_id );
            }
        }
        else
        {
            NGUITools.SetActive(lastServerArea.gameObject, false);
            NGUITools.SetActive(lastServerName.gameObject, false);
            NGUITools.SetActive(lastLevel.gameObject, false);
            NGUITools.SetActive(lastLoad.gameObject, false);
        }
        if (lastServer != null && lastServer != "")
            lastServerName.text = lastServer;
        if (lastServerLevel > 0)
            lastLevel.text = CommonFunction.GetConstString("UI_HALL_LEVEL_1") + lastServerLevel.ToString();

        //if (currentLoadState == (uint)ServerState.SS_BUSY)
        //{
        //    lastLoad.spriteName = "login_login_Orange";
        //}
        //else if (currentLoadState == (uint)ServerState.SS_CLOSE)
        //{
        //    lastLoad.spriteName = "login_login_Gray";
        //}
        //else if (currentLoadState == (uint)ServerState.SS_FULL)
        //{
        //    lastLoad.spriteName = "login_login_Red";
        //}
        //else if (currentLoadState == (uint)ServerState.SS_IDLE)
        //{
        //    lastLoad.spriteName = "login_login_Green";
        //}
        //else
        //    NGUITools.SetActive(lastLoad.gameObject, false);
    }

    public void SetErrorTips(string message)
    {
        _ui.lblTips.text = message;
        _ui.lblTips.gameObject.SetActive(true);
    }
	void ErrorTipsHide()
	{
		_ui.lblTips.gameObject.SetActive(false);
	}
	public void SetDefaultServer(List<ServerListInfo> infos, uint last_server_id)
	{
		Logger.Log("DefaultServer -------------");
		if (infos == null)
			Logger.LogError("info is null");
		if (infos.Count <= 0)
			return;
		
		ServerListInfo defaultServer = null;
		ServerListInfo curServer = null;
		foreach (var server in infos)
		{
			Logger.Log("Server name:" + server.name + " default:" + server.default_server);
			if (server.server_id == last_server_id)
			{
				curServer = server;
				break;
			}
			if (server.default_server == 1)
			{
				defaultServer = server;
			}
		}
		if (curServer == null)
		{
			curServer = defaultServer;
		}
		LoginIDManager.SetPlatServerID(curServer.server_id);
		LoginIDManager.SetPlatDisplayServerID(curServer.display_server_id);
		LoginIDManager.SetPlatServerName(curServer.server_name);
		LoginIDManager.SetLastLevel(curServer.level);
		_ui.lblServerLabel.text = curServer.server_name;
        SetDisplayServerLabel(curServer.display_server_id, curServer.server_name);
	}
	void OnNameInputOK()
	{
		_isInput = false;
#if IOS_SDK || ANDROID_SDK
#else
		Account = _ui.InputAccount.value;
		LoginIDManager.SetAccount(Account);
		LoginNetwork.Instance.authorizationCode = Account;
		SetAccountName(Account);
		_ui.goInput.SetActive(false);
#endif
	}


    private void SetDisplayServerLabel(uint displayServerId, string serverName)
    {
        if( string.IsNullOrEmpty(serverName))
        {
            return; 
        }

        // Avoid init ConstString failed.
        string zoneStr = "{0} 区";
        if( GameSystem.Instance.ConstStringConfigData != null)
        {
            zoneStr = CommonFunction.GetConstString("LOGIN_SEVER_AREA");

        }
        string s = string.Format(
           zoneStr, 
             displayServerId)
            + "  "
            + serverName;
        _ui.lblServerLabel.text = s;
    }
	void SetAccountName(string name)
	{
		if(name.Length == 0)
		{
			//show login
			_ui.lblText.text = "未登录帐号，[u][00ff00]立即登录[-][/u]";//CommonFunction.GetConstString("LOGIN_TIPS");
			Account = "";
		}
		else{
			Account = name;
			_ui.lblText.text = "欢迎您，"+"[u]"+"[00ff00]"+ name+"[-]"+"[/u]";
			//			_ui.lblText.text = "[u]"+_ui.lblText.text+"[/u]";
		}
	}
    public void OnLoginFailed()
    {
        _isLogin = false;
        _isInput = false;
        UIButton btnScprit = null;

        if (_ui != null)
            btnScprit = _ui.ButtonOK.GetComponent<UIButton>();
        if (btnScprit != null)
            btnScprit.enabled = true;
        UIWait.StopWait();
    }
}

