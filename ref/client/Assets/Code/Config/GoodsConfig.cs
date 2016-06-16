using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.config;

public class SuitAddnAttrData
{
    public uint goodsID;
    public uint suitID;
    public List<uint> parts = new List<uint>();
    public Dictionary<uint, Dictionary<uint, uint>> addn_attr = new Dictionary<uint, Dictionary<uint, uint>>();
    public Dictionary<uint, Dictionary<uint, uint>> multi_attr = new Dictionary<uint, Dictionary<uint, uint>>();
}

public class GoodsConfig
{
    string name1 = GlobalConst.DIR_XML_GOODSATTR;
    string name2 = GlobalConst.DIR_XML_GOODSUSE;
    string name3 = GlobalConst.DIR_XML_GOODSCOMPOSITE;
    bool isLoadFinish = false;
    public bool isReadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    private Dictionary<uint, GoodsAttrConfig> goodsAttrConfig = new Dictionary<uint,GoodsAttrConfig>();
    private Dictionary<uint, GoodsUseConfig> goodsUseConfig = new Dictionary<uint, GoodsUseConfig>();
    private Dictionary<uint, GoodsCompositeConfig> goodsCompositeConfig = new Dictionary<uint, GoodsCompositeConfig>();
    private Dictionary<uint, SuitAddnAttrData> suitAttrConfig = new Dictionary<uint, SuitAddnAttrData>();

