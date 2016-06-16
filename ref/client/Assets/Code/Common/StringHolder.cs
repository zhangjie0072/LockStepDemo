using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 依赖关系对象
/// </summary>
[System.Serializable]
public class DependItem
{
    /// <summary>
    /// assetbundle名称
    /// </summary>
    public string name;
    /// <summary>
    /// 所依赖的AssetBundle列表
    /// </summary>
    public List<string> list;
}

public class StringHolder : ScriptableObject
{
    public string[] content;

    /// <summary>
    /// 保存依赖关系
    /// </summary>
    public List<DependItem> dependencies;

    /// <summary>
    /// 以下内容同时被使用
    /// 下标一样的的对应关系
    /// ResourceList存放资源路径，AssetBundleList存放资源所在的Bundle路径
    /// </summary>
    public List<string> ResourceList, AssetBundleList;
}
