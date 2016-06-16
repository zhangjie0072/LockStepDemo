using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Collections;

public class ServerIPAndPort
{
    public string server_type;
    public string IP;
    public int EndPoint;
}

public class ServerDataConfig
{
    string name = GlobalConst.DIR_XML_SERVERIPANDENDPOINT;
    private bool IsCached = false;
    private ServerIPAndPort serverData = new ServerIPAndPort();

    public ServerDataConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        //ResourceLoadManager.Instance.GetConfigResource(name);
        ResourceLoadManager.Instance.GetConfigResource(name, ParseConfig);
        //ParseConfig();
    }

    public void ParseConfig(string vPath, object obj)
    //public void ParseConfig()
    {
        //string text = ResourceLoadManager.Instance.GetConfigText(name);
        //if (text == null)
        //{
        //    Logger.LogError("LoadConfig failed: " + name);
        //    return;
        //}

		Logger.ConfigBegin(name);
        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name, obj);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            foreach (XmlElement xe in land)
            {
                if (xe.Name == "Server_type")
                {
                    serverData.server_type = xe.InnerText;
                }
                if (xe.Name == "Server_ip")
                {
                    serverData.IP = xe.InnerText;
                }
                else if (xe.Name == "Server_endpoint")
                {
                    serverData.EndPoint = int.Parse(xe.InnerText);
                }
            }
            IsCached = true;
        }

		Logger.ConfigEnd(name);
    }

    public ServerIPAndPort GetServerIPAndPort()
    {
        if (!IsCached)
        {
            ResourceLoadManager.Instance.GetConfigResource(name, ParseConfig);
            //ResourceLoadManager.Instance.GetConfigResource(name);
            //ParseConfig();
        }

        return serverData;
    }
}
