using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

/// <summary>
/// 版本更新管理器
/// </summary>
public class VersionUpdateManager : Singleton<VersionUpdateManager>
{
    public delegate void OnDecompressionPeracent(float peracent);
    /// <summary>
    /// 解压进度事件
    /// </summary>
    public OnDecompressionPeracent onDecompressionPeracent = null;

    public delegate void OnDownLoadPeracent(long currentSize, long totalSize);
    /// <summary>
    /// 下载进度事件
    /// </summary>
    public OnDownLoadPeracent onDownLoadPeracent = null;

    public delegate void OnUpdateFinished(int ver);
    /// <summary>
    /// 更新完成事件
    /// </summary>
    public OnUpdateFinished onUpdateFinished;

    public delegate void OnUpdateFaild();
    /// <summary>
    /// 更新失败事件
    /// </summary>
    public OnUpdateFaild onUpdateFaild = null;

    public delegate void OnUpdateTips(UpdateVersionTipsScript.TipsMessageType msgType, long size);
    /// <summary>
    /// 版本更新提示
    /// </summary>
    public OnUpdateTips onUpdateTips = null;

    public static List<UpdateCmd> updateCmdList = new List<UpdateCmd>();

    //失败重试
    public static bool isRetry = false;

    /// <summary>
    /// 压缩包中包含的文件数互谅
    /// </summary>
    private int fileCount = 0;

    /// <summary>
    /// 已解压出来的文件数量，与fileCount相等则表示解压完成
    /// </summary>
    private int fileCompletedCount = 0;

    /// <summary>
    /// 解压的完成百分比
    /// </summary>
    private float peracent = 0;

    /// <summary>
    /// 本地版本号
    /// </summary>
    private int localVersion = 0;

    /// <summary>
    /// 版本号 更新 列表(版本号，压缩包中的文件数量)
    /// </summary>
    private Dictionary<int, VersionInfo> verList = new Dictionary<int,VersionInfo>();

    private StreamAssetVersionInfo streamAssetVersionInfo = new StreamAssetVersionInfo();

    /// <summary>
    /// Android OR IOS
    /// </summary>
    public static string GamePlat
    {
        get
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IPHONE
            return "Iphone";
#else 
            return "Editor";
#endif
        }
    }

    /// <summary>
    /// 下载地址
    /// </summary>
    string urlRoot = ResPath.CHECKVERSIONADDRESS+"update";
    //string urlRoot = "http://192.168.1.29/update";

    /// <summary>
    /// 存放资源的可读写目录
    /// </summary>
    public static readonly string persistentDataPath = Application.persistentDataPath;

#if UNITY_EDITOR
    static readonly string streamingAssetsPath = "file:///" + Application.streamingAssetsPath;
#elif UNITY_IOS
     static readonly string streamingAssetsPath = "file://" + Application.streamingAssetsPath;
#elif UNITY_ANDROID
    static readonly string streamingAssetsPath = Application.streamingAssetsPath;
#else
    static readonly string streamingAssetsPath = "file:///" + Application.streamingAssetsPath;
