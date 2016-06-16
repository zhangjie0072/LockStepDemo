using UnityEngine;
using System.Collections;

public class ResPath
{
    //public const string CHECKVERSIONADDRESS = "http://192.168.1.35:81/";  //内网资源服
    public const string CHECKVERSIONADDRESS = "http://res.gamebean.com/10001018/";     //外网资源服
    public const string APKVERSIONXML = "Config/ApkVersion";
    public const string CHECKVERSION_ASSETNAME = "Version.xml";
    public const string RESPATHXML = "ResourcesXML/ResourcesList.xml";
    public const string ANDROIDPATH = "Android/";
    public const string IPHONEPATH = "Iphone/";
    public const string OTHERPATH = "Editor/";
    //public const string ANDROIDPATH = "TDTest/TD/Assetbundle/Android/";     //测试资源路径
    //public const string IPHONEPATH = "TDTest/TD/Assetbundle/Iphone/";       //测试资源路径
    //public const string OTHERPATH = "TDTest/TD/Assetbundle/Editor/";        //测试资源路径
    public const string DIR_ATLAS = "Atlas/";
    public const string DIR_SOUND = "Audio/";
    public const string DIR_ALONERES = "AloneRes/";
    public const string DIR_EFFECT = "Effect/";
    public const string DIR_IMAGE = "Texture/";
    public const string DIR_ROLE = "Player/";
    public const string DIR_VIEW = "Prefab/";
    public const string DIR_CONFIG = "Config/";
    public const string DIR_LUA = "Lua/";
    public const string DIR_PROTO = "Proto/";
    public const string DIR_LOACAL_VERSIONXML = "Version/";
    public const string DLLPATH = "/mnt/sdcard/com.pixone.LTD/files/Assembly-CSharp.dll";
    public const string APKPATH = "/mnt/sdcard/com.pixone.LTD/files";

    public const string PREFIX_VIEW = "prefab_";
    public const string PREFIX_SOUND = "audio_";
    public const string PREFIX_ATLAS = "atlas_";
    public const string PREFIX_ROLE = "player_";
    public const string PREFIX_CONFIG = "config_";
    public const string PREFIX_ALONERES = "aloneres_";
    public const string PREFIX_IMAGE = "texture_";
    public const string PREFIX_EFFECT = "effect_";
    public const string PREFIX_FONT = "font_";
    public const string PREFIX_LUA = "lua_";
    public const string PREFIX_PROTO = "proto_";
    public const string FILESUFFIX = ".assetbundle";

    /// <summary>
    /// Application.persistentDataPath
    /// </summary>
    public static string RLOCALPATH
    {
        get
        {
            switch (Application.platform)
            {
                //case RuntimePlatform.IPhonePlayer: return "file://" + Application.persistentDataPath + "/" + IPHONEPATH;
                //case RuntimePlatform.Android: return "file://" + Application.persistentDataPath + "/" + ANDROIDPATH;
                //case RuntimePlatform.WindowsEditor:
                //case RuntimePlatform.OSXEditor: return "file:///" + Application.persistentDataPath + "/" + OTHERPATH;
                //default: return "file:///" + Application.persistentDataPath + OTHERPATH;
                case RuntimePlatform.IPhonePlayer: return Application.persistentDataPath + "/" + IPHONEPATH;
                case RuntimePlatform.Android: return Application.persistentDataPath + "/" + ANDROIDPATH;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor: return Application.persistentDataPath + "/" + OTHERPATH;
                default: return Application.persistentDataPath + OTHERPATH;
            }
        }
    }

    public static string WLOCALPATH
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer: return Application.dataPath +"/Raw/" + IPHONEPATH;
                case RuntimePlatform.Android: return Application.dataPath + "!/assets/" + ANDROIDPATH;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor: return Application.dataPath + "/StreamingAssets/" + OTHERPATH;
                default: return Application.persistentDataPath + OTHERPATH;
            }
        }
    }

    public static string DOWNLOADPATH
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer: return CHECKVERSIONADDRESS + IPHONEPATH;
                case RuntimePlatform.Android: return CHECKVERSIONADDRESS + ANDROIDPATH;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor: return CHECKVERSIONADDRESS + OTHERPATH;
                default: return CHECKVERSIONADDRESS + OTHERPATH;
            }
        }
    }

    //public static string CHECHVERSIONXMLPATH
    //{
    //    get
    //    {
    //        return DIR_LOACAL_VERSIONXML;
    //    }
    //}


    /// <summary>
    /// Application.streamingAssetsPath
    /// </summary>
    public static string WPRELOCALPATH
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer: return Application.streamingAssetsPath + IPHONEPATH;
                case RuntimePlatform.Android: return Application.streamingAssetsPath + ANDROIDPATH;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor: return Application.streamingAssetsPath + OTHERPATH;
                default: return Application.dataPath + OTHERPATH;
            }
        }
    }

    //public static string RPRELOCALPATH
    //{
    //    get
    //    {
    //        switch (Application.platform)
    //        {
    //            case RuntimePlatform.IPhonePlayer: return "file://" + Application.dataPath + IPHONEPATH;
    //            case RuntimePlatform.Android: return "file://" + Application.dataPath + "!/assets/" + ANDROIDPATH;
    //            case RuntimePlatform.WindowsEditor:
    //            case RuntimePlatform.OSXEditor: return "file://" + Application.dataPath + "/../" + OTHERPATH;
    //            default: return "file://" + Application.dataPath + OTHERPATH;
    //        }
    //    }
    //}

    /// <summary>
    /// 获取PersistentDataPath目录
    /// </summary>
    public static string PersistentDataPath
    {
        get
        {
            string path = Application.persistentDataPath + "/";
#if UNITY_STANDALONE
            path += OTHERPATH;
#elif UNITY_IPHONE
            path += IPHONEPATH;
#elif UNITY_ANDROID
            path += ANDROIDPATH;
#endif

            return path;
        }
    }
}
