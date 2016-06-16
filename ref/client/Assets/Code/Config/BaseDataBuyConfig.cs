using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class BuyData
{
    public uint times;//购买次数
    public uint diamond_need;//所需钻石
    public uint value;//获得数量
    public uint level_min;
    public uint level_max;
}

public class BaseDataBuyConfig
{
    string name1 = GlobalConst.DIR_XML_BUYGOLD;
    string name2 = GlobalConst.DIR_XML_BUYHP;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public List<BuyData> BuyGoldDatas = new List<BuyData>();
    public List<BuyData> BuyHpDatas = new List<BuyData>();

    public BaseDataBuyConfig()
    {
        Initialize();
    }
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_BUYGOLD, LoadFinish);
        //ReadBuyGoldData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_BUYHP, LoadFinish);
        //ReadBuyHPData();
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

		Logger.ConfigBegin(name1);
		ReadBuyGoldData();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadBuyHPData();
		Logger.ConfigEnd(name2);
    }

    public void ReadBuyGoldData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        BuyGoldDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_BUYGOLD, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            BuyData buyGoldData = new BuyData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out buyGoldData.times);
                }
                else if (xel.Name == "diamond_need")
                {
                    uint.TryParse(xel.InnerText, out buyGoldData.diamond_need);
                }
                else if (xel.Name == "value_get")
                {
                    uint.TryParse(xel.InnerText, out buyGoldData.value);
                }
                else if (xel.Name == "level_min")
                {
                    uint.TryParse(xel.InnerText, out buyGoldData.level_min);
                }
                else if (xel.Name == "level_max")
                {
                    uint.TryParse(xel.InnerText, out buyGoldData.level_max);
                }
            }
            BuyGoldDatas.Add(buyGoldData);
        }
    }
    public void ReadBuyHPData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        BuyHpDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_BUYHP, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            BuyData buyHpData = new BuyData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out buyHpData.times);
                }
                else if (xel.Name == "diamond_need")
                {
                    uint.TryParse(xel.InnerText, out buyHpData.diamond_need);
                }
                else if (xel.Name == "value_get")
                {
                    uint.TryParse(xel.InnerText, out buyHpData.value);
                }
            }
            BuyHpDatas.Add(buyHpData);
        }
    }
    /// <summary>
    /// 根据购买次数返回购买金币信息
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public BuyData GetBuyGoldDataByTimes(uint times)
    {
        uint level = MainPlayer.Instance.Level;
        for (int i = 0; i < BuyGoldDatas.Count; i++)
        {
            if (BuyGoldDatas[i].times == times && BuyGoldDatas[i].level_min <= level && level <= BuyGoldDatas[i].level_max)
            {
                return BuyGoldDatas[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 根据购买次数返回购买体力消息
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public BuyData GetBuyHpDataByTimes(uint times)
    {
        for (int i = 0; i < BuyHpDatas.Count; i++)
        {
            if (BuyHpDatas[i].times == times  )
            {
                return BuyHpDatas[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 返回购买金币最大次数
    /// </summary>
    /// <returns></returns>
    public uint GetBuyGoldMaxTimes()
    {
        return BuyGoldDatas[BuyGoldDatas.Count-1].times;
    }
    /// <summary>
    /// 返回购买体力最大次数
    /// </summary>
    /// <returns></returns>
    public uint GetBuyHpMaxTimes()
    {
        return BuyHpDatas[BuyHpDatas.Count-1].times;
    }
}
