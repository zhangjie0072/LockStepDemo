using UnityEngine;
using System.IO;
using System.Collections.Generic;
using fogs.proto.msg;
using ProtoBuf;
using System;

public class LoginMsgHandler
	:MsgHandler
{
	public LoginMsgHandler()
    {
		m_strId = "ls";

		RegisterHandler(MsgID.VerifySdkRespID, VerifySdkRespHandle);
		RegisterHandler(MsgID.VerifyCDKeyRespID, VerifyCDKeyRespHandle);
        RegisterHandler(MsgID.ServerInfoRespID, ServerInfoRespHandle);
    }


	void VerifySdkRespHandle( Pack pack )
	{
		Debug.Log("---------------------VerifySdkRespHandle");
		VerifySdkResp resp = Serializer.Deserialize<VerifySdkResp>(new MemoryStream(pack.buffer));
		if (resp.result != 0)
		{
			Debug.Log("Error -- VerifySdkRespHandle returns error: " + resp.result);

            //不知道服务器为什么会发送 104错误，+return后。客户端无法获取默认的服务信息选择
			//return;
		}

		if ( GameSystem.Instance.mClient.mUIManager.LoginCtrl.isGetServerList)
		{
            LoginNetwork.Instance.ServerInfoReq();
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.isGetServerList = false;
        }
        else
        {
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.OnSubmitNum();
        }
        //else if (LoginIDManager.GetPlatServerID() == 0)
        //{
        //    GameSystem.Instance.mClient.mUIManager.LoginCtrl.isDefaultServer = true;
        //    LoginNetwork.Instance.ServerInfoReq();
        //}
	}


	//CDKeyÑéÖ¤»Ø¸´ÏûÏ¢´¦Àí
	void VerifyCDKeyRespHandle(Pack pack)
	{
		Debug.Log("---------------------VerifyCDKeyRespHandle");
		
		VerifyCDKeyResp resp = Serializer.Deserialize<VerifyCDKeyResp>(new MemoryStream(pack.buffer));
		if (resp.result != 0)
		{
			Debug.Log("Error -- VerifyCDKeyResp returns error: " + resp.result);
			GameSystem.Instance.mNetworkManager.StopAutoReconn();

            if (resp.result == (uint)ErrorID.LOGIN_SERVER_CLOSED)
            {
                CommonFunction.ShowPopupMsg(DynamicStringManager.Instance.LoginServerClosedString, null, GameSystem.Instance.mNetworkManager.ReturnToLogin);
            }
            else
            {
                CommonFunction.ShowErrorMsg((ErrorID)resp.result, null, GameSystem.Instance.mNetworkManager.ReturnToLogin);
            }
			return;
		}
		
		//¹Ø±ÕLoginNetµÄÁ¬½Ó
		GameSystem.Instance.mNetworkManager.CloseLoginConn();
		
		//¼ÇÂ¼µÇÂ¼ÐÅÏ¢
		PlatNetwork.Instance.SaveCDKeyRespResult(resp);
		
		//½¨Á¢PlatServerÁ¬½Ó
        string platIP = resp.ip;
        uint platPort = resp.port;
        if (platIP != null && platIP != "")
        {
            PlatNetwork.Instance.ConnectToPS(platIP, platPort);
            LoginIDManager.SetServerIP(platIP);
        }
	}





    void ServerInfoRespHandle(Pack pack) 
    {

        Debug.Log("---------------------ServerInfoRespHandle");
        ServerInfoResp resp = Serializer.Deserialize<ServerInfoResp>(new MemoryStream(pack.buffer));
        if (resp.result != 0) 
        {
            Debug.Log("Error -- ServerInfoResp returns error: " + resp.result);
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetErrorTips(CommonFunction.GetConstString("LOGIN_TIPS"));
            return;
        }
        if (GameSystem.Instance.mClient.mUIManager.LoginCtrl.isDefaultServer)
        {
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.SetDefaultServer(resp.info, resp.last_server_id);
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.isDefaultServer = false;
        }
        else
            GameSystem.Instance.mClient.mUIManager.LoginCtrl.DisplayServerList(resp.info);
    }
}

