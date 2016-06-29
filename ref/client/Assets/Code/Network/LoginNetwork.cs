
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;


public class LoginNetwork : Singleton<LoginNetwork>
{
    public string authorizationCode;
    public string gameVer = "0.1";
    public string resVer = "0.1";

	private string cdKey;
	private string tokenId;
	private string nickName;
	private string userName;
	private string serverId;
    public bool isVerifySDK = false;


    //连接LoginServer
    public void ConnectToLS()
    {
        if (GlobalConst.IS_NETWORKING)
        {
            ServerIPAndPort serverInfo = GameSystem.Instance.ServerDataConfigData.GetServerIPAndPort();
            string serverIP = serverInfo.IP;
            int port = serverInfo.EndPoint;
            //GameSystem.Instance.mClient.mUIManager.DisplayError("onChangeServer------6 " + serverIP);

            //GameSystem.Instance.mClient.mUIManager.DisplayError("onChangeServer------7 " + port);

            Debug.Log("--- Connecting to LoginServer: " + serverIP + " -- " + port);

            GameSystem.Instance.mNetworkManager.ConnectToLS(serverIP, port);
        }
        else
        {
            //TODO: add process here.
        }
    }


	public void HandleSDKInfo(string str, char splitChar,bool isRefresh)
	{
        Debug.Log("HandleSDKInfo isRefresh =" + isRefresh);
        UIManager.Instance.LoginCtrl.canSwitch = true;
        string[] items = str.Split(splitChar);
        SetSDKInfo(items[0], items[1], items[2], items[3], items[4], isRefresh);
#if ANDROID_SDK || IOS_SDK
        Debug.Log("HanldeSDKInfo - 1A");
        MainPlayer.Instance.CanGoCenter = items[5].Equals("1");
        MainPlayer.Instance.CanSwitchAccount = items[6].Equals("1");
        MainPlayer.Instance.CanLogout = items[7].Equals("1");

        Debug.Log("HandlerSDKInfo + CanGoCenter=" + MainPlayer.Instance.CanGoCenter);
        Debug.Log("HandlerSDKInfo + CanSwitchAccount=" + MainPlayer.Instance.CanSwitchAccount);
        Debug.Log("HandlerSDKInfo + CanLogout=" + MainPlayer.Instance.CanLogout);
        if( !MainPlayer.Instance.CanGoCenter )
        {
            UIManager.Instance.LoginCtrl._ui.lbSwitch.gameObject.SetActive(false);
        }

        if( MainPlayer.Instance.CanSwitchAccount )
        {
            Debug.Log("1927 - Set Login Switch gameObject true");
            UIManager.Instance.LoginCtrl._ui.ButtonSwitch.gameObject.SetActive(true);
        }

        if( MainPlayer.Instance.CanLogout)
        {
            Debug.Log("1927 - Set Login Logout gameObject true");
            UIManager.Instance.LoginCtrl._ui.ButtonCancle.gameObject.SetActive(true);
        }
#endif

	}

    // Set the Values need for SDK.
    public void SetSDKInfo(string cdKeySDK,
        string tokenIdSDK,
        string nickNameSDK,
        string userNameSDK, 
        string serverIdSDK,
        bool isRefresh)
    {
        cdKey = cdKeySDK;
        tokenId = tokenIdSDK;
        nickName = nickNameSDK;
        userName = userNameSDK;
        serverId = serverIdSDK;

#if IOS_SDK || ANDROID_SDK
        Debug.Log("SetSDKInfo called, MainPlayer.Instance.SDkLogin = " + MainPlayer.Instance.SDKLogin);
        //if (!MainPlayer.Instance.SDKLogin)
        {
            Debug.Log("SetSDK userName=" + userName);
            if (userName == null || userName.Equals(""))
            {
                Debug.Log("GameSystem.Instance.ConstStringConfigData" + GameSystem.Instance.ConstStringConfigData);
                if (GameSystem.Instance.ConstStringConfigData != null)
                {
                    Debug.Log("SetSDK --- 1");
                    userName = CommonFunction.GetConstString("STR_TOURIST");
                    Debug.Log("SetSDK --- 2");
                    if (string.Compare(userName, "STR_TOURIST") == 0)
                    {
                        // 当资源没有加载的时候,强行显示游客.
                        Debug.Log("SetSDK --- 3");
                        userName = "游客";
                    }
                }
                else
                {
                    Debug.Log("SetSDK --- 4");
                    userName = "游客";
                }
                MainPlayer.Instance.Tourist = true;
            }

            MainPlayer.Instance.ZQServerId = serverId;
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetAccount(cdKey);
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetUserName(userName);
            MainPlayer.Instance.SDKLogin = true;

            Debug.Log("SetSDKInfo MainPlayer.Instance.SDkLogin +" + MainPlayer.Instance.SDKLogin);
            Debug.Log("SetSDKInfo LoginNetwork.Instance.isVerifySDK =" + LoginNetwork.Instance.isVerifySDK);
            Debug.Log("GameSystem.Instance.mNetworkManager.m_loginConn = " + GameSystem.Instance.mNetworkManager.m_loginConn);
            if (GameSystem.Instance.mNetworkManager.m_loginConn != null && !isVerifySDK)
            {
                Debug.Log("VerifySDK in SetSDKInfo");
                isVerifySDK = true;
                VerifySDKReq();
            }

        }

#endif
    }

