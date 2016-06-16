
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 资源管理类
/// 实现资源读取，为了保证原有的类（ResourceLoadManager）正常使用
/// 这里只作为基类用于提供逻辑处理，用于ResourceLoadManager来继承使用
/// 
/// 本类加载方式当前都采用同步加载方式
/// </summary>
public class ResourceLoadManagerBase : Singleton<ResourceLoadManagerBase>
{
    /// <summary>
    /// 资源清单
    /// </summary>
    protected Dictionary<string, string> _dictionaryResourcesPath = new Dictionary<string, string>();

    /// <summary>
    /// 已读取的资源进行缓存
    /// </summary>
    public Dictionary<string, Object> _dictionaryResourcesObject = new Dictionary<string, Object>();

    /// <summary>
    /// 已读取的AssetBundle进行缓存
    /// </summary>
    public Dictionary<string, AssetBundle> _dictionaryAssetBundle = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// lua 和 proto 文件缓存
    /// </summary>
    private Dictionary<string, TextAsset> _luaList = new Dictionary<string, TextAsset>();

    /// <summary>
    /// AssetBundle资源根路径
    /// </summary>
    protected string AssetRoot
    {
        get
        {
            return VersionUpdateManager.persistentDataPath + "/" + VersionUpdateManager.GamePlat;
        }
    }

