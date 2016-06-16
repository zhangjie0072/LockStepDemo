using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class FashionMatch
{
    public string body_type;
    public string shape_id;
    public List<uint> hide_id = new List<uint>();
}
public class FashionItem
{
	public uint		fashion_id;
    public Dictionary<string, FashionMatch> fashion_matchs = new Dictionary<string, FashionMatch>();
}

public class HidePart
{
	public uint		hide_id;
	public string	part_id;
}

public class FashionAttr 
{
    public uint attr_id;
    public uint player_attr_id;
    public uint player_attr_num;
}

public class FashionData
{
    public uint fashion_id;
    public uint reset_id;
    public uint reset_num;
}

public class FashionConfig
{
    string name1 = GlobalConst.DIR_XML_FASHION;
    string name2 = GlobalConst.DIR_XML_BONE_MAPPING;
    string name3 = GlobalConst.DIR_XML_FASHIONATTR;
    string name4 = GlobalConst.DIR_XML_FASHIONDATA;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	public Dictionary<uint, FashionItem> configs = new Dictionary<uint, FashionItem>();
	public Dictionary<uint, HidePart> mapHideParts = new Dictionary<uint, HidePart>();
    public Dictionary<uint, FashionAttr> fashionAttrs = new Dictionary<uint, FashionAttr>();
    public Dictionary<uint, FashionData> fashionDatas = new Dictionary<uint, FashionData>();

	public FashionConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //ReadConfig();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name4, LoadFinish);
    }
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 4)
        {
            isLoadFinish = true;
            lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
        }
    }
	public FashionItem GetConfig(uint fashion_id)
    {
		FashionItem data = null;
		configs.TryGetValue(fashion_id, out data);
        return data;
    }

	public HidePart MappingPart(uint hide_id)
	{
		HidePart data = null;
		mapHideParts.TryGetValue(hide_id, out data);
		return data;
	}

    public FashionAttr GetFashionAttr(uint attr_id)
    {
        FashionAttr fashionAttr = null;
        fashionAttrs.TryGetValue(attr_id, out fashionAttr);
        return fashionAttr;
    }

    public FashionData GetFashionData(uint fashion_id) 
    {
        FashionData data = null;
        fashionDatas.TryGetValue(fashion_id, out data);
        return data;
    }

    public FashionAttr GetRandomFashionAttr() 
    {
        FashionAttr attr = null;
        uint keyIndex = (uint)Random.Range(1, fashionAttrs.Count);
        uint index = 1;
        foreach (KeyValuePair<uint, FashionAttr> item in fashionAttrs)
        {
            if (index == keyIndex) 
            {
                attr = item.Value;
                return attr;
            }
            index += 1;
        }
        return attr;
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name1);
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }

        //读取以及处理XML文本的类
        XmlDocument doc = CommonFunction.LoadXmlConfig(name1, text);

        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            FashionItem data = new FashionItem();
            data.fashion_id = uint.Parse(node_line.SelectSingleNode("id").InnerText);

            for (int i = 0; i < 6; i++)
            {
                string tmp = "shape_id" + (i + 1);

                string shaderID = node_line.SelectSingleNode(tmp).InnerText;
                if (shaderID == "")
                {
                    continue;
                }

                FashionMatch item = new FashionMatch();

                item.shape_id = shaderID;

                tmp = "bone_id" + (i + 1);
                string strHideId = node_line.SelectSingleNode(tmp).InnerText;
                string[] tokens = strHideId.Split('&');
                foreach (string token in tokens)
                {
                    uint hide_Id;
                    if (uint.TryParse(token, out hide_Id))
                        item.hide_id.Add(hide_Id);
                }
                tmp = "body_type" + (i + 1);
                item.body_type = node_line.SelectSingleNode(tmp).InnerText;


                data.fashion_matchs.Add(item.body_type, item);
            }
      



            configs.Add(data.fashion_id, data);
        }

		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);

        text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }

        //读取以及处理XML文本的类
        doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_BONE_MAPPING, text);
        node_data = doc.SelectSingleNode("Data");
		foreach (XmlNode node_line in node_data.SelectNodes("Line"))
		{
			HidePart data = new HidePart();
			data.hide_id = uint.Parse(node_line.SelectSingleNode("id").InnerText);
			data.part_id = node_line.SelectSingleNode("part").InnerText;
			mapHideParts.Add(data.hide_id, data);
		}

		Logger.ConfigEnd(name2);
		Logger.ConfigBegin(name3);
        text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name3);
            return;
        }

        //读取以及处理XML文本的类
        doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_FASHIONATTR, text);
        node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            FashionAttr fashionAttr = new FashionAttr();
            fashionAttr.attr_id = uint.Parse(node_line.SelectSingleNode("attrs_id").InnerText);
            string attrsStr = node_line.SelectSingleNode("attrs").InnerText.ToString();
            string[] attr = attrsStr.Split(':');
            fashionAttr.player_attr_id = uint.Parse(attr[0]);
            fashionAttr.player_attr_num = uint.Parse(attr[1]);

            if (!fashionAttrs.ContainsKey(fashionAttr.attr_id))
                fashionAttrs.Add(fashionAttr.attr_id, fashionAttr);
        }

		Logger.ConfigEnd(name3);
		Logger.ConfigBegin(name4);
        text = ResourceLoadManager.Instance.GetConfigText(name4);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name4);
            return;
        }

        //读取以及处理XML文本的类
        doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_FASHIONDATA, text);
        node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            FashionData fashionData = new FashionData();
            fashionData.fashion_id = uint.Parse(node_line.SelectSingleNode("id").InnerText);
            string resetInfo = node_line.SelectSingleNode("reset_price").InnerText.ToString();
            string[] resetArr = resetInfo.Split(':');
            fashionData.reset_id = uint.Parse(resetArr[0]);
            fashionData.reset_num = uint.Parse(resetArr[1]);

            if (!fashionDatas.ContainsKey(fashionData.fashion_id))
                fashionDatas.Add(fashionData.fashion_id, fashionData);
		}
		Logger.ConfigEnd(name4);
	}
}
