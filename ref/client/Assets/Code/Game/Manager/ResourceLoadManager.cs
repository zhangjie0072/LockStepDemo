#define DEVELOPMENT

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public sealed class ResourceLoadManager : Singleton<ResourceLoadManager>
{
    private Dictionary<string, string> _configTextList = new Dictionary<string, string>();
    private Dictionary<string, byte[]> _configByteList = new Dictionary<string, byte[]>();

    /// <summary>
    /// 释放依赖的AssetBundle
    /// </summary>
    public void UnloadDependAB()
    {
        //_configTextList.Clear();
        //_configByteList.Clear();
    }

    /// <summary>
    /// 加载音效
    /// </summary>
    public void LoadSound(string vName, System.Action<Object> onLoaded = null, System.Action<string> onLoadError = null)
    {
        Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(AudioClip));

        if (result != null)
        {
            if (onLoaded != null)
                onLoaded(result);
        }
        else
        {
            if (onLoadError != null)
                onLoadError("load error sound res is null  desc:" + vName);
        }
    }

    /// <summary>
    /// 加载特效
    /// </summary>
    //public void LoadEffect(string vName, System.Action<GameObject> onLoaded, System.Action<string> onLoadError = null,
    //    ResourceLoadType type = ResourceLoadType.AssetBundle)
    //{
    //    Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(GameObject));

    //    if (result != null)
    //    {
    //        if (onLoaded != null)
    //            onLoaded((GameObject)result);
    //    }
    //    else
    //    {
    //        if (onLoadError != null)
    //            onLoadError("load error effect res is null  desc:" + vName);
    //    }
    //}

    /// <summary>
    /// 加载独立资源 返回Object对象
    /// </summary>
    //public void LoadAloneAsset(string vName, System.Action<Object> onLoaded, System.Action<string> onLoadError = null,
    //    ResourceLoadType type = ResourceLoadType.AssetBundle)
    //{
    //    Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(GameObject));
    //    if (result != null)
    //    {
    //        if (onLoaded != null)
    //            onLoaded(result);
    //    }
    //    else
    //    {
    //        if (onLoadError != null)
    //            onLoadError("load error alone res is null  desc:" + vName);
    //    }
    //}

    /// <summary>
    /// 加载角色资源
    /// </summary>
    public void LoadCharacter(string vName, System.Action<GameObject> onLoaded, System.Action<string> onLoadError = null)
    {
        Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(GameObject));

        if (result != null)
        {
            if (onLoaded != null)
                onLoaded((GameObject)result);
        }
        else
        {
            if (onLoadError != null)
                onLoadError("load error character res is null  desc:" + vName);
        }
    }

    /// <summary>
    /// 加载独立的Texture
    /// </summary>
    public void LoadAloneImage(string vName, System.Action<Texture> onLoaded, System.Action<string> onLoadError = null)
    {
        Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(Texture2D));

        if (result != null)
        {
            if (onLoaded != null)
                onLoaded((Texture2D)result);
        }
        else
        {
            if (onLoadError != null)
                onLoadError("load error alone res is null  desc:" + vName);
        }
    }

    /// <summary>
    /// 加载预制体(已使用)
    /// </summary>
    public GameObject LoadPrefab(string vName)
    {
        Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vName, typeof(GameObject));
        return result as GameObject;
    }

    /// <summary>
    /// 获取图集(已使用)
    /// </summary>
    public UIAtlas GetAtlas(string vPath)
    {
        UIAtlas atlas = null;

        var obj = ResourceLoadManagerBase.Instance.BaseGetResources(vPath, typeof(GameObject));
        var goAtlas = obj as GameObject;
        if (goAtlas != null)
        {
            atlas = goAtlas.GetComponent<UIAtlas>();
        }

        if (atlas == null)
        {
            Debug.LogError("GetAtlas is null =>" + vPath);
        }

        return atlas;
    }

    /// <summary>
    /// 获取资源(已使用)
    /// </summary>
    public Object GetResources(string vPath, bool vIsCache = true)
    {
        var obj = ResourceLoadManagerBase.Instance.BaseGetResources(vPath, typeof(Object));
        if (obj == null)
            Debug.LogError(vPath + " not find");
        return obj;
    }

    public Object GetResources(string vPath, System.Type type)
    {
        var obj = ResourceLoadManagerBase.Instance.BaseGetResources(vPath, type);
        if (obj == null)
            Debug.LogError(vPath + " not find");
        return obj;
    }

    public Object GetConfigResource(string vPath, DelegateLoadComplete callback = null, bool isCache = true)
    {
        Object result = ResourceLoadManagerBase.Instance.BaseGetResources(vPath, typeof(Object));
        if (vPath.Contains("Config/"))
        {
            TextAsset conf = result as TextAsset;
            if (vPath.Contains("GoodsAttr"))
            {
                _configByteList.Add(vPath, conf.bytes);
            }
            else
            {
                if (!_configTextList.ContainsKey(vPath))
                {
                    _configTextList.Add(vPath, conf.text);
                }
            }
            if (callback != null)
            {
                callback(vPath, result);
            }
        }
        else
        {
            callback(vPath, result);
        }

        return result;
    }

    public string GetConfigText(string vPath)
    {
        if (_configTextList.ContainsKey(vPath))
        {
            return _configTextList[vPath];
        }
        else
        {
            TextAsset result = this.GetConfigResource(vPath) as TextAsset;
            if (result == null)
                return null;

            return result.text;
        }
    }

    public byte[] GetConfigByte(string vPath)
    {
        if (_configByteList.ContainsKey(vPath))
        {
            return _configByteList[vPath];
        }
        return null;
    }

    public Object GetLuaResource(string vPath, DelegateLoadComplete callback = null, bool isCache = true)
    {
        return ResourceLoadManagerBase.Instance.GetLua(vPath);
    }
}
