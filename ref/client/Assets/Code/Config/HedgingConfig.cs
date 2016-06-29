
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class HedgingConfig
{
    string name = GlobalConst.DIR_XML_HEDGING;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public IM.Number factorMin { get; private set; }
    public IM.Number factorMax { get; private set; }
    public class hedgeLevelData
    {
        public IM.PrecNumber factor;
        public uint oppositeID;
    }
    public Dictionary<uint, hedgeLevelData> hedgeLevelFactor;

	XmlDocument doc;
	XmlNode root;

	public HedgingConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public IM.Number GetAttribute(string attr, string func)
	{
		XmlNode node = root.SelectSingleNode(func);
		if (node != null)
		{
			XmlAttribute attribute = node.Attributes[attr];
			if (attribute != null)
			{
                IM.Number resultMin = IM.Number.Parse(attribute.InnerText);
				return resultMin;
			}
		}
		Debug.LogError("HedgingConfig: no " + attr + " in " + func);
		return IM.Number.zero;
	}
    public IM.PrecNumber GetRatio(string attr, string func)
    {
        XmlNode ratio = root.SelectSingleNode("ratio");
        XmlNode node = ratio.SelectSingleNode(func);
        if (node != null)
        {
            XmlAttribute attribute = node.Attributes[attr];
            return IM.PrecNumber.Parse(attribute.InnerText);
        }
        Debug.LogError("hedging ratio: no " + attr + " in " + func);
        return IM.PrecNumber.zero;
    }

    public hedgeLevelData GetHedgeLevelFactor(uint hedgeLevelID)
    {
        hedgeLevelData data;
        hedgeLevelFactor.TryGetValue(hedgeLevelID, out data);
        return data;
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        
		doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_HEDGING, text);
		root = doc.SelectSingleNode("Hedging");
		factorMin = IM.Number.Parse(root.Attributes["factorMin"].InnerText);
		factorMax = IM.Number.Parse(root.Attributes["factorMax"].InnerText);

        hedgeLevelFactor = new Dictionary<uint, hedgeLevelData>();
        XmlNode xmlNode = root.SelectSingleNode("HedgeLevelFactor");
        foreach (XmlNode node in xmlNode.ChildNodes)
        {
            if (node != null)
            {
                hedgeLevelData data = new hedgeLevelData();
                uint id = uint.Parse(node.Attributes["id"].InnerText);
                data.factor = IM.PrecNumber.Parse(node.Attributes["factor"].InnerText);
                data.oppositeID = uint.Parse(node.Attributes["oppositeID"].InnerText);
                if (!hedgeLevelFactor.ContainsKey(id))
                    hedgeLevelFactor.Add(id, data);
            }
        }
		
	}
}
