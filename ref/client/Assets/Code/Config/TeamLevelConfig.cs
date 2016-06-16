using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using fogs.proto.config;

public class TeamLevelData
{
	public uint award_id;
	public short flag;
    public uint exp;
    public uint max_hp;
    public uint add_hp;
    public uint max_role_quality;
    public uint max_tattoo;
    public uint max_train;
    public uint max_passive_skill;
    public List<string> unlock_icon = new List<string>();
    public List<string> unlock_describe = new List<string>();
    public List<uint> link = new List<uint>();
    public List<uint> subId= new List<uint>();
}

public class TeamLevelConfig
{
    string name = GlobalConst.DIR_XML_TEAMLEVEL;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public Dictionary<uint, TeamLevelData> TeamLevelDatas = new Dictionary<uint, TeamLevelData>();

    public TeamLevelConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TEAMLEVEL, LoadFinish);
        //ReadTeamLevelData();
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
        TeamLevelDatas.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TEAMLEVEL, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            TeamLevelData data = new TeamLevelData();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "Level")
                {
                    uint.TryParse(xel.InnerText, out level);
                }
                else if (xel.Name == "Exp")
                {
                    uint.TryParse(xel.InnerText, out data.exp);
                }
                else if (xel.Name == "max_hp")
                {
                    uint.TryParse(xel.InnerText, out data.max_hp);
                }
                else if (xel.Name == "add_hp")
                {
                    uint.TryParse(xel.InnerText, out data.add_hp);
                }
                else if (xel.Name == "role_qua_limit")
                {
                    uint.TryParse(xel.InnerText, out data.max_role_quality);
                }
                else if (xel.Name == "tattoo_limit")
                {
                    uint.TryParse(xel.InnerText, out data.max_tattoo);
                }
                else if (xel.Name == "train_limit")
                {
                    uint.TryParse(xel.InnerText, out data.max_train);
                }
                else if (xel.Name == "passive_skill_limit")
                {
                    uint.TryParse(xel.InnerText, out data.max_passive_skill);
				}
				else if (xel.Name == "award_id")
				{
					uint.TryParse(xel.InnerText, out data.award_id);
				}
				else if (xel.Name == "flag")
				{
					short.TryParse(xel.InnerText, out data.flag);
				}
                else if (xel.Name == "unlock_icon")
                {
                    if (!string.IsNullOrEmpty(xel.InnerText))
                    {
                        string[] s = xel.InnerText.Split('&');
                        foreach( var icon in s )
                        {
                            data.unlock_icon.Add(icon);
                        }
                    }
                }
                else if (xel.Name == "unlock_describe")
                {
                    if (!string.IsNullOrEmpty(xel.InnerText))
                    {
                        string[] s = xel.InnerText.Split('&');
                        foreach( var describe in s)
                        {
                            data.unlock_describe.Add(describe);
                        }
                    }
                }
                else if (xel.Name == "link")
                {
                    if (!string.IsNullOrEmpty(xel.InnerText))
                    {
                        if (xel.InnerText.Contains("&"))
                        {
                            string[] entirety = xel.InnerText.Split('&');
                            foreach( string item in entirety )
                            {
                                if( item.Contains(":"))
                                {
                                    string[] it = item.Split(':');
                                    data.link.Add(uint.Parse(it[0]));
                                    data.subId.Add(uint.Parse(it[1]));
                                }
                                else
                                {
                                    data.link.Add(uint.Parse(item));
                                    data.subId.Add(0);
                                }
                            }
                        }
                        else
                        {
                            if (xel.InnerText.Contains(":"))
                            {
                                string[] it = xel.InnerText.Split(':');
                                data.link.Add(uint.Parse(it[0]));
                                data.subId.Add(uint.Parse(it[1]));
                            }
                            else
                            {
                                data.link.Add(uint.Parse(xel.InnerText));
                                data.subId.Add(0);
                            }
                        }
                    }
                }
            }
            if (!TeamLevelDatas.ContainsKey(level))
            {
                TeamLevelDatas.Add(level, data);
            }
        }
		Logger.ConfigEnd(name);
    }

    public uint GetMaxExp(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.exp;
        return 0x7FFF;
    }

    public uint GetMaxHP(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.max_hp;
        return 0;
    }

    public uint GetMaxRoleQuality(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.max_role_quality;
        return 0;
    }

    public uint GetMaxTattoo(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.max_tattoo;
        return 1;
    }

    public uint GetMaxTrain(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.max_train;
        return 1;
    }


    public uint GetMaxPassiveSkill(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data))
            return data.max_passive_skill;
        return 1;
    }


    public uint GetQualityLimitLevel(uint quality)
    {
        foreach (KeyValuePair<uint, TeamLevelData> child in TeamLevelDatas)
        {
            if (child.Value.max_role_quality == quality)
            {
                return child.Key;
            }
        }
        return 0;
    }

    public uint GetAddHp(uint level)
    {
        TeamLevelData data;
        if (TeamLevelDatas.TryGetValue(level, out data)) 
        {
            return data.add_hp;
        }
        return 0;
    }

    public TeamLevelData GetUnLockdata(uint level)
    {
        if (TeamLevelDatas.ContainsKey(level))
        {
            if (TeamLevelDatas[level].unlock_icon.Count != 0)
                return TeamLevelDatas[level];
            else
                return null;
        }
        return null;
    }
	public TeamLevelData GetTeamLevelDataWithReward(uint level)
	{
		if(TeamLevelDatas.ContainsKey(level))
		{
			if(TeamLevelDatas[level].award_id!=0)
			{
				return TeamLevelDatas[level];
			}
		}
		return null;
	}

}
