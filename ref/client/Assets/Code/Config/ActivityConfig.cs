using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;
using fogs.proto.msg;

  public class ActivityData
    {
        public uint id;
        public uint activity;
        public List<uint> level = new List<uint>();
        public List<uint> awards = new List<uint>();
        public string icon;

    }

public class ActivityConfig
{
    string name = GlobalConst.DIR_XML_ACTIVITY;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public static List<ActivityData> activityConfig = new List<ActivityData>();
   
    //构造函数初始化
    public ActivityConfig()
    {
        Initialize();
    }
    //初始化函数
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ACTIVITY, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name);
		ReadActivityData();
		Logger.ConfigEnd(name);
    }

    public void ReadActivityData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        activityConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ACTIVITY, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            ActivityData config = new ActivityData();
            foreach (XmlElement xel in xe)
            {
                uint value = 0;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "activity")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.activity = value;
                }
                else if (xel.Name == "level_range")
                {
                    string[] temp = xel.InnerText.Split('&');
                    for (int i = 0; i < temp.Length; ++i)
                    {
                        config.level.Add(uint.Parse(temp[i]));
                    }
                }
                else if (xel.Name == "awards")
                {
                    string[] temp = xel.InnerText.Split('&');
                    for (int i = 0; i < temp.Length; ++i)
                    {
                        config.awards.Add(uint.Parse(temp[i]));
                    }
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;

                }
            }
            activityConfig.Add(config);
        }
    }

    public ActivityData GetActivityData(uint id)
    {
        return activityConfig.Find(x => id == x.id);
    }
   
}
