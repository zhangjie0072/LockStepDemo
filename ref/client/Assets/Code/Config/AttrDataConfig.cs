using fogs.proto.msg;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;


public class AttrData
{
    public uint level = 0;
    public uint quality = 0;
    public uint bias = 0;
    public uint talent = 0;
	public IM.Number fightingCapacity;
    public List<uint> skills = new List<uint>();
    public Dictionary<string, uint> attrs = new Dictionary<string, uint>();
}

//队长属性数据
public class CaptainAttrData
{
    public List<AttrData> captainAttrs = new List<AttrData>();
}

//队员属性数据
public class RoleAttrData
{
    public List<AttrData> roleAttrs = new List<AttrData>();
}

//机器人属性数据
public class RobotAttrData
{
    public uint min_level;
    public uint max_level;
	public uint winning_streak;
	public PositionType position;
	public uint AIID;
	public AttrData attrData;
}

//篮板高度限制
public class ReboundAttrConfig
{
    string name = GlobalConst.DIR_XML_REBOUNDRANGE;
    bool isLoadFinish = false;
    private object LockObject = new object();

	public class ReboundAttr
	{
		public IM.Number maxHeight;
		public IM.Number minHeight;
		public IM.Number reboundHeightScale;
		public IM.Number ballHeightScale;
	}

	private Dictionary<PositionType, ReboundAttr> reboundAttrs = new Dictionary<PositionType, ReboundAttr>();

	public ReboundAttrConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_REBOUNDRANGE, LoadFinish);
		//ReadReboundData();
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
        lock (LockObject){ GameSystem.Instance.readConfigCnt += 1; }

		Logger.ConfigBegin(name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        reboundAttrs.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_REBOUNDRANGE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
		foreach (XmlElement xe in nodelist)
		{
			if (CommonFunction.IsCommented(xe))
				continue;

			PositionType position = (PositionType)(int.Parse(xe.SelectSingleNode("position").InnerText));
			ReboundAttr data = new ReboundAttr();
			data.minHeight = IM.Number.Parse(xe.SelectSingleNode("height_min").InnerText);
			data.maxHeight = IM.Number.Parse(xe.SelectSingleNode("height_max").InnerText);
			data.reboundHeightScale = IM.Number.Parse(xe.SelectSingleNode("rebound_height_scale").InnerText);
			data.ballHeightScale = IM.Number.Parse(xe.SelectSingleNode("ball_height_scale").InnerText);
			reboundAttrs.Add(position, data);
		}
		Logger.ConfigEnd(name);
	}

	public ReboundAttr GetReboundAttr(PositionType position)
	{
		ReboundAttr attr = null;
		reboundAttrs.TryGetValue(position, out attr);
		return attr;
	}
}

//扣篮改动机率
public class DunkRateConfig
{
    string name = GlobalConst.DIR_XML_DUNK_RATE;
    bool isLoadFinish = false;
    private object LockObject = new object();

	private Dictionary<PositionType, IM.Number> rates = new Dictionary<PositionType, IM.Number>();

	public DunkRateConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadConfig();
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

        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name);
            return;
        }
        
		rates.Clear();

        XmlDocument doc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_DUNK_RATE, text);
        XmlNode root = doc.SelectSingleNode("Data");
		foreach (XmlNode line in root.SelectNodes("Line"))
		{
			if (CommonFunction.IsCommented(line))
				continue;

			PositionType position = (PositionType)(int.Parse(line.SelectSingleNode("position").InnerText));
			IM.Number rate = IM.Number.Parse(line.SelectSingleNode("dunk_rate").InnerText);
			rates.Add(position, rate);
		}
	}

	public IM.Number GetRate(PositionType positioin)
	{
	    IM.Number rate = IM.Number.zero;
		rates.TryGetValue(positioin, out rate);
		return rate;
	}
}


public class AttrDataConfig
{
    //string name1 = GlobalConst.DIR_XML_CAPTAINATTR;
    //string name2 = GlobalConst.DIR_XML_ROLEATTR;
    string name3 = GlobalConst.DIR_XML_ROBOTATTR;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    //public Dictionary<uint, CaptainAttrData> captainAttrDatas = new Dictionary<uint, CaptainAttrData>();
    //public Dictionary<uint, RoleAttrData> roleAttrDatas = new Dictionary<uint, RoleAttrData>();
    public List<RobotAttrData> robotAttrDatas = new List<RobotAttrData>();

    public AttrDataConfig()
    {
        Initialize();
    }

    public void Initialize()
    {
        //ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_CAPTAINATTR, LoadFinish);
        //ReadCaptainAttrData();
        //ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ROLEATTR);
        //ReadRoleAttrData();
        ResourceLoadManager.Instance.GetConfigResource(GlobalConst.DIR_XML_ROBOTATTR, LoadFinish);
        //ReadRobotAttrData();
    }

    void LoadFinish(string vPath, object obj)
    {
        ++count;
        if (count == 1)
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

        //ReadCaptainAttrData();
        //ReadRoleAttrData();
        ReadRobotAttrData();
    }

