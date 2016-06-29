using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public class PracticePveData
{
    public uint ID;
    public uint gamemode;
    public uint awardpack;

}

public class PracticePveConfig
{
    string name = GlobalConst.DIR_XML_PRACTISEPVE;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public Dictionary<uint, PracticePveData> configs = new Dictionary<uint, PracticePveData>();

    public PracticePveConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadData();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

    public Dictionary<uint, PracticePveData>.ValueCollection.Enumerator GetEnumerator()
    {
        return configs.Values.GetEnumerator();
    }

    public int Count
    {
        get
        {
            return configs.Count;
        }
    }

    public PracticePveData GetConfig(uint ID)
    {
        PracticePveData practise = null;
        configs.TryGetValue(ID, out practise);
        return practise;
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
        configs.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PRACTISEPVE, text);
        //解析xml的过程
        foreach (XmlNode xe in xmlDoc.SelectSingleNode("Data").SelectNodes("Line"))
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            PracticePveData data = new PracticePveData();
            //data.ID = uint.Parse(xe.SelectSingleNode("id").InnerText);
            uint.TryParse(xe.SelectSingleNode("id").InnerText, out data.ID);
            uint.TryParse(xe.SelectSingleNode("gamemode").InnerText, out data.gamemode);
            uint.TryParse(xe.SelectSingleNode("awardpack").InnerText, out data.awardpack);
            configs.Add(data.ID, data);
        }
		
    }
}

