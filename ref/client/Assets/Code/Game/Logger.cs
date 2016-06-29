using System;
using UnityEngine;

/// <summary>
/// Unity Debug.Log 输出时的回调实现
/// </summary>
public class LoggerHandler
{
    public static void Init()
    {
        Debug.logger.logEnabled = true;
        Application.logMessageReceivedThreaded += HandleLog;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        LoggerHandler.WriteLog(logString, stackTrace, type);
    }

    private static void WriteLog(string logString, string stackTrace, LogType type)
    {
        string log = string.Format("[{0}][{1}] {2} \n {3} \n",
                                        type.ToString(),
                                        System.DateTime.Now.ToString("HH:mm:ss"),
                                        logString,
                                        stackTrace);

        if (LoggerNet.receiveThread != null)
        {
            LoggerNet.mutex.WaitOne();
            LoggerNet.log_list.Enqueue(log);
            LoggerNet.mutex.ReleaseMutex();
        }
    }
}

//public class Debug
//{
//    public static void LogWarning(object message)
//    {
//        Debug.LogWarning(message);
//    }

//    public static void LogError(object message)
//    {
//        Debug.LogError(message);
//    }

//    /// <summary>
//    /// 输出配置文件加载日志(开始解析)
//    /// </summary>
//    /// <param name="name"></param>
//	public static void ConfigBegin(string name)
//    {
//        Debug.Log("Config reading " + name + " ...");
//    }
//    /// <summary>
//    /// 输出配置文件加载日志(结束解析)
//    /// </summary>
//    /// <param name="name"></param>
//    public static void ConfigEnd(string name)
//    {
//        //LoggerHandler.WriteLog("Config reading " + name + " end...", LogType.LOG);
//    }
//}