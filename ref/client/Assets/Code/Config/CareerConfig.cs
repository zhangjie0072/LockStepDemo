using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Collections;
using fogs.proto.config;

public class CareerConfig
{
    string name1 = GlobalConst.DIR_XML_CHAPTER;
    string name2 = GlobalConst.DIR_XML_SECTION;
    string name3 = GlobalConst.DIR_XML_CareerAWARDLIB;
    string name4 = GlobalConst.DIR_XML_AWARDPACK;
    string name5 = GlobalConst.DIR_XML_PLOT;
    string name6 = GlobalConst.DIR_XML_STARCONDITION;
    string name7 = GlobalConst.DIR_XML_SECTION_RESET_TIMES;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    //已废弃，//配置文件迁移到 AwardPackDataConfig.cs
    public static Dictionary<uint, AwardPackConfig> awardPackConfig = new Dictionary<uint, AwardPackConfig>();

    public static Dictionary<uint, ChapterConfig> chapterConfig = new Dictionary<uint, ChapterConfig>();
    public static Dictionary<uint, SectionConfig> sectionConfig = new Dictionary<uint, SectionConfig>();
    public static Dictionary<uint, CareerAwardLibConfig> careerAwardLibConfig = new Dictionary<uint, CareerAwardLibConfig>();
    public static Dictionary<uint, List<PlotConfig>> plotConfig = new Dictionary<uint, List<PlotConfig>>();
    public static Dictionary<uint, string> dialogConfig = new Dictionary<uint, string>();
    public static Dictionary<uint, string> starConditionConfig = new Dictionary<uint, string>();
    public static Dictionary<uint, List<uint>> assistConfig = new Dictionary<uint, List<uint>>();//外援球员id
    public static Dictionary<uint, BuyGameTimesConfig> sectionResetConfig = new Dictionary<uint, BuyGameTimesConfig>();//关卡重置配置
    public static Dictionary<uint, List<uint>> sectionAwardsConfig = new Dictionary<uint, List<uint>>();

