using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;



public class ShootMatch
{
	public uint id;
	public uint hard;
	public uint unlock_level;
	public uint times;
	public uint win_hp_cost;
	public uint lose_hp_cost;
	public uint reward_id;
	//public List<uint> npc = new List<uint>();
    public uint gameModeId;
};


public class ShootMatchConfig
{

    public static List<ShootMatch> shootMatch = new List<ShootMatch>();

    public ShootMatchConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_SHOOT_MATCH, Read);
        //Read();
    }

	public ShootMatch GetShootMatch( uint id, uint hard )
	{
		foreach( var v in shootMatch )
		{
			if( v.id == id && v.hard == hard )
			{
				return v;
			}
		}
		return null;
	}

	public List<ShootMatch>GetShootMatchByID( uint id)
	{
		List<ShootMatch> ret = new List<ShootMatch>();
		
		foreach( var v in shootMatch )
		{
			if( v.id == id )
			{
				ret.Add(v);
			}
		}
		return ret;
	}

    public void Read(string vPath, object obj)
    {
        string text = ResourceLoadManager.Instance.GetConfigText(GlobalConst.DIR_XML_SHOOT_MATCH);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + GlobalConst.DIR_XML_SHOOT_MATCH);
            return;
        }

		Logger.ConfigEnd(GlobalConst.DIR_XML_SHOOT_MATCH);
        shootMatch.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SHOOT_MATCH, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            ShootMatch level = new ShootMatch();
            foreach (XmlElement xel in xe)
            {
				if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out level.id);
                }
                else if (xel.Name == "hard")
                {
                    uint.TryParse(xel.InnerText, out level.hard);
                }
                else if (xel.Name == "times")
                {
                    uint.TryParse(xel.InnerText, out level.times);
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
                else if (xel.Name == "game_mode")
                {
                    uint.TryParse(xel.InnerText, out level.gameModeId);
                }
                //else if (xel.Name == "npc")
                //{
                //    string[] array = xel.InnerText.Split('&');
                //    foreach (string items in array)
                //    {
                //        uint npc;
                //        uint.TryParse(items, out npc);
                //        level.npc.Add(npc);
                //    }                  
                //}
            }
            shootMatch.Add(level);
		}
		Logger.ConfigBegin(GlobalConst.DIR_XML_SHOOT_MATCH);
    }




}
