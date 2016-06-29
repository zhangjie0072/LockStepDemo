using LuaInterface;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
public class NetworkManager : NetworkConn.Listener
{
	public NetworkConn 		m_loginConn;
	public NetworkConn 		m_gameConn;
	public NetworkConn 		m_platConn;
	public LoginMsgHandler m_loginMsgHandler;
	public PlatMsgHandler m_platMsgHandler;
	public GameMsgHandler m_gameMsgHandler;

	public delegate	void OnServerConnected(NetworkConn.Type serverType);
	public OnServerConnected	onServerConnected;

	public double			m_dServerTime;
	public double			m_dLocalTime;

    public bool connLogin = false;
	public bool connPlat = false;
    public bool connGS	 = false;

    long connFailTime;
	bool autoReconn = false;

    //public bool mTimeoutShowing = false;
	public bool autoReconnInMatch;
    public bool isReconnecting;
	public bool isPushedOut;
	public bool isWaittingReLogin;
	public bool isKickOut;
    private bool isDisplayReconnect = false;

	public NetworkManager()
	{
		m_loginMsgHandler = new LoginMsgHandler();
		m_platMsgHandler = new PlatMsgHandler();
		m_gameMsgHandler = new GameMsgHandler();
	}

	public void Getloginconn(string ip, int port)
	{
		if (m_loginConn == null)
		{
			Debug.Log("Get login conn.");
			m_loginConn = new NetworkConn(m_loginMsgHandler, "Login", NetworkConn.Type.eLoginServer, 5);
			m_loginConn.AddEventListener(this);
			LoginNetwork.Instance.ConnectToLS();
		}
	}

	public void ConnectToLS(string ip, int port)
	{
		if( m_loginConn != null )
			CloseLoginConn();

        if (m_loginConn == null)
		{
			Debug.Log("New login conn.");
            m_loginConn = new NetworkConn(m_loginMsgHandler, "Login", NetworkConn.Type.eLoginServer, 6);
            m_loginConn.AddEventListener(this);
		}
        m_loginConn.Connect(ip, port);
		m_loginConn.EnableTimeout(false);
	}
	
	public void ConnectToPS(string ip, int port)
	{
		if( m_platConn != null )
			ClosePlatConn();

        if (m_platConn == null)
		{
			/*
#if UNITY_EDITOR
			int timeout = 0;
#else
			int timeout = 5000;
#endif
*/
			int timeout = 6;
			m_platConn = new NetworkConn(m_platMsgHandler, "Plat", NetworkConn.Type.ePlatformServer, timeout);
			CheatingDeath.Instance.mAntiSpeedUp.SetWatchTarget(m_platConn);
            m_platConn.AddEventListener(this);
		}
        m_platConn.Connect(ip, port);
	}

	public void ConnectToGS( GameMatch.Type uMatchType, string ip, int port)
	{
		if( m_gameConn != null )
			CloseGameServerConn();

		if( uMatchType == GameMatch.Type.ePVP_1PLUS
		   || uMatchType == GameMatch.Type.ePVP_3On3  ) 
		{
			if (m_gameConn == null)
			{
				/*
#if UNITY_EDITOR
			int timeout = 0;
#else
			int timeout = 5000;
#endif
*/
				int timeout = 6;
				m_gameConn = new NetworkConn(m_gameMsgHandler, "Game", NetworkConn.Type.eGameServer, timeout);
				m_gameConn.AddEventListener(this);
			}
		}
		else
		{
			m_gameConn = new VirtualNetworkConnSimple(m_gameMsgHandler);
			m_gameConn.AddEventListener(this);
            VirtualGameServer.Instance = new VirtualGameServer();
		}
		m_gameConn.Connect(ip, port);
	}

    public void CloseLoginConn()
    {
		Debug.Log("Close login conn.");
        if (m_loginConn != null)
            m_loginConn.Close();

        m_loginConn = null;
    }

