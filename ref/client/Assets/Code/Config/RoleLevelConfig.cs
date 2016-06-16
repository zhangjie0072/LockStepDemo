using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using fogs.proto.config;


public class RoleLevel
{
	public uint level;
	public uint exp;
	public float factor;
	public uint role_quality_limit;
	public List<uint> skill_limit = new List<uint>();
}

public class RoleLevelConfig
{
    string name = GlobalConst.DIR_XML_ROLELEVEL;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public Dictionary<uint, RoleLevel> RoleLevelDatas = new Dictionary<uint, RoleLevel>();

    public RoleLevelConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ROLELEVEL, LoadFinish);
        //ReadRoleLevelData();
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
        RoleLevelDatas.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROLELEVEL, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
			RoleLevel role_level = new RoleLevel();
			
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "Level")
                {
                    uint.TryParse(xel.InnerText, out role_level.level);
                }
                else if (xel.Name == "Exp")
                {
                    uint.TryParse(xel.InnerText, out role_level.exp);
                }
				else if (xel.Name == "factor")
				{
					float.TryParse(xel.InnerText, out  role_level.factor);
				}
				else if( xel.Name.Contains("passive_skill_limit"))
				{
					uint skill_limit;
					uint.TryParse(xel.InnerText, out skill_limit);
					role_level.skill_limit.Add( skill_limit);
				}
				
            }
            if (!RoleLevelDatas.ContainsKey(role_level.level))
            {
                RoleLevelDatas.Add(role_level.level, role_level);
            }
        }

		Logger.ConfigEnd(name);
    }

    public float GetFactor( uint level )
    {
        if (RoleLevelDatas.ContainsKey(level))
            return RoleLevelDatas[level].factor;
        return 1.0f;
    }
    public uint GetMaxExp(uint level)
    {
        if (RoleLevelDatas.ContainsKey(level))
            return RoleLevelDatas[level].exp;
        return 0x7FFF;
    }

}
