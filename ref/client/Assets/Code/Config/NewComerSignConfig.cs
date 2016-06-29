using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NewComerSignData
{
	public uint awardType;
	public uint awardNum;
	public uint consumeType;
	public uint consumeNum;
	public string descript;
}

public class NewComerTotalData
{
	public List<string> totalAwards = new List<string>();
	public string descript;
}

public class NewComerSignConfig
{
    string name = GlobalConst.DIR_XML_NEWCOMERSIGN;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<uint, NewComerSignData> configData = new Dictionary<uint, NewComerSignData>();
    public Dictionary<uint, NewComerTotalData> totalConfigData = new Dictionary<uint, NewComerTotalData>();


    public NewComerSignConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_NEWCOMERSIGN, LoadFinish);
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

		Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }

        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_NEWCOMERSIGN, text);
        XmlNode root = doc.SelectSingleNode("Data");
        foreach (XmlNode line in root.SelectNodes("Line"))
        {
            if (CommonFunction.IsCommented(line))
                continue;
            NewComerSignData signData = new NewComerSignData();
            NewComerTotalData totalData = new NewComerTotalData();
            XmlElement signDay = line.SelectSingleNode("day") as XmlElement;
            XmlElement signType = line.SelectSingleNode("type") as XmlElement;
            XmlElement signAward = line.SelectSingleNode("awards") as XmlElement;
            XmlElement appendConsume = line.SelectSingleNode("append_consume") as XmlElement;
            XmlElement desc = line.SelectSingleNode("desc") as XmlElement;
            int type = int.Parse(signType.InnerText);
            if (type == 1)
            {
                string[] sign = signAward.InnerText.Split(':');
                signData.awardType = uint.Parse(sign[0]);
                signData.awardNum = uint.Parse(sign[1]);
                string[] consume = appendConsume.InnerText.Split(':');
                signData.consumeType = uint.Parse(consume[0]);
                signData.consumeNum = uint.Parse(consume[1]);
                signData.descript = desc.InnerText;
                configData.Add(uint.Parse(signDay.InnerText), signData);
            }
            else if (type == 2)
            {
                string[] totalAwards = signAward.InnerText.Split('&');
                foreach (string award in totalAwards)
                {
                    totalData.totalAwards.Add(award);
                }
                totalData.descript = desc.InnerText;
                totalConfigData.Add(uint.Parse(signDay.InnerText), totalData);
            }
        }
		
    }

    //通过Day获得每日的奖励类型
    public uint GetDayAwardType(uint day)
    {
        NewComerSignData data;
        configData.TryGetValue(day, out data);
        return data.awardType;
    }

    //通过Day获得每日的奖励数量
    public uint GetDayAwardNum(uint day)
    {
        NewComerSignData data;
        configData.TryGetValue(day, out data);
        return data.awardNum;
    }

    //通过Day获得补签消耗类型
    public uint GetConsumeType(uint day)
    {
        NewComerSignData data;
        configData.TryGetValue(day, out data);
        return data.consumeType;
    }

    //通过Day获得补签消耗数量
    public uint GetConsumeNum(uint day)
    {
        NewComerSignData data;
        configData.TryGetValue(day, out data);
        return data.consumeNum;
    }

    //通过Day获得累计奖励
    public List<string> GetTotalAward(uint day)
    {
        NewComerTotalData totalData;
        totalConfigData.TryGetValue(day, out totalData);
        if (totalData != null)
            return totalData.totalAwards;
        return null;
    }

    //通过type和day获得描述
    public string GetDayDesc(uint type, uint day)
    {
        if (type == 1)
        {
            NewComerSignData data;
            configData.TryGetValue(day, out data);
            return data.descript;
        }
        else if (type == 2)
        {
            NewComerTotalData totalData;
            totalConfigData.TryGetValue(day, out totalData);
            return totalData.descript;
        }
        return null;
    }
}
