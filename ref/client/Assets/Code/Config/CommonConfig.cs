using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonConfig
{
    string name = GlobalConst.DIR_XML_COMMON;
    bool isLoadFinish = false;
    private object LockObject = new object();

    public static Dictionary<string, string> configs = new Dictionary<string,string>();

    public CommonConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
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

        configs.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name, text);
        //解析xml的过程
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Data").ChildNodes;

        foreach (XmlElement line in nodeList)
        {
            XmlNode comment = line.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            XmlElement elemDesc = line.SelectSingleNode("description") as XmlElement;
            XmlElement elemValue = line.SelectSingleNode("value") as XmlElement;

            if (configs.ContainsKey(elemDesc.InnerText))
            {
                Logger.LogWarning(elemDesc.InnerText + " is exist");
            }
            else
            {
                configs.Add(elemDesc.InnerText, elemValue.InnerText);
            }
		}
		ValueToGlobal();
		Logger.ConfigEnd(name);
    }

    private void ValueToGlobal()
    {
        GlobalConst.DIAMOND_ID = GetUInt("gDiamondGoodsID"); //钻石
        GlobalConst.GOLD_ID = GetUInt("gGoldGoodsID"); //金币
        GlobalConst.HONOR_ID = GetUInt("gHonorGoodsID"); //荣誉
        GlobalConst.HP_ID = GetUInt("gHpGoodsID"); //体力
        GlobalConst.TEAM_EXP_ID = GetUInt("gTeamExpGoodsID"); //球队经验
        GlobalConst.ROLE_EXP_ID = GetUInt("gRoleExpGoodsID"); //球员经验
        GlobalConst.PRESTIGE_ID = GetUInt("gPrestigeGoodsID"); //威望
        GlobalConst.REPUTATION_ID = GetUInt("gReputationID"); //声望


       
        for (int i = 1; i <= 5; ++i)
        {
            string[] c = GetString("gQualityColor" + i).Split(',');
            float r, g, b;
            float.TryParse(c[0], out r);
            float.TryParse(c[1], out g);
            float.TryParse(c[2], out b);
            GlobalConst.QUALITY_COLOR[i-1] = new Color(r, g, b);
        }
		GlobalConst.IS_GUIDE = GetUInt("gEnableGuide") == 1;
        GlobalConst.IS_DEBUG_START_GUIDE = GetUInt("gEnableDebugStartGuide") == 1;
        GlobalConst.IS_FASHION_OPEN = GetUInt("gFashionOpen"); // 开放服装商城
        GlobalConst.IS_ENABLE_TALKING_DATA = GetUInt("gEnableTalkingData") == 1;
        string[] x = GetString("gChallengePVPOpenTime2").Split('&');
        GlobalConst.CHALLENGE_OPEN = uint.Parse(x[0]);
        GlobalConst.CHALLENGE_CLOSE = uint.Parse(x[1]);
        GlobalConst.QUALIFYING_TIMES = GetUInt("gQualifyingTimes");
        GlobalConst.GShOOTSEQUENCE = GetString("gShootSequence");
        GlobalConst.GPRACRICECD = GetUInt("gPracticeCD");
        GlobalConst.MAX_QUALITY_NUM = GetUInt("gMaxQualityNum");
		GlobalConst.PVP_VALID_LATENCY = GetUInt("gPVPValidLatency");
        GlobalConst.ALL_HEDGING_ID = GetUInt("gAllHedgingID");
        GlobalConst.SHOOTCARD_DIAMOND = GetString("gShootCardDiamond");

		GlobalConst.MATCHED_KEY_BLOCK_RATE_ADJUST = GetNumber("gBlockRateAdjust_KeyMatched");
    }

    public string GetString(string key)
    {
        string value = "";
        configs.TryGetValue(key, out value);
        return value;
    }

    public uint GetUInt(string key)
    {
        uint value = 0;
        uint.TryParse(GetString(key), out value);
        return value;
    }

	public float GetFloat(string key)
	{
		float value = 0f;
		float.TryParse(GetString(key), out value);
		return value;
	}

    public IM.Number GetNumber(string key)
    {
        IM.Number value = IM.Number.zero;
        IM.Number.TryParse(GetString(key), out value);
        return value;
    }
}
