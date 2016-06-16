using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.config;
using System.Xml;
using fogs.proto.msg;
public class BadgeSlotBaseConfig
{
   public uint id;
   public string name;
   public BadgeCG category;
   public uint requireLevel;
   public float layoutPosx;
   public float layoutPosy;
   public uint unlockCostGoodsId;
   public uint unlockCostGoodsNum;
}

//徽章槽位配置
public class BadgeSlotConfig{
    string name = GlobalConst.DIR_XML_BADGESLOT;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<uint, BadgeSlotBaseConfig> configs = new Dictionary<uint, BadgeSlotBaseConfig>();
    public BadgeSlotConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    //通过槽位ID获取本地配置的相关信息
    public BadgeSlotBaseConfig GetConfig(uint slotId)
    {
        BadgeSlotBaseConfig data = null;
        configs.TryGetValue(slotId, out data);
        return data;
    }
    //读取解析配置
    public void ReadConfig()
    {
        if (isLoadFinish == false) return;
        isLoadFinish = false;
		lock (LockObject) { GameSystem.Instance.readConfigCnt += 1;}
		Logger.ConfigBegin(name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }

        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);
        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            BadgeSlotBaseConfig data = new BadgeSlotBaseConfig();
            data.id = uint.Parse(node_line.SelectSingleNode("id").InnerText);
            data.name = node_line.SelectSingleNode("name").InnerText;
            data.category =(BadgeCG)uint.Parse(node_line.SelectSingleNode("category").InnerText);
            data.requireLevel = uint.Parse(node_line.SelectSingleNode("requireLevel").InnerText);
            data.layoutPosx = float.Parse(node_line.SelectSingleNode("layoutPosX").InnerText);
            data.layoutPosy = float.Parse(node_line.SelectSingleNode("layoutPosY").InnerText);
            data.unlockCostGoodsId = uint.Parse(node_line.SelectSingleNode("unlockCostGoodsID").InnerText);
            data.unlockCostGoodsNum = uint.Parse(node_line.SelectSingleNode("unlockCostGoodsNum").InnerText);
            configs.Add(data.id, data);
		}
		Logger.ConfigEnd(name);
    }
    
    public Dictionary<uint,BadgeSlotBaseConfig> GetAllConfigs()
    {
          return configs;
    }
    
}
