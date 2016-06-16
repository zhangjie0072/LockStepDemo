using System;
using System.IO;
using System.Net;

using UnityEngine;

    /// <summary>
    /// 下载对象
    /// </summary>
public class Downloader
{
    public delegate void OnDownFinished(string url);
    public OnDownFinished onDownFinished;

    public delegate void OnDownLoadPeracent(long currentSize, long totalSize);
    public OnDownLoadPeracent onDownLoadPeracent = null;

    /// <summary>
    /// 链接地址
    /// </summary>
    private string url;
    /// <summary>
    /// 文件名
    /// </summary>
    private string fileName;
    /// <summary>
    /// 总大小
    /// </summary>
    private long totalSize;
    /// <summary>
    /// 下载文件的路径，作为临时文件
    /// </summary>
    private string filePath;
    /// <summary>
    /// 文件存储目录
    /// </summary>
    private string directory;
    /// <summary>
    /// 当前进度
    /// </summary>
    private long currentSize;
    /// <summary>
    /// 是否完成
    /// </summary>
    private bool isFinished;
    /// <summary>
    /// 缓冲池大小
    /// </summary>
    private int bufferSize = 10240;
    /// <summary>
    /// 生成文件路径
    /// </summary>
    public string FilePath
    {
        get { return filePath; }
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="url"></param>
    public Downloader(string url, string directory)
    {
        isFinished = false;
        Init(url, directory);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="url"></param>
    /// <param name="directory"></param>
    private void Init(string url, string directory)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("url is null or empty!!!");
            return;
        }

        this.url = url;
        this.directory = directory;
        this.fileName = url.Substring(url.LastIndexOf('/') + 1);
        this.filePath = Path.Combine(this.directory, fileName);

        Debug.Log("url:" + url);
        Debug.Log("filePath:" + filePath);
    }

    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="url"></param>
    /// <param name="range"></param>
    public bool Download()
    {
        //资源包存在，则表示已经下载完成了
        if (File.Exists(this.filePath))
            return true;

        string tmpPath = this.filePath + ".tmp";
        FileStream fs = new FileStream(tmpPath , FileMode.OpenOrCreate, FileAccess.ReadWrite);
        this.currentSize = fs.Length;
        try
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.AddRange((int)this.currentSize);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response == null)
            {
                return false;
            }
            this.totalSize = this.currentSize + response.ContentLength;//如果下载了一半，ContentLength为剩余的大小

            Debug.Log(string.Format("from:{0}   total size:{1}", this.currentSize, this.totalSize));

            if (this.currentSize >= this.totalSize)
            {
                Debug.Log("isFinished 111");

                this.isFinished = true;
                if (onDownFinished != null)
                    onDownFinished(this.url);

                return true;
            }

            
            fs.Seek(this.currentSize, SeekOrigin.Begin);
            byte[] buffer = this.Buffer;
            using (Stream stream = response.GetResponseStream())
            {
                int size = stream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    //只将读出的字节写入文件
                    fs.Write(buffer, 0, size);

                    this.currentSize += size; //更新当前进度
                    size = stream.Read(buffer, 0, buffer.Length);

                    if (onDownLoadPeracent != null)
                        onDownLoadPeracent(currentSize, totalSize);

                    //Debug.Log("currentSize:" + this.currentSize);
                }
            }

            //关闭文件流
            fs.Flush();
            fs.Close();
            fs.Dispose();
            fs = null;
            
            if (this.currentSize >= this.totalSize)
            {
                Debug.Log("isFinished 222");

                this.isFinished = true;
                if (onDownFinished != null)
                    onDownFinished(this.url);

                //下载完成后，将临时后缀修改为正常后缀
                ChangeFileName(tmpPath, this.filePath);

                return true;
            }

            //如果返回的response头中Content-Range值为空，说明服务器不支持Range属性，不支持断点续传,返回的是所有数据
            if (response.Headers["Content-Range"] == null)
            {
                Debug.Log("isFinished 333");

                this.isFinished = true;
                if (onDownFinished != null)
                    onDownFinished(this.url);

                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);
            return false;
        }
        finally
        {
            if (fs != null)
            {
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
        }

        return false;
    }

    /// <summary>
    /// FTP下载资源包
    /// Warning 还没有测试过的函数
    /// </summary>
    /// <returns></returns>
    public bool FtpDownLoad()
    {
        FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        this.currentSize = fs.Length;

        try
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(url);
            //设定用户名和密码
            //request.Credentials = new System.Net.NetworkCredential("username", "password");
            //Method WebRequestMethods.Ftp.DownloadFile("RETR")设定
            request.Method = System.Net.WebRequestMethods.Ftp.DownloadFile;
            //要求终了后关闭连接
            request.KeepAlive = false;
            //使用ASCII方式传送
            request.UseBinary = false;
            //设定PASSIVE方式无效
            request.UsePassive = false;
            request.ContentOffset = this.currentSize;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            if (response == null)
            {
                return false;
            }
            this.totalSize = response.ContentLength;

            Debug.Log(string.Format("from:{0}   total size:{1}", this.currentSize, this.totalSize));

            if (this.currentSize >= this.totalSize)
            {
                Debug.Log("isFinished 111");

                this.isFinished = true;
                if (onDownFinished != null)
                    onDownFinished(this.url);

                return true;
            }


            fs.Seek(this.currentSize, SeekOrigin.Begin);
            byte[] buffer = this.Buffer;
            using (Stream stream = response.GetResponseStream())
            {
                int size = stream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    //只将读出的字节写入文件
                    fs.Write(buffer, 0, size);

                    this.currentSize += size; //更新当前进度
                    size = stream.Read(buffer, 0, buffer.Length);

                    if (onDownLoadPeracent != null)
                        onDownLoadPeracent(currentSize, totalSize);

                    //Debug.Log("currentSize:" + this.currentSize);
                }

                if (this.currentSize >= this.totalSize)
                {
                    Debug.Log("isFinished 222");

                    this.isFinished = true;
                    if (onDownFinished != null)
                        onDownFinished(this.url);

                    return true;
                }

                //如果返回的response头中Content-Range值为空，说明服务器不支持Range属性，不支持断点续传,返回的是所有数据
                if (response.Headers["Content-Range"] == null)
                {
                    Debug.Log("isFinished 333");

                    this.isFinished = true;
                    if (onDownFinished != null)
                        onDownFinished(this.url);

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);
            return false;
        }
        finally
        {
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private byte[] Buffer
    {
        get
        {
            if (this.bufferSize <= 0)
            {
                this.bufferSize = 10240;
            }
            return new byte[this.bufferSize];
        }
    }

    public static string GetSizeStr(long size)
    {
        double mb = (double)size / 1024 / 1024;
        return mb.ToString("0.00") + "MB";
    }

    /// <summary>
    /// 修改文件名字
    /// </summary>
    /// <param name="srcFileName"></param>
    /// <param name="destFileName"></param>
    public static void ChangeFileName(string srcFileName, string destFileName)
    {
        if (System.IO.File.Exists(srcFileName))
        {
            System.IO.File.Move(srcFileName, destFileName);
        }
    }
}