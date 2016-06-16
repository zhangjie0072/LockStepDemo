using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;


public class VipData
{
    public uint consume;
    public uint hp_times;
    public uint gold_times;
    public uint career_times;
    public uint regular_times;
    public uint reset_regular_times;
    public uint reset_tour_times;
    public uint add_training_count;
    public uint skill_slot;
    public uint add_vigor_limit;
    public uint mail_id;
    public uint gift;
	public uint ori_gift_price;
	public uint gift_price;
    public uint append_sign;
    public uint bullfight_buytimes;
    public uint shoot_buytimes;
	public uint qualifying_buytimes;
    public List<uint> exp_buytimes = new List<uint>();
}


public class VipState
{
	public uint level;
	public List<string> states = new List<string>();
}

public class Recharge
{
	public uint id;
    public string name;
	public uint recharge;
	public uint diamond;
	public uint ext_diamond;
	public string first_intro;
	public string intro;
	public uint recommend;
	public string icon;
	public string des;
}

public class VipPrivilegeConfig
{
    string name1 = GlobalConst.DIR_XML_VIPPRIVILEGE;
    string name2 = GlobalConst.DIR_XML_RECHARGE;
    string name3 = GlobalConst.DIR_XML_VIPPRIVILEGE_STATE;
    bool isLoadFinish = false;
    private object LockObject = new object();
    uint count = 0;

    public Dictionary<uint, VipData> Vipdatas = new Dictionary<uint, VipData>();
	public Dictionary<uint, VipState> vipStates = new Dictionary<uint,VipState>();
	public Dictionary<uint,Recharge> recharges = new Dictionary<uint, Recharge>();
	public uint maxVip = 0;
	
    public VipPrivilegeConfig()
    {
        ResourceLoadManager.Instance.GetConfigResource(name1, LoadFinish);
        //ReadVipPrivilege();
        ResourceLoadManager.Instance.GetConfigResource(name2, LoadFinish);
		//ReadVipPrivilegeState();
        ResourceLoadManager.Instance.GetConfigResource(name3, LoadFinish);
        //ReadRecharge();
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
		Logger.ConfigBegin(name1);
		ReadVipPrivilege();
		Logger.ConfigEnd(name1);
		Logger.ConfigBegin(name2);
		ReadVipPrivilegeState();
		Logger.ConfigEnd(name2);
		Logger.ConfigBegin(name3);
		ReadRecharge();
		Logger.ConfigEnd(name3);
    }
	
    public VipData GetVipData(uint level)
    {
        VipData vipData = null;
        Vipdatas.TryGetValue(level, out vipData);
        return vipData;
    }

    public uint GetVipRestTimes( uint level)
    { 
        VipData vipData = GetVipData(level);
        return vipData.reset_tour_times;
    }

    public uint GetVipCareerResetTimes(uint level)
    {
        VipData vipData = GetVipData(level);
        return vipData.career_times;
    }
    void ReadVipPrivilege()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name1);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name1);
            return;
        }
        
        Vipdatas.Clear();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_VIPPRIVILEGE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            VipData vipdata = new VipData();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "level")
                {
                    uint.TryParse(xel.InnerText, out level);
					if( level > maxVip )
					{
						maxVip = level;
					}
                }
                else if (xel.Name == "consume")
                {
                    uint.TryParse(xel.InnerText, out vipdata.consume);
                }
                else if (xel.Name == "hp_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.hp_times);
                }
                else if (xel.Name == "gold_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.gold_times);
                }
                else if (xel.Name == "career_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.career_times);
                }
                else if (xel.Name == "regular_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.regular_times);
                }
                else if (xel.Name == "reset_regular_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.reset_regular_times);
                }
                else if (xel.Name == "reset_tour_times")
                {
                    uint.TryParse(xel.InnerText, out vipdata.reset_tour_times);
                }
                else if (xel.Name == "add_training_count")
                {
                    uint.TryParse(xel.InnerText, out vipdata.add_training_count);
                }
                else if (xel.Name == "skill_slot")
                {
                    uint.TryParse(xel.InnerText, out vipdata.skill_slot);
                }
                else if (xel.Name == "add_vigor_limit")
                {
                    uint.TryParse(xel.InnerText, out vipdata.add_vigor_limit);
                }
                else if (xel.Name == "mail_id")
                {
                    uint.TryParse(xel.InnerText, out vipdata.mail_id);
                }
                else if (xel.Name == "mail_id")
                {
                    uint.TryParse(xel.InnerText, out vipdata.mail_id);
                }
				else if (xel.Name == "gift")
                {
                    uint.TryParse(xel.InnerText, out vipdata.gift);
                }
				else if (xel.Name == "ori_gift_price")
                {
                    uint.TryParse(xel.InnerText, out vipdata.ori_gift_price);
                }

				else if (xel.Name == "gift_price")
                {
                    uint.TryParse(xel.InnerText, out vipdata.gift_price);
                }
                else if (xel.Name == "append_sign")
                {
                    uint.TryParse(xel.InnerText, out vipdata.append_sign);
                }
                else if (xel.Name == "bullfight_buytimes")
                {
                    uint.TryParse(xel.InnerText, out vipdata.bullfight_buytimes);
                }
                else if (xel.Name == "shoot_buytimes")
                {
                    uint.TryParse(xel.InnerText, out vipdata.shoot_buytimes);
                }
				else if (xel.Name == "qualify_buytimes")
				{
                    uint.TryParse(xel.InnerText, out vipdata.qualifying_buytimes);
                }
                else if (xel.Name.Contains("exp_buytimes"))
                {
                    uint times;
                    uint.TryParse(xel.InnerText, out times);
                    vipdata.exp_buytimes.Add(times);
                }		
            }
            Vipdatas.Add(level, vipdata);
        }
    }


    void ReadRecharge()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name2);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name2);
            return;
        }
        
        recharges.Clear();
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_RECHARGE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
            Recharge recharge = new Recharge();
            uint id = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "id")
                {
                    uint.TryParse(xel.InnerText, out id);
					recharge.id =id;
                }
                else if (xel.Name == "name")
                {
                    recharge.name = xel.InnerText;
                }
                else if (xel.Name == "recharge")
                {
                    uint.TryParse(xel.InnerText, out recharge.recharge);
                }
                else if (xel.Name == "diamond")
                {
                    uint.TryParse(xel.InnerText, out recharge.diamond);
                }
                else if (xel.Name == "ext_diamond")
                {
                    uint.TryParse(xel.InnerText, out recharge.ext_diamond);
                }
                else if (xel.Name == "first_intro")
                {
					recharge.first_intro = xel.InnerText;
                }
                else if (xel.Name == "intro")
                {
					recharge.intro = xel.InnerText;
                }
                else if (xel.Name == "recommend")
                {
                    uint.TryParse(xel.InnerText, out recharge.recommend);
                }
                else if (xel.Name == "icon")
                {
					recharge.icon = xel.InnerText;
                }
                else if (xel.Name == "des")
                {
					recharge.des = xel.InnerText;
                }
            }
            recharges.Add(id, recharge);
        }
    }



    void ReadVipPrivilegeState()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name3);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name3);
            return;
        }
        

