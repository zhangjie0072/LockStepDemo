using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;


public class ShootGame
{
	public uint id;
    public uint times;
	public uint level_low;
	public uint level_high;
    public uint npc_level;
	public uint basic;
    public float hedging;
	public uint game_mode_id;
    public string score_level;
    public string rewards_num;
	public uint reward_id;
};

public class ShootGameConsume
{
    public uint buy_times;
    public uint consume_type;
    public uint consume_value;

}

public class ShootGameConfig
{
    string name1 = GlobalConst.DIR_XML_SHOOT_GAME;
    string name2 = GlobalConst.DIR_XML_SHOOT_GAME_CONSUME;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public static List<ShootGame> shootGame = new List<ShootGame>();
    public static List<ShootGameConsume> shootGameConsumes = new List<ShootGameConsume>();

    public ShootGameConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_SHOOT_GAME, LoadFinish);
        //Read();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_SHOOT_GAME_CONSUME, LoadFinish);
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

		Debug.Log("Config reading " + name1);
		Read();
		
		Debug.Log("Config reading " + name2);
		ReadConsume();
		
    }


    public void Read()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        shootGame.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SHOOT_GAME, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            ShootGame shoot_game = new ShootGame();
            foreach (XmlElement xel in xe)
            {
				if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out shoot_game.id);
                }
                else if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out shoot_game.times);
                }
                else if (xel.Name == "level_region")
                {
                    string[] temp = xel.InnerText.Split(',');
                    shoot_game.level_low = uint.Parse(temp[0]);
                    shoot_game.level_high = uint.Parse(temp[1]);
                }
                else if (xel.Name == "npc_level")
                {
                    uint.TryParse(xel.InnerText, out shoot_game.npc_level);
                }
                else if (xel.Name == "modified_factor")
                {
                    string[] temp = xel.InnerText.Split('&');
                    shoot_game.basic = uint.Parse(temp[0]);
                    shoot_game.hedging = float.Parse(temp[1]);
                }
                else if (xel.Name == "game_mode")
                {
                    uint.TryParse(xel.InnerText, out shoot_game.game_mode_id);
                }
                else if (xel.Name == "score_level")
                {
                    shoot_game.score_level = xel.InnerText;

                }
                else if (xel.Name == "rewards_num")
                {
                    shoot_game.rewards_num = xel.InnerText;
                }
                else if (xel.Name == "rewards_id")
                {
                    uint.TryParse(xel.InnerText, out shoot_game.reward_id);
                }
            }
            shootGame.Add(shoot_game);
        }
    }

    public void ReadConsume()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        shootGameConsumes.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SHOOT_GAME_CONSUME, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            ShootGameConsume consume = new ShootGameConsume();
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
                else if (xel.Name == "consume_value")
                {
                    uint.TryParse(xel.InnerText, out consume.consume_value);
                }
            }
            shootGameConsumes.Add(consume);
        }
    }

    public AttrData GetShootgNPCAttrData(uint id, uint type_id, uint level)
    {
        uint position = (uint)GameSystem.Instance.NPCConfigData.GetConfigData(id).position;
        AttrData attr = new AttrData();
        ShootGame shootInfo = GetShootInfo(type_id, level);
        uint basic = shootInfo.basic;
        float hedging = shootInfo.hedging;
        AttrData npcattr = GameSystem.Instance.NPCConfigData.TourNpcs.Find(x => x.position == position && x.level == shootInfo.npc_level).attrs;
        attr.attrs["shoot_near"] = npcattr.attrs["shoot_near"] + basic;
        attr.attrs["shoot_middle"] = npcattr.attrs["shoot_middle"] + basic;
        attr.attrs["shoot_far"] = npcattr.attrs["shoot_far"] + basic;
        attr.attrs["dunk_near"] = npcattr.attrs["dunk_near"] + basic;
        attr.attrs["dunk_middle"] = npcattr.attrs["dunk_middle"] + basic;
        attr.attrs["layup_near"] = npcattr.attrs["layup_near"] + basic;
        attr.attrs["layup_middle"] = npcattr.attrs["layup_middle"] + basic;
        attr.attrs["anti_disturb"] = (uint)(npcattr.attrs["anti_disturb"] * hedging);
        attr.attrs["pass"] = (uint)(npcattr.attrs["pass"] * hedging);
        attr.attrs["control"] = (uint)(npcattr.attrs["control"] * hedging); ;
        attr.attrs["block"] = (uint)(npcattr.attrs["block"] * hedging);
        attr.attrs["anti_block"] = (uint)(npcattr.attrs["anti_block"] * hedging);
        attr.attrs["steal"] = (uint)(npcattr.attrs["steal"] * hedging);
        attr.attrs["disturb"] = (uint)(npcattr.attrs["disturb"] * hedging);
        attr.attrs["rebound"] = (uint)(npcattr.attrs["rebound"] * hedging);
        attr.attrs["speed"] = npcattr.attrs["speed"] + basic;
        attr.attrs["strength"] = (uint)(npcattr.attrs["strength"] * hedging);
        attr.attrs["interception"] = (uint)(npcattr.attrs["interception"] * hedging);
        attr.attrs["ph"] = npcattr.attrs["ph"] + basic;
        attr.attrs["rebound_height"] = npcattr.attrs["rebound_height"];
        attr.attrs["cross_speed"] = npcattr.attrs["cross_speed"];
        attr.attrs["shoot_far_dist"] = npcattr.attrs["shoot_far_dist"];
        attr.attrs["bodythrowcatch_distance"] = npcattr.attrs["bodythrowcatch_distance"];
        attr.attrs["bodythrowcatch_prob"] = npcattr.attrs["bodythrowcatch_prob"];

        Debug.Log("shoot_near:" + attr.attrs["shoot_near"]);
        Debug.Log("shoot_middle:" + attr.attrs["shoot_middle"]);
        Debug.Log("anti_disturb:" + attr.attrs["anti_disturb"]);
        Debug.Log("steal:" + attr.attrs["steal"]);
        Debug.Log("rebound:" + attr.attrs["rebound"]);
        Debug.Log("disturb:" + attr.attrs["disturb"]);
        Debug.Log("strength:" + attr.attrs["strength"]);
        return attr;
    }
    public ShootGame GetShootInfo(uint id, uint level)
    {
        return shootGame.Find(x=> x.id == id && (level <= x.level_high && level >= x.level_low));
    }

    public ShootGameConsume GetShootGameConsume(uint times)
    {
        return shootGameConsumes.Find(x => x.buy_times == times);
    }
}
