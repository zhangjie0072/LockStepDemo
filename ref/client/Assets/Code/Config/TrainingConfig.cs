using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.msg;
using fogs.proto.config;

public class TrainingLevelConfigData
{
    public uint cd; //训练持续时间（单位：秒）
    public uint level_limit; //队长等级对训练的限制
    public Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>(); //提升属性值
    public Dictionary<uint, uint> normal_consume = new Dictionary<uint, uint>(); //普通训练消耗
}

public class TrainingConfigData
{
    public uint id; //训练项目ID
    public string name; //名称
    public string icon; //图标
    public string intro; //介绍
    public Dictionary<uint, uint> lv_limit = new Dictionary<uint, uint>(); //职业对训练等级的限制
    public Dictionary<uint, TrainingLevelConfigData> lv_data = new Dictionary<uint, TrainingLevelConfigData>(); //训练等级对应的配置信息
}

public class TrainingConfig
{
    string name1 = GlobalConst.DIR_XML_CAPTAIN_TRAINING;
    string name2 = GlobalConst.DIR_XML_CAPTAIN_TRAINING_LEVEL;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    Dictionary<uint, TrainingConfigData> Data = new Dictionary<uint, TrainingConfigData>();

    public TrainingConfig()
    {
        Initialize();
    }
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //ReadTrainingData();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        //ReadTrainingLevelData();
    }
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }
		Debug.Log("Config reading " + name1);
        ReadTrainingData();
		
		Debug.Log("Config reading " + name2);
		ReadTrainingLevelData();
		
    }

    public void ReadTrainingData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        Data.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CAPTAIN_TRAINING, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            TrainingConfigData data = new TrainingConfigData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    data.id = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "icon")
                {
                    data.icon = xel.InnerText;
                }
                else if (xel.Name == "intro")
                {
                    data.intro = xel.InnerText;
                }
                else if (xel.Name == "lv_limit")
                {
                    string[] entirety = xel.InnerText.Split('&');
                    for (int i = 0; i < entirety.Length; ++i)
                    {
                        string[] detail = entirety[i].Split(':');
                        if (detail.Length < 2)
                            continue;
                        data.lv_limit[uint.Parse(detail[0])] = uint.Parse(detail[1]);
                    }
                }
            }
            if (!Data.ContainsKey(data.id))
            {
                Data[data.id] = data;
            }
        }
    }
    public void ReadTrainingLevelData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CAPTAIN_TRAINING_LEVEL, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        uint value;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            uint id = 0;
            uint level = 0;
            TrainingLevelConfigData levelData = new TrainingLevelConfigData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out id);
                    if (!Data.ContainsKey(id))
                        break;
                }
                else if (xel.Name == "level")
                {
                    uint.TryParse(xel.InnerText, out level);
                }
                else if (xel.Name == "duration")
                {
                    uint.TryParse(xel.InnerText, out value);
                    levelData.cd = value;
                }
                else if (xel.Name == "level_limit")
                {
                    uint.TryParse(xel.InnerText, out value);
                    levelData.level_limit = value;
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
                    levelData.addn_attr = addn_attr;
                }
                else if (xel.Name == "normal_consume")
                {
                    Dictionary<uint, uint> normal_consume = new Dictionary<uint, uint>();
                    string[] entirety = xel.InnerText.Split('&');
                    for (int i = 0; i < entirety.Length; ++i)
                    {
                        string[] detail = entirety[i].Split(':');
                        if (detail.Length < 2)
                            continue;
                        normal_consume[uint.Parse(detail[0])] = uint.Parse(detail[1]);
                    }
                    levelData.normal_consume = normal_consume;
                }
            }
            if (!Data[id].lv_data.ContainsKey(level))
            {
                Data[id].lv_data.Add(level, levelData);
            }
        }
    }

    //根据训练ID获取基本信息
    public TrainingConfigData GetTrainingConfig(uint trainingID)
    {
        if (Data.ContainsKey(trainingID))
        {
            return Data[trainingID];
        }
        return null;
    }

    //根据训练ID和等级获取相应的消耗，属性增加，时间
    public TrainingLevelConfigData GetTrainingLevelConfig(uint trainingID, uint level)
    {
        if (Data.ContainsKey(trainingID) && Data[trainingID].lv_data.ContainsKey(level))
        {
            return Data[trainingID].lv_data[level];
        }
        return null;
    }
}