// 		public class VipPrivilegeState
// {
// 	public uint level;
// 	public List<string> states = new List<string>();
// }

		vipStates.Clear();

        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_VIPPRIVILEGE_STATE, text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("Data").ChildNodes;
        foreach (XmlElement xe in nodelist)
        {
            XmlNode comment = xe.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;
			VipState state = new VipState();
            uint level = 0;
            foreach (XmlElement xel in xe)
            {
                if (xel.Name == "VipLv")
                {
                    uint.TryParse(xel.InnerText, out level);
					state.level = level;
                }
				else if(xel.Name.Contains("privilegestate"))
				{
					state.states.Add(xel.InnerText);
				}
            }
			Logger.Log("level="+level);
            vipStates.Add(level, state);
        }
    }


	public VipState GetVipState(uint level)
	{
		VipState state = null;
		vipStates.TryGetValue( level, out state);
		return state;
	}

	public uint GetVipLevel(uint exp)
	{
		uint level = 0;
		foreach (KeyValuePair<uint, VipData> pair in Vipdatas)
		{
			if (exp >= pair.Value.consume)
				level = pair.Key;
			else
				break;
		}
		return level;
	}


    /// <summary>
    /// 根据购买次数返回购体力次数最大值
    /// </summary>
    /// <param name="hp_times"></param>
    /// <returns></returns>
    public uint GetBuyhp_times(uint level)
    {

        //Logger.Log("--------------------buyhptimes:"+Vipdatas[level].hp_times);
        foreach (KeyValuePair<uint, VipData> a in Vipdatas)
        {
            Logger.Log("-------key=" + a.Key);
        }
        Logger.Log("------------level=" + level);
        return Vipdatas[level].hp_times;
    }

    /// <summary>
    /// 根据购买次数返回购金币次数最大值
    /// </summary>
    /// <param name="gold_times"></param>
    /// <returns></returns>
    public uint GetBuygold_times(uint level)
    {

        Logger.Log("--------------------buygoldtimes:" + Vipdatas[level].gold_times);
        return Vipdatas[level].gold_times;
    }

    /// <summary>
    /// 根据viplevel返回补签次数
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public uint GetAppendSignTimes(uint level)
    {
        return Vipdatas[level].append_sign;
    }

    /// <summary>
    /// 根据viplevel返回斗牛赛购买次数
    /// </summary>
    /// <param name="bullfight_buytimes"></param>
    /// <returns></returns>
    public uint GetBullFightBuyTimes(uint level)
    {
        return Vipdatas[level].bullfight_buytimes;
    }

    /// <summary>
    /// 根据viplevel返回投篮赛赛购买次数
    /// </summary>
    /// <param name="shoot_buytimes"></param>
    /// <returns></returns>
    public uint GetShootGameBuyTimes(uint level)
    {
        return Vipdatas[level].shoot_buytimes;
    }

	public uint GetQualifyingBuyTimes(uint level)
    {
        return Vipdatas[level].qualifying_buytimes;
    }

    public uint GetCareerBuyTimes(uint level)
    {
        return Vipdatas[level].career_times;
    }
}