	public void DoConnectToLS()
	{
        Debug.Log("1927 - LoginNetwork DOConnectToLS is called");
		if (userName == null || userName.Equals ("")) 
        {
            //userName = CommonFunction.GetConstString("STR_TOURIST");
            //if (string.Compare(userName, "STR_TOURIST") == 0)
            //{
            //    // 当资源没有加载的时候,强行显示游客.
            //    userName = "游客";
            //}
            userName = "";
            MainPlayer.Instance.Tourist = true;
		}
		MainPlayer.Instance.ZQServerId = serverId;
		GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetAccount (cdKey);
		GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetUserName (userName);

		ConnectToLS ();
	}

	public void VerifySDKReq()
	{
		VerifySdk verifySdk = new VerifySdk();
		
		verifySdk.cdkey = cdKey;
		verifySdk.token_id = tokenId;
		verifySdk.nick_name = nickName;
		verifySdk.user_name = userName;
		Debug.Log("-------------SDK response verifySdk.cdkey ="+ verifySdk.cdkey );
		Debug.Log("-------------SDK response verifySdk.token_id ="+ verifySdk.token_id );
		Debug.Log("-------------SDK response verifySdk.nick_name ="+ verifySdk.nick_name );
		Debug.Log("-------------SDK response verifySdk.user_name ="+ verifySdk.user_name );

        Debug.Log("GameSystem.Instance.mNetworkManager.m_loginConn = " + GameSystem.Instance.mNetworkManager.m_loginConn);
		GameSystem.Instance.mNetworkManager.m_loginConn.SendPack<VerifySdk>(0, verifySdk, MsgID.VerifySdkID);
	}


    //CDKey验证请求
    public bool VerifyCDKeyReq()
    {
        Debug.Log("-------------VerifyCDKeyReq");

        uint ID = LoginIDManager.GetPlatServerID();
        VerifyCDKey verifyCDKey = new VerifyCDKey();

#if IOS_SDK || ANDROID_SDK
        if( string.IsNullOrEmpty(cdKey) )
        {
            // 如果cdKey 为null时候，不处理
            return false;
        }
        verifyCDKey.authorization_code = cdKey;
#else
        verifyCDKey.authorization_code = authorizationCode;
#endif
        verifyCDKey.server_id = ID;
        verifyCDKey.game_ver = GlobalConst.GAME_VERSION;
        verifyCDKey.res_ver = GlobalConst.RES_VERSION;
        verifyCDKey.mac_add = SystemInfo.deviceUniqueIdentifier;  
        GameSystem.Instance.mNetworkManager.m_loginConn.SendPack<VerifyCDKey>(0, verifyCDKey, MsgID.VerifyCDKeyID);

        return true;
    }

    //ServerList请求
    public void ServerInfoReq() 
    {
        Debug.Log("-------------ServerInfoReq");

        ServerInfoReq serverInfo = new ServerInfoReq();

		if (authorizationCode == null || authorizationCode.Equals ("")) 
		{
			serverInfo.cdkey = cdKey;
		}
		else 
		{
			serverInfo.cdkey = authorizationCode;
		}
        if (serverInfo.cdkey == null || serverInfo.cdkey == "")
        {
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetErrorTips(CommonFunction.GetConstString("LOGIN_TIPS"));
            return;
        }
        serverInfo.mac_add = SystemInfo.deviceUniqueIdentifier;
		Debug.Log("-------------ServerInfoReq serverInfo.cdkey =" + serverInfo.cdkey );
        GameSystem.Instance.mNetworkManager.m_loginConn.SendPack<ServerInfoReq>(0, serverInfo, MsgID.ServerInfoReqID);
    }
}