using UnityEngine;
using System.Collections;
using System.IO;

public enum UpdataFileState : byte
{
    None = 0,
    GetSourceList,
    GetLocalList,
    CheckVersion,
    UpdateResource,
    DeleteFile,
    Over
}

////资源类型 枚举
//public enum ResourceType
//{
//    UI,
//    Scene,
//    Prefab,
//    Resource,
//    AssetBundle,
//    AssetBundleForWWW,
//}

public enum LoadSceneState
{
    NotLoading = 0,
    Loading,
    LoadingFinish
}

public enum LoadState
{
    None = 0,
    LoadStart,
    Loading,
    LoadFinish,
    LoadClose
}

public struct UpdateFileData
{
    public string FilePath;
    public string FileName;
    public string FilteMD5;
}

public struct FilePathData 
{
    public string UrlPath;
    public string LocalPath;
}

public struct HintData 
{
    public ushort id;
    public string info;
}

public enum StorePlatform : byte
{
    None = 0,
    APPStore = 1,
    AndroidMarket = 2,
}
