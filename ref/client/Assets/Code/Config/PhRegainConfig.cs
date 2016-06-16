using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class PhRegainConfig
{
    string name = GlobalConst.DIR_XML_PH_REGAIN;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public struct PhStage
    {
        public IM.Number stage;
        public IM.Number value1;
        public IM.Number value2;
        public IM.Number regain;
    }

    public Dictionary<IM.Number, PhStage> stages = new Dictionary<IM.Number, PhStage>();

    public PhRegainConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadConfig();
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
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        stages.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PH_REGAIN, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
        foreach (XmlNode line in root.SelectNodes("Line"))
        {
			if (CommonFunction.IsCommented(line))
				continue;
            PhStage stage;
            stage.stage = IM.Number.Parse(line.SelectSingleNode("stage").InnerText);
            stage.value1 = IM.Number.Parse(line.SelectSingleNode("value_1").InnerText);
            stage.value2 = IM.Number.Parse(line.SelectSingleNode("value_2").InnerText);
            string regain = line.SelectSingleNode("Regain").InnerText;
            stage.regain = string.IsNullOrEmpty(regain) ? IM.Number.zero : IM.Number.Parse(regain);
            stages.Add(stage.stage, stage);
        }
		Logger.ConfigEnd(name);
    }

    public PhStage GetStage(IM.Number stage)
    {
        PhStage config;
        stages.TryGetValue(stage, out config);
        return config;
    }

    public Dictionary<IM.Number, PhStage>.Enumerator GetEnumerator()
    {
        return stages.GetEnumerator();
    }
}
