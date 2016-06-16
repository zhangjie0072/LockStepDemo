
using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class StarAttr
{
    public int id;
    public int star;
    public Dictionary<uint, uint> consume = new Dictionary<uint, uint>();
    public Dictionary<uint, uint> attrs = new Dictionary<uint, uint>();

	public uint GetAttrValue(uint id)
	{
		if (attrs.ContainsKey(id))
			return attrs[id];
		return 0;
	}
}


public class StarAttrConfig
{
    string name = GlobalConst.DIR_XML_STAR_ATTR;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static List<StarAttr> starAttrData = new List<StarAttr>();
    public StarAttrConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadStarAttr();
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
        
        starAttrData.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_STAR_ATTR, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            StarAttr data = new StarAttr();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "ID")
                {
                    int.TryParse(xel.InnerText, out data.id);
                }
                if (xel.Name == "star")
                {
                    int.TryParse(xel.InnerText, out data.star);
                }
                if (xel.Name == "consume")
                {
                    string[] consumes = xel.InnerText.Split('&');
                    foreach (string consume in consumes)
                    {
                        string[] item = consume.Split(':');
    
                        if(item.Length == 2)
                        {
                            uint id;
                            uint value;
                            uint.TryParse(item[0], out id);
                            uint.TryParse(item[1], out value);
                            data.consume.Add(id, value);
                        }       
                    }
                }
                else if (xel.Name == "attrs")
                {
                    string[] attrs = xel.InnerText.Split('&');
                    foreach (string attr in attrs)
                    {
                        string[] item = attr.Split(':');
                        uint id;
                        uint value;
                        uint.TryParse(item[0], out id);
                        uint.TryParse(item[1], out value);
                        data.attrs.Add(id,value);
                    }
                }

            }
            starAttrData.Add(data);
        }
		Logger.ConfigEnd(name);
    }


    public StarAttr GetStarAttr( uint id, uint star )
    {
        foreach( StarAttr item in starAttrData )
        {
            if( item.id == id && item.star == star )
            {
                return item;
            }
        }

        return null;
    }

    public Dictionary<uint, uint> GetAttr( uint id, uint star )
    {
        StarAttr atr = GetStarAttr(id, star);
        if( atr != null )
        {
            return atr.attrs;
        }
        return null;
    }
}
