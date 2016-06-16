using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BodyInfo
{
	public uint		body_id;
	public string	type_id;
	public string	head_id;
	public uint		complexion;
}

public class BodyInfoListConfig
{
    string name = GlobalConst.DIR_XML_BODY_INFO_LIST;
    bool isLoadFinish = false;
    private object LockObject = new object();
	public Dictionary<uint, BodyInfo> configs = new Dictionary<uint, BodyInfo>();

	public BodyInfoListConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public BodyInfo GetConfig(uint body_id)
    {
		BodyInfo data = null;
		configs.TryGetValue(body_id, out data);
        return data;
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

        //读取以及处理XML文本的类
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);

        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
			if( node_line.SelectSingleNode("switch").InnerText == "#" )
				continue;

			BodyInfo data = new BodyInfo();
			data.body_id = uint.Parse(node_line.SelectSingleNode("body_id").InnerText);
			data.type_id = node_line.SelectSingleNode("type_id").InnerText;
			data.head_id = node_line.SelectSingleNode("head_id").InnerText;
			data.complexion = uint.Parse(node_line.SelectSingleNode("complexion").InnerText);
            
			configs.Add(data.body_id, data);
		}
		Logger.ConfigEnd(name);
    }
}
