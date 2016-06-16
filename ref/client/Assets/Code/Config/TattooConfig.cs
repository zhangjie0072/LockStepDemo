using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.msg;
using fogs.proto.config;

public class TattooLvConfigData
{
    public string name; //纹身名
    public string icon; //纹身图标
    public Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>(); //提升属性值
	public List<PositionType> positions = new List<PositionType>();		//适用职业
	public uint require_exp;	//进阶需求经验
	public uint require_level;	//进阶需求等级
	public uint sacrifice_consume;	//作为进阶品消耗金币
	public uint sacrifice_exp;		//作为进阶品提供经验
}

public class TattooConfigData
{
    public uint id; //纹身ID
    public Dictionary<uint, TattooLvConfigData> lvData = new Dictionary<uint, TattooLvConfigData>(); //纹身等级对应的属性值
}

public class TattooConfig
{
    string name = GlobalConst.DIR_XML_TATTOO;
    bool isLoadFinish = false;
    private object LockObject = new object();

    Dictionary<uint, TattooConfigData> Data = new Dictionary<uint, TattooConfigData>();

    public TattooConfig()
    {
        Initialize();
    }
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TATTOO, LoadFinish);
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
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TATTOO, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            TattooConfigData data = new TattooConfigData();
            TattooLvConfigData lvData = new TattooLvConfigData();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    data.id = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "level")
                {
                    level = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "name")
                {
                    lvData.name = xel.InnerText;
                }
                else if (xel.Name == "icon")
                {
                    lvData.icon = xel.InnerText;
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
                    lvData.addn_attr = addn_attr;
                }
				else if (xel.Name == "position")
				{
					if (!string.IsNullOrEmpty(xel.InnerText))
					{
						string[] tokens = xel.InnerText.Split('&');
						foreach (string token in tokens)
						{
							lvData.positions.Add((PositionType)int.Parse(token));
						}
					}
				}
				else if (xel.Name == "require_exp")
				{
					uint.TryParse(xel.InnerText, out lvData.require_exp);
				}
				else if (xel.Name == "require_level")
				{
					uint.TryParse(xel.InnerText, out lvData.require_level);
				}
                else if (xel.Name == "sacrifice_consume")
                {
                    lvData.sacrifice_consume = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "sacrifice_exp")
                {
                    lvData.sacrifice_exp = uint.Parse(xel.InnerText);
                }
            }
            data.lvData[level] = lvData;
            if (Data.ContainsKey(data.id))
            {
               if ( !Data[data.id].lvData.ContainsKey(level))
               {
                   Data[data.id].lvData[level] = lvData;
               }
            }
            else
            {
                Data[data.id] = data;
            }
        }
		Logger.ConfigEnd(name);
    }

    //根据训练ID获取基本信息
    public TattooLvConfigData GetTattooConfig(uint id, uint level)
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

    //
    public bool HasTattooGoods(uint id)
    {
        return Data.ContainsKey(id);
    }
}
