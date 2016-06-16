using UnityEngine;
using System.Collections;

public class LoginIDManager {

	// Use this for initialization
    const string IsFristGuid = "FristGuid";//第一次新手引导//
    const string IsFristLogin = "FirstLoginstate";//登录的状态，第一次或非第一次//
    const string LoginAccount = "LoginAccount"; // 本地保存的账号//
    const string LoginPassword = "LoginPassword"; //本地保存的密码//
    const string ServerIP = "ServerIP"; //本地保存的服务器选择结果
    const string PlatServerID = "PlatServerID"; //本地保存的PlatServerID
    const string PlatDisplayServerID = "PlatDisplayServerID"; //本地保存的PlatDisplayServerID
    const string PlatServerName = "PlatServerName"; //本地保存的PlatServerName
    const string LastLevel = "LastLevel";   //本地保存的LastLevel
    //const string LastServerState = "ServerState";   //本地保存的上次服务器状态
    const string UserName = "UserName";   // 显示的用户名字
    const string AnnouncementVersion = "AnnouncementVersion"; //记录公告版本号
    const string IsFirstPractice_ = "IsFirstPractice_";//记录练习赛是否第一次完成

    public static void SetFirstLoginState(int value)
    {
        PlayerPrefs.SetInt(IsFristLogin, value);
    }

    public static bool GetFirstLoginState()
    {
        int value = 1 - PlayerPrefs.GetInt(IsFristLogin);
        if (value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetAccount(string value)
    {
        PlayerPrefs.SetString(LoginAccount, value);
    }

    public static string GetAccount()
    {
        return PlayerPrefs.GetString(LoginAccount);
    }
    public static void SetUserName(string value)
    {
        PlayerPrefs.SetString(UserName, value);
    }

    public static string GetUserName()
    {
        return PlayerPrefs.GetString(UserName);
    }


    public static void SetPassword(string value)
    {
        if (value != "")
            PlayerPrefs.SetString(LoginPassword, value);
    }

    public static string GetPassword()
    {
        return PlayerPrefs.GetString(LoginPassword);
    }

    public static void SetFristGuid(int value)
    {
        PlayerPrefs.SetInt(IsFristGuid, value);
    }

    public static int GetFristGuid()
    {
        return PlayerPrefs.GetInt(IsFristGuid);
    }

    public static void SetServerIP(string value)
    {
        PlayerPrefs.SetString(ServerIP, value);
    }

    public static string GetServerIP()
    {
        return PlayerPrefs.GetString(ServerIP);
    }

    public static void SetPlatDisplayServerID(uint value)
    {
        PlayerPrefs.SetInt(PlatDisplayServerID, (int)value);
    }
    public static uint GetPlatDisplayServerID()
    {
        return (uint)PlayerPrefs.GetInt(PlatDisplayServerID);
    }
    public static void SetPlatServerID(uint value)
    {
        PlayerPrefs.SetInt(PlatServerID, (int)value);
    }
    public static uint GetPlatServerID()
    {
        return (uint)PlayerPrefs.GetInt(PlatServerID);
    }

    public static void SetPlatServerName(string value)
    {
        PlayerPrefs.SetString(PlatServerName, value);
    }
    public static string GetPlatServerName()
    {
        return PlayerPrefs.GetString(PlatServerName);
    }
    public static void SetLastLevel(uint value)
    {
        PlayerPrefs.SetInt(LastLevel, (int)value);
    }

    public static uint GetLastLevel()
    {
        return (uint)PlayerPrefs.GetInt(LastLevel);
    }
    //public static void SetServerState(uint value)
    //{
    //    PlayerPrefs.SetInt(LastServerState, (int)value);
    //}

    //public static uint GetServerState()
    //{
    //    return (uint)PlayerPrefs.GetInt(LastServerState);
    //}

    public static void SetAnnouceVersion( float value)
    {
        PlayerPrefs.SetFloat(AnnouncementVersion, value);
    }

    public static float GetAnnounceVersion()
    {
        return (float)PlayerPrefs.GetFloat(AnnouncementVersion);
    }


    public static void SetFirstPractice(uint key)
    {
        PlayerPrefs.SetInt(MainPlayer.Instance.AccountID + "IsFirstPractice_" + key, 1);
    }

    public static uint GetFirstPractice(uint key)
    {
        return (uint)PlayerPrefs.GetInt(MainPlayer.Instance.AccountID + "IsFirstPractice_" + key);
    }
}
