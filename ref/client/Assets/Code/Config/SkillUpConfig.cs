
using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class SkillUp
{
    public int id;
    public int level;
    public Dictionary<uint, uint> consume = new Dictionary<uint, uint>();
    public Dictionary<uint, uint> addn_attr = new Dictionary<uint, uint>();
}


public class SkillUpConfig
{
    string name = GlobalConst.DIR_XML_SKILL_UPGRADE;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static List<SkillUp> skillUpList = new List<SkillUp>();


    public Dictionary<uint, uint> GetSkillAttr(int id, int level)
    {
        foreach (SkillUp item in skillUpList)
        {
            if (item.id == id && item.level == level)
            {
                return item.addn_attr;
            }
        }
        return null;
    }

    public Dictionary<uint, uint> GetSkillConsume(int id, int level)
    {
        foreach (SkillUp item in skillUpList)
        {
            if (item.id == id && item.level == level)
            {
                return item.consume;
            }
        }
        return null;
    }
    public Dictionary<string, uint> GetSkillAttrSymbol(int id, int level)
    {
        Dictionary<string, uint> skillAttr = new Dictionary<string, uint>();

        foreach( SkillUp item in skillUpList )
        {
            if( item.id == id && item.level == level)
            {
                foreach( KeyValuePair<uint,uint> kv in item.addn_attr)
                {
                    string symbol = GameSystem.Instance.AttrNameConfigData.GetAttrSymbol((uint)kv.Key);
                    skillAttr.Add(symbol, kv.Value);
                }
                return skillAttr;
            }
        }
        return null;
    }

    public SkillUpConfig()
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
        
        skillUpList.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SKILL_UPGRADE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            SkillUp data = new SkillUp();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    int.TryParse(xel.InnerText, out data.id);
                }
                if (xel.Name == "level")
                {
                    int.TryParse(xel.InnerText, out data.level);
                }
                else if (xel.Name == "consume")
                {
                    string[] array = xel.InnerText.Split('&');
                    foreach (string items in array)
                    {
                        string[] item = items.Split(':');
                        if( item.Length == 2 )
                        {
                            uint id;
                            uint value;
                            uint.TryParse(item[0], out id);
                            uint.TryParse(item[1], out value);
                            data.consume.Add(id, value);
                        }
                    }
                }
                else if (xel.Name == "addn_attr")
                {
                    string[] array = xel.InnerText.Split('&');
                    foreach (string items in array)
                    {
                        string[] item = items.Split(':');
                        if (item.Length == 2)
                        {
                            uint id;
                            uint value;
                            uint.TryParse(item[0], out id);
                            uint.TryParse(item[1], out value);
                            data.addn_attr.Add(id, value);
                        }
                    }
                }
            }

            skillUpList.Add(data);
        }
		
    }




}
