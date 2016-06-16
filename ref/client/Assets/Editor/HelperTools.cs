using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class HelperTools
{
    /// <summary>
    /// 将指定文件夹中的材质球替换为不带通道，但支持透明的Shader
    /// </summary>
    [MenuItem("Game/ReplaceToETC", false, 2)]
    public static void ReplaceEtc()
    {
        ReclaceEtcMaterial(Application.dataPath + "/Resources/Atlas");

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Packager", "Succeed!", "ok");
    }

    /// <summary>
    /// 将指定文件夹中的材质球替换为NGUI支持带透明通道的Shader
    /// </summary>
    [MenuItem("Game/ReplaceToRGBA32", false, 3)]
    public static void ReplaceRGBA32()
    {
        ReclaceRGBA32Material(Application.dataPath + "/Resources/Atlas");
		
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Packager", "Succeed!", "ok");
    }

    /// <summary>
    /// 将选中的图集导出为散图
    /// </summary>
    [MenuItem("Game/ExportAtlasToPng", false, 62)]
    static void ExportAtlasToPng()
    {
        string RootFolderPath = EditorUtility.SaveFolderPanel("Save As", "Assets", "atlas");

        //获取被选中的游戏对象
        Object[] SelectedAsset = Selection.GetFiltered(typeof(UIAtlas), SelectionMode.Unfiltered);

        foreach (Object obj in SelectedAsset)
        {
            UIAtlas atlas = obj as UIAtlas;
            if (atlas == null)
                continue;

            string folderPath = RootFolderPath + "/" + atlas.name;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            for (int i = 0; i < atlas.spriteList.Count; i++)
            {
                UISpriteData sprite = atlas.spriteList[i];

                string path = folderPath + "/" + sprite.name + ".png";

                //NGUISettings.currentPath = System.IO.Path.GetDirectoryName(path);
                int width = sprite.width + sprite.paddingLeft + sprite.paddingRight;
                int height = sprite.height + sprite.paddingTop + sprite.paddingBottom;

                UIAtlasMaker.SpriteEntry se = UIAtlasMaker.ExtractSprite(atlas, sprite.name);

                if (se != null)
                {
                    byte[] bytes = null;

                    if (sprite.hasPadding) 
                    {
                        //创建图片原尺寸，使用透明填充
                        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
                        int count = width * height;
                        Color[] alphaColor = new Color[width * height];
                        for (int j = 0; j < count; j++)
                        {
                            alphaColor[j] = new Color(0, 0, 0, 0);
                        }

                        tex.SetPixels(alphaColor);
                        tex.SetPixels(sprite.paddingLeft, sprite.paddingTop, se.tex.width, se.tex.height, se.tex.GetPixels());
                        bytes = tex.EncodeToPNG();
                    }
                    else
                    {
                        //没有空白的，不需要重新创建图片透明填充，以方便导出时速度更快
                        bytes = se.tex.EncodeToPNG();
                    }
                     
                    File.WriteAllBytes(path, bytes);
                    AssetDatabase.ImportAsset(path);
                    if (se.temporaryTexture)
                    {
                        GameObject.DestroyImmediate(se.tex);
                    }
                }

                Debug.Log(string.Format("export: atlas={0} sprite={1} {2}/{3}", atlas.name, sprite, i + 1, atlas.spriteList.Count));
            }
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Packager", "Succeed!", "ok");
    }

    /// <summary>
    /// 将指定路径下的材质球信息变更为支持压缩并不带透明通道的贴图
    /// 资源路径在 ClientResources/AndroidAtlas/
    /// </summary>
    /// <param name="dic_path"></param>
    private static void ReclaceEtcMaterial(string dic_path)
    {
        //获取带双贴图的专用SHADER
        Shader uiEtcShader = Resources.Load<Shader>("Shaders/UIETC");

        //获取指定目录下的材质球文件路径信息
        string[] file_paths = Directory.GetFiles(dic_path, "*.mat", SearchOption.AllDirectories);

        //遍历所有材质球，并变更材质球信息
        foreach (var file_path in file_paths)
        {
            Debug.Log(file_path);

            string path = file_path.Replace("\\", "/");

            //获取材质球的资源路径
            path = path.Substring(path.IndexOf("Assets"));
            path = path.Substring(0, path.Length - 4);
            //获取到材质球
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path + ".mat");

            //获取到安卓目录的资源路径
            path = path.Replace("Resources/Atlas/", "ClientResources/AndroidAtlas/");
            //获取贴图信息
            Texture _MainTex = AssetDatabase.LoadAssetAtPath<Texture>(path + ".png");
            Texture _AlphaTex = AssetDatabase.LoadAssetAtPath<Texture>(path + "_alpha.png");

            //变更材质球的相关信息
            mat.shader = uiEtcShader;
            mat.SetTexture("_MainTex", _MainTex);
            mat.SetTexture("_AlphaTex", _AlphaTex);
        }
    }

    private static void ReclaceRGBA32Material(string dic_path)
    {
        //获取带双贴图的专用SHADER
        Shader uiRGBA32Shader = Resources.Load<Shader>("Shaders/Unlit - Transparent Colored");

        //获取指定目录下的材质球文件路径信息
        string[] file_paths = Directory.GetFiles(dic_path, "*.mat", SearchOption.AllDirectories);

        //遍历所有材质球，并变更材质球信息
        foreach (var file_path in file_paths)
        {
            Debug.Log(file_path);

            string path = file_path.Replace("\\", "/");

            //获取材质球的资源路径
            path = path.Substring(path.IndexOf("Assets"));
            path = path.Substring(0, path.Length - 4);
            //获取到材质球
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path + ".mat");

            //获取到安卓目录的资源路径
            path = path.Replace("Resources/Atlas/", "ClientResources/IosAtlas/");
            //获取贴图信息
            Texture _MainTex = AssetDatabase.LoadAssetAtPath<Texture>(path + ".png");

            //变更材质球的相关信息
            mat.shader = uiRGBA32Shader;
            mat.SetTexture("_MainTex", _MainTex);
        }
    }

    [MenuItem("Game/SplitZipFile", false, 4)]
    private static void SplitZipFile()
    {
        int size = 1024 * 1024 * 20;
        string dir = Application.dataPath + "/../AssetBundle/";

        string path = dir + "Android.zip";
        if (File.Exists(path))
            FileSplit(path, size);

        path = dir + "Iphone.zip";
        if (File.Exists(path))
            FileSplit(path, size);

        EditorUtility.DisplayDialog("", "Completed", "OK");
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 把一个文件拆分成多个散文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="singleSize"></param>
    /// <returns></returns>
    public static bool FileSplit(string filePath, int singleSize)
    {
        filePath.Replace(@"\", "/");

        string dir = filePath.Substring(0, filePath.LastIndexOf('/') + 1);
        string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1).Replace(".zip", "");

        FileStream fs = new FileStream(filePath, FileMode.Open);
        long total = fs.Length;
        long curPos = 0;

        int index = 1;
        while (curPos < total)
        {
            string path = dir + fileName + index.ToString();

            int size = singleSize;
            byte[] buff = new byte[size];

            fs.Seek(curPos, SeekOrigin.Begin);
            int rSize = fs.Read(buff, 0, size);

            if (File.Exists(path))
                File.Delete(path);
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Write(buff, 0, rSize);
            stream.Flush();
            stream.Close();
            stream.Dispose();

            curPos += rSize;

            index++;
        }

        fs.Close();
        fs.Dispose();

        return true;
    }

    /// <summary>
    /// 将多个散文件合并成1个文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool FileMerge(string filePath)
    {
        filePath.Replace(@"\", "/");

        string dir = filePath.Substring(0, filePath.LastIndexOf('/') + 1);
        string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1).Replace(".zip", "");

        if (File.Exists(filePath))
            File.Delete(filePath);

        FileStream fs = new FileStream(filePath, FileMode.Create);

        long curPos = 0;
        int index = 1;

        while (true)
        {
            string path = dir + fileName + index.ToString();
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buff = new byte[stream.Length];
                int rSize = stream.Read(buff, 0, buff.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();

                fs.Seek(curPos, SeekOrigin.Begin);
                fs.Write(buff, 0, rSize);
                fs.Flush();

                curPos += rSize;
            }
            else
            {
                break;
            }

            index++;
        }

        fs.Close();
        fs.Dispose();

        return true;
    }

    /// <summary>
    /// 将选中目录中的预制体所关联图集列表出来
    /// </summary>
    [MenuItem("Game/ListAllAtlas", false, 60)]
    public static void ListAllAtlas()
    {
        Dictionary<string, UISprite[]> atlasInfo = new Dictionary<string, UISprite[]>();
        Object[] prefabs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object prefab in prefabs)
        {
            if (!(prefab is GameObject))
                continue;

            //获取Sprite
            UISprite[] sprites = ((GameObject)prefab).transform.GetComponentsInChildren<UISprite>(true);
            if (sprites == null || sprites.Length == 0)
                continue;

            //获取路径
            string path = AssetDatabase.GetAssetPath(prefab);
            atlasInfo.Add(path, sprites);
        }

        if (atlasInfo.Count > 0)
        {
            //写文件
            FileStream fs = new FileStream("D:\\AtlasInfoList.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Unicode);

            foreach (KeyValuePair<string, UISprite[]> pairs in atlasInfo)
            {
                for (int i = 0; i < pairs.Value.Length; ++i)
                    sw.WriteLine(pairs.Key + "\t" + pairs.Value[i].atlas + "\t" + pairs.Value[i].spriteName);
            }

            sw.Close();
            fs.Close();
        }
    }
}
