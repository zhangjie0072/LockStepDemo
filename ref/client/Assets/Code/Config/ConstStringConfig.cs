using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;

public class ConstStringConfig
{
    string name = GlobalConst.DIR_XML_CONSTSTRING;
    bool isLoadFinish = false;
    private object LockObject = new object();

    Dictionary<string, string> constStringConfig = new Dictionary<string, string>();

    public ConstStringConfig()
    {
        Initialize();
    }

    void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
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

		UnityEngine.Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            UnityEngine.Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        constStringConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name, text);
        //解析XML的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            StringConfig data = new StringConfig();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "info")
                {
                    data.info = xel.InnerText;
                }
            }
            if (!constStringConfig.ContainsKey(data.name))
            {
                constStringConfig.Add(data.name, data.info);
            }
		}
		
    }

    //根据文字名字返回内容
    public string GetInfoByName(string name)
    {
        if (constStringConfig.ContainsKey(name))
        {
            return constStringConfig[name];
        }
        return name;
    }
}
