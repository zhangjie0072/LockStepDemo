using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class GoodsComposeBaseNewConfig
{
    //要合成的物品ID
    public uint id;
    //合成这种物品需要的物品ID和数量集合(key表示需要的物品ID,value表示需要对应物品的数量)
    public Dictionary<uint, uint> needIDs = new Dictionary<uint, uint>();
    //需要的背包数量
    public uint packNum;
    //消耗物品和数量
    public Dictionary<uint, uint> costIDs = new Dictionary<uint, uint>();
}
public class GoodsComposeNewConfig{

    string name = GlobalConst.DIR_XML_GOODS_COMPOSE_NEW;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<uint, GoodsComposeBaseNewConfig> allConfig = new Dictionary<uint, GoodsComposeBaseNewConfig>();
    public GoodsComposeNewConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
    //解析读取配置
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
        allConfig.Clear();
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);
        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            //if (node_line.SelectSingleNode("switch").InnerText == "#")
                //continue;
            GoodsComposeBaseNewConfig data = new GoodsComposeBaseNewConfig();
            data.id = uint.Parse(node_line.SelectSingleNode("dest").InnerText);
            string needGoodsStr = node_line.SelectSingleNode("src").InnerText;
            if(needGoodsStr!="")
            {
                string[] goodsInfo = needGoodsStr.Split('&');
                for(int i=0;i<goodsInfo.Length;i++)
                {
                    string[] singleGoodsInfo = goodsInfo[i].Split(':');
                    data.needIDs.Add(uint.Parse(singleGoodsInfo[0]),uint.Parse(singleGoodsInfo[1]));
                }
            }
            string costGoodsStr = node_line.SelectSingleNode("costing").InnerText;
            if(costGoodsStr!="")
            {
                string[] costGoodInfo = costGoodsStr.Split('&');
                for(int i =0;i<costGoodInfo.Length;i++)
                {
                    string[] singleCostGoodInfo = costGoodInfo[i].Split(':');
                    data.costIDs.Add(uint.Parse(singleCostGoodInfo[0]), uint.Parse(singleCostGoodInfo[1]));
                }
            }
            allConfig.Add(data.id, data);
        }

		
    }

    public GoodsComposeBaseNewConfig GetBaseConfig(uint id)
    {
        if (allConfig.ContainsKey(id))
            return allConfig[id];
        return null;
    }

}
