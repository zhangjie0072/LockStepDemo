using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class AttrNameData
{
    public uint id;
    public AttributeType type; 
    public string symbol;
    public string name;
    public uint display;
    public List<uint> recommend = new List<uint>();
    public uint is_factor;
	public IM.Number fc_weight;
}


public class AttrNameConfig
{
    string name = GlobalConst.DIR_XML_ATTRNAME;
    bool isLoadFinish = false;
	public bool isReadFinish = false;
    private object LockObject = new object();

    public List<AttrNameData> AttrNameDatas = new List<AttrNameData>();
    public Dictionary<string, string> AttrName = new Dictionary<string, string>();

    public AttrNameConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ParseConfig();
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
        AttrNameDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ATTRNAME, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            AttrNameData data = new AttrNameData();
            foreach (XmlElement xel in xe)
            {
                uint value;
                if (xel.Name == "ID")
                {
                    uint.TryParse(xel.InnerText, out data.id);
                }

                else if (xel.Name == "type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.type = (AttributeType)value;
                }

                else if (xel.Name == "symbol")
                {
                    data.symbol = xel.InnerText;
                }

                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }
                else if (xel.Name == "display")
                {
                    uint.TryParse(xel.InnerText, out data.display);
                }
                else if (xel.Name == "recommend")
                {
                    string s = xel.InnerText;
                    string[] slist= s.Split('&');
                    
                    foreach( string item in slist )
                    {
                        uint n;
                        uint.TryParse(item,out n);
                        data.recommend.Add(n);
                    }
                }
                else if (xel.Name == "is_factor")
                {
                    uint.TryParse(xel.InnerText, out data.is_factor);
                }
				else if (xel.Name == "fc_weight")
				{
					IM.Number.TryParse(xel.InnerText, out data.fc_weight);
				}
            }
            AttrNameDatas.Add(data);
            if (!AttrName.ContainsKey(data.symbol))
            {
                AttrName.Add(data.symbol, data.name);
            }
        }
		isReadFinish = true;
		
    }

 

    public string GetAttrName(string symbol)
    {
        if (AttrName.ContainsKey(symbol))
            return AttrName[symbol];
        return null;
    }

   
    public string GetAttrNameById(uint id)
    {
        // for lua calling.
        return GetAttrName(id);
    }

  
	public string GetAttrName(uint id)
	{
		foreach (AttrNameData data in AttrNameDatas)
		{
			if (data.id == id)
				return data.name;
		}
		return string.Empty;
	}

    public bool IsFactor( uint id )
    {
        foreach (AttrNameData data in AttrNameDatas)
        {
            if( data.id == id )
            {
                return data.is_factor == 1;
            }
        }
        return false;
    }


	public string GetAttrSymbol(uint id)
	{
		foreach (AttrNameData data in AttrNameDatas)
		{
			if (data.id == id)
				return data.symbol;
		}
		return string.Empty;
	}




    public bool IsRecommend(uint id, uint pos )
    {
        foreach (AttrNameData data in AttrNameDatas)
        {
            if (data.id == id)
            {
                List<uint> recommend = data.recommend;
                return recommend.Contains(pos);
            }
        }
        return false;
    }

    public AttrNameData GetAttrData(string symbol)
    {
        foreach (AttrNameData data in AttrNameDatas)
        {
            if (data.symbol == symbol)
                return data;
        }
        return null;
    }

    public AttributeType GetTypeBySymbol(string symbol)
    {
        for (int i = 0; i < AttrNameDatas.Count; ++i)
        {
            if (AttrNameDatas[i].symbol == symbol)
                return AttrNameDatas[i].type;
        }
        return AttributeType.NONE;
    }


    public bool isHide(  uint skillID )
    {
        foreach( AttrNameData i in AttrNameDatas)
        {
            if( i.id == skillID )
            {
                return i.display == 0;
            }
        }
        return true;
    }
}
