using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class AnnouncementItem
{
    public string title;
    public string label;
    public string info;
    public uint is_open;
    public float version;
}


public class AnnouncementConfig
{
    string name = GlobalConst.DIR_XML_ANNOUNCEMENT;
    bool isLoadFinish = false;
    private object LockObject = new object();

    List<AnnouncementItem> AnnouncementItems = new List<AnnouncementItem>();

    public AnnouncementConfig()
    {
        Initialize();
    }

    void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ANNOUNCEMENT, LoadFinish);
        //Read();
    }

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }

        UIManager.Instance.LoginCtrl.SetNoticeActive();
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }

        AnnouncementItems.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name, text);
        //解析XML的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            AnnouncementItem item = new AnnouncementItem();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "name")
                {
                    item.title = xel.InnerText;
                }
                else if (xel.Name == "label")
                {
                    item.label = xel.InnerText;
                }
                else if (xel.Name == "info")
                {
                    item.info = xel.InnerText;
                }
                else if (xel.Name == "is_open")
                {
                    uint.TryParse(xel.InnerText, out item.is_open);
                }
                else if (xel.Name == "version")
                {
                    float.TryParse(xel.InnerText, out item.version);
                }
            }

            AnnouncementItems.Add(item);
		}
		Logger.ConfigEnd(name);
    }

    public AnnouncementItem GetOpenItem()
    {
        AnnouncementItem ans = AnnouncementItems.Find(x => x.is_open == 1);
        return AnnouncementItems.Find(x => x.is_open == 1);
    }
}