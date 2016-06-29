using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using QualifyingNewerGrade = fogs.proto.config.QualifyingNewerConfig;
using QualifyingNewerSeason = fogs.proto.config.QualifyingNewerSeasonConfig;

public class QualifyingNewerConfig
{
    string name1 = GlobalConst.DIR_XML_QUALIFYING_NEWER;
    string name2 = GlobalConst.DIR_XML_QUALIFYING_NEWER_SEASON;
    string name3 = GlobalConst.DIR_XML_QUALIFYING_NEWER_LEAGUEAWARDS;

    bool isLoadFinish = false;
    uint count = 0;

    public List<QualifyingNewerGrade> grades = new List<QualifyingNewerGrade>();
	public Dictionary<uint, QualifyingNewerSeason> seasons = new Dictionary<uint, QualifyingNewerSeason>();
	public Dictionary<uint, uint> maxStarNum = new Dictionary<uint, uint>();
    uint _maxScore = 0;
    public uint MaxScore
    {
        get
        {
            return _maxScore;
        }
    }

    Dictionary<uint, QualifyingNewerGrade> firstGrades = new Dictionary<uint, QualifyingNewerGrade>();
    Dictionary<uint, QualifyingNewerGrade> firstSubGrades = new Dictionary<uint, QualifyingNewerGrade>();

    public QualifyingNewerConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        //ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
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

		Debug.Log("Config reading " + name1);
		ReadQualifyingNewData();
		
		Debug.Log("Config reading " + name2);
		ReadSeasonData();
		
    }

	public QualifyingNewerGrade GetGrade(uint score)
	{
		QualifyingNewerGrade grade = null;
		foreach (QualifyingNewerGrade g in grades)
		{
			if (score >= g.score)
				grade = g;
			else
				break;
		}
		return grade;
	}

	public QualifyingNewerGrade GetPrevGrade(uint score)
	{
		QualifyingNewerGrade grade = null;
		QualifyingNewerGrade prevGrade = null;
		foreach (QualifyingNewerGrade g in grades)
		{
			if (g.score > score)
				break;
			prevGrade = grade;
			grade = g;
		}
		return prevGrade;
	}

	public QualifyingNewerGrade GetNextGrade(uint score)
	{
		QualifyingNewerGrade grade = null;
		foreach (QualifyingNewerGrade g in grades)
		{
			grade = g;
			if (g.score > score)
				break;
		}
		return grade;
	}

    public QualifyingNewerGrade GetNextSubSection( uint score)
    {
        QualifyingNewerGrade curGrade = GetGrade(score);

        QualifyingNewerGrade nextGrade = GetNextGrade(curGrade.score);

        while(curGrade.sub_section == nextGrade.sub_section && nextGrade != null )
        {
            nextGrade = GetNextGrade(nextGrade.score);

        }
        return nextGrade;
    }


	public uint GetMaxStarNum(uint section)
	{
		uint num = 0;
		maxStarNum.TryGetValue(section, out num);
		return num;
	}

	public QualifyingNewerSeason GetSeason(uint ID)
	{
		QualifyingNewerSeason season = null;
		seasons.TryGetValue(ID, out season);
		return season;
	}

    void ReadQualifyingNewData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name1, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;

            QualifyingNewerGrade data = new QualifyingNewerGrade();
			data.section = uint.Parse(xe.SelectSingleNode("section").InnerText);
			data.score = uint.Parse(xe.SelectSingleNode("score").InnerText);
			data.title = xe.SelectSingleNode("title").InnerText;
			uint star = 0;
			uint.TryParse(xe.SelectSingleNode("star").InnerText, out star);
			data.star = star;
			data.icon = xe.SelectSingleNode("icon").InnerText;
            data.icon_small = xe.SelectSingleNode("icon_small").InnerText;
			uint upgrade_score;
			uint.TryParse(xe.SelectSingleNode("upgrade_score").InnerText, out upgrade_score);
			data.upgrade_score = upgrade_score;
			data.award_id = uint.Parse(xe.SelectSingleNode("award_id").InnerText);
			data.award_icon = xe.SelectSingleNode("award_icon").InnerText;
            data.combo = uint.Parse(xe.SelectSingleNode("combo").InnerText);
            data.awardpack_id = uint.Parse(xe.SelectSingleNode("awardpack_id").InnerText);
			data.sub_section = uint.Parse(xe.SelectSingleNode("sub_section").InnerText);
			data.team_ai = uint.Parse(xe.SelectSingleNode("team_ai").InnerText);
			data.enemy_ai = uint.Parse(xe.SelectSingleNode("enemy_ai").InnerText);

			grades.Add(data);

            if( !firstGrades.ContainsKey(data.section))
            {
                firstGrades.Add(data.section, data);
            }
            if( !firstSubGrades.ContainsKey(data.sub_section))
            {
                firstSubGrades.Add(data.sub_section, data);
            }

			maxStarNum[data.section] = data.star;
            if( _maxScore < data.score )
            {
                _maxScore = data.score;
            }
        }
    }

	void ReadSeasonData()
	{
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name2, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;

            QualifyingNewerSeason data = new QualifyingNewerSeason();
			data.season = uint.Parse(xe.SelectSingleNode("season").InnerText);
			data.start_time = xe.SelectSingleNode("start_time").InnerText;
			data.end_time = xe.SelectSingleNode("end_time").InnerText;
			seasons.Add(data.season, data);
        }
	}
    public QualifyingNewerGrade GetFirstGrade(uint section)
    {
        return firstGrades[section];
    }


    public QualifyingNewerGrade GetFirstSubGrade(uint subSection)
    {
        return firstSubGrades[subSection];
    }
}

