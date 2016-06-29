using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class LadderLevel 
{
	public uint level;
	public string name;
	public uint score_start;
	public uint score_end;
    public string icon;
    public string iconSmall;
};

public class LadderSeason
{
    public uint season;
    public uint startYear;
    public uint startMonth;
    public uint startDay;
    public uint endYear;
    public uint endMonth;
    public uint endDay;
}

public class LadderReward
{
    public uint win_times;
    public Dictionary<uint, uint> rewards = new Dictionary<uint, uint>();
    public uint extra_score;

}

public class LadderConfig
{
    string name1 = GlobalConst.DIR_XML_LADDER_LEVEL;
    string name2 = GlobalConst.DIR_XML_LADDER_SEASON;
    string name3 = GlobalConst.DIR_XML_LADDER_REWARD;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;
      
    public static Dictionary<uint, LadderLevel> levels = new Dictionary<uint, LadderLevel>();
    public static Dictionary<uint,LadderSeason> seasons = new Dictionary<uint, LadderSeason>();
    public static Dictionary<uint, LadderReward> rewards = new Dictionary<uint, LadderReward>();

    public LadderConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);

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

		Debug.Log("Config reading " + name1);
		Read();
		Debug.Log("Config reading " + name2);
		
		ReadSeason();
		Debug.Log("Config reading " + name3);
		
		ReadReward();
		
    }


    public void Read()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        levels.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_LADDER_LEVEL, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            LadderLevel level = new LadderLevel();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "level")
                {
                    uint.TryParse(xel.InnerText, out level.level);
                }
                else if (xel.Name == "name")
                {
                    level.name = xel.InnerText;
                }
                else if (xel.Name == "score_start")
                {
                    uint.TryParse(xel.InnerText, out level.score_start);
                }
                else if (xel.Name == "score_end")
                {
                    uint.TryParse(xel.InnerText, out level.score_end);
                }
                else if (xel.Name == "icon")
                {
                    level.icon = xel.InnerText;
                }
                else if (xel.Name == "icon_small")
                {
                    level.iconSmall = xel.InnerText;
                }
            }

            levels.Add(level.level,level);
        }
    }

    public void ReadSeason()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_LADDER_SEASON, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            LadderSeason season = new LadderSeason();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "season")
                {
                    uint.TryParse(xel.InnerText, out season.season);
                }
                else if (xel.Name == "start_time")
                {
                    string date = xel.InnerText.Split(' ')[0];
                    string[] items = date.Split('-');

                    uint.TryParse(items[0], out season.startYear);
                    uint.TryParse(items[1], out season.startMonth);
                    uint.TryParse(items[2], out season.startDay);

                }
                else if (xel.Name == "end_time")
                {
                    string date = xel.InnerText.Split(' ')[0];
                    string[] items = date.Split('-');

                    uint.TryParse(items[0], out season.endYear);
                    uint.TryParse(items[1], out season.endMonth);
                    uint.TryParse(items[2], out season.endDay);
                }
            }
            seasons.Add(season.season, season);
        }
    }


    public void ReadReward()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name3);
            return;
        }

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_LADDER_REWARD, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            LadderReward reward = new LadderReward();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "win_times")
                {
                    uint.TryParse(xel.InnerText, out reward.win_times);
                }
                else if (xel.Name == "rewards")
                {
                    string[] items = xel.InnerText.Split('&');
                    foreach(var item in items)
                    {
                        string[] v = item.Split(':');
                        if(v.Length != 2 )
                        {
                            continue;
                        }
                        uint id, num;
                        uint.TryParse(v[0], out id);
                        uint.TryParse(v[1], out num);
                        reward.rewards.Add(id,num);
                    }
                }
                else if (xel.Name == "extra_score")
                {
                    uint.TryParse(xel.InnerText, out reward.extra_score);
                }
            }
            rewards.Add(reward.win_times, reward);
        }
    }


    public LadderLevel GetLevelByScore(uint score)
    {
        foreach (var kv in levels)
        {
            if (score >= kv.Value.score_start && score <= kv.Value.score_end)
            {
                return kv.Value;
            }
        }
        return null;
    }

    public LadderSeason GetSeason(uint season)
    {
        if (!seasons.ContainsKey(season))
        {
            string log = string.Format("{0}.xml 没有找到 season={1}", GlobalConst.DIR_XML_LADDER_SEASON, season);
            Debug.LogError(log);
            return null;
        }
        else
        {
            return seasons[season];
        }
    }

    public LadderReward GetReward(uint winTimes)
    {
        if (!rewards.ContainsKey(winTimes))
        {
            string log = string.Format("{0}.xml 没有找到 winTimes={1}", GlobalConst.DIR_XML_LADDER_REWARD, winTimes);
            Debug.LogError(log);
            return null;
        }
        else
        {
            return rewards[winTimes];
        }
    }
}