    /*
    public void ReadCaptainAttrData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        captainAttrDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_CAPTAINATTR, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            uint id = 0;
            AttrData attrData = new AttrData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "ID")
                {
                    uint.TryParse(xel.InnerText, out id);
                }
                else
                {
                    ReadAttrData(xel, ref attrData);
                }
            }
            if (!captainAttrDatas.ContainsKey(id))
            {
                CaptainAttrData captainData = new CaptainAttrData();
                captainData.captainAttrs.Add(attrData);
                captainAttrDatas.Add(id, captainData);
            }
            else
            {
                captainAttrDatas[id].captainAttrs.Add(attrData);
            }
        }
    }
     * */

    /*
    public void ReadRoleAttrData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        roleAttrDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROLEATTR, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            uint id = 0;
            AttrData attrData = new AttrData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "ID")
                {
                    uint.TryParse(xel.InnerText, out id);
                }
                else
                {
                    ReadAttrData(xel, ref attrData);
                }
            }
            if (!roleAttrDatas.ContainsKey(id))
            {
                RoleAttrData roleData = new RoleAttrData();
                roleData.roleAttrs.Add(attrData);
                roleAttrDatas.Add(id, roleData);
            }
            else
            {
                roleAttrDatas[id].roleAttrs.Add(attrData);
            }
        }
    }
     * */

    public void ReadRobotAttrData()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name3);
            return;
        }
        robotAttrDatas.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_ROBOTATTR, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

			RobotAttrData data = new RobotAttrData();
            data.attrData = new AttrData();
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "min_level")
                {
                    uint.TryParse(xel.InnerText, out data.min_level);
                }
                else if (xel.Name == "max_level")
                {
                    uint.TryParse(xel.InnerText, out data.max_level);
                }
                else if (xel.Name == "winning_streak")
                {
                    uint.TryParse(xel.InnerText, out data.winning_streak);
                }
                else if (xel.Name == "position")
                {
                    uint value = 0;
                    uint.TryParse(xel.InnerText, out value);
                    data.position = (PositionType)value;
                }
                else if (xel.Name == "passive_skill")
                {
                    string[] tokens = xel.InnerText.Split('&');
                    foreach (string token in tokens)
                    {
                        uint skill_id;
                        if (uint.TryParse(token, out skill_id))
                            data.attrData.skills.Add(skill_id);
                    }
                }
                else if (xel.Name == "active_skill")
                {
                    string[] tokens = xel.InnerText.Split('&');
                    foreach (string token in tokens)
                    {
                        uint skill_id;
                        if (uint.TryParse(token, out skill_id))
                            data.attrData.skills.Add(skill_id);
                    }
                }
                else
                    ReadAttrData(xel, ref data.attrData);
            }

			var existed = (from d in robotAttrDatas
						where d.min_level == data.min_level &&
							d.max_level == data.max_level &&
							d.winning_streak == data.winning_streak &&
							d.position == data.position
						select d).FirstOrDefault();
            if (existed == null)
            {
                robotAttrDatas.Add(data);
            }
            else
            {
                Logger.LogError(string.Format("Robot attr config repeating. min_level:{0} max_level:{1} winning_streak:{2} position:{3}",
                    data.min_level, data.max_level, data.winning_streak, data.position));
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

    //队长属性列表
    public AttrData GetCaptainAttrData(uint captainID)
    {
        /*
        uint level = 1;
        RoleInfo captainInfo = MainPlayer.Instance.GetCaptainInfo(MainPlayer.Instance.CaptainID);
        if (captainInfo != null && captainID == captainInfo.id)
            level = MainPlayer.Instance.Level;

		return GetCaptainAttrData(captainID, level);
         * */
        return null;
    }

    /*
    public AttrData GetCaptainAttrData(uint captainID, uint level)
    {
		if (captainAttrDatas.ContainsKey(captainID))
		{
			CaptainAttrData data = captainAttrDatas[captainID];
			for (int i = 0; i < data.captainAttrs.Count; ++i)
			{
				//if (data.captainAttrs[i].level == level)
				{
					return data.captainAttrs[i];
				}
			}
		}
        if (roleAttrDatas.ContainsKey(captainID))
        {
            RoleAttrData data = roleAttrDatas[captainID];
            for (int i = 0; i < data.roleAttrs.Count; ++i)
            {
                //if (data.roleAttrs[i].quality == quality)
                {
                    return data.roleAttrs[i];
                }
            }
        }
		return null;
		//return GetRoleAttrData(captainID, level);
    }
     * */

    /*
    //队员属性列表
    public AttrData GetRoleAttrData(uint roleID, uint quality)
    {
        if (roleAttrDatas.ContainsKey(roleID))
        {
            RoleAttrData data = roleAttrDatas[roleID];
            for (int i = 0; i < data.roleAttrs.Count; ++i)
            {
                //if (data.roleAttrs[i].quality == quality)
                {
                    return data.roleAttrs[i];
                }
            }
        }
		if (captainAttrDatas.ContainsKey(roleID))
		{
			CaptainAttrData data = captainAttrDatas[roleID];
			for (int i = 0; i < data.captainAttrs.Count; ++i)
			{
				//if (data.captainAttrs[i].level == quality)
				{
					return data.captainAttrs[i];
				}
			}
		}
        return null;
    }
     * */

    public RobotAttrData GetRobotAttr(uint level, uint winning_streak, PositionType position)
    {
		return (from d in robotAttrDatas
				where d.min_level <= level && level <= d.max_level &&
				d.winning_streak == winning_streak &&
				d.position == position
				select d).FirstOrDefault();
    }
}
