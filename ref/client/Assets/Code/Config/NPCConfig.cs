using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;
using fogs.proto.config;


public class NPCproperty
{
    public uint position;
    public uint level;
    public AttrData attrs;
}
public class PropertyModify
{
    public uint basic;
    public float hedging;
}

public class NPCDataConfig
{
    string name1 = GlobalConst.DIR_XML_NPC;
    string name2 = GlobalConst.DIR_XML_TOURNPC;
    string name3 = GlobalConst.DIR_XML_TOURNPCMODIFY;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public Dictionary<uint, NPCConfig> NPCDatas = new Dictionary<uint, NPCConfig>();
    public Dictionary<uint, AttrData> attrs = new Dictionary<uint, AttrData>();
    public List<NPCproperty> TourNpcs = new List<NPCproperty>();
    public Dictionary<uint ,List<PropertyModify>> NPCmodify = new Dictionary<uint,List<PropertyModify>>();
    public NPCDataConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_NPC, LoadFinish);
        //ReadNPCData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TOURNPC, LoadFinish);
        //ReadTourNPC();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_TOURNPCMODIFY, LoadFinish);
        //ReadNPCmodify();
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 3)
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
		ReadNPCData();
		Debug.Log("Config reading " + name2);
		
		ReadTourNPC();
		Debug.Log("Config reading " + name3);
		
		ReadNPCmodify();
		
    }

    public void ReadNPCData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name1);
            return;
        }
        NPCDatas.Clear();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_NPC, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            NPCConfig data = new NPCConfig();
            AttrData attrData = new AttrData();
            foreach (XmlElement xel in xe)
            {
                uint value;
                if (xel.Name == "NPC_ID")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.npc_id = value;
                }

                else if (xel.Name == "name")
                {
                    data.name = xel.InnerText;
                }

                else if (xel.Name == "icon")
                {
                    data.icon = xel.InnerText;
                }

                else if (xel.Name == "level")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.level = value;
                }

                else if (xel.Name == "quality")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.quality = value;
                }

                else if (xel.Name == "position")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.position = value;
                }

                else if (xel.Name == "talent")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.talent = value;
                }

                else if (xel.Name == "shape")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.shape = value;
                }

                else if (xel.Name == "AIID")
                {
                    uint.TryParse(xel.InnerText, out value);
                    data.aiid = value;
                }

				else if (xel.Name == "skill")
				{
					if (!string.IsNullOrEmpty(xel.InnerText))
					{
						string[] tokens = xel.InnerText.Split('&');
						foreach (string token in tokens)
						{
							uint skillID;
							if (uint.TryParse(token, out skillID))
							{
								data.skills.Add(skillID);
							}
						}
					}
				}

                else
                {
                    ReadAttrData(xel, ref attrData);
                }
            }
            if (!NPCDatas.ContainsKey(data.npc_id))
            {
                NPCDatas.Add(data.npc_id, data);
                attrs.Add(data.npc_id, attrData);
            }
        }
    }

	private void ReadAttrData(XmlElement xel, ref AttrData attrData)
	{
		uint value;
		if (xel.Name == "bias")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.bias = value;
		}
		if (xel.Name == "quality")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.quality = value;
		}
		else if (xel.Name == "level")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.level = value;
		}
		else if (xel.Name == "talent")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.talent = value;
		}
		else if (xel.Name == "shoot_near")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "shoot_middle")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "shoot_far")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "dunk_near")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "dunk_middle")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "layup_near")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "layup_middle")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "anti_disturb")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "shoot_far_dist")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "bodythrowcatch_distance")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "bodythrowcatch_prob")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "pass")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "control")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "block")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "anti_block")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "steal")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "disturb")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "rebound")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "rebound_height")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "speed")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "cross_speed")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
		else if (xel.Name == "strength")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
        }
        else if (xel.Name == "interception")
        {
            uint.TryParse(xel.InnerText, out value);
            attrData.attrs.Add(xel.Name, value);
        }
		else if (xel.Name == "ph")
		{
			uint.TryParse(xel.InnerText, out value);
			attrData.attrs.Add(xel.Name, value);
		}
	}

    public void ReadTourNPC()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name2);
            return;
        }
        TourNpcs.Clear();
          XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TOURNPC, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;
            NPCproperty tourNpc = new NPCproperty();
            //AttrData data = new AttrData();
            Dictionary<string, uint> attrs = new Dictionary<string, uint>();
            //AttrValueConfig attrData = new AttrValueConfig();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "switch") continue;
                if ( xel.Name == "position")
                {
                    tourNpc.position = uint.Parse(xel.InnerText);
                }else if ( xel.Name == "level")
                {
                    tourNpc.level = uint.Parse(xel.InnerText);                    
                }
                else if (xel.Name == "shoot_near")
                {
                   attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "shoot_middle")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "shoot_far")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "dunk_near")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "dunk_middle")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "layup_near")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "layup_middle")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "anti_disturb")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "shoot_far_dist")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "bodythrowcatch_distance")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "bodythrowcatch_prob")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "pass")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "control")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "block")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "anti_block")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "steal")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "disturb")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "rebound")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "rebound_height")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "speed")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "cross_speed")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "strength")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "interception")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
                else if (xel.Name == "ph")
                {
                    attrs.Add(xel.Name, uint.Parse(xel.InnerText));
                }
            }
            AttrData data = new AttrData();
            data.attrs = attrs;
            tourNpc.attrs = data;
            TourNpcs.Add(tourNpc);
        }
    }

    public void ReadNPCmodify()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name3);
            return;
        }
        NPCmodify.Clear();
         XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_TOURNPCMODIFY, text);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            if (CommonFunction.IsCommented(xe))
                continue;
            List<PropertyModify> data = new List<PropertyModify>();
            //AttrValueConfig attrData = new AttrValueConfig();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "switch") continue;
                if (xel.Name == "level")
                {
                    level = uint.Parse(xel.InnerText);
                }
                else
                {
                    PropertyModify modify = new PropertyModify();
                    string[] temp = xel.InnerText.Split('&');
                    modify.basic = uint.Parse(temp[0]);
                    modify.hedging = float.Parse(temp[1]);
                    data.Add(modify);
                }
            }
            NPCmodify.Add(level, data);
        }
    }

    public NPCConfig GetConfigData(uint ID)
    {
        if (NPCDatas.ContainsKey(ID))
        {
            return NPCDatas[ID];
        }
        else
        {
            string log = string.Format("{0}.xml 没有找到 ID={1}", GlobalConst.DIR_XML_NPC, ID);
            Debug.LogError(log);
            return null;
        }
    }

    public AttrData GetTourNPCAttrData(uint NPCID , uint id)
    {
		uint position;
        NPCConfig npcConfig = GetConfigData(NPCID);
        if (npcConfig != null)
        {
            position = npcConfig.position;
        }
        else
        {
            position = (uint)(GameSystem.Instance.RoleBaseConfigData2.GetConfigData(NPCID).position);
        }
        uint level = MainPlayer.Instance.Level;
        AttrData attr = new AttrData();
        AttrData npcattr = TourNpcs.Find(x => x.position == position && x.level == level).attrs;
		TourData tourData = GameSystem.Instance.TourConfig.GetTourData(level, id);
		uint basic = (uint)(Random.Range((int)tourData.baseAttrLower, (int)tourData.baseAttrUpper + 1));
		float hedging = Random.Range(tourData.hedgingAttrLower, tourData.hedgingAttrUpper);
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

        Debug.Log("================================================NPC:" + NPCID);
        Debug.Log("shoot_near:" +  attr.attrs["shoot_near"]);
        Debug.Log("shoot_middle:" + attr.attrs["shoot_middle"]);
        Debug.Log("anti_disturb:" + attr.attrs["anti_disturb"]);
        Debug.Log("steal:" +  attr.attrs["steal"]);
        Debug.Log("rebound:" + attr.attrs["rebound"]);
        Debug.Log("disturb:" + attr.attrs["disturb"]);
        Debug.Log("strength:" + attr.attrs["strength"]);
        //return TourNpcs.Find(x => x.position == NPCDatas[NPCID].position && x.level == MainPlayer.Instance.Level).attrs;
        return attr;
    }
    
    public AttrData GetQualifyingNPCAttrData(uint id, uint level)
    {
        uint position = (uint)GameSystem.Instance.RoleBaseConfigData2.GetPosition(id);
        AttrData attr = new AttrData();
        AttrData npcattr = TourNpcs.Find(x => x.position == position && x.level == level).attrs;
        RobotTeam qualifyingData = GameSystem.Instance.qualifyingConfig.GetRobotPlayerAttr(level);
        uint basic = qualifyingData.basic;
        float hedging = qualifyingData.hedging;
        attr.attrs["shoot_near"] = npcattr.attrs["shoot_near"] + basic;
        attr.attrs["shoot_middle"] = npcattr.attrs["shoot_middle"] + basic;
        attr.attrs["shoot_far"] = npcattr.attrs["shoot_far"] + basic;
        attr.attrs["dunk_near"] = npcattr.attrs["dunk_near"] + basic;
        attr.attrs["dunk_middle"] = npcattr.attrs["dunk_middle"] + basic;
        attr.attrs["layup_near"] = npcattr.attrs["layup_near"] + basic;
        attr.attrs["layup_middle"] = npcattr.attrs["layup_middle"] + basic;
        attr.attrs["anti_disturb"] = (uint)(npcattr.attrs["anti_disturb"] * hedging);
        attr.attrs["pass"] = npcattr.attrs["pass"] + basic;
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
        //return TourNpcs.Find(x => x.position == NPCDatas[NPCID].position && x.level == MainPlayer.Instance.Level).attrs;
        return attr;
    }


    public AttrData GetNPCAttrData(uint NPCID)
    {
        if (attrs.ContainsKey(NPCID))
        {
            return attrs[NPCID];
        }
        return null;
    }

    public uint GetShapeID(uint ID)
    {
        var data = GetConfigData(ID);
        if (data != null)
            return data.shape;
        return 0;
    }

    public string GetName(uint ID)
    {
        var data = GetConfigData(ID);
        if (data != null)
            return data.name;
        return "";
    }

    public string GetIcon(uint ID)
    {
        var data = GetConfigData(ID);
        if (data != null)
            return data.icon;
        return "";
    }

}
