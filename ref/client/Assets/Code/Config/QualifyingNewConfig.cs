using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using QualifyingNewGrade = fogs.proto.config.QualifyingNewConfig;
using QualifyingNewSeason = fogs.proto.config.QualifyingNewSeasonConfig;

public class QualifyingNewConfig
{
    string name1 = GlobalConst.DIR_XML_QUALIFYING_NEW;
    string name2 = GlobalConst.DIR_XML_QUALIFYING_NEW_SEASON;
    bool isLoadFinish = false;
    uint count = 0;

    public List<QualifyingNewGrade> grades = new List<QualifyingNewGrade>();
	public Dictionary<uint, QualifyingNewSeason> seasons = new Dictionary<uint, QualifyingNewSeason>();
	public Dictionary<uint, uint> maxStarNum = new Dictionary<uint, uint>();

    public QualifyingNewConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 2)
        {
            isLoadFinish = true;
            GameSystem.Instance.loadConfigCnt += 1;
        }
    }

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        GameSystem.Instance.readConfigCnt += 1;

		Logger.ConfigBegin(name1);
		ReadQualifyingNewData();
		Logger.ConfigBegin(name2);
		Logger.ConfigEnd(name1);
		ReadSeasonData();
		Logger.ConfigEnd(name2);
    }

	public QualifyingNewGrade GetGrade(uint score)
	{
		QualifyingNewGrade grade = null;
		foreach (QualifyingNewGrade g in grades)
		{
			if (score >= g.score)
				grade = g;
			else
				break;
		}
		return grade;
	}

	public QualifyingNewGrade GetPrevGrade(uint score)
	{
		QualifyingNewGrade grade = null;
		QualifyingNewGrade prevGrade = null;
		foreach (QualifyingNewGrade g in grades)
		{
			if (g.score > score)
				break;
			prevGrade = grade;
			grade = g;
		}
		return prevGrade;
	}

	public QualifyingNewGrade GetNextGrade(uint score)
	{
		QualifyingNewGrade grade = null;
		foreach (QualifyingNewGrade g in grades)
		{
			grade = g;
			if (g.score > score)
				break;
		}
		return grade;
	}

	public uint GetMaxStarNum(uint section)
	{
		uint num = 0;
		maxStarNum.TryGetValue(section, out num);
		return num;
	}

	public QualifyingNewSeason GetSeason(uint ID)
	{
		QualifyingNewSeason season = null;
		seasons.TryGetValue(ID, out season);
		return season;
	}

    void ReadQualifyingNewData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name1, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;

            QualifyingNewGrade data = new QualifyingNewGrade();
			uint value;
			uint.TryParse(xe.SelectSingleNode("section").InnerText,out value);
			data.section = value;
			uint value_score;
			uint.TryParse(xe.SelectSingleNode("score").InnerText,out value_score);
			data.score = value_score;
//			data.section = uint.Parse(xe.SelectSingleNode("section").InnerText);
//			data.score = uint.Parse(xe.SelectSingleNode("score").InnerText);
			data.title = xe.SelectSingleNode("title").InnerText;
			uint star = 0;
			uint.TryParse(xe.SelectSingleNode("star").InnerText, out star);
			data.star = star;
			data.icon = xe.SelectSingleNode("icon").InnerText;
            data.icon_small = xe.SelectSingleNode("icon_small").InnerText;
			uint upgrade_score;
			uint.TryParse(xe.SelectSingleNode("upgrade_score").InnerText, out upgrade_score);
			data.upgrade_score = upgrade_score;
			uint value_id;
			uint.TryParse(xe.SelectSingleNode("award_id").InnerText,out value_id);
			data.award_id = value_id;
//			data.award_id = uint.Parse(xe.SelectSingleNode("award_id").InnerText);
			data.award_icon = xe.SelectSingleNode("award_icon").InnerText;
			uint value_ai ;
			uint.TryParse(xe.SelectSingleNode("opponentAI").InnerText,out value_ai);
			data.opponentAI = value_ai;
			grades.Add(data);

			maxStarNum[data.section] = data.star;
        }
    }
	public QualifyingNewGrade getGradeConfigByGrade(int idx)
	{
		foreach(QualifyingNewGrade qfnc in grades)
		{
			if(idx == qfnc.section)
			{
				return qfnc;
			}
		}
		Debug.LogError("no grade data found with section "+idx);
		return null;
	}
	void ReadSeasonData()
	{
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name2, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;

            QualifyingNewSeason data = new QualifyingNewSeason();
			data.season = uint.Parse(xe.SelectSingleNode("season").InnerText);
			data.start_time = xe.SelectSingleNode("start_time").InnerText;
			data.end_time = xe.SelectSingleNode("end_time").InnerText;
			seasons.Add(data.season, data);
        }
	}
}