    public CareerConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_CHAPTER, LoadFinish);
        //ParseChapterConfig();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_SECTION, LoadFinish);
        //ParseSectionConfig();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_CareerAWARDLIB, LoadFinish);
        //ParseCareerAwardLibConfig();
        //ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_AWARDPACK);
        //ParseAwardPackConfig();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_PLOT, LoadFinish);
        //ParsePlotConfig();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_STARCONDITION, LoadFinish);
        //ParseStarConditionConfig();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_SECTION_RESET_TIMES, LoadFinish);
        //ParseSectionResetConfig();
    }
    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 6)
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
		ParseChapterConfig();
		
		Debug.Log("Config reading " + name2);
		ParseSectionConfig();
		
		Debug.Log("Config reading " + name3);
		ParseCareerAwardLibConfig();
		
		Debug.Log("Config reading " + name4);
        //ParseAwardPackConfig();
		ParsePlotConfig();
		
		Debug.Log("Config reading " + name5);
		ParseStarConditionConfig();

		Debug.Log("Config reading " + name6);
		ParseSectionResetConfig();
    }

    public void ParseChapterConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        chapterConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CHAPTER, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            ChapterConfig config = new ChapterConfig();
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "name")
                {
                    config.name = xel.InnerText;
                }
                else if (xel.Name == "next_chapter_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.next_chapter_id = value;
                }
                else if (xel.Name == "first_section_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.first_section_id = value;
                }
                else if (xel.Name == "area")
                {
                    config.area = xel.InnerText;
                }
                else if (xel.Name == "unlock_level")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.unlock_level = value;
                }
                else if (xel.Name == "bronze_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.bronze_value = value;
                }
                else if (xel.Name == "bronze_award")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.bronze_award = value;
                }
                else if (xel.Name == "silver_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.silver_value = value;
                }
                else if (xel.Name == "silver_award")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.silver_award = value;
                }
                else if (xel.Name == "gold_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.gold_value = value;
                }
                else if (xel.Name == "gold_award")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.gold_award = value;
                }
            }
            chapterConfig[config.id] = config;
        }
    }

    public void ParseSectionConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        sectionConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SECTION, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            SectionConfig config = new SectionConfig();
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "name")
                {
                    config.name = xel.InnerText;
                }
                else if (xel.Name == "type")
                {
                    config.type = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "role_gift")
                {
                    config.role_gift = uint.Parse(xel.InnerText);
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;
                }
                else if (xel.Name == "coord_x")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.coord_x = value;
                }
                else if (xel.Name == "coord_y")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.coord_y = value;
                }
                else if (xel.Name == "next_section_id")
                {
                    //uint.TryParse(xel.InnerText, out value);
                    config.next_section_id = xel.InnerText;
                }
                else if (xel.Name == "costing")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.costing = value;
                }
                else if (xel.Name == "sweep_card")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.sweep_card = value;
                }
                else if (xel.Name == "daily_times")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.daily_times = value;
                }
                else if (xel.Name == "buy_consume")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.buy_consume = value;
                }
                else if (xel.Name == "member_need")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.member_need = value;
                }
                else if (xel.Name == "space_need")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.space_need = value;
                }
                else if (xel.Name == "condition_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.condition_id = value;
                }
                else if (xel.Name == "condition_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.condition_value = value;
                }
                else if (xel.Name == "award_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.award_id = value;
                }
                else if (xel.Name == "one_star_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.one_star_id = value;
                }
                else if (xel.Name == "one_star_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.one_star_value = value;
                }
                else if (xel.Name == "two_star_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.two_star_id = value;
                }
                else if (xel.Name == "two_star_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.two_star_value = value;
                }
                else if (xel.Name == "three_star_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.three_star_id = value;
                }
                else if (xel.Name == "three_star_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.three_star_value = value;
                }
                else if (xel.Name == "plot_begin_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.plot_begin_id = value;
                }
                else if (xel.Name == "plot_end_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.plot_end_id = value;
                }
                else if (xel.Name == "plot_intro")
                {
                    config.plot_intro = xel.InnerText;
                }
                else if (xel.Name == "scene")
                {
                    config.scene = xel.InnerText;
                }
                else if (xel.Name == "music")
                {
                    config.music = xel.InnerText;
                }
                else if (xel.Name == "time")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.time = value;
                }
                else if (xel.Name == "team_side")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.team_side = value;
                }
                else if (xel.Name == "home_score")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.home_score = value;
                }
                else if (xel.Name == "guest_score")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.guest_score = value;
                }
                else if (xel.Name.Contains("npc_id"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    if (value != 0)
                        config.npc_id.Add(value);
                }
                else if (xel.Name.Contains("assistant_id"))
                {
                    config.assistant_id = xel.InnerText;
                    string[] ids = config.assistant_id.Split('&');
                    foreach (string id in ids)
                    {
                        uint roleID = uint.Parse(id);
                        if (assistConfig.ContainsKey(config.id) == false)
                        {
                            assistConfig[config.id] = new List<uint>();
                        }
                        assistConfig[config.id].Add(roleID);
                    }
                }
                else if (xel.Name.Contains("assistant_level"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    if (value != 0)
                    {
                        config.assistant_level = value;
                    }
                }
                else if (xel.Name.Contains("awards_id"))
                {
                    if (!sectionAwardsConfig.ContainsKey(config.id))
                    {
                        List<uint> awards = new List<uint>();
                        sectionAwardsConfig.Add(config.id, awards);
                    }
                    if (xel.InnerText.Contains("&"))
                    {
                        foreach (string str in xel.InnerText.Split('&'))
                        {
                            //config.awards_id.Add(uint.Parse(str));
                            sectionAwardsConfig[config.id].Add(uint.Parse(str));
                        }
                    }
                    else
                    {
                        //config.awards_id.Add(uint.Parse(xel.InnerText));
                        sectionAwardsConfig[config.id].Add(uint.Parse(xel.InnerText));
                    }
                }
                else if (xel.Name == "game_mode_id")
                {
                    if (uint.TryParse(xel.InnerText, out value))
                    {
                        config.game_mode_id = value;
                    }
                }
                else if (xel.Name == "loading")
                {
                    config.loading = xel.InnerText;
                }
                else if (xel.Name == "icon_level")
                {
                    config.icon_level = xel.InnerText;
                }
                else if (xel.Name == "frame")
                {
                    config.frame = xel.InnerText;
                }
            }
            sectionConfig[config.id] = config;
        }
    }

    public void ParseCareerAwardLibConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name3);
            return;
        }
        careerAwardLibConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CareerAWARDLIB, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            CareerAwardLibConfig config = new CareerAwardLibConfig();
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "loop_times")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.loop_times = value;
                }
                else if (xel.Name == "multiple_type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.multiple_type = (AwardMultipleType)value;
                }
                else if (xel.Name == "multiple_value")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.multiple_value = value;
                }
                else if (xel.Name.Contains("award_pack"))
                {
                    uint.TryParse(xel.InnerText, out value);
                    if (value != 0)
                        config.award_pack.Add(value);
                }
            }
            careerAwardLibConfig[config.id] = config;
        }
    }

    public void ParseAwardPackConfig()
    {
        //配置文件迁移到 AwardPackDataConfig.cs
        //string text = ResourceLoadManager.Instance.GetConfigText(name4);
        //if (text == null)
        //{
        //    Debug.LogError("LoadConfig failed: " + name4);
        //    return;
        //}
        //awardPackConfig.Clear();

        ////读取以及处理XML文本的类
        //XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_AWARDPACK, text);
        ////解析xml的过程
        //XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
        //foreach (XmlElement land in nodeList)
        //{
        //    XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
        //    if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
        //        continue;
        //    AwardPackConfig config = new AwardPackConfig();
        //    AwardConfig award = new AwardConfig();
        //    foreach (XmlElement xel in land)
        //    {
        //        if (xel.InnerText == "")
        //            continue;

        //        uint value;
        //        if (xel.Name == "id")
        //        {
        //            uint.TryParse(xel.InnerText, out value);
        //            config.id = value;
        //        }
        //        //else if (xel.Name.Contains("award_type"))
        //        //{
        //        //    uint.TryParse(xel.InnerText, out value);
        //        //    award.award_type = value;
        //        //}
        //        else if (xel.Name.Contains("award_id"))
        //        {
        //            award = new AwardConfig();
        //            uint.TryParse(xel.InnerText, out value);
        //            award.award_id = value;
        //        }
        //        else if (xel.Name.Contains("award_value"))
        //        {
        //            if (xel.InnerText.Contains("-"))
        //            {
        //                string[] tokens = xel.InnerText.Split('-');
        //                award.award_value = uint.Parse(tokens[0]);
        //                award.award_max_value = uint.Parse(tokens[1]);
        //            }
        //            else
        //            {
        //                uint.TryParse(xel.InnerText, out value);
        //                award.award_value = value;
        //                award.award_max_value = value;
        //            }

        //            config.awards.Add(award);
        //        }
        //    }
        //    awardPackConfig[config.id] = config;
        //}
    }

    public void ParsePlotConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name5);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name5);
            return;
        }
        plotConfig.Clear();
        dialogConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_PLOT, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            PlotConfig config = new PlotConfig();
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name == "dialog_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.dialog_id = value;
                }
                else if (xel.Name == "icon")
                {
                    config.icon = xel.InnerText;
                }
                else if (xel.Name == "content")
                {
                    config.content = xel.InnerText;
                }
                else if (xel.Name == "next_dialog_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.next_dialog_id = value;
                }
            }
            if (plotConfig.ContainsKey(config.id) == false)
            {
                plotConfig[config.id] = new List<PlotConfig>();
            }
            plotConfig[config.id].Add(config);
            dialogConfig[config.dialog_id] = config.content;
        }
    }

    public void ParseStarConditionConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name6);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name6);
            return;
        }
        starConditionConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_STARCONDITION, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            uint id = 0;
            string conditionStr = "";
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "condition_id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    id = value;
                }
                else if (xel.Name == "condition")
                {
                    conditionStr = xel.InnerText;
                }
            }
            starConditionConfig[id] = conditionStr;
        }
    }
    //读取关卡重置配置
    public void ParseSectionResetConfig()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name7);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name7);
            return;
        }
        sectionResetConfig.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SECTION_RESET_TIMES, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement land in nodeList)
        {
            uint times = 0;
            BuyGameTimesConfig data = new BuyGameTimesConfig();
            foreach (XmlElement xel in land)
            {
                uint value;
                if (xel.Name == "reset_times")
                {
                    uint.TryParse(xel.InnerText, out value);
                    times = value;
                }
                else if (xel.Name == "consume_type")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.consume_type = value;
                }
                else if (xel.Name == "consume")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.consume = value;
                }
            }
            if (!sectionResetConfig.ContainsKey(times))
            {
                sectionResetConfig.Add(times, data);
            }
        }
    }
    /// <summary>
    /// 根据重置次数获取重置消耗配置
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    /// 无用接口
    //public static BuyGameTimesConfig GetSectionResetConfigData(uint times)
    //{
    //    if (sectionResetConfig.ContainsKey(times))
    //    {
    //        return sectionResetConfig[times];
    //    }
    //    return null;
    //}
    /// <summary>
    /// 根据关卡ID获取关卡图标
    /// </summary>
    /// <param name="sectionID"></param>
    /// <returns></returns>
    /// 无用接口
    //public static string GetSectionIcon(uint sectionID)
    //{
    //    if (sectionConfig.ContainsKey(sectionID))
    //    {
    //        return sectionConfig[sectionID].icon;
    //    }
    //    return null;
    //}
    public ChapterConfig GetChapterData(uint chapterID)
    {
        if (chapterConfig.ContainsKey(chapterID))
            return chapterConfig[chapterID];
        return null;
    }

    public SectionConfig GetSectionData(uint sectionID)
    {
        if (sectionConfig.ContainsKey(sectionID))
            return sectionConfig[sectionID];
        return null;
    }

	public static AwardPackConfig GetAwardPackConfig(uint packID)
	{
        if (AwardPackDataConfig.awardPackConfig.ContainsKey(packID)) 
		{
            return AwardPackDataConfig.awardPackConfig[packID];
		}
		return null;
	}

    public static List<AwardConfig> GetGoodsList(uint libID, int times)
    {
        if (careerAwardLibConfig.ContainsKey(libID))
        {
            CareerAwardLibConfig libConf = careerAwardLibConfig[libID];
            times = (times - 1) % libConf.award_pack.Count;
            if (times < libConf.award_pack.Count)
            {
                uint packID = libConf.award_pack[times];
				return GetAwardPackConfig(packID).awards;
            }
        }
        return null;
    }

    public static List<AwardConfig> GetSectionAllGoodsList(uint libID)
    {
        List<AwardConfig> retList = new List<AwardConfig>();
        if (careerAwardLibConfig.ContainsKey(libID))
        {
            int listIdx = 0;
            for (int packCnt = 0; packCnt < careerAwardLibConfig[libID].award_pack.Count; ++packCnt)
            {
                uint packID = careerAwardLibConfig[libID].award_pack[packCnt];
                if (AwardPackDataConfig.awardPackConfig.ContainsKey(packID))
                {
                    for (int awardCnt = 0; awardCnt < AwardPackDataConfig.awardPackConfig[packID].awards.Count; ++awardCnt)
                    {
                        retList.Insert(listIdx++, AwardPackDataConfig.awardPackConfig[packID].awards[awardCnt]);
                    }
                }
            }
            return retList;
        }
        return null;
    }
    //无用接口
    // public static List<uint> GetSectionGoodsList(uint sectionID)
    // {
    //     if (sectionAwardsConfig.ContainsKey(sectionID))
    //     {
    //         return sectionAwardsConfig[sectionID];
    //     }
    //     return null;
    // }

    public static string GetStarConditionString(uint id, uint value)
    {
        if (starConditionConfig.ContainsKey(id))
            return starConditionConfig[id].Replace("%s%", value.ToString());
        return null;
    }
    /// <summary>
    /// 根据章节ID获取外援球员list列表
    /// </summary>
    /// <param name="sectionID"></param>
    /// <returns></returns>
    /// 无用接口
    //public static List<uint> GetAssistsBySectionID(uint sectionID)
    //{
    //    if (assistConfig.ContainsKey(sectionID))
    //    {
    //        Debug.Log("外援球员ID " + assistConfig[sectionID]);
    //        return assistConfig[sectionID];
    //    }
    //    return null;
    //}
}