    public ResourceLoadManagerBase()
    {
        System.DateTime time = System.DateTime.Now;
        GetResourcsList();
        Logger.Log("【Time】GetResourcsList=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

        time = System.DateTime.Now;
        this.Init();
        Logger.Log("【Time】ResourceLoadManagerBase Init=>" + (System.DateTime.Now - time).TotalSeconds.ToString());
    }

    /// <summary>
    /// 初始化管理器
    /// </summary>
    public void Init()
    {
        if (!GlobalConst.IS_DEVELOP)
        {
            string assetBundlePath = "";
            AssetBundle assetBundle = null;
            Object[] objs = null;
            System.DateTime time = System.DateTime.Now;

            //time = System.DateTime.Now;
            //assetBundlePath = "atlas/atlas_script.assetbundle";
            //assetBundle = this.GetLoadAssetBundle(assetBundlePath);
            //Logger.Log("【Time】ResourceLoadManagerBase Init AtlasScript=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

            time = System.DateTime.Now;
            #region lua
            assetBundlePath = "lua/lua_all.assetbundle";
            assetBundle = this.GetLoadAssetBundle(assetBundlePath);
            objs = assetBundle.LoadAllAssets(typeof(TextAsset));
            for (int i = 0; i < objs.Length; i++)
            {
                string luaName = objs[i].name.ToLower();
                if (!_luaList.ContainsKey(luaName))
                    _luaList.Add(luaName, objs[i] as TextAsset);
                else
                    Logger.LogWarning("Lua Name is Exist =>" + luaName);
            }
            #endregion
            Logger.Log("【Time】ResourceLoadManagerBase Init lua=>" + (System.DateTime.Now - time).TotalSeconds.ToString());

            time = System.DateTime.Now;
            #region proto
            assetBundlePath = "proto/proto_all.assetbundle";
            assetBundle = this.GetLoadAssetBundle(assetBundlePath);
            objs = assetBundle.LoadAllAssets(typeof(TextAsset));
            for (int i = 0; i < objs.Length; i++)
            {
                string protoName = objs[i].name.ToLower();
                if (!_luaList.ContainsKey(protoName))
                    _luaList.Add(protoName, objs[i] as TextAsset);
                else
                    Logger.LogWarning("proto Name is Exist =>" + protoName);
            }
            #endregion
            Logger.Log("【Time】ResourceLoadManagerBase Init proto=>" + (System.DateTime.Now - time).TotalSeconds.ToString());
        }
    }

    /// <summary>
    /// 获取AssetBundle中资源清单
    /// </summary>
    protected void GetResourcsList()
    {
        if (!GlobalConst.IS_DEVELOP)
        {
            string path = AssetRoot + "/ResourceList.assetbundle";

            byte[] fileBytes = File.ReadAllBytes(path);
            AssetBundle assetBundle = AssetBundle.LoadFromMemory(fileBytes);

            StringHolder depList = assetBundle.LoadAsset("ResourceList", typeof(StringHolder)) as StringHolder;
            if (depList != null && depList.AssetBundleList != null && depList.ResourceList != null)
            {
                for (int i = 0; i < depList.ResourceList.Count; i++)
                {
                    string resStr = depList.ResourceList[i].ToLower();
                    if (!_dictionaryResourcesPath.ContainsKey(resStr))
                        _dictionaryResourcesPath.Add(resStr, depList.AssetBundleList[i]);
                    else
                        Debug.LogWarning("ResourcsList=>" + resStr);
                }
            }

            assetBundle.Unload(true);
        }
    }

    /// <summary>
    /// 获取lua文件内容
    /// </summary>
    /// <param name="vPath"></param>
    /// <returns></returns>
    public TextAsset GetLua(string vPath)
    {
        if (GlobalConst.IS_DEVELOP)
        {
            return Resources.Load<TextAsset>(vPath);
        }
        else
        {
            string name = vPath.Replace("/", "_").ToLower() + ".txt";
            if (_luaList.ContainsKey(name))
                return _luaList[name];
        }

        return null;
    }

    /// <summary>
    /// 获取资源对象
    /// </summary>
    /// <param name="vPath"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object BaseGetResources(string vPath, System.Type type)
    {
        Object result = null;
        if (_dictionaryResourcesObject.TryGetValue(vPath, out result))
        {
            return result;
        }

        if (GlobalConst.IS_DEVELOP)
        {
            result = Resources.Load(vPath, type);
            _dictionaryResourcesObject.Add(vPath, result);
            return result;
        }
        else
        {
            result = LoadAssetBundle(vPath, type);
            return result;
        }
    }

    /// <summary>
    /// 同步加载一个AssetBundle资源
    /// </summary>
    /// <param name="assetBundlePath"></param>
    /// <returns>AssetBundle资源对象</returns>
    public AssetBundle GetLoadAssetBundle(string assetBundlePath)
    {
        byte[] dependAssetBytes = null;
        AssetBundle assetBundle = null;

        assetBundlePath = AssetRoot + "/" + assetBundlePath;
        if (_dictionaryAssetBundle.ContainsKey(assetBundlePath))
        {
            assetBundle = _dictionaryAssetBundle[assetBundlePath];
        }
        else
        {
            dependAssetBytes = File.ReadAllBytes(assetBundlePath);
            assetBundle = AssetBundle.LoadFromMemory(dependAssetBytes);
            _dictionaryAssetBundle.Add(assetBundlePath, assetBundle);
        }

        return assetBundle;
    }

    /// <summary>
    /// 同步加载一个AssetBundle资源
    /// </summary>
    /// <param name="vPath">资源路径</param>
    /// <returns>AssetBundle中的某个资源</returns>
    private Object LoadAssetBundle(string vPath, System.Type type)
    {
        Object result = null;
        if (_dictionaryResourcesObject.TryGetValue(vPath, out result))
        {
            return result;
        }

        string filePath = GetAssetBundlePath(vPath);
        string resName = GetAssetResName(vPath);

        byte[] dependAssetBytes = null;
        AssetBundle assetBundle = null;
        try
        {
            if (_dictionaryAssetBundle.ContainsKey(filePath))
            {
                assetBundle = _dictionaryAssetBundle[filePath];
            }
            else
            {
                if (!File.Exists(filePath))
                {
                    Logger.LogError("file not find filePath=>" + filePath + "\n vPath=>" + vPath);
                    return null;
                }
                dependAssetBytes = File.ReadAllBytes(filePath);
                assetBundle = AssetBundle.LoadFromMemory(dependAssetBytes);
                _dictionaryAssetBundle.Add(filePath, assetBundle);
            }
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex.StackTrace + " vpath=" + vPath);
            return null;
        }

        //获取资源是否有附带的描述信息，当前只有NGUI窗口预制体存在
        StringHolder holder = (StringHolder)assetBundle.LoadAsset("DependentBundleNames", typeof(StringHolder));
        if (holder == null || holder.dependencies == null || holder.dependencies.Count == 0)
        {
            result = assetBundle.LoadAsset(resName, type);
            _dictionaryResourcesObject.Add(vPath, result);
            return result;
        }

        foreach (var item in holder.dependencies)
        {
            if (item.name != resName)
                continue;

            foreach (var path in item.list)
            {
                string typestr = path.Substring(0, path.IndexOf("/")).ToLower();
                if (typestr == "atlas" || typestr == "atlasother" ||
                    typestr == "prefab" || typestr == "object")
                {
                    LoadAssetBundle(path, typeof(GameObject));
                }
                else if (typestr == "texture")
                {
                    LoadAssetBundle(path, typeof(Texture2D));
                }
                else
                {
                    LoadAssetBundle(path, typeof(Object));
                }
            }
        }

        //assetBundle.Unload(false);

        result = assetBundle.LoadAsset(resName, type);
        _dictionaryResourcesObject.Add(vPath, result);

        return result;
    }

    #region Static 函数
    /// <summary>
    /// 切换资源路径（Resources目录切换到AssetBundle目录）
    /// </summary>
    /// <param name="vpath"></param>
    /// <returns></returns>
    private string GetAssetBundlePath(string vpath)
    {
        string assetBundlePath = "";
        _dictionaryResourcesPath.TryGetValue(vpath.ToLower(), out assetBundlePath);
        return AssetRoot + "/" + assetBundlePath;
    }

    /// <summary>
    /// 纯字符串截取操作
    /// 根据完整的资源路径，获取资源名称
    /// </summary>
    /// <param name="vpath"></param>
    /// <returns></returns>
    private string GetAssetResName(string vpath)
    {
        string name = vpath.Substring(vpath.LastIndexOf("/") + 1);
        return name;
    }
    #endregion
}