
using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class QualityAttrCor
{
    public int roleId;
    public int quality;
    public int symbol;
    public Dictionary<uint, uint> consume = new Dictionary<uint, uint>();
    public float factor;

}


public class QualityAttrCorConfig
{
    string name = GlobalConst.DIR_XML_QUALITY_ATTR_COR;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static List<QualityAttrCor> attrData = new List<QualityAttrCor>();

    public QualityAttrCorConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadAttr();
    }
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }

   public Dictionary<uint, uint> GetConsume( uint roleId, uint quality )
   {
       foreach( var item in attrData )
       {
           if(item.roleId == roleId )
           {
               return item.consume;
           }
       }
        return null;
   }
    public float GetFactor( uint roleId, uint quality )
    {
        foreach( var item in attrData )
        {
            if( item.roleId == roleId && item.quality == quality )
            {
                return item.factor;
            }
        }
        return 1.0f;
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
        
        attrData.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_QUALITY_ATTR_COR, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            QualityAttrCor data = new QualityAttrCor();

            foreach (XmlElement xel in xe)
            {
                if( xel.Name == "ID")
                {
                    int.TryParse(xel.InnerText, out data.roleId);
                }
                else if (xel.Name == "quality")
                {
                    int.TryParse(xel.InnerText, out data.quality);
                }
                else if (xel.Name == "symbol")
                {
                    int.TryParse(xel.InnerText, out data.symbol);
                }
                else if (xel.Name == "consume")
                {
                    string[] consumeArray = xel.InnerText.Split('&');
                    foreach (string items in consumeArray)
                    {
                        string[]  item = items.Split(':');
                        if( item.Length == 2 )
                        {
                            uint id, value;
                            uint.TryParse( item[0], out id );
                            uint.TryParse( item[1], out value );
                            data.consume.Add(id, value);
                        }
                    }
                }
                else if (xel.Name == "factor")
                {
                    float.TryParse( xel.InnerText, out data.factor );
                }
            }
            attrData.Add(data);
        }

		Logger.ConfigEnd(name);
    }

}
