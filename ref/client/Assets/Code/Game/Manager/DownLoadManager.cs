using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System;
public class DownLoadManager
{
    private WebClient mClient = new WebClient();
    //操作完成调用方法//
    public delegate void OnFinished();
    public OnFinished mFinished;
    public delegate void OnDownLoadException(string vPath);
    public OnDownLoadException mDownLoadException;
    public bool LoadFile(string vURL, string vPath)
    {
        if (!Directory.Exists(Path.GetDirectoryName(vPath)))
            Directory.CreateDirectory(Path.GetDirectoryName(vPath));
        bool tmpResult = true;
        if (tmpResult) 
        {
            try
            {
                mClient.DownloadFile(vURL, vPath);
            }
            catch (Exception e)
            {
                //CommonFunction.DebugLog(string.Format("[Exception][OperateClassDownLoad LoadFile]: [{0}][{1}][{2}]", vURL, vPath, e.Message));
                // File.Delete(vPath);
                Debug.LogError(e.Message + "  " + vURL);
                //CommonFunction.DebugLog(string.Format("[Exception][OperateClassDownLoad LoadFile]: [{0}][{1}][{2}]", vURL, vPath, Path.GetDirectoryName(vPath)));
                tmpResult = false;
                return tmpResult;
                throw e;
            }
        }
        return tmpResult;
    }
}
