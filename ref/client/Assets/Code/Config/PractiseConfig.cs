using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PractiseData
{
	public enum Type
	{
		Normal,
		Guide,
	}
	public uint ID;
	public Type type;
	public uint diffcult;
	public string title;
	public string intro;
	public string[] tips;
	public uint num_total;
	public uint num_complete;
	public uint self_id;
	public uint npc_id;
	public string scene;
	public string complete_sound;
	public string failed_sound;
	public Dictionary<uint, uint> awards = new Dictionary<uint, uint>();
	public uint is_activity;
}

public class PractiseConfig
{
    string name = GlobalConst.DIR_XML_PRACTISE;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public Dictionary<uint, PractiseData> configs = new Dictionary<uint, PractiseData>();

    public PractiseConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadData();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

    public Dictionary<uint, PractiseData>.ValueCollection.Enumerator GetEnumerator()
    {
        return configs.Values.GetEnumerator();
    }

	public int Count
	{
		get {
		return configs.Count;
		}
	}

    public PractiseData GetConfig(uint ID)
    {
        PractiseData practise = null;
        configs.TryGetValue(ID, out practise);
        return practise;
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
        configs.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PRACTISE, text);
        //解析xml的过程
        foreach (XmlNode xe in xmlDoc.SelectSingleNode("Data").SelectNodes("Line"))
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            PractiseData data = new PractiseData();
            data.ID = uint.Parse(xe.SelectSingleNode("id").InnerText);
			XmlNode node = xe.SelectSingleNode("type");
			if (node != null)
				data.type = (PractiseData.Type)(int.Parse(node.InnerText));
            data.title = xe.SelectSingleNode("title").InnerText;
            data.intro = xe.SelectSingleNode("intro").InnerText;
            data.tips = xe.SelectSingleNode("tips").InnerText.Split('&');
			for (int i = 0; i < data.tips.Length; ++i)
				data.tips[i] = data.tips[i].Trim(new char[] { '\r', '\n' });
            uint.TryParse(xe.SelectSingleNode("diffcult").InnerText, out data.diffcult);
            uint.TryParse(xe.SelectSingleNode("num_practice").InnerText, out data.num_total);
            uint.TryParse(xe.SelectSingleNode("num_award").InnerText, out data.num_complete);
            uint.TryParse(xe.SelectSingleNode("self").InnerText, out data.self_id);
            uint.TryParse(xe.SelectSingleNode("npc").InnerText, out data.npc_id);
            data.scene = xe.SelectSingleNode("scene").InnerText;
            data.complete_sound = xe.SelectSingleNode("achieved_mic").InnerText;
            data.failed_sound = xe.SelectSingleNode("fail_mic").InnerText;
            for (uint i = 1; i <= 3; ++i)
            {
                string award_id = xe.SelectSingleNode("award_id" + i).InnerText;
                string award_value = xe.SelectSingleNode("award_value" + i).InnerText;
                if (award_id != string.Empty && award_value != string.Empty)
                {
                    data.awards.Add(uint.Parse(award_id), uint.Parse(award_value));
                }
            }
            configs.Add(data.ID, data);
            uint.TryParse(xe.SelectSingleNode("is_activity").InnerText, out data.is_activity);
		}
		Logger.ConfigEnd(name);
    }
}
