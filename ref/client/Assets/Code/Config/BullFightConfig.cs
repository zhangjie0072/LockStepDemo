using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class BullFightLevel
{
	public uint hard;
	public uint unlock_level;
	public uint win_hp_cost;
	public uint lose_hp_cost;
	public uint reward_id;
	public List<uint> npc = new List<uint>();
};

public class BullFightConsume
{
    public uint buy_times;
    public uint consume_type;
    public uint consume_value;
    
}
public class BullFightConfig
{
    string name1 = GlobalConst.DIR_XML_BULL_FIGHT;
    string name2 = GlobalConst.DIR_XML_BULLFIGHT_CONSUME;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public static Dictionary<uint, BullFightLevel> levels = new Dictionary<uint, BullFightLevel>();
    public static List<BullFightConsume> bullFightConsumes = new List<BullFightConsume>();

    public BullFightConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //Read();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
        //ReadConsume();
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
		Read();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadConsume();
		Logger.ConfigEnd(name2);
    }

	public BullFightLevel GetFightLevel( uint hard )
	{
		BullFightLevel level;
		levels.TryGetValue(hard, out level);
		return level;
	}

    public void Read()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        levels.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_BULL_FIGHT, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            BullFightLevel level = new BullFightLevel();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "hard")
                {
                    uint.TryParse(xel.InnerText, out level.hard);
                }
                else if (xel.Name == "unlock_level")
                {
                    uint.TryParse(xel.InnerText, out level.unlock_level);
                }
                else if (xel.Name == "win_hp_cost")
                {
                    uint.TryParse(xel.InnerText, out level.win_hp_cost);
                }
                else if (xel.Name == "lose_hp_cost")
                {
                    uint.TryParse(xel.InnerText, out level.lose_hp_cost);
                }
                else if (xel.Name == "reward_id")
                {
                    uint.TryParse(xel.InnerText, out level.reward_id);
                }
                else if (xel.Name == "npc")
                {
                    string[] array = xel.InnerText.Split('&');
                    foreach (string items in array)
                    {
                        uint npc;
                        uint.TryParse(items, out npc);
                        level.npc.Add(npc);
                    }                  
                }
            }

            levels.Add(level.hard,level);
        }
    }

    public void ReadConsume()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        bullFightConsumes.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_BULLFIGHT_CONSUME, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            BullFightConsume consume = new BullFightConsume();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "buy_times")
                {
                    uint.TryParse(xel.InnerText, out consume.buy_times);
                }
                else if (xel.Name == "consume_type")
                {
                    uint.TryParse(xel.InnerText, out consume.consume_type);
                }
                else if(xel.Name == "consume_value")
                {
                    uint.TryParse(xel.InnerText, out consume.consume_value);
                }
            }

            bullFightConsumes.Add(consume);
        } 
    }

    public BullFightConsume GetBullFightConsume(uint times)
    {
        return bullFightConsumes.Find(x => x.buy_times == times);     
    }


}
