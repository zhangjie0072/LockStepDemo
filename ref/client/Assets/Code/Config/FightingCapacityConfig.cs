using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class FightingCapacityConfig
{
    string name1 = GlobalConst.DIR_XML_EXPECTED_SCORE_DIFF;
    string name2 = GlobalConst.DIR_XML_ATTR_ENHANCE;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

	Dictionary<IM.Number, int> scoreDiff = new Dictionary<IM.Number, int>();	// Fighting capacity -> Score diff
	Dictionary<int, IM.Number> enhanceFactor = new Dictionary<int, IM.Number>();	// Level -> Enhance factor

	public FightingCapacityConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_EXPECTED_SCORE_DIFF, LoadFinish);
		//ReadScoreDiff();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ATTR_ENHANCE, LoadFinish);
		//ReadEnhance();
	}
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
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
		ReadScoreDiff();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadEnhance();
		Logger.ConfigEnd(name2);
    }

    void ReadScoreDiff()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_EXPECTED_SCORE_DIFF, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;
			IM.Number fc_ratio = IM.Number.Parse(line.SelectSingleNode("fc_ratio").InnerText);
			int score_diff = int.Parse(line.SelectSingleNode("score_diff").InnerText);
			scoreDiff.Add(fc_ratio, score_diff);
		}
	}

    void ReadEnhance()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        
		XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ATTR_ENHANCE, text);
		XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;
			int level = int.Parse(line.SelectSingleNode("level").InnerText);
			IM.Number factor = IM.Number.Parse(line.SelectSingleNode("factor").InnerText);
			enhanceFactor.Add(level, factor);
		}
	}

	public int GetExpectedScoreDiff(IM.Number fcRatio)
	{
		if (fcRatio < 1)
			Logger.LogError("FC ratio can not be smaller than 1.");
		int diff = 0;
		foreach (KeyValuePair<IM.Number, int> pair in scoreDiff)
		{
			if (fcRatio >= pair.Key)
				diff = pair.Value;
			else
				break;
		}
		return diff;
	}
	
	public IM.Number GetEnhanceFactor(int level)
	{
		level = Mathf.Min(level, 6);
		level = Mathf.Max(level, -6);
		IM.Number factor = IM.Number.one;
		if (!enhanceFactor.TryGetValue(level, out factor))
			Logger.LogError("Enhance factor of level " + level + " does not exist.");
		return factor;
	}
}
