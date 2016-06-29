using UnityEngine;
//using UnityEditor;
using System;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

public class Util {

    /// <summary>
    /// 取得Lua路径
    /// </summary>
    public static string LuaPath(string name) {
        string path = Application.dataPath + "/";
        if (Application.isMobilePlatform)	//移动平台下，Lua脚本暂时放在Resources里面，由Unity管理
            path = "";
        string lowerName = name.ToLower();
        if (lowerName.EndsWith(".lua"))
        {
            return path + "ulua/Lua/" + name;
        }
        return path + "ulua/Lua/" + name + ".lua";
    }

	public static void Log(string str, string stackTrace = "")
	{
        if (ErrorDisplay.Instance != null && ErrorDisplay.Instance.enabled)
        {
            Debug.Log(str);
            ErrorDisplay.Instance.SupplementLastLogStackTrace("\n" + stackTrace);
        }
        else
        {
            Debug.Log(str + "\n" + stackTrace);
        }
	}

    public static void LogWarning(string str, string stackTrace = "")
    {
        if (ErrorDisplay.Instance != null && ErrorDisplay.Instance.enabled)
        {
            Debug.LogWarning(str);
            ErrorDisplay.Instance.SupplementLastLogStackTrace("\n" + stackTrace);
        }
        else
        {
            Debug.LogWarning(str + "\n" + stackTrace);
        }
    }

	public static void LogError(string str, string stackTrace = "")
	{
        if (ErrorDisplay.Instance != null && ErrorDisplay.Instance.enabled)
        {
            Debug.LogError(str);
            ErrorDisplay.Instance.SupplementLastLogStackTrace("\n" + stackTrace);
        }
        else
        {
            Debug.LogError(str + "\n" + stackTrace);
        }
	}

    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory() {
        GC.Collect();
        Resources.UnloadUnusedAssets();
        LuaScriptMgr mgr = LuaScriptMgr.Instance;
        if (mgr != null && mgr.lua != null) mgr.LuaGC();
    }

    /// <summary>
    /// 防止初学者不按步骤来操作
    /// </summary>
    /// <returns></returns>
    static int CheckRuntimeFile() {
        if (!Application.isEditor) return 0;
        string sourceDir = AppConst.uLuaPath + "/Source/LuaWrap/";
        if (!Directory.Exists(sourceDir)) {
            return -2;
        } else {
            string[] files = Directory.GetFiles(sourceDir);
            if (files.Length == 0) return -2;
        }
        return 0;
    }

    /// <summary>
    /// 检查运行环境
    /// </summary>
    public static bool CheckEnvironment() {
#if UNITY_EDITOR
        int resultId = Util.CheckRuntimeFile();
        if (resultId == -1) {
            Debug.LogError("没有找到框架所需要的资源，单击Game菜单下Build xxx Resource生成！！");
            UnityEditor.EditorApplication.isPlaying = false;
            return false;
        } else if (resultId == -2) {
            Debug.LogError("没有找到Wrap脚本缓存，单击Lua菜单下Gen Lua Wrap Files生成脚本！！");
            UnityEditor.EditorApplication.isPlaying = false;
            return false;
        }
#endif
        return true;
    }
    /// <summary>
    /// 是不是苹果平台
    /// </summary>
    /// <returns></returns>
    public static bool isApplePlatform {
        get {
            return Application.platform == RuntimePlatform.IPhonePlayer ||
                   Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.OSXPlayer;
        }
    }

    /// <summary>
    /// HashToMD5Hex
    /// </summary>
    public static string HashToMD5Hex(string sourceStr)
    {
        byte[] Bytes = Encoding.UTF8.GetBytes(sourceStr);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] result = md5.ComputeHash(Bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                builder.Append(result[i].ToString("x2"));
            return builder.ToString();
        }
    }

    /// <summary>
    /// 计算字符串的MD5值
    /// </summary>
    public static string md5(string source)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();

        string destString = "";
        for (int i = 0; i < md5Data.Length; i++)
        {
            destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
        }
        destString = destString.PadLeft(32, '0');
        return destString;
    }

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }

}