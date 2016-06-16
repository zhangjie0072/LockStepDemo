using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class WinningStreakAwardConfig
{
    string name = GlobalConst.DIR_XML_WINNINGSTREAKAWARD;
    bool isLoadFinish = false;
    private object LockObject = new object();

    private Dictionary<uint, uint> award_config = new Dictionary<uint, uint>();

    public WinningStreakAwardConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
        //ReadWinningStreakAwardConfig();
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
		Logger.ConfigBegin(name);
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        award_config.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_WINNINGSTREAKAWARD, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            uint winning_streak = 0;
            uint award_pack_id = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out winning_streak);
                }
                else if (xel.Name == "award_pack_id")
                {
                    uint.TryParse(xel.InnerText, out award_pack_id);
                }
            }
            award_config.Add(winning_streak, award_pack_id);
        }
		Logger.ConfigEnd(name);
    }

    public uint GetAwardPackID(uint winning_streak)
    {
        uint id = 0;
        award_config.TryGetValue(winning_streak, out id);
        return id;
    }
}
