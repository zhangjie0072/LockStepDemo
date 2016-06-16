using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourData
{
	public uint level;
	public uint ID;
	public uint baseAttrLower;
	public uint baseAttrUpper;
	public float hedgingAttrLower;
	public float hedgingAttrUpper;
	public uint challengeConsume;
	public List<uint> winAwards = new List<uint>();
    public List<uint> failAwards = new List<uint>();
	public uint gameModeID;
    public List<uint> quality = new List<uint>();
    public List<uint> star = new List<uint>();
}

public class TourConfig
{
    string name1 = GlobalConst.DIR_XML_TOUR;
    string name2 = GlobalConst.DIR_XML_TOUR_RESET_LIMIT;
    string name3 = GlobalConst.DIR_XML_TOUR_RESET_COST;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	private Dictionary<uint, Dictionary<uint, TourData>> tours = new Dictionary<uint, Dictionary<uint, TourData>>();
	private Dictionary<uint, uint> resetTimes = new Dictionary<uint, uint>();
	private Dictionary<uint, KeyValuePair<uint, uint>> resetCost = new Dictionary<uint, KeyValuePair<uint, uint>>();

	public TourConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
		//ReadTourConfig();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
		//ReadTourResetTimeConfig();
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
		//ReadTourResetCostConfig();
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
		Logger.ConfigBegin(name1);
        ReadTourConfig();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadTourResetTimeConfig();
		Logger.ConfigEnd(name2);
		Logger.ConfigBegin(name3);
		ReadTourResetCostConfig();
		Logger.ConfigEnd(name3);
    }

	public TourData GetTourData(uint level, uint ID)
	{
		Dictionary<uint, TourData> levelTours = null;
		if (tours.TryGetValue(level, out levelTours))
		{
			TourData tour = null;
			if (levelTours.TryGetValue(ID, out tour))
			{
				return tour;
			}
		}
		return null;
	}

	public KeyValuePair<uint, uint> GetResetCost(uint resetTimes)
	{
		KeyValuePair<uint, uint> cost;
		resetCost.TryGetValue(resetTimes, out cost);
		return cost;
	}

	public uint GetResetTimes(uint VIPLevel)
	{
		uint times = 0u;
		resetTimes.TryGetValue(VIPLevel, out times);
		return times;
	}

    void ReadTourConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        tours.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TOUR, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root)
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			uint level = uint.Parse(line.SelectSingleNode("level").InnerText);
			Dictionary<uint, TourData> levelTours = null;
			if (!tours.TryGetValue(level, out levelTours))
			{
				levelTours = new Dictionary<uint, TourData>();
				tours.Add(level, levelTours);
			}
			uint ID = uint.Parse(line.SelectSingleNode("id").InnerText);
			if (levelTours.ContainsKey(ID))
			{
				Logger.LogError("TourConfig, tour config already existed. Level: " + level + " ID: " + ID);
			}

			TourData tour = new TourData();
			tour.level = level;
			tour.ID = ID;

			string[] tokens = line.SelectSingleNode("attr_lower_limit").InnerText.Split('&');
			tour.baseAttrLower = uint.Parse(tokens[0]);
			tour.hedgingAttrLower = float.Parse(tokens[1]);
			tokens = line.SelectSingleNode("attr_upper_limit").InnerText.Split('&');
			tour.baseAttrUpper = uint.Parse(tokens[0]);
			tour.hedgingAttrUpper = float.Parse(tokens[1]);

			tour.challengeConsume = uint.Parse(line.SelectSingleNode("challenge_consume").InnerText);

			tokens = line.SelectSingleNode("win_awards").InnerText.Split('&');
			foreach (string token in tokens)
			{
				if (!string.IsNullOrEmpty(token))
				{
					uint id = uint.Parse(token);
					if (id != 0)
						tour.winAwards.Add(id);
				}
			}
			tokens = line.SelectSingleNode("fail_awards").InnerText.Split('&');
			foreach (string token in tokens)
			{
				if (!string.IsNullOrEmpty(token))
				{
					uint id = uint.Parse(token);
					if (id != 0)
						tour.failAwards.Add(id);
				}
			}

			tour.gameModeID = uint.Parse(line.SelectSingleNode("game_mode_id").InnerText);

            for (int i = 1; i <= 3; i++ )
            {
                string node = "quality_star" + i;
                tokens = line.SelectSingleNode(node).InnerText.Split('&');
                string t = tokens[0];
                if( !string.IsNullOrEmpty(t))
                {
                    tour.quality.Add(uint.Parse(t));
                }

                t = tokens[1];
                if( !string.IsNullOrEmpty(t))
                {   
                    tour.star.Add(uint.Parse(t));
                }
            }
            levelTours.Add(tour.ID, tour);
		}
	}

    void ReadTourResetTimeConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        resetTimes.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TOUR_RESET_LIMIT, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root)
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			resetTimes[uint.Parse(line.SelectSingleNode("VIP_level").InnerText)] = uint.Parse(line.SelectSingleNode("max_reset_time").InnerText);
		}
	}

    void ReadTourResetCostConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name3);
            return;
        }
        
        resetCost.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TOUR_RESET_COST, text);
        //解析xml的过程
        XmlNode root = xmlDoc.SelectSingleNode("Data");
		foreach (XmlNode line in root)
		{
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			uint times = uint.Parse(line.SelectSingleNode("reset_time").InnerText);
			uint ID = uint.Parse(line.SelectSingleNode("id").InnerText);
			uint value = uint.Parse(line.SelectSingleNode("value").InnerText);
			resetCost[times] = new KeyValuePair<uint, uint>(ID, value);
		}
	}
}
