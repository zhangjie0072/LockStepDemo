using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Collections;

using fogs.proto.msg;

public class PotientialEffect
{
	public uint		level;
	public string	resource;
	public uint		idx;			
}

public class PotientialEffectConfig
{
    string name = GlobalConst.DIR_XML_POTENTIAL_EFFECT;
    bool isLoadFinish = false;
    private object LockObject = new object();

	private Dictionary<uint, PotientialEffect> configs = new Dictionary<uint, PotientialEffect>();
	
	public PotientialEffectConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_POTENTIAL_EFFECT, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public PotientialEffect GetConfig(uint lv)
	{
		PotientialEffect data = null;
		configs.TryGetValue(lv, out data);
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
		configs.Clear();

		//读取以及处理XML文本的类
		XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_POTENTIAL_EFFECT, text);
		//解析xml的过程
		XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
		
		foreach (XmlElement land in nodeList)
		{
			XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
			if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
				continue;

			PotientialEffect config = new PotientialEffect();
			foreach (XmlElement xel in land)
			{
				uint value;
				if (xel.Name == "lv")
				{
					uint.TryParse(xel.InnerText, out value);
					config.level = value;
				}
				else if (xel.Name == "resource")
				{
					config.resource = xel.InnerText;
				}
				else if (xel.Name == "id")
				{
					uint.TryParse(xel.InnerText, out value);
					config.idx = value;
				}
			}
			configs.Add(config.level , config);
		}
		
	}
}
