using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using fogs.proto.config;

public class AwardPackDataConfig
{
    string name = GlobalConst.DIR_XML_AWARDPACK;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static Dictionary<uint, AwardPackConfig> awardPackConfig = new Dictionary<uint, AwardPackConfig>();
    public AwardPackDataConfig()
    {
        Initialize();
    }
    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadAwardPackConfigData();
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
        awardPackConfig.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_AWARDPACK, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement land in nodeList)
        {
            XmlNode comment = land.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            AwardPackConfig config = new AwardPackConfig();
            AwardConfig award = new AwardConfig();
            foreach (XmlElement xel in land)
            {
                if (xel.InnerText == "")
                    continue;

                uint value;
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out value);
                    config.id = value;
                }
                else if (xel.Name.Contains("award_id"))
                {
                    award = new AwardConfig();
                    uint.TryParse(xel.InnerText, out value);
                    award.award_id = value;
                }
                else if (xel.Name.Contains("award_value"))
                {
					if (xel.InnerText.Contains("-"))
					{
						string[] tokens = xel.InnerText.Split('-');
						award.award_value = uint.Parse(tokens[0]);
						award.award_max_value = uint.Parse(tokens[1]);
					}
					else
					{
						uint.TryParse(xel.InnerText, out value);
						award.award_value = value;
						award.award_max_value = value;
					}
					award.award_prob = 10000;
                    config.awards.Add(award);
                }
				else if (xel.Name.Contains("award_prob"))
				{
					uint prob = 10000;
					uint.TryParse(xel.InnerText, out prob);
					award.award_prob = prob;
				}
            }
            awardPackConfig[config.id] = config;
		}
		Logger.ConfigEnd(name);
    }
    /// <summary>
    /// 根据ID找到奖励库
    /// </summary>
    /// <param name="award_id"></param>
    /// <returns></returns>
    public AwardPackConfig GetAwardPackByID(uint award_id)
    {
        if (awardPackConfig.ContainsKey(award_id))
        {
            return awardPackConfig[award_id];
        }
        return null;
    }
    /// <summary>
    /// 根据ID找到奖励库奖励列表
    /// </summary>
    /// <param name="award_id"></param>
    /// <returns></returns>
    public List<AwardConfig> GetAwardPackDatasByID(uint award_id)
    {
        if (awardPackConfig.ContainsKey(award_id))
        {
            return awardPackConfig[award_id].awards;
        }
        return null;
    }
}
