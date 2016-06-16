using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class ExportAssetBundles
{
    //[MenuItem("Game/ReplaceToETC", false, 20)]

    /// <summary>
    /// AssetBundle制作选项
    /// </summary>
    private static BuildAssetBundleOptions buildOp = BuildAssetBundleOptions.CollectDependencies |
                                                        BuildAssetBundleOptions.CompleteAssets |
                                                        BuildAssetBundleOptions.DeterministicAssetBundle;
    /// <summary>
    /// Push依赖关系的次数
    /// </summary>
    private static int pushNumber = 0;

#if UNITY_ANDROID
    private static BuildTarget buildTarget = BuildTarget.Android;
    private static string AssetbundlePath = "AssetBundle/Android/";
#elif UNITY_IPHONE
    private static BuildTarget buildTarget = BuildTarget.iOS;
    private static string AssetbundlePath = "AssetBundle/Iphone/";
#else
    private static BuildTarget buildTarget = BuildTarget.StandaloneWindows;
    private static string AssetbundlePath = "AssetBundle/Editor/";
#endif

    /// <summary>
    /// <AssetBundle, AssetBundle包含的资源清单>
    /// </summary>
    private static Dictionary<string, List<string>> ResourceDictionary = null;

    //日志相关
    private static FileStream fileStream = null;
    private static StreamWriter streamWriter = null;

    private static bool isPrintLog = false;

    private static void Log(string content, bool isError = false)
    {
        if (!isPrintLog)
            return;

        string log = "";
        if (isError)
            log += "[ERROR ]";
        else
            log += "[NORMAL]";

        log += System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" +content;
        streamWriter.WriteLine(log);
        streamWriter.Flush();
    }

    private static void AddResouce(string assetbundle, string res)
    {
        if (ResourceDictionary == null)
            return;

        assetbundle = assetbundle.Substring(AssetbundlePath.Length);

        if (!ResourceDictionary.ContainsKey(assetbundle))
        {
            List<string> resList = new List<string>();
            ResourceDictionary.Add(assetbundle, resList);
        }
        if (!ResourceDictionary[assetbundle].Contains(res))
        {
            ResourceDictionary[assetbundle].Add(res);
        }
    }

    /// <summary>
    /// 通过资源的AssetPath查找所在的AssetBundle路径
    /// </summary>
    /// <param name="assetPath"></param>
    /// <returns></returns>
    private static string GetAssetBundleByResPath(string assetPath)
    {
        foreach (var d in ResourceDictionary)
        {
            if (d.Value.Contains(assetPath))
            {
                string res = assetPath.Replace("Assets/Resources/", "");
                res = res.Substring(0, res.IndexOf("."));
                return res;
            }
        }

        Log("Not Find AssetBundle=>" + assetPath, true);
        return null;
    }

    #region 辅助函数
    private static void Push()
    {
        BuildPipeline.PushAssetDependencies();
        pushNumber++;
    }

    private static void Pop()
    {
        pushNumber--;
        if (pushNumber < 0)
            Log("pushNumber=>" + pushNumber.ToString(), true);
        else
            BuildPipeline.PopAssetDependencies();
    }

    /// <summary>
    /// 获取路径中文件名（不包含后缀）
    /// </summary>
    /// <returns></returns>
    private static string GetFileName(string path)
    {
        path = path.Replace("\\", "/");
        int start = path.LastIndexOf("/") + 1;
        int end = path.IndexOf(".", start);

        string name = path.Substring(start, end - start);
        return name;
    }

    private static string GetAssetBundlePath(string prefabPath)
    {
        //"atlas_career_career.assetbundle"
        string fileName = prefabPath.Replace("Assets/Resources/", "").Replace("/", "_");
        fileName = fileName.Substring(0, fileName.LastIndexOf(".")).ToLower() + ".assetbundle";

        string pre = prefabPath.Replace("Assets/Resources/", "");
        pre = pre.Substring(0, pre.IndexOf('/')).ToLower();

        string dir = CheckDirectory(AssetbundlePath + pre);
        string path = dir + "/" + fileName;

        return path;
    }

    private static string CheckDirectory(string path)
    {
        if (Directory.Exists(path) == false)
            Directory.CreateDirectory(path);

        return path;
    }

    [MenuItem("Game/PrintDependencies", false, 61)]
    private static void PrintDependencies()
    {
        Object[] objs = Selection.objects;

        string file_path = AssetDatabase.GetAssetPath(objs[0]);
        string[] depAssetPaths = AssetDatabase.GetDependencies( new string[] { file_path });

        foreach (var path in depAssetPaths)
        {
            Debug.Log(path);
        }
    }
    #endregion

    #region 

    /// <summary>
    /// 获取rootDirectoryPath目录下，满足fileSuffixString条件的所有文件路径
    /// </summary>
    /// <param name="rootDirectoryPath">根目录</param>
    /// <param name="fileSuffixString">文件过滤后缀，多个后缀|分割</param>
    /// <returns></returns>
    private static List<string> GetFilesPath(string rootDirectoryPath, string fileSuffixString)
    {
        string[] suffix_array = fileSuffixString.Split('|');
        List<string> pathList = new List<string>();
        foreach (var suffix in suffix_array)
        {
            string[] file_paths = Directory.GetFiles(rootDirectoryPath, suffix, SearchOption.AllDirectories);
            pathList.AddRange(file_paths);
        }

        return pathList;
    }

    /// <summary>
    /// 获取指定路径下的资源对象
    /// </summary>
    /// <param name="file_path"></param>
    /// <returns></returns>
    private static Object GetObject(string file_path)
    {
        if (file_path.EndsWith(".meta"))
            return null;

        string path = file_path.Replace("\\", "/");
        path = path.Substring(Application.dataPath.Length - 6);

        Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        return obj;
    }

    /// <summary>
    /// 将指定文件夹中的资源，制作成对应的单个assetBundle
    /// </summary>
    /// <param name="rootDirectoryPath"></param>
    /// <param name="fileSuffixString"></param>
    private static void BaseAsset(string rootDirectoryPath, string fileSuffixString)
    {
        var pathList = GetFilesPath(rootDirectoryPath, fileSuffixString);
        for (int index = 0; index < pathList.Count; index++)
        {
            string file_path = pathList[index];
            Object obj = GetObject(file_path);
            if (obj == null)
                continue;

            file_path = AssetDatabase.GetAssetPath(obj);
            string assetBundlePath = GetAssetBundlePath(file_path);
            BuildPipeline.BuildAssetBundle(obj, null, assetBundlePath, buildOp, buildTarget);

            //填充资源列表，在添加依赖关系时会使用
            AddResouce(assetBundlePath, file_path);

            Log(file_path);
        }
    }

    /// <summary>
    /// 将指定文件夹中的资源制作成1个assetBundle
    /// </summary>
    /// <param name="rootDirectoryPath"></param>
    /// <param name="fileSuffixString"></param>
    /// <param name="assetBundleName"></param>
    private static void BaseAssetAll(string rootDirectoryPath, string fileSuffixString, string assetBundleName, bool isFullName = false)
    {
        var pathList = GetFilesPath(rootDirectoryPath, fileSuffixString);

        List<Object> objList = new List<Object>();
        for (int index = 0; index < pathList.Count; index++)
        {
            string file_path = pathList[index];
            Object obj = GetObject(file_path);
            if (obj == null)
                continue;

            if (isFullName)
            {
                file_path = AssetDatabase.GetAssetPath(obj);
                file_path = file_path.Replace("Assets/Resources/", "").Replace("/", "_");
                obj.name = file_path.ToLower();
            }
            objList.Add(obj);
        }

        string assetBundlePath = rootDirectoryPath + "/" + assetBundleName + ".assetbundle";
        assetBundlePath = assetBundlePath.Substring(Application.dataPath.Length - 6);
        assetBundlePath = GetAssetBundlePath(assetBundlePath);

        BuildPipeline.BuildAssetBundle(null, objList.ToArray(), assetBundlePath, buildOp, buildTarget);

        //填充资源列表，在添加依赖关系时会使用
        Log("***********Start:" + assetBundlePath);
        foreach (var obj in objList)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            AddResouce(assetBundlePath, assetPath);
            Log("2=>" + assetPath);
        }
        Log("***********End:" + assetBundlePath);
    }

    /// <summary>
    /// UI预制体按文件夹制作成一个大的assetbundle
    /// </summary>
    /// <param name="rootDirectoryPath"></param>
    /// <param name="fileSuffixString"></param>
    private static void GUIAssetBundleDep(string rootDirectoryPath, string fileSuffixString, string assetBundleName)
    {
        var pathList = GetFilesPath(rootDirectoryPath, fileSuffixString);
        List<Object> objList = new List<Object>();

        string dependentBundleNames = "Assets/DependentBundleNames.asset";
        StringHolder holder = ScriptableObject.CreateInstance<StringHolder>();
        holder.dependencies = new List<DependItem>();

        for (int index = 0; index < pathList.Count; index++)
        {
            string file_path = pathList[index];
            Object obj = GetObject(file_path);
            if (obj == null)
                continue;

            file_path = AssetDatabase.GetAssetPath(obj);
            List<string> depBundleNames = new List<string>();
            string[] depAssetPaths = AssetDatabase.GetDependencies(new string[] { file_path });
            Log("********Start**********" + file_path);
            for (int i = 0; i < depAssetPaths.Length; i++)
            {
                string depAssetPath = depAssetPaths[i];
                //自己不用添加对自己的依赖关系
                if (depAssetPath == file_path)
                    continue;
                //所有的GUI prefab已经被打在了一个AssetBundle中，无需要依赖关系
                if (depAssetPath.StartsWith("Assets/Resources/Prefab/GUI/"))
                    continue;

                Object depAsset = AssetDatabase.LoadAssetAtPath(depAssetPath, typeof(UnityEngine.Object));
                string depBundlePath = "";
                /* ****************************************
                 * 当前UI预制体会和一下类型存在依赖关系
                 * 1、系统字体
                 * 2、声音
                 * 3、UI字体（图片字体）
                 * 4、UI图集
                 * 备注：UI使用到的动画都在一个assetbundle里面，这里就不添加关联关系了。
                 * ****************************************/
                if (depAsset is Font ||
                    depAsset is AudioClip ||
                    depAssetPath.ToLower().Contains("texture/"))
                {
                    depBundlePath = GetAssetBundleByResPath(depAssetPath);

                }
                else if (depAsset is GameObject)
                {
                    GameObject depObject = depAsset as GameObject;
                    if (depObject.GetComponents<UIFont>() != null ||
                        depObject.GetComponents<UIAtlas>() != null)
                    {
                        depBundlePath = GetAssetBundleByResPath(depAssetPath);
                    }
                }

                if (!string.IsNullOrEmpty(depBundlePath))
                {
                    depBundleNames.Add(depBundlePath);
                    Log("dep add " + depBundlePath);
                }
            }

            Log("********End**********" + file_path);

            //添加依赖描述信息
            DependItem item = new DependItem();
            item.name = obj.name;
            item.list = depBundleNames;
            holder.dependencies.Add(item);

            //当前的游戏对象添加到队列中
            objList.Add(obj);
        }

        AssetDatabase.CreateAsset(holder, dependentBundleNames);
        Object depBundleDescription = AssetDatabase.LoadAssetAtPath(dependentBundleNames, typeof(StringHolder));

        //assetBundlePath路径
        string assetBundlePath = rootDirectoryPath + "/" + assetBundleName + ".assetbundle";
        assetBundlePath = assetBundlePath.Substring(Application.dataPath.Length - 6);
        assetBundlePath = GetAssetBundlePath(assetBundlePath);

        BuildPipeline.BuildAssetBundle(depBundleDescription, objList.ToArray(), assetBundlePath, buildOp, buildTarget);

        //填充资源列表，在添加依赖关系时会使用
        foreach (var obj in objList)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            AddResouce(assetBundlePath, assetPath);
        }

        AssetDatabase.DeleteAsset(dependentBundleNames);
        AssetDatabase.Refresh();
    }

    private static void CreateAtlasScript()
    {
        GameObject go = new GameObject("AtlasScript");
        go.AddComponent<UIAtlas>();
        go.AddComponent<UIFont>();

        string assetPath = "Assets/AtlasScript.prefab";
        if (File.Exists(assetPath))
            File.Delete(assetPath);
        var asset = PrefabUtility.CreatePrefab(assetPath, go);

        string assetBundleName = "script";
        string assetBundlePath = "Assets/Resources/Atlas/" + assetBundleName + ".assetbundle";
        assetBundlePath = GetAssetBundlePath(assetBundlePath);

        BuildPipeline.BuildAssetBundle(asset, null, assetBundlePath, buildOp, buildTarget);
        GameObject.DestroyImmediate(go);
    }

    /// <summary>
    /// 制作资源清单
    /// </summary>
    private static void CreateResourceList()
    {
        string resourceList = "Assets/ResourceList.asset";
        StringHolder holder = ScriptableObject.CreateInstance<StringHolder>();
        holder.ResourceList = new List<string>();
        holder.AssetBundleList = new List<string>();
        foreach (var d in ResourceDictionary)
        {
            foreach (var l in d.Value)
            {
                string res = l.Replace("Assets/Resources/", "");
                res = res.Substring(0, res.IndexOf("."));

                string ass = d.Key;

                holder.ResourceList.Add(res);
                holder.AssetBundleList.Add(ass);
            }
        }

        if (File.Exists(resourceList))
            File.Delete(resourceList);
        AssetDatabase.CreateAsset(holder, resourceList);
        Object objResourceList= AssetDatabase.LoadAssetAtPath(resourceList, typeof(StringHolder));

        string assetBundle = AssetbundlePath + "ResourceList.assetbundle";
        bool result = BuildPipeline.BuildAssetBundle(objResourceList, null, assetBundle, buildOp, buildTarget);
        Log(assetBundle, !result);
    }

    #endregion

    [MenuItem("Game/ExportAssetBundles<All>", false, 21)]
    public static void Make()
    {
        isPrintLog = true;

        Init();

        //必须保证依赖顺序
        MakeLuaAndProto();

        MakeAnimation();
        MakeConfig();
        MakeFashion();
        MakePlayer();
        MakePrefabOther();

        //Push();
        //CreateAtlasScript();

        Push();
        MakeTexture();
        MakeAudio();
        MakeAtlas();
        MakeFont();

        Push();
        MakePrefabGUI();

        Pop();
        Pop();
        //Pop();

        //////////////////////////////////////////////////////////////////////////
        //必须重新制作一次，因为上面有依赖关系，图集共享脚本和shader
        //运行时会出现错误
        //////////////////////////////////////////////////////////////////////////
        MakeAtlas();

        End();
    }

    [MenuItem("Game/ExportAssetLuaAndProto", false, 24)]
    public static void MakeLuaAndProto()
    {
        //打包之前，先拷贝资源到Resource目录下
        CopyLuaAndProto.CopyLuaFilesToResource(false);

        string rootDirectoryPath, fileSuffixString;

        rootDirectoryPath = Application.dataPath + "/Resources/Lua";
        fileSuffixString = "*.lua.txt";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all", true);

        rootDirectoryPath = Application.dataPath + "/Resources/Proto";
        fileSuffixString = "*.proto.txt";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all", true);
    }

    private static void Init()
    {
        ResourceDictionary = new Dictionary<string, List<string>>();

        CheckDirectory(AssetbundlePath);
        string logPath = AssetbundlePath + "log.txt";
        if (File.Exists(logPath))
            File.Delete(logPath);
        fileStream = new FileStream(logPath, FileMode.OpenOrCreate);
        streamWriter = new StreamWriter(fileStream);
    }

    private static void End()
    {
        CreateResourceList();

        streamWriter.Flush();
        streamWriter.Close();
        fileStream.Close();

        EditorUtility.DisplayDialog("", "Completed", "OK");
        AssetDatabase.Refresh();
    }

    #region 分类型打AssetBundle
    [MenuItem("Game/ExportAssetAtlas", false, 23)]
    private static void MakeAtlas()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Atlas";
        string fileSuffixString = "*.prefab";
        BaseAsset(rootDirectoryPath, fileSuffixString);

        rootDirectoryPath = Application.dataPath + "/Resources/AtlasOther";
        fileSuffixString = "*.prefab";
        BaseAsset(rootDirectoryPath, fileSuffixString);
    }


    private static void MakeAudio()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Audio";
        string fileSuffixString = "*.OGG";
        BaseAsset(rootDirectoryPath, fileSuffixString);
    }

    private static void MakeAnimation()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Animation";
        string fileSuffixString = "*.controller";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "ani");
    }


     [MenuItem("Game/ExportAssetConfig", false, 22)]
    private static void MakeConfig()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Config";
        string fileSuffixString = "*.xml|*.txt|*.bytes";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "xml");
    }

    private static void MakeFont()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Font";
        string fileSuffixString = "*.ttf";
        BaseAsset(rootDirectoryPath, fileSuffixString);
    }

    private static void MakeTexture()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Texture";
        string fileSuffixString = "*";
        BaseAsset(rootDirectoryPath, fileSuffixString);
    }

    private static void MakeFashion()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Object/Fashion";
        string fileSuffixString = "*.fbx";
        BaseAsset(rootDirectoryPath, fileSuffixString);
    }

    private static void MakePlayer()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Object/Player/body";
        string fileSuffixString = "*.prefab|*.mat";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");

        rootDirectoryPath = Application.dataPath + "/Resources/Object/Player/head";
        fileSuffixString = "*.fbx";
        BaseAsset(rootDirectoryPath, fileSuffixString);


        rootDirectoryPath = Application.dataPath + "/Resources/Object/Player/SpecialAction";
        fileSuffixString = "*.anim";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");

        rootDirectoryPath = Application.dataPath + "/Resources/Object/Player/tongyong-nv/animation";
        fileSuffixString = "*.anim";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");
    }

    private static void MakePrefabOther()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Prefab/Camera";
        string fileSuffixString = "*.prefab";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");

        rootDirectoryPath = Application.dataPath + "/Resources/Prefab/DynObject";
        fileSuffixString = "*.prefab";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");

        rootDirectoryPath = Application.dataPath + "/Resources/Prefab/Effect";
        fileSuffixString = "*.prefab";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");

        rootDirectoryPath = Application.dataPath + "/Resources/Prefab/Indicator";
        fileSuffixString = "*.prefab";
        BaseAssetAll(rootDirectoryPath, fileSuffixString, "all");
    }

    private static void MakePrefabGUI()
    {
        string rootDirectoryPath = Application.dataPath + "/Resources/Prefab/GUI";
        string fileSuffixString = "*.prefab";
        GUIAssetBundleDep(rootDirectoryPath, fileSuffixString, "all");
    }
    #endregion


    #region 场景打包

    //[MenuItem("Game/ExportScene", false, 3)]

    #endregion

}
