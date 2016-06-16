using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.msg;
using fogs.proto.config;

public class EquipmentBaseDataConfig
{
    public string name; //名称
    public string icon; //图标
    public Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>(); //提升属性值
    public uint require_level;	//强化等级限制
    public uint sacrifice_consume;	//强化消耗金币数量
    public uint sell_price;	//出售价格

	public uint GetAddnAttr(uint id)
	{
		if (addn_attr.ContainsKey(id))
			return addn_attr[id];
		return 0;
	}
}

public class EquipmentLvDataConfig
{
    public uint id; //D
    public Dictionary<uint, EquipmentBaseDataConfig> lvData = new Dictionary<uint, EquipmentBaseDataConfig>(); //等级对应的属性值
}

public class EquipmentConfig
{
    string name = GlobalConst.DIR_XML_EQUIPMENT;
    bool isLoadFinish = false;
    private object LockObject = new object();

    Dictionary<uint, EquipmentLvDataConfig> Data = new Dictionary<uint, EquipmentLvDataConfig>();

    public EquipmentConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_EQUIPMENT, LoadFinish);
        //ReadData();
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
        Data.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_EQUIPMENT, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            EquipmentLvDataConfig lvData = new EquipmentLvDataConfig();
            EquipmentBaseDataConfig data = new EquipmentBaseDataConfig();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    lvData.id = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "level")
                {
                    level = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "icon")
                {
                    data.icon = xel.InnerText;
                }
                else if (xel.Name == "addn_attr")
                {
                    Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
                    string[] entirety = xel.InnerText.Split('&');
                    for (int i = 0; i < entirety.Length; ++i)
                    {
                        string[] detail = entirety[i].Split(':');
                        if (detail.Length < 2)
                            continue;
                        addn_attr[uint.Parse(detail[0])] = uint.Parse(detail[1]);
                    }
                    data.addn_attr = addn_attr;
                }
                else if (xel.Name == "require_level")
                {
                    data.require_level = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "sacrifice_consume")
                {
                    data.sacrifice_consume = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "sell_price")
                {
                    data.sell_price = uint.Parse(xel.InnerText);
                }
            }
            lvData.lvData[level] = data;
            if (Data.ContainsKey(lvData.id))
            {
                if (!Data[lvData.id].lvData.ContainsKey(level))
                {
                    Data[lvData.id].lvData[level] = data;
                }
            }
            else
            {
                Data[lvData.id] = lvData;
            }
        }
		Logger.ConfigEnd(name);
    }

    //根据装备ID和等级获取基础配置
    public EquipmentBaseDataConfig GetBaseConfig(uint id, uint level)
    {
        if (Data.ContainsKey(id))
        {
            if (Data[id].lvData.ContainsKey(level))
            {
                return Data[id].lvData[level];
            }
        }
        return null;
    }
}
