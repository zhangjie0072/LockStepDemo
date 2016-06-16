using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.msg;
using fogs.proto.config;

public class RoleGiftConfig
{
    string name = GlobalConst.DIR_XML_ROLE_GIFT;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static Dictionary<uint, List<uint>> roleList = new Dictionary<uint, List<uint>>();

    public RoleGiftConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ROLE_GIFT, LoadFinish);
        //ReadConfig();
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
        
        roleList.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROLE_GIFT, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            uint id = 0;
            List<uint> data = new List<uint>();
            foreach (XmlElement xel in land)
            {
                if (xel.Name == "id")
                {
                    id = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "role1")
                {
                    data.Add(uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "role2")
                {
                    data.Add(uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "role3")
                {
                    data.Add(uint.Parse(xel.InnerText));
                }  
            }
            roleList.Add(id, data);
        }

		Logger.ConfigEnd(name);
    }

    public List<uint> GetRoleGiftList( uint id)
    {
        if (roleList.ContainsKey(id))
        {
            return roleList[id];
        }
        return null;
    }
}