    public void ClosePlatConn()
    {
        if (m_platConn != null)
            m_platConn.Close();

        m_platConn = null;
    }

	public void CloseGameServerConn()
	{
		if (m_gameConn != null)
			m_gameConn.Close();
		
		m_gameConn = null;
	}

	public void Exit()
	{
		if (m_loginConn != null)
		{
			m_loginConn.Close();
			m_loginConn = null;
			connLogin = false;
		}
		if (m_gameConn != null && m_gameConn.m_type != NetworkConn.Type.eVirtualServer)
		{
			m_gameConn.Close();
			m_gameConn = null;
			connGS = false;
		}
		if (m_platConn != null)
		{
			m_platConn.Close();
			m_platConn = null;
			connPlat = false;
		}
	}

	public void FixedUpdate(float fDeltaTime)
	{
	}

	public void Update(float deltaTime)
	{
		/*
		if (mTimeoutShowing && PlatNetwork.Instance.IsEnterPlatTimeout())
		{
			ReconnectPrompt(CommonFunction.GetConstString("NETWORK_UNSTABLE_RETRY_LATER"));
			mTimeoutShowing = false;
		}
		*/

		if (autoReconn && (DateTime.Now.Ticks - connFailTime) > 30000000)
		{
			autoReconn = false;
			if (CanAutoReconn())
				Reconnect();
		}

        if (m_loginConn != null)
			m_loginConn.Update(deltaTime);

        if (m_platConn != null)
			m_platConn.Update(deltaTime);

        if (m_gameConn != null)
			m_gameConn.Update(deltaTime);
	}

