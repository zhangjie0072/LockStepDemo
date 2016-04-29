using System;
using UnityEngine;

public class NetworkManager : Singleton<NetworkManager>, NetworkConn.Listener
{
	public NetworkConn 		m_loginConn;
	public NetworkConn 		m_gameConn;
	public NetworkConn 		m_platConn;

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

    public GameMsgHandler m_gameMsgHandler { get; private set; }

	public NetworkManager()
	{
		m_gameMsgHandler = new GameMsgHandler();
	}

    public void ConnectToGS(string ip, int port)
    {
        if (m_gameConn != null)
            CloseGameServerConn();

        if (m_gameConn == null)
        {
            m_gameConn = new NetworkConn(m_gameMsgHandler, "Game", NetworkConn.Type.eGameServer, 0);
            m_gameConn.AddEventListener(this);
        }
        m_gameConn.Connect(ip, port);
    }

	public void CloseGameServerConn()
	{
		if (m_gameConn != null)
			m_gameConn.Close();
		
		m_gameConn = null;
	}

	public void FixedUpdate(float fDeltaTime)
	{
        if (m_loginConn != null)
			m_loginConn.Update(fDeltaTime);

        if (m_platConn != null)
			m_platConn.Update(fDeltaTime);

        if (m_gameConn != null)
			m_gameConn.Update(fDeltaTime);

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

	public void Update()
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
	}

    public void OnEvent(NetworkConn.NetworkEvent nEvent, NetworkConn sender)
    {
		Logger.Log("Network Event -- sender.m_type: " + sender.m_type + " nEvent: " + nEvent);

        switch (sender.m_type)
        {
            case NetworkConn.Type.eLoginServer:
                {
					Logger.Log("connLogin:" + connLogin);
                    if (nEvent == NetworkConn.NetworkEvent.disconnected)
                    {
						if (connLogin)
						{
							connLogin = false;
						}
                    }
			        else if (nEvent == NetworkConn.NetworkEvent.connected)
                    {
                        if (connLogin == false)
                        {
                            connLogin = true;
							if( onServerConnected != null )
								onServerConnected(NetworkConn.Type.eLoginServer);
                        }
                    }
                    else if (nEvent == NetworkConn.NetworkEvent.connectFail)
                    {
						connLogin = false;
						OnConnectFailed(sender);
                    }
                }
                break;
            case NetworkConn.Type.ePlatformServer:
                {
					Logger.Log("connPlat:" + connPlat);
                    if (nEvent == NetworkConn.NetworkEvent.disconnected)
                    {
                        if (connPlat)
                        {
                            connPlat = false;
							OnDisconnected(sender);
                        }
                    }
                    else if (nEvent == NetworkConn.NetworkEvent.connected)
                    {
                        if (connPlat == false)
                        {
                            connPlat = true;
							if( onServerConnected != null )
								onServerConnected(NetworkConn.Type.ePlatformServer);
                        }
                        else
                        {
                            Logger.Log("Warning: Platform server already connected.");
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
					Logger.Log("connGS:" + connGS);
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
							Logger.Log("Warning: GameServer server already connected.");
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
		Logger.Log("OnConnectFailed, isReconnecting:" + isReconnecting);
		if (isReconnecting)
		{
			if (CanAutoReconn())
			{
				autoReconn = true;
				connFailTime = DateTime.Now.Ticks;
				Logger.Log("connFailTime:" + connFailTime);
			}
		}
		else if (CanAutoReconn())
			Reconnect();
	}

	public bool CanAutoReconn()
	{
        return false;
	}

	public bool InNormalState()
	{
		Logger.Log("InNormalState, " + isReconnecting + isPushedOut + isKickOut + isWaittingReLogin + autoReconn);
		return !isReconnecting && !isPushedOut && !isKickOut && !isWaittingReLogin && !autoReconn;
	}

	public void StopAutoReconn()
	{
		autoReconn = false;
		Exit();
	}

	public void ReconnectPrompt(string tip)
	{
		Logger.Log("ReconnectPrompt, tip:" + tip);
		autoReconn = false;
		isReconnecting = false;
        GameSystem.Instance.Pause = true;
        if( !isDisplayReconnect )
        {
        }
        isDisplayReconnect = true; 
	}

	public void DisconnectPrompt(string tip)
	{
		Logger.Log("DisconnectPrompt, tip:" + tip);
		autoReconn = false;
		isReconnecting = false;
        GameSystem.Instance.Pause = true;
	}

    public void Reconnect(GameObject go = null)
    {
        if( go != null )
        {
            isDisplayReconnect = false;
        }
		Logger.Log("Reconnect to LoginServer");

		//GuideSystem.Instance.ModuleClear();

        isReconnecting = true;
		isPushedOut = false;
		isKickOut = false;
		Exit();
    }

    public void ReturnToLogin(GameObject go = null)
    {
        if( go != null )
        {
            isDisplayReconnect = false;
        }
        GameSystem.Instance.Pause = false;
        isReconnecting = false;
		isPushedOut = false;
		isKickOut = false;

		Exit();
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