    public GoodsConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_GOODSATTR, LoadFinish);
        //ReadGoodsAttrData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_GOODSUSE, LoadFinish);
        //ReadGoodsUseData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_GOODSCOMPOSITE, LoadFinish);
        //ReadGoodsCompositeData();
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 3)
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
		ReadGoodsAttrData();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadGoodsUseData();
		Logger.ConfigEnd(name2);
		Logger.ConfigBegin(name3);
		ReadGoodsCompositeData();
		Logger.ConfigEnd(name3);
        isReadFinish = true;
    }

    public void ReadGoodsAttrData()
    {
        //string text = ResourceLoadManager.Instance.GetConfigText(name1);
        //if (text == null)
        //{
        //    Logger.LogError("LoadConfig failed: " + name1);
        //    return;
        //}
        goodsAttrConfig.Clear();
        suitAttrConfig.Clear();
        //TextAsset conf = ResourceLoadManager.Instance.GetResources(GlobalConst.DIR_XML_GOODSATTR) as TextAsset;
        //TextAsset conf = obj as TextAsset;

        MemoryStream stream = new MemoryStream(ResourceLoadManager.Instance.GetConfigByte(GlobalConst.DIR_XML_GOODSATTR));
        BinaryReader objBinaryRead = new BinaryReader(stream);

        int count = objBinaryRead.ReadInt32(); 
        for (int i = 0; i < count; ++i)
        {
            GoodsAttrConfig config = new GoodsAttrConfig();
            config.id = (uint)objBinaryRead.ReadInt32();
            config.name = objBinaryRead.ReadString();
            config.icon = objBinaryRead.ReadString();
            config.purpose = objBinaryRead.ReadString();
            config.intro = objBinaryRead.ReadString();
            config.category = (uint)objBinaryRead.ReadInt32();
            config.sub_category = objBinaryRead.ReadString();
            config.gender = (uint)objBinaryRead.ReadInt32();
            config.suit_id = (uint)objBinaryRead.ReadInt32();
            config.suit_addn_attr = objBinaryRead.ReadString();
            config.suit_multi_attr = objBinaryRead.ReadString();
            config.quality = (uint)objBinaryRead.ReadInt32();
            config.stack_num = (uint)objBinaryRead.ReadInt32();
            config.can_use = (uint)objBinaryRead.ReadInt32();
            config.use_result_id = (uint)objBinaryRead.ReadInt32();
            config.can_sell = (uint)objBinaryRead.ReadInt32();
            config.sell_id = (uint)objBinaryRead.ReadInt32();
            config.sell_price = (uint)objBinaryRead.ReadInt32();
            config.access_way_type = (uint)objBinaryRead.ReadInt32();
            config.access_way = objBinaryRead.ReadString();
            config.show_special_effect = objBinaryRead.ReadString();

            if (config.suit_id != 0)
            {
                SuitAddnAttrData suitAttrData = new SuitAddnAttrData();
                suitAttrData.goodsID = config.id;
                suitAttrData.suitID = config.suit_id;
                suitAttrData.parts.Add(uint.Parse(config.sub_category));
                //加系数
                string[] entirety1 = config.suit_addn_attr.Split('|');
                for (uint dei = 0; dei < entirety1.Length; ++dei)
                {
                    Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
                    string[] items = entirety1[dei].Split('&');
                    for (int dej = 0; dej < items.Length; ++dej)
                    {
                        string[] detail = items[dej].Split(':');
                        if (detail.Length < 2)
                            continue;
                        addn_attr[uint.Parse(detail[0])] = uint.Parse(detail[1]);
                    }
                    suitAttrData.addn_attr.Add(dei+1, addn_attr);
                }
                //乘系数
                string[] entirety2 = config.suit_multi_attr.Split('|');
                for (uint dei = 0; dei < entirety2.Length; ++dei)
                {
                    Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
                    string[] items = entirety2[dei].Split('&');
                    for (int dej = 0; dej < items.Length; ++dej)
                    {
                        string[] detail = items[dej].Split(':');
                        if (detail.Length < 2)
                            continue;
                        addn_attr[uint.Parse(detail[0])] = uint.Parse(detail[1]);
                    }
                    suitAttrData.multi_attr.Add(dei+1, addn_attr);
                }
                suitAttrConfig.Add(config.id, suitAttrData);
            }

            if (!goodsAttrConfig.ContainsKey(config.id))
            {
                goodsAttrConfig.Add(config.id, config);
            }
        }
       
        objBinaryRead.Close();
        stream.Close();
        //读取以及处理XML文本的类
        //XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GOODSATTR);
        //解析xml的过程
        /*XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            GoodsAttrConfig config = new GoodsAttrConfig();
            foreach (XmlElement xel in xe)
            {
                uint value = 0;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "name")
                {
                    config.name = xel.InnerText;
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;
                }
                else if (xel.Name == "intro")
                {
                    config.intro = xel.InnerText;
                }
                else if (xel.Name == "category")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.category = value;
                }
                else if (xel.Name == "sub_category")
                {
                   // uint.TryParse(xel.InnerText, out value);
                    config.sub_category = xel.InnerText;
                }
                else if (xel.Name == "quality")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.quality = value;
                }
                else if (xel.Name == "can_use")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.can_use = value;
                }
                else if (xel.Name == "use_result_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.use_result_id = value;
                }
                else if (xel.Name == "stack_num")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.stack_num = value;
                }
                else if (xel.Name == "can_sell")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.can_sell = value;
                }
                else if (xel.Name == "sell_price")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.sell_price = value;
                }
                else if (xel.Name == "can_composite")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.can_composite = value;
                }
                else if( xel.Name == "gender" )
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.gender = value;
                }
                else if (xel.Name == "purpose")
                {
                    config.purpose = xel.InnerText;
                }            
            }
            if (!goodsAttrConfig.ContainsKey(config.id))
            {
                goodsAttrConfig.Add(config.id, config);
            }
        }*/
    }

    public void ReadGoodsUseData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        goodsUseConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GOODSUSE, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            GoodsUseConfig config = new GoodsUseConfig();
            GenerateNewGoodsArgConfig args = null;
            foreach (XmlElement xel in xe)
            {
                uint value = 0;
                if (xel.Name == "id")
                {
                    
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "pack_num")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.pack_num = value;
                }
                else if (xel.Name.Contains("new_type"))
                {
                    args = new GenerateNewGoodsArgConfig();
                    uint.TryParse(xel.InnerText, out value);
                    args.type = value;
                }
                else if (xel.Name.Contains("new_id"))
                {          
                    uint.TryParse(xel.InnerText, out value);
                    args.id = value;
                }
                else if (xel.Name.Contains("odds"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    args.odds = value;
                }
                else if (xel.Name.Contains("num_min"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    args.num_min = value;
                }
                else if (xel.Name.Contains("num_max"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    args.num_max = value;

                    if( args.id != 0 )
                    {
                        config.args.Add(args);
                    }
                }
            }
            if (!goodsUseConfig.ContainsKey(config.id))
            {
                goodsUseConfig.Add(config.id, config);
            }
        }
    }

    public void ReadGoodsCompositeData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name3);
            return;
        }
        goodsCompositeConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_GOODSCOMPOSITE, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            GoodsCompositeConfig config = new GoodsCompositeConfig();
            foreach (XmlElement xel in xe)
            {
                uint value = 0;
                if (xel.Name == "src_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.src_id = value;
                }
                else if (xel.Name == "src_num")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.src_num = value;
                }
                else if (xel.Name == "dest_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.dest_id = value;
                }
                else if (xel.Name == "dest_num")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.dest_num = value;
                }
                else if (xel.Name == "costing")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.costing = value;
                }
            }
            if (!goodsCompositeConfig.ContainsKey(config.src_id))
            {
                goodsCompositeConfig.Add(config.src_id, config);
            }
        }
    }
    /// <summary>
    /// 根据物品ID获取物品信息
    /// </summary>
    /// <param name="good_id"></param>
    /// <returns></returns>
    public GoodsAttrConfig GetgoodsAttrConfig(uint good_id)
    {
        if (goodsAttrConfig.ContainsKey(good_id))
        {
            return goodsAttrConfig[good_id];
        }
        return null;
    }

    public bool CanComposite(uint goods_id)
    {
        return goodsCompositeConfig.ContainsKey(goods_id);
    }

    public GoodsCompositeConfig GetComposite(uint id)
    {
        if (goodsCompositeConfig.ContainsKey(id))
        {
            return goodsCompositeConfig[id];
        }
        return null;
    }

    public SuitAddnAttrData GetSuitAttrConfig(uint goods_id)
    {
        foreach (KeyValuePair<uint, SuitAddnAttrData> suitData in suitAttrConfig)
        {
            if (suitData.Value.goodsID == goods_id)
                return suitData.Value;
        }
        return null;
    }

    public GoodsUseConfig GetGoodsUseConfig(uint id)
    {
        if (goodsUseConfig.ContainsKey(id))
        {
            return goodsUseConfig[id];
        }
        return null;
    }
    
    public Dictionary<uint,GoodsAttrConfig> GetGoodsDicByCategory(uint catetory)
    {
        Dictionary<uint, GoodsAttrConfig> cgGoods = new Dictionary<uint, GoodsAttrConfig>();
        foreach(KeyValuePair<uint,GoodsAttrConfig> goods in goodsAttrConfig)
        {
            if(goods.Value.category == catetory)
            {
                cgGoods.Add(goods.Key, goods.Value);
            }
        }
        return cgGoods;
    }
}