    public void OnEvent(NetworkConn.NetworkEvent nEvent, NetworkConn sender)
    {
		Debug.Log("Network Event -- sender.m_type: " + sender.m_type + " nEvent: " + nEvent);

        switch (sender.m_type)
        {
            case NetworkConn.Type.eLoginServer:
                {
                    if (nEvent == NetworkConn.NetworkEvent.disconnected)
                    {
						if (connLogin)
						{
							connLogin = false;					
						}
						UIWait.StopWait();
                    }
			        else if (nEvent == NetworkConn.NetworkEvent.connected)
                    {
                        if (connLogin == false)
                        {
                            connLogin = true;
							if( onServerConnected != null )
								onServerConnected(NetworkConn.Type.eLoginServer);
#if IOS_SDK || ANDROID_SDK
							Debug.Log("IsReconnecting: " + isReconnecting);
                            Debug.Log("MainPlayer.Instance.SDkLogin +" + MainPlayer.Instance.SDKLogin);
                            Debug.Log("LoginNetwork.Instance.isVerifySDK =" + LoginNetwork.Instance.isVerifySDK );
							if (isReconnecting)
								LoginNetwork.Instance.VerifyCDKeyReq();
                            else if( MainPlayer.Instance.SDKLogin && !LoginNetwork.Instance.isVerifySDK )
                            {
                                Debug.Log("VerifySDK in NetworkManger");
                                LoginNetwork.Instance.isVerifySDK = true;
                                LoginNetwork.Instance.VerifySDKReq();
                            }
#else
							if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.isGetServerList || LoginIDManager.GetPlatServerID() == 0)
							{
								LoginNetwork.Instance.ServerInfoReq();
								GameSystem.Instance.mClient.mUIManager.LoginCtrl.isGetServerList = false;
							}
							else if( GameSystem.Instance.mClient.mUIManager.LoginCtrl.isSetLastServer )
							{
								LoginNetwork.Instance.ServerInfoReq();
								GameSystem.Instance.mClient.mUIManager.LoginCtrl.isSetLastServer = false;
							}
							else
							{
								LoginNetwork.Instance.VerifyCDKeyReq();
							}
#endif
                        }
                    }
                    else if (nEvent == NetworkConn.NetworkEvent.connectFail)
                    {
						connLogin = false;
						OnConnectFailed(sender);			
						if(GameSystem.Instance.mClient.mUIManager.LoginCtrl!=null)
						{
							GameSystem.Instance.mClient.mUIManager.LoginCtrl.OnLoginFailed();
						}	
                    }
                }
                break;
            case NetworkConn.Type.ePlatformServer:
                {
					UIWait.StopWait();
					Debug.Log("connPlat:" + connPlat);
                    if (nEvent == NetworkConn.NetworkEvent.disconnected)
                    {
                        if (connPlat)
                        {
                            connPlat = false;
							OnDisconnected(sender);
							if (PlatNetwork.Instance.onDisconnected != null)
								PlatNetwork.Instance.onDisconnected();
                        }
                    }
                    else if (nEvent == NetworkConn.NetworkEvent.connected)
                    {
                        if (connPlat == false)
                        {
                            connPlat = true;
							if( onServerConnected != null )
								onServerConnected(NetworkConn.Type.ePlatformServer);
                            PlatNetwork.Instance.EnterPlatReq();
                        }
                        else
                        {
                            Debug.Log("Warning: Platform server already connected.");
                        }
                    }
                    else if (nEvent == NetworkConn.NetworkEvent.connectFail)
                    {
						connPlat = false;
						OnConnectFailed(sender);
                    }
                }
                break;

			case NetworkConn.Type.eVirtualServer:
			case NetworkConn.Type.eGameServer:
                {
					UIWait.StopWait();
					Debug.Log("connGS:" + connGS);
					if (nEvent == NetworkConn.NetworkEvent.disconnected)
					{
						if (connGS)
						{
							connGS = false;
						}
					}
					else if (nEvent == NetworkConn.NetworkEvent.connected)
					{
						if (connGS == false)
						{
							connGS = true;
							if( onServerConnected != null )
								onServerConnected(NetworkConn.Type.eGameServer);
						}
						else
						{
							Debug.Log("Warning: GameServer server already connected.");
						}
					}
					else if (nEvent == NetworkConn.NetworkEvent.connectFail)
					{
						connGS = false;
					}
				}
		        break;
        }
    }

	void OnDisconnected(NetworkConn conn)
	{
		if (CanAutoReconn())
			Reconnect();
	}

	void OnConnectFailed(NetworkConn conn)
	{
		Debug.Log("OnConnectFailed, isReconnecting:" + isReconnecting);
		if (isReconnecting)
		{
			if (CanAutoReconn())
			{
				autoReconn = true;
				connFailTime = DateTime.Now.Ticks;
				Debug.Log("connFailTime:" + connFailTime);
			}
		}
		else if (CanAutoReconn())
			Reconnect();
	}

	public bool CanAutoReconn()
	{
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if (!autoReconnInMatch && match != null &&
			match.leagueType != GameMatch.LeagueType.eRegular1V1 &&
			match.leagueType != GameMatch.LeagueType.eQualifyingNew &&
			match.leagueType != GameMatch.LeagueType.ePVP &&
			match.leagueType != GameMatch.LeagueType.ePractise &&
            match.leagueType != GameMatch.LeagueType.ePractise1vs1 &&
            match.m_stateMachine.m_curState!=null &&
            match.m_stateMachine.m_curState.m_eState != MatchState.State.eOver)
			return false;

        NetworkConn game = GameSystem.Instance.mNetworkManager.m_gameConn;

        if (game != null 
            && game.m_type == NetworkConn.Type.eVirtualServer
            && match != null
            && match.m_stateMachine.m_curState != null
            && match.m_stateMachine.m_curState.m_eState != MatchState.State.eOver
            )
        {
            return false;
        }
        
		Debug.Log("CanAutoReconn, " + isPushedOut + isWaittingReLogin + isKickOut);
        return !isPushedOut && !isWaittingReLogin && !isKickOut; 
	}

	public bool InNormalState()
	{
		Debug.Log("InNormalState, " + isReconnecting + isPushedOut + isKickOut + isWaittingReLogin + autoReconn);
		return !isReconnecting && !isPushedOut && !isKickOut && !isWaittingReLogin && !autoReconn;
	}

	public void StopAutoReconn()
	{
		autoReconn = false;
		NetLoading.Instance.play = false;
		Exit();
	}

	public void ReconnectPrompt(string tip)
	{
		Debug.Log("ReconnectPrompt, tip:" + tip);
		autoReconn = false;
		isReconnecting = false;
        GameSystem.Instance.mClient.pause = true;
        if( !isDisplayReconnect )
        {
            LuaComponent luaCom = CommonFunction.ShowPopupMsg(tip, null, Reconnect, ReturnToLogin,
                CommonFunction.GetConstString("BUTTON_RECONN"), "");
            luaCom.GetComponent<UIPanel>().depth = 20000;	// Cover guide panel
            luaCom.tag = "CrossScene";
        }
        isDisplayReconnect = true; 
	}

	public void DisconnectPrompt(string tip)
	{
		Debug.Log("DisconnectPrompt, tip:" + tip);
		autoReconn = false;
		isReconnecting = false;
        GameSystem.Instance.mClient.pause = true;
		LuaComponent luaCom = CommonFunction.ShowPopupMsg(tip, null, ReturnToLogin);
		luaCom.GetComponent<UIPanel>().depth = 20000;	// Cover guide panel
		luaCom.tag = "CrossScene";
	}

    public void Reconnect(GameObject go = null)
    {
        if( go != null )
        {
            isDisplayReconnect = false;
        }
		Debug.Log("Reconnect to LoginServer");
		if (isPushedOut)
		{
			NetLoading.Instance.isReLogin = true;
			NetLoading.Instance.onTimeOut = null;
		}
		else
		{
			NetLoading.Instance.isReLogin = false;
			NetLoading.Instance.onTimeOut = () => ReconnectPrompt(CommonFunction.GetConstString("NETWORK_UNSTABLE_RETRY_LATER"));
		}
        NetLoading.Instance.play = true;

		//GuideSystem.Instance.ModuleClear();

        isReconnecting = true;
		isPushedOut = false;
		isKickOut = false;
		Exit();
        LoginNetwork.Instance.ConnectToLS();
    }

    public void ReturnToLogin(GameObject go = null)
    {
        if( go != null )
        {
            isDisplayReconnect = false;
        }
        GameSystem.Instance.mClient.pause = false;
        isReconnecting = false;
		isPushedOut = false;
		isKickOut = false;

        GameSystem.Instance.mClient.Reset();
		Exit();

		LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
		tScene.Set("targetUI", null);
		tScene.Set("subID", null);
		tScene.Set("params", null);
		SceneManager.Instance.ChangeScene(GlobalConst.SCENE_STARTUP);
    }



    public void ForceToLogin()
    {
#if ANDROID_SDK
       AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
       AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
       jo.Call("Logout");
#endif
        isDisplayReconnect = false;
        GameSystem.Instance.mClient.pause = false;
        isReconnecting = false;
		isPushedOut = false;
		isKickOut = true;

        GameSystem.Instance.mClient.Reset();
		Exit();

		LuaTable tScene = LuaScriptMgr.Instance.GetLuaTable("Scene");
		tScene.Set("targetUI", null);
		tScene.Set("subID", null);
		tScene.Set("params", null);
		SceneManager.Instance.ChangeScene(GlobalConst.SCENE_STARTUP);

    }

	public void OnApplicationPause(bool pause)
	{
		if (m_loginConn != null)
			m_loginConn.EnableTimeout(!pause);
		if (m_gameConn != null && m_gameConn.m_type != NetworkConn.Type.eVirtualServer)
			m_gameConn.EnableTimeout(!pause);
		if (m_platConn != null)
			m_platConn.EnableTimeout(!pause);
	}
}
