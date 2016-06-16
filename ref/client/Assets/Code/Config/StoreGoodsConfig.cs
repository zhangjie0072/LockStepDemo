using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;

public class StoreGoodsData
{
    public int store_good_grid;//商品格子
    public int store_good_id;//商品id
    public int store_good_num;//商品数量
    public int store_good_consume_type;//商品花费类型（1-金币，2-钻石）
    public int store_good_price;//商品价格
    public int store_goods_weight; // the weigh of goods which display in store.
    public int apply_min_level;
    public int apply_max_level;
    public uint is_sell;
}
public class StoreGoodsConfig
{
    string name1 = GlobalConst.DIR_XML_STOREREFRESHCONSUME;
    string name2 = GlobalConst.DIR_XML_STOREGOODS;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    Dictionary<uint, List<StoreRefreshConsumeConfig>> StoreRefreshConsumeDatas = new Dictionary<uint, List<StoreRefreshConsumeConfig>>();
    Dictionary<uint, List<StoreGoodsData>> StoreDatas = new Dictionary<uint, List<StoreGoodsData>>();

    public StoreGoodsConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_STOREREFRESHCONSUME, LoadFinish);
        //ReadStoreRefreshConsumeData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_STOREGOODS, LoadFinish);
        //ReadStoreData();
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
		ReadStoreRefreshConsumeData();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
        ReadStoreData();
		Logger.ConfigEnd(name2);
    }

    public void ReadStoreRefreshConsumeData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        StoreRefreshConsumeDatas.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_STOREREFRESHCONSUME, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            StoreRefreshConsumeConfig storeRefreshConsumeData = new StoreRefreshConsumeConfig();
            uint storeID = 0;
            foreach (XmlElement xel in xe)
            {
                uint value = 0;
                if (xel.Name == "store_id")
                {
                    uint.TryParse(xel.InnerText, out storeID);
                }
                else if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out value);
                    storeRefreshConsumeData.times = value;
                }
                else if (xel.Name == "consume_type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    storeRefreshConsumeData.consume_type = value;
                }
                else if (xel.Name == "consume")
                {
                    uint.TryParse(xel.InnerText, out value);
                    storeRefreshConsumeData.consume = value;
                }
                if (StoreRefreshConsumeDatas.ContainsKey(storeID) == false)
                {
                    StoreRefreshConsumeDatas[storeID] = new List<StoreRefreshConsumeConfig>();
                }
                StoreRefreshConsumeDatas[storeID].Add(storeRefreshConsumeData);
            }
        }
    }

    public void ReadStoreData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        StoreDatas.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_STOREGOODS, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            StoreGoodsData storeGoodsData = new StoreGoodsData();
            uint storeID = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "store_id")
                {
                    uint.TryParse(xel.InnerText, out storeID);
                }
                else if (xel.Name == "goods_id")
                {
                    int.TryParse(xel.InnerText, out storeGoodsData.store_good_id);
                }
                else if (xel.Name == "consume_type")
                {
                    int.TryParse(xel.InnerText, out storeGoodsData.store_good_consume_type);
                }
                else if (xel.Name == "price")
                {
                    int.TryParse(xel.InnerText, out storeGoodsData.store_good_price);
		        }
		        else if (xel.Name == "weight")
		        {
			        int.TryParse(xel.InnerText, out storeGoodsData.store_goods_weight);
		        }
		        else if (xel.Name == "apply_min_level")
		        {
			        int.TryParse(xel.InnerText, out storeGoodsData.apply_min_level);
		        }
		        else if (xel.Name == "apply_max_level")
		        {
			        int.TryParse(xel.InnerText, out storeGoodsData.apply_max_level);
                }
                else if (xel.Name == "is_sell" && !string.IsNullOrEmpty(xel.InnerText))
                {
                    uint.TryParse(xel.InnerText, out storeGoodsData.is_sell);
                }
            }
            if (StoreDatas.ContainsKey(storeID) == false)
            {
                StoreDatas[storeID] = new List<StoreGoodsData>();
            }
            StoreDatas[storeID].Add(storeGoodsData);
        }
    }
    /// <summary>
    /// 根据刷新次数获取消耗钻石数量
    /// </summary>
    /// <param name="teamLevel"></param>
    /// <returns></returns>
    public StoreRefreshConsumeConfig GetConsume(uint storeID, uint times)
    {
        if (StoreRefreshConsumeDatas.ContainsKey(storeID))
        {
            int count = StoreRefreshConsumeDatas[storeID].Count;
            for (int i = 0; i < count; ++i)
            {
                if (StoreRefreshConsumeDatas[storeID][i].times == times)
                {
                    return StoreRefreshConsumeDatas[storeID][i];
                }
            }
            return StoreRefreshConsumeDatas[storeID][count-1];
        }
        return null;
    }
    /// <summary>
    /// 根据商品库ID获取
    /// </summary>
    /// <param name="storeID"></param>
    /// <returns></returns>
    public StoreGoodsData GetStoreGoodsData(uint storeID, uint storeGoodID)
    {
        if (StoreDatas.ContainsKey(storeID))
        {
            for (int i = 0; i < StoreDatas[storeID].Count; i++)
            {
                if (StoreDatas[storeID][i].store_good_id == storeGoodID)
                {
                    return StoreDatas[storeID][i];
                }
            }
        }
        return null;
    }

	public List<StoreGoodsData> GetStoreGoodsDataList(uint storeID)
	{
		if (StoreDatas.ContainsKey (storeID))
		{
			return StoreDatas[storeID];
		}
		return null;
	}
}
