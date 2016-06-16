using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public class MatchAchievement
{
	public PlayerStatistics.StatType type;
	public uint level;
	public uint value;
	public string title;
	public string intro;
}

public class MatchAchievementConfig
{
    string name = GlobalConst.DIR_XML_MATCH_ACHIEVEMENT;
    bool isLoadFinish = false;
    private object LockObject = new object();

	Dictionary<PlayerStatistics.StatType, Dictionary<uint, MatchAchievement>> achievements = new Dictionary<PlayerStatistics.StatType, Dictionary<uint, MatchAchievement>>();

	public MatchAchievementConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
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
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_MATCH_ACHIEVEMENT, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			MatchAchievement data = new MatchAchievement();
			data.type = (PlayerStatistics.StatType)(int.Parse(line.SelectSingleNode("type").InnerText));
			data.level = uint.Parse(line.SelectSingleNode("level").InnerText);
			data.value = uint.Parse(line.SelectSingleNode("value").InnerText);
			data.title = line.SelectSingleNode("title").InnerText;
			data.intro = line.SelectSingleNode("intro").InnerText;

			Dictionary<uint, MatchAchievement> levels = null;
			if (!achievements.TryGetValue(data.type, out levels))
			{
				levels = new Dictionary<uint, MatchAchievement>();
				achievements.Add(data.type, levels);
			}
			if (levels.ContainsKey(data.level))
				Logger.LogError("Match achievement config error. Type: " + data.type + " Level:" + data.level + " already existed.");
			levels.Add(data.level, data);
		}
		Logger.ConfigEnd(name);
	}

	public MatchAchievement GetMatchAchievement(PlayerStatistics.StatType type, uint value)
	{
		Dictionary<uint, MatchAchievement> levels = null;
		if (achievements.TryGetValue(type, out levels))
		{
			for (uint i = 3; i > 0; --i)
			{
				if (value >= levels[i].value)
					return levels[i];
			}
		}
		return null;
	}
}
