using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using fogs.proto.config;

public class PVPPointConfig
{
    string name1 = GlobalConst.DIR_XML_PVP_POINT;
    string name2 = GlobalConst.DIR_XML_PVP_POINT_CHARGE_COST;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;
    public class LevelInfo
    {
        public uint vip_level;
        public uint point;
        public uint max_charge_time;
    }

    private Dictionary<uint, LevelInfo> _level_info = new Dictionary<uint, LevelInfo>();
    private Dictionary<uint, BuyGameTimesConfig> _charge_cost = new Dictionary<uint, BuyGameTimesConfig>();

    public PVPPointConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_PVP_POINT, LoadFinish);
        //ReadLevelInfo();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_PVP_POINT_CHARGE_COST, LoadFinish);
        //ReadChargeCost();
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
		ReadLevelInfo();
		Debug.Log("Config reading " + name2);
		
		ReadChargeCost();
		
    }

    public LevelInfo GetLevelInfo(uint vip_level)
    {
        LevelInfo info = null;
        if (!_level_info.TryGetValue(vip_level, out info))
        {
            Debug.LogError("PVP point config for VIP level " + vip_level + "not found.");
        }
        return info;
    }

    public BuyGameTimesConfig GetChargeCost(uint times)
    {
        BuyGameTimesConfig cost = new BuyGameTimesConfig();
        if (!_charge_cost.TryGetValue(times, out cost))
        {
            Debug.Log("PVP charge cost config for times " + times + "not found");
        }
        return cost;
    }

    private void ReadLevelInfo()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        _level_info.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PVP_POINT, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            LevelInfo info = new LevelInfo();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "VIP_level")
                {
                    uint.TryParse(xel.InnerText, out info.vip_level);
                }
                else if (xel.Name == "point")
                {
                    uint.TryParse(xel.InnerText, out info.point);
                }
                else if (xel.Name == "max_charge_time")
                {
                    uint.TryParse(xel.InnerText, out info.max_charge_time);
                }
            }
            if (!_level_info.ContainsKey(info.vip_level))
            {
                _level_info.Add(info.vip_level, info);
            }
            else
            {
                Debug.LogWarning("VIP level " + info.vip_level + "already existed.");
            }
        }
    }

    private void ReadChargeCost()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        _charge_cost.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PVP_POINT_CHARGE_COST, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            uint times = 0;
            uint value = 0;
            BuyGameTimesConfig cost = new BuyGameTimesConfig();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out times);
                }
                else if (xel.Name == "consume_type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    cost.consume_type = value;
                }
                else if (xel.Name == "consume")
                {
                    uint.TryParse(xel.InnerText, out value);
                    cost.consume = value;
                }
            }
            if (!_charge_cost.ContainsKey(times))
            {
                _charge_cost.Add(times, cost);
            }
            else
            {
                Debug.LogWarning("PVP point charge cost config for times " + times + "already existed.");
            }
        }
    }
}
