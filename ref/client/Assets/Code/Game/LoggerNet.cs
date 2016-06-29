using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class LoggerNet
{
    #region 网络相关
    /* *********************************************
     * 日志网络
     * 客户端作为日志系统的服务器存在
     * 日志只向终端发送数据，不收任何数据
     * *********************************************/
    public static Mutex mutex = new Mutex();
    public static Queue<string> log_list = new Queue<string>();
    public static Thread receiveThread = null;

    private static Socket serverSocket = null;
    private static Socket clientSocket = null;
    private static int logNetPort = 6000;

    public static void StartNetService()
    {
#if UNITY_EDITOR
        ;
#elif UNITY_IPHONE || UNITY_ANDROID
        //服务器IP地址  
        IPAddress ip = IPAddress.Any;
        IPEndPoint iep = new IPEndPoint(ip, logNetPort);
        //创建服务器的socket对象
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(iep);  //绑定IP地址：端口  
        serverSocket.Listen(10);    //设定最多10个排队连接请求  
        UnityEngine.Debug.Log(string.Format("启动监听{0}成功", serverSocket.LocalEndPoint.ToString()));
        //监听客户端的链接
        serverSocket.BeginAccept(new AsyncCallback(Accept), serverSocket);
#endif
    }

    private static void Accept(IAsyncResult iar)
    {
        //获取到客户端的socket信息
        clientSocket = serverSocket.EndAccept(iar);
        receiveThread = new Thread(Send);
        receiveThread.Start();
    }

    /// <summary>
    /// 检查队列是否有消息
    /// 有消息的时候发送给终端
    /// </summary>
    private static void Send()
    {
        try
        {
            while (true)
            {
                int count = log_list.Count;

                if (count == 0)
                {
                    Thread.Sleep(50);
                    continue;
                }

                while (count > 0)
                {
                    mutex.WaitOne();
                    string str = log_list.Dequeue();
                    mutex.ReleaseMutex();

                    byte[] byteData = Encoding.UTF8.GetBytes(str);
                    int len = clientSocket.Send(byteData);
                    if (len == 0)
                    {
                        UnityEngine.Debug.Log("发送失败");
                        break; 
                    }

                    count--;
                }
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.Log(ex.StackTrace.ToString());
        }
        
    }

#endregion
}