#endif

    public static void AddUpdateCmd(UpdateCmdType cmdType, object obj = null)
    {
        UpdateCmd cmd = new UpdateCmd();
        cmd.cmdType = cmdType;
        cmd.obj = obj;

        lock (updateCmdList)
        {
            updateCmdList.Add(cmd);
        }
    }

    public VersionUpdateManager()
    {
        localVersion = VersionUpdateManager.VersionTag;  //本地版本号
        Logger.Log("persistentDataPath:" + persistentDataPath);
        Logger.Log("localVersion:" + localVersion);
    }

    /// <summary>
    /// 更新版本
    /// 客户端有可能长时间没更新，会一次性更新多个版本
    /// </summary>
    public void DownLoadAndUpdateVersion()
    {
        try
        {
            string directoryUpdateRoot = string.Format("{0}/update/", persistentDataPath);
            if (!Directory.Exists(directoryUpdateRoot))
            {
                Directory.CreateDirectory(directoryUpdateRoot);
            }

            foreach (var ver in verList)
            {
                if (localVersion < ver.Key)
                {
                    string url = string.Format("{0}/{1}/{2}.zip", urlRoot, ver.Key, GamePlat);

                    string directory = string.Format("{0}{1}/", directoryUpdateRoot, ver.Key);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    //下载资源包
                    Downloader downLoader = new Downloader(url, directory);
                    downLoader.onDownLoadPeracent = OnDownLoadPeracentChanged;
                    bool resultDownLoad = downLoader.Download();
                    if (!resultDownLoad)
                    {
                        if (this.onUpdateFaild != null)
                        {
                            this.onUpdateFaild();
                        }
                        Logger.LogError("Download Error!!!" + url);
                        return;
                    }

                    Logger.Log("Download over!!!");

                    //解压已下载的资源包
                    fileCount = ver.Value.file_num;
                    if (!this.ExtractZip(downLoader.FilePath, persistentDataPath + "/"))
                    {
                        if (this.onUpdateFaild != null)
                            this.onUpdateFaild();
                    }

                    //通过包里面的更新描述删除不再使用的资源文件
                    this.DelFile();

                    //更新本地版本号
                    localVersion = ver.Key;
                    VersionUpdateManager.VersionTag = localVersion;
                    
                    Thread.Sleep(10);
                }
            }

            //删除zip包
            Directory.Delete(directoryUpdateRoot, true);

            if (onUpdateFinished != null)
                this.onUpdateFinished(localVersion);
        }
        catch (Exception ex)
        {
            Logger.Log(ex.StackTrace);

            if (this.onUpdateFaild != null)
                this.onUpdateFaild();
        }
    }

    public void StreamAssetInitVersion(object obj)
    {
        try
        {
            StreamAssetVersionInfo param = (StreamAssetVersionInfo)obj;

            //1、解压资源包
            fileCount = param.file_num;
            this.ExtractZip(param.path, persistentDataPath + "/");

            //2、通过包里面的更新描述删除不再使用的资源文件
            this.DelFile();

            //3、删除zip包
            if (File.Exists(param.path))
                File.Delete(param.path);

            //4、更新本地版本号
            localVersion = param.ver;
            VersionUpdateManager.VersionTag = localVersion;

            VersionUpdateManager.AddUpdateCmd(UpdateCmdType.LoadVersionList);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.StackTrace);
        }
    }

    public IEnumerator GetStreamAssetInfo()
    {
        if (localVersion == 0)
        {
            string verPath = streamingAssetsPath + "/version.txt";
            WWW wwwVer = new WWW(verPath);
            yield return wwwVer;

            if (!string.IsNullOrEmpty(wwwVer.error))
            {
                Logger.LogWarning("StreamAssetCopyToPersistentDataPath version!!! " + wwwVer.error);
                //if (this.onUpdateFaild != null)
                //    this.onUpdateFaild();

                //streamAsset下没有东西，直接去网络上获取资源
                VersionUpdateManager.AddUpdateCmd(UpdateCmdType.LoadVersionList);
            }
            else
            {
                //资源版本号@zip压缩包中的文件数@被拆分的文件流数量
                var list = SplitString(wwwVer.text, '@');
                Logger.Log("version.txt => " + wwwVer.text);
                if (list != null && list.Count == 3)
                {
                    streamAssetVersionInfo.ver = list[0];
                    streamAssetVersionInfo.file_num = list[1];
                    streamAssetVersionInfo.pack_num = list[2];
                }

                VersionUpdateManager.AddUpdateCmd(UpdateCmdType.StreamAssetInit);
            }
        }
        else
        {
            VersionUpdateManager.AddUpdateCmd(UpdateCmdType.StreamAssetInit);
        }
    }

    public IEnumerator StreamAssetInit()
    {
        if (localVersion != 0)
        {
             VersionUpdateManager.AddUpdateCmd(UpdateCmdType.LoadVersionList);
        }
        else
        {
            UpdateVersionScript.SetLogoActive(false);
            bool isOk = true;
            int curPos = 0;
            string name = GamePlat;
            string desPath = persistentDataPath + "/" + name + ".zip";
            if (File.Exists(desPath))
                File.Delete(desPath);
            FileStream stream = new FileStream(desPath, FileMode.Create);

            for (int index = 1; index <= streamAssetVersionInfo.pack_num; index++)
            {
                string srcPath = streamingAssetsPath + "/" + name + index.ToString();

                WWW www = new WWW(srcPath);
                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                {
                    isOk = false;
                    Logger.LogWarning(srcPath);
                    Logger.LogError("StreamAssetCopyToPersistentDataPath Error!!! " + www.error);
                    if (this.onUpdateFaild != null)
                        this.onUpdateFaild();

                    //失败了，则从网络获取更新列表（最小打包的方式，本地不放置资源包）
                    VersionUpdateManager.AddUpdateCmd(UpdateCmdType.LoadVersionList);

                    break;
                }
                else
                {
                    stream.Seek(curPos, SeekOrigin.Begin);
                    stream.Write(www.bytes, 0, www.bytes.Length);
                    stream.Flush();
                    curPos += www.bytes.Length;
                }
            }

            stream.Close();
            stream.Dispose();

            if (isOk)
            {
                streamAssetVersionInfo.path = desPath;

                Thread thread = new Thread(new ParameterizedThreadStart(StreamAssetInitVersion));
                thread.Start(streamAssetVersionInfo);
            }
        }
    }

    public IEnumerator LoadVersionList()
    {
        WWW wwwVerList = new WWW(urlRoot + "/version_list.txt");
        yield return wwwVerList;

        if (!string.IsNullOrEmpty(wwwVerList.error))
        {
            Logger.Log("网络故障或没有找到更新列表文件=>" + urlRoot + "/version_list.txt");
            if (this.onUpdateFaild != null)
                this.onUpdateFaild();

            yield return null;
        }
        else
        {
            verList.Clear();

            //版本描述信息 版本号@文件数 换行
            Logger.Log("version_list:" + wwwVerList.text);
            byte[] strBytes = Encoding.UTF8.GetBytes(wwwVerList.text);
            MemoryStream stream = new MemoryStream(strBytes);
            StreamReader reader = new StreamReader(stream);

            string lineInfo;
            bool isFirstLine = true;
            bool isUpdate = false;      //是否需要更新
            long iUpdateTotalSize = 0;

            while ((lineInfo = reader.ReadLine()) != null)
            {
                var list = SplitString(lineInfo, '@');
                if (list == null || list.Count != 3)
                    continue;

                //大版本更新检查
                if (isFirstLine && localVersion > 0)
                {
                    isFirstLine = false;
                    int sBig = list[0] / 1000000;
                    int lBig = localVersion / 1000000;
                    if (sBig > lBig)
                    {
                        iUpdateTotalSize = -1;
                        break;
                    }
                }

                VersionInfo info = new VersionInfo();
                info.ver = list[0];
                info.file_num = list[1];
                info.zip_size = list[2];
                verList.Add(info.ver, info);

                //计算本次更新总共需要下载资源大小
                if (info.ver > localVersion)
                {
                    isUpdate = true;
                    string directory = string.Format("{0}/update/{1}/{2}.zip", persistentDataPath, info.ver, GamePlat);
                    if (!File.Exists(directory))
                        iUpdateTotalSize += info.zip_size;
                }
            }

            if( iUpdateTotalSize == -1)
            {
                UpdateVersionScript.SetLogoActive(false);
                Debug.Log("需要大版本更新游戏，请市场下载最新版本！");
                if (onUpdateTips != null)
                    onUpdateTips(UpdateVersionTipsScript.TipsMessageType.VersionUpdateBig, 0);
            }
            else if( iUpdateTotalSize == 0)
            {
                if (isUpdate) //已下载好资源，没解压。需要下载的资源流量也是0
                {
                    this.StartUpdate(); 
                }
                else
                {
                    Logger.Log("已是最新版本，无需要更新游戏");
                    if (this.onUpdateFinished != null)
                        this.onUpdateFinished(localVersion);
                }
            }
            else
            {
                if (!isRetry)
                {
                    UpdateVersionScript.SetLogoActive(false);
                    Logger.Log("提示玩家是否现在更新");
                    if (onUpdateTips != null)
                        onUpdateTips(UpdateVersionTipsScript.TipsMessageType.VersionUpdateSmall, iUpdateTotalSize);
                }
                else
                {
                    VersionUpdateManager.Instance.StartUpdate();
                }
            }
        }
    }

    public void StartUpdate()
    {
        Logger.Log("本地版本小于服务器版本，需要网络下载更新");
        Thread thread = new Thread(new ThreadStart(DownLoadAndUpdateVersion));
        thread.Start();
    }

    /*
    public IEnumerator CheckVersion()
    {
        localVersion = VersionUpdateManager.VersionTag;  //本地版本号
        Logger.Log("persistentDataPath:" + persistentDataPath);
        Logger.Log("localVersion:" + localVersion);

        #region 填充更新列表
        
        #endregion

        if (localVersion > 0)
        {
            #region 如果本地资源已经初始化了，则直接检查是否需要网络更新
            bool isUpdate = false;
            long iUpdateTotalSize = 0;
            foreach (var info in verList)
            {
                if (localVersion < info.Key)
                {
                    iUpdateTotalSize += info.Value.zip_size;
                }
            }

            if( isUpdate)
            {
                if (onUpdateTips != null)
                    onUpdateTips(false);

                Logger.Log("本地版本小于服务器版本，需要网络下载更新");
                Thread thread = new Thread(new ThreadStart(DownLoadAndUpdateVersion));
                thread.Start();
            }
            #endregion

            Logger.Log("已是最新版本，无需要更新游戏");
            if (this.onUpdateFinished != null)
                this.onUpdateFinished(localVersion);
        }
        else
        {
            ThreaParam param = new ThreaParam();

            #region 获取streamingAssetsPath中资源的版本信息
            string verPath = streamingAssetsPath + "/version.txt";
            WWW wwwVer = new WWW(verPath);
            yield return wwwVer;
            if (!string.IsNullOrEmpty(wwwVer.error))
            {
                Logger.LogError("StreamAssetCopyToPersistentDataPath version!!! " + wwwVer.error);
            }
            else
            {
                //资源版本号@zip压缩包中的文件数@被拆分的文件流数量
                var list = SplitString(wwwVer.text, '@');
                Logger.Log("version.txt => " + wwwVer.text);
                if (list != null && list.Count == 3)
                {
                    param.ver = list[0];
                    param.file_num = list[1];
                    param.pack_num = list[2];
                }
            }
            #endregion


            #region 获取streamingAssetsPath中资源文件

            int curPos = 0;
            bool isOk = true;
            string name = GamePlat;
            string desPath = persistentDataPath + "/" + name + ".zip";
            if (File.Exists(desPath))
                File.Delete(desPath);
            FileStream stream = new FileStream(desPath, FileMode.Create);

            for (int index = 1; index <= param.pack_num; index++ )
            {
                string srcPath = streamingAssetsPath + "/" + name + index.ToString();

                WWW www = new WWW(srcPath);
                yield return www;

                if (!string.IsNullOrEmpty(www.error))
                {
                    isOk = false;
                    Logger.LogWarning(srcPath);
                    Logger.LogError("StreamAssetCopyToPersistentDataPath Error!!! " + www.error);
                    break;
                }
                else
                {
                    stream.Seek(curPos, SeekOrigin.Begin);
                    stream.Write(www.bytes, 0, www.bytes.Length);
                    stream.Flush();
                    curPos += www.bytes.Length;
                }
            }

            stream.Close();
            stream.Dispose();

            if (isOk)
            {
                param.path = desPath;

                Thread thread = new Thread(new ParameterizedThreadStart(StreamAssetInitVersion));
                thread.Start(param);
            }

            #endregion
        }
    }
    */

    /// <summary>
    /// 删除更新时标注不再使用的文件列表
    /// </summary>
    public void DelFile()
    {
        try
        {
            string dir = string.Format("{0}/{1}/", persistentDataPath, GamePlat);
            string path = string.Format("{0}dell_list.txt", dir);
            
            Logger.Log("version path=>" + path);
            if (!File.Exists(path))
            {
                Logger.Log("没有需要删除的文件!!! dell_list.txt为空！");
                return;
            }

            StreamReader sr = new StreamReader(path);
            try
            {
                string lineInfo;
                while ((lineInfo = sr.ReadLine()) != null)
                {
                    path = dir + lineInfo;
                    path = path.Replace('\\', '/');
                    Logger.Log("dell file" + path);

                    if (File.Exists(path))
                        File.Delete(path);
                    else
                        Logger.LogWarning("file is not exists:" + path);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.StackTrace);
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
        }
        catch(Exception ex)
        {
            Logger.LogError(ex.StackTrace);
        }
    }

    /// <summary>
    /// 更新版本信息
    /// </summary>
    /// <param name="ver"></param>
    public static int VersionTag
    {
        get
        {
            string path = string.Format("{0}/version.txt", persistentDataPath);
            if (!File.Exists(path))
                return 0;

            StreamReader sr = new StreamReader(path);
            string str = sr.ReadLine();
            sr.Close();
            sr.Dispose();

            int tag = 0;
            int.TryParse(str, out tag);
            return tag;
        }
        set
        {
            try
            {
                VersionUpdateManager.AddUpdateCmd(UpdateCmdType.ChangeVersionLabel, value);

                string path = string.Format("{0}/version.txt", persistentDataPath);
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(value.ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.StackTrace);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static List<int> SplitString(string str, char tag)
    {
        List<int> list = null;

        if (string.IsNullOrEmpty(str))
            return list;

        string[] array = str.Split(tag);
        if (array.Length <= 0)
            return list;

        int i;
        foreach(var s in array)
        {
            if (int.TryParse(s, out i))
            {
                if (list == null)
                    list = new List<int>();
                list.Add(i);
            }
        }
        return list;
    }

    /// <summary>
    /// 将版本号转换为数字
    /// </summary>
    /// <param name="version">0.0.0</param>
    /// <returns></returns>
    public static int VersionStrToInt(string version)
    {
        var array = SplitString(version, '.');
        if (array != null && array.Count == 3)
        {
            int i = array[0] * 1000000 + array[1] * 1000 + array[2];
            return i;
        }

        return 0;
    }

    /// <summary>
    /// 将版本号转换为字符
    /// </summary>
    /// <param name="version">0.0.0</param>
    /// <returns></returns>
    public static string VersionIntToStr(int version)
    {
        int v1 = version / 1000000;
        int v2 = (version % 1000000) / 1000;
        int v3 = version % 1000;

        return string.Format("{0}.{1}.{2}", v1, v2, v3);
    }

    /// <summary>
    /// 下载进度更新回调
    /// </summary>
    /// <param name="peracent"></param>
    public void OnDownLoadPeracentChanged(long currentSize, long totalSize)
    {
        if (onDownLoadPeracent != null)
            onDownLoadPeracent(currentSize, totalSize);
    }

    #region 解压资源相关
    public void CompletedFileHandler(string filePath)
    {
        fileCompletedCount++;

        peracent = (float)fileCompletedCount / (float)fileCount;
        if (onDecompressionPeracent != null)
        { 
            onDecompressionPeracent(peracent);
        }
    }

    public bool ExtractZip(string zipFilePath, string targetDirectory)
    {
        fileCompletedCount = 0;

        Logger.Log("filePath=>" + zipFilePath);
        Logger.Log("targetDirectory=>" + targetDirectory);

        if (!File.Exists(zipFilePath))
        {
            Logger.LogError("zip file is not exists!!!");
            return false;
        }
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

        ZipInputStream s = null;
        ZipEntry theEntry = null;
        string filePath;
        FileStream streamWriter = null;
        try
        {
            s = new ZipInputStream(File.OpenRead(zipFilePath));
            byte[] buff = new byte[2048];
            //s.Password = Password;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                if (theEntry.Name != String.Empty)
                {
                    filePath = Path.Combine(targetDirectory, theEntry.Name);
                    filePath = filePath.Replace(@"\", @"/");

                    //判断文件路径是否是文件夹
                    if (filePath.EndsWith("/"))
                    {
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);
                        continue;
                    }

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    streamWriter = File.Create(filePath);
                    int size = 0;
                    while (true)
                    {
                        size = s.Read(buff, 0, buff.Length);
                        if (size > 0)
                            streamWriter.Write(buff, 0, size);
                        else
                            break;
                    }

                    //单个文件解压完成
                    CompletedFileHandler(filePath);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError("error:" + ex.ToString());
            return false;
        }
        finally
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
                streamWriter = null;
            }
            if (theEntry != null)
            {
                theEntry = null;
            }
            if (s != null)
            {
                s.Close();
                s = null;
            }
            GC.Collect();
            GC.Collect(1);

            Logger.Log("GC.Collect!!!");
        }
    }

    #endregion
}


/// <summary>
/// 多线程参数（初始化StreamAsset中的资源）
/// </summary>
public class StreamAssetVersionInfo
{
    public int ver;         //资源版本
    public int file_num;    //zip压缩包中的文件数量
    public int pack_num;    //zip被分了多少个文件流
    public string path;     //zip存放的路径
}

public class VersionInfo
{
    public int ver;         //资源版本
    public int file_num;    //zip压缩包中的文件数量
    public int zip_size;    //zip包的大小（字节）
}

public enum UpdateCmdType
{
    GetStreamAssetInfo, //在StreamAsset中获取Version信息
    StreamAssetInit,    //从StreamAsset中初始化资源
    LoadVersionList,    //装载服务器中存放的版本信息列表（用于小版本更新）
    UpdateVersionFinish,//更新结束
    ChangeVersionLabel, //变更版本号显示信息

    UpdateMessageTips,  //弹出的更新确认框
}

public enum UpdateState
{

}

public class UpdateCmd
{
    public UpdateCmdType cmdType;
    public object obj;
}