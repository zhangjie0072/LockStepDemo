using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class BadgeAttrBaseConfig
{
    //物品ID
    public uint id;
    //物品添加的属性(基础属性）(key表示对象属性的ID，Value表示对应属性加成值）
    public Dictionary<uint, uint> addAttr = new Dictionary<uint, uint>();
    //添加的特殊属性
    public KeyValuePair<uint, uint> specAttr;
    //等级
    public uint level;
}
public class BadgeAttrConfig{
    string name = GlobalConst.DIR_XML_BADGE_ATTRIBUTE;
    bool isLoadFinish = false;
    private object LockObject = new object();
    public Dictionary<uint, BadgeAttrBaseConfig> allConfig = new Dictionary<uint, BadgeAttrBaseConfig>();
    public BadgeAttrConfig()
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

		Logger.ConfigBegin(name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        allConfig.Clear();
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);
        XmlNode node_data = doc.SelectSingleNode("Data");
        foreach (XmlNode node_line in node_data.SelectNodes("Line"))
        {
            if (node_line.SelectSingleNode("switch").InnerText == "#")
                continue;

            BadgeAttrBaseConfig data = new BadgeAttrBaseConfig();
            data.id = uint.Parse(node_line.SelectSingleNode("id").InnerText);
            data.level = uint.Parse(node_line.SelectSingleNode("level").InnerText);
            string addAttrsString = node_line.SelectSingleNode("addn_attr").InnerText;
            if(addAttrsString!="")
            {
                string[] addattrs = addAttrsString.Split('&');
                for(int i =0;i<addattrs.Length;i++)
                {
                    string[] detail = addattrs[i].Split(':');
                    data.addAttr.Add(uint.Parse(detail[0]), uint.Parse(detail[1]));
                }
            }

            string specAttrString = node_line.SelectSingleNode("spec_attr").InnerText;
            if(specAttrString!="")
            {
                string[] specAttrs = specAttrString.Split(':');
                data.specAttr = new KeyValuePair<uint, uint>(uint.Parse(specAttrs[0]), uint.Parse(specAttrs[1]));
            }
            allConfig.Add(data.id, data);
		}
		Logger.ConfigEnd(name);

    }

    /**获取一种物品属性基础配置*/
    public BadgeAttrBaseConfig GetBaseConfig(uint id)
    {
        if (allConfig.ContainsKey(id))
            return allConfig[id];
        return null;
    }

}